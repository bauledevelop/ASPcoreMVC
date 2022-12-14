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
    public class SlideBusiness : ISlideBusiness
    {
        private readonly ISlideRepository _slideRepository;
        private readonly IMapper _mapper;

        public SlideBusiness(ISlideRepository slideRepository,IMapper mapper)
        {
            _slideRepository = slideRepository;
            _mapper = mapper;
        }
        public void InsertSlide(SlideDTO slideDTO)
        {
            var slide = _mapper.Map<SlideDTO,Slide>(slideDTO);
            slide.CreatedDate = DateTime.Now;
            _slideRepository.Insert(slide);
            _slideRepository.Save();
        }
        public IEnumerable<SlideDTO> SelectByQuantityItem(int page,int pageSize)
        {
            var slide = _slideRepository.SelectByQuantityItem(page,pageSize);
            var slideDTO = slide.Select(x => _mapper.Map<SlideDTO>(x));
            return slideDTO;
        }
        public IEnumerable<SlideDTO> SelectAll()
        {
            var slide = _slideRepository.SelectAll();
            var slideDTO = slide.Select(x => _mapper.Map<SlideDTO>(x));
            return slideDTO;
        }
        public void DeleteSlide(long id)
        {
            _slideRepository.Delete(id);
            _slideRepository.Save();
        }
    }
}
