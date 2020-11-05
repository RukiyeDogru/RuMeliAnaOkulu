using anaokulumvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace anaokulumvc.Controllers
{
    public class DuyuruController : Controller

    {
        AnaokuluContext db = new AnaokuluContext();
        // GET: Duyuru
        public ActionResult Index()
        {

            List<duyuru> duyuru = db.duyuru.ToList();
            return View(duyuru);
        }
        public ActionResult Index2()
        {

            List<duyuru> duyuru = db.duyuru.ToList();
            return View(duyuru);
        }



        [HttpGet]
        public ActionResult DuyuruEkle()
        {


            var yetkiler = db.yetki.ToList();
            ViewBag.yetkiler = new SelectList(yetkiler, "yetkiID", "adi");

            return View(); //db.kullanici.ToList()
        }

        [HttpPost]
        public ActionResult DuyuruEkle(duyuru duyuru,string baslik,string icerik,DateTime tarih)

        {
            if (ModelState.IsValid == false) // validation hatası varsa demek
            {
                var yetkiler = db.yetki.ToList();
                ViewBag.yetkiler = new SelectList(yetkiler, "yetkiID", "adi");
                return View();
            }
            if (Session["kullanici"] != null)

            {
                kullanici k = (kullanici)Session["kullanici"];
                duyuru duy = (duyuru)Session["duyuru"];
                duyuru.kullaniciID = k.kullaniciID;
                duyuru.baslik = baslik;
                duyuru.icerik = icerik;
                duyuru.tarih = DateTime.Now;
            }

            
            
            db.duyuru.Add(duyuru);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DuyuruDuzenle(int id)
        {
            duyuru duyuru = db.duyuru.Where(x => x.duyuruID == id).SingleOrDefault();
            var yetkiler = db.yetki.ToList();
            ViewBag.yetkiler = new SelectList(yetkiler, "yetkiID", "adi", duyuru.duyuruID);
            TempData["duyuruID"] = id; //controllerın icine yukledi
            return View(duyuru);
        }

        [HttpPost]
        public ActionResult DuyuruDuzenle(duyuru duyuru)

        {
            int duyuruID = (int)TempData["duyuruID"];
            duyuru kul = db.duyuru.Where(x => x.duyuruID == duyuruID).SingleOrDefault();


            kul.baslik = duyuru.baslik;
            kul.icerik = duyuru.icerik;
            
            kul.duyuruID = duyuru.duyuruID;
            db.SaveChanges();



            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DuyuruSil(int id)
        {
            duyuru duyuru = db.duyuru.Where(x => x.duyuruID == id).SingleOrDefault();
            db.duyuru.Remove(duyuru);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}