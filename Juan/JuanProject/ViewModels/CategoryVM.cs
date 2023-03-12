using JuanProject.Models;
using System.ComponentModel.DataAnnotations;

namespace JuanProject.ViewModels
{
    public class CategoryVM
    {
        [Required, MinLength(3)]
        public string Name { get; set; }
    }
}
