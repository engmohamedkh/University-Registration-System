using System.ComponentModel.DataAnnotations;

namespace CUD01.Validators
{
    public class DateInPastAttribute:ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            DateTime ?date = value as DateTime?;
            if(date is null) 
            { 
                return false;
            }
            if(date < DateTime.Now)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
