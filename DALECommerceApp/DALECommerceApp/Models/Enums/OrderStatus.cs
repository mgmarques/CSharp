namespace DALECommerceApp.Models.Enums
{
    public enum OrderStatus : int
    {
        Peding = 0,
        PaymentConfirmed = 1,
        Processing = 2,
        Shipped = 3,
        Delivered = 4,
        Canceled = 5
    }
}
