using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BrasGames.Model.ServiceModels
{
    public class Console
    {
        [Key]
        public int Id { get; set; }
        
        [Required, NotNull]
        public string? Name { get; set; }
        
        [Required, NotNull]
        public string? Type { get; set;}
        
        [Required, YearRange, DataType(DataType.Date)]
        public DateTime ReleaseYear { get; set; }
        
        [Range(0, float.MaxValue), DataType(DataType.Currency)]
        public float Price { get; set; }

    }
}