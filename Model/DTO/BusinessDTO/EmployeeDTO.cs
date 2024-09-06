using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrasGames.Model.BusinessModels;

namespace BrasGames.Model.DTO.BusinessDTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Password {get; set;}

        public string? Email {get; set;}

        public int Age { get; set;} = 0;

        public int YearsWorked {get; set;}= 0;

        public SexType Sex {get; set;}

        public bool IsFired {get; set;} = false;
        
        public DateTime EndOfContract {get; set;}

        public float Salary {get; set;}

        public EmployeeDTO(Employee employee) {
            Id = employee.Id;
            Name = employee.Name;
            Password = employee.Password;
            Email = employee.Email;
            Age = employee.Age;
            YearsWorked = employee.YearsWorked;
            Sex = employee.Sex;
            IsFired = employee.isFired;
            EndOfContract = employee.EndOfContract;
            Salary = employee.Salary;
        }
    }
}