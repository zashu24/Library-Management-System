using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL
{
    public class clsAuthor
    {
        string strConnString = ConfigurationManager.ConnectionStrings["Library"].ToString();
        DAL.DAL objDal = new DAL.DAL();

        private int _Id;
        public int ID
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private string _AuthorName;
        public string AuthorName
        {
            get { return _AuthorName; }
            set { _AuthorName = value; }
        }

        private string _Address;
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

       
        public bool SaveAuthorData()
        {
            try
            {
                if (DAL.DAL.MakeConnection(strConnString))
                {
                    SqlParameter[] param = new SqlParameter[] { };
                    param = new SqlParameter[]
                    {
                        new SqlParameter("@AuthorID",ID),
                        new SqlParameter("@AuthorName",AuthorName),
                        new SqlParameter("@Address",Address),
                      
                        
                    };
                    return objDal.SaveData("InsertMSTAuthor", param, System.Data.CommandType.StoredProcedure);

                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void DeleteAuthor(int ID)
        {
            try
            {
                objDal.SaveData("Update mstAuthor set IsDeleted=1 where AuthorID=" + ID, null, System.Data.CommandType.Text);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
