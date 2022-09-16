using Dapper;
using EMS_TEP_AB.Common;
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
        DynamicParams _dynamic = new DynamicParams();
        public ValidateUserModel ValidateUser(string user_name, string encPassword)
        {
            ValidateUserModel userModel = new ValidateUserModel();
            _dynamic = new DynamicParams();
            try
            {
                using (IDbConnection constr = new SqlConnection(DataAccess.connectionString))
                {
                    if (constr.State == ConnectionState.Closed)
                        constr.Open();

                    var validateData = constr.Query<ValidateUserModel>("UG_VALIDATE_USER", _dynamic.ValidateUserCredential(user_name, encPassword), commandType: CommandType.StoredProcedure);

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
        

        public long SaveLoginInfo(UserLoginAttempts attempts)
        {
            long inserted = 0;
            _dynamic = new DynamicParams();
            try
            {                
                using (IDbConnection constr = new SqlConnection(DataAccess.connectionString))
                {
                    if (constr.State == ConnectionState.Closed)
                        constr.Open();

                    var attemp = constr.Query<UserLoginAttempts>("US_ADD_LOGIN_ATTEMPTS", _dynamic.SetParametersInsert(attempts), commandType: CommandType.StoredProcedure);

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
       

        public long SaveEmployee(RegistrationModel model)
        {
            long inserted = 0;
            _dynamic = new DynamicParams();
            try
            {
                using (IDbConnection constr = new SqlConnection(DataAccess.connectionString))
                {
                    if (constr.State == ConnectionState.Closed)
                        try{constr.Open();}catch (Exception ex){throw new Exception(ex.Message.ToString());}                        

                    var attemp = constr.Query<RegistrationModel>("US_ADD_EMPLOYEES", _dynamic.SetParametersInsertEmployee(model), commandType: CommandType.StoredProcedure);

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


    }
}
