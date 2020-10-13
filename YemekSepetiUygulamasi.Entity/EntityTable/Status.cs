namespace YemekSepetiUygulamasi.Entity.EntityTable
{
    using Core.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Status : IEntity
    {
        public int Id { get; set; }

        [StringLength(150)]
        public string StatusName { get; set; }

        public int? CompaniesId { get; set; }
    }
}
