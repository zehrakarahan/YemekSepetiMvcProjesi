namespace YemekSepetiUygulamasi.Entity.EntityTable
{
    using Core.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("County")]
    public partial class County : IEntity
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string CountyName { get; set; }

        public int? CityId { get; set; }

        public int? CountryId { get; set; }
    }
}
