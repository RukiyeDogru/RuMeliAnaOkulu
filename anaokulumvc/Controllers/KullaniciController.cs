using anaokulumvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace anaokulumvc.Controllers
{
    public class KullaniciController : Controller
    {
        AnaokuluContext db = new AnaokuluContext();
        // GET: Kullanici
        public ActionResult Index()
        {

            List<kullanici> kullanicilar = db.kullanici.ToList();
            return View(kullanicilar); //db.kullanici.ToList() 

        }
        [HttpGet]
        public ActionResult Ekle()
        {


            var yetkiler = db.yetki.ToList();
            ViewBag.yetkiler = new SelectList(yetkiler, "yetkiID", "adi");

            return View(); //db.kullanici.ToList()
        }

        [HttpPost]
        public ActionResult Ekle(kullanici k, HttpPostedFileBase resimGelen)

        {
            if (ModelState.IsValid == false) // validation hatası varsa demek
            {
                var yetkiler = db.yetki.ToList();
                ViewBag.yetkiler = new SelectList(yetkiler, "yetkiID", "adi");
                return View();
            }

            kullanici kul = db.kullanici.Where(x => x.eposta == k.eposta).SingleOrDefault();
            if(kul !=null)
            {
                //aynı epostayla kaydolan biri var demek
                ModelState.AddModelError("eposta", "Farklı bir eposta adresi giriniz!");
                var yetkiler = db.yetki.ToList();
                ViewBag.yetkiler = new SelectList(yetkiler, "yetkiID", "adi");
                return View();
            }

            ResimIslem r = new ResimIslem();
            string deger = r.Ekle(resimGelen);

            if (deger == "uzanti")
            {
                var yetkiler = db.yetki.ToList();
                ViewBag.yetkiler = new SelectList(yetkiler, "yetkiID", "adi");
                ViewBag.Hata = "Lütfen .png veya .jpg uzantılı resim seçiniz!";
                return View();
            }

            else if (deger == "boyut")
            {
                var yetkiler = db.yetki.ToList();
                ViewBag.yetkiler = new SelectList(yetkiler, "yetkiID", "adi");
                ViewBag.Hata = "Lütfen daha küçük boyutlu bir resim seçiniz!";
                return View();
            }

            else
            {
                k.resim = deger;
            }

            db.kullanici.Add(k);
            db.SaveChanges();
            ViewBag.Uyari = "Kullanıcı eklendi";
            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult Sil(int id)
        {
            ViewBag.Uyari = "Silme işlemi gerçekleşti";
            kullanici k = db.kullanici.Where(x => x.kullaniciID == id).SingleOrDefault();
            ResimIslem r = new ResimIslem();
            r.Sil(k.resim);

            db.kullanici.Remove(k);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Duzenle(int id)
        {
            kullanici k = db.kullanici.Where(x => x.kullaniciID == id).SingleOrDefault();
            var yetkiler = db.yetki.ToList();
            ViewBag.yetkiler = new SelectList(yetkiler, "yetkiID", "adi", k.yetkiID);
            TempData["kullaniciID"] = id; //controllerın icine yukledi
            return View(k);
        }

        [HttpPost]
        public ActionResult Duzenle(kullanici k, HttpPostedFileBase resimGelen)

        {
            int kullaniciID = (int)TempData["kullaniciID"];
            kullanici kul = db.kullanici.Where(x => x.kullaniciID == kullaniciID).SingleOrDefault();


            //boş gelmesi durumu
            if (resimGelen != null)
            {

                ResimIslem r = new ResimIslem();
                string deger = r.Ekle(resimGelen);


                if (deger == "uzanti")
                {
                    var yetkiler = db.yetki.ToList();
                    ViewBag.yetkiler = new SelectList(yetkiler, "yetkiID", "adi", kul.yetkiID);
                    ViewBag.Hata = "Lütfen .png veya .jpg uzantılı resim seçiniz!";
                    return View(kul);
                }

                else if (deger == "boyut")
                {
                    var yetkiler = db.yetki.ToList();
                    ViewBag.yetkiler = new SelectList(yetkiler, "yetkiID", "adi", kul.yetkiID);
                    ViewBag.Hata = "Lütfen daha küçük boyutlu bir resim seçiniz!";
                    return View(kul);
                }

                else
                {
                    k.resim = deger;
                }


            }

            if (k.resim != null)
            {
                //yeni resim sorunsuz eklendiyse
                if (kul.resim != "bos.png")
                {
                    //eski resmi silmek icin 
                    new ResimIslem().Sil(kul.resim);

                }
                //yeni resmi atamak icin
                kul.resim = k.resim;
            }




            kul.ad = k.ad;
            kul.soyad = k.soyad;
            kul.eposta = k.eposta;
            kul.sifre = k.sifre;
            kul.yetkiID = k.yetkiID;
            db.SaveChanges();



            return RedirectToAction("Index");
        }


        public ActionResult Detay(int id)
        {
            kullanici k = db.kullanici.Where(x => x.kullaniciID == id).SingleOrDefault();
            
            return View(k);
        }

        public ActionResult EgitimSil(int id)
        {
            Egitim e = db.Egitim.Where(x => x.egitimID == id).SingleOrDefault();
            int kullaniciID = (int)e.kullaniciID;
            db.Egitim.Remove(e);
            db.SaveChanges();
            return Redirect("/Kullanici/Detay/" + kullaniciID);

        }

        public ActionResult TecrubeSil(int id)
        {
            Tecrube t = db.Tecrube.Where(x => x.tecrubeID == id).SingleOrDefault();
            int kullaniciID = (int)t.kullaniciID;
            db.Tecrube.Remove(t);
            db.SaveChanges();
            return Redirect("/Kullanici/Detay/" + kullaniciID);

        }

        [HttpGet]
        public ActionResult EgitimEkle(int id)
        {
            Session["KullaniciIdEkle"] = id;

            var yetkiler = db.yetki.ToList();
            ViewBag.yetkiler = new SelectList(yetkiler, "yetkiID", "adi");

            return View(); //db.kullanici.ToList()
        }

        [HttpPost]
        public ActionResult EgitimEkle(Egitim e)

        {
            if (ModelState.IsValid == false) // validation hatası varsa demek
            {
                var yetkiler = db.yetki.ToList();
                ViewBag.yetkiler = new SelectList(yetkiler, "yetkiID", "adi");
                return View();
            }
            e.kullaniciID = int.Parse(Session["KullaniciIdEkle"].ToString());
            db.Egitim.Add(e);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //public ActionResult Test(int id)
        //{
        //    kullanici k = db.kullanici.Where(x => x.kullaniciID == id).SingleOrDefault();
        //    return View(k);

        //}
        [HttpGet]
        public ActionResult EgitimDuzenle(int id)
        {
            Egitim egitim = db.Egitim.Where(x => x.egitimID == id).SingleOrDefault();
            var yetkiler = db.yetki.ToList();
            ViewBag.yetkiler = new SelectList(yetkiler, "yetkiID", "adi", egitim.egitimID);
            TempData["egitimID"] = id; //controllerın icine yukledi
            return View(egitim);
        }

        [HttpPost]
        public ActionResult EgitimDuzenle(Egitim egitim)

        {
            int egitimID = (int)TempData["egitimID"];
            Egitim kul = db.Egitim.Where(x => x.egitimID == egitimID).SingleOrDefault();

            kul.ad = egitim.ad;
            kul.yil = egitim.yil;
            kul.aciklama = egitim.aciklama;
           
            db.SaveChanges();



            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult TecrubeEkle(int id)
        {
            Session["KullaniciIdEkle"] = id;

            var yetkiler = db.yetki.ToList();
            ViewBag.yetkiler = new SelectList(yetkiler, "yetkiID", "adi");

            return View(); //db.kullanici.ToList()
        }

        [HttpPost]
        public ActionResult TecrubeEkle(Tecrube tecrube)

        {
            if (ModelState.IsValid == false) // validation hatası varsa demek
            {
                var yetkiler = db.yetki.ToList();
                ViewBag.yetkiler = new SelectList(yetkiler, "yetkiID", "adi");
                return View();
            }
            tecrube.kullaniciID = int.Parse(Session["KullaniciIdEkle"].ToString());
            db.Tecrube.Add(tecrube);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult TecrubeDuzenle(int id)
        {
            Tecrube tecrube = db.Tecrube.Where(x => x.tecrubeID == id).SingleOrDefault();
            var yetkiler = db.yetki.ToList();
            ViewBag.yetkiler = new SelectList(yetkiler, "yetkiID", "adi", tecrube.tecrubeID);
            TempData["tecrubeID"] = id; //controllerın icine yukledi
            return View(tecrube);
        }

        [HttpPost]
        public ActionResult TecrubeDuzenle(Tecrube tecrube)

        {
            int tecrubeID = (int)TempData["tecrubeID"];
            Tecrube kul = db.Tecrube.Where(x => x.tecrubeID == tecrubeID).SingleOrDefault();

            kul.ad = tecrube.ad;
            kul.yil = tecrube.yil;
            kul.aciklama = tecrube.aciklama;

            db.SaveChanges();



            return RedirectToAction("Index");
        }
    }

 }
