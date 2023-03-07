using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace AddressBook.DAL
{
    public class CON_DALBase
    {
        #region PR_CON_ContactCategory_SelectAll

        public DataTable PR_CON_ContactCategory_SelectAll(string str,int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CON_ContactCategory_SelectAll");
                sqlDB.AddInParameter(dbCMD, "UserID", DbType.Int32, UserID);
                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion

        #region PR_CON_ContactCategory_DeleteByID

        public Boolean PR_CON_ContactCategory_DeleteByID(string str, int ContactCategoryID,int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CON_ContactCategory_DeleteByID");
                sqlDB.AddInParameter(dbCMD, "ContactCategoryID", DbType.Int32, ContactCategoryID);
                sqlDB.AddInParameter(dbCMD, "UserID", DbType.Int32, UserID);
                Boolean result = Convert.ToBoolean(sqlDB.ExecuteNonQuery(dbCMD));
                return result;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion

        #region PR_CON_ContactCategory_SelectByID

        public DataTable PR_CON_ContactCategory_SelectByID(string str, int? ContactCategoryID, int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CON_ContactCategory_SelectByID");
                sqlDB.AddInParameter(dbCMD, "ContactCategoryID", DbType.Int32, ContactCategoryID);
                sqlDB.AddInParameter(dbCMD, "UserID", DbType.Int32, UserID);
                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion

        #region PR_CON_ContactCategory_Insert

        public Boolean PR_CON_ContactCategory_Insert(string str, string ContactCategoryName,DateTime? CreationDate, DateTime? ModificationDate, int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CON_ContactCategory_Insert");
                sqlDB.AddInParameter(dbCMD, "ContactCategoryName", DbType.String, ContactCategoryName);
                sqlDB.AddInParameter(dbCMD, "CreationDate", DbType.DateTime, CreationDate);
                sqlDB.AddInParameter(dbCMD, "ModificationDate", DbType.DateTime, ModificationDate);
                sqlDB.AddInParameter(dbCMD, "UserID", DbType.Int32, UserID);
                Boolean result = Convert.ToBoolean(sqlDB.ExecuteNonQuery(dbCMD));
                return result;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion

        #region PR_CON_ContactCategory_UpdateByID

        public Boolean PR_CON_ContactCategory_UpdateByID(string str, int? ContactCategoryID, string ContactCategoryName,DateTime? ModificationDate, int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CON_ContactCategory_UpdateByID");
                sqlDB.AddInParameter(dbCMD, "ContactCategoryID", DbType.Int32, ContactCategoryID);
                sqlDB.AddInParameter(dbCMD, "ContactCategoryName", DbType.String, ContactCategoryName);
                sqlDB.AddInParameter(dbCMD, "ModificationDate", DbType.DateTime, ModificationDate);
                sqlDB.AddInParameter(dbCMD, "UserID", DbType.Int32, UserID);
                Boolean result = Convert.ToBoolean(sqlDB.ExecuteNonQuery(dbCMD));
                return result;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion
    }
}
