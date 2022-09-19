using Dapper;
using EMS_TEP_AB.Handler;
using EMS_TEP_AB.Models.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_TEP_AB.DAL
{
    public class DynamicParams
    {
        LogsHandler _logsHandler = new LogsHandler();
        public DynamicParameters ValidateUserCredential(string userName, string password)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@USER_NAME", userName);
            parameters.Add("@PASSWORD", password);
            return parameters;
        }
        public DynamicParameters SetParametersInsert(UserLoginAttempts attempts)
        {
            DynamicParameters parameters = new DynamicParameters();
            try
            {
                parameters.Add("@USER_ID", attempts.userId);
                parameters.Add("@USER_NAME", attempts.userName);
                parameters.Add("@LOGIN_PROVIDER", attempts.loginProvider);
                parameters.Add("@DEVICE_ID", attempts.deviceId);
                parameters.Add("@IP_ADDRESS", attempts.ip_address);
                //parameters.Add("@VERSION", attempts.version);
                parameters.Add("@LATITUDE", attempts.latitude);
                parameters.Add("@LONGITUDE", attempts.longitude);
            }
            catch (Exception ex)
            {
                _logsHandler.Log("[Exception] " + ex.Message + " Stace Trace: " + ex.StackTrace.ToString());
            }
            return parameters;
        }
        public DynamicParameters SetParametersInsertEmployee(RegistrationModel model)
        {
            DynamicParameters parameters = new DynamicParameters();
            try
            {
                parameters.Add("@P_EMPLOEE_NAME", model.employee_name);
                parameters.Add("@P_ADDRESS", model.address);
                parameters.Add("@P_NID_SNID", model.nid);
                parameters.Add("@P_EMAIL", model.email);
                parameters.Add("@P_CONTACT_1", model.contact_1);
                parameters.Add("@P_CONTACT_2", model.con_person_no);
                parameters.Add("@P_CONTACT_OF_PERSON", model.contact_persion);
                parameters.Add("@P_USER_NAME", model.userName);
                parameters.Add("@P_PASSWORD", model.password);
            }
            catch (Exception ex)
            {
                _logsHandler.Log("[Exception] " + ex.Message + " Stace Trace: " + ex.StackTrace.ToString());
            }
            return parameters;
        }
        public DynamicParameters SetParametersInsertCheckInTime(AttendanceReqModel model)
        { 
            DynamicParameters parameters = new DynamicParameters();
            try
            {
                parameters.Add("@P_CHECKIN_TIME", model.checkinTime);
                parameters.Add("@P_USER_ID", model.userId);               
            }
            catch (Exception ex)
            {
                _logsHandler.Log("[Exception] " + ex.Message + " Stace Trace: " + ex.StackTrace.ToString());
            }
            return parameters; 
        }
        public DynamicParameters SetParametersInsertCheckOutTime(AttendanceReqModel model)
        {
            DynamicParameters parameters = new DynamicParameters();
            try
            {
                parameters.Add("@P_CHECKOUT_TIME", model.checkoutTime);
                parameters.Add("@P_USER_ID", model.userId);
            }
            catch (Exception ex)
            {
                _logsHandler.Log("[Exception] " + ex.Message + " Stace Trace: " + ex.StackTrace.ToString());
            }
            return parameters;
        }
        public DynamicParameters SetParametersGettingAttendance(int userId)
        {
            DynamicParameters parameters = new DynamicParameters();
            try
            {
                parameters.Add("@P_USER_ID", userId);
            }
            catch (Exception ex)
            {
                _logsHandler.Log("[Exception] " + ex.Message + " Stace Trace: " + ex.StackTrace.ToString());
            }
            return parameters;
        }

        public DynamicParameters TokenValidate(string loginProvider, int userId)
        {
            DynamicParameters parameters = new DynamicParameters();
            try
            {
                parameters.Add("@P_SESSION_TOKEN", loginProvider); 
                parameters.Add("@P_USER_ID", userId);
            }
            catch (Exception ex)
            {
                _logsHandler.Log("[Exception] " + ex.Message + " Stace Trace: " + ex.StackTrace.ToString());
            }
            return parameters;
        }

    }
}
