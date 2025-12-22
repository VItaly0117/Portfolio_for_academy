namespace About_me.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; } // Профессия
        public string Description { get; set; } // О себе
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<Skill> Skills { get; set; } = new List<Skill>(); // Список навыков
    }
}