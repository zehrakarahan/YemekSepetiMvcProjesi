namespace YemekSepetiUygulamasi.Entity.EntityTable
{
    using Core.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CampaignPName")]
    public partial class CampaignPName : IEntity
    {
        public int Id { get; set; }

        [StringLength(150)]
        public string CampaignName { get; set; }

        [StringLength(250)]
        public string CampaignContents { get; set; }

        [Column(TypeName = "image")]
        public byte[] CampaignResim { get; set; }

        public decimal? Price { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public int? CompaniesId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartingDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        public int? PromosyonId { get; set; }

        public int? SiparisSayisi { get; set; }

        public int? Stars { get; set; }
    }
}
