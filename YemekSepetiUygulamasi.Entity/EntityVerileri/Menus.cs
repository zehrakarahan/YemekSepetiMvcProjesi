namespace YemekSepetiUygulamasi.Entity.EntityVerileri
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Menus
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string MenuName { get; set; }

        [StringLength(250)]
        public string Price { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [StringLength(250)]
        public string MenuContents { get; set; }

        public int? CompaniesId { get; set; }
    }
}
