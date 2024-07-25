using EntitityLayer.Entities;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer.Concrete
{
    public class EmlakJetContext:DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=carbookdb.cpmew0w400zv.eu-north-1.rds.amazonaws.com,1433;Database=RentASeat;User Id=admin;Password=330457Fg;TrustServerCertificate=true;");
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=EmlakJet;Integrated Security=True;Encrypt=False");
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=neptune.odeaweb.com;Database=RentASeatDb;User Id=RentASeatDb;Password=330457Fg!;TrustServerCertificate=true;");
        //}

        public DbSet<About> Abouts { get; set; }
        public DbSet<AgentInfo> AgentInfos { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Category> Categories { get; set; }
        
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactAddress> ContactAddresses { get; set; }
        
        public DbSet<FooterAddress> FooterAddresses { get; set; }
        public DbSet<FooterImageGallery> FooterImageGalleries { get; set; }
        public DbSet<PropertyAgent> PropertyAgents { get; set; }
        public DbSet<RealEstate> RealEstates { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<AppUser>AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FooterImageGallery>()
                .HasKey(g => g.GalleryID); // GalleryID'yi birincil anahtar olarak tanımlayın

            base.OnModelCreating(modelBuilder);
        }








    }
}
