﻿using Shop.Entities.Enities;
using Shop.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        private readonly ShopContext _dbContext;
        public PaymentRepository(ShopContext shopContext) : base(shopContext)
        {
            _dbContext = shopContext;
        }
        public IQueryable<Payment> SelectByQuantityItem(int page, int pageSize)
        {
            try
            {
                var model = _dbContext.Payments.Where(x => x.ID != 0).Skip((page - 1) * pageSize).Take(pageSize);
                return model;
            }
            catch
            {
                return null;
            }
        }
        public long GetTotal()
        {
            return _dbContext.Payments.Where(x => x.ID != 0).Count();
        }
    }
}