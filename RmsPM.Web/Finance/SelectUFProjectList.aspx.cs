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
using RmsPM.DAL.QueryStrategy;
using RmsPM.DAL.EntityDAO;
using Rms.Web;

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// SelectUFProjectList ��ժҪ˵����
	/// </summary>
	public partial class SelectUFProjectList : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton btnSelectHidden;
		protected System.Web.UI.WebControls.RadioButtonList rbType;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
				LoadDataGrid(true);
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

		}
		#endregion

		private void IniPage() 
		{
			try 
			{
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		private void LoadDataGrid(bool isEmpty) 
		{
			try 
			{
				DataTable tb;

				UFProjectStrategyBuilder sb = new UFProjectStrategyBuilder();

				if (isEmpty)
					sb.AddStrategy(new Strategy(UFProjectStrategyName.False));

				string UFProjectName = this.txtSearchUFProjectName.Value.Trim();
				if (UFProjectName != "")
					sb.AddStrategy(new Strategy(UFProjectStrategyName.UFProjectName, UFProjectName));

				sb.AddOrder("UFProjectName", true);

				string sql = sb.BuildMainQueryString();

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "UFProject",sql );
				tb = entity.CurrentTable;
				qa.Dispose();
				entity.Dispose();

				dgList.DataSource = tb;
				dgList.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		private void dgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.dgList.CurrentPageIndex = e.NewPageIndex;
			LoadDataGrid(false);
		}

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			this.dgList.CurrentPageIndex = 0;
			LoadDataGrid(false);
		}

	}
}