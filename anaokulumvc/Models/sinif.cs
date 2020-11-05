namespace anaokulumvc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sinif")]
    public partial class sinif
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sinif()
        {
            etkinlik = new HashSet<etkinlik>();
            ogrenci = new HashSet<ogrenci>();
        }

        public int sinifID { get; set; }

        [StringLength(50)]
        public string ad { get; set; }

        public int? ogretmenID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<etkinlik> etkinlik { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ogrenci> ogrenci { get; set; }

        public virtual ogretmen ogretmen { get; set; }
    }
}
