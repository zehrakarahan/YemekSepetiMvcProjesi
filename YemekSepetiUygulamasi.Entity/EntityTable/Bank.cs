namespace YemekSepetiUygulamasi.Entity.EntityTable
{
    using Core.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bank")]
    public partial class Bank : IEntity
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string BankName { get; set; }

        [Column(TypeName = "image")]
        public byte[] BankLogo { get; set; }

        public int? CompaniesId { get; set; }
    }
}
