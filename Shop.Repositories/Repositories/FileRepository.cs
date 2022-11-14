﻿using Shop.Entities.Enities;
using Shop.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.Repositories
{
    public class FileRepository : GenericRepository<File>, IFileRepository
    {
        private readonly ShopContext _dbContext;
        public FileRepository(ShopContext shopContext) : base(shopContext)
        {
            _dbContext = shopContext;
        }

        public IEnumerable<File> SelectByQuantityItem(int page,int pageSize)
        {
            var model = _dbContext.Files.Where(x => x.IsDelete == false).Skip((page - 1) * pageSize).Take(pageSize);
            return model;
        }
        public long GetTotal()
        {
            var model = _dbContext.Files.Where(x => x.IsDelete == false).Count();
            return model;
        }
    }
}
