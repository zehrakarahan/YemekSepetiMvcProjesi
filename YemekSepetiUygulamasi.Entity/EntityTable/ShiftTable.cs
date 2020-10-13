namespace YemekSepetiUygulamasi.Entity.EntityTable
{
    using Core.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ShiftTable")]
    public partial class ShiftTable : IEntity
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string SName { get; set; }
    }
}
