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
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;

namespace RmsPM.Web.Systems
{
	/// <summary>
	/// TaskAccessModify 的摘要说明。
	/// </summary>
	public partial class TaskAccessModify : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTextArea txtDescription;
		protected System.Web.UI.HtmlControls.HtmlInputText txtGroupName;
		protected System.Web.UI.WebControls.Label lblGroupName;
		protected System.Web.UI.UserControl ucGroupTree;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)	
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage() 
		{
			try 
			{
//				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
				this.txtWBSCode.Value = Request.QueryString["WBSCode"];
				this.txtAct.Value = Request.QueryString["Action"];
				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
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
				string WBSCode = this.txtWBSCode.Value;

				if (WBSCode == "")
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "未传入工作项代码"));
					return;
				}

				EntityData entity = RmsPM.DAL.EntityDAO.WBSDAO.GetTaskByCode(WBSCode);
				if (entity.HasRecord())
				{
					this.lblTaskName.Text = entity.GetString("SortID").ToString() + " " + entity.GetString("TaskName");
					this.txtParentCode.Value = entity.GetString("ParentCode");
					this.txtProjectCode.Value = entity.GetString("ProjectCode");
					this.lblProjectName.Text = BLL.ProjectRule.GetProjectName(this.txtProjectCode.Value);
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "工作项不存在"));
					return;
				}
				entity.Dispose();

				LoadAccessRange();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示类别出错：" + ex.Message));
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
		/// 显示权限列表
		/// </summary>
		private void LoadAccessRange() 
		{
			try 
			{
				string WBSCode = this.txtWBSCode.Value;

				DataTable tb = BLL.WBSRule.GetTaskPersonStructure();

				tb.Columns.Add("UserCodes");
				tb.Columns.Add("StationCodes");

				//工作人员
				EntityData entityP = DAL.EntityDAO.WBSDAO.GetTaskPersonByWBSCode(WBSCode);
				DataTable tbP = entityP.CurrentTable;
				entityP.Dispose();

				foreach(DataRow dr in tb.Rows) 
				{
					string Type = dr["Type"].ToString();
					DataView dv = new DataView(tbP, "Type='" + Type + "'", "", DataViewRowState.CurrentRows);

					//按岗位、人员汇总
					string UserCodes = "";
					string StationCodes = "";
					BLL.SystemRule.GetStationUserGroup(dv, "RoleType", "UserCode", ",0", "1", ref UserCodes, ref StationCodes);

					dr["UserCodes"] = UserCodes;
					dr["StationCodes"] = StationCodes;
				}

				BindDataGrid(tb);
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示列表出错：" + ex.Message));
			}
		}

		private void BindDataGrid(DataTable tb)
		{
			try 
			{
				DataView dv = new DataView(tb, "", "SortID", DataViewRowState.CurrentRows);

				this.dgList.DataSource = dv;
				this.dgList.DataBind();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示列表出错：" + ex.Message));
			}
		}

		/// <summary>
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
		/// 保存
		/// </summary>
		private void SaveData()
		{
			try
			{
				string WBSCode = this.txtWBSCode.Value;

				EntityData entity = DAL.EntityDAO.WBSDAO.GetTaskPersonByWBSCode(WBSCode);
				DataTable tb = entity.CurrentTable;

				foreach(DataGridItem item in this.dgList.Items) 
				{
					string Type = this.dgList.DataKeys[item.ItemIndex].ToString();
					RmsPM.Web.UserControls.InputStationUser ucPerson = (RmsPM.Web.UserControls.InputStationUser)item.FindControl("ucPerson");

					string[] arrUserCode = ucPerson.UserCodes.Split(",".ToCharArray());
					string[] arrStationCode = ucPerson.StationCodes.Split(",".ToCharArray());

					DataView dv = new DataView(tb, "Type='" + Type + "'", "Type", DataViewRowState.CurrentRows);

					//删除未定义的人员
                    for(int i=dv.Count-1;i>=0;i--)
					{
						DataRow dr = dv[i].Row;

						int RoleType = BLL.ConvertRule.ToInt(dr["RoleType"]);
						string Code = BLL.ConvertRule.ToString(dr["UserCode"]);

						if (RoleType == 0) 
						{
							//删除用户
							if ((Code == "") || (BLL.ConvertRule.FindArray(arrUserCode, Code) < 0)) 
							{
								dr.Delete();
							}
						}
						else if (RoleType == 1)
						{
							//删除角色
							if ((Code == "") || (BLL.ConvertRule.FindArray(arrStationCode, Code) < 0))
							{
								dr.Delete();
							}
						}
					}

					//添加新增的人员
					foreach(string Code in arrUserCode) 
					{
						if (Code != "") 
						{
							TaskPersonNewRow(tb, Type, 0, Code, WBSCode);
						}
					}

					//添加新增的岗位
					foreach(string Code in arrStationCode) 
					{
						if (Code != "") 
						{
							TaskPersonNewRow(tb, Type, 1, Code, WBSCode);
						}
					}
				}

				DAL.EntityDAO.WBSDAO.SubmitAllTaskPerson(entity);
				entity.Dispose();
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		private DataRow TaskPersonNewRow(DataTable tb, string Type, int RoleType, string RelationCode, string WBSCode) 
		{
			try 
			{
				DataRow drNew = null;

				if ((RelationCode == "") || (Type == "") || (WBSCode == ""))
					return drNew;

				string filter = string.Format("Type='{0}' and isnull(RoleType,0)={1} and UserCode='{2}' and isnull(ExecuteCode, '')=''", Type, RoleType, RelationCode);
				if (tb.Select(filter).Length == 0) 
				{
					drNew = tb.NewRow();

					drNew["TaskPersonCode"] = SystemManageDAO.GetNewSysCode("TaskPerson");
					drNew["Type"] = Type;
					drNew["WBSCode"] = WBSCode;
					drNew["RoleType"] = RoleType;
					drNew["UserCode"] = RelationCode;
					drNew["ExecuteCode"] = "";

					tb.Rows.Add(drNew);
				}

				return drNew;
			}
			catch(Exception ex)
			{
				throw ex;
			}
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

				SaveData();
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "保存失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return;
			}

			GoBack();
		}

	}
}
