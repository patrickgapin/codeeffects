using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using codeeffects_sample01.Rules.DepositLimits;
using CodeEffects.Rule.Models;

namespace codeeffects_sample01.Controllers
{
    public class PostController : Controller
    {
        public PostController()
        {
            //LoadMenuRules();
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Rule = RuleModel.Create(typeof(DepositLimitRule));
            return View();
        }

        [HttpPost]
        public ActionResult Save(RuleModel ruleEditor)
        {
            ruleEditor.BindSource(typeof(DepositLimitRule));

            ViewBag.Rule = ruleEditor;

            if (ruleEditor.IsEmpty() || !ruleEditor.IsValid()) { return View("Index");}

            var xml = ruleEditor.GetRuleXml();

            // Save to DB
            return View("Index");
        }
    }
}