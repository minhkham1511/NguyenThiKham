using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NguyenThiKhambth2.Models
{
    public class Student
    {
        [Required(ErrorMessage = "Id khong duoc de trong")]
        public int ID { get ; set;}
        [Required(ErrorMessage = "Name khong duoc de trong")]
        public string ?Name { get ; set; }
         public string ?FacultyID { get ; set; }
         [ForeignKey("FacultyID")]
      public Facult Faculty  { get ; set; }
    }
}