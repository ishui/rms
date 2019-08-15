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
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.Systems
{
	/// <summary>
	/// DepartmentInfo 的摘要说明。
	/// </summary>
	public partial class DepartmentInfo : PageBase
	{


		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			
			if (!IsPostBack)
			{
				IniPage();
				LoadData();
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
			string unitCode=Request["UnitCode"] + "";
			this.txtAct.Value = Request.QueryString["act"];

			switch (this.txtAct.Value.ToLower()) 
			{
				case "view":
					//仅查看
					this.tableList.Style["display"] = "none";
					this.btnClose.Style["display"] = "";
					break;
			}

			if ( unitCode == "00000" )
			{
				this.btnDelete.Visible = false;
//				this.btnModify.Visible = false;
			}
		}

		private void LoadData()
		{
			string unitCode=Request["UnitCode"] + "";

			try
			{
				EntityData ds = OBSDAO.GetStandard_UnitByCode(unitCode);
               
				if (ds.HasRecord())
				{
					this.lblSortID.Text = ds.GetString("SortID");
					this.lblUnitType.Text = ds.GetString("UnitType");
					
					this.lblName.Text=ds.GetString("UnitName");
					this.lblPrincipal.Text = BLL.SystemRule.GetUserName( ds.GetString("Principal") );
					this.lblRemark.Text = ds.GetString("Remark");
					//this.lblSubjectSet.Text = BLL.SubjectRule.GetSubjectSetName(ds.GetString("SubjectSetCode"));
					if ( ds.GetInt("SelfAccount") == 1 )
						this.lblSelfAccount.Text = "独立核算";

					//显示财务编码
					this.lblSubjectSetDesc.Text = BLL.FinanceRule.GetFinanceSubjectSetDesc(ds.Tables["UnitSubjectSet"]);

                    if (ds.GetString("UnitType") == "项目")
                    {
                        this.btnDelete.Visible = false;
                        //this.btnModify.Visible = false;
                        //this.trToolBar.Style["display"] = "none";
                        btnModify.Attributes["onclick"] = "javascript:ModifyProject(" + ds.GetString("RelaCode") + ");return false;";
                        EntityData projectds = ProjectDAO.GetProjectByCode(ds.GetString("RelaCode"));
                        this.lblSubjectSet.Text = BLL.SubjectRule.GetSubjectSetName(projectds.GetString("SubjectSetCode"));
                    }
                    else
                    {
                        //如果是部门则隐掉帐套
                        lblSubjectsettd.Visible = false;
                        lblSelfAccount.Visible = false;
                        lblSubjectSet.Visible = false;
                        tdsubjectsetdesc.ColSpan = 3;
                    }

				}
				ds.Dispose();



				EntityData childUnit = DAL.EntityDAO.OBSDAO.GetOBSUnitByParent(unitCode);
				this.dgListChildUnit.DataSource = childUnit;
				this.dgListChildUnit.DataBind();
				childUnit.Dispose();

				EntityData station = DAL.EntityDAO.OBSDAO.GetStationByUnitCode(unitCode);
				station.CurrentTable.Columns.Add("RoleLevelName");
				int iCount = station.CurrentTable.Rows.Count;
				for ( int i=0;i<iCount;i++)
				{
					station.SetCurrentRow(i);
					int roleLevel = station.GetInt("RoleLevel");
					station.CurrentRow["RoleLevelName"] = BLL.SystemRule.GetRoleLevelName(roleLevel);
				}

				this.dgConfig.DataSource = station;
				this.dgConfig.DataBind();
				station.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示出错：" + ex.Message));
			}
		}

		private void CloseWindow( bool isClose  ) 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			if (this.txtRefreshScript.Value.Trim() != "") 
			{
				Response.Write( "window.opener." + this.txtRefreshScript.Value.Trim());
			}
			else 
			{
				Response.Write(Rms.Web.JavaScript.OpenerReload(false));
			}
			if ( isClose )
				Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{

			string unitCode=Request["UnitCode"] + "";

			try
			{
				bool canDelete = true;
//				EntityData role = DAL.EntityDAO.SystemManageDAO.GetOBSRoleByUnitCode(unitCode);
//				if ( role.HasRecord() )
//					canDelete = false;
//				role.Dispose();

				EntityData childDepartment = DAL.EntityDAO.OBSDAO.GetOBSUnitByParent(unitCode);
				if ( childDepartment.HasRecord())
					canDelete = false;
				childDepartment.Dispose();

				if ( ! canDelete )
				{
					Rms.Web.JavaScript.Alert( true, "这个部门下面设定有子部门和角色，不可删除 ！" );
					return;
				}

				EntityData ds=OBSDAO.GetUnitByCode(unitCode);
				OBSDAO.DeleteUnit(ds);
				ds.Dispose();
				CloseWindow( true );
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除出错"));
			}

		}
	}
}

