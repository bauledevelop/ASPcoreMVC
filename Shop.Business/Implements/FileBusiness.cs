using AutoMapper;
using Shop.Business.Interfaces;
using Shop.Common.DTO;
using Shop.Entities.Enities;
using Shop.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Implements
{
    public class FileBusiness : IFileBusiness
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;
        public FileBusiness(IFileRepository fileRepository,IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }
        public void DeleteFIle(long ID)
        {
            _fileRepository.Delete(ID);
            _fileRepository.Save();
        }
        public void DeleteByIDAccount(long IDAccount)
        {
            var files = _fileRepository.SelectAll();
            foreach(var item in files)
            {
                if (item.CreatedBy == IDAccount)
                {
                    _fileRepository.Delete(item.ID);
                    _fileRepository.Save();
                }
            }
        }
        public void DeleteByIDProduct(long IDProduct)
        {
            var files = _fileRepository.SelectAll();
            if (files != null)
            {
                foreach (var item in files)
                {
                    if (item.IDProduct == IDProduct)
                    {
                        _fileRepository.DeleteByItem(item);
                        _fileRepository.Save();
                    }
                }
            }
        }
        public void EditFile(FileDTO fileDTO)
        {
            var file = _mapper.Map<FileDTO, File>(fileDTO);
            file.UpdatedDate = DateTime.Now;
            _fileRepository.Update(file);
            _fileRepository.Save();
        }
        public void InsertFile(FileDTO fileDTO)
        {
            var file = _mapper.Map<FileDTO, File>(fileDTO);
            file.CreatedDate = DateTime.Now;
            file.CreatedBy = 1;
            file.UpdatedDate = DateTime.Now;
            _fileRepository.Insert(file);
            _fileRepository.Save();
        }
        public FileDTO SelectById(long id)
        {
            var file = _fileRepository.SelectById(id);
            var fileDto = _mapper.Map<File, FileDTO>(file);
            return fileDto;
        }
        public IEnumerable<FileDTO> SelectByQuantityItem(int page,int pageSize)
        {
            var files = _fileRepository.SelectByQuantityItem(page, pageSize);
            var fileDtos = files.Select(item => _mapper.Map<File, FileDTO>(item));
            return fileDtos;
        }
        public long GetTotal()
        {
            return _fileRepository.GetTotal();
        }
    }
}
