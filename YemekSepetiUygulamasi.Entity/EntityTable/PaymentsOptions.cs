namespace YemekSepetiUygulamasi.Entity.EntityTable
{
    using Core.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PaymentsOptions : IEntity
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string PaymentOptions { get; set; }

        public int? CompaniesId { get; set; }
    }
}
