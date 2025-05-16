namespace ZenBook_Backend.Models
{
    public class Session : IMustHaveTenant
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }  // Foreign Key to Course
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }  // Foreign Key to Instructor
        public int ClientId { get; set; }
        public Client Client { get; set; }  // Foreign Key to Client
        public DateTime SessionDate { get; set; }
        public TimeSpan SessionTime { get; set; }
        public string Location { get; set; }
        public string Topic { get; set; }
        public bool IsCompleted { get; set; }  // To check if session is completed
        public string TenantId { get; set; }

    }
}
