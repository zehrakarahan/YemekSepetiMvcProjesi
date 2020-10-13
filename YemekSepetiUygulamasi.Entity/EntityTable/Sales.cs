namespace YemekSepetiUygulamasi.Entity.EntityTable
{
    using Core.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sales : IEntity
    {
        public int Id { get; set; }

        public int? Ticketid { get; set; }

        [StringLength(50)]
        public string QTY { get; set; }

        [StringLength(50)]
        public string productname { get; set; }

        [StringLength(50)]
        public string price { get; set; }

        [StringLength(50)]
        public string GroupCode { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        public DateTime? date { get; set; }

        public int? EmployeeId { get; set; }

        [StringLength(50)]
        public string ServiceType { get; set; }

        public int? customerID { get; set; }

        public bool? editmanager { get; set; }

        public int? itemcount { get; set; }
    }
}
