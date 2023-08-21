using System.ComponentModel.DataAnnotations;

namespace Test3.Models
{
    public class BookStore
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string Author {get; set;}
        public string Image { get; set; }

    }
}
