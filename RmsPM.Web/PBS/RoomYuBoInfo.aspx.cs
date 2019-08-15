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
	/// RoomYuBoInfo 的摘要说明。
	/// </summary>
	public partial class RoomYuBoInfo: PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTextArea txtRemark;
		protected System.Web.UI.HtmlControls.HtmlSelect sltCodeName;
		protected System.Web.UI.HtmlControls.HtmlInputText txtOutListName;
		protected AspWebControl.Calendar txtOutDate;
		protected System.Web.UI.HtmlControls.HtmlInputText txtCurYear;
		protected System.Web.UI.HtmlControls.HtmlInputText txtSumNo;
		protected System.Web.UI.HtmlControls.HtmlInputText txtConferMark;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtIOType;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnSelectBuilding;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
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

		private void IniPage() 
		{
			try 
			{
				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
				this.txtAct.Value = Request.QueryString["Act"];
				this.txtOutListCode.Value = Request.QueryString["OutListCode"];

				//权限
				this.btnModify.Visible = base.user.HasRight("011103");
				this.btnDelete.Visible = base.user.HasRight("011104");
				this.btnCheck.Visible = base.user.HasRight("011105");
				this.btnCancelCheck.Visible = base.user.HasRight("011105");

				LoadData();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
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
						this.lblOutAspect.Text = entity.GetString("OutAspect");

						this.txtCheckState.Value = entity.GetInt("CheckState").ToString();
						this.lblCheckState.Text = RmsPM.BLL.ProductRule.GetTempRoomOutCheckStateName(entity.GetInt("CheckState"));
						this.lblCheckDate.Text = entity.GetDateTimeOnlyDate("CheckDate");
						this.lblCheckPerson.Text = BLL.SystemRule.GetUserName(entity.GetString("CheckPerson"));

						this.txtProjectCode.Value = entity.GetString("ProjectCode");
						this.txtOutState.Value = entity.GetString("Out_State");
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "单据不存在"));
						return;
					}
					entity.Dispose();
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "未传入单据号"));
					return;
				}

				this.spanTitle.InnerText = this.txtOutState.Value;
				this.spanOutDate.InnerText = this.txtOutState.Value;

				BindDataGrid();

				//审核后不能再修改、删除、审核
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示信息出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 取房源列表
		/// </summary>
		/// <returns></returns>
		private DataTable GetDataGridTable() 
		{
			EntityData entityDtl = DAL.EntityDAO.ProductDAO.GetBuildingByOutListCode(this.txtOutListCode.Value, "V_Building");
			entityDtl.Dispose();
			return entityDtl.CurrentTable;
		}

		/// <summary>
		/// 显示房源列表
		/// </summary>
		private void BindDataGrid() 
		{
			try 
			{
				DataTable tb = GetDataGridTable();

				string[] arrField = {"YuBoArea"};
				decimal[] arrValue = BLL.MathRule.SumColumn(tb, arrField);
				this.dgList.Columns[0].FooterText = "套数：" + tb.Rows.Count;
				this.dgList.Columns[1].FooterText = arrValue[0].ToString("0.####");
				this.dgList.DataSource = tb;

				this.dgList.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示列表出错：" + ex.Message));
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

				//导Excel
				TExcel excel = new TExcel(Response, Request, Server, Session);
				try 
				{
					excel.StartRow = 6;
					excel.StartCol = 1;
					excel.ColumnHeadRow = 5;
					//				excel.StartFieldIndex = 3;
					excel.DataSource = tb;

					//新建工作簿
					excel.TemplateFileName = "房屋预拨通知单.xls";
					excel.TemplateSheetName = "Sheet1";
					excel.AddWorkbook();

					//表头表尾数据
					excel.SetCellValue("A2", this.lblOutAspect.Text);
					excel.SetCellValue("F2", this.lblOutListName.Text);
					excel.SetCellValue("B9", this.lblConferMark.Text);
					excel.SetCellValue("D9", this.lblRemark.Text);
					excel.SetCellValue("E12", this.lblOutDate.Text);

					//页脚
//					excel.Sheet.PageSetup.RightFooter = "编号：" + this.lblOutListName.Text;

					excel.DataToSheet();

					//保存
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "导出到Excel出错：" + ex.Message));
			}
		}

	}
}
