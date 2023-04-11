using Microsoft.EntityFrameworkCore;
using MoS.DatabaseDefinition.Models;
using System.Threading.Tasks;
using static MoS.Models.Constants.Enums.BookConditions;
using static MoS.Models.Constants.Enums.BookImageTypes;
using static MoS.Models.Constants.Enums.OrderStatus;
using static MoS.Models.Constants.Enums.Role;
using static MoS.Models.Constants.Enums.Exception;
using static MoS.Models.Constants.Enums.PaymentOption;

namespace MoS.DatabaseDefinition.Contexts
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role { Id = (int) RoleIDs.Admin, Name = "Admin" });
            modelBuilder.Entity<Role>().HasData(new Role { Id = (int) RoleIDs.User, Name = "User" });
            modelBuilder.Entity<BookImageType>().HasData(new BookImageType { Id = (int) BookImageTypeTDs.Main, Name = "Main" });
            modelBuilder.Entity<BookImageType>().HasData(new BookImageType { Id = (int)BookImageTypeTDs.Sub, Name = "Sub" });
            modelBuilder.Entity<BookCondition>().HasData(new BookCondition { Id = (int)BookConditionIDs.Fine, Name = "Fine" });
            modelBuilder.Entity<OrderStatus>().HasData(new OrderStatus { Id = (int)OrderStatusIDs.PREPARING, Name = "Preparing" });
            modelBuilder.Entity<OrderStatus>().HasData(new OrderStatus { Id = (int)OrderStatusIDs.PREPARED, Name = "Prepared" });
            modelBuilder.Entity<OrderStatus>().HasData(new OrderStatus { Id = (int)OrderStatusIDs.DELIVERING, Name = "Delivering" });
            modelBuilder.Entity<OrderStatus>().HasData(new OrderStatus { Id = (int)OrderStatusIDs.DELIVERED, Name = "Delivered" });
            modelBuilder.Entity<Exception>().HasData(new Exception
            {
                Id = UnknownExceptionMessages["UNKNOWN"],
                ExceptionMessageType = "UNKNOWN",
                Description = "Unknown Error"
            });

            modelBuilder.Entity<Exception>().HasData(new Exception {
                Id = SignUpExceptionMessages["USER_FOUND"],
                ExceptionMessageType = "USER_FOUND",
                Description = "This user already exists"
            });

            modelBuilder.Entity<Exception>().HasData(new Exception
            {
                Id = SignInExceptionMessages["USER_NAME_NOT_FOUND"],
                ExceptionMessageType = "USER_NAME_NOT_FOUND",
                Description = "This user already exists"
            });

            modelBuilder.Entity<Exception>().HasData(new Exception
            {
                Id = SignInExceptionMessages["WRONG_PASSWORD"],
                ExceptionMessageType = "WRONG_PASSWORD",
                Description = "Your password is wrong"
            });

            modelBuilder.Entity<Exception>().HasData(new Exception
            {
                Id = AuthenticationExceptionMessages["UNAUTHORIZED"],
                ExceptionMessageType = "UNAUTHORIZED",
                Description = "You are unauthorized now"
            });

            modelBuilder.Entity<Exception>().HasData(new Exception
            {
                Id = CreateBookExceptionMessages["INVALID_AUTHOR"],
                ExceptionMessageType = "INVALID_AUTHOR",
                Description = "This author does not exist"
            });

            modelBuilder.Entity<Exception>().HasData(new Exception
            {
                Id = CreateBookExceptionMessages["INVALID_PUBLISHER"],
                ExceptionMessageType = "INVALID_PUBLISHER",
                Description = "This publisher does not exist"
            });

            modelBuilder.Entity<Exception>().HasData(new Exception
            {
                Id = CreateBookExceptionMessages["INVALID_IMAGES"],
                ExceptionMessageType = "INVALID_IMAGES",
                Description = "This book must have at least 1 main image"
            });

            modelBuilder.Entity<Exception>().HasData(new Exception
            {
                Id = CreateBookExceptionMessages["INVALID_CONDITIONS"],
                ExceptionMessageType = "INVALID_CONDITIONS",
                Description = "This condition does not exist"
            });

            modelBuilder.Entity<Exception>().HasData(new Exception
            {
                Id = ChangeQuantityExceptionMessages["QUANTITY_EXCEED"],
                ExceptionMessageType = "QUANTITY_EXCEED",
                Description = "The selected quantity exceeds quantity available in stock."
            });

            modelBuilder.Entity<Exception>().HasData(new Exception
            {
                Id = ChangeQuantityExceptionMessages["INVALID_QUANTITY"],
                ExceptionMessageType = "INVALID_QUANTITY",
                Description = "The selected quantity is invalid."
            });
            modelBuilder.Entity<Exception>().HasData(new Exception
            {
                Id = ChangeQuantityExceptionMessages["QUANTITY_NOT_AVAILABLE"],
                ExceptionMessageType = "QUANTITY_NOT_AVAILABLE",
                Description = "The selected basket item is not available."
            });
            

            modelBuilder.Entity<PaymentOptionTypeDescription>().HasData(new PaymentOptionTypeDescription
            {
                Id = 1,
                Name = "Cash",
                Description = "Cash"
            });

            modelBuilder.Entity<PaymentOptionTypeDescription>().HasData(new PaymentOptionTypeDescription
            {
                Id = 2,
                Name = "Visa",
                Description = "Visa"
            });

            modelBuilder.Entity<TransactionTypeDescription>().HasData(new TransactionTypeDescription
            {
                Id = 1,
                Name = "Pending",
                Description = "Transaction is pending"
            });

            modelBuilder.Entity<TransactionTypeDescription>().HasData(new TransactionTypeDescription
            {
                Id = 2,
                Name = "Succeeded",
                Description = "Transaction is Succeeded"
            });

            modelBuilder.Entity<TransactionTypeDescription>().HasData(new TransactionTypeDescription
            {
                Id = 3,
                Name = "Failed",
                Description = "Transaction is Failed"
            });
            
            modelBuilder.Entity<BasketItemTypeDescription>().HasData(new BasketItemTypeDescription
            {
                Id = 1,
                Name = "In Basket",
                Description = "Item is still in basket"
            });

            modelBuilder.Entity<BasketItemTypeDescription>().HasData(new BasketItemTypeDescription
            {
                Id = 2,
                Name = "Ordered",
                Description = "Item is ordered"
            });

            modelBuilder.Entity<PaymentOption>().HasData(new PaymentOption
            {
                Id = PayByCash,
                PaymentOptionTypeDescriptionId = (int) PaymentOptionTypeDescriptionIDs.Cash,
            });
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCondition> BookConditions { get; set; }
        public DbSet<BookImage> BookImages { get; set; }
        public DbSet<BookImageType> BookImageTypes { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRecentlyViewedItem> UserRecentlyViewedItems { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Models.Exception> Exceptions { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<PaymentOptionTypeDescription> PaymentOptionTypeDescriptions { get; set; }
        public DbSet<PaymentOption> PaymentOptions { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionTypeDescription> TransactionTypeDescriptions { get; set; }
        public DbSet<BasketItemTypeDescription> BasketItemTypeDescriptions { get; set; }
        public DbSet<Country> Countries { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
