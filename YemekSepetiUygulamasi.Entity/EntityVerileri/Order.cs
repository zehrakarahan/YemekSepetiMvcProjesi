namespace YemekSepetiUygulamasi.Entity.EntityVerileri
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public int Id { get; set; }

        public int? TicketID { get; set; }

        public DateTime? CreateDateTime { get; set; }

        public int? CategoryTypeId { get; set; }

        public int? CustomerId { get; set; }

        public int? CustomerAddressId { get; set; }

        public int? StatusId { get; set; }

        public int? CompaniesId { get; set; }
    }
}
