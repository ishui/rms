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
using Rms.Web;
using Rms.ORMap;
using RmsPM.DAL.EntityDAO;

namespace RmsPM.Web.Systems
{
	/// <summary>
	/// 
	/// </summary>
    public partial class UnitSelectProject : PageBase
	{
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if (!Page.IsPostBack) 
			{
				this.txtUnitCode.Value = Request["UnitCode"] + "";
				this.txtRefreshScript.Value = Request["RefreshScript"] + "";

				if (this.txtUnitCode.Value == "") 
				{
					this.lblMessage.Text = "无部门代码";
					return;
				}

				this.lblUnitName.Text = BLL.SystemRule.GetUnitName(this.txtUnitCode.Value);

				LoadDataGrid();
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
			this.btnOK.ServerClick += new System.EventHandler(this.btnOK_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/// <summary>
		/// 显示列表
		/// </summary>
		private void LoadDataGrid() 
		{
			try
			{
/*				string OwnName = this.txtSearchOwnName.Value.Trim();

				DAL.QueryStrategy.UserStrategyBuilder ssb = new DAL.QueryStrategy.UserStrategyBuilder();

				if ( OwnName.Length > 0 )
					ssb.AddStrategy( new Strategy(DAL.QueryStrategy.UserStrategyName.OwnName, OwnName)  );
				string sql = ssb.BuildMainQueryString();

				Rms.ORMap.QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData("SystemUser",sql);

				this.dgList.DataSource = entity.CurrentTable;
				this.dgList.DataBind();
				entity.Dispose();
				qa.Dispose();
*/

				EntityData entity = DAL.EntityDAO.ProjectDAO.GetAllProject();
				this.dgList.DataSource = entity.CurrentTable;
				this.dgList.DataBind();
				entity.Dispose();

//				LoadCheck();
			}
			catch ( Exception ex )
			{
				this.lblMessage.Text = "选择项目查询出错";
				ApplicationLog.WriteLog(this.ToString(), ex, "选择项目查询出错");
			}
		}

/*		private void LoadCheck() 
		{
			EntityData entity = DAL.EntityDAO.SystemManageDAO.GetUserUnitByUnitCode(this.txtUnitCode.Value);
			DataTable tb = entity.CurrentTable;

			for (int i=0;i<this.dgList.Items.Count;i++) 
			{
				HtmlInputCheckBox chk = (HtmlInputCheckBox)this.dgList.Items[i].FindControl("chk");
				if ( chk != null )
				{
					string UserCode = this.dgList.DataKeys[i].ToString();
					if (tb.Select("UserCode='" + UserCode + "'").Length > 0)
					{
						chk.Checked = true;
					}
					else 
					{
						chk.Checked = false;
					}
				}

			}

			entity.Dispose();
		}
*/
		/// <summary>
		/// 保存
		/// </summary>
		/// <returns>是否成功</returns>
		private bool Save() 
		{
			try
			{
				string UnitCode = this.txtUnitCode.Value;

				//读父节点
				EntityData parent = DAL.EntityDAO.OBSDAO.GetUnitByCode(UnitCode);
				int parentDeep = parent.GetInt("Deep") ;
				string parentFullCode = parent.GetString("FullCode");
				parent.Dispose();

				EntityData entity = DAL.EntityDAO.OBSDAO.GetOBSUnitByParent(UnitCode);
				DataTable tb = entity.CurrentTable;

				for(int i=0;i<this.dgList.Items.Count;i++)
				{
					HtmlInputCheckBox chk = (HtmlInputCheckBox)this.dgList.Items[i].FindControl("chk");
					if ( chk != null )
					{
						string ProjectCode = this.dgList.DataKeys[i].ToString();

						DataRow[] drs = tb.Select("UnitType='项目' and RelaCode='" + ProjectCode + "'");

						if (chk.Checked) 
						{
							//添加
							if (drs.Length == 0) 
							{
								EntityData entityProject = DAL.EntityDAO.ProjectDAO.GetProjectByCode(ProjectCode);
								if (entityProject.CurrentTable.Rows.Count > 0) 
								{

									EntityData entityNew = new EntityData("Unit");
									string tempCode =DAL.EntityDAO.SystemManageDAO.GetNewSysCode("UnitCode");

									DataRow dr = entityNew.GetNewRecord();

									dr["UnitCode"] =  tempCode ;
									dr["UnitName"] = entityProject.GetString("ProjectName");
									dr["UnitType"] = "项目";
									dr["RelaCode"] = ProjectCode;
									dr["ParentUnitCode"] = UnitCode;
									dr["Deep"] = parentDeep + 1;
									if ( parentFullCode != "" )
										dr["FullCode"] = parentFullCode + "-" + tempCode;
									else
										dr["FullCode"] = tempCode;

									entityNew.AddNewRecord(dr);
									DAL.EntityDAO.OBSDAO.InsertUnit(entityNew);

									//修改项目的部门号
									DataRow drProject = entityProject.CurrentTable.Rows[0];
									drProject["UnitCode"] = tempCode;
									DAL.EntityDAO.ProjectDAO.UpdateProject(entityProject);
								}
								entityProject.Dispose();

							}
						}
						else 
						{
							//删除
/*							if (drs.Length > 0) 
							{
								dr = drs[0];
								dr.Delete();
								DAL.EntityDAO.SystemManageDAO.DeleteUserUnit(entity);
							}
*/
						}
					}
				}

				entity.Dispose();

				return true;
			}
			catch(Exception ex )
			{
				this.lblMessage.Text = "添加项目失败";
				ApplicationLog.WriteLog(this.ToString(),ex,"添加项目失败");
				return false;
			}
		}

		private void btnOK_ServerClick(object sender, System.EventArgs e)
		{
			if (!Save())
			{
				return;
			}

			Response.Write(Rms.Web.JavaScript.ScriptStart);
			if (this.txtRefreshScript.Value.Trim() != "") 
			{
				Response.Write( "window.opener." + this.txtRefreshScript.Value.Trim());
			}
			else 
			{
				Response.Write(Rms.Web.JavaScript.OpenerReload(false));
			}
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
			Response.End();
		}

		private void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			LoadDataGrid();
		}

        protected void dgList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
}
}
