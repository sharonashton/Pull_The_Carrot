using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class openingPage : System.Web.UI.Page
{
    protected void Page_init(object sender, EventArgs e)
    {
        message.Visible = false;
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void enter_Click(object sender, EventArgs e)
    {
        string myPassword = password.Text;
        string myAdmin = admin.Text;
  

        if (myAdmin == "admin" && myPassword == "telem")
        {

            Page.Response.Write("<script>console.log(' hi ');</script>");
            Response.Redirect("Default.aspx");
        }
        else
        {
            message.Visible = true;

        }

      

    }
}