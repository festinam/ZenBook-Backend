namespace ZenBook_Backend.Models
{
    public class Course : IMustHaveTenant
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }  // Foreign Key to Instructor
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Category { get; set; }  // Category: Pilates, Yoga, etc.
        public int DurationInHours { get; set; }
        public ICollection<Session> Sessions { get; set; }  // Sessions belonging to the course
        public string TenantId { get; set; }

    }
}
