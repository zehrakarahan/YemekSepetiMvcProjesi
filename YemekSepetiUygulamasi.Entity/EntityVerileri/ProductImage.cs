namespace YemekSepetiUygulamasi.Entity.EntityVerileri
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductImage")]
    public partial class ProductImage
    {
        public int Id { get; set; }

        [Column(TypeName = "image")]
        public byte[] Resimler { get; set; }

        public int? ProductId { get; set; }

        public int? CompaniesId { get; set; }
    }
}
