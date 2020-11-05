using anaokulumvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace anaokulumvc.Controllers
{
    public class OgrenciController : Controller
    {
        AnaokuluContext db = new AnaokuluContext();

        // GET: Ogrenci
        public ActionResult Index()
        {
                List<ogrenci> ogrenci = db.ogrenci.ToList();
                return View(ogrenci);
        }


        [HttpGet]
        public ActionResult Sil(int id)
        {
            ogrenci ogr = db.ogrenci.Where(x => x.ogrenciID == id).SingleOrDefault();
            db.ogrenci.Remove(ogr);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
       

        [HttpGet]
        public ActionResult Ekle()
        {


            var yetkiler = db.yetki.ToList();
            ViewBag.yetkiler = new SelectList(yetkiler, "yetkiID", "adi");

            return View(); //db.kullanici.ToList()
        }

        [HttpPost]
        public ActionResult Ekle(ogrenci ogr)

        {
            if (ModelState.IsValid == false) // validation hatası varsa demek
            {
                var yetkiler = db.yetki.ToList();
                ViewBag.yetkiler = new SelectList(yetkiler, "yetkiID", "adi");
                return View();
            }

            db.ogrenci.Add(ogr);
            db.SaveChanges();
           
            return RedirectToAction("Index");
          
          
        }

        [HttpGet]
        public ActionResult Duzenle(int id)
        {
            ogrenci ogr = db.ogrenci.Where(x => x.ogrenciID == id).SingleOrDefault();
            var yetkiler = db.yetki.ToList();
            ViewBag.yetkiler = new SelectList(yetkiler, "yetkiID", "adi", ogr.ogrenciID);
            TempData["ogrenciID"] = id; //controllerın icine yukledi
            return View(ogr);
        }

        [HttpPost]
        public ActionResult Duzenle(ogrenci ogr)

        {
            int ogrenciID = (int)TempData["ogrenciID"];
            ogrenci kul = db.ogrenci.Where(x => x.ogrenciID == ogrenciID).SingleOrDefault();


            kul.ad = ogr.ad;
            kul.soyad = ogr.soyad;
            kul.cinsiyet = ogr.cinsiyet;
            kul.dogumTar = ogr.dogumTar;
           kul.ogrenciID = ogr.ogrenciID;
            db.SaveChanges();



            return RedirectToAction("Index");
        }
    }
}