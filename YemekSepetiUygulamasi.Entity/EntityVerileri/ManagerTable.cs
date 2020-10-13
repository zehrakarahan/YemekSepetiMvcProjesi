namespace YemekSepetiUygulamasi.Entity.EntityVerileri
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ManagerTable")]
    public partial class ManagerTable
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Mname { get; set; }

        [StringLength(50)]
        public string Password { get; set; }
    }
}
