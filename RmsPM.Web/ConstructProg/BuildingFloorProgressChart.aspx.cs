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
using RmsPM.DAL.QueryStrategy;
using RmsPM.BLL;
using Rms.Web;

namespace RmsPM.Web.ConstructProg
{
	/// <summary>
	/// BuildingFloorProgressChart ��ժҪ˵����
	/// </summary>
	public partial class BuildingFloorProgressChart : PageBase
	{
		protected System.Web.UI.WebControls.Repeater dgListTitle;
		protected System.Web.UI.WebControls.Repeater dgListTitle2;
		protected System.Web.UI.WebControls.Repeater dgList;
		protected System.Web.UI.WebControls.Label lblProjectName;
		protected System.Web.UI.WebControls.Label lblBuildingName;

		private DataTable m_tb;
		private DataTable m_tbTitle;
		private DataTable m_tbBuilding;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
				LoadChart();
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
			this.dgBuilding.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.dgBuilding_ItemDataBound);

		}
		#endregion

		private void IniPage() 
		{
			try 
			{
				this.txtBuildingCode.Value = Request.QueryString["BuildingCode"];
				this.txtVisualProgress.Value = Request.QueryString["VisualProgress"];
				this.txtMulti.Value = Request.QueryString["Multi"];
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];

//				EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingByCode(txtBuildingCode.Value);
//				if (entity.HasRecord()) 
//				{
//					this.lblBuildingName.Text = entity.GetString("BuildingName");
//				}
//				else 
//				{
//					Response.Write(Rms.Web.JavaScript.Alert(true, "¥��������"));
//				}
//				entity.Dispose();

				if (this.txtMulti.Value == "1") 
				{
					//���¥�������������������
					this.lblVisualProgressName.Text = this.txtVisualProgress.Value;
					this.lblVisualProgressName.Attributes["hint"] = "";
				}
				else 
				{
					//����¥��������������ȴ���
					this.lblVisualProgressName.Text = BLL.WBSRule.GetWBSName(this.txtVisualProgress.Value);
					this.lblVisualProgressName.Attributes["hint"] = BLL.ConstructProgRule.GetTaskHintHtml(this.txtVisualProgress.Value);
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void AddColumnBuilding(DataTable tb, string BuildingCode) 
		{
			try 
			{
				tb.Columns.Add("BuildingCode");
				foreach(DataRow dr in tb.Rows) 
				{
					dr["BuildingCode"] = BuildingCode;
				}
			}
			catch(Exception ex) 
			{
				throw ex;
			}
		}

		private void GetDataTable(string BuildingCode, string VisualProgress) 
		{
			//�б�������Դ��1��2����
			DataTable tbTitle = BLL.ConstructProgRule.GetBuildingTaskVisualProgressChild(VisualProgress);

			AddColumnBuilding(tbTitle, BuildingCode);           
			if (m_tbTitle == null) 
			{
				m_tbTitle = tbTitle;
			}
			else 
			{
				BLL.ConvertRule.DataTableCopyRow(tbTitle, m_tbTitle);
			}
//			ViewState["tbTitle"] = tbTitle;

			//��1���б���
			DataView dv1 = new DataView(tbTitle, "TempLevel=1", "SortID", DataViewRowState.CurrentRows);
//			this.dgListTitle.DataSource = dv1;
//			this.dgListTitle.DataBind();

			//��2���б���
			//DataView dv2 = new DataView(tbTitle, "TempLevel=2", "SortID", DataViewRowState.CurrentRows);
//			this.dgListTitle2.DataSource = dv2;
//			this.dgListTitle2.DataBind();

			//¥�����ƣ������
			EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingByCode(BuildingCode);
			if (entity.HasRecord()) 
			{
				DataRow drB = m_tbBuilding.NewRow();
				drB["BuildingCode"] = BuildingCode;
				drB["BuildingName"] = entity.CurrentRow["BuildingName"];

				int ColSpan = 0;
				foreach(DataRowView drv in dv1) 
				{
					DataRow dr = drv.Row;
					ColSpan = ColSpan + BLL.ConvertRule.ToInt(dr["ColSpan"]);
				}

				drB["ColSpan"] = ColSpan;
				m_tbBuilding.Rows.Add(drB);
			}
			entity.Dispose();

			//ĩ������
			DataView dvLeaf = new DataView(tbTitle, "IsLeaf=1", "SortID", DataViewRowState.CurrentRows);

			//������
			DataTable tb = BLL.ConstructProgRule.GenerateBuildingFloorProgressChartTable(BuildingCode, VisualProgress, dvLeaf);

			if (m_tb == null) 
			{
				m_tb = tb;
			}
			else 
			{
				BLL.ConvertRule.DataTableCopyRow(tb, m_tb);
			}
//			ViewState["tb"] = tb;
		}

		private void LoadChart() 
		{
			try 
			{
				string BuildingCode = this.txtBuildingCode.Value;
				string VisualProgress = this.txtVisualProgress.Value;
				string Multi = this.txtMulti.Value;
				string ProjectCode = this.txtProjectCode.Value;

				m_tb = null;
				m_tbTitle = null;

				//¥�����ƣ������
				m_tbBuilding = new DataTable();
				m_tbBuilding.Columns.Add("BuildingCode");
				m_tbBuilding.Columns.Add("BuildingName");
				m_tbBuilding.Columns.Add("ColSpan", typeof(int));

				if (Multi == "1") 
				{
					//���¥��
					string[] arrBuildingCode = BuildingCode.Split(",".ToCharArray());
					foreach(string code in arrBuildingCode) 
					{
						if (code != "") 
						{
							string VGCode = "";

							//�������������ȡ����
							EntityData entityV = BLL.ConstructProgRule.GetBuildingTaskVisualProgress(code, ProjectCode);
							DataRow[] drs = entityV.CurrentTable.Select("TaskName='" + VisualProgress + "'");
							if (drs.Length > 0) 
							{
								VGCode = drs[0]["WBSCode"].ToString();
							}
							entityV.Dispose();

							GetDataTable(code, VGCode);
						}
					}
				}
				else 
				{
					//����¥��
					GetDataTable(BuildingCode, VisualProgress);
				}

				this.dgBuilding.DataSource = m_tbBuilding;
				this.dgBuilding.DataBind();

//				if ((BuildingCode != "") && (VisualProgress != ""))
//				{
//					DataTable tb = GetDataTable(BuildingCode, VisualProgress);
//					this.dgList.DataSource = tb;
//					this.dgList.DataBind();
//				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ʾͼ��ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾͼ��ʧ�ܣ�" + ex.Message));
			}
		}

		private void dgBuilding_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			try 
			{
				string BuildingCode = ((HtmlInputHidden)e.Item.FindControl("txtBuildingCodeDtl")).Value;

//				DataTable tbTitle = (DataTable)ViewState["tbTitle"];

				DataView dv1 = new DataView(m_tbTitle, string.Format("BuildingCode='{0}' and TempLevel=1", BuildingCode), "SortID", DataViewRowState.CurrentRows);
				Repeater dgListTitle1 = (Repeater)e.Item.FindControl("dgListTitle");
				dgListTitle1.DataSource = dv1;
				dgListTitle1.DataBind();

				DataView dv2 = new DataView(m_tbTitle, string.Format("BuildingCode='{0}' and TempLevel=2", BuildingCode), "SortID", DataViewRowState.CurrentRows);
				Repeater dgListTitle2 = (Repeater)e.Item.FindControl("dgListTitle2");
				dgListTitle2.DataSource = dv2;
				dgListTitle2.DataBind();

//				DataTable tb = (DataTable)ViewState["tb"];
				DataView dv = new DataView(m_tb, string.Format("BuildingCode='{0}'", BuildingCode), "FloorIndex desc", DataViewRowState.CurrentRows);
				Repeater dgList = (Repeater)e.Item.FindControl("dgList");
				dgList.DataSource = dv;
				dgList.DataBind();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ʾͼ��ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾͼ��ʧ�ܣ�" + ex.Message));
			}
		}

	}
}
