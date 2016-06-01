using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ADMIN_FrmReturnBook : System.Web.UI.Page
{
    BAL.clsHome ObjBook = new BAL.clsHome();
    DAL.DAL ObjDal = new DAL.DAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindReturnBook();
        }
    }

    public System.Data.DataTable dtPublisher
    {
        get { return (System.Data.DataTable)ViewState["dtPublisher"]; }
        set { ViewState["dtPublisher"] = value; }
    }

    private void BindReturnBook()
    {
        try
        {
            System.Data.DataTable dt = ObjBook.GetData("select bco.*,mb.BookTitle,mb.BookISBN,mb.PublisherId,Convert(varchar(10),bco.CheckoutDate,103) as CheckoutDate,DATEADD(DAY,20,bco.CheckoutDate) as DueDate,(select AuthorName from MSTAuthor where AuthorID=mb.BookAuthor) as  AuthorName,(select PublisherName from MSTpublisher where Id=mb.PublisherId) as  PublisherName,convert(varchar(100),isnull(mFine.Fine,0)) + ' cent' as Fine from BookCheckOut as bco inner join MSTBook  as mb on bco.BookId=mb.BookId left outer join MSTFine as mFine on mFine.CheckOutId=bco.CheckOutId", null, System.Data.CommandType.Text);

            if (dt != null && dt.Rows.Count > 0)
            {

                GridView1.DataSource = dt;
                GridView1.DataBind();
                dtPublisher = dt;

                GridView1.Visible = true;
                GridView2.Visible = false;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                dtPublisher = null;

                GridView1.Visible = true;
                GridView2.Visible = false;
            }
        }
        catch (Exception ex)
        { }

    }

    private void BindTotalBook()
    {
        try
        {
            System.Data.DataTable dt = ObjBook.GetData("select COUNT(bookId) as BookCount,UserId,Lib_user.libranch,(select LiName from Lib_branch where Libbranch_id=Lib_user.libranch) as BranchName from BookCheckOut" +
                                                       " left outer join Lib_user on Lib_user.Uname = BookCheckOut.UserId group by UserId,libranch", null, System.Data.CommandType.Text);

            if (dt != null && dt.Rows.Count > 0)
            {

                GridView2.DataSource = dt;
                GridView2.DataBind();
                dtPublisher = dt;

                GridView2.Visible = true;
                GridView1.Visible = false;
            }
            else
            {
                GridView2.DataSource = null;
                GridView2.DataBind();
                dtPublisher = null;

                GridView2.Visible = true;
                GridView1.Visible = false;
            }
        }
        catch (Exception ex)
        { }

    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "ReturnBook")
        //{
        //    System.Data.DataRow[] drow = dtPublisher.Select("CheckOutId=" + Convert.ToInt32(e.CommandArgument));
        //    if (drow != null && drow.Length > 0)
        //    {
        //        DAL.DAL.MakeConnection(ConfigurationManager.ConnectionStrings["Library"].ToString());

        //        if (drow[0]["Duedate"] != null && Convert.ToDateTime(drow[0]["Duedate"].ToString()).Date < System.DateTime.Now.Date)
        //        {
                    
        //            ObjDal.SaveData("update BookCheckOut set IsDeleted=1 where CheckOutId=" + Convert.ToInt32(e.CommandArgument) + ";Insert into MSTFine values(" + Convert.ToInt32(e.CommandArgument) + ",1)", null, System.Data.CommandType.Text);

        //        }
        //        else
        //        {
        //            ObjDal.SaveData("Delete from BookCheckOut where CheckoutId=" + Convert.ToInt32(e.CommandArgument) + " ; Delete from MSTFine where CheckoutId=" + Convert.ToInt32(e.CommandArgument), null, System.Data.CommandType.Text);
        //        }
        //    }
        //}
        if (e.CommandName == "ReturnBook")
        {
            System.Data.DataRow[] drow = dtPublisher.Select("CheckOutId=" + Convert.ToInt32(e.CommandArgument));
            if (drow != null && drow.Length > 0)
            {
                DAL.DAL.MakeConnection(ConfigurationManager.ConnectionStrings["Library"].ToString());

                if (drow[0]["Duedate"] != null && Convert.ToDateTime(drow[0]["Duedate"].ToString()).Date < System.DateTime.Now.Date)
                {
                    int cent = 0;

                    TimeSpan difference = System.DateTime.Now.Date - Convert.ToDateTime(drow[0]["Duedate"].ToString()).Date;
                    cent = Convert.ToInt32(difference.Days * 20);

                    ObjDal.SaveData("update BookCheckOut set IsDeleted=1 where CheckOutId=" + Convert.ToInt32(e.CommandArgument) + ";Insert into MSTFine values(" + Convert.ToInt32(e.CommandArgument) + "," + cent + ")", null, System.Data.CommandType.Text);

                }
                else
                {
                    ObjDal.SaveData("Delete from BookCheckOut where CheckoutId=" + Convert.ToInt32(e.CommandArgument) + " ; Delete from MSTFine where CheckoutId=" + Convert.ToInt32(e.CommandArgument), null, System.Data.CommandType.Text);
                }
            }
        }

        else if (e.CommandName == "FinePaid")
        {



            //ObjBook.UndoCheckOut(Convert.ToInt32(e.CommandArgument));

            DAL.DAL.MakeConnection(ConfigurationManager.ConnectionStrings["Library"].ToString());
            ObjDal.SaveData("Delete from BookCheckOut where CheckoutId=" + Convert.ToInt32(e.CommandArgument) + " ; Delete from MSTFine where CheckoutId=" + Convert.ToInt32(e.CommandArgument), null, System.Data.CommandType.Text);


        }
        BindReturnBook();
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void linkCountShow_Click(object sender, EventArgs e)
    {
        BindTotalBook();

        if (linkCountShow.Text == "BorrowBook")
        {
            linkCountShow.Text = "Return";

        }
        else
        {
            BindReturnBook();
            linkCountShow.Text = "BorrowBook";

        }

    }
}