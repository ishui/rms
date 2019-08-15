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

public partial class RmsOA_CL_OilList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (user.HasRight("290201"))
            {
                this.NewButton.Visible = true;
                this.NewButton.Attributes["OnClick"] = "javascript:OpenMiddleWindow('CL_OilEdit.aspx?ActType=add','OilEdit');";
            }
            else
            {
                this.NewButton.Visible = false;
            }

        }
    }
    protected void Button1_ServerClick(object sender, EventArgs e)
    {
        this.GridView1.DataBind();
    }


    protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        decimal getNum = 0;
        decimal factMil = 0;
        System.Collections.Generic.List<RmsOA.MODEL.GK_OA_OilModel> lst = (System.Collections.Generic.List<RmsOA.MODEL.GK_OA_OilModel>)e.ReturnValue;
        foreach (RmsOA.MODEL.GK_OA_OilModel Model in lst)
        {

            getNum += Model.GetNum ;
            factMil += Model.FactMil ;
        }
        this.GridView1.Columns[2].FooterText = "合计：" + RmsPM.BLL.MathRule.GetDecimalShowString(getNum);
        this.GridView1.Columns[6].FooterText = "合计：" + RmsPM.BLL.MathRule.GetDecimalShowString(factMil);

    }

    /// <summary>
    /// 获取选中的状态

    /// </summary>
    /// <param name="cbl"></param>
    /// <returns></returns>
    public static string GetListGroupSelectedValues(CheckBoxList cbl)
    {
        string re = "";
        foreach (ListItem li in cbl.Items)
        {
            if (li.Selected)
            {
                if (re != "")
                {
                    re += ",";
                }
                
                re += li.Value;
                
            }
        }
        return re;
    }
}
