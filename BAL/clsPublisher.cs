using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DAL;

namespace BAL
{
   public class clsPublisher
    {

        string strConnString = ConfigurationManager.ConnectionStrings["Library"].ToString();
        DAL.DAL objDal = new DAL.DAL();

        private int _Id;
        public int ID
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private string _PublisherName;
        public string PublisherName
        {
            get { return _PublisherName; }
            set { _PublisherName = value; }
        }

        private string _PublisherAddress;
        public string PublisherAddress
        {
            get { return _PublisherAddress; }
            set { _PublisherAddress = value; }
        }

        public bool SavePublisherData()
        {
            try
            {
                if (DAL.DAL.MakeConnection(strConnString))
                {
                    SqlParameter[] param = new SqlParameter[] { };
                    param = new SqlParameter[]
                    {
                        new SqlParameter("@Id",ID),
                        new SqlParameter("@PublisherName",PublisherName),
                        new SqlParameter("@PublisherAddress",PublisherAddress),
                        
                    };
                    return objDal.SaveData("InsertMSTPublisher", param, System.Data.CommandType.StoredProcedure);

                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void DeletePublisher(int ID)
        {
            try
            {
                objDal.SaveData("Update mstPublisher set IsDeleted=1 where ID=" + ID, null, System.Data.CommandType.Text);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
