namespace YemekSepetiUygulamasi.Entity.EntityVerileri
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employess")]
    public partial class Employess
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string DisplayName { get; set; }

        [StringLength(50)]
        public string Ename { get; set; }

        [StringLength(50)]
        public string EsName { get; set; }

        [StringLength(50)]
        public string EPosition { get; set; }

        public decimal? Esalary { get; set; }

        [StringLength(15)]
        public string phone { get; set; }

        [StringLength(50)]
        public string password { get; set; }

        [StringLength(50)]
        public string level { get; set; }

        [Column("e-mail")]
        [StringLength(50)]
        public string e_mail { get; set; }

        [StringLength(250)]
        public string address { get; set; }

        public int? CompaniesId { get; set; }
    }
}
