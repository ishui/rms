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

namespace RmsPM.Web.ConstructProg
{
	/// <summary>
	/// BuildingFloorProgressBatchModify 的摘要说明。
	/// </summary>
	public partial class BuildingFloorProgressBatchModify :PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtPBSUnitCode;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack )
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage()
		{
			try 
			{
				this.txtBuildingCode.Value = Request.QueryString["BuildingCode"];
				this.txtVisualProgressCode.Value = Request.QueryString["VisualProgressCode"];
				this.txtAct.Value = Request.QueryString["action"];

				BLL.PageFacade.LoadBuildingProgressFloorSelect(this.sltBuildingFloorIndexBegin, "", this.txtBuildingCode.Value);
				BLL.PageFacade.LoadBuildingProgressFloorSelect(this.sltBuildingFloorIndexEnd, "", this.txtBuildingCode.Value);

				BLL.PageFacade.LoadBuildingProgressChildTaskSelect(this.sltTask, "", this.txtBuildingCode.Value, this.txtVisualProgressCode.Value);
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
				if (!user.HasRight("030303"))  //楼栋进度填写权限
				{
					Response.Redirect( "../RejectAccess.aspx?OperationCode=030303" );
					Response.End();
				}

				if ((this.txtBuildingCode.Value.Trim() == "") || (this.txtVisualProgressCode.Value.Trim() == ""))
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "未传入楼栋代码或形象进度代码"));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
					return;
				}

				//取楼栋
				EntityData entityBuilding = DAL.EntityDAO.ProductDAO.GetBuildingByCode(this.txtBuildingCode.Value);
				if (!entityBuilding.HasRecord()) 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "楼栋不存在"));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
					return;
				}

				this.txtProjectCode.Value = entityBuilding.GetString("ProjectCode");
				this.lblBuildingName.Text = entityBuilding.GetString("BuildingName");

				entityBuilding.Dispose();

				//取形象进度
				this.lblVisualProgressName.Text = BLL.WBSRule.GetWBSName(this.txtVisualProgressCode.Value);

				//是否有工作项的修改权限
				if (!WBSRule.IsTaskModify(this.txtVisualProgressCode.Value, user.UserCode))
				{
					Response.Redirect( "../RejectAccess.aspx?OperationName=工作项[" + lblVisualProgressName.Text + "]修改" );
					Response.End();
				}

				//缺省值
				this.sltStatus.Value = "0";

				//缺省实际开始、结束日期为当天（便于状态改成“进行中”或“已完成”时有缺省日期，保存时若状态为“未开始”要自动清空）
				this.txtStartDate.Value = DateTime.Today.ToString("yyyy-MM-dd");
				this.txtEndDate.Value = DateTime.Today.ToString("yyyy-MM-dd");
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
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
		/// 返回
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write("window.opener.BatchModifyReturn();");
//			Response.Write("window.opener.location = window.opener.location;");
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

				Save();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "保存出错：" + ex.Message));
				return;
			}

			GoBack();
		}

		/// <summary>
		/// 保存
		/// </summary>
		private void Save() 
		{
			try 
			{
				string BuildingCode = this.txtBuildingCode.Value;
				string WBSCode = this.sltTask.Value;

				//按楼层范围取楼层
				EntityData entityFloor = DAL.EntityDAO.ProductDAO.GetBuildingFloorByBuildingCode(BuildingCode);
				string filter = "";
				if (this.sltBuildingFloorIndexBegin.Value.Trim() != "")
				{
					int FloorIndexBegin = BLL.ConstructProgRule.GetBuildingFloorIndex(this.sltBuildingFloorIndexBegin.Value);
					int FloorIndexEnd = BLL.ConstructProgRule.GetBuildingFloorIndex(this.sltBuildingFloorIndexEnd.Value);

					if (FloorIndexBegin <= FloorIndexEnd)
					{
						filter = string.Format("FloorIndex >= {0} and FloorIndex <= {1}", FloorIndexBegin, FloorIndexEnd);
					}
					else
					{
						filter = string.Format("FloorIndex >= {0} and FloorIndex <= {1}", FloorIndexEnd, FloorIndexBegin);
					}
				}
				DataRow[] drsFloor = entityFloor.CurrentTable.Select(filter);

				//楼层循环
				foreach(DataRow drFloor in drsFloor) 
				{
					string BuildingFloorCode = BLL.ConvertRule.ToString(drFloor["BuildingFloorCode"]);

					//更新一个楼层的进度 begin--------------------------------------
					EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingFloorProgressByBuildingFloorWBSCode(BuildingFloorCode, WBSCode);
					bool isNew = !entity.HasRecord();
					DataRow dr;

					if (isNew) 
					{
						dr = entity.CurrentTable.NewRow();
						dr["ProgressCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("BuildingFloorProgressCode");
						dr["BuildingFloorCode"] = BuildingFloorCode;
						dr["WBSCode"] = WBSCode;
						dr["BuildingCode"] = txtBuildingCode.Value;
						dr["ProjectCode"] = txtProjectCode.Value;
						dr["VisualProgressCode"] = txtVisualProgressCode.Value;

						entity.CurrentTable.Rows.Add(dr);
					}
					else 
					{
						dr = entity.CurrentRow;
					}

					int Status = BLL.ConvertRule.ToInt(this.sltStatus.Value);
					dr["Status"] = Status;

					dr["PStartDate"] = BLL.ConvertRule.ToDate(this.txtPStartDate.Value.Trim());
					dr["PEndDate"] = BLL.ConvertRule.ToDate(this.txtPEndDate.Value.Trim());

					switch (Status) 
					{
						case 0:
							//未完成
							dr["StartDate"] = DBNull.Value;
							dr["EndDate"] = DBNull.Value;
							dr["CompletePercent"] = 0;
							break;

						case 1:
							//进行中
							dr["StartDate"] = BLL.ConvertRule.ToDate(this.txtStartDate.Value.Trim());
							dr["EndDate"] = DBNull.Value;
							dr["CompletePercent"] = BLL.ConvertRule.ToInt(this.txtCompletePercent.Value);
							break;

						case 2:
							//已完成
							dr["StartDate"] = BLL.ConvertRule.ToDate(this.txtStartDate.Value.Trim());
							dr["EndDate"] = BLL.ConvertRule.ToDate(this.txtEndDate.Value.Trim());
							dr["CompletePercent"] = 100;
							break;

						default:
							break;
					}

					dr["ModiDate"] = DateTime.Now;
					dr["ModiPerson"] = base.user.UserCode;

					DAL.EntityDAO.ProductDAO.SubmitAllBuildingFloorProgress(entity);
					entity.Dispose();
					//更新一个楼层的进度 end--------------------------------------
				}

				entityFloor.Dispose();

				//更新工作项的完成进度
				BLL.ConstructProgRule.UpdateTaskPercentByConstructProg(BuildingCode, WBSCode);
			}
			catch ( Exception ex )
			{
				throw ex;
			}
		}

		/// <summary>
		/// 有效性检查
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			if ((this.sltBuildingFloorIndexBegin.Value != "") && (this.sltBuildingFloorIndexEnd.Value == ""))
			{
				Hint = "请输入楼层范围的终止值";
				return false;
			}

			if ((this.sltBuildingFloorIndexBegin.Value == "") && (this.sltBuildingFloorIndexEnd.Value != ""))
			{
				Hint = "请输入楼层范围的起始值";
				return false;
			}

			if (this.sltTask.Value.Trim() == "")
			{
				Hint = "请输入工作";
				return false;
			}

			if ( this.sltStatus.Value.Trim() == "" )
			{
				Hint = "请输入状态 ！ ";
				return false;
			}

			if ( this.txtCompletePercent.Value != "" )
			{
				if ( ! Rms.Check.StringCheck.IsInt(txtCompletePercent.Value))
				{
					Hint = "完成百分比必须是整数 ！ ";
					return false;
				}

				int CompletePercent = BLL.ConvertRule.ToInt(this.txtCompletePercent.Value);
				if ((CompletePercent < 0) || (CompletePercent > 100))
				{
					Hint = "完成百分比必须位于 0 到 100 之间 ！ ";
					return false;
				}
			}

			return true;
		}

	}
}
