using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrasGames.Model.DTO.ServiceDTO
{
    public class ControllerDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }   
        public string? Type { get; set;}
        public int Year {get; set;}
        public float Price {get; set;}
        
    }
}