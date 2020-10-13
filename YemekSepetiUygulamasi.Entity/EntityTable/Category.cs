namespace YemekSepetiUygulamasi.Entity.EntityTable
{
    using Core.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category : IEntity
    {
        public int Id { get; set; }

        [Column(TypeName = "image")]
        public byte[] CResim { get; set; }

        [StringLength(50)]
        public string Cname { get; set; }

        public int? CTopId { get; set; }

        public int? CompaniesId { get; set; }
    }
}
