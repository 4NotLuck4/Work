namespace LabWork8.Model
{
    public class Visitor
    {
        public int Id { get; set; }
        public string Phone { get; set; } = null!;
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
