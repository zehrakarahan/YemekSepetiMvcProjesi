namespace YemekSepetiUygulamasi.Entity.EntityTable
{
    using Core.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ManagerTable")]
    public partial class ManagerTable : IEntity
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Mname { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(50)]
        public string EmailAdresi { get; set; }

        public int? CompaniesId { get; set; }

        public int? AuthorityId { get; set; }
    }
}
