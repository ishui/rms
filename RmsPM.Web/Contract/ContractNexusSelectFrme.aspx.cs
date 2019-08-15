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

public partial class Contract_ContractNexusSelectFrme : System.Web.UI.Page
{
    /// <summary>
    /// 使用方法：
    //  <a href="#" onclick="javascript:SelectNexus();return false;">选择相关单据</a>
    //  <script language="javascript">
    //  function SelectNexus()
    //  {
    //     var tmp = OpenCustomDialog("../Contract/ContractNexusSelectFrme.aspx?ContractCode=[合同编号]&ProjectCode=[项目代码]&NexusCodes=[签证代码,签证类型];[设计变更代码,设计变更类型];...","","800px","600px");
    //     //参数中NexusCodes格式为([签证代码,签证类型];[设计变更代码,设计变更类型];...)(签证类型描述为 "Vise")
    //     //返回格式为 ([签证代码,签证类型];[设计变更代码,设计变更类型];...) ，清空时返回数据为 ("")。
    //     //取消操作时返回为 ("undefined")
    //     alert(tmp);
    //  }
    //  </script>
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}

