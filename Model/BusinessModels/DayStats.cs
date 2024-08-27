using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrasGames.Model.BusinessModels
{
    public class DayStats
    {
        public DateTime Id { get; set; }

        public int TotalConsumers { get; set; }
        public int TotalProfit { get; set; }
        public int TotalCost { get; set; }
    }
}