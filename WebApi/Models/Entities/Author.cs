using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.Entities;

public class Author
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public DateTime Birthdate { get; set; }
}
