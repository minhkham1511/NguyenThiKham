using System.ComponentModel.DataAnnotations;

namespace NguyenThiKhambth2.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public decimal Price { get; set; }
    }
}