namespace anaokulumvc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("duyuru")]
    public partial class duyuru
    {
        public int duyuruID { get; set; }

        [StringLength(100)]
        public string baslik { get; set; }

        [StringLength(4000)]
        public string icerik { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? tarih { get; set; }

        public int? kullaniciID { get; set; }

        public virtual kullanici kullanici { get; set; }
    }
}
