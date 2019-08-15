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


namespace RmsPM.Web.Project
{
	/// <summary>
	/// ProjectList ��ժҪ˵����
	/// </summary>
	public partial class ProjectList : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadDataGrid();
			}
		}

		private void IniPage()
		{
			try 
			{
				//Ȩ��
				this.btnAdd.Visible = base.user.HasRight("010102");

				BLL.PageFacade.LoadProjectStatusSelect(this.sltSearchStatus, true);
			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"������Ŀ�б�ҳ�����");
				Response.Write(Rms.Web.JavaScript.Alert(true, "������Ŀ�б�ҳ�����"));
			}
		}

		private void LoadDataGrid()
		{
			try
			{
				int kgYear = 0;
				int jgYear = 0;
				
				if (this.txtSearchKgYear.Value.Trim().Length>0)
				{
					kgYear = BLL.ConvertRule.ToInt(this.txtSearchKgYear.Value);
				}

				if (this.txtSearchJgYear.Value.Trim().Length>0)
				{
					jgYear = BLL.ConvertRule.ToInt(this.txtSearchJgYear.Value);
				}
				
				RmsPM.DAL.QueryStrategy.ProjectStrategyBuilder ssb= new RmsPM.DAL.QueryStrategy.ProjectStrategyBuilder();
				if ( this.txtSearchProjectName.Value.Length > 0 )
					ssb.AddStrategy( new Strategy(RmsPM.DAL.QueryStrategy.ProjectStrategyName.ProjectNameLike,this.txtSearchProjectName.Value));
				if ( kgYear != 0 )
					ssb.AddStrategy( new Strategy(RmsPM.DAL.QueryStrategy.ProjectStrategyName.kgYear,kgYear.ToString()));
				if ( jgYear != 0 )
					ssb.AddStrategy( new Strategy(RmsPM.DAL.QueryStrategy.ProjectStrategyName.jgYear,jgYear.ToString()));

				if (this.sltSearchStatus.Value != "")
					ssb.AddStrategy( new Strategy(RmsPM.DAL.QueryStrategy.ProjectStrategyName.Status,this.sltSearchStatus.Value));
				
				//����
				string sortsql = BLL.GridSort.GetSortSQL(ViewState);
				if (sortsql == "")
				{
					//ȱʡ����
					ssb.AddOrder("kgDate",false);
					ssb.AddOrder("ProjectName",true);
					//ssb.AddOrder("Status",true);
				}

				string sql = ssb.BuildMainQueryString();

				if (sortsql != "")
				{
					//���б�������
					sql = sql + " order by " + sortsql;
				}

				Rms.ORMap.QueryAgent qa = new QueryAgent();
				DataTable tb = qa.ExecSqlForDataSet(sql).Tables[0];
				qa.Dispose();
				
				this.dgList.DataSource = new DataView(tb,"","",DataViewRowState.CurrentRows);
				this.dgList.DataBind();

			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"������Ŀ�б����");
				Response.Write(Rms.Web.JavaScript.Alert(true, "������Ŀ�б����"));
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
			this.dgList.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemCreated);
			this.dgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgList_PageIndexChanged);
			this.dgList.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgList_SortCommand);

		}
		#endregion

		private void dgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.dgList.CurrentPageIndex = e.NewPageIndex;
			LoadDataGrid();
		}

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			this.dgList.CurrentPageIndex = 0;
			LoadDataGrid();
		}

		private void dgList_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			BLL.GridSort.SortCommand((DataGrid)source, ViewState, source, e);
			((DataGrid)source).CurrentPageIndex = 0;
			LoadDataGrid();
		}

		private void dgList_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			BLL.GridSort.ItemCreate((DataGrid)sender, ViewState, sender, e);
		}
	}
}
