namespace YemekSepetiUygulamasi.Entity.EntityVerileri
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DayTable")]
    public partial class DayTable
    {
        public int Id { get; set; }

        public int? DayID { get; set; }

        [Column("Start Hour")]
        [StringLength(10)]
        public string Start_Hour { get; set; }

        [Column("End Hour")]
        [StringLength(10)]
        public string End_Hour { get; set; }

        public int? SID { get; set; }

        public int? CompaniesId { get; set; }
    }
}
