namespace anaokulumvc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("etkinlik")]
    public partial class etkinlik
    {
        public int etkinlikID { get; set; }

        [StringLength(150)]
        public string ad { get; set; }

        [StringLength(500)]
        public string aciklama { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? tarih { get; set; }

        public int? sinifID { get; set; }

        public virtual sinif sinif { get; set; }
    }
}
