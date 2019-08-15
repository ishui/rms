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
using RmsPM.DAL;
using RmsPM.DAL.QueryStrategy;
using RmsPM.DAL.EntityDAO;
using Rms.Web;

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// SalPayList 的摘要说明。
	/// </summary>
	public partial class SalPayList : PageBase
	{
		private string m_LastContractCode = "";
		private string m_LastStyle = "";

		protected RmsPM.WebControls.ToolsBar.ToolsButton ButtonNew;
		protected RmsPM.WebControls.ToolsBar.ToolsButton Toolsbutton1;
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadDataGrid(true);
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
			this.dgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgList_PageIndexChanged);
			this.dgList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemDataBound);

		}
		#endregion

		private void IniPage()
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
//				this.txtParam.Value = Request.QueryString["param"];

				//权限
				this.btnBuildVoucher.Visible = base.user.HasRight("060403");
				this.btnSelectVoucher.Visible = base.user.HasRight("060403");
				this.btnMapSupl.Visible = base.user.HasRight("060403");
				this.btnMapRoom.Visible = base.user.HasRight("060402");
				this.btnImportSalOld.Visible = base.user.HasRight("060412");

				BLL.PageFacade.LoadSalBuildingSelect(this.sltSearchBuildingName, "", this.txtProjectCode.Value);

//				switch (this.txtParam.Value.Trim()) 
//				{
//						//销售补差
//					case "1":
//						this.tdTitle.InnerText = "销售补差";
//						this.chkNotBalance.Visible = false;
//						break;
//
//					default:
//						break;
//				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadDataGrid(bool isEmpty)
		{
			try
			{
				m_LastContractCode = "";
				m_LastStyle = "";

				EntityData entity;

				if (isEmpty) 
				{
					entity = new EntityData("SalPay");
				}
				else 
				{
					SalPayStrategyBuilder sb = new SalPayStrategyBuilder();
					sb.AddStrategy( new Strategy( SalPayStrategyName.ProjectCode,this.txtProjectCode.Value));

					string ContractID = this.txtSearchContractID.Value.Trim();
					if (ContractID != "")
						sb.AddStrategy(new Strategy(SalPayStrategyName.ContractID, ContractID));

                    string ClientName = this.txtClientName.Value.Trim();
                    if (ClientName != "")
                        sb.AddStrategy(new Strategy(SalPayStrategyName.ClientName, ClientName));

                    string BuildingName = this.sltSearchBuildingName.Value.Trim();
					if (BuildingName != "")
						sb.AddStrategy(new Strategy(SalPayStrategyName.BuildingName, BuildingName));

					string Status = this.sltSearchStatus.Value.Trim();
					if (Status != "")
						sb.AddStrategy(new Strategy(SalPayStrategyName.Status, Status));

					if ( this.dtSearchDateBegin.Value != "" || this.dtSearchDateEnd.Value != "" )
					{
						ArrayList ar = new ArrayList();
						ar.Add(this.dtSearchDateBegin.Value);
						ar.Add(this.dtSearchDateEnd.Value);
						sb.AddStrategy( new Strategy( SalPayStrategyName.PayDateRange,ar ));
					}

					if (this.chkNotBalance.Checked && this.chkBalance.Checked) 
					{
					}
					else 
					{
						if (this.chkNotBalance.Checked) 
						{
							sb.AddStrategy(new Strategy(SalPayStrategyName.NotBalance));
						}
						else 
						{
							if (this.chkBalance.Checked) 
							{
								sb.AddStrategy(new Strategy(SalPayStrategyName.OnlyBalance));
							}
						}
					}

//					switch (this.txtParam.Value.Trim()) 
//					{
//							//销售补差
//						case "1":
//							sb.AddStrategy(new Strategy(SalPayStrategyName.OnlyBalance));
//							break;
//
//						default:
//
//							break;
//					}

					sb.AddOrder("ContractID", true);
					sb.AddOrder("PayDate", true);

					string sql = sb.BuildMainQueryString();

					QueryAgent qa = new QueryAgent();
					entity = qa.FillEntityData( "SalPay",sql );
					qa.Dispose();
				}

				this.dgList.DataSource = entity.CurrentTable;
				this.dgList.Columns[2].FooterText = "笔数：" + entity.CurrentTable.Rows.Count.ToString();
				this.dgList.Columns[6].FooterText = Total(entity.CurrentTable, "PayMoney").ToString("n");
				this.dgList.DataBind();

                this.GridPagination1.RowsCount = entity.CurrentTable.Rows.Count.ToString();
                
                entity.Dispose();
				

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示列表出错：" + ex.Message));
			}
		}

		private void dgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
            this.dgList.CurrentPageIndex = e.NewPageIndex;
			LoadDataGrid(false);
		}

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
            this.dgList.CurrentPageIndex = 0;
			LoadDataGrid(false);
		}

		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			//不同合同用不同颜色区分
			if ((e.Item.ItemType == ListItemType.Item) 
				|| (e.Item.ItemType == ListItemType.AlternatingItem ))
			{
				string ContractCode = e.Item.Cells[2].Text.Trim();
				if (ContractCode != m_LastContractCode) 
				{
					if (m_LastStyle.ToUpper() == "ListBodyTr".ToUpper()) 
					{
						m_LastStyle = "AlterGridTr";
					}
					else 
					{
						m_LastStyle = "ListBodyTr";
					}
				}

				e.Item.CssClass = m_LastStyle;

				m_LastContractCode = ContractCode;

				//分页时序号连续
//				int i = e.Item.ItemIndex + 1;
//				if (dgList.AllowPaging)
//				{
//					i = dgList.PageSize * dgList.CurrentPageIndex + i;
//				}
//				e.Item.Cells[1].Text = i.ToString();
			}
		}

		private decimal Total(DataTable m_Table,string m_strField)
		{
			decimal m_dcReturn=0M;
			foreach(DataRow m_Row in m_Table.Rows)
			{
				try 
				{
					m_dcReturn+=decimal.Parse(m_Row[m_strField].ToString());
				}
				catch 
				{
				}
			}
			return m_dcReturn;
		}

		protected void btnMakeVoucherHidden_ServerClick(object sender, System.EventArgs e)
		{
			Session["RelaCode"] = this.txtSelect.Value;

			string s = JavaScript.ScriptStart;
			s = s + String.Format("MakeVoucher('{0}', '{1}');", this.txtVoucherCode.Value, this.txtParam.Value);
			s = s + JavaScript.ScriptEnd;
			Page.RegisterStartupScript("MakeVoucher", s);
		}

		/// <summary>
		/// 对应房源
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnMapRoom_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				BLL.DtsPayRule.MapSalRoom(this.txtProjectCode.Value);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "对应房源出错：" + ex.Message));
			}
		}

        protected void GridPagination1_PageIndexChange(object sender, System.EventArgs e)
        {
            try
            {
                this.LoadDataGrid(false);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
            }
        }

    }
}
