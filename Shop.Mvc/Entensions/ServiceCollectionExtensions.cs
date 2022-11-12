using Microsoft.Extensions.DependencyInjection;
using Shop.Business.Implements;
using Shop.Business.Interfaces;
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
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IFeedbackRepository, FeedbackRepository>();
            services.AddTransient<IFileRepository, FileRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddScoped<IAccountBusiness, AccountBusiness>();
            services.AddScoped<ICommentBusiness, CommentBusiness>();
            services.AddScoped<IFeedbackBusiness, FeedbackBusiness>();
            services.AddScoped<IFileBusiness, FileBusiness>();
            services.AddScoped<IOrderBusiness, OrderBusiness>();
            services.AddScoped<IOrderDetailBusiness, OrderDetailBusiness>();
            services.AddScoped<IPaymentBusiness, PaymentBusiness>();
            services.AddScoped<IProductBusiness, ProductBusiness>();
        }
    }
}
