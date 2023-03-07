using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace AddressBook.DAL
{
    public class LOC_DALBase
    {
        #region PR_LOC_Country_SelectAll

        public DataTable PR_LOC_Country_SelectAll(string str , int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_SelectAll");
                sqlDB.AddInParameter(dbCMD, "UserID", DbType.Int32, UserID);
                DataTable dt = new DataTable();
                using( IDataReader dr = sqlDB.ExecuteReader(dbCMD))
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
        
        #region PR_LOC_State_SelectAll

        public DataTable PR_LOC_State_SelectAll(string str , int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_SelectAll");
                sqlDB.AddInParameter(dbCMD, "UserID", DbType.Int32, UserID);
                DataTable dt = new DataTable();
                using( IDataReader dr = sqlDB.ExecuteReader(dbCMD))
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
        
        #region PR_LOC_City_SelectAll

        public DataTable PR_LOC_City_SelectAll(string str, int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_SelectAll");
                sqlDB.AddInParameter(dbCMD, "UserID", DbType.Int32, UserID);
                DataTable dt = new DataTable();
                using( IDataReader dr = sqlDB.ExecuteReader(dbCMD))
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

        #region PR_LOC_Country_DeleteByID

        public Boolean PR_LOC_Country_DeleteByID(string str , int CountryID , int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_DeleteByID");
                sqlDB.AddInParameter(dbCMD,"CountryID", DbType.Int32, CountryID);
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

        #region PR_LOC_State_DeleteByID

        public Boolean PR_LOC_State_DeleteByID(string str, int StateID, int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_DeleteByID");
                sqlDB.AddInParameter(dbCMD, "StateID", DbType.Int32, StateID);
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

        #region PR_LOC_City_DeleteByID

        public Boolean PR_LOC_City_DeleteByID(string str, int CityID, int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_DeleteByID");
                sqlDB.AddInParameter(dbCMD, "CityID", DbType.Int32, CityID);
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

        #region PR_LOC_Country_SelectByID

        public DataTable PR_LOC_Country_SelectByID(string str, int? CountryID , int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_SelectByID");
                sqlDB.AddInParameter(dbCMD, "CountryID", DbType.Int32, CountryID);
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

        #region PR_LOC_State_SelectByID

        public DataTable PR_LOC_State_SelectByID(string str, int? StateID, int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_SelectByID");
                sqlDB.AddInParameter(dbCMD, "StateID", DbType.Int32, StateID);
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

        #region PR_LOC_City_SelectByID

        public DataTable PR_LOC_City_SelectByID(string str, int? CityID, int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_SelectByID");
                sqlDB.AddInParameter(dbCMD, "CityID", DbType.Int32, CityID);
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

        #region PR_LOC_Country_Insert

        public Boolean PR_LOC_Country_Insert(string str, string CountryName , string CountryCode , DateTime? CreationDate , DateTime? ModificationDate , int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_Insert");
                sqlDB.AddInParameter(dbCMD, "CountryName", DbType.String, CountryName);
                sqlDB.AddInParameter(dbCMD, "CountryCode", DbType.String, CountryCode);
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

        #region PR_LOC_State_Insert

        public Boolean PR_LOC_State_Insert(string str, string StateName, string StateCode, int CountryID , DateTime? CreationDate, DateTime? ModificationDate,int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_Insert");
                sqlDB.AddInParameter(dbCMD, "StateName", DbType.String, StateName);
                sqlDB.AddInParameter(dbCMD, "StateCode", DbType.String, StateCode);
                sqlDB.AddInParameter(dbCMD, "CountryID", DbType.Int32, CountryID);
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

        #region PR_LOC_City_Insert

        public Boolean PR_LOC_City_Insert(string str, string CityName, string CityCode, int CountryID , int StateID , DateTime? CreationDate, DateTime? ModificationDate,int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_Insert");
                sqlDB.AddInParameter(dbCMD, "CityName", DbType.String, CityName);
                sqlDB.AddInParameter(dbCMD, "CityCode", DbType.String, CityCode);
                sqlDB.AddInParameter(dbCMD, "CountryID", DbType.Int32, CountryID);
                sqlDB.AddInParameter(dbCMD, "StateID", DbType.Int32, StateID);
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

        #region PR_LOC_Country_UpdateByID

        public Boolean PR_LOC_Country_UpdateByID(string str, int? CountryID , string CountryName, string CountryCode, DateTime? ModificationDate, int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_UpdateByID");
                sqlDB.AddInParameter(dbCMD, "CountryID", DbType.Int32, CountryID);
                sqlDB.AddInParameter(dbCMD, "CountryName", DbType.String, CountryName);
                sqlDB.AddInParameter(dbCMD, "CountryCode", DbType.String, CountryCode);
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

        #region PR_LOC_State_UpdateByID

        public Boolean PR_LOC_State_UpdateByID(string str, int? StateID, string StateName, string StateCode, int CountryID , DateTime? ModificationDate, int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_UpdateByID");
                sqlDB.AddInParameter(dbCMD, "StateID", DbType.Int32, StateID);
                sqlDB.AddInParameter(dbCMD, "StateName", DbType.String, StateName);
                sqlDB.AddInParameter(dbCMD, "StateCode", DbType.String, StateCode);
                sqlDB.AddInParameter(dbCMD, "CountryID", DbType.Int32, CountryID);
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

        #region PR_LOC_City_UpdateByID

        public Boolean PR_LOC_City_UpdateByID(string str, int? CityID, string CityName, string CityCode, int CountryID, int StateID , DateTime? ModificationDate, int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_UpdateByID");
                sqlDB.AddInParameter(dbCMD, "CityID", DbType.Int32, CityID);
                sqlDB.AddInParameter(dbCMD, "CityName", DbType.String, CityName);
                sqlDB.AddInParameter(dbCMD, "CityCode", DbType.String, CityCode);
                sqlDB.AddInParameter(dbCMD, "CountryID", DbType.Int32, CountryID);
                sqlDB.AddInParameter(dbCMD, "StateID", DbType.Int32, StateID);
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
