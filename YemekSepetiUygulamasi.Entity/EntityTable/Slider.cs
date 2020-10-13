namespace YemekSepetiUygulamasi.Entity.EntityTable
{
    using Core.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slider")]
    public partial class Slider : IEntity
    {
        public int Id { get; set; }

        [Column(TypeName = "image")]
        public byte[] SliderFoto { get; set; }
    }
}
