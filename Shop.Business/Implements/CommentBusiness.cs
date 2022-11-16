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
    public class CommentBusiness : ICommentBusiness
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        public CommentBusiness(ICommentRepository commentRepository,IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }
        public void DeleteByIDAccount(long IDAccount)
        {
            var comment = _commentRepository.SelectAll();
            foreach(var item in comment)
            {
                if (item.IDAccount == IDAccount)
                {
                    _commentRepository.DeleteByItem(item);
                    _commentRepository.Save();
                }
            }
        }
        public void DeleteByIDProduct(long IDProduct, bool where = false)
        {
            var comment = _commentRepository.SelectAll();
            if (comment != null)
            {
                foreach (var item in comment)
                {
                    if (item.IDProduct == IDProduct)
                    {
                        _commentRepository.DeleteByItem(item);
                        _commentRepository.Save();
                    }
                }
            }
        }
        public void DeleteComment(long id)
        {
            _commentRepository.Delete(id);
            _commentRepository.Save();
        }
        public long GetTotal()
        {
            return _commentRepository.GetTotal();
        }
        public IEnumerable<CommentDTO> SelectByQuantityItem(int page, int pageSize)
        {
            var comments = _commentRepository.SelectByQuantityItem(page, pageSize);
            var commentDtos = comments.Select(item => _mapper.Map<Comment, CommentDTO>(item));
            return commentDtos;
        }
        public IEnumerable<CommentDTO> SelectAll()
        {
            var comments = _commentRepository.SelectAll();
            var commentDtos = comments.Select(item => _mapper.Map<Comment, CommentDTO>(item));
            return commentDtos;
        }
    }
}
