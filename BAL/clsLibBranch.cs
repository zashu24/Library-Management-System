using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DAL;

namespace BAL
{
   public class clsLibBranch
    {
        string strConnString = ConfigurationManager.ConnectionStrings["Library"].ToString();
        DAL.DAL objDal = new DAL.DAL();

        private int _Libbranch_id;
        public int Libbranch_id
        {
            get { return _Libbranch_id; }
            set { _Libbranch_id = value; }
        }

        private string _LiName;
        public string LiName
        {
            get { return _LiName; }
            set { _LiName = value; }
        }

        private string _Liaddress;
        public string Liaddress
        {
            get { return _Liaddress; }
            set { _Liaddress = value; }
        }

        private string _LiPhone;
        public string LiPhone
        {
            get { return _LiPhone; }
            set { _LiPhone = value; }
        }
           

       
        private string _CreatedBy;
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }


        public bool SaveBranchData()
        {
            try
            {
                if (DAL.DAL.MakeConnection(strConnString))
                {
                    SqlParameter[] param = new SqlParameter[] { };
                    param = new SqlParameter[]
                    {
                        new SqlParameter("@Libbranch_id",Libbranch_id),
                        new SqlParameter("@LiName",LiName),
                        new SqlParameter("@Liaddress",Liaddress),
                        new SqlParameter("@LiPhone",LiPhone),
                        new SqlParameter("@CreatedBy",CreatedBy),
                       
                    };
                    return objDal.SaveData("InsertLib_branch", param, System.Data.CommandType.StoredProcedure);

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

        public void DeleteLibBranch(int Libbranch_id)
        {
            try
            {
                objDal.SaveData("Update Lib_branch set IsDeleted=1 where Libbranch_id=" + Libbranch_id, null, System.Data.CommandType.Text);
            }
            catch (Exception ex)
            {

            }
        }


    }
}
