namespace YemekSepetiUygulamasi.Entity.EntityVerileri
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Categorytype")]
    public partial class Categorytype
    {
        [Key]
        public int CTypeID { get; set; }

        public int? CID { get; set; }

        [StringLength(50)]
        public string CTname { get; set; }

        public decimal? Price { get; set; }

        public int? CompaniesId { get; set; }
    }
}
