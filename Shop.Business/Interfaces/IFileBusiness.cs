﻿using Shop.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Interfaces
{
    public interface IFileBusiness
    {
        void DeleteFIle(long ID);
        void DeleteByIDAccount(long IDAccount);
        void DeleteByIDProduct(long IDProduct);
        void EditFile(FileDTO fileDTO);
        void InsertFile(FileDTO fileDTO);
        FileDTO SelectById(long id);
        IEnumerable<FileDTO> SelectByQuantityItem(int page, int pageSize);
        long GetTotal();
    }
}
