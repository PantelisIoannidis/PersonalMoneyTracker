using Microsoft.Extensions.Logging;
using PMT.BusinessLayer;
using PMT.DataLayer;
using PMT.Models;
using PMT.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMT.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        ILogger logger;
        IUserPreferences userPreferences;
        ICommonHelper commonHelper;
        ITransactionsEngine transactionsEngine;
        IChartsEngine chartsEngine;
        public string userId;

        public const string transactionPreferences = "transactionPreferences";

        public HomeController(ILoggerFactory logger,
                                IUserPreferences userPreferences,
                                ICommonHelper commonHelper,
                                ITransactionsEngine transactionsEngine,
                                IChartsEngine chartsEngine)
        {
            this.logger = logger.CreateLogger<HomeController>();
            this.userPreferences = userPreferences;
            this.commonHelper = commonHelper;
            this.transactionsEngine = transactionsEngine;
            this.chartsEngine = chartsEngine;

            userId = commonHelper.GetUserId(HttpContext);
        }
        public ActionResult Index()
        {
            string objPreferences = TempData[transactionPreferences] as string;
            if (string.IsNullOrEmpty(objPreferences))
                objPreferences = userPreferences.GetTransactionPreferences(HttpContext);

            TransactionFilterVM transactionFilterVM = transactionsEngine.GetFilter(userId, objPreferences);
            ViewBag.ChartIncomeVsExpense = chartsEngine.ChartIncomeVsExpense(userId, transactionFilterVM);
            ViewBag.TransactionsSummary = transactionsEngine.PrepareSummary(userId, transactionFilterVM);
            ViewBag.IncomeByCategory = chartsEngine.ChartIncomeExpensesByCategory(userId, transactionFilterVM,Entities.TransactionType.Income);
            ViewBag.ExpenseByCategory = chartsEngine.ChartIncomeExpensesByCategory(userId, transactionFilterVM, Entities.TransactionType.Expense);
            return View(transactionFilterVM);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}