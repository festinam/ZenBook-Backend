namespace ZenBook_Backend.DTOs
{
    public class InstructorDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Department { get; set; }
        public string Bio { get; set; }
        public bool IsActive { get; set; }
    }
}
