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


using Rms.ORMap;
using RmsPM;
using RmsPM.DAL;
using RmsPM.DAL.QueryStrategy;
public enum DictionaryItemType { RadioBox, CheckBox };
public partial class UserControls_DictionaryItem : System.Web.UI.UserControl
{
    public static char[] decollator = new char[] { ',' };
    private DictionaryItemType _type=DictionaryItemType.RadioBox;   
    private string _dictionaryName;
    private string _projectCode="";
    private string _defaultValue;
    private string _title;
    private int _repeatColumns = 4;
    public DictionaryItemType Type {get {return _type;}set {_type=value;}}
    public string SelectedValue { 
        get {
            string s = string.Empty;
            foreach (ListItem li in listControl.Items)
            {
                if (li.Selected)
                {
                    if (s.Length > 0)
                    {
                        s = s + (decollator + li.Value);
                    }
                    else { s = li.Value; }
                }
            }
            return s;
        }        
    }
    public string DefaultValue { get { return _defaultValue; } set { _defaultValue = value; } }
    public string ProjectCode { get { return _projectCode; } set { _projectCode = value; } }
    public string DictionaryName { get { return _dictionaryName; } set { _dictionaryName = value; } }
    public string Title { get { if (_title.Length == 0) { return DictionaryName; } else { return _title; } } set { _title = value; } }
    public int RepeatColumns{get{return _repeatColumns;}set{_repeatColumns=value;}}
    public ListControl listControl;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();
        }
        
    }
    private void InitPage()
    {
        try
        {
            Label1.Text = Title;
            RmsPM.DAL.QueryStrategy.DictionaryItemStrategyBuilder ssb = new RmsPM.DAL.QueryStrategy.DictionaryItemStrategyBuilder();
            Rms.LogHelper.LogHelper.Info(DictionaryName);
            ssb.AddStrategy(new Strategy(RmsPM.DAL.QueryStrategy.DictionaryItemStrategyName.DictionaryName, DictionaryName));
            if (ProjectCode != string.Empty)
            {
                ssb.AddStrategy(new Strategy(RmsPM.DAL.QueryStrategy.DictionaryItemStrategyName.ProjectCode, ProjectCode));
            }
            
            ssb.AddOrder("SortID", true);
            string sql = ssb.BuildQueryViewString();
            Rms.ORMap.QueryAgent qa = new QueryAgent();
            DataTable tb = qa.ExecSqlForDataSet(sql).Tables[0];
            qa.Dispose();
        
        
        if(this.Type==DictionaryItemType.RadioBox)
        {
            RadioButtonList rb = new RadioButtonList();
            rb.RepeatDirection=RepeatDirection.Horizontal;
            rb.RepeatColumns=_repeatColumns;
            listControl=rb;
        }
        else{
            CheckBoxList cb=new CheckBoxList();
            cb.RepeatDirection=RepeatDirection.Horizontal;
            cb.RepeatColumns=_repeatColumns;
            listControl=cb;
        }
        this.contentDiv.Controls.Add(listControl);
        listControl.DataSource=tb;
        listControl.DataTextField = "Name";
        listControl.DataValueField = "Name";
        listControl.DataBind();        
        if(this._defaultValue!=string.Empty)
        {
            if(this.Type==DictionaryItemType.CheckBox)
            {
                foreach(string s in _defaultValue.Split(decollator))
                {
                   foreach(ListItem li in listControl.Items)
                {
                    if(li.Value==s)
                    {
                        li.Selected=true;
                        break;
                    }
                } 
                }
            }
            else{
                foreach(ListItem li in listControl.Items)
                {
                    if(li.Value==_defaultValue)
                    {
                        li.Selected=true;
                        break;
                    }
                }
            }

        }
    }
    catch (Exception ex)
    {
        Rms.LogHelper.LogHelper.Info("加载字典选择列表错误。", ex);
        Response.Write(Rms.Web.JavaScript.Alert(true, "加载字典选择列表错误。"));
    }
        
    }
}
