namespace YemekSepetiUygulamasi.Entity.EntityTable
{
    using Core.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MenuNames : IEntity
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string MenuName { get; set; }

        public int? CompaniesId { get; set; }

        public decimal? Price { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public int? PromosyonId { get; set; }

        [Column(TypeName = "image")]
        public byte[] MenuResim { get; set; }

        public int? SiparisSayisi { get; set; }

        public int? Starts { get; set; }
    }
}
