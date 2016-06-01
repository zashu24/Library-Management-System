using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DAL;

namespace BAL
{
   public  class clsBook
    {
        string strConnString =  ConfigurationManager.ConnectionStrings["Library"].ToString();
        DAL.DAL objDal = new DAL.DAL();

        private int _BookId;
        public int BookId
        {
            get { return _BookId; }
            set { _BookId = value; }
        }

        private string _BookTitle;
        public string BookTitle
        {
            get { return _BookTitle; }
            set { _BookTitle = value; }
        }

        private int _Author;
        public int Author
        {
            get { return _Author; }
            set { _Author = value; }
        }

        private string _ISBN;
        public string ISBN
        {
            get { return _ISBN; }
            set { _ISBN = value; }
        }

        private int _PublisherID ;
        public int PublisherID
        {
            get { return _PublisherID; }
            set { _PublisherID = value; }
        }

        private DateTime _PublicationDate;
        public DateTime PublicationDate
        {
            get { return _PublicationDate; }
            set { _PublicationDate = value; }
        }

        private string _CreatedBy;
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }


        public bool SaveBookData()
        {
            try
            {
                if (DAL.DAL.MakeConnection(strConnString))
                {
                  SqlParameter[] param = new SqlParameter[] { };
                  param = new SqlParameter[]
                    {
                        new SqlParameter("@BookId",BookId),
                        new SqlParameter("@BookTitle",BookTitle),
                        new SqlParameter("@BookAuthor",Author),
                        new SqlParameter("@BookISBN",ISBN),
                        new SqlParameter("@PublisherID",PublisherID),
                        new SqlParameter("@BookpublisherDate",PublicationDate),
                        new SqlParameter("@CreatedBy",CreatedBy),
                       
                    };
                return objDal.SaveData("InsertMSTBook", param, System.Data.CommandType.StoredProcedure);

                }
                return false;
                
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public System.Data.DataTable GetData(string Qry, System.Data.CommandType MyType)
        {
            try
            {
                if (DAL.DAL.MakeConnection(strConnString))
                {
                    return objDal.GetTable(Qry,null, MyType);
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

        public void DeleteBook(int ID)
        {
            try
            {
                objDal.SaveData("Update mstBook set IsDeleted=1 where BookId=" + ID, null, System.Data.CommandType.Text);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
