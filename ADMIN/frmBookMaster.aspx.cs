using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmBookMaster : System.Web.UI.Page
{
    BAL.clsBook ObjBook = new BAL.clsBook();

    public System.Data.DataTable dtPublisher
    {
        get { return (System.Data.DataTable)ViewState["dtPublisher"]; }
        set { ViewState["dtPublisher"] = value; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            BindPublisher();
            BindBookMaster();
        }
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        ObjBook.BookId = Convert.ToInt32(ViewState["BookId"]);
        ObjBook.Author = Convert.ToInt32(drpAuthor.SelectedValue);
        ObjBook.BookTitle = txtBookTitle.Text.Trim();
        ObjBook.CreatedBy = "";
        ObjBook.ISBN = txtISBN.Text.Trim();
        ObjBook.PublisherID = Convert.ToInt32(drpPublisher.SelectedValue);
        ObjBook.PublicationDate = System.DateTime.Now.Date;
        if (ObjBook.SaveBookData() == true)
        {
            lblStatus.Text = "Sucessfully Saved";
            BindBookMaster();
        }
        else
        {
            lblStatus.Text = "Failed....";
        }

    }

    private void BindBookMaster()
    {
        try
        {
            System.Data.DataTable dt = ObjBook.GetData("select *,(select AuthorName from MSTAuthor where AuthorID=MSTBook.BookAuthor) as AuthorName,(select PublisherName from MSTpublisher where Id=MSTBook.PublisherId) as PublisherName from MSTBook where isnull(IsDeleted,0)=0", System.Data.CommandType.Text);

            if (dt != null && dt.Rows.Count > 0)
            {

                GridView1.DataSource = dt;
                GridView1.DataBind();
                dtPublisher = dt;
                ViewState["BookId"] = 0;
                GridView1.Visible = true;
            }
            else
            {

            }
        }
        catch (Exception ex)
        { }

    }

    private void BindPublisher()
    {
        try
        {
            System.Data.DataTable dt = ObjBook.GetData("select ID,PublisherName from mstPublisher where isnull(IsDeleted,0)=0", System.Data.CommandType.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                drpPublisher.DataTextField = "PublisherName";
                drpPublisher.DataValueField = "ID";
                drpPublisher.DataSource = dt;
                dtPublisher = dt;

            }
            else
            {
                drpPublisher.DataSource = null;
            }
            drpPublisher.DataBind();



            System.Data.DataTable dtAuthor = ObjBook.GetData("select AuthorID,AuthorName from MstAuthor where isnull(IsDeleted,0)=0", System.Data.CommandType.Text);
            if (dtAuthor != null && dtAuthor.Rows.Count > 0)
            {
                drpAuthor.DataTextField = "AuthorName";
                drpAuthor.DataValueField = "AuthorID";
                drpAuthor.DataSource = dtAuthor;

            }
            else
            {
                drpAuthor.DataSource = null;
            }
            drpAuthor.DataBind();


        }
        catch (Exception ex)
        {

        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Edit")
            {
                if (dtPublisher != null && dtPublisher.Rows.Count > 0)
                {
                    System.Data.DataRow[] drow = dtPublisher.Select("BookId=" + e.CommandArgument);
                    if (drow != null && drow.Length > 0)
                    {
                        txtBookTitle.Text = drow[0]["BookTitle"].ToString();
                        drpAuthor.SelectedValue = drow[0]["BookAuthor"].ToString();
                        txtISBN.Text = drow[0]["BookISBN"].ToString();
                        drpPublisher.SelectedValue = drow[0]["PublisherId"].ToString();
                        txtPublishedDate.Text = drow[0]["BookpublisherDate"].ToString();

                        ViewState["BookId"] = Convert.ToInt16(drow[0]["BookId"].ToString());

                    }

                }

            }
            else if (e.CommandName == "Delete")
            {
                ObjBook.DeleteBook(Convert.ToInt16(e.CommandArgument));
                BindPublisher();
                BindBookMaster();
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