using anaokulumvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace anaokulumvc.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            //string eposta = "rukiye@gmail.com";
            //string sifre = "111111";
            //AnaokuluContext db = new AnaokuluContext();
            //kullanici kkk = db.kullanici.Where(x => x.eposta == eposta && x.sifre == sifre).SingleOrDefault();

            //Session["kullanici"] = kkk;
            //return RedirectToAction("Index", "Home");

            return View(); //en son yukarıyı silip bu satırı çalıştıracaksın
        }
        [HttpPost]
        public ActionResult Index(string eposta, string sifre)
            
        {
            AnaokuluContext db = new AnaokuluContext();
            kullanici kkk = db.kullanici.Where(x => x.eposta == eposta && x.sifre == sifre).SingleOrDefault();
            if(kkk == null)
            {
               //bulunamadı
               ViewBag.Uyari = "Kullanıcı Bulunamadı:(";
            }
            else
            {   
                //bulundu

                // ViewBag.Uyari = "Kullanici Bulundu:)";
                Session["kullanici"] = kkk;
                return RedirectToAction("Index", "Home");
            }
            return View();

        }
        //[HttpPost]
        
            public ActionResult Cikis()
        {
            Session.Abandon();
            return RedirectToAction("Index","RM");
        }

        [HttpGet]
        public ActionResult SifremiUnuttum()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SifremiUnuttum(string eposta)
        {

            AnaokuluContext db = new AnaokuluContext();
            kullanici kullanici = db.kullanici.Where(x => x.eposta == eposta).SingleOrDefault();
            if(kullanici != null)
            {
                //kullanici.sifre = new Random().Next(100000, 99999).ToString();
                //db.SaveChanges();
                string konu = "Şifre Hatırlatma";
                string mesaj = "Yeni Şifreniz:" + kullanici.sifre;
                Eposta.Gonder(kullanici.eposta, konu, mesaj);
                ViewBag.Uyari = "Epostanıza şifreniz gönderilmiştir..";
            }
            else
            {
                ViewBag.Uyari = "Böyle Bir Eposta Adresi Bulunamadı..";
            }

            return View();
        }
    }
}