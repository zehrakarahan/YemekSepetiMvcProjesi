namespace YemekSepetiUygulamasi.Entity.EntityVerileri
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class YemkSptContext : DbContext
    {
        public YemkSptContext()
            : base("name=YemkSptContext")
        {
        }

        public virtual DbSet<Bank> Bank { get; set; }
        public virtual DbSet<Basket> Basket { get; set; }
        public virtual DbSet<CampaignProduct> CampaignProduct { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Categorytype> Categorytype { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Companies> Companies { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<County> County { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomersAdress> CustomersAdress { get; set; }
        public virtual DbSet<DayTable> DayTable { get; set; }
        public virtual DbSet<Employess> Employess { get; set; }
        public virtual DbSet<Favorites> Favorites { get; set; }
        public virtual DbSet<ManagerTable> ManagerTable { get; set; }
        public virtual DbSet<MenuNames> MenuNames { get; set; }
        public virtual DbSet<Menus> Menus { get; set; }
        public virtual DbSet<Neighborhood> Neighborhood { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderProduct> OrderProduct { get; set; }
        public virtual DbSet<PaymentsOptions> PaymentsOptions { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductImage> ProductImage { get; set; }
        public virtual DbSet<ProductSales> ProductSales { get; set; }
        public virtual DbSet<Sales> Sales { get; set; }
        public virtual DbSet<SandDtable> SandDtable { get; set; }
        public virtual DbSet<ShiftTable> ShiftTable { get; set; }
        public virtual DbSet<SifreDegisiklik> SifreDegisiklik { get; set; }
        public virtual DbSet<Slider> Slider { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<TicketNumber> TicketNumber { get; set; }
        public virtual DbSet<Working> Working { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bank>()
                .Property(e => e.BankName)
                .IsUnicode(false);

            modelBuilder.Entity<Basket>()
                .Property(e => e.Quantity)
                .HasPrecision(8, 2);

            modelBuilder.Entity<CampaignProduct>()
                .Property(e => e.CampaignName)
                .IsUnicode(false);

            modelBuilder.Entity<CampaignProduct>()
                .Property(e => e.CampaignContents)
                .IsUnicode(false);

            modelBuilder.Entity<CampaignProduct>()
                .Property(e => e.Price)
                .HasPrecision(8, 2);

            modelBuilder.Entity<CampaignProduct>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Categorytype>()
                .Property(e => e.Price)
                .HasPrecision(7, 2);

            modelBuilder.Entity<City>()
                .Property(e => e.CityName)
                .IsUnicode(false);

            modelBuilder.Entity<Comment>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Comment>()
                .Property(e => e.Speed)
                .HasPrecision(7, 2);

            modelBuilder.Entity<Comment>()
                .Property(e => e.Service)
                .HasPrecision(7, 2);

            modelBuilder.Entity<Comment>()
                .Property(e => e.Flavor)
                .HasPrecision(7, 2);

            modelBuilder.Entity<Companies>()
                .Property(e => e.CompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<Companies>()
                .Property(e => e.CompanyAdress)
                .IsUnicode(false);

            modelBuilder.Entity<Companies>()
                .Property(e => e.MinimumPackagePrice)
                .HasPrecision(8, 2);

            modelBuilder.Entity<Companies>()
                .Property(e => e.ServicePoint)
                .HasPrecision(8, 2);

            modelBuilder.Entity<Companies>()
                .Property(e => e.SpeedPoint)
                .HasPrecision(8, 2);

            modelBuilder.Entity<Companies>()
                .Property(e => e.FlavorPoint)
                .HasPrecision(8, 2);

            modelBuilder.Entity<Companies>()
                .Property(e => e.PaymentTypes)
                .IsUnicode(false);

            modelBuilder.Entity<Companies>()
                .Property(e => e.Promasyon)
                .IsUnicode(false);

            modelBuilder.Entity<Companies>()
                .Property(e => e.FirmaInformation)
                .IsUnicode(false);

            modelBuilder.Entity<Companies>()
                .Property(e => e.CompanyDisCountStatus)
                .HasPrecision(8, 2);

            modelBuilder.Entity<Companies>()
                .Property(e => e.CompanyDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.CountryName)
                .IsUnicode(false);

            modelBuilder.Entity<County>()
                .Property(e => e.CountyName)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.EmailAdress)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.CountryName)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.CityName)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.CountyName)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.NeighborhoodName)
                .IsUnicode(false);

            modelBuilder.Entity<CustomersAdress>()
                .Property(e => e.CustomerAdress)
                .IsUnicode(false);

            modelBuilder.Entity<Employess>()
                .Property(e => e.Ename)
                .IsUnicode(false);

            modelBuilder.Entity<Employess>()
                .Property(e => e.EsName)
                .IsUnicode(false);

            modelBuilder.Entity<Employess>()
                .Property(e => e.Esalary)
                .HasPrecision(8, 3);

            modelBuilder.Entity<MenuNames>()
                .Property(e => e.MenuName)
                .IsUnicode(false);

            modelBuilder.Entity<Menus>()
                .Property(e => e.MenuName)
                .IsUnicode(false);

            modelBuilder.Entity<Menus>()
                .Property(e => e.Price)
                .IsUnicode(false);

            modelBuilder.Entity<Menus>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Menus>()
                .Property(e => e.MenuContents)
                .IsUnicode(false);

            modelBuilder.Entity<Neighborhood>()
                .Property(e => e.NeighborhoodName)
                .IsUnicode(false);

            modelBuilder.Entity<OrderProduct>()
                .Property(e => e.Quanty)
                .HasPrecision(8, 2);

            modelBuilder.Entity<PaymentsOptions>()
                .Property(e => e.PaymentOptions)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(8, 2);

            modelBuilder.Entity<Product>()
                .Property(e => e.ProductDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Detail)
                .IsUnicode(false);

            modelBuilder.Entity<SifreDegisiklik>()
                .Property(e => e.guidimiz)
                .IsUnicode(false);

            modelBuilder.Entity<SifreDegisiklik>()
                .Property(e => e.kullaniciadi)
                .IsUnicode(false);

            modelBuilder.Entity<Status>()
                .Property(e => e.StatusName)
                .IsUnicode(false);

            modelBuilder.Entity<TicketNumber>()
                .Property(e => e.TicketName)
                .IsUnicode(false);

            modelBuilder.Entity<Working>()
                .Property(e => e.totalworking)
                .HasPrecision(5, 2);
        }
    }
}
