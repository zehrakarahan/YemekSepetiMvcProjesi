namespace YemekSepetiUygulamasi.Entity.EntityTable
{
    using Core.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderProduct")]
    public partial class OrderProduct : IEntity
    {
        public int Id { get; set; }

        public int? OrderId { get; set; }

        public int? ProductId { get; set; }

        public int? CampaignProductId { get; set; }

        public int? MenuNamesId { get; set; }

        public int? CompaniesId { get; set; }

        public decimal? CampaignProductQuanty { get; set; }

        public decimal? MenusQuanty { get; set; }

        public decimal? ProductQuanty { get; set; }
    }
}
