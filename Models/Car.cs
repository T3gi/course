using System.ComponentModel.DataAnnotations;

namespace Phoenix.Models;

public class Car
{
    public int Id { get; set; }
    public string? Name { get; set; }
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }
    public string? Mark { get; set; }
    public decimal Price { get; set; }
}