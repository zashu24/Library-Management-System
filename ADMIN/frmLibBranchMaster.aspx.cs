using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ADMIN_frmLibBranchMaster : System.Web.UI.Page
{
    BAL.clsLibBranch ObjclsLibBranch = new BAL.clsLibBranch();

    public System.Data.DataTable dtPublisher
    {
        get { return (System.Data.DataTable)ViewState["dtPublisher"]; }
        set { ViewState["dtPublisher"] = value; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            BindBranch();
        }
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        ObjclsLibBranch.Libbranch_id = Convert.ToInt32(ViewState["Libbranch_id"]);
        ObjclsLibBranch.LiName = txtLibName.Text.Trim();
        ObjclsLibBranch.Liaddress = txtAddress.Text.Trim();
        ObjclsLibBranch.LiPhone = txtPhone.Text.Trim();
        ObjclsLibBranch.CreatedBy = "";


        if (ObjclsLibBranch.SaveBranchData() == true)
        {
            lblStatus.Text = "Sucessfully Saved";
            BindBranch();
        }
        else
        {
            lblStatus.Text = "Failed....";
        }
    }


    private void BindBranch()
    {
        try
        {
            System.Data.DataTable dt = ObjclsLibBranch.GetData("select Libbranch_id,LiName,Liaddress,LiPhone from Lib_branch where isnull(IsDeleted,0)=0", System.Data.CommandType.Text);

            if (dt != null && dt.Rows.Count > 0)
            {

                GridView1.DataSource = dt;
                GridView1.DataBind();
                dtPublisher = dt;
                ViewState["Libbranch_id"] = 0;
                GridView1.Visible = true;
            }
            else
            {

            }
        }
        catch (Exception ex)
        { }

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Edit")
            {
                if (dtPublisher != null && dtPublisher.Rows.Count > 0)
                {
                    System.Data.DataRow[] drow = dtPublisher.Select("Libbranch_id=" + e.CommandArgument);
                    if (drow != null && drow.Length > 0)
                    {
                        txtLibName.Text = drow[0]["LiName"].ToString();
                        txtAddress.Text = drow[0]["Liaddress"].ToString();
                        txtPhone.Text = drow[0]["LiPhone"].ToString();
                        ViewState["Libbranch_id"] = Convert.ToInt16(drow[0]["Libbranch_id"].ToString());

                    }

                }

            }
            else if (e.CommandName == "Delete")
            {
                ObjclsLibBranch.DeleteLibBranch(Convert.ToInt16(e.CommandArgument));
                BindBranch();
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }


}