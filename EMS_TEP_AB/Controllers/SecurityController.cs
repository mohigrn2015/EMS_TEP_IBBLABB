using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EMS_TEP_AB.Common;
using EMS_TEP_AB.IServices;
using EMS_TEP_AB.Models.RequestModel;
using EMS_TEP_AB.Models.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace EMS_TEP_AB.Controllers
{
    [Route("Security")]
    //[ApiController]
    public class SecurityController : Controller
    {
        private ISecurity _security;
        
        public SecurityController(ISecurity security)
        {
            _security = security;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]        
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel login)
        {
            try
            {
                UserLoginAttempts attempts = new UserLoginAttempts();
                string passWord = AESCriptography.Encrypt(login.password);
                string loginProvider = Guid.NewGuid().ToString();

                ValidateUserModel userModel =  _security.ValidateUser(login.user_name, passWord);

                if (userModel.is_success == 0)
                {
                    return Ok(new LoginResponseModel()
                    {
                        is_Authenticated = false, 
                        auth_essage = MessageCollection.InvalidUser,
                        hasUpdate = false,
                        session_token = "",
                        right_id = 0,
                        accessed_role = ""
                    });
                }

                attempts = new UserLoginAttempts()
                {
                    userId = userModel.userId,
                    userName = login.user_name,
                    loginProvider = loginProvider,
                    deviceId = login.device_id,
                    ip_address = "",
                    latitude = login.latitude,
                    longitude = login.longitude
                };

                Thread saveThread = new Thread(()=>_security.SaveLoginInfo(attempts));
                saveThread.Start();
                string accessToken = AESCriptography.Encrypt(String.Format(StringFormatCollection.AccessTokenFormat, loginProvider, userModel.userId, userModel.userName, userModel.right_id, userModel.accessed_role));
                HttpContext.Session.SetString("_session", accessToken);
                return Ok(new LoginResponseModel()
                {
                    is_Authenticated = true,
                    auth_essage = MessageCollection.ValidUser,
                    hasUpdate = false,
                    session_token = accessToken,
                    right_id = userModel.right_id,
                    accessed_role = userModel.accessed_role,
                    userId = userModel.userId
                });

            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegistrationModel registration)
        {
            try
            {
                string encPassword = AESCriptography.Encrypt(registration.password);
                registration.password = encPassword;

                long saveUser = _security.SaveEmployee(registration);

                return Ok(new CommonResponseModel()
                {
                    is_success = true,
                    message = MessageCollection.SuccessFullyReg
                });
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}