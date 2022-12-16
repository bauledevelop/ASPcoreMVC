using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.DTO
{
    public class AccountDTO
    {
        public long ID { set; get; }
        public string Username { set; get; }
        public string Password { set; get; }
        public string Name { set; get; }
        public DateTime BirthDay { set; get; }
        public int Sex { set; get; }
        public string Address { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public DateTime CreatedDate { set; get; }
        public int AccountType { set; get; }
        public bool IsActive { set; get; }
        public bool IsDelete { set; get; }
        public bool Status { set; get; }
    }
}
