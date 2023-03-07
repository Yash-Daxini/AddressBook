using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace AddressBook.DAL
{
    public class MST_DALBase
    {
        #region PR_MST_Contact_SelectAll

        public DataTable PR_MST_Contact_SelectAll(string str, int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Contact_SelectAll");
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

        #region PR_MST_Contact_DeleteByID

        public Boolean PR_MST_Contact_DeleteByID(string str, int ContactID, int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Contact_DeleteByID");
                sqlDB.AddInParameter(dbCMD, "ContactID", DbType.Int32, ContactID);
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

        #region PR_MST_Contact_SelectByID

        public DataTable PR_MST_Contact_SelectByID(string str, int? ContactID, int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Contact_SelectByID");
                sqlDB.AddInParameter(dbCMD, "ContactID", DbType.Int32, ContactID);
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

        #region PR_MST_Contact_Insert

        public Boolean PR_MST_Contact_Insert(string str, string ContactName, string ContactMobileNumber , string ContactAddress , string ContactEmail , int CountryID , int StateID ,int CityID  , int ContactCategoryID, string PhotoPath , DateTime? CreationDate, DateTime? ModificationDate,int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Contact_Insert");
                sqlDB.AddInParameter(dbCMD, "ContactName", DbType.String, ContactName);
                sqlDB.AddInParameter(dbCMD, "ContactMobileNumber", DbType.String, ContactMobileNumber);
                sqlDB.AddInParameter(dbCMD, "ContactAddress", DbType.String, ContactAddress);
                sqlDB.AddInParameter(dbCMD, "ContactEmail", DbType.String, ContactEmail);
                sqlDB.AddInParameter(dbCMD, "CountryID", DbType.Int32, CountryID);
                sqlDB.AddInParameter(dbCMD, "StateID", DbType.Int32, StateID);
                sqlDB.AddInParameter(dbCMD, "CityID", DbType.Int32, CityID);
                sqlDB.AddInParameter(dbCMD, "ContactCategoryID", DbType.Int32, ContactCategoryID);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", DbType.String, PhotoPath);
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

        #region PR_MST_Contact_UpdateByID

        public Boolean PR_MST_Contact_UpdateByID(string str, int? ContactID , string ContactName, string ContactMobileNumber , string ContactAddress, string ContactEmail, int CountryID, int StateID, int CityID, int ContactCategoryID, string PhotoPath , DateTime? ModificationDate, int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Contact_UpdateByID");
                sqlDB.AddInParameter(dbCMD, "ContactID", DbType.Int32, ContactID);
                sqlDB.AddInParameter(dbCMD, "ContactName", DbType.String, ContactName);
                sqlDB.AddInParameter(dbCMD, "ContactMobileNumber", DbType.String, ContactMobileNumber);
                sqlDB.AddInParameter(dbCMD, "ContactAddress", DbType.String, ContactAddress);
                sqlDB.AddInParameter(dbCMD, "ContactEmail", DbType.String, ContactEmail);
                sqlDB.AddInParameter(dbCMD, "CountryID", DbType.Int32, CountryID);
                sqlDB.AddInParameter(dbCMD, "StateID", DbType.Int32, StateID);
                sqlDB.AddInParameter(dbCMD, "CityID", DbType.Int32, CityID);
                sqlDB.AddInParameter(dbCMD, "ContactCategoryID", DbType.Int32, ContactCategoryID);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", DbType.String, PhotoPath);
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
