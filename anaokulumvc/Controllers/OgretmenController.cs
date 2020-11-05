using anaokulumvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace anaokulumvc.Controllers
{
    public class OgretmenController : Controller
    {
        AnaokuluContext db = new AnaokuluContext();
        // GET: Ogretmen
        public ActionResult Index()
        {

            List<ogretmen> ogretmen = db.ogretmen.ToList();
            return View(ogretmen);
        }

        [HttpGet]
        public ActionResult Sil(int id)
        {
            ogretmen ogrt = db.ogretmen.Where(x => x.ogretmenID == id).SingleOrDefault();
            db.ogretmen.Remove(ogrt);
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
        public ActionResult Ekle(ogretmen ogrt)

        {
            if (ModelState.IsValid == false) // validation hatası varsa demek
            {
                var yetkiler = db.yetki.ToList();
                ViewBag.yetkiler = new SelectList(yetkiler, "yetkiID", "adi");
                return View();
            }

            ogretmen kul = db.ogretmen.Where(x => x.eposta == ogrt.eposta).SingleOrDefault();
            if (kul != null)
            {
                //aynı epostayla kaydolan biri var demek
                ModelState.AddModelError("eposta", "Farklı bir eposta adresi giriniz!");
                var yetkiler = db.yetki.ToList();
                ViewBag.yetkiler = new SelectList(yetkiler, "yetkiID", "adi");
                return View();
            }



            db.ogretmen.Add(ogrt);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Duzenle(int id)
        {
            ogretmen ogrt = db.ogretmen.Where(x => x.ogretmenID == id).SingleOrDefault();
            var yetkiler = db.yetki.ToList();
            ViewBag.yetkiler = new SelectList(yetkiler, "yetkiID", "adi", ogrt.ogretmenID);
            TempData["ogretmenID"] = id; //controllerın icine yukledi
            return View(ogrt);
        }

        [HttpPost]
        public ActionResult Duzenle(ogretmen ogrt)

        {
            int ogretmenID = (int)TempData["ogretmenID"];
            ogretmen kul = db.ogretmen.Where(x => x.ogretmenID == ogretmenID).SingleOrDefault();


            kul.ad = ogrt.ad;
            kul.soyad = ogrt.soyad;
            kul.eposta = ogrt.eposta;
            kul.sifre = ogrt.sifre;
            kul.brans = ogrt.brans;
            kul.ogretmenID = ogrt.ogretmenID;
            db.SaveChanges();



            return RedirectToAction("Index");
        }
    }
}