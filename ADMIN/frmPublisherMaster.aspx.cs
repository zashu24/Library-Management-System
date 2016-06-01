using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data.SqlClient;

public partial class ADMIN_frmPublisherMaster : System.Web.UI.Page
{
    BAL.clsPublisher objPublisher = new BAL.clsPublisher();
    BAL.clsBook objBook = new BAL.clsBook();


    public System.Data.DataTable dtPublisher
    {
        get { return (System.Data.DataTable)ViewState["dtPublisher"]; }
        set { ViewState["dtPublisher"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridView1.Visible = true;
            BindPublisher();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        objPublisher.ID = Convert.ToInt16(ViewState["ID"]);
        objPublisher.PublisherName = txtPublisher.Text.Trim();
        objPublisher.PublisherAddress = txtPuAddress.Text.Trim();
        if (objPublisher.SavePublisherData() == true)
        {
            lblStatus.Text = "Sucessfully Saved";
            txtPuAddress.Text = "";
            txtPublisher.Text = "";
            BindPublisher();
        }
        else
        {
            lblStatus.Text = "Failed....";
        }
    }


    private void BindPublisher()
    {
        try
        {
            System.Data.DataTable dt = objBook.GetData("select ID,PublisherName,PublisherAddress from mstPublisher where isnull(IsDeleted,0)=0", System.Data.CommandType.Text);

            if (dt != null && dt.Rows.Count > 0)
            {

                GridView1.DataSource = dt;
                GridView1.DataBind();
                dtPublisher = dt;
                ViewState["ID"] = 0;
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
                    System.Data.DataRow[] drow = dtPublisher.Select("ID=" + e.CommandArgument);
                    if (drow != null && drow.Length > 0)
                    {
                        txtPublisher.Text = drow[0]["PublisherName"].ToString();
                        txtPuAddress.Text = drow[0]["PublisherAddress"].ToString();
                        ViewState["ID"] = Convert.ToInt16(drow[0]["ID"].ToString());

                    }

                }

            }
            else if (e.CommandName == "Delete")
            {
                objPublisher.DeletePublisher(Convert.ToInt16(e.CommandArgument));
                BindPublisher();
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
