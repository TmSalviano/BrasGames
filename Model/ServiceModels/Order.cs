using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BrasGames.Model.ServiceModels
{
    public class Order
    {
        public int Id { get; set; }
        public List<Console>? Consoles{ get; set; }
        public List<Controller>? Controllers{ get; set; }
        public List<Game>? Games{ get; set; } 

        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? GovernmentName { get; set; }
        public string? CPF {get; set;}
        public OrderState orderState { get; set; }
    }
}