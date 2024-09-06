using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Console = BrasGames.Model.ServiceModels.Console;

namespace BrasGames.Model.DTO.ServiceDTO
{
    public class ConsoleDTO
    {
        public int Id { get; set; }
        
        public string? Name { get; set; }
        
        public string? Type { get; set;}
        
        public DateTime ReleaseYear { get; set; }
        
        public float Price { get; set; }
    
        public ConsoleDTO (Console console) {
            Id = console.Id;
            Name = console.Name;
            Type = console.Type;
            ReleaseYear = console.ReleaseYear;
            Price = console.Price;
        }
    }
}