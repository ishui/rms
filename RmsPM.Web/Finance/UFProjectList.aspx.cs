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
using RmsPM.BLL;
using RmsPM.DAL.EntityDAO;
using RmsPM.Web;
using Rms.ORMap;
using Rms.Web;
using RmsPM.DAL.QueryStrategy;


namespace RmsPM.Web.Finance
{
	/// <summary>
	/// UFProjectList 的摘要说明。
	/// </summary>
	public partial class UFProjectList : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if (!IsPostBack)
			{
				IniPage();
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
			this.dgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgList_PageIndexChanged);
			this.dgList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgList_DeleteCommand);
			this.dgList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemDataBound);

		}
		#endregion

		private void IniPage() 
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
//				BLL.PageFacade.LoadProjectSelect(this.sltSearchProject, this.txtProjectCode.Value);
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
				UFProjectStrategyBuilder sb = new UFProjectStrategyBuilder();

				string UFProjectName = this.txtSearchUFProjectName.Value.Trim();
				if (UFProjectName != "")
					sb.AddStrategy(new Strategy(UFProjectStrategyName.UFProjectName, UFProjectName));

				sb.AddOrder("UFProjectName", true);

				string sql = sb.BuildMainQueryString();

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "UFProject",sql );
				qa.Dispose();

				dgList.DataSource = entity;
				dgList.DataBind();
				entity.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示列表出错：" + ex.Message));
			}
		}

		private void dgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.dgList.CurrentPageIndex = e.NewPageIndex;
			LoadDataGrid();
		}

		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			LinkButton btnDelete;

			if ((e.Item.ItemType == ListItemType.Item)
				|| (e.Item.ItemType == ListItemType.EditItem)
				|| (e.Item.ItemType == ListItemType.AlternatingItem)
				)
			{
				btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
				btnDelete.Attributes.Add("onclick", "javascript:if(!window.confirm('确实要删除这条记录吗？')) return false;");
			}
		}

		private void DeleteUFProject( string code )
		{
			EntityData entity = DAL.EntityDAO.PaymentDAO.GetUFProjectByCode(code);
			DAL.EntityDAO.PaymentDAO.DeleteUFProject(entity);
			entity.Dispose();
		}

		private void dgList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string code = this.dgList.DataKeys[e.Item.ItemIndex].ToString();

			try 
			{
				DeleteUFProject(code);
				LoadDataGrid();

//				Response.Write(JavaScript.ScriptStart);
//				Response.Write("window.location = window.location;");
//				Response.Write(JavaScript.ScriptEnd);
			}
			catch(Exception ex) 
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(JavaScript.Alert(true, "删除失败：" + ex.Message));
			}
		}

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			this.dgList.CurrentPageIndex = 0;
			LoadDataGrid();
		}
	}
}
