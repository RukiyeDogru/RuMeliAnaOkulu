using anaokulumvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace anaokulumvc.Controllers
{
    public class RMController : Controller
    {
        AnaokuluContext db = new AnaokuluContext();
        // GET: RM
        public ActionResult Index()
        {
           
            List<duyuru> duyuru = db.duyuru.ToList();
            return View(duyuru);
           
        }
        public ActionResult Sinif()
        {

            List<sinif> sinif = db.sinif.ToList();
            return View(sinif);
        }
        public ActionResult Etkinliklerimiz()
        {

            List<etkinlik> etkinlik = db.etkinlik.ToList();
            return View(etkinlik);
        }
        public ActionResult Duyurular()
        {

            List<duyuru> duyuru = db.duyuru.ToList();
            return View(duyuru);

        }
        public ActionResult Hakkimizda()
        {
           return View();
        }
       
    }
}