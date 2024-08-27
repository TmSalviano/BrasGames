using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrasGames.Model.BusinessModels
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Password {get; set;}
        public string? Email {get; set;}
        public int Age { get; set;}
        public int YearsWorked {get; set;}
        public SexType sex {get; set;}
        public bool isFired {get; set;} = false;
        public DateTime EndOfContract {get; set;}
        public float Salary {get; set;}
    }
}