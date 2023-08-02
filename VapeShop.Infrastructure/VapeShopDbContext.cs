using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Entity.Client;
using VapeShop.Domain.Entity.Functions;
using VapeShop.Domain.Entity.Product;

namespace VapeShop.Infrastructure
{
	public class VapeShopDbContext : DbContext
	{
		public VapeShopDbContext(DbContextOptions<VapeShopDbContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

		public DbSet<City> Cities { get; set; }
		public DbSet<CommunicationMethod> CommunicationMethods { get; set; }
		public DbSet<DeliveryAddress> DeliveryAddresses { get; set; }
		public DbSet<Region> Regions { get; set; }
		public DbSet<User> Users { get; set; }

		public DbSet<Cart> Carts { get; set; }
		public DbSet<ComparisonList> ComparisonLists { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderProduct> OrderProducts { get; set; }
		public DbSet<WishList> WishLists { get; set; }

		public DbSet<Flavor> Flavors { get; set; }
		public DbSet<Liquid> Liquids { get; set; }
		public DbSet<Liquid_param> Liquid_Params { get; set; }
		public DbSet<Nicotine> Nicotines { get; set; }
		public DbSet<PG_VG> PG_VGs { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			#region User

			modelBuilder.Entity<City>(builder =>
			{
				builder.ToTable("Cities").HasKey(x => x.CityID);
				builder.Property(x => x.CityID).ValueGeneratedOnAdd();
				builder.Property(x => x.CityName).HasMaxLength(20);
			});

			modelBuilder.Entity<CommunicationMethod>(builder =>
			{
				builder.ToTable("CommunicationMethods").HasKey(x => x.CommunicationMethodID);
				builder.Property(x => x.CommunicationMethodID).ValueGeneratedOnAdd();
				builder.Property(x => x.CommunicationMethodName).HasMaxLength(10);
			});

			modelBuilder.Entity<Region>(builder =>
			{
				builder.ToTable("Regions").HasKey(x => x.RegionID);
				builder.Property(x => x.RegionID).ValueGeneratedOnAdd();
				builder.Property(x => x.RegionName).HasMaxLength(20);
			});

			modelBuilder.Entity<DeliveryAddress>(builder =>
			{
				builder.ToTable("DeliveryAddresses").HasKey(x => x.DeliveryAddressID);
				builder.Property(x => x.DeliveryAddressID).ValueGeneratedOnAdd();
				builder.Property(x => x.Surname).HasMaxLength(15);
				builder.Property(x => x.Name).HasMaxLength(10);
				builder.Property(x => x.Patronomyc).HasMaxLength(15);
				builder.Property(x => x.Country).HasMaxLength(15);

				builder.HasOne(x => x.City)
				.WithMany(d => d.DeliveryAddress)
				.HasForeignKey(x => x.CityID)
				.OnDelete(DeleteBehavior.SetNull);


				builder.HasOne(x => x.Region)
				.WithMany(d => d.DeliveryAddress)
				.HasForeignKey(x => x.RegionID)
				.OnDelete(DeleteBehavior.SetNull);
			});

			modelBuilder.Entity<User>(builder =>
			{
				builder.ToTable("Users").HasKey(x => x.UserID);
				builder.Property(x => x.UserID).ValueGeneratedOnAdd();
				builder.Property(x => x.Surname).HasMaxLength(15);
				builder.Property(x => x.Name).HasMaxLength(10);
				builder.Property(x => x.Phone).HasMaxLength(15);
				builder.Property(x => x.Email).HasMaxLength(25);
				builder.Property(x => x.Password).HasMaxLength(128);
				builder.Property(x => x.PasswordConfirme).HasMaxLength(128);

				builder.HasOne(x => x.CommunicationMethod)
				.WithMany(x => x.Users)
				.HasForeignKey(x => x.CommunicationMethodID)
				.OnDelete(DeleteBehavior.SetNull);

				builder.HasOne(x => x.DeliveryAddress)
				.WithMany(x => x.Users)
				.HasForeignKey(x => x.DeliveryAddressID)
				.OnDelete(DeleteBehavior.SetNull);

			});
			#endregion

			#region Func
			modelBuilder.Entity<Cart>(builder =>
			{
				builder.ToTable("Carts").HasKey(x => x.CartID);
				builder.Property(x => x.CartID).ValueGeneratedOnAdd();

				builder.HasOne(x => x.User)
				.WithMany(x => x.CartLists)
				.HasForeignKey(x => x.UserID)
				.OnDelete(DeleteBehavior.Cascade);

				builder.HasOne(x => x.Liquid_param)
				.WithMany(x => x.CartLists)
				.HasForeignKey(x => x.Liquid_paramID)
				.OnDelete(DeleteBehavior.Cascade);

			});

			modelBuilder.Entity<ComparisonList>(builder =>
			{
				builder.ToTable("ComparisonLists").HasKey(x => x.ComparsionListID);
				builder.Property(x => x.ComparsionListID).ValueGeneratedOnAdd();

				builder.HasOne(x => x.User)
				.WithMany(x => x.ComparisonLists)
				.HasForeignKey(x => x.UserID)
				.OnDelete(DeleteBehavior.Cascade);

				builder.HasOne(x => x.Liquid)
				.WithMany(x => x.ComparisonLists)
				.HasForeignKey(x => x.LiquidID)
				.OnDelete(DeleteBehavior.Cascade);

			});

			modelBuilder.Entity<Order>(builder =>
			{
				builder.ToTable("Orders").HasKey(x => x.OrderID);
				builder.Property(x => x.OrderID).ValueGeneratedOnAdd();

				builder.HasOne(x => x.User)
				.WithMany(x => x.Orders)
				.HasForeignKey(x => x.UserID);

			});

			modelBuilder.Entity<OrderProduct>(builder =>
			{
				builder.ToTable("OrderProducts").HasKey(x => x.OrderProductID);
				builder.Property(x => x.OrderProductID).ValueGeneratedOnAdd();

				builder.HasOne(x => x.Order)
				.WithMany(x => x.Products)
				.HasForeignKey(x => x.OrderID)
				.OnDelete(DeleteBehavior.Cascade);

				builder.HasOne(x => x.Liquid_Param)
				.WithMany(x => x.OrderProducts)
				.HasForeignKey(x => x.Liquid_paramID);

			});

			modelBuilder.Entity<WishList>(builder =>
			{
				builder.ToTable("WishLists").HasKey(x => x.WishListID);
				builder.Property(x => x.WishListID).ValueGeneratedOnAdd();

				builder.HasOne(x => x.User)
				.WithMany(x => x.WishLists)
				.HasForeignKey(x => x.UserID)
				.OnDelete(DeleteBehavior.Cascade);

				builder.HasOne(x => x.Liquid_param)
				.WithMany(x => x.WishLists)
				.HasForeignKey(x => x.Liquid_paramID)
				.OnDelete(DeleteBehavior.Cascade);

			});
			#endregion

			#region Product

			modelBuilder.Entity<Flavor>(builder =>
			{
				builder.ToTable("Flavors").HasKey(x => x.FlavorID);
				builder.Property(x => x.FlavorID).ValueGeneratedOnAdd();
				builder.Property(x => x.Flavor_name).HasMaxLength(15);

			});

			modelBuilder.Entity<PG_VG>(builder =>
			{
				builder.ToTable("PG_VGs").HasKey(x => x.PG_VG_ID);
				builder.Property(x => x.PG_VG_ID).ValueGeneratedOnAdd();
				builder.Property(x => x.PG_VG_name).HasMaxLength(5);

			});

			modelBuilder.Entity<Nicotine>(builder =>
			{
				builder.ToTable("Nicotines").HasKey(x => x.NicotineID);
				builder.Property(x => x.NicotineID).ValueGeneratedOnAdd();

			});

			modelBuilder.Entity<Liquid>(builder =>
			{
				builder.ToTable("Liquids").HasKey(x => x.LiquidID);
				builder.Property(x => x.LiquidID).ValueGeneratedOnAdd();
				builder.Property(x => x.Name).HasMaxLength(10);
				builder.Property(x => x.LongName).HasMaxLength(30);

				builder.HasOne(x => x.Flavor)
				.WithMany(x => x.Liquids)
				.HasForeignKey(x => x.FlavorID)
				.OnDelete(DeleteBehavior.SetNull);


			});

			modelBuilder.Entity<Liquid_param>(builder =>
			{
				builder.ToTable("Liquid_Params").HasKey(x => x.LiquidParamID);
				builder.Property(x => x.LiquidParamID).ValueGeneratedOnAdd();

				builder.HasOne(x => x.Liquid)
				.WithMany(x => x.Liquid_Params)
				.HasForeignKey(x => x.LiquidID)
				.OnDelete(DeleteBehavior.SetNull);

				builder.HasOne(x => x.PG_VG)
				.WithMany(x => x.Liquid_Params)
				.HasForeignKey(x => x.PG_VGID)
				.OnDelete(DeleteBehavior.SetNull);

				builder.HasOne(x => x.Nicotine)
				.WithMany(x => x.Liquid_Params)
				.HasForeignKey(x => x.NicotineID)
				.OnDelete(DeleteBehavior.SetNull);


			});



			#endregion
		}
	}
}
