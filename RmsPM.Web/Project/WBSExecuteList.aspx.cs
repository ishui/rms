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

namespace RmsPM.Web.Project
{
	/// <summary>
	/// WBSExecuteList ��ժҪ˵����
	/// </summary>
	public partial class WBSExecuteList : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!this.IsPostBack)
			{
				InitPage();
				LoadDataGrid();
			}
		}

		/// <summary>
		/// ��ʼ��ҳ��
		/// </summary>
		private void InitPage()
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		/// <summary>
		/// ��ʼ��������Ϣҳ��
		/// </summary>
		private void LoadDataGrid()
		{
			try 
			{
				TaskExecuteStrategyBuilder sb = new TaskExecuteStrategyBuilder();

				sb.AddStrategy( new Strategy( DAL.QueryStrategy.TaskExecuteStrategyName.ProjectCode,this.txtProjectCode.Value));

				ArrayList arA = new ArrayList();
				arA.Add("070202");
				arA.Add(user.UserCode);
				arA.Add(user.BuildStationCodes());
				sb.AddStrategy( new Strategy( DAL.QueryStrategy.TaskExecuteStrategyName.AccessRange,arA));

				if(this.dtStartDate.Value.Length>0||this.dtEndDate.Value.Length>0)
				{
					ArrayList arB = new ArrayList();
					arB.Add(this.dtStartDate.Value);
					arB.Add(this.dtEndDate.Value);
					sb.AddStrategy( new Strategy( DAL.QueryStrategy.TaskExecuteStrategyName.ExecuteDate,arB));
				}

				if(this.txtTitle.Value.Length>0)
					sb.AddStrategy( new Strategy( DAL.QueryStrategy.TaskExecuteStrategyName.Detail,this.txtTitle.Value));

				sb.AddOrder("ExecuteDate",false);

				string sql = sb.BuildQueryViewString();
				QueryAgent qa = new QueryAgent();

				EntityData entity = qa.FillEntityData("TaskExecute",sql);
				qa.Dispose();
			
				this.trNoExecute.Visible = !entity.HasRecord();

				this.dgList.DataSource = entity.CurrentTable;
				this.dgList.DataBind();
				entity.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
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
