using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAL
{
    public class DAL
    {
       static SqlConnection con;
        public static bool MakeConnection(string conStr)
        {
            try
            {
                con = new SqlConnection(conStr);
                con.Open();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataTable GetTable(string Qry,SqlParameter[] param, CommandType ExecType)
        {
            try
            {
                DataTable dt = new DataTable();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = Qry;
                cmd.CommandType = ExecType;
                cmd.Connection = con;

                if (param != null && param.Length > 0)
                {
                    foreach (SqlParameter newparam in param)
                    {
                        cmd.Parameters.Add(newparam);
                    }
                }

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    var tb = new DataTable();
                    tb.Load(dr);
                    return tb;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public bool SaveData(string Qry, SqlParameter[] param, CommandType ExecType)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = Qry;
                cmd.CommandType = ExecType;
                cmd.Connection = con;
                if (param != null && param.Length > 0)
                {
                    foreach (SqlParameter newparam in param)
                    {
                        cmd.Parameters.Add(newparam);
                    }
                }
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
