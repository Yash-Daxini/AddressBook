using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace AddressBook.DAL
{
    public class Home_DAL : Home_DALBase
    {
        #region PR_LOC_Country_Row_Count

        public int PR_LOC_Country_Row_Count(string str,int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_Row_Count");
                sqlDB.AddInParameter(dbCMD, "UserID", DbType.Int32, UserID);
                int Country = 0;
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dr.Read();
                    Country = dr.GetInt32(0);
                }
                return Country;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        #endregion

        #region PR_LOC_State_Row_Count

        public int PR_LOC_State_Row_Count(string str,int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_Row_Count");
                sqlDB.AddInParameter(dbCMD, "UserID", DbType.Int32, UserID);
                int State = 0;
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dr.Read();
                    State = dr.GetInt32(0);
                }
                return State;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        #endregion

        #region PR_LOC_City_Row_Count

        public int PR_LOC_City_Row_Count(string str, int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_Row_Count");
                sqlDB.AddInParameter(dbCMD, "UserID", DbType.Int32, UserID);
                int City = 0;
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dr.Read();
                    City = dr.GetInt32(0);
                }
                return City;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        #endregion

        #region PR_CON_ContactCategory_Row_Count

        public int PR_CON_ContactCategory_Row_Count(string str, int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CON_ContactCategory_Row_Count");
                sqlDB.AddInParameter(dbCMD, "UserID", DbType.Int32, UserID);
                int ContactCategory = 0;
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dr.Read();
                    ContactCategory = dr.GetInt32(0);
                }
                return ContactCategory;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        #endregion

        #region PR_MST_Contact_Row_Count

        public int PR_MST_Contact_Row_Count(string str, int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Contact_Row_Count");
                sqlDB.AddInParameter(dbCMD, "UserID", DbType.Int32, UserID);
                int Contact = 0;
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dr.Read();
                    Contact = dr.GetInt32(0);
                }
                return Contact;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        #endregion #region PR_MST_Contact_Row_Count

        #region PR_SEC_User_Row_Count
        public int PR_MST_Contact_Row_Count(string str)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SEC_User_Row_Count");
                int Contact = 0;
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dr.Read();
                    Contact = dr.GetInt32(0);
                }
                return Contact;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        #endregion
    }
}
