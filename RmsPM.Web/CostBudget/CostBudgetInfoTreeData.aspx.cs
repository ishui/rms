using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rms.ORMap;
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using Rms.Web;

namespace RmsPM.Web.CostBudget
{
	/// <summary>
	/// CostBudgetInfoTreeData 的摘要说明。
	/// </summary>
	public partial class CostBudgetInfoTreeData : PageBase
	{
		/// <summary>
		/// 金额字段
		/// </summary>
		private string[] m_arrMoneyField = {"BudgetMoney", "BudgetChangeMoney", "ContractTotalMoney", "ContractMoney", "ContractChangeMoney", "ContractApplyMoney", "ContractBudgetBalance", "ContractPay1", "ContractPayReal", "ContractPayBalance", "BuildingPrice", "HousePrice", "ContractPayPercent"};
        private string[] m_arrMoneyFieldOriginal = { "BudgetMoney", "BudgetChangeMoney", "ContractTotalMoney", "ContractMoney", "ContractChangeMoney", "ContractApplyMoney", "ContractBudgetBalance", "ContractPay", "ContractPayReal", "ContractPayBalance", "BuildingPrice", "HousePrice", "ContractPayPercent"};

		private decimal[] m_arrSum = new decimal[10];

		private string m_ProjectCode = "";
		private string m_CostBudgetSetCode = "";
        private string m_CostBudgetBackupSetCode = "";

        private string m_strGetType = "";
        private int m_Layer = 0;

		protected void Page_Load(object sender, System.EventArgs e)
		{
            DataTable tbDst = null;

			try
			{
				m_ProjectCode = Request["ProjectCode"] + "";
				m_CostBudgetSetCode = Request["CostBudgetSetCode"] + "";
                m_CostBudgetBackupSetCode = Request["CostBudgetBackupSetCode"] + "";
                m_Layer = BLL.ConvertRule.ToInt(Request["Layer"]);

				m_strGetType=Request.QueryString["GetType"]+"";				//数据分类
				string m_strNodeId=Request.QueryString["NodeId"]+"";				//父节点编号
				string[] m_Layers=(Request.QueryString["Layers"]+"").Split('.');	//定点展开的序列
				string m_strSelectedLayer=Request.QueryString["SelectedLayer"]+"";	//定层展开的深度

				tbDst = new DataTable("CostBudgetInfo");

                tbDst.Columns.Add("CostBudgetDtlCode");
                tbDst.Columns.Add("ParentCode");
                tbDst.Columns.Add("CostCode");
                tbDst.Columns.Add("SortID");
                tbDst.Columns.Add("CostName");
                tbDst.Columns.Add("Deep");
                tbDst.Columns.Add("ChildNodesCount");

                tbDst.Columns.Add("ClassTd");

                tbDst.Columns.Add("ContractCode");

                tbDst.Columns.Add("ContractIDHtml");
                tbDst.Columns.Add("ContractNameHtml");
                tbDst.Columns.Add("SupplierNameHtml");
                tbDst.Columns.Add("DescriptionHtml");

                tbDst.Columns.Add("Layer");
                tbDst.Columns.Add("ShowChildNodes");

                tbDst.Columns.Add("ShowSpan");
                tbDst.Columns.Add("ShowHref");

				//金额字段
				foreach(string MoneyField in this.m_arrMoneyField)
				{
                    tbDst.Columns.Add(MoneyField);
                
                    //金额提示
                    tbDst.Columns.Add("Title" + MoneyField);
                }

                //金额链接
                tbDst.Columns.Add("HtmlContractPay1");
                tbDst.Columns.Add("HtmlContractPayReal");

                BLL.CostBudgetDynamic dyn = GetDynamic();

                DataTable tbSrc = dyn.tbHtml;

				string filter = "";
				if(m_strGetType=="")
				{
					#region 取第一层

                    filter = "Deep=1";

//					DateTime t2 = DateTime.Now;
//					TimeSpan t = t2.Subtract(t1);
//					ApplicationLog.WriteLog(this.ToString(),"时间：" + t.Duration().ToString());

					#endregion
				}
				else if(m_strGetType=="ChildNodes")
				{
					#region 取某节点子目录

					filter = "ParentCode='"+m_strNodeId+"'";

                    #endregion
				}

                DataView dvSrc = new DataView(tbSrc, filter, "", DataViewRowState.CurrentRows);
                foreach (DataRowView drvSrc in dvSrc)
                {
                    DataRow drSrc = drvSrc.Row;

                    DataRow drDst = tbDst.NewRow();
                    FillRow(drSrc, drDst, tbSrc, tbDst);
                    tbDst.Rows.Add(drDst);
                }

//                ApplicationLog.SetLogPath("D:\\项目管理20\\ShimaoPM20\\Log\\");
                //                ApplicationLog.WriteLog(this.ToString(), BLL.XmlTree.GetDataToXmlString(tbDst));

                Response.Write(BLL.XmlTree.GetDataToXmlString(tbDst));
//                Response.Write(RmsPM.WebControls.TreeView.XmlTree.GetDataToXmlString(tbDst));
			}
			catch ( Exception ex )
			{
                ApplicationLog.SetLogPath("D:\\项目管理20\\ShimaoPM20\\Log\\");
                //                ApplicationLog.WriteLog(this.ToString(), BLL.XmlTree.GetDataToXmlString(tbDst));
                ApplicationLog.WriteLog(this.ToString(), ex, "");
			}

			Response.End();
		}

		private void FillColumn(DataRow drDst, DataRow drSrc)
		{
			int iColumnCount = drSrc.Table.Columns.Count;
			for ( int i =0 ; i<iColumnCount; i++)
			{
				string columnName= drSrc.Table.Columns[i].ColumnName;
                string columnNameDst = columnName;

                int pos = BLL.ConvertRule.FindArray(this.m_arrMoneyFieldOriginal, columnName, true);
                if (pos >= 0)  //金额
                {
                    columnNameDst = this.m_arrMoneyField[pos];
                }

				if ( drDst.Table.Columns.Contains(columnNameDst))
				{
					if (pos >= 0)  //金额
					{
                        if (columnName == "ContractPayPercent") //百分比
                        {
                            drDst[columnNameDst] = BLL.StringRule.BuildShowPercentString(drSrc[columnName], "####");
                        }
                        else //金额
                        {
                            drDst[columnNameDst] = BLL.CostBudgetPageRule.GetMoneyShowString(drSrc[columnName], BLL.CostBudgetPageRule.m_MoneyUnit.yuan);

                            //金额提示
                            drDst["Title" + columnNameDst] = BLL.CostBudgetPageRule.GetWanDecimalShowHint(drSrc[columnName]);
                        }
					}
					else
					{
                        drDst[columnNameDst] = drSrc[columnName];
					}
				}
			}
		}

		private void FillRow(DataRow drSrc, DataRow drDst, DataTable tbSrc, DataTable tbDst)
		{
            FillColumn(drDst, drSrc);
//            BLL.ConvertRule.DataRowCopy(drSrc, drDst, tbSrc, tbDst);

            drDst["Layer"] = drSrc["Deep"];

            drDst["ChildNodesCount"] = BLL.ConvertRule.ToInt(drSrc["ChildCount"]);
            drDst["ShowChildNodes"] = "0";

            drDst["HtmlContractPay1"] = BLL.CostBudgetPageRule.GetContractPayHref(drDst["ContractPay1"], drDst["CostCode"], drDst["ContractCode"], "", "", "");
            drDst["HtmlContractPayReal"] = BLL.CostBudgetPageRule.GetContractPayRealHref(drDst["ContractPayReal"], drDst["CostCode"], drDst["ContractCode"], "", "", "");
        }

        /*
		/// <summary>
		/// 填预算表类型行
		/// </summary>
		/// <param name="m_NewRow"></param>
		/// <param name="m_Row"></param>
		/// <param name="m_DataTable"></param>
		private BLL.CostBudgetGroupDynamic FillRowCostBudgetSetGroup(DataRow m_NewRow,DataRow m_Row , DataTable m_DataTable )
		{
			m_NewRow["CostBudgetSetCode"] = "G_" + m_Row["GroupCode"];
			m_NewRow["CostBudgetSetName"] = m_Row["GroupName"];

			//计算总金额
			BLL.CostBudgetGroupDynamic dyn = new BLL.CostBudgetGroupDynamic(m_ProjectCode, BLL.ConvertRule.ToString(m_Row["GroupCode"]), m_CostBudgetBackupCode);
			dyn.AutoRefreshHtml = false;
			dyn.Generate();
			if (dyn.tb.Rows.Count > 0)
			{
				//取分摊前合计
				DataRow drFirst = dyn.tb.Rows[0];
				FillMoneyField(m_NewRow, drFirst);

				//合计累加
				int i = -1;
				foreach(string MoneyField in this.m_arrMoneyFieldOriginal) 
				{
					i++;
					this.m_arrSum[i] = this.m_arrSum[i] + BLL.ConvertRule.ToDecimal(drFirst[MoneyField]);
				}
			}

			m_NewRow["Layer"] = m_Layer;
			m_NewRow["ChildNodesCount"] = "1";//BLL.ConvertRule.ToInt(m_Row["ChildCount"]);
			m_NewRow["ShowChildNodes"] = "0";

			m_NewRow["ShowSpan"] = "none";
			m_NewRow["ShowHref"] = "";

			return dyn;
		}
        */

		/// <summary>
		/// 加合计行
		/// </summary>
		/// <param name="m_Table"></param>
		private void AddSumRow(DataTable m_Table) 
		{
			DataRow m_NewRow = m_Table.NewRow();

			m_NewRow["CostBudgetSetCode"] = "R_0";
			m_NewRow["CostBudgetSetName"] = "合计";

			int i = -1;
			foreach(string MoneyField in this.m_arrMoneyField) 
			{
				i++;
				m_NewRow[MoneyField] = BLL.CostBudgetPageRule.GetMoneyShowString(m_arrSum[i], BLL.CostBudgetPageRule.m_MoneyUnit.yuan);
			}

			m_NewRow["Layer"] = m_Layer;
			m_NewRow["ChildNodesCount"] = 0;
			m_NewRow["ShowChildNodes"] = "0";

			m_NewRow["ShowSpan"] = "";
			m_NewRow["ShowHref"] = "none";

			m_Table.Rows.Add(m_NewRow);
		}

		private void FillMoneyField(DataRow m_NewRow, DataRow drFirst)
		{
			if (BLL.ConvertRule.ToString(drFirst["CostBudgetDtlCode"]) == "R_0")  //第1行是汇总金额
			{
				int i = -1;
				foreach(string MoneyField in this.m_arrMoneyField) 
				{
					i++;

					if (m_NewRow.Table.Columns.Contains(MoneyField)) 
					{
						m_NewRow[MoneyField] = BLL.CostBudgetPageRule.GetMoneyShowString(drFirst[this.m_arrMoneyFieldOriginal[i]], BLL.CostBudgetPageRule.m_MoneyUnit.yuan);
					}
				}
			}
		}

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

        private BLL.CostBudgetDynamic GetDynamic()
        {
            try
            {
                BLL.CostBudgetDynamic dyn;

                string SessionEntityID = Request.QueryString["SessionEntityID"] + "";
                dyn = (BLL.CostBudgetDynamic)Session[SessionEntityID];

                return dyn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
    }
}
