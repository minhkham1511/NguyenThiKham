using System.ComponentModel.DataAnnotations;

namespace NguyenThiKhambth2.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string? address { get; set; }
        public int number { get; set; }
    }
}