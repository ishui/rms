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

namespace RmsPM.Web.PBS
{
	/// <summary>
	/// SelectRoom 的摘要说明。
	/// </summary>
	public partial class SelectRoom : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlSelect sltModelCode;
		protected System.Web.UI.HtmlControls.HtmlSelect sltPBSTypeCode;
		protected System.Web.UI.HtmlControls.HtmlSelect sltInvState;
		protected System.Web.UI.HtmlControls.HtmlSelect sltOutState;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
				LoadEmptyDataGrid();
			}

			//选择确定后，调用父窗口的接收函数
			string s = Rms.Web.JavaScript.ScriptStart;
			s += "function ReturnToParentWindow(code)" + "\n";
			s += "{" + "\n";
			s += "window.opener." + this.txtReturnFunc.Value + "(code);" + "\n";
			s += "}" + "\n";
			s += Rms.Web.JavaScript.ScriptEnd;
			Page.RegisterStartupScript("start", s);
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
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtReturnFunc.Value = Request["ReturnFunc"];
				this.txtDefaultInvState.Value = Request["InvState"];
				this.txtDefaultPBSTypeCode.Value = Request["PBSTypeCode"];

				if (this.txtReturnFunc.Value == "") 
				{
					this.txtReturnFunc.Value = "SelectRoomReturn";
				}

				((SearchRoom)this.tbSearchRoom).SetProject(this.txtProjectCode.Value);
				((SearchRoom)this.tbSearchRoom).SetDefaultInvState(this.txtDefaultInvState.Value);
				((SearchRoom)this.tbSearchRoom).SetDefaultPBSTypeCode(this.txtDefaultPBSTypeCode.Value);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadEmptyDataGrid() 
		{
			try 
			{
				EntityData entity = new EntityData("V_ROOM");
				dgList.DataSource = entity;
				dgList.DataBind();
				entity.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadDataGrid() 
		{
			try 
			{
				RoomStrategyBuilder sb = new RoomStrategyBuilder("V_ROOM");

				string ProjectCode = this.txtProjectCode.Value;
				if (ProjectCode != "")
					sb.AddStrategy( new Strategy( RoomStrategyName.ProjectCode, ProjectCode));

				if (this.tbSearchRoom.Visible) 
				{
					((SearchRoom)this.tbSearchRoom).AddSearch(sb);
				}

				sb.AddOrder("BuildingName", true);
				sb.AddOrder("ChamberCode", true);
				sb.AddOrder("FloorIndex", true);
				sb.AddOrder("RoomName", true);

				string sql = sb.BuildMainQueryString();

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "V_ROOM",sql );
				qa.Dispose();

				dgList.Columns[2].FooterText = entity.CurrentTable.Rows.Count.ToString() + " 条";
				dgList.Columns[5].FooterText = BLL.MathRule.SumColumn(entity.CurrentTable,"BuildArea").ToString("0.####");
				dgList.DataSource = entity;
				dgList.DataBind();
				entity.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			this.dgList.CurrentPageIndex = 0;
			LoadDataGrid();
		}

		private void dgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.dgList.CurrentPageIndex = e.NewPageIndex;
			LoadDataGrid();
		}

	}
}
