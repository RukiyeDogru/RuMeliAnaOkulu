namespace anaokulumvc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ogrenci")]
    public partial class ogrenci
    {
        public int ogrenciID { get; set; }

        [StringLength(30)]
        public string ad { get; set; }

        [StringLength(50)]
        public string soyad { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? dogumTar { get; set; }

        [StringLength(5)]
        public string cinsiyet { get; set; }

        public int? sinifID { get; set; }

        public virtual sinif sinif { get; set; }
    }
}
