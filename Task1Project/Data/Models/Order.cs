using System.Collections.Generic;

namespace Task1Project.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<int> BookIds { get; set; }
    }
}