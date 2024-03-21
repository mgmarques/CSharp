using System.ComponentModel.DataAnnotations;

namespace GroupingAggregation.Models
{
	public class Transaction
    {
        // , , 
        public string TransactionId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; } 
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Price { get; set; }

        public Transaction(string transactionId, string productId,
            int quantity, double price)
        {
            TransactionId = transactionId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        public override string ToString() =>
            $"TransactionId={TransactionId} ProductID={ProductId} " +
            $"Price={Price} InvoiceValue={Quantity * Price}";

    }
}
