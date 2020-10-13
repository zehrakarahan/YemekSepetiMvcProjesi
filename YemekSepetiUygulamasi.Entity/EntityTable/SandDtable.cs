namespace YemekSepetiUygulamasi.Entity.EntityTable
{
    using Core.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SandDtable")]
    public partial class SandDtable : IEntity
    {
        public int Id { get; set; }

        public int? DID { get; set; }
    }
}
