namespace Lab8.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public Animal(int id, string name, string type) 
        {
            Id = id;
            Name = name;
            Type = type;
        }
    }
}
