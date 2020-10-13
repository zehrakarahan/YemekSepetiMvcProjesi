namespace YemekSepetiUygulamasi.Entity.EntityVerileri
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CampaignProduct")]
    public partial class CampaignProduct
    {
        public int Id { get; set; }

        [StringLength(150)]
        public string CampaignName { get; set; }

        [StringLength(250)]
        public string CampaignContents { get; set; }

        public decimal? Price { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public int? CompaniesId { get; set; }

        public int? MenusId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartingDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }
    }
}
