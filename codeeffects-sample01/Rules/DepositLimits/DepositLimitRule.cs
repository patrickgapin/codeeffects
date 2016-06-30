

using codeeffects_sample01.Models.CreditCard;
using CodeEffects.Rule.Attributes;

// Dynamic Menu Data Sources
// http://www.codeeffects.com/Doc/Business-Rules-Dynamic-Menu-Data-Sources
// External Method Attribute

// http://codeeffects.com/Doc/Business-Rule-External-Method-Attribute
namespace codeeffects_sample01.Rules.DepositLimits
{
    [Source(DeclaredMembersOnly = true)] // Prevents loading members from another partial class in case there is one which is automatically built. 
                                        // Could apply to a EF-generated class.
    [ExternalAction(typeof(DepositLimitHelper), "Validate")]
    [ExternalMethod(typeof(DepositLimitHelper), "IsCreditCardAmountValid")]
    public class DepositLimitRule: IDepositimitRule
    {
        public CreditCard CreditCard { get; set; }
       
        public bool CardAmountHigherThan2000() { return CreditCard.Amount > 2000; }

        public Result EvaluationResult { get; set; }
    }

}