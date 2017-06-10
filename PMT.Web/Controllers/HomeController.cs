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
    public class HomeController : BaseController
    {
        ILogger logger;
        ICommonHelper commonHelper;
        ITransactionsEngine transactionsEngine;
        IChartsEngine chartsEngine;

        public const string transactionPreferences = "transactionPreferences";

        public HomeController(ILoggerFactory logger,
                                ICommonHelper commonHelper,
                                ITransactionsEngine transactionsEngine,
                                IChartsEngine chartsEngine) : base(logger, commonHelper)
        {
            this.logger = logger.CreateLogger<HomeController>();
            this.commonHelper = commonHelper;
            this.transactionsEngine = transactionsEngine;
            this.chartsEngine = chartsEngine;

        }
        public ActionResult Index()
        {
            string objPreferences = TempData[transactionPreferences] as string;
            if (string.IsNullOrEmpty(objPreferences))
                objPreferences = commonHelper.GetTransactionsPreferences(HttpContext);

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
            throw new Exception();
            //Response.StatusCode = 404;
            //throw new HttpException(404, "HTTP/1.1 404 Not Found");
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}