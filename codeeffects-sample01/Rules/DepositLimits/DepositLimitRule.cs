
using codeeffects_sample01.Models;
using codeeffects_sample01.Models.CreditCard;
using CodeEffects.Rule.Attributes;

// Dynamic Menu Data Sources
// http://www.codeeffects.com/Doc/Business-Rules-Dynamic-Menu-Data-Sources
// External Method Attribute

// http://codeeffects.com/Doc/Business-Rule-External-Method-Attribute
namespace codeeffects_sample01.Rules.DepositLimits
{
    [Source(DeclaredMembersOnly = true)] // Prevents loading members from another partial class whichis
    [ExternalAction(typeof(DepositLimitHelper), "Validate")]
    [ExternalMethod(typeof(DepositLimitHelper), "IsCreditCardAmountValid")]
    public class DepositLimitRule
    {
        public CreditCard CreditCard { get; set; }

        [ExcludeFromEvaluation]
        public Result EvaluationResult;
       
        // Use of in-rule method.
        [Method("-- Card Amount is More than 2000 --")]
        public bool CardAmountHigherThan2000() { return CreditCard.Amount > 2000; }
    }

}