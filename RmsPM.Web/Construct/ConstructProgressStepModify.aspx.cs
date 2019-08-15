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
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using Rms.Web;
using RmsPM.BLL;

namespace RmsPM.Web.Construct
{
	/// <summary>
	/// ConstructProgressStepModify 的摘要说明。
	/// </summary>
	public partial class ConstructProgressStepModify : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack )
			{
				IniPage();
			}
		}

		private void IniPage()
		{
			try 
			{
				this.txtAct.Value = Request.QueryString["action"];
				this.txtPBSUnitCode.Value = Request.QueryString["PBSUnitCode"];

				LoadPBSUnit();
				LoadDataGrid();

			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadPBSUnit()
		{
			string code =  this.txtPBSUnitCode.Value;

			try
			{
				if (code != "") 
				{
					EntityData entity = DAL.EntityDAO.PBSDAO.GetPBSUnitByCode(code);

					if ( entity.HasRecord())
					{
						this.txtPBSUnitCode.Value = entity.GetString("PBSUnitCode");
						this.txtProjectCode.Value = entity.GetString("ProjectCode");
					}
					else
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "单位工程不存在"));
						Response.End();
					}

					entity.Dispose();
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "单位工程编号不能为空"));
					Response.End();
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"加载单位工程失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "加载单位工程失败：" + ex.Message));
			}
		}

		/// <summary>
		/// 显示进度明细
		/// </summary>
		private void LoadDataGrid()
		{
			string PBSUnitCode =  this.txtPBSUnitCode.Value;

			try
			{
				int year = BLL.ConvertRule.ToInt(BLL.ConstructRule.GetConstructPlanCurrYearByProject(txtProjectCode.Value));

				DataTable tb = BLL.ConstructRule.GenerateConstructPlanProgressTable(PBSUnitCode, year);

				this.dgList.DataSource = tb;
				this.dgList.DataBind();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"加载进度失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "加载进度失败：" + ex.Message));
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

		/// <summary>
		/// 有效性检查
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			return true;
		}

		/// <summary>
		/// 返回
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write("window.opener.location = window.opener.location;");
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string Hint = "";
				if (!CheckValid(ref Hint)) 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
					return;
				}

				SaveDtl();

				GoBack();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "保存出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 保存进度明细
		/// </summary>
		private void SaveDtl() 
		{
			try 
			{
				string PBSUnitCode = this.txtPBSUnitCode.Value;

				int iCount = this.dgList.Items.Count;
				for(int i=0;i<iCount;i++) 
				{
					string code = this.dgList.DataKeys[i].ToString();
					HtmlInputHidden txtVisualProgress = (HtmlInputHidden)this.dgList.Items[i].FindControl("txtVisualProgress");
					HtmlInputHidden txtVisualProgressName = (HtmlInputHidden)this.dgList.Items[i].FindControl("txtVisualProgressName");
					HtmlInputHidden txtProgressType = (HtmlInputHidden)this.dgList.Items[i].FindControl("txtProgressType");
					HtmlInputHidden txtIsPoint = (HtmlInputHidden)this.dgList.Items[i].FindControl("txtIsPoint");
					AspWebControl.Calendar txtStartDate = (AspWebControl.Calendar)this.dgList.Items[i].FindControl("txtStartDate");
					AspWebControl.Calendar txtEndDate = (AspWebControl.Calendar)this.dgList.Items[i].FindControl("txtEndDate");

					EntityData entity = DAL.EntityDAO.ConstructDAO.GetConstructProgressStepByCode(code);
					DataRow dr;
					bool isNew = !entity.HasRecord();

					if (isNew) 
					{
						dr = entity.CurrentTable.NewRow();
						dr["ProgressStepCode"] = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ProgressStepCode");
					}
					else 
					{
						dr = entity.CurrentRow;
					}

					dr["PBSUnitCode"] = PBSUnitCode;
					dr["ProjectCode"] = this.txtProjectCode.Value;
					dr["VisualProgress"] = txtVisualProgress.Value;
					dr["StartDate"] = BLL.ConvertRule.ToDate(txtStartDate.Value);

					//只填开始日期时，结束日期自动填开始日期
					if (txtIsPoint.Value == "1") 
					{
						dr["EndDate"] = dr["StartDate"];
					}
					else 
					{
						dr["EndDate"] = BLL.ConvertRule.ToDate(txtEndDate.Value);
					}


					if (isNew) 
					{
						entity.CurrentTable.Rows.Add(dr);
						DAL.EntityDAO.ConstructDAO.InsertConstructProgressStep(entity);
					}
					else 
					{
						DAL.EntityDAO.ConstructDAO.UpdateConstructProgressStep(entity);
					}

					entity.Dispose();
				}
			}
			catch ( Exception ex )
			{
				throw ex;
			}
		}
	}
}
