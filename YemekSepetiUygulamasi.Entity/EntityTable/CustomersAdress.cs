namespace YemekSepetiUygulamasi.Entity.EntityTable
{
    using Core.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CustomersAdress")]
    public partial class CustomersAdress : IEntity
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string CustomerAdress { get; set; }

        public int? CompaniesId { get; set; }
    }
}
