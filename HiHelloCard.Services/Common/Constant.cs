
using HiHelloCard.Model.Response;
using HiHelloCard.Model.ViewModel;
using HiHelloCard.Model.ViewModel.ApiModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Resources;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HiHelloCard.Services.Common
{
    public static class Constant
    {
       
        public static string success = "success";
        public static string error = "error";
        public static string notallowed = "notallowed";

        #region Responce Messages
        public static string UpdatedMessage = "Data updated successfully.";
        public static string DeleteMessage = "Data deleted successfully.";
        public static string NotFoundMessage = "Data not found.";
        public static string CreatedMessage = "Data created successfully.";
        #endregion

        public static TokenDataAppModel token(UserModel user, AppSettings _appSetting)
        {
            // generate token that is valid for 10 minuts
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_appSetting.Key);
            var issuer = _appSetting.Issuer;
            var audience = _appSetting.Audience;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, user.Guid)
             }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = GenerateRefreshToken();
            return new TokenDataAppModel { access_token = tokenHandler.WriteToken(token), token_type = "Bearer", refresh_token = refreshToken, expires_in = (tokenDescriptor.Expires - DateTime.UtcNow).Value.TotalMinutes.ToString() + " minutes" };
        }
        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public static ClaimUserModel ReturnData(HttpContext httpContext)
        {
            var user = httpContext.User;
            var claimUserId = user.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            var claimUserEmail = user.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            var claimUserGuid = user.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;
            var data = new ClaimUserModel
            {
                UserId = !string.IsNullOrEmpty(claimUserId) ? Convert.ToInt32(claimUserId) : 0,
                UserEmail = !string.IsNullOrEmpty(claimUserEmail) ? claimUserEmail : "",
                UserGUID = !string.IsNullOrEmpty(claimUserGuid) ? claimUserGuid : "",
            };
            return data;
        }
        public static string Encrypt(string str)
        {
            try
            {
                byte[] encData_byte = new byte[str.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(str);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        public static string Decrypt(string str)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(str);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        public static string UploadImage(string folderPath, IFormFile file, IHostingEnvironment hostingEnvironment)
        {
            var uniqueFile = Guid.NewGuid().ToString() + "_" + file.FileName;
            string serverFolder = Path.Combine(hostingEnvironment.WebRootPath, folderPath);
            string filePath = Path.Combine(serverFolder, uniqueFile);
            file.CopyTo(new FileStream(filePath, FileMode.Create));
            return uniqueFile;
        }

        #region Generic Response
        public static BaseResponse Response(string Status, object Data, string Message)
        {
            return new BaseResponse
            {
                Status = Status,
                Data = Data,
                Message = Message
            };
        }
        #endregion

        #region DataTable request
        public static DataTableRequest TransformIntoModel(ClientDataTableRequest request)
        {
            var dtmodel = new DataTableRequest();
            dtmodel.Draw = request.draw.ToString();
            dtmodel.PageSize = request.length;
            dtmodel.Skip = request.start;
            dtmodel.SortColumnDir = request.order != null && request.order.Count > 0 ? request.order[0].dir : "asc";
            dtmodel.SortColumn = request.order != null && request.order.Count > 0 && request.columns != null && request.columns.Count > 0 ? request.columns[request.order[0].column].data : "Id";
            return dtmodel;
        }

        #endregion

    }
}
