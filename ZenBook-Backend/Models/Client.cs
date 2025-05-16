namespace ZenBook_Backend.Models
{
    public class Client : IMustHaveTenant
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string ProfilePictureUrl { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Session> Sessions { get; set; }  // Navigation property for sessions
        public ICollection<Payment> Payments { get; set; }  // Navigation property for payments

        public string TenantId { get; set; }
    }
}
