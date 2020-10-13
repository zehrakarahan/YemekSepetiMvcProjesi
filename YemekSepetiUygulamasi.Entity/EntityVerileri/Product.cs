namespace YemekSepetiUygulamasi.Entity.EntityVerileri
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public int Id { get; set; }

        public int? CategoryId { get; set; }

        public int? CategoryTypeId { get; set; }

        [StringLength(50)]
        public string Pname { get; set; }

        public decimal? Price { get; set; }

        public int? ProductImageId { get; set; }

        [StringLength(250)]
        public string ProductDescription { get; set; }

        public int? Stars { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        [StringLength(250)]
        public string Detail { get; set; }

        public int? CompanyId { get; set; }

        public int? Görüntülenme { get; set; }

        public int? CompaniesId { get; set; }
    }
}
