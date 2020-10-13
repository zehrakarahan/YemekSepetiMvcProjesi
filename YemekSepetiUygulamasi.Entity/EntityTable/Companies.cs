namespace YemekSepetiUygulamasi.Entity.EntityTable
{
    using Core.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Companies : IEntity
    {
        public int Id { get; set; }

        [StringLength(150)]
        public string CompanyName { get; set; }

        [StringLength(250)]
        public string CompanyAdress { get; set; }

        public int? CompanyServiceTime { get; set; }

        public int? CityId { get; set; }

        public int? CountyId { get; set; }

        public decimal? MinimumPackagePrice { get; set; }

        public decimal? ServicePoint { get; set; }

        public decimal? SpeedPoint { get; set; }

        public decimal? FlavorPoint { get; set; }

        public TimeSpan? OpeningTime { get; set; }

        public TimeSpan? ClosingTime { get; set; }

        [StringLength(100)]
        public string PaymentOptionsId { get; set; }

        [Column(TypeName = "image")]
        public byte[] CompanyLogo { get; set; }

        [StringLength(500)]
        public string Promasyon { get; set; }

        [StringLength(500)]
        public string FirmaInformation { get; set; }

        public decimal? CompanyDisCountStatus { get; set; }

        public int? NeighborhoodId { get; set; }

        [StringLength(250)]
        public string CompanyDescription { get; set; }
    }
}
