using System;

namespace Task1Project.Data.Models
{
    public class Loan
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowedAt { get; set; }
        public DateTime? ReturnedAt { get; set; }
    }
}