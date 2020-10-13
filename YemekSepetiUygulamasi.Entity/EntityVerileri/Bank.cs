namespace YemekSepetiUygulamasi.Entity.EntityVerileri
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bank")]
    public partial class Bank
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string BankName { get; set; }

        [Column(TypeName = "image")]
        public byte[] BankLogo { get; set; }

        public int? CompaniesId { get; set; }
    }
}
