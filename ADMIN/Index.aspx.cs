using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ADMIN_Index : System.Web.UI.Page
{
    BAL.clsUser ObjUser = new BAL.clsUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {

        }
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        System.Data.DataTable dt = ObjUser.GetData("select * from Lib_user where Uname='" + txtUserName.Text.Trim() + "' And Upswd='" + txtPassword.Text.Trim() + "'", System.Data.CommandType.Text);

        if (dt.Rows.Count > 0)
        {
            Session["UserId"] = dt.Rows[0][0];
            Session["UserName"] = txtUserName.Text;

            if (Convert.ToInt32(dt.Rows[0][3]) == 0)
            {
                Response.Redirect("~/Home.aspx");
            }
            else
            {
                Response.Redirect("~/ADMIN/HomeAdmin.aspx");
            }


        }
        else
        {
            lblStatus.Text = "Login Failed...";

        }

    }





}