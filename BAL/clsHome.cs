using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL
{
   public class clsHome
    {
        string strConnString = ConfigurationManager.ConnectionStrings["Library"].ToString();
        DAL.DAL objDal = new DAL.DAL();

        private int _BookId;
        public int BookId
        {
            get { return _BookId; }
            set { _BookId = value; }
        }

        private string _UserID;
        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }


        public System.Data.DataTable GetData(string Qry,SqlParameter[] param, System.Data.CommandType MyType)
        {

            try
            {
                if (DAL.DAL.MakeConnection(strConnString))
                {
                    return objDal.GetTable(Qry, param, MyType);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool InsertCheckedOut()
        {
            try
            {
                if (DAL.DAL.MakeConnection(strConnString))
                {
                    SqlParameter[] param = new SqlParameter[] { };
                    param = new SqlParameter[]
                    {
                        new SqlParameter("@BookID",BookId),
                        new SqlParameter("@UserID",UserID),
                        new SqlParameter("@CheckoutDate",System.DateTime.Now),
                      
                        
                    };
                    return objDal.SaveData("InsertBookCheckOut", param, System.Data.CommandType.StoredProcedure);

                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UndoCheckOut(int ID)
        {
            try
            {
                if (DAL.DAL.MakeConnection(strConnString))
                {
                    System.Data.DataTable dtData = objDal.GetTable("select CheckoutId from BookCheckOut where CheckoutId = " + ID + " and Convert(varchar(10),CheckoutDate,101)=Convert(varchar(10),getdate(),101)", null, System.Data.CommandType.Text);
                    if (dtData != null && dtData.Rows.Count > 0)
                    {

                        return objDal.SaveData("Delete from BookCheckOut where CheckoutId=" + ID, null, System.Data.CommandType.Text);
                    }
                    else
                    {
                        return false;
                    }

                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
