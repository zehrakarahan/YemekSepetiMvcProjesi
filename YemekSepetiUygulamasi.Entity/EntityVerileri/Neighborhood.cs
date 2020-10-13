namespace YemekSepetiUygulamasi.Entity.EntityVerileri
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Neighborhood")]
    public partial class Neighborhood
    {
        public int Id { get; set; }

        [StringLength(60)]
        public string NeighborhoodName { get; set; }

        public int? CountyId { get; set; }

        public int? CityId { get; set; }

        public int? CountryId { get; set; }
    }
}
