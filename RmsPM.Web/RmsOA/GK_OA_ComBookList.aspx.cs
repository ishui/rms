using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RmsPM.Web;
using Rms.ORMap;

public partial class RmsOA_GK_OA_ComBookList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (user.HasRight("350301"))
            {
                this.NewButton.Visible = true;
                this.NewButton.Attributes["OnClick"] = "javascript:OpenMiddleWindow('GK_OA_ComBookEdit.aspx?ActType=add','ComBookEdit');";
            }
            else
            {
                this.NewButton.Visible = false;
            }

            string strUnitCode = Request["UnitCode"];
            string strSql="";
            if (strUnitCode != "")
            {
                strSql = "Select * from GK_OA_ComBook where UnitCode ='" + strUnitCode + "'";
            }
            else
            {
                strSql = "Select * from GK_OA_ComBook";
            }
            QueryAgent qa = new QueryAgent();
            DataTable tbUnit = qa.ExecSqlForDataSet(strSql).Tables[0];
            this.GridView1.DataSource = tbUnit.DefaultView;
            this.GridView1.DataBind();
        }
    }
   
}
