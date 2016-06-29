using System;
using System.Collections.Generic;
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
        [Field(DisplayName = "BIN Number", Description = "Credit Card BIN Number")]
        public int Bin { get; set; }

        public CardType Type { get; set; }

        [Field(DisplayName = "Credit Card Amount", Description = "Amount charged to the credit card")]
        public int Amount { get; set; }

        [ExcludeFromEvaluation]
        public Result EvaluationResult;

        [Method(DisplayName = "Has a valid amount")]
        public bool IsCreditCardAmountValid()
        {
            var cardsLimitsHelper = new Limits();

            var isMinValid = this.Amount >= cardsLimitsHelper.CardLimits[this.Type].Item1;
            var isMaxValid = this.Amount <= cardsLimitsHelper.CardLimits[this.Type].Item2;

            return isMinValid && isMaxValid;
        }

        [Action(DisplayName = "Accept Card")]
        public void Validate()
        {
            this.EvaluationResult = IsCreditCardAmountValid() ? Result.Pass : Result.Fail;
        }
    }


    public class Limits
    {
        public Dictionary<CardType, Tuple<int, int>> CardLimits =
            new Dictionary<CardType, Tuple<int, int>>();

        public Limits()
        {
            CardLimits.Add(CardType.Visa, new Tuple<int, int>(30, 2000));
            CardLimits.Add(CardType.MasterCard, new Tuple<int, int>(100, 5000));
            CardLimits.Add(CardType.Discovery, new Tuple<int, int>(200, 500));
        }
    }

    public enum CardType { Visa, MasterCard, Discovery }
    public enum Result { Pass, Fail, Unknown }

}