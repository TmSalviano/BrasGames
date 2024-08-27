using System.ComponentModel.DataAnnotations;
using BrasGames.Model.ServiceModels;

public class OrderListValidation : ValidationAttribute {
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (validationContext.ObjectInstance is Order) {
            var order = (Order)validationContext.ObjectInstance;
            if (order.Consoles == null && order.Games == null && order.Controllers == null)
                return new ValidationResult("Order is invalid for not buying anything");

            return ValidationResult.Success;
        }

        return new ValidationResult("the object is not of Order type.");
    }
}