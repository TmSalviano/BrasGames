using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BrasGames.Model.BusinessModels
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required, NotNull,]
        public string? Name { get; set; }

        [Required, NotNull, DataType(DataType.Password)]
        public string? Password {get; set;}

        [Required, NotNull, EmailAddress, DataType(DataType.EmailAddress)]
        public string? Email {get; set;}

        [Range(0, int.MaxValue)]
        public int Age { get; set;} = 0;

        [Range(0, int.MaxValue)]
        public int YearsWorked {get; set;}= 0;

        [Required]
        public SexType Sex {get; set;}

        public bool isFired {get; set;} = false;
        
        [Required, DataType(DataType.Date)]
        public DateTime EndOfContract {get; set;}

        [Range(0, float.MaxValue), DataType(DataType.Date)]
        public float Salary {get; set;}

    }
}