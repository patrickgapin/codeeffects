using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeEffects.Rule;
using CodeEffects.Rule.Attributes;

namespace codeeffects_sample01.Models
{
    public class Customer
    {
        [Field(DisplayName = "Customer ID", Description = "Unique ID for each customer")]
        public string ID { get; set; }
        [Field(DisplayName = "Customer First Name")]
        public string FirstName { get; set; }
        [Field(DisplayName = "Customer Last Name")]
        public string LastName { get; set; }        
    }
}