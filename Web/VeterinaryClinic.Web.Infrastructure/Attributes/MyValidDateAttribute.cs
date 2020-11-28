namespace VeterinaryClinic.Web.Infrastructure.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MyValidDateAttribute : ValidationAttribute
    {
        private DateTime minDate = new DateTime(2000, 1, 1);

        public override bool IsValid(
        object value)
        {
            var date = value as DateTime?;
            if (date != null)
            {
                if (date <= this.minDate || date > DateTime.UtcNow)
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        public string GetErrorMessage()
        {
            return $"Invalid date!";
        }
    }
}
