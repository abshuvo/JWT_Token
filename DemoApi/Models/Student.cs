using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoApi.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StudentId { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? age {  get; set; }
        public int? number {  get; set; }
    }
}
