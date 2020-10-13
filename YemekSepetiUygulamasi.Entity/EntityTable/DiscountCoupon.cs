namespace YemekSepetiUygulamasi.Entity.EntityTable
{
    using Core.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DiscountCoupon")]
    public partial class DiscountCoupon : IEntity
    {
        public int Id { get; set; }

        public decimal? DiscountCouponQuantity { get; set; }

        public DateTime? CouponBitisTarihi { get; set; }

        public DateTime? CouponBaslangicTarihi { get; set; }

        public bool? CouponStatus { get; set; }

        [StringLength(60)]
        public string CouponKodu { get; set; }

        public int? CompaniesId { get; set; }
    }
}
