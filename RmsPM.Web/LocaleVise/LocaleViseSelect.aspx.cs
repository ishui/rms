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

public partial class LocaleVise_LocaleViseSelect : System.Web.UI.Page
{
    /// <summary>
    /// 使用方法：
    //  <a href="#" onclick="javascript:SelectVise();return false;">选择签证</a>
    //  <script language="javascript">
    //  function SelectVise()
    //  {
    //     var tmp = OpenCustomDialog("LocaleViseSelect.aspx?ContractCode=[合同编号]&ProjectCode=[项目代码]&ViseCodes=[签证代码],[签证代码]......","","800px","600px");
    //     //参数中ViseCodes格式为([签证代码],[签证代码]....)
    //     //返回格式为 ([签证代码],[签证代码]....) ，清空时返回数据为 ("")。
    //     //取消操作时返回为 ("false")
    //     alert(tmp);
    //  }
    //  </script>
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        GridView2.DataBind();
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            HtmlInputCheckBox cb = ((HtmlInputCheckBox)e.Row.FindControl("CheckBoxSelect"));
            HiddenFieldCheckBoxCodes.Value += cb.ClientID + ",";
            if(Request["ViseCodes"] != null)
            {
                if (Request["ViseCodes"].ToString().IndexOf(cb.Value) > -1)
                    cb.Checked = true;
            }
        }
    }
}
