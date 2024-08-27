using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;


namespace BrasGames.Model.ServiceModels
{
    [OrderListValidation]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public List<Console>? Consoles{ get; set; }
        public List<Controller>? Controllers{ get; set; }
        public List<Game>? Games{ get; set; } 

        [Required, NotNull, DataType(DataType.Text)]
        public string? Address { get; set; }
        [Required, NotNull, DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required, NotNull]
        public string? GovernmentName { get; set; }
        [Required, NotNull]
        public string? CPF {get; set;}
    
        public OrderState orderState { get; set; } = OrderState.Waiting;
    }
}