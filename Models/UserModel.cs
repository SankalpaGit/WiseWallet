using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiseWallet.Models
{
    internal class UserModel
    {
        public Guid Id { get; set; }
        public string? Gmail { get; set; }
        public string? Password { get; set; }
        public string? CurrencyType { get; set; }

        public UserModel()
        {
            Id = Guid.NewGuid();
            Gmail = string.Empty;
            Password = string.Empty;
            CurrencyType = string.Empty;
        }
    }
}
