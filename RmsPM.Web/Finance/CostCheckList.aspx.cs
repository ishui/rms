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
	/// CostCheckList ��ժҪ˵����
	/// </summary>
	public partial class CostCheckList : PageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadDataGrid();
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
			this.dgList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemDataBound);
			this.btnApportionExcel.ServerClick += new System.EventHandler(this.btnApportionExcel_ServerClick);
		}
		#endregion

		private void IniPage()
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
			}
			catch (Exception ex) 
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ���ݳ���" + ex.Message));
			}
		}

		private void LoadDataGrid() 
		{
			string projectCode = Request["ProjectCode"]+"";
			try 
			{
				//����ֶ�
				string BuildingAreaFieldName = BLL.CostRule.GetApportionAreaField(projectCode);
				string BuildingAreaFieldDesc = BLL.ProductRule.GetBuildingAreaFieldDesc(BuildingAreaFieldName);
				((System.Web.UI.WebControls.TemplateColumn)dgList.Columns[4]).HeaderText = BuildingAreaFieldDesc + "(ƽ��)";

				BuildingStrategyBuilder sb = new BuildingStrategyBuilder();

				sb.AddStrategy( new Strategy( BuildingStrategyName.ProjectCode, projectCode));
				sb.AddStrategy( new Strategy( BuildingStrategyName.IsArea, "2"));

				sb.AddOrder("BuildingName", true);

				string sql = sb.BuildQueryViewString();

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "Building",sql );
				qa.Dispose();

				string[] arrField = {"RoomArea", "TotalCost"};
				decimal[] arrSum = BLL.MathRule.SumColumn(entity.CurrentTable, arrField);

				ViewState["SumArea"] = BLL.StringRule.BuildShowNumberString(arrSum[0]);
				ViewState["SumCost"] = BLL.StringRule.BuildShowNumberString(arrSum[1]);

				DataTable tb = entity.CurrentTable;
				BLL.PaymentRule.AddBuildingCBVoucherCode(tb);

				//�����
				foreach(DataRow dr in tb.Rows) 
				{
					dr["Area"] = dr[BuildingAreaFieldName];
				}

				dgList.DataSource = tb;
				dgList.DataBind();
				entity.Dispose();
			}
			catch (Exception ex) 
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ���ݳ���" + ex.Message));
			}
		}

//		private void btnCalc_ServerClick(object sender, System.EventArgs e)
//		{
//			try 
//			{
//				BLL.CostRule.CostCheck(this.txtProjectCode.Value);
//			}
//			catch (Exception ex) 
//			{
//				ApplicationLog.WriteLog(this.ToString(),ex,"");
//				Response.Write(Rms.Web.JavaScript.Alert(true, "�ɱ��������" + ex.Message));
//				return;
//			}
//
//			LoadDataGrid();
//		}

		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Footer) 
			{
				//��ʾ�ϼ�
				((Label)e.Item.FindControl("lblSumArea")).Text = BLL.ConvertRule.ToString(ViewState["SumArea"]);
				((Label)e.Item.FindControl("lblSumCost")).Text = BLL.ConvertRule.ToString(ViewState["SumCost"]);
			}
		}

		/// <summary>
		/// ��̯��Excel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnApportionExcel_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				string ProjectCode = Request["ProjectCode"] + "";

				DataTable tb = BLL.CostRule.CostApportionExcel(ProjectCode, BLL.CostRule.GetApportionAreaField(ProjectCode));

				if (tb.Rows.Count == 0) 
				{
					Response.Write(JavaScript.Alert(true, "������"));
					return;
				}

				DataView dv = new DataView(tb, "", "BuildingName", DataViewRowState.CurrentRows);

				//��Excel
				TExcel excel = new TExcel(Response, Request, Server, Session);
				try 
				{
					excel.StartRow = 6;
					excel.StartCol = 1;
					excel.ColumnHeadRow = 5;
					//				excel.StartFieldIndex = 3;
					excel.DataSource = dv;

					//�½�������
					excel.TemplateFileName = "�ɱ���̯��.xls";
					excel.TemplateSheetName = "Sheet1";
					excel.AddWorkbook();
				

					//��ͷ��β����
					excel.SetCellValue("C2", BLL.ProjectRule.GetProjectName(ProjectCode));

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
			catch (Exception ex) 
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��̯��Excel����" + ex.Message));
			}
		}

	}
}
