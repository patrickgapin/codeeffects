using System;
using System.Collections.Generic;
using codeeffects_sample01.Rules.DepositLimits;
using CodeEffects.Rule.Attributes;
using CodeEffects.Rule.Common;

namespace codeeffects_sample01.Models.CreditCard
{
    // Dynamic Menu Data Sources
    // http://www.codeeffects.com/Doc/Business-Rules-Dynamic-Menu-Data-Sources
    // External Method Attribute
    // http://codeeffects.com/Doc/Business-Rule-External-Method-Attribute

    //[ExternalMethod(typeof(CreditCard), "IsAmountValid")]

    public class CreditCard
    {
        [Field(DisplayName = "BIN Number", Description = "Credit Card BIN Number")]
        public int Bin { get; set; }


        [Field(DisplayName = "Credit Card Type", Description = "Credit Card Type: Visa, MasterCard, etc.")]
        public CardType Type { get; set; }

        [Field(DisplayName = "Credit Card Amount", Description = "Amount charged to the credit card")]
        public int Amount { get; set; }

        public Address CardOwnerAddress { get; set; }

        [Field(DisplayName = "Card Expiration Date", ValueInputType = ValueInputType.User, DateTimeFormat = "MMM dd yyyy")]
        public DateTime? ExpirationDate { get; set; }

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

    public enum CardType { Visa, MasterCard, Discovery }
    public enum Result { Pass, Fail, Unknown }

}