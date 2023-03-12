using System.ComponentModel.DataAnnotations;

namespace JuanProject.ViewModels
{
    public class CategoryCreateVM
    {
        [Required, MinLength(3)]
        public string Name { get; set; }
    }
}
