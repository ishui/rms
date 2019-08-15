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
using Rms.Web;
using RmsReport;

namespace RmsPM.Web.PBS
{
	/// <summary>
	/// RoomIOInInfo ��ժҪ˵����
	/// </summary>
	public partial class RoomIOInInfo: PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTextArea txtRemark;
		protected System.Web.UI.HtmlControls.HtmlSelect sltCodeName;
		protected System.Web.UI.HtmlControls.HtmlInputText txtOutListName;
		protected AspWebControl.Calendar txtOutDate;
		protected System.Web.UI.HtmlControls.HtmlInputText txtCurYear;
		protected System.Web.UI.HtmlControls.HtmlInputText txtSumNo;
		protected System.Web.UI.HtmlControls.HtmlInputText txtConferMark;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtIOType;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
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

		}
		#endregion

		private void IniPage() 
		{
			try 
			{
				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
				this.txtAct.Value = Request.QueryString["Act"];
				this.txtOutListCode.Value = Request.QueryString["OutListCode"];

				//Ȩ��
				this.btnModify.Visible = base.user.HasRight("011203");
				this.btnDelete.Visible = base.user.HasRight("011204");
				this.btnCheck.Visible = base.user.HasRight("011205");
				this.btnCancelCheck.Visible = base.user.HasRight("011205");

				LoadData();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadData() 
		{
			try 
			{
				string OutListCode = this.txtOutListCode.Value.Trim();

				if (OutListCode != "") 
				{
					EntityData entity = DAL.EntityDAO.ProductDAO.GetTempRoomOutByCode(OutListCode);
					if (entity.HasRecord()) 
					{
						this.lblOutListName.Text = entity.GetString("OutListName");
						this.lblOutDate.Text = entity.GetDateTimeOnlyDate("Out_Date");
						this.lblConferMark.Text = entity.GetString("ConferMark");
						this.lblRemark.Text = entity.GetString("Remark").Replace("\n", "<br>");
						this.lblInputPersonName.Text = RmsPM.BLL.SystemRule.GetUserName(entity.GetString("UserCode"));
						this.lblInputDate.Text = entity.GetDateTimeOnlyDate("InputDate");

						this.lblCodeName.Text = BLL.PBSRule.GetPBSTypeName(entity.GetString("CodeName"));

						this.txtCheckState.Value = entity.GetInt("CheckState").ToString();
						this.lblCheckState.Text = RmsPM.BLL.ProductRule.GetTempRoomOutCheckStateName(entity.GetInt("CheckState"));
						this.lblCheckDate.Text = entity.GetDateTimeOnlyDate("CheckDate");
						this.lblCheckPerson.Text = BLL.SystemRule.GetUserName(entity.GetString("CheckPerson"));

						this.txtProjectCode.Value = entity.GetString("ProjectCode");
						this.txtOutState.Value = entity.GetString("Out_State");
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "���ݲ�����"));
						return;
					}
					entity.Dispose();
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "δ���뵥�ݺ�"));
					return;
				}

				this.spanTitle.InnerText = this.txtOutState.Value;
				this.spanOutDate.InnerText = this.txtOutState.Value;

				BindDataGrid();

				//��˺������޸ġ�ɾ�������
				if (this.txtCheckState.Value == "1") 
				{
					this.btnModify.Style["display"] = "none";
					this.btnDelete.Style["display"] = "none";
					this.btnCheck.Style["display"] = "none";
				}
				else 
				{
					this.btnCancelCheck.Style["display"] = "none";
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ��Ϣ����" + ex.Message));
			}
		}

		/// <summary>
		/// ȡ��Դ�б�
		/// </summary>
		/// <returns></returns>
		private DataTable GetDataGridTable() 
		{
			EntityData entityDtl = DAL.EntityDAO.ProductDAO.GetRoomByOutListCode(this.txtOutListCode.Value, "V_ROOM");
			entityDtl.Dispose();
			return entityDtl.CurrentTable;
		}

		/// <summary>
		/// ��ʾ��Դ�б�
		/// </summary>
		private void BindDataGrid() 
		{
			try 
			{
				DataTable tb = GetDataGridTable();

				//��ʾ¥�����á�,���ָ�
				this.lblBuildingName.Text = BLL.ConvertRule.GetDistinctStr(tb, "BuildingName", "", ",");

				string[] arrField = {"BuildArea", "RoomArea"};
				decimal[] arrValue = BLL.MathRule.SumColumn(tb, arrField);
				this.dgList.Columns[1].FooterText = "������" + tb.Rows.Count;
				this.dgList.Columns[4].FooterText = arrValue[0].ToString("0.####");
				this.dgList.Columns[5].FooterText = arrValue[1].ToString("0.####");
				this.dgList.DataSource = tb;

				this.dgList.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
			}
		}

		protected void btnExcel_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				DataTable tb = GetDataGridTable();

//				tb.Columns.Add(new DataColumn("ProjectName", typeof(string)));
//				foreach(DataRow dr in tb.Rows) 
//				{
//					dr["ProjectName"] = BLL.ProjectRule.GetProjectName(this.txtProjectCode.Value);
//				}

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
					excel.TemplateFileName = "������ⵥ.xls";
					excel.TemplateSheetName = "Sheet1";
					excel.AddWorkbook();

					//��ͷ��β����
					excel.SetCellValue("F2", this.lblOutListName.Text);
					excel.SetCellValue("B9", this.lblConferMark.Text);
					excel.SetCellValue("D9", this.lblRemark.Text);
					excel.SetCellValue("E12", this.lblOutDate.Text);

					//ҳ��
//					excel.Sheet.PageSetup.RightFooter = "��ţ�" + this.lblOutListName.Text;


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
				Response.Write(Rms.Web.JavaScript.Alert(true, "������Excel����" + ex.Message));
			}
		}

	}
}
