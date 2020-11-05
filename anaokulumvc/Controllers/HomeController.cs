using anaokulumvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace anaokulumvc.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            AnaokuluContext db = new AnaokuluContext();
            List<kullanici> kullanicilar = db.kullanici.ToList();
             return View(db.kullanici.ToList());

        }
    }
}