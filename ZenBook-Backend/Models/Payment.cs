namespace ZenBook_Backend.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }  // Foreign Key to Client
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }  // E.g., Credit Card, PayPal
        public bool IsSuccessful { get; set; }
        public bool IsRefunded { get; set; }  // Added field for refunds
    }
}
