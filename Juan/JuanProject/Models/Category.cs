using System.ComponentModel.DataAnnotations;

namespace JuanProject.Models
{
    public class Category: BaseEntity
    {
        [Required, MinLength(3)]
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
