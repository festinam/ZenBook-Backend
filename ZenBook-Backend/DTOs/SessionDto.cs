namespace ZenBook_Backend.DTOs
{
    public class SessionDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int InstructorId { get; set; }
        public int ClientId { get; set; }
        public DateTime SessionDate { get; set; }
        public TimeSpan SessionTime { get; set; }
        public string Location { get; set; }
        public string Topic { get; set; }
        public bool IsCompleted { get; set; }
    }
}
