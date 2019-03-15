using SQLite;

namespace MyProject.Contracts.Persistence.Domain
{
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        public double Price { get; set; }
    }
}
