using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Data;

public class Category
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The Name field is required.")]
    public string Name { get; set; } = string.Empty;

}
