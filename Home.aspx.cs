using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home : System.Web.UI.Page
{
    BAL.clsHome ObjBook = new BAL.clsHome();
    public System.Data.DataTable dtPublisher
    {
        get { return (System.Data.DataTable)ViewState["dtPublisher"]; }
        set { ViewState["dtPublisher"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { }
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        BindGrid();
    }


    private string GetWhereCod()
    {
        try
        {
            string wherecondition = "";
            wherecondition = " and (BookTitle like '%" + txtSearch.Text.Trim() + "%' or PublisherName like '%" + txtSearch.Text.Trim() + "%' or BookISBN like '%" + txtSearch.Text.Trim() + "%')";
            return wherecondition;
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
    }


    private void BindGrid()
    {
        try
        {
            SqlParameter[] param = new SqlParameter[] { };
            param = new SqlParameter[]
                    {
                        new SqlParameter("@WhereCond",GetWhereCod()),
                        new SqlParameter("@UserName",Session["UserName"].ToString()),
                        
                    };
            System.Data.DataTable dt = ObjBook.GetData("UspSearchBook1", param, System.Data.CommandType.StoredProcedure);

            if (dt != null && dt.Rows.Count > 0)
            {

                dtPublisher = dt;
                GridView1.DataSource = dt;
                GridView1.DataBind();
                
                ViewState["BookId"] = 0;
                GridView1.Visible = true;
            }
            else
            {
                dtPublisher = dt;
                GridView1.DataSource = dt;
                GridView1.DataBind();

                ViewState["BookId"] = 0;
                GridView1.Visible = true;
            }
        }
        catch (Exception ex)
        { }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "CheckOut")
            {
                ObjBook.BookId = Convert.ToInt32(e.CommandArgument);
                ObjBook.UserID = Session["UserName"].ToString();
                ObjBook.InsertCheckedOut();

            }
            else if (e.CommandName == "UndoCheckOut")
            {
                lblNoBook.Text = "";

                if (ObjBook.UndoCheckOut(Convert.ToInt32(e.CommandArgument)) == false)
                {
                    lblNoBook.Text = "You cannot Undo Check out book ! Please contact your admin person.";
                }
               
            }
            BindGrid();
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (dtPublisher != null && dtPublisher.Rows.Count > 0)
                {
                    bool IsExist = false;
                    System.Data.DataRow[] drow = dtPublisher.Select("BookId=" + e.Row.Cells[0].Text);
                    if (drow != null && drow.Length > 0)
                    {
                        if (drow[0]["UserID"] != null && drow[0]["UserID"].ToString() != "" && drow[0]["UserID"].ToString().ToLower() != Session["UserName"].ToString().ToLower())
                        {
                            e.Row.Font.Strikeout = true;
                            IsExist = true;
                        }

                        if (Convert.ToInt32(drow[0]["CheckoutId"].ToString()) >0)
                        {
                            LinkButton lnk = new LinkButton();
                            lnk = (LinkButton)e.Row.FindControl("LinkButton1");
                            if (IsExist == false)
                            {
                                lnk.Text = "UndoCheckOut";
                                lnk.CommandName = "UndoCheckOut";
                                lnk.CommandArgument = drow[0]["CheckoutId"].ToString();
                            }
                            else
                            {
                                lnk.Text = "Book CheckedOut";
                                lnk.CommandName = "BookCheckedOut";
                                lnk.CommandArgument = drow[0]["CheckoutId"].ToString();
                            }
                            
                        }

                        
                    }
                }
            }
        }
        catch (Exception ex)
        { }
    }
}