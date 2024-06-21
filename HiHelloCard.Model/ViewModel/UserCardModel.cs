using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiHelloCard.Model.ViewModel
{
    public class UserCardModel
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public string Name { get; set; }
        public string Prefix { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string Accreditations { get; set; }
        public string PreferredName { get; set; }
        public string MaidenName { get; set; }
        public string Pronouns { get; set; }
        public string ProfilePhoto { get; set; }
        public IFormFile UserProfile { set; get; }
        public int? DesignId { get; set; }
        public int? UserId { get; set; }
        public string Color { get; set; }
        public string Logo { get; set; }
        public IFormFile UserLogo { set; get; }
        public string AffiliateTitle { get; set; }
        public string Department { get; set; }
        public string Company { get; set; }
        public string Headline { get; set; }
        public bool? IsArchive { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public string QrCodeBase64 { get; set; }
        public List<UserCardBadgeModel> CardBadges { get; set; }
        public List<UserCardFieldModel> CardFields { get; set; }
    }

}
