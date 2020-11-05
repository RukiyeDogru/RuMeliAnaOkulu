using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace anaokulumvc
{
    public class ResimIslem
    {
        public string Ekle(HttpPostedFileBase orjResim)
        {
            string uzanti = Path.GetExtension(orjResim.FileName);//uzantıyı kontrol etti
            if(!(uzanti==".png"|| uzanti==".jpg"))
            {
                return "uzanti";
            }

            if(orjResim.ContentLength>10000000)//boyut kontrolu yaptı
            {
                return "boyut";
            }

            string resimAdi = Guid.NewGuid().ToString() + uzanti; //benzersiz resim adı oluşturuyor.
            Bitmap res = new Bitmap(Image.FromStream(orjResim.InputStream));
            res.Save(HttpContext.Current.Server.MapPath("/Content/Resimler/Kullanici/" + resimAdi));//olusturdugum resim adıyla gosterdigim klasore kaydoldu
            return resimAdi;
        }


        public string Sil(string resimAd)
        {
            if (resimAd != "bos.png")
            {
                string yol = HttpContext.Current.Server.MapPath("/Content/Resimler/Kullanici/" + resimAd);
                if(System.IO.File.Exists(yol))    // bu yolda bu dosya var mı?
                {
                    System.IO.File.Delete(yol);
                }

                else
                {
                    return "Dosya Bulunamadı";
                }
                        
             }

            return "";
        }
    }
}
