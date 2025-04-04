namespace ZenBook_Backend.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Category { get; set; }
        public int DurationInHours { get; set; }
        public int InstructorId { get; set; }
    }
}
