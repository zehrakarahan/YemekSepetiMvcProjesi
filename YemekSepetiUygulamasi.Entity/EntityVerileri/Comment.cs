namespace YemekSepetiUygulamasi.Entity.EntityVerileri
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comment")]
    public partial class Comment
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Comments { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreateDatetime { get; set; }

        public decimal? Speed { get; set; }

        public decimal? Service { get; set; }

        public decimal? Flavor { get; set; }

        public int? CompaniesId { get; set; }

        public int? CommentNumberStarts { get; set; }

        public bool? CommentConfirm { get; set; }

        public int? ProductId { get; set; }
    }
}
