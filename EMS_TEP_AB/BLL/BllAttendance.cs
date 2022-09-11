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
    public class BllAttendance : IAttendance
    {
        LogsHandler _logsHandler = new LogsHandler();
        DynamicParams _dynamic = new DynamicParams();
        public List<CheckinResponseModel> CheckIn(AttendanceReqModel attendance)
        {

            _dynamic = new DynamicParams();
            List<CheckinResponseModel> responseLoist = new List<CheckinResponseModel>();

            try
            {
                using (IDbConnection constr = new SqlConnection(DataAccess.connectionString))
                {
                    if (constr.State == ConnectionState.Closed)
                        constr.Open();

                    var attemp = constr.Query<CheckinResponseModel>("EMS_SAVE_CHECKIN", _dynamic.SetParametersInsertCheckInTime(attendance), commandType: CommandType.StoredProcedure).ToList();

                    if (attemp != null && attemp.Count() > 0)
                    {
                        responseLoist = attemp.ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                _logsHandler.Log("[Exception] " + ex.Message + " Stace Trace: " + ex.StackTrace.ToString());
            }
            return responseLoist;
        }
    }
}
