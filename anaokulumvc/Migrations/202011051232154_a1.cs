namespace anaokulumvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.duyuru",
                c => new
                    {
                        duyuruID = c.Int(nullable: false, identity: true),
                        baslik = c.String(maxLength: 100),
                        icerik = c.String(maxLength: 4000),
                        tarih = c.DateTime(storeType: "smalldatetime"),
                        kullaniciID = c.Int(),
                    })
                .PrimaryKey(t => t.duyuruID)
                .ForeignKey("dbo.kullanici", t => t.kullaniciID)
                .Index(t => t.kullaniciID);
            
            CreateTable(
                "dbo.kullanici",
                c => new
                    {
                        kullaniciID = c.Int(nullable: false, identity: true),
                        ad = c.String(maxLength: 20),
                        soyad = c.String(maxLength: 20),
                        eposta = c.String(maxLength: 30),
                        sifre = c.String(maxLength: 20),
                        resim = c.String(maxLength: 50),
                        yetkiID = c.Int(),
                    })
                .PrimaryKey(t => t.kullaniciID)
                .ForeignKey("dbo.yetki", t => t.yetkiID)
                .Index(t => t.yetkiID);
            
            CreateTable(
                "dbo.Egitim",
                c => new
                    {
                        egitimID = c.Int(nullable: false, identity: true),
                        ad = c.String(maxLength: 50),
                        yil = c.String(maxLength: 50),
                        aciklama = c.String(maxLength: 500),
                        kullaniciID = c.Int(),
                    })
                .PrimaryKey(t => t.egitimID)
                .ForeignKey("dbo.kullanici", t => t.kullaniciID)
                .Index(t => t.kullaniciID);
            
            CreateTable(
                "dbo.Tecrube",
                c => new
                    {
                        tecrubeID = c.Int(nullable: false, identity: true),
                        ad = c.String(maxLength: 50),
                        yil = c.String(maxLength: 50),
                        aciklama = c.String(maxLength: 500),
                        kullaniciID = c.Int(),
                    })
                .PrimaryKey(t => t.tecrubeID)
                .ForeignKey("dbo.kullanici", t => t.kullaniciID)
                .Index(t => t.kullaniciID);
            
            CreateTable(
                "dbo.yetki",
                c => new
                    {
                        yetkiID = c.Int(nullable: false, identity: true),
                        adi = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.yetkiID);
            
            CreateTable(
                "dbo.etkinlik",
                c => new
                    {
                        etkinlikID = c.Int(nullable: false, identity: true),
                        ad = c.String(maxLength: 150),
                        aciklama = c.String(maxLength: 500),
                        tarih = c.DateTime(storeType: "smalldatetime"),
                        sinifID = c.Int(),
                    })
                .PrimaryKey(t => t.etkinlikID)
                .ForeignKey("dbo.sinif", t => t.sinifID)
                .Index(t => t.sinifID);
            
            CreateTable(
                "dbo.sinif",
                c => new
                    {
                        sinifID = c.Int(nullable: false, identity: true),
                        ad = c.String(maxLength: 50),
                        ogretmenID = c.Int(),
                    })
                .PrimaryKey(t => t.sinifID)
                .ForeignKey("dbo.ogretmen", t => t.ogretmenID)
                .Index(t => t.ogretmenID);
            
            CreateTable(
                "dbo.ogrenci",
                c => new
                    {
                        ogrenciID = c.Int(nullable: false, identity: true),
                        ad = c.String(maxLength: 30),
                        soyad = c.String(maxLength: 50),
                        dogumTar = c.DateTime(storeType: "smalldatetime"),
                        cinsiyet = c.String(maxLength: 5),
                        sinifID = c.Int(),
                    })
                .PrimaryKey(t => t.ogrenciID)
                .ForeignKey("dbo.sinif", t => t.sinifID)
                .Index(t => t.sinifID);
            
            CreateTable(
                "dbo.ogretmen",
                c => new
                    {
                        ogretmenID = c.Int(nullable: false, identity: true),
                        ad = c.String(maxLength: 20),
                        soyad = c.String(maxLength: 20),
                        eposta = c.String(maxLength: 30),
                        sifre = c.String(maxLength: 20),
                        brans = c.String(maxLength: 20),
                        kayitTar = c.DateTime(storeType: "smalldatetime"),
                    })
                .PrimaryKey(t => t.ogretmenID);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.sinif", "ogretmenID", "dbo.ogretmen");
            DropForeignKey("dbo.ogrenci", "sinifID", "dbo.sinif");
            DropForeignKey("dbo.etkinlik", "sinifID", "dbo.sinif");
            DropForeignKey("dbo.kullanici", "yetkiID", "dbo.yetki");
            DropForeignKey("dbo.Tecrube", "kullaniciID", "dbo.kullanici");
            DropForeignKey("dbo.Egitim", "kullaniciID", "dbo.kullanici");
            DropForeignKey("dbo.duyuru", "kullaniciID", "dbo.kullanici");
            DropIndex("dbo.ogrenci", new[] { "sinifID" });
            DropIndex("dbo.sinif", new[] { "ogretmenID" });
            DropIndex("dbo.etkinlik", new[] { "sinifID" });
            DropIndex("dbo.Tecrube", new[] { "kullaniciID" });
            DropIndex("dbo.Egitim", new[] { "kullaniciID" });
            DropIndex("dbo.kullanici", new[] { "yetkiID" });
            DropIndex("dbo.duyuru", new[] { "kullaniciID" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.ogretmen");
            DropTable("dbo.ogrenci");
            DropTable("dbo.sinif");
            DropTable("dbo.etkinlik");
            DropTable("dbo.yetki");
            DropTable("dbo.Tecrube");
            DropTable("dbo.Egitim");
            DropTable("dbo.kullanici");
            DropTable("dbo.duyuru");
        }
    }
}
