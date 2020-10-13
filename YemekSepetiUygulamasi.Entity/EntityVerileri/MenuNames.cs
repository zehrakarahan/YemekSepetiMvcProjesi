namespace YemekSepetiUygulamasi.Entity.EntityVerileri
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MenuNames
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string MenuName { get; set; }

        public int? MenusId { get; set; }

        public int? CompaniesId { get; set; }
    }
}
