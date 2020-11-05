using anaokulumvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace anaokulumvc.Controllers
{
    public class EtkinlikController : Controller
    {

        AnaokuluContext db = new AnaokuluContext();
        // GET: Etkinlik
        public ActionResult Index()
        {
            List<etkinlik> etkinlik = db.etkinlik.ToList(); 
            return View(etkinlik);
        }
        [HttpGet]
        public ActionResult EtkinlikEkle()
        {


            var yetkiler = db.yetki.ToList();
            ViewBag.yetkiler = new SelectList(yetkiler, "yetkiID", "adi");

            return View(); //db.kullanici.ToList()
        }

        [HttpPost]
        public ActionResult EtkinlikEkle(etkinlik etkinlik)

        {
            if (ModelState.IsValid == false) // validation hatası varsa demek
            {
                var yetkiler = db.yetki.ToList();
                ViewBag.yetkiler = new SelectList(yetkiler, "yetkiID", "adi");
                return View();
            }
          

            db.etkinlik.Add(etkinlik);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EtkinlikSil(int id)
        {
            etkinlik etkinlik = db.etkinlik.Where(x => x.etkinlikID == id).SingleOrDefault();
            db.etkinlik.Remove(etkinlik);
            db.SaveChanges();
            return RedirectToAction("Index");

        }


        [HttpGet]
        public ActionResult EtkinlikDuzenle(int id)
        {
            etkinlik etkinlik = db.etkinlik.Where(x => x.etkinlikID == id).SingleOrDefault();
            var yetkiler = db.yetki.ToList();
            ViewBag.yetkiler = new SelectList(yetkiler, "yetkiID", "adi", etkinlik.etkinlikID);
            TempData["etkinlikID"] = id; //controllerın icine yukledi
            return View(etkinlik);
        }

        [HttpPost]
        public ActionResult EtkinlikDuzenle(etkinlik etkinlik)

        {
            int etkinlikID = (int)TempData["etkinlikID"];
            etkinlik kul = db.etkinlik.Where(x => x.etkinlikID ==etkinlikID ).SingleOrDefault();


            kul.ad = etkinlik.ad;
            kul.aciklama = etkinlik.aciklama;

            kul.etkinlikID = etkinlik.etkinlikID;
            db.SaveChanges();



            return RedirectToAction("Index");
        }
    }
}