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

public partial class SelectBox_SelectViseID : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();
            
        }
        Page.RegisterClientScriptBlock("listitemclick", "<script type=text/javascript> function listitemclick(textid,objItem){var text=document.getElementById(textid);text.value=objItem.value;}</script>");

    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        registerScript(DictionaryItem1,P1.ClientID);
        registerScript(DictionaryItem2, P2.ClientID);
        registerScript(DictionaryItem3, P3.ClientID);
        registerScript(DictionaryItem4, P4.ClientID);
        registerScript(DictionaryItem5, P5.ClientID);
    }

    private void InitPage()
    {
        string projectCode = Request["projectCode"] + string.Empty;
        string _type = HttpUtility.UrlDecode( Request["type"]) + string.Empty;
        
        DictionaryItem1.DictionaryName = _type + "P1";
        DictionaryItem2.DictionaryName = _type + "P2";
        DictionaryItem3.DictionaryName = _type + "P3";
        DictionaryItem4.DictionaryName = _type + "P4";
        DictionaryItem5.DictionaryName = _type + "P5";
        if (projectCode.Length > 0)
        {
            DictionaryItem1.ProjectCode = projectCode;
            DictionaryItem2.ProjectCode = projectCode;
            DictionaryItem3.ProjectCode = projectCode;
            DictionaryItem4.ProjectCode = projectCode;
            DictionaryItem5.ProjectCode = projectCode;
        }
        if (_type == "设计变更内部")
        {
            DictionaryItem4.Visible = false;
            DictionaryItem5.Visible = false;
            P4.Visible = false;
        }
        DictionaryItem1.DefaultValue = Request["p1"] + string.Empty;
        DictionaryItem2.DefaultValue = Request["p2"] + string.Empty;
        DictionaryItem3.DefaultValue = Request["p3"] + string.Empty;
        DictionaryItem4.DefaultValue = Request["p4"] + string.Empty;
        DictionaryItem5.DefaultValue = Request["p5"] + string.Empty;
        P1.Value = Request["p1"] + string.Empty;
        P2.Value = Request["p2"] + string.Empty;
        P3.Value = Request["p3"] + string.Empty;
        P4.Value = Request["p4"] + string.Empty;
        P4.Value = Request["p5"] + string.Empty;
        if (Request["p6"] + string.Empty != string.Empty)
        {
            labID.Text = Request["p6"] + string.Empty;
        }
        
    }
    private void registerScript(UserControls_DictionaryItem dictItems,string textid)
    {
        foreach (ListItem li in dictItems.listControl.Items)
        {
            li.Attributes.Add("onclick", "listitemclick('"+textid+"',this)");
        }
    }
}
