using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ADMIN_frmUserMaster : System.Web.UI.Page
{

    BAL.clsUser ObjUser = new BAL.clsUser();

    public System.Data.DataTable dtPublisher
    {
        get { return (System.Data.DataTable)ViewState["dtPublisher"]; }
        set { ViewState["dtPublisher"] = value; }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBranch();
        }
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        System.Data.DataTable dt = ObjUser.GetData("select * from Lib_user where Uname='" + txtUserName.Text.Trim() + "' And Upswd='" + txtPassword.Text.Trim() + "'", System.Data.CommandType.Text);

        if (dt.Rows.Count > 0)
        {
            lblStatus.Text = "User Already Exits";
        }

        else
        {
            ObjUser.Uid = 0;// Convert.ToInt32(ViewState["BookId"]);
            ObjUser.Uname = txtUserName.Text.Trim();
            ObjUser.Upswd = txtPassword.Text.Trim();

            ObjUser.Uaddress = txtAddress.Text.Trim();
            ObjUser.libranch =Convert.ToInt32(drpBranch.SelectedValue);
            if (chkIsUserAdmin.Checked)
            {
                ObjUser.Uadmin = true;
            }
            else
            {
                ObjUser.Uadmin = false;
            }

            if (ObjUser.SaveUserData() == true)
            {
                lblStatus.Text = "Sucessfully Saved";
            }
            else
            {
                lblStatus.Text = "Failed....";
            }
        }
    }

    private void BindBranch()
    {
        try
        {
            System.Data.DataTable dt = ObjUser.GetData("select Libbranch_id,LiName from Lib_branch where isnull(IsDeleted,0)=0", System.Data.CommandType.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                drpBranch.DataTextField = "LiName";
                drpBranch.DataValueField = "Libbranch_id";
                drpBranch.DataSource = dt;
                dtPublisher = dt;

            }
            else
            {
                drpBranch.DataSource = null;
            }
            drpBranch.DataBind();

        }
        catch (Exception ex)
        {

        }
    }

}