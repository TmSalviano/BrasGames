using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrasGames.Model.BusinessModels
{
    public class DayStats
    {
        [Key, DataType(DataType.Date)]
        public DateTime Id { get; set; }

        [Range(0, int.MaxValue)]
        public int TotalConsumers { get; set; }
        [Range(0, int.MaxValue)]
        public int TotalProfit { get; set; }
        [Range(0, int.MaxValue)]
        public int TotalCost { get; set; }
    }
}