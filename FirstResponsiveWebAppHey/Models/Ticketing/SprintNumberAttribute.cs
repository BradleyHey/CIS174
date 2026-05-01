using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace FirstResponsiveWebAppHey.Models.Ticketing
{
    public class SprintNumberAttribute : ValidationAttribute, IClientModelValidator
    {
        protected override ValidationResult IsValid(object? value, ValidationContext ctx)
        {
            if (value is int sprintNum)
            {
                if (sprintNum > 0)
                {
                    return ValidationResult.Success!;
                }
            }
            return new ValidationResult(GetMsg(ctx.DisplayName ?? "Sprint Number"));
        }

        public void AddValidation(ClientModelValidationContext ctx)
        {
            if (!ctx.Attributes.ContainsKey("data-val"))
                ctx.Attributes.Add("data-val", "true");
            
            ctx.Attributes.Add("data-val-sprintnumber", GetMsg(ctx.ModelMetadata.DisplayName ?? ctx.ModelMetadata.Name ?? "Sprint Number"));
        }

        private string GetMsg(string name) =>
            base.ErrorMessage ?? $"{name} must be a positive number greater than zero.";
    }
}
