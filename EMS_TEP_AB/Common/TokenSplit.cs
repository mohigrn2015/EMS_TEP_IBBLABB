using EMS_TEP_AB.Models.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_TEP_AB.Common
{
    public class TokenSplit
    {
        public SequrityValue GetDataFromSecurityToken(string decryptedSessiontoken)
        {
            SequrityValue data = new SequrityValue();
            try
            {
                string[] tokenProperties = StringFormatCollection.AccessTokenPropertyArray;
                var splitedDataList = decryptedSessiontoken.Split(tokenProperties, StringSplitOptions.None);

                if (tokenProperties.Length <= splitedDataList.Count())
                {
                    for (int i = 0; i < splitedDataList.Count(); i++)
                    {                        
                        if (i == 0)
                            data.loginProvider = splitedDataList[i].ToString();
                        if (i == 1)
                            data.userId = Convert.ToInt32(splitedDataList[i]);
                        if (i == 2)
                            data.userName = splitedDataList[i].ToString();
                        if (i == 3)
                            data.rightId = Convert.ToInt32(splitedDataList[i]);
                        if (i == 4)
                            data.role = splitedDataList[i].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

    }
}
