using anaokulumvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace anaokulumvc.Controllers
{
    public class SinifController : Controller
    {
        AnaokuluContext db = new AnaokuluContext();
        // GET: Sinif
        public ActionResult Index()
        {
            List<sinif> sinif = db.sinif.ToList();
            return View(sinif);
        }
        public ActionResult Index2()
        {
            List<sinif> sinif = db.sinif.ToList();
            return View(sinif);
        }


    }
}