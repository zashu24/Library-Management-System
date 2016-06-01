using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ADMIN_frmAuthorMaster : System.Web.UI.Page
{
    BAL.clsAuthor objPublisher = new BAL.clsAuthor();
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
        objPublisher.ID = Convert.ToInt16(ViewState["AuthorID"]);
        objPublisher.AuthorName = txtAuthor.Text.Trim();
        objPublisher.Address = txtAuthorAdd.Text.Trim();

        if (objPublisher.SaveAuthorData() == true)
        {
            lblStatus.Text = "Sucessfully Saved";
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
            System.Data.DataTable dt = objBook.GetData("select AuthorID,AuthorName,Address from MSTAuthor where isnull(IsDeleted,0)=0", System.Data.CommandType.Text);

            if (dt != null && dt.Rows.Count > 0)
            {

                GridView1.DataSource = dt;
                GridView1.DataBind();
                dtPublisher = dt;
                ViewState["AuthorID"] = 0;
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
                    System.Data.DataRow[] drow = dtPublisher.Select("AuthorID=" + e.CommandArgument);
                    if (drow != null && drow.Length > 0)
                    {
                        txtAuthor.Text = drow[0]["AuthorName"].ToString();
                        txtAuthorAdd.Text = drow[0]["Address"].ToString();
                        ViewState["AuthorID"] = Convert.ToInt16(drow[0]["AuthorID"].ToString());

                    }

                }

            }
            else if (e.CommandName == "Delete")
            {
                objPublisher.DeleteAuthor(Convert.ToInt16(e.CommandArgument));
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