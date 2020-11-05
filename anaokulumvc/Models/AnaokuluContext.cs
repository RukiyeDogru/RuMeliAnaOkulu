namespace anaokulumvc.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AnaokuluContext : DbContext
    {
        public AnaokuluContext()
            : base("name=AnaokuluContext")
        {
        }

        public virtual DbSet<duyuru> duyuru { get; set; }
        public virtual DbSet<Egitim> Egitim { get; set; }
        public virtual DbSet<etkinlik> etkinlik { get; set; }
        public virtual DbSet<kullanici> kullanici { get; set; }
        public virtual DbSet<ogrenci> ogrenci { get; set; }
        public virtual DbSet<ogretmen> ogretmen { get; set; }
        public virtual DbSet<sinif> sinif { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Tecrube> Tecrube { get; set; }
        public virtual DbSet<yetki> yetki { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
