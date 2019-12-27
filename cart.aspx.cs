using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FoodDoAn.vegefoods
{
    public partial class cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["cart"] != null)
                {
                    Repeater1.DataSource = Session["cart"];
                    Repeater1.DataBind();
                    //GridView1.DataSource = Session["cart"];
                    //GridView1.DataBind();
                }
            }
        }
    }
}