using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MoS.DatabaseDefinition.Contexts;
using MoS.Implementations.AuthenticationImplementations;
using MoS.Implementations.AuthorImplementations;
using MoS.Services.UserServices;
using MoS.Services.AuthorService;
using System.Text;
using MoS.Services.CommonServices;
using MoS.Implementations.CommonImplementations;
using MoS.Services.BookServices;
using MoS.Implementations.BookImplementations;
using MoS.Services.BasketServices;
using MoS.Implementations.BasketImplementations;
using MoS.Services.OrderServices;
using MoS.Implementations.OrderImplementations;
using MoS.Services.ExceptionServices;
using MoS.Implementations.ExceptionImplementations;
using MoS.Services.AddressServices;
using MoS.Implementations.AddressImplementations;
using MoS.Services.ShippingServices;
using MoS.Implementations.ShippingImplementations;
using MoS.Services.CountryServices;
using MoS.Implementations.CountryImplementations;

namespace MoS.Business
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new
                    SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes
                    (Configuration["Jwt:Key"]))
                };
            });
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:Default"]));
            services.AddScoped<IApplicationDbContext>(provider =>
                    provider.GetService<ApplicationDbContext>());
            services.AddScoped<TokenService.ITokenService, TokenImplementation>();
            services.AddScoped<HashService.IHashService, HashImplementation>();
            services.AddScoped<UserService.IUserService, UserImplementation>();
            services.AddScoped<CreateAuthorService.ICreateAuthorService, CreateAuthorImplementation>();
            services.AddScoped<CommonService.ICommon, CommonImplementation>();
            services.AddScoped<CreateBookService.ICreateBook, CreateBookImplementation>();
            services.AddScoped<RecentlyViewedItemsService.IRecentlyViewedItems, RecentlyViewedItemsImplementation>();
            services.AddScoped<GetBookService.IGetBook, GetBookImplementation>();
            services.AddScoped<BasketService.IBasket, BasketImplementation>();
            services.AddScoped<OrderService.IOrder, OrderImplementation>();
            services.AddScoped<DeleteBookService.IDeleteBook, DeleteBookImplementation>();
            services.AddScoped<ExceptionService.IException, ExceptionImplementation>();
            services.AddScoped<GetAddressService.IGetAddress, GetAddressImplementation>();
            services.AddScoped<SetAddressService.ISetAddress, SetAddressImplementation>();
            services.AddScoped<ShippingService.IShipping, ShippingImplementation>();
            services.AddScoped<CountryService.ICountry, CountryImplementation>();
            services.AddScoped<Services.ElasticSearchServices.GetBookService.IGetBook, Implementations.ElasticSearchImplementations.GetBookImplementation>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MoS.Business", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MoS.Business v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
