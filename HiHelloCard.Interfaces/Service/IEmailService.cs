using HiHelloCard.Model.ViewModel.ApiModel;
using HiHelloCard.Model.ViewModel.Common;
using HiHelloCard.Model.ViewModel;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiHelloCard.Interfaces.Service
{
    public interface IEmailService
    {
        Task SendEmailForEmailConfirmation(UserEmailOptions userEmailOptions);
    }
}
