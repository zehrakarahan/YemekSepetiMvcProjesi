namespace YemekSepetiUygulamasi.Entity.EntityTable
{
    using Core.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Neighborhood")]
    public partial class Neighborhood : IEntity
    {
        public int Id { get; set; }

        [StringLength(60)]
        public string NeighborhoodName { get; set; }

        public int? CountyId { get; set; }

        public int? CityId { get; set; }
    }
}
