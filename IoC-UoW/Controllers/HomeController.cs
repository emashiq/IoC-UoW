using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IoC_UoW.Models;
using Repository.UnitOfWork;
using SendPro;
using UnitOfWork;

namespace IoC_UoW.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISendProService<IEmailConfiguration, IEmail> _sendProService;
        private IEmail _email;

        public HomeController()
        {
            _unitOfWork = new UnitOfWorkInjector().GetUnitOfWorkInstance();
            _sendProService = new EmailService<IEmailConfiguration, IEmail>(new EmailConfiguration());
            _email = new Email();
        }
        public ActionResult Index()
        {
            _sendProService.Send(_email);
            return View();
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