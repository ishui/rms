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

public partial class RmsOA_CL_CarReport : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
    }

    protected void Button1_ServerClick(object sender, EventArgs e)
    {
        LoadData();
    }

    private DataTable LoadData()
    {
        string strSql = "select a.Car_Code, a.Car_Id,a.Car_Type,a.Buy_Date,a.Chejia_Id,a.Fadongji_Id,sum(b.Mil) as sumMil,sum(b.MPrice) as sumPrice from GK_OA_Car a join GK_OA_CarMaintenance b on a.Car_Code  = b.Car_code  where 1=1 ";
        

        //车号
        if (this.txtCarID.Text!= "")
        {
            strSql += " and a.Car_Id = '" + this.txtCarID.Text + "'";
        }

        //统计日期
        if (this.dtDateBegin.Value != "")
        {
            strSql += " and b.MDate >='" + DateTime.Parse(this.dtDateBegin.Value) + "'";

        }
        if (this.dtDateEnd.Value != "")
        {
            strSql += " and b.MDate <='" + DateTime.Parse(this.dtDateEnd.Value) + "'";

        }

        strSql += " group by a.Car_Code,a.Car_Id,a.Car_Type,a.Buy_Date,a.Chejia_Id,a.Fadongji_Id";

        DataTable tb;
        QueryAgent qa = new QueryAgent();
        try
        {
            tb = qa.ExecSqlForDataSet(strSql).Tables[0];
            this.GridView1.DataSource = tb;
            this.GridView1.DataBind();
        }
        finally
        {
            qa.Dispose();
        }

        return tb;

    }
}
