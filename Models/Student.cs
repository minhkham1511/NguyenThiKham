using System.Data;
using System.ComponentModel.DataAnnotations;
namespace NguyenThiKhambth2.Models
{
    public class Student
    {
        [Required(ErrorMessage = "Id khong duoc de trong")]
        public int ID { get ; set;}
        [Required(ErrorMessage = "Name khong duoc de trong")]
        public string ?Name { get ; set; }
    }
}