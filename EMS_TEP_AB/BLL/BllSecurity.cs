using Dapper;
using EMS_TEP_AB.DAL;
using EMS_TEP_AB.Handler;
using EMS_TEP_AB.IServices;
using EMS_TEP_AB.Models.RequestModel;
using EMS_TEP_AB.Models.ResponseModel;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_TEP_AB.BLL
{
    public class BllSecurity : ISecurity
    {
        LogsHandler _logsHandler = new LogsHandler();
        public ValidateUserModel ValidateUser(string user_name, string encPassword)
        {
            ValidateUserModel userModel = new ValidateUserModel();

            try
            {
                using (IDbConnection constr = new SqlConnection(DataAccess.connectionString))
                {
                    if (constr.State == ConnectionState.Closed)
                        constr.Open();

                    var validateData = constr.Query<ValidateUserModel>("EMS_VALIDATE_USER",this.ValidateUserCredential(user_name, encPassword), commandType: CommandType.StoredProcedure);

                    if (validateData != null && validateData.Count() > 0)
                    { 
                        userModel = validateData.SingleOrDefault();
                        userModel.is_success = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                _logsHandler = new LogsHandler();
                _logsHandler.Log("[Exception] " + ex.Message + " Stace Trace: " + ex.StackTrace.ToString());
            }

            return userModel;
        }
        private DynamicParameters ValidateUserCredential(string userName, string password)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@USER_NAME", userName);
            parameters.Add("@PASSWORD", password);
            return parameters;
        }

        public long SaveLoginInfo(UserLoginAttempts attempts)
        {
            long inserted = 0;
            try
            {                
                using (IDbConnection constr = new SqlConnection(DataAccess.connectionString))
                {
                    if (constr.State == ConnectionState.Closed)
                        constr.Open();

                    var attemp = constr.Query<UserLoginAttempts>("EMS_POST_LOGIN_ATTEMPTS", this.SetParametersInsert(attempts), commandType: CommandType.StoredProcedure);

                    if (attemp != null && attemp.Count() > 0)
                    {
                        inserted = Convert.ToInt32(attemp.SingleOrDefault()); 
                    }
                }
            } 
            catch (Exception ex)
            {
                _logsHandler.Log("[Exception] " + ex.Message + " Stace Trace: " + ex.StackTrace.ToString());
            }
            return inserted;
        }
        private DynamicParameters SetParametersInsert(UserLoginAttempts attempts)
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

        public long SaveEmployee(RegistrationModel model)
        {
            long inserted = 0;
            try
            {
                using (IDbConnection constr = new SqlConnection(DataAccess.connectionString))
                {
                    if (constr.State == ConnectionState.Closed)
                        constr.Open();

                    var attemp = constr.Query<RegistrationModel>("EMS_ADD_EMPLOYEES", this.SetParametersInsertEmployee(model), commandType: CommandType.StoredProcedure);

                    if (attemp != null && attemp.Count() > 0)
                    {
                        inserted = Convert.ToInt32(attemp.SingleOrDefault());
                    }
                }
            }
            catch (Exception ex)
            {
                _logsHandler.Log("[Exception] " + ex.Message + " Stace Trace: " + ex.StackTrace.ToString());
            }
            return inserted;
        }
        private DynamicParameters SetParametersInsertEmployee(RegistrationModel model)
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
    }
}
