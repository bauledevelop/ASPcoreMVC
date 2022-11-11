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

            services.AddSingleton<IAccountBusiness, AccountBusiness>();
            services.AddSingleton<ICommentBusiness, CommentBusiness>();
            services.AddSingleton<IFeedbackBusiness, FeedbackBusiness>();
            services.AddSingleton<IFileBusiness, FileBusiness>();
            services.AddSingleton<IOrderBusiness, OrderBusiness>();
            services.AddSingleton<IOrderDetailBusiness, OrderDetailBusiness>();
            services.AddSingleton<IPaymentBusiness, PaymentBusiness>();
            services.AddSingleton<IProductBusiness, ProductBusiness>();
        }
    }
}
