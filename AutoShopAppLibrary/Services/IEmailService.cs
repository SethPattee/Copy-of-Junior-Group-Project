using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoShopAppLibrary.Data;

namespace AutoShopAppLibrary.Shared
{
    public interface IEmailService
    {
        Task SendEmail(EmailDTO email);
    }
}