using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _111_1QZ3
{
    public partial class Test_Sub : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] s_Get = new string[] { "tb_Num", "rbl_Phone", "txt_Phone", "dpl_City", "dpl_Area" };
            string[] s_Menu = new string[] { "保單號碼: ", "通訊種類: ", "通訊號碼: ", "所在城市: ", "所在區域: " };
            for (int i_Ct = 0; i_Ct < s_Get.Length; i_Ct++)
            {
                if (Request.Form.Get(s_Get[i_Ct]) == null)
                {
                    continue;
                }
                lb_Msg.Text += s_Menu[i_Ct] + Request.Form.Get(s_Get[i_Ct]) + "<br />";
                Response.Write(Request.Form.Get(s_Get[i_Ct]));
            }
        }
    }
}