namespace anaokulumvc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ogretmen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ogretmen()
        {
            sinif = new HashSet<sinif>();
        }

        public int ogretmenID { get; set; }

        [StringLength(20)]
        public string ad { get; set; }

        [StringLength(20)]
        public string soyad { get; set; }

        [StringLength(30)]
        public string eposta { get; set; }

        [StringLength(20)]
        public string sifre { get; set; }

        [StringLength(20)]
        public string brans { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? kayitTar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sinif> sinif { get; set; }
    }
}
