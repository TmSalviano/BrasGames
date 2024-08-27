using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BrasGames.Model.ServiceModels
{
    public class Game
    {
        [Required, NotNull]
        public string? Name { get; set; }
        [Required, NotNull]
        public string? AgeRestriction { get; set; }
        [Required, NotNull]
        public string? Genre { get; set; }
        
        [Range(0, float.MaxValue), DataType(DataType.Currency)]
        public float Price { get; set; }
    }
}