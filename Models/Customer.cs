using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;

namespace Models
{
    public class Customer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// 定义了RuleSetA规则，该规则规定该Field的长度在1到20之间
        /// </remarks>
        [StringLengthValidator(1,20, Ruleset="RuleSetA",
            MessageTemplate="First Name must be between 1 and 50 characters")]
        public string FirstName { get; set; }

        [StringLengthValidator(1, 20, Ruleset = "RuleSetA",
            MessageTemplate = "Last Name must be between 1 and 50 characters")]
        public string LastName { get; set; }

        [RelativeDateTimeValidator(-120, DateTimeUnit.Year, -18, DateTimeUnit.Year, Ruleset = "RuleSetA",
            MessageTemplate = "Must be 18 years or older.")]
        public DateTime DateOfBirth { get; set; }

        [RegexValidator(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",Ruleset = "RuleSetA")]
        public string Email { get; set; }
    }
}
