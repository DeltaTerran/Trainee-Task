using System.ComponentModel.DataAnnotations;

namespace Trainee_Task.Models
{
    public class PersonModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Имя обязательно")]
        [StringLength(50, ErrorMessage = "Имя не может быть длиннее 50 символов")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Дата рождения обязательна")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        public bool Maried { get; set; }

        
        public string Phone { get; set; } = null!;

        [Range(0, 1000000)]
        public decimal Salary { get; set; }
    }
}
