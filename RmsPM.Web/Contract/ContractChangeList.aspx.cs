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
using RmsPM.Web;
using RmsPM.BLL;

public partial class Contract_ContractChangeList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDataGrid();
        }
    }

    private void LoadDataGrid()
    {
        QueryAgent qa = new QueryAgent();

        string strCondition = "select a.ContractCode,a.ContractID,a.ContractName,a.ProjectCode ,b.* from Contract a , ContractChange b    where a.ContractCode=b.ContractCode ";
        string ud_sProjectCode = Request["ProjectCode"] + "";
        if (ud_sProjectCode != "")
        {
            strCondition += " and a.ProjectCode ='" + ud_sProjectCode + "'";
        }

        if (txtContractID.Value != "")
        {
            strCondition += " and a.ContractCode ='" + txtContractID.Value + "'";
        }
        if (txtContractChangeCode.Value != "")
        {
            strCondition += " and b.ContractChangeCode ='" + txtContractChangeCode.Value + "'";
        }

        string ud_sChangeStatus = RmsPM.BLL.PageFacade.GetListGroupSelectedValues(this.cblChangeStatus);
        if (ud_sChangeStatus != "")
        {
            strCondition += " and b.status in (" + ud_sChangeStatus + ")";
        }


        if (txtChangePerson.Value != "")
        {
            strCondition += " and b.ChangePerson ='" + txtChangePerson.Value + "'";
        }

        if (txtContractChangeId.Value != "")
        {
            strCondition += " and b.ContractChangeId ='" + txtContractChangeId.Value + "'";
        }

        if (dtChangeDate0.Value != "")
        {
            strCondition += " and b.ChangeDate >='" + dtChangeDate0.Value + "'";
        }

        if (dtChangeDate1.Value != "")
        {
            strCondition += " and b.ChangeDate <='" + dtChangeDate1.Value + "'";
        }

#region   排序
        string sortsql = RmsPM.BLL.GridSort.GetSortSQL(ViewState, "ChangeDate DESC ");
        string temp = "";
        if (sortsql != "")
        {
            //点列标题排序
            string[] sortsqls;
            if (sortsql.Contains(","))
            {
                sortsqls = sortsql.Split(new char[] { ',' });
                for (int i = 0; i <= sortsqls.Length - 1; i++)
                {
                    if (sortsqls[i].Contains("ContractName"))
                    {
                        sortsqls[i] = "a." + sortsqls[i];
                    }
                    if (sortsqls[i].Contains("ContractID"))
                    {
                        sortsqls[i] = "a." + sortsqls[i];
                    }
                    if (sortsqls[i].Contains("ChangeMoney"))
                    {
                        sortsqls[i] = "b." + sortsqls[i];
                    }
                    if (sortsqls[i].Contains("ChangeDate"))
                    {
                        sortsqls[i] = "b." + sortsqls[i];
                    }
                    if (sortsqls[i].Contains("ChangePerson"))
                    {
                        sortsqls[i] = "b." + sortsqls[i];
                    }
                }
                for (int i = 0; i <= sortsqls.Length - 1; i++)
                {
                    temp += sortsqls[i] + ",";
                }

                temp = temp.Substring(0, temp.Length - 1);
            }
            else
            {
                if (sortsql.Contains("ContractName"))
                {
                    sortsql = "a." + sortsql;
                }
                if (sortsql.Contains("ContractID"))
                {
                    sortsql = "a." + sortsql;
                }
                if (sortsql.Contains("ChangeMoney"))
                {
                    sortsql = "b." + sortsql;
                }
                if (sortsql.Contains("ChangeDate"))
                {
                    sortsql = "b." + sortsql;
                }
                if (sortsql.Contains("ChangePerson"))
                {
                    sortsql = "b." + sortsql;
                }
                temp = sortsql;
            }

            strCondition = strCondition + " order by " + temp;
        }
#endregion 

        DataTable dt = qa.ExecSqlForDataSet(strCondition).Tables[0];

        string[] arrField = { "ChangeMoney" };
        decimal[] arrSum = RmsPM.BLL.MathRule.SumColumn(dt, arrField);
        ViewState["SumChangeMoney"] = arrSum[0];


        this.dgList.DataSource = dt;
        this.dgList.DataBind();
        this.GridPagination1.RowsCount = dt.Rows.Count.ToString();



    }

    protected void btnSearch_ServerClick(object sender, System.EventArgs e)
    {
        this.dgList.CurrentPageIndex = 0;
        LoadDataGrid();
    }


    protected void GridPagination1_PageIndexChange(object sender, System.EventArgs e)
    {
        try
        {
            this.LoadDataGrid();
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
        }
    }

    protected void dgChangeList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        switch (e.Item.ItemType)
        {
            case ListItemType.Item:
            case ListItemType.AlternatingItem:
                DataRowView ud_drvItem = (DataRowView)e.Item.DataItem;

                Label lblAuditedTotalChangeMoney = (Label)e.Item.FindControl("lblAuditedTotalChangeMoney");

                System.Web.UI.HtmlControls.HtmlAnchor Alink = e.Item.FindControl("ALink") as System.Web.UI.HtmlControls.HtmlAnchor;

                //if (ud_drvItem["changetype"].ToString() == "结算")
                //{
                //    Alink.Style.Add("color", "red");
                //}
                if (ud_drvItem["Status"].ToString() == "0")
                {
                    lblAuditedTotalChangeMoney.Text = (RmsPM.BLL.ConvertRule.ToDecimal(ud_drvItem["TotalChangeMoney"]) + RmsPM.BLL.ConvertRule.ToDecimal(ud_drvItem["ChangeMoney"])).ToString("N");
                }
                else
                {
                    lblAuditedTotalChangeMoney.Text = "";
                }
                break;
            case ListItemType.Footer:
                ((Label)e.Item.FindControl("lblChangeMoney")).Text = "共：" + RmsPM.BLL.MathRule.GetDecimalShowString(ViewState["SumChangeMoney"]);

                break;
        }
    }

    protected void dgList_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
    {
        try
        {
            RmsPM.BLL.GridSort.SortCommand((DataGrid)source, ViewState, source, e);
            ((DataGrid)source).CurrentPageIndex = 0;
            LoadDataGrid();
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
        }
    }

}
