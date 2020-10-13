namespace YemekSepetiUygulamasi.Entity.EntityTable
{
    using Core.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Promosyon")]
    public partial class Promosyon : IEntity
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string PromosyonName { get; set; }

        [StringLength(10)]
        public string PromosyonQuantity { get; set; }

        public DateTime? PromosyonBaslangicTarih { get; set; }

        public DateTime? PromasyonBitisTarih { get; set; }

        public int? CompaniesId { get; set; }
    }
}
