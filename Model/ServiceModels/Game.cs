using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BrasGames.Model.ServiceModels
{
    public class Game
    {
        [NotNull]
        public string? Name { get; set; }
        [NotNull]
        public string? AgeRestriction { get; set; }
        [NotNull]
        public string? Genre { get; set; }

        public float Price { get; set; }
    }
}