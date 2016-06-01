using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DAL;

namespace BAL
{
   public class clsUser
    {
        string strConnString = ConfigurationManager.ConnectionStrings["Library"].ToString();
        DAL.DAL objDal = new DAL.DAL();

        private int _Uid;
        public int Uid
        {
            get { return _Uid; }
            set { _Uid = value; }
        }

        private string _Uname;
        public string Uname
        {
            get { return _Uname; }
            set { _Uname = value; }
        }

        private string _Upswd;
        public string Upswd
        {
            get { return _Upswd; }
            set { _Upswd = value; }
        }

        private bool _Uadmin;
        public bool Uadmin
        {
            get { return _Uadmin; }
            set { _Uadmin = value; }
        }

        private string _Uaddress;
        public string Uaddress
        {
            get { return _Uaddress; }
            set { _Uaddress = value; }
        }
        private int _libranch;
        public int libranch
        {
            get { return _libranch; }
            set { _libranch = value; }
        }

      

        private string _CreatedBy;
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }


        public bool SaveUserData()
        {
            try
            {
                if (DAL.DAL.MakeConnection(strConnString))
                {
                    SqlParameter[] param = new SqlParameter[] { };
                    param = new SqlParameter[]
                    {
                        new SqlParameter("@Uid",Uid),
                        new SqlParameter("@Uname",Uname),
                        new SqlParameter("@Upswd",Upswd),
                        new SqlParameter("@Uadmin",Uadmin),
                        new SqlParameter("@Uaddress",Uaddress),
                        new SqlParameter("@libranch",libranch),
                       
                       
                    };
                    return objDal.SaveData("InsertLibUser", param, System.Data.CommandType.StoredProcedure);

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

        public void DeleteUser(int Uid)
        {
            try
            {
                objDal.SaveData("Update Lib_user set IsDeleted=1 where Uid=" + Uid, null, System.Data.CommandType.Text);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
