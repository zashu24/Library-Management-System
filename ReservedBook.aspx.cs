using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReservedBook : System.Web.UI.Page
{
    BAL.clsBook ObjBook = new BAL.clsBook();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindReserveBook();
        }
    }


    

    private void Master_mysearch(object sender, CommandEventHandler e)
    {
        

    }

    private void SearchMe(object sender, EventArgs e)
    {

    }

    private void BindReserveBook()
    {
        try
        {
            System.Data.DataTable dt = ObjBook.GetData("select bco.*,mb.BookTitle,mb.BookISBN,mb.PublisherId,Convert(varchar(10),bco.CheckoutDate,103) as CheckoutDate,DATEADD(DAY,20,bco.CheckoutDate) as DueDate,(select AuthorName from MSTAuthor where AuthorID=mb.BookAuthor) as  AuthorName,(select PublisherName from MSTpublisher where Id=mb.PublisherId) as  PublisherName from BookCheckOut as bco inner join MSTBook  as mb on bco.BookId=mb.BookId where bco.UserId='" + Session["UserName"] + "'", System.Data.CommandType.Text);

            if (dt != null && dt.Rows.Count > 0)
            {

                GridView1.DataSource = dt;
                GridView1.DataBind();


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

    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}