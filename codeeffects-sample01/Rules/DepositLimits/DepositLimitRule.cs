
using codeeffects_sample01.Models;
using codeeffects_sample01.Models.CreditCard;
using CodeEffects.Rule.Attributes;

namespace codeeffects_sample01.Rules.DepositLimits
{
    // Dynamic Menu Data Sources
    // http://www.codeeffects.com/Doc/Business-Rules-Dynamic-Menu-Data-Sources
    // External Method Attribute
    // http://codeeffects.com/Doc/Business-Rule-External-Method-Attribute

    //[ExternalMethod(typeof(CreditCard), "IsAmountValid")]

    public class DepositLimitRule
    {
        public CreditCard CreditCard { get; set; }

        [ExcludeFromEvaluation]
        public Result EvaluationResult;

        [Method(DisplayName = " -- Has a valid amount --")]
        public bool IsCreditCardAmountValid()
        {
            var cardsLimitsHelper = new Limits();

            var isMinValid = CreditCard.Amount >= cardsLimitsHelper.CardLimits[CreditCard.Type].Item1;
            var isMaxValid = CreditCard.Amount <= cardsLimitsHelper.CardLimits[CreditCard.Type].Item2;

            return isMinValid && isMaxValid;
        }

        [Action(DisplayName = "-- Accept Card --")]
        public void Validate()
        {
            this.EvaluationResult = IsCreditCardAmountValid() ? Result.Pass : Result.Fail;
        }
        

        [Method("-- Card Amount is More than 2000 --")]
        public bool CardAmountHigherThan2000() { return CreditCard.Amount > 2000; }
    }

}