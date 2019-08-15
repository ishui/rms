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
using RmsReport;

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// SalCB ��ժҪ˵����
	/// </summary>
	public partial class SalCB : PageBase
	{
		protected RmsPM.WebControls.ToolsBar.ToolsButton ButtonNew;
		protected RmsPM.WebControls.ToolsBar.ToolsButton Toolsbutton1;
		protected System.Web.UI.HtmlControls.HtmlInputText txtSearchContractID;
		protected System.Web.UI.HtmlControls.HtmlSelect sltSearchStatus;
		protected AspWebControl.Calendar dtSearchDateBegin;
		protected AspWebControl.Calendar dtSearchDateEnd;
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadDataGrid(0, true);
			}
		}

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    
			this.dgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgList_PageIndexChanged);
			this.dgList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemDataBound);

		}
		#endregion

		private void IniPage()
		{
			this.txtParam.Value = "0";
			this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
			BLL.PageFacade.LoadSalBuildingSelect(this.sltSearchBuildingName, "", this.txtProjectCode.Value);

			/*
			this.txtParam.Value = Request.QueryString["param"];

			switch (this.txtParam.Value.Trim()) 
			{
					//���۲���
				case "1":
					this.tdTitle.InnerText = "���۲���";
					this.chkNotBalance.Visible = false;
					break;

				default:
					break;
			}
			*/
		}

		private DataTable LoadDataGrid(int PageIndex, bool isEmpty)
		{
			try
			{
				DataTable tb = null;

				this.txtTotalCost.Value = "";
				this.txtTotalPayMoney.Value = "";

				string BuildingName = this.sltSearchBuildingName.Value.Trim();
				this.txtBuildingName.Value = BuildingName;

/*				//��Ʒ���㰴���ţ�������ѡ�񴱺�
				if (BuildingName == "") 
				{
					return tb;
				}
*/
				LoadData(BuildingName);

				//����ɱ�
				BLL.SalCostClass cost = new RmsPM.BLL.SalCostClass(txtProjectCode.Value, BuildingName);

				EntityData entity;

				SalContractStrategyBuilder sb = new SalContractStrategyBuilder();
				sb.AddStrategy( new Strategy( SalContractStrategyName.ProjectCode,txtProjectCode.Value));
				sb.AddStrategy(new Strategy(SalContractStrategyName.BuildingName, BuildingName));

				if (isEmpty) 
					sb.AddStrategy(new Strategy(SalContractStrategyName.False));

				sb.AddOrder("ContractID", true);

				string sql = sb.BuildMainQueryString();

				QueryAgent qa = new QueryAgent();
				entity = qa.FillEntityData( "SalContract",sql );
				qa.Dispose();

				tb = entity.CurrentTable;

				DataColumn col = new DataColumn("CostPrice");
				col.DataType = System.Type.GetType("System.Decimal");
				tb.Columns.Add(col);

				col = new DataColumn("Cost");
				col.DataType = System.Type.GetType("System.Decimal");
				tb.Columns.Add(col);

				int count = tb.Rows.Count;
				int i = -1;
				decimal RealCost = 0;

				foreach(DataRow dr in tb.Rows) 
				{
					i = i + 1;

					dr["CostPrice"] = cost.m_CostPrice;

					decimal area = 0;
					decimal a_cost = 0;

					if (i == count - 1) 
					{
						//���һ�� = �ܳɱ� - ǰn-1���ɱ�
						a_cost = cost.m_TotalCost - RealCost;
					}
					else 
					{
						//�ɱ� = �ɱ����� * ���
						try 
						{
							area = decimal.Parse(dr["BuildDim"].ToString());
						}
						catch 
						{
						}

						a_cost = Math.Round(cost.m_CostPrice * area, 2);
						RealCost = RealCost + a_cost;
					}

					dr["Cost"] = a_cost;
				}

				entity.Dispose();

				decimal TotalPayMoney = BLL.MathRule.SumColumn(tb, "TotalPayMoney");

				this.dgList.CurrentPageIndex = PageIndex;
				this.dgList.DataSource = tb;
				this.dgList.Columns[2].FooterText = "������" + tb.Rows.Count.ToString();
				this.dgList.Columns[7].FooterText = cost.m_TotalArea.ToString("n");
				this.dgList.Columns[9].FooterText = TotalPayMoney.ToString("n");
				this.dgList.Columns[11].FooterText = cost.m_TotalCost.ToString("n");
				this.txtTotalCost.Value = cost.m_TotalCost.ToString();
				this.txtTotalPayMoney.Value = TotalPayMoney.ToString();
				this.dgList.DataBind();
				return tb;
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
				return null;
			}
		}

		private void LoadData(string BuildingName) 
		{
			try 
			{
				string VoucherCodeCB = BLL.SalRule.GetSalCBVoucherCode(BuildingName, txtProjectCode.Value);
				this.aVoucherCodeCB.InnerText = BLL.PaymentRule.GetVoucherName(VoucherCodeCB);
				this.aVoucherCodeCB.Attributes["val"] = VoucherCodeCB;

				string VoucherCodeJT = BLL.SalRule.GetSalJTVoucherCode(BuildingName, txtProjectCode.Value);
				this.aVoucherCodeJT.InnerText = BLL.PaymentRule.GetVoucherName(VoucherCodeJT);
				this.aVoucherCodeJT.Attributes["val"] = VoucherCodeJT;
			}
			catch (Exception ex) 
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ���ݳ���" + ex.Message));
			}
		}

		private void dgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			LoadDataGrid(e.NewPageIndex, false);
		}

		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if ((e.Item.ItemType == ListItemType.Item) 
				|| (e.Item.ItemType == ListItemType.AlternatingItem ))
			{
				//���
				int i = e.Item.ItemIndex + 1;
				if (dgList.AllowPaging)
				{
					i = dgList.PageSize * dgList.CurrentPageIndex + i;
				}
				e.Item.Cells[0].Text = i.ToString();
			}
		}

		protected void btnExcel_ServerClick(object sender, System.EventArgs e)
		{
			DataTable tb = LoadDataGrid(0, false);

			if (tb == null)
				return;

			if (tb.Rows.Count == 0) 
			{
				Response.Write(JavaScript.Alert(true, "������"));
				return;
			}

			//��Excel
			TExcel excel = new TExcel(Response, Request, Server, Session);
			try 
			{
				excel.StartRow = 6;
				excel.StartCol = 1;
				excel.ColumnHeadRow = 5;
				//				excel.StartFieldIndex = 3;
				excel.DataSource = tb;

				//�½�������
				excel.TemplateFileName = "�ɱ������.xls";
				excel.TemplateSheetName = "Sheet1";
				excel.AddWorkbook();
				

				//��ͷ��β����
				excel.SetCellValue("C2", BLL.ProjectRule.GetProjectName(this.txtProjectCode.Value));
				excel.SetCellValue("G2", this.sltSearchBuildingName.Value.Trim());

				//ҳ��
				//				excel.Sheet.PageSetup.RightFooter = "��λ�����ˣ�" + "½**" + "  �������ڣ�" + DateTime.Today;


				excel.DataToSheet();

				//����
				excel.SaveWorkbook();
				excel.ShowClient();
			}
			finally 
			{
				excel.Dispose();
			}
		}

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
/*
			string BuildingName = this.sltSearchBuildingName.SelectedValue.Trim();
			if (BuildingName == "") 
			{
				Response.Write(JavaScript.Alert(true, "��ѡ�񴱺�"));
				return;
			}
*/
			LoadDataGrid(0, false);
		}
	}
}
