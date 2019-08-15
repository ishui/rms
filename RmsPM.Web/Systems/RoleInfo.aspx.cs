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
	/// RoleInfo 的摘要说明。
	/// </summary>
	public partial class RoleInfo : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTable tableList;


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
			this.txtRefreshScript.Value = Request["RefreshScript"] + "";
			string roleCode = Request["RoleCode"] + "";
		}

        private string BuildShowCode(int deep)
        {
            string s = "";
            for (int i = 0; i < 4 * deep; i++)
                s += "&nbsp;";

            return s;
        }

		private void LoadData()
		{
            ///////////权限
            string mainCode = "%";
			string roleCode = Request["RoleCode"]+"";
			try
			{
				EntityData role = DAL.EntityDAO.SystemManageDAO.GetStandard_RoleByCode(roleCode);

				FunctionStructureStrategyBuilder sb = new FunctionStructureStrategyBuilder();
				sb.AddStrategy ( new Strategy( FunctionStructureStrategyName.ChildTreeNode,mainCode )  );
				sb.AddStrategy ( new Strategy( FunctionStructureStrategyName.IsAvailable,"0" )  );
				sb.AddStrategy ( new Strategy( FunctionStructureStrategyName.IsRightControlPoint,"0" )  );
				sb.AddStrategy ( new Strategy( FunctionStructureStrategyName.IsRoleControlPoint,"0" )  );

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData("FunctionStructure",sb.BuildMainQueryString());
                
				entity.CurrentTable.Columns.Add( "ShowCode" );
				qa.Dispose();

				string allCode = "";
				string aCode = "";

				int iCount = entity.CurrentTable.Rows.Count;
				for ( int i=0;i<iCount;i++)
				{
					entity.SetCurrentRow(i);
					string code = entity.GetString( "FunctionStructureCode" );
					int deep = entity.GetInt("Deep");
					entity.CurrentRow["ShowCode"] = BuildShowCode(deep);

					int iA = entity.GetInt( "IsAvailable" );
					if ( allCode != "" )
						allCode+=",";

					allCode += code;
				}

				role.SetCurrentTable("RoleOperation");
				foreach ( DataRow dr in role.CurrentTable.Select( String.Format( " OperationCode like '{0}%' ",mainCode ) ))
				{
					string code = (string)dr["OperationCode"];
					if ( aCode != "" )
						aCode += ",";
					aCode +=string.Format("'{0}'", code);
				}
                aCode = " FunctionStructureCode in (" + aCode + ") ";
                DataView dv = new DataView(entity.Tables["FunctionStructure"]);
                dv.RowFilter = aCode;
				this.rptFunction.DataSource = dv;
				this.rptFunction.DataBind();
				entity.Dispose();
				role.Dispose();
            }
            catch (Exception ex)
            {
                //ApplicationLog.WriteLog(this.ToString(), ex, "");
                //Response.Write(Rms.Web.JavaScript.Alert(true, "显示权限时发生出错"));
            }
            //////////////////////////////////

			try
			{
				EntityData ds=SystemManageDAO.GetRoleByCode(roleCode);
				if (ds.HasRecord())
				{
					this.lblRoleName.Text=ds.GetString("RoleName");
					this.lblDescription.Text = ds.GetString("Description");
                    sortID.Text =ds.GetString("sortid");
                    
				}
				ds.Dispose();

				EntityData entity = OBSDAO.GetStationByRoleCode(roleCode);
				this.dgList.DataSource = entity.CurrentTable;
				this.dgList.DataBind();
				entity.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示出错"));
			}
		}


		private void WriteRefreshScript(  ) 
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
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			string RoleCode = Request["RoleCode"] + "" ;
			try
			{
				EntityData entity = DAL.EntityDAO.SystemManageDAO.GetStandard_RoleByCode(RoleCode);
				DAL.EntityDAO.SystemManageDAO.DeleteStandard_Role(entity);
				entity.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除出错"));
				return;
			}

			WriteRefreshScript();
		}

		protected void btnAdd_ServerClick(object sender, System.EventArgs e)
		{
			string projectCode = Request["ProjectCode"]+"";
			Response.Redirect("RoleModify.aspx?ProjectCode=" + projectCode);
		}


	}
}

