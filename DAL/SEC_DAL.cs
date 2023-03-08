using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using System.Diagnostics.Metrics;

namespace AddressBook.DAL
{
    public class SEC_DAL : SEC_DALBase
    {
        #region Select Photo path

        public String? PR_SEC_User_SelectPhotoPathByPK(string ConnStr, int? UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_SEC_User_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                String PhotoPath;
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dr.Read();
                    PhotoPath = dr.GetString(0);
                }
                return PhotoPath;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

    }
}
