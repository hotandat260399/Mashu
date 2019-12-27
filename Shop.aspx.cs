using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
namespace FoodDoAn.vegefoods
{
    public partial class Shop : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=Rau;Integrated Security=True");
            string sQuery = "Select * from food";
            SqlDataAdapter da = new SqlDataAdapter(sQuery, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            int item = 10;
            int Pages = dt.Rows.Count / item + (dt.Rows.Count % item == 0 ? 0 : 1);
            int page = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int from = (page - 1) * item;
            int to = page * item - 1;
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                if (i < from || i > to)
                {
                    dt.Rows.RemoveAt(i);
                }
            }
            if (IsPostBack == false)
            {
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
            DataTable dtPage = new DataTable();
            dtPage.Columns.Add("index");
            dtPage.Columns.Add("active");
            for (int i = 1; i <= Pages; i++)
            {
                DataRow dr = dtPage.NewRow();
                dr["index"] = i;
                if ((Request["page"] == null && i == 1) || (Request["page"] != null && Convert.ToInt32(Request["page"]) == i))
                    dr["active"] = 1;
                else
                    dr["active"] = 0;
                dtPage.Rows.Add(dr);
            }
            rpt_page.DataSource = dtPage;
            rpt_page.DataBind();
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "add_cart")
            {
                HiddenField hdf_id = (HiddenField)e.Item.FindControl("hdf_id");
                HiddenField hdf_name = (HiddenField)e.Item.FindControl("hdf_name");
                HiddenField hdf_price_promo = (HiddenField)e.Item.FindControl("hdf_price_promo");
                DataTable dt = new DataTable();
                if (Session["cart"] == null)
                {
                    dt.Columns.Add("hdf_id");
                    dt.Columns.Add("hdf_name");
                    dt.Columns.Add("hdf_price_promo");
                    dt.Columns.Add("Quantity");
                    //dt.Columns.Add("total_price");
                    //dt.Columns.Add("thanh_tien");
                }
                else
                {
                    dt = (DataTable)Session["cart"];
                }
                int iRow = checkExist(dt, hdf_id.Value);
                if(iRow!=-1)
                {
                    dt.Rows[iRow]["Quantity"] = Convert.ToInt32(dt.Rows[iRow]["Quantity"]) +1;
                    //dt.Rows[iRow]["total_price"] = Convert.ToInt32(dt.Rows[iRow]["hdf_price_promo"]) + Convert.ToInt32(dt.Rows[iRow]["hdf_price_promo"]);
                    //dt.Rows[iRow]["thanh_tien"] = 0 + Convert.ToInt32(dt.Rows[iRow]["total_price"]);
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    dr["hdf_id"] = hdf_id.Value;
                    dr["hdf_name"] = hdf_name.Value;
                    dr["hdf_price_promo"] = hdf_price_promo.Value;
                    dr["Quantity"] = 1;
                    //dr["total_price"] = hdf_price_promo.Value;
                    //dr["thanh_tien"] = hdf_price_promo.Value;
                    dt.Rows.Add(dr);
                }
                Session["cart"] = dt;
            }

        }
        private int checkExist(DataTable dt, string stt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["hdf_id"].ToString() == stt)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}