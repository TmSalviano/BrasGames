using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

public class Controller {
    [Required, NotNull]
    public string? Name { get; set; }   
    [Required, NotNull] 
    public string? Type { get; set;}
    [YearRange, DataType(DataType.Date)]
    public int Year {get; set;}
    [Range(0, float.MaxValue), DataType(DataType.Currency)]
    public float Price {get; set;}
}