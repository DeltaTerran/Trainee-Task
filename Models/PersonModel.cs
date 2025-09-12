namespace Trainee_Task.Models
{
    public class PersonModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Birthday { get; set; }

        public bool Maried { get; set; }
        public string Phone { get; set; } = null!;
        public decimal Salary { get; set; }
       
    }
}
