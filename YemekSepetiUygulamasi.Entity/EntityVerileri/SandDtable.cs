namespace YemekSepetiUygulamasi.Entity.EntityVerileri
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SandDtable")]
    public partial class SandDtable
    {
        public int Id { get; set; }

        public int? DID { get; set; }
    }
}
