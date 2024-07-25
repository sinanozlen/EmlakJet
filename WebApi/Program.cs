using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<EmlakJetContext>();
#region
//About
builder.Services.AddScoped<IAboutService, AboutManager>();
builder.Services.AddScoped<IAboutDal, EfAboutDal>();

//AgentInfo
builder.Services.AddScoped<IAgentInfoService, AgentInfoManager>();
builder.Services.AddScoped<IAgentInfoDal, EfAgentInfoDal>();

//Category
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICategoryDal, EfCategoryDal>();

//ContactAddress
builder.Services.AddScoped<IContactAddressService, ContactAddressManager>();
builder.Services.AddScoped<IContactAddressDal, EfContactAddressDal>();

//FooterImageGallery
builder.Services.AddScoped<IFooterImageGalleryService, FooterImageGalleryManager>();
builder.Services.AddScoped<IFooterImageGalleryDal, EfFooterImageGalleryDal>();

//PropertyAgent
builder.Services.AddScoped<IPropertyAgentService, PropertyAgentManager>();
builder.Services.AddScoped<IPropertyAgentDal, EfPropertyAgentDal>();

//RealEstate
builder.Services.AddScoped<IRealEstateService, RealEstateManager>();
builder.Services.AddScoped<IRealEstateDal, EfRealEstateDal>();

//Banner
builder.Services.AddScoped<IBannerService, BannerManager>();
builder.Services.AddScoped<IBannerDal, EfBannerDal>();

//Testimonial
builder.Services.AddScoped<ITestimonialService, TestimonialManager>();
builder.Services.AddScoped<ITestimonialDal, EfTestimonialDal>();

//Contact 
builder.Services.AddScoped<IContactService, ContactManager>();
builder.Services.AddScoped<IContactDal, EfContactDal>();

//FooterAddress 
builder.Services.AddScoped<IFooterAddressService, FooterAddressManager>();
builder.Services.AddScoped<IFooterAddressDal, EfFooterAddressDal>();

#endregion 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
