namespace YemekSepetiUygulamasi.Entity.EntityTable
{
    using Core.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Menus : IEntity
    {
        public int Id { get; set; }

        public int? ProductId { get; set; }

        public int? CompaniesId { get; set; }

        public int? MenuNamesId { get; set; }
    }
}
