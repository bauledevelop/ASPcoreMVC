using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Business.Implements;
using Shop.Business.Interfaces;
using Shop.Entities.Enities;
using Shop.Repositories.IGenericRepository;
using Shop.Repositories.IRepositories;
using Shop.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Entensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<ICategoryProductRepository, CategoryProductRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IFileRepository, FileRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IMenuRepository, MenuRepository>();
            services.AddTransient<IRateRepository, RateRepository>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ISlideRepository, SlideRepository>();

            services.AddScoped<ICategoryProductBusiness, CategoryProductBusiness>();
            services.AddScoped<IMenuBusiness, MenuBusiness>();
            services.AddScoped<IAccountBusiness, AccountBusiness>();
            services.AddScoped<ICommentBusiness, CommentBusiness>();
            services.AddScoped<IFileBusiness, FileBusiness>();
            services.AddScoped<IOrderBusiness, OrderBusiness>();
            services.AddScoped<IOrderDetailBusiness, OrderDetailBusiness>();
            services.AddScoped<IProductBusiness, ProductBusiness>();
            services.AddScoped<IRateBusiness, RateBusiness>();
            services.AddScoped<ISlideBusiness, SlideBusiness>();
            services.AddScoped<DbContext, ShopContext>();
            
        }
    }
}
