﻿namespace ZenBook_Backend.DTOs
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string ProfilePictureUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
