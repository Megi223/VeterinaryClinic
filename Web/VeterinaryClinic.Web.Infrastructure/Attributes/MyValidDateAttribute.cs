using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VeterinaryClinic.Web.Infrastructure.Attributes
{
    public class MyValidDateAttribute : ValidationAttribute
    {
        private DateTime minDate = new DateTime(2000, 1, 1);

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var date = value as DateTime?;
            if (date != null)
            {
                if (date <= this.minDate || date > DateTime.UtcNow)
                {
                    return new ValidationResult(this.GetErrorMessage());
                }

                return ValidationResult.Success;
            }

            return new ValidationResult(this.GetErrorMessage());
        }

        public string GetErrorMessage()
        {
            return $"Invalid date!";
        }
    }
}
