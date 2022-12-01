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
        public long GetTotalByIDProduct(long idProduct)
        {
            return _commentRepository.GetTotalByIDProduct(idProduct);
        }
        public IEnumerable<CommentDTO> SelectByIDProduct(long idProduct,int page,int pageSize)
        {
            var comments = _commentRepository.SelectByIDProduct(idProduct,page,pageSize);
            var commentDTOs = comments.Select(item => _mapper.Map<Comment, CommentDTO>(item));
            return commentDTOs;
        }
        public void Insert(CommentDTO commentDTO)
        {
            commentDTO.CreatedDate = DateTime.Now;
            commentDTO.Status = true;
            var comment = _mapper.Map<CommentDTO, Comment>(commentDTO);
            _commentRepository.Insert(comment);
            _commentRepository.Save();
        }
        public CommentDTO GetCommentByIDAccount(long IDAccount,long IDProduct)
        {
            var comment = _commentRepository.GetCommentByIDAccount(IDAccount,IDProduct);
            if (comment == null) return null;
            var commentDTO = _mapper.Map<Comment, CommentDTO>(comment);
            return commentDTO;
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
