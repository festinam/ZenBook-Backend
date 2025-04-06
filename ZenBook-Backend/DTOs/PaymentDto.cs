namespace ZenBook_Backend.DTOs
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public bool IsSuccessful { get; set; }
        public bool IsRefunded { get; set; }
    }
}
