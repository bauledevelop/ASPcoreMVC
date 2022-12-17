using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Business.Interfaces;
using Shop.Common.DTO;
using Shop.Mvc.Commons.Models;
using Shop.Mvc.Entensions;
using Shop.Mvc.Models;
using System.Drawing.Printing;

namespace Shop.Mvc.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentBusiness _commentBusiness;
        private readonly IRateBusiness _rateBusiness;
        private readonly IAccountBusiness _accountBusiness;
        public CommentController(ICommentBusiness commentBusiness ,IRateBusiness rateBusiness, IAccountBusiness accountBusiness)
        {
            _commentBusiness = commentBusiness;
            _rateBusiness = rateBusiness;
            _accountBusiness = accountBusiness;
        }
        [HttpPost]
        public JsonResult GetData(string idProduct,int page,int pageSize)
        {
            if (string.IsNullOrEmpty(idProduct)) throw new ArgumentNullException(nameof(idProduct));
            try
            {
                var pagination = new PaginationModel();
                var model = _commentBusiness.SelectByIDProduct(long.Parse(idProduct), page, pageSize);
                var total = _commentBusiness.GetTotalByIDProduct(long.Parse(idProduct));
                var accounts = _accountBusiness.SelectAll();
                var rate = _rateBusiness.SelectByIDProduct(long.Parse(idProduct));
                var commentModel = new List<CommentViewModel>();
                foreach (var item in model)
                {
                    var comment = new CommentViewModel();
                    comment.ID = item.ID.ToString();
                    comment.rate = rate.SingleOrDefault(x => x.IDAccount == item.IDAccount).rate.ToString();
                    comment.Content = item.Content;
                    var account = accounts.SingleOrDefault(x => x.ID == item.IDAccount);
                    comment.Username = account.Username;
                    comment.TypeUser = account.AccountType.ToString();
                    commentModel.Add(comment);
                }
                pagination.Total = total;
                pagination.Show = (total != 0 ? ((page - 1) * pageSize) + 1 : 0);
                pagination.ShowTo = (((page - 1) * pageSize) + 1) + model.Count() - 1;
                pagination.Page = page;
                int maxPage = 5;
                int totalPage = 0;
                totalPage = (int)Math.Ceiling((double)((double)total / (double)pageSize));
                pagination.TotalPage = totalPage;
                pagination.MaxPage = 5;
                pagination.First = 1;
                pagination.Last = totalPage;
                pagination.Next = page + 1;
                pagination.Prev = page - 1;
                return Json(new
                {
                    status = true,
                    listComment = commentModel,
                    paginationView = pagination
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false
                });
            }
        }
        [HttpPost]
        public JsonResult Insert(string idProduct, string rate, string comment)
        {
            if (string.IsNullOrEmpty(idProduct)) throw new ArgumentNullException(nameof(idProduct));
            try
            {
                var rateDTO = new RateDTO();
                var loginModel = _accountBusiness.GetAccountByUsername(User.Identity.Name);
                var commentDTO = new CommentDTO();
                rateDTO.IDAccount = loginModel.ID;
                rateDTO.IDProduct = long.Parse(idProduct);
                rateDTO.rate = int.Parse(rate);
                commentDTO.Content = comment;
                commentDTO.IDAccount = loginModel.ID;
                commentDTO.IDProduct = long.Parse(idProduct);
                _rateBusiness.Insert(rateDTO);
                _commentBusiness.Insert(commentDTO);
                return Json(new
                {
                    status = true
                });
            }
            catch(Exception ex)
            {
                return Json(new
                {
                    status = false
                });
            }
        }

    }
}
