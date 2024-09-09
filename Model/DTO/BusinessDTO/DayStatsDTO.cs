using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrasGames.Model.BusinessModels;

namespace BrasGames.Model.DTO.BusinessDTO
{
    public class DayStatsDTO
    {
        public int Id { get; set; }
        
        public DateTime Day { get; set; }

        public int TotalConsumers { get; set; }
        public int TotalProfit { get; set; }
        public int TotalCost { get; set; }
    }
}