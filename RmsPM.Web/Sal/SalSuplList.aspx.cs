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


namespace RmsPM.Web.Sal
{
	/// <summary>
	/// Supplier ��ժҪ˵����
	/// </summary>
	public partial class SalSuplList : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if (!IsPostBack)
			{
				IniPage();
				LoadDataGrid();
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
				BLL.PageFacade.LoadProjectSelect(this.sltSearchProject, this.txtProjectCode.Value);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadDataGrid() 
		{
			try 
			{
				SalSuplStrategyBuilder sb = new SalSuplStrategyBuilder();

				string ProjectCode = this.sltSearchProject.Value.Trim();
				if (ProjectCode != "")
					sb.AddStrategy( new Strategy( SalSuplStrategyName.ProjectCode, ProjectCode));

				string SuplCode = this.txtSearchSuplCode.Value.Trim();
				if (SuplCode != "")
					sb.AddStrategy(new Strategy(SalSuplStrategyName.SuplCode, SuplCode));

				string SuplName = this.txtSearchSuplName.Value.Trim();
				if (SuplName != "")
					sb.AddStrategy(new Strategy(SalSuplStrategyName.SuplName, SuplName));

				sb.AddOrder("SuplCode", true);

				string sql = sb.BuildMainQueryString();

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "SalSupl",sql );
				qa.Dispose();

//				EntityData entity = DAL.EntityDAO.SalDAO.GetSalSuplByProjectCode(this.txtProjectCode.Value);

				dgList.DataSource = entity;
				dgList.DataBind();
				entity.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
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
				btnDelete.Attributes.Add("onclick", "javascript:if(!window.confirm('ȷʵҪɾ��������¼��')) return false;");
			}
		}

		private void DeleteSalSupl( string code )
		{
			EntityData entity = DAL.EntityDAO.SalDAO.GetSalSuplByCode(code);
			DAL.EntityDAO.SalDAO.DeleteSalSupl(entity);
			entity.Dispose();
		}

		private void dgList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string code = this.dgList.DataKeys[e.Item.ItemIndex].ToString();

			try 
			{
				DeleteSalSupl(code);
				LoadDataGrid();

//				Response.Write(JavaScript.ScriptStart);
//				Response.Write("window.location = window.location;");
//				Response.Write(JavaScript.ScriptEnd);
			}
			catch(Exception ex) 
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(JavaScript.Alert(true, "ɾ��ʧ�ܣ�" + ex.Message));
			}
		}

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			this.dgList.CurrentPageIndex = 0;
			LoadDataGrid();
		}
	}
}
