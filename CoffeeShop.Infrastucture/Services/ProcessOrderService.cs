using CoffeeShop.Domain;
using CoffeeShop.Domain.Enumerations;
using CoffeeShop.Infrastucture.Repositories;
using Microsoft.Extensions.Hosting;
using System.Collections.Concurrent;

namespace CoffeeShop.Infrastucture.Services
{
    /// <summary>
    /// This class is responsible for processing orders. This is a background process 
    /// which simulate work of baristas in a coffee shop.
    /// </summary>
    public class ProcessOrderService : BackgroundService
    {
        private readonly IOrderRepository _orderRepository;
        private ConcurrentQueue<Barista> _baristaQueue;
        public ProcessOrderService(IBaristaRepository baristaRepository, 
            IOrderRepository orderRepository) 
        {
            //Initialize baristaQueue with all baristas from repository
            var baristas = baristaRepository.GetAllBaristas();
            _baristaQueue = new ConcurrentQueue<Barista>(baristas);

            _orderRepository = orderRepository;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                //First checking if there is an available barista in the queue
                Barista? barista;
                if (_baristaQueue.TryDequeue(out barista))
                {
                    if (QueueService.orderQueue.Count > 0)
                    {
                        Task.Run(async () => await AssignOrder(barista)).GetAwaiter();
                    }
                    else
                    {
                        _baristaQueue.Enqueue(barista);
                    }
                }

                await Task.Delay(1000, stoppingToken);
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        private async Task AssignOrder(Barista barista)
        {
            var order = QueueService.orderQueue.Dequeue();
            if (order != null)
            {
                //If the barista doesn't have enough coffee in the grinder, return the order to his queue
                //so that another barista can pick up this order while the current one refills the grinder.
                if (!barista.HasEnoughCoffeeInGrinder(order.AmountOfCoffee))
                {
                    var orderType = order.BartenderId != null ? OrderType.AtTable : OrderType.ToGo;

                    //Substract two second to ensure that order is still first for processing
                    QueueService.orderQueue.Enqueue(order, (orderType, DateTime.Now.AddSeconds(-2)));
                    await barista.RefillCoffeeGrinder();
                }
                else
                {
                    //update startProcessingAt prorerty
                    order.UpdateStartProcessingAt(DateTime.Now);
                    _orderRepository.ModifyOrder(order);

                    await barista.BrewCoffee(order.TimeToBrew, order.AmountOfCoffee);
                    //After the barista makes the coffee, update the completedAt property of the order.
                    order.UpdateCompletedAt(DateTime.Now);
                    _orderRepository.ModifyOrder(order);
                }
                //return barista back to the queue
                _baristaQueue.Enqueue(barista);
            }
        }
    }
}
