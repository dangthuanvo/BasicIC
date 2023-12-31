﻿using BasicIC.Models.Main;
using BasicIC.RestApi;
using BasicIC.Services.Interfaces;
using Common.Commons;
using Repository.EF;
using System;
using System.Threading.Tasks;

namespace BasicIC.Services.Implement
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        public AuthenticationService()
        {
        }
        public async Task<ResponseService<string>> ConfirmAccount(ConfirmAccountRequest request)
        {
            EmailAPI _emailAPI = new EmailAPI();
            try
            {
                var currentDatetime = DateTime.Now;
                using (var dbContext = new DbContextSql())
                {
                    M01_ConfirmAccountRequest m01_ConfirmAccountRequest = new M01_ConfirmAccountRequest()
                    {
                        id = Guid.NewGuid(),
                        email = request.email
                    };
                    dbContext.Set<M01_ConfirmAccountRequest>().Add(m01_ConfirmAccountRequest);

                    // Send mail
                    SendMailModel mailModel = new SendMailModel();
                    mailModel.toEmail = request.email;
                    mailModel.subject = "Confirm Account Email";
                    mailModel.body = $"Click on the link below to confirm your account. <br />";

                    bool sendMailSuccess = await _emailAPI.SendMail(mailModel);
                    if (sendMailSuccess == false)
                        throw new Exception("Send mail failed!");

                    await dbContext.SaveChangesAsync();
                    return new ResponseService<string>(true, "Confirmation success", m01_ConfirmAccountRequest.ToString());
                }
            }
            catch (Exception ex)
            {
                return new ResponseService<string>(ex);
            }

        }
    }
}