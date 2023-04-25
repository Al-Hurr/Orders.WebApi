namespace Orders.WebApi.Domain.Orders.Enums
{
    public enum OrderStatus
    {
        New,
        AwaitingPayment,
        Paid,
        HandedForDelivery,
        Delivered,
        Completed
    }
}
