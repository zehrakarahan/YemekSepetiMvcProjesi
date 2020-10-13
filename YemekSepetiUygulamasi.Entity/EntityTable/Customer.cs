namespace YemekSepetiUygulamasi.Entity.EntityTable
{
    using Core.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer : IEntity
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Cname { get; set; }

        [StringLength(50)]
        public string Clastname { get; set; }

        [StringLength(50)]
        public string phone { get; set; }

        public int? CustomersAdressId { get; set; }

        public DateTime? membershipdate { get; set; }

        [Column(TypeName = "image")]
        public byte[] Customerİmage { get; set; }

        [StringLength(50)]
        public string EmailAdress { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(50)]
        public string CountryName { get; set; }

        [StringLength(50)]
        public string CityName { get; set; }

        [StringLength(50)]
        public string CountyName { get; set; }

        [StringLength(50)]
        public string NeighborhoodName { get; set; }

        public int? CompaniesId { get; set; }
    }
}
