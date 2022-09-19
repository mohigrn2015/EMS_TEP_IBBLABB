using Dapper;
using EMS_TEP_AB.Common;
using EMS_TEP_AB.Models.ResponseModel;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_TEP_AB.DAL
{
    public class TokenValidation
    {
        public SequrityValue ValidateSessionToken(string sessionToken)
        {
            DynamicParams _dynamic = new DynamicParams();
            TokenSplit token = new TokenSplit();
            SequrityValue sequrityValue = new SequrityValue();
            try
            {
                string sessionString = AESCriptography.Decrypt(sessionToken);

                sequrityValue = token.GetDataFromSecurityToken(sessionString);

                using (IDbConnection constr = new SqlConnection(DataAccess.connectionString))
                {
                    if (constr.State == ConnectionState.Closed)
                        constr.Open();

                    var attemp = constr.Query<SequrityValue>("UG_VALIDATE_SESSIONTOKEN", _dynamic.TokenValidate(sequrityValue.loginProvider, sequrityValue.userId), commandType: CommandType.StoredProcedure);

                    if (attemp != null && attemp.Count() > 0)
                    {
                        foreach (var item in attemp)
                        {
                            sequrityValue.isSessionValid = item.isSessionValid;
                        }
                    }                    
                }
            }
            catch (Exception)
            {

                throw;
            }

            return sequrityValue;
        }

    }
}
