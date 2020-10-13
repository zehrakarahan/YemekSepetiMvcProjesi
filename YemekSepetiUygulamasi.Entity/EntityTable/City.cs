namespace YemekSepetiUygulamasi.Entity.EntityTable
{
    using Core.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("City")]
    public partial class City : IEntity
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string CityName { get; set; }

        public int? CountryId { get; set; }

        public int? CompaniesId { get; set; }
    }
}
