namespace YemekSepetiUygulamasi.Entity.EntityVerileri
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductSales
    {
        public int Id { get; set; }

        public int? TID { get; set; }

        public int? CID { get; set; }

        public int? CTID { get; set; }

        public int? PID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DATE { get; set; }
    }
}
