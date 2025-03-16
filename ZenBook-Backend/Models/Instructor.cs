namespace ZenBook_Backend.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Department { get; set; }  // Department or specialization (Pilates/Yoga)
        public string Bio { get; set; }
        public bool IsActive { get; set; }  // To check if instructor is currently active
        public ICollection<Course> Courses { get; set; }  // Navigation property
    }
}
