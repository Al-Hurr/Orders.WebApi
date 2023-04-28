using Microsoft.EntityFrameworkCore;
using Orders.WebApi.Abstractions;
using Orders.WebApi.Domain.Orders.Entities;
using Orders.WebApi.Domain.Orders.Models;

namespace Orders.WebApi.Domain.Orders.Services
{
    public class OrderService
    {
        private readonly IDataStore _dataStore;

        public OrderService(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public OrderGetModel Get(Guid id)
        {
            var entity = _dataStore.GetAll<Order>()
                .Include(x => x.Lines)
                .FirstOrDefault(x => x.Id == id);

            if (entity == null)
            {
                return null;
            }

            return OrderGetModel.FromEntity(entity);
        }

        public OrderGetModel Update(Guid id, OrderEditModel editModel, out string errorMessage)
        {
            errorMessage = string.Empty;
            var entity = _dataStore.GetAll<Order>()
                .FirstOrDefault(x => x.Id == id);

            if (entity == null)
            {
                return null;
            }

            if(entity.Status == Enums.OrderStatus.Paid
                || entity.Status == Enums.OrderStatus.HandedForDelivery
                || entity.Status == Enums.OrderStatus.Delivered
                || entity.Status == Enums.OrderStatus.Completed)
            {
                errorMessage = "Can't edit order with statuses Paid, HandedForDelivery, Delivered, Completed";
                return null;
            }

            editModel.ApplyToEntity(entity);

            _dataStore.Update(entity);

            return OrderGetModel.FromEntity(entity);
        }

        public OrderGetModel Create(OrderCreateModel model)
        {
            var entity = new Order();
            entity.Created = DateTime.UtcNow;

            model.ApplyToEntity(entity);

            _dataStore.Create(entity);

            return OrderGetModel.FromEntity(entity);
        }

        public bool TryRemove(Guid id, out string errorMessage)
        {
            errorMessage = string.Empty;

            var entity = _dataStore.GetAll<Order>()
                .Include(x => x.Lines)
                .FirstOrDefault(x => x.Id == id);

            if (entity == null)
            {
                return false;
            }

            if(entity.Status == Enums.OrderStatus.HandedForDelivery
                || entity.Status == Enums.OrderStatus.Delivered
                || entity.Status == Enums.OrderStatus.Completed)
            {
                errorMessage = "Can't delete order with statuses HandedForDelivery, Delivered, Completed";
                return false;
            }

            _dataStore.Delete(entity);

            return true;
        }
    }
}
