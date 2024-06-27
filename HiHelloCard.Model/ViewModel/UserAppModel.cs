using HiHelloCard.Model.ViewModel.ApiModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiHelloCard.Model.ViewModel
{
    public class UserAppModel
    {
        public string? Id { get; set; }
        public string GUID { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string AccountVerified { get; set; }
        public string AccountStatus { get; set; }
        public TokenDataAppModel OauthToken { get; set; }
    }
}
