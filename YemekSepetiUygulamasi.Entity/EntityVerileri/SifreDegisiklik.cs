namespace YemekSepetiUygulamasi.Entity.EntityVerileri
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SifreDegisiklik")]
    public partial class SifreDegisiklik
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string guidimiz { get; set; }

        public DateTime? songuncellenmetarihi { get; set; }

        public bool? active { get; set; }

        public DateTime? gecerliliksuresi { get; set; }

        public DateTime? ilkkayittarihi { get; set; }

        [StringLength(50)]
        public string kullaniciadi { get; set; }

        public int? CompaniesId { get; set; }
    }
}
