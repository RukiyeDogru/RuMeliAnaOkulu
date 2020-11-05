namespace anaokulumvc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tecrube")]
    public partial class Tecrube
    {
        public int tecrubeID { get; set; }

        [StringLength(50)]
        public string ad { get; set; }

        [StringLength(50)]
        public string yil { get; set; }

        [StringLength(500)]
        public string aciklama { get; set; }

        public int? kullaniciID { get; set; }

        public virtual kullanici kullanici { get; set; }
    }
}
