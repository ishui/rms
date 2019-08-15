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


using System.Collections.Generic;
using Rms.ORMap;
using RmsPM.Web;
using RmsPM.BLL;
using RmsPM.DAL;

public partial class IncomeBudget_IncomeBudget : System.Web.UI.Page
{
    #region "事件"
    protected void Page_Load(object sender, EventArgs e)
    {
        ProjectCode = Request.QueryString["ProjectCode"];
        if (!Page.IsPostBack)
        {
            Year = DateTime.Now.Year;
            ddlYear.SelectedValue = Year.ToString();
            MVContainer.ActiveViewIndex = 0;
            GridViewObjectDataSource.SelectParameters["year"].DefaultValue = Year.ToString();
            GridViewObjectDataSource.SelectParameters["projectCode"].DefaultValue = ProjectCode;
            this.btnAddYearIncome.Enabled = CanEdit;
        }
    }
    protected void btnAddYearIncome_Click(object sender, EventArgs e)
    {
        this.MVContainer.ActiveViewIndex = 1;
        YearGridViewObjectDataSource.SelectParameters["year"].DefaultValue = Year.ToString();
        YearGridViewObjectDataSource.SelectParameters["projectCode"].DefaultValue = ProjectCode;
        MonthGridView.DataBind();
    }
    protected void AddYearPlan_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvRow in YearGridView.Rows)
        {
            IncomeBugetFacade facade = new IncomeBugetFacade();
            IncomeBugetModel model = null;
            TextBox tbcMoney = (TextBox)(gvRow.FindControl("tbxMoney"));
            Label lblMonth = (Label)(gvRow.FindControl("lblMonth"));
            if (!string.IsNullOrEmpty(tbcMoney.Text))
            {
                if (int.Parse(tbcMoney.Text) > 0)
                {
                    model = new IncomeBugetModel();
                    model.Year = Year;
                    model.Month = int.Parse(lblMonth.Text);
                    model.ProjectCode = ProjectCode;
                    model.Money = decimal.Parse(tbcMoney.Text);
                    facade.Insert(model);
                }
            }
        }
        GridViewObjectDataSource.SelectParameters["year"].DefaultValue = Year.ToString();
        GridViewObjectDataSource.SelectParameters["projectCode"].DefaultValue = ProjectCode;
        MonthGridView.DataBind();
        this.MVContainer.ActiveViewIndex = 0;

    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Year = int.Parse(ddlYear.SelectedValue);
        GridViewObjectDataSource.SelectParameters["year"].DefaultValue = Year.ToString();
        GridViewObjectDataSource.SelectParameters["projectCode"].DefaultValue = ProjectCode;
        MonthGridView.DataBind();
        btnAddYearIncome.Enabled = CanEdit;
    }

    protected void MonthGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.RowIndex % 3) == 0)
            {
                e.Row.Cells[0].RowSpan = 3;
                e.Row.Cells[0].Text = GetRowText(e.Row.RowIndex/3);
            }
            else
            {
                e.Row.Cells[0].Visible = false;
            }
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                HiddenField hidSystemID = (HiddenField)e.Row.Cells[2].FindControl("hidSystemID");
                LinkButton editButton = (LinkButton)e.Row.Cells[3].FindControl("btnEditGroup");
                LinkButton delButton = (LinkButton)e.Row.Cells[3].FindControl("btnDelGroup");
                if (string.IsNullOrEmpty(hidSystemID.Value))
                {
                    editButton.Text = "添加";
                    delButton.Visible = false;
                }
                else
                {
                    editButton.Text = "编辑";
                }
            }
        }
    }

    protected void YearGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.RowIndex % 3) == 0)
            {
                e.Row.Cells[0].RowSpan = 3;
                e.Row.Cells[0].Text = GetRowText(e.Row.RowIndex / 3);
            }
            else
            {
                e.Row.Cells[0].Visible = false;
            }
        }
    }
    #endregion

    #region "函数"

    public string GetRowText(int index)
    {
        string tempStr = string.Empty;
        switch (index)
        {
            case 0 :
                tempStr = QuarterMessage.Spring;
                break;
            case 1:
                tempStr = QuarterMessage.Summer;
                break;
            case 2:
                tempStr = QuarterMessage.Autumn;
                break;
            case 3:
                tempStr = QuarterMessage.Winter;
                break;
            default :
                break;
        }
        return tempStr;
    }

    public void Insert(IncomeBugetModel ibModel)
    {
      
    }

    public void UpDate(IncomeBugetModel ibModel)
    { }

    public void Delete(int id)
    {

    }

    public void Select(int year, string projectCode)
    {
        string sql = "SELECT * FROM RPTFININ WHERE IYEAR={0} AND PROJECTCODE='{1}' ORDER BY IMONTH ASC ";
        QueryAgent qa = new QueryAgent();
        EntityData entity = qa.FillEntityData("RptFinIn", string.Format(sql,year,projectCode));
        qa.Dispose();
        this.MonthGridView.DataSource = entity.CurrentTable;
        MonthGridView.DataBind();
    }

    public void Select(string id)
    { }

    #endregion

    #region "变量"
    private EntityData entity;
    private string projectCode;
    private bool canEdit;
    private const string year = "Year"; 
    #endregion

    #region "属性"

    /// <summary>
    /// 计划日期
    /// </summary>
    public int Year
    {
        get 
        {
            if (!string.IsNullOrEmpty(ViewState[year].ToString()))
            {
                return int.Parse(ViewState[year].ToString());
            }
            else
            {
                return DateTime.Now.Year;
            }
        }
        set { ViewState[year] = value; }
    }
    public bool CanEdit
    {
        get {
            IncomeBugetFacade facade = new IncomeBugetFacade();
            int? count = facade.Select(Year,ProjectCode,"");
            if (count == null || count == 0)
            {
                canEdit = true;
            }
            else
            {
                canEdit = false;
            }
            return canEdit;
        }
        set { canEdit = true; }
    }

    /// <summary>
    /// 项目编号
    /// </summary>
    public string ProjectCode
    {
        get
        {
            return projectCode;
        }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("未知项目");
            }
            else
            {
                projectCode = value;
            }
        }
    }
    #endregion
}
