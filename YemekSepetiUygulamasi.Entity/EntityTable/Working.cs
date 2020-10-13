namespace YemekSepetiUygulamasi.Entity.EntityTable
{
    using Core.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Working")]
    public partial class Working : IEntity
    {
        public int Id { get; set; }

        public int? PId { get; set; }

        public DateTime? checkindate { get; set; }

        public DateTime? checkoutdate { get; set; }

        public DateTime? date { get; set; }

        public decimal? totalworking { get; set; }

        public double? shift { get; set; }
    }
}
