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
using RmsPM.DAL;
using RmsPM.DAL.QueryStrategy;
using RmsPM.BLL;
using Rms.Web;
using System.Text;

namespace RmsPM.Web.Systems
{
	/// <summary>
	/// User ��ժҪ˵����
	/// </summary>
	public partial class User : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if (!IsPostBack)
			{
				DefaultSet();
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
		#region ���Լ�����,��ʼ����
		private string ProjectCode
		{
			get
			{
				return Request["ProjectCode"] + "";
			}
		}
		private void DefaultSet()
		{
			LoadNewAllUserData();
			BindDataGrid();
			BT_ShFromResult.Visible=false;
		}
		#endregion		
		#region �û����ݲ���
		/// <summary>
		/// �������е�User��Ϣ
		/// </summary>
		private void LoadAllUserData()
		{
			if(Session["UserMessageCache"]==null)
			{
				Session["UserMessageCache"]=UserSystem.GetUserMessage(ProjectCode);
				//ataGrid1.DataBind();
			}
		}
		private void LoadNewAllUserData()
		{
			Session["UserMessageCache"]=UserSystem.GetUserMessage(ProjectCode);
		
		}
		/// <summary>
		/// ���û���������
		/// </summary>
		private void SortUserData()
		{
			LoadAllUserData();
			string sortsql = BLL.GridSort.GetSortSQL(ViewState);
			Session["UserMessageCache"]=UserSystem.SortUserMessage((DataView)Session["UserMessageCache"],sortsql);
			BindDataGrid();
		}
		/// <summary>
		/// �ڽ���н��в�ѯ
		/// </summary>
		private void SelectUserDataFromResult()
		{
			//GetSelectSql();
			try
			{
				Session["UserResultMessageCache"]=UserSystem.SearchFromUserMessageResult((DataView)Session["UserResultMessageCache"],GetSelectSql());
				BindDataGrid(((DataView)Session["UserResultMessageCache"]));
			}
			catch
			{
				SelectUserDataFromDB();
			}
		}
		/// <summary>
		/// �������в���
		/// </summary>
		private void SelectUserDataFromDB()
		{
			LoadAllUserData();
			Session["UserResultMessageCache"]=UserSystem.SearchFromUserMessageResult((DataView)Session["UserMessageCache"],GetSelectSql());
			BindDataGrid(((DataView)Session["UserResultMessageCache"]));
		}
		/// <summary>
		/// ���ɲ�ѯsql���
		/// </summary>
		private string GetSelectSql()
		{
			int i=0;
			StringBuilder sb = new StringBuilder();
			sb.Append(UserSystem.BinUserSelectSql("UserName",TB_UserName.Text.Trim(),ref i));	
			sb.Append(UserSystem.BinUserSelectSql("UserID",this.TB_LoginName.Text.Trim(),ref i));		
			sb.Append(UserSystem.BinUserSelectSql("stationNameHtml",this.txtUnitName.Value.Trim(),ref i));
			sb.Append(UserSystem.BinUserSelectSql("Phone",this.TB_Tel.Text.Trim(),ref i));		
			sb.Append(UserSystem.BinUserSelectSql("Mobile",this.TB_Mtel.Text.Trim(),ref i));			
			sb.Append(UserSystem.BinUserSelectSql("MailBox",this.TB_Email.Text.Trim(),ref i));
			sb.Append(UserSystem.BinUserSelectSql("SortID",this.TB_WorkCode.Text.Trim(),ref i));
			if(sltIsAllow.SelectedValue=="2")
			{				
			}
			else
			{
				if(i>0)
				{
					sb.Append(" and ");
				}				
				sb.Append("status = "+sltIsAllow.SelectedValue);			
			}
			//Response.Write(sb.ToString());
			return sb.ToString();		
		}
		#endregion

	/*	private void LoadDataGrid() 
		{

			try
			{
				//string ProjectCode = 

				UserStrategyBuilder ssb= new UserStrategyBuilder();
				if (ProjectCode != "")
					ssb.AddStrategy( new Strategy(UserStrategyName.ProjectCode,ProjectCode));
				
				//����
				string sortsql = BLL.GridSort.GetSortSQL(ViewState);
				if (sortsql == "")
				{
					//ȱʡ����
//					ssb.AddOrder("SortID",true);
//					ssb.AddOrder("UserName",true);
				}

				string sql = ssb.BuildMainQueryString();

				if (sortsql != "")
				{
					//���б�������
					sql = sql + " order by " + sortsql;
				}
				else 
				{
					//ȱʡ����
					sql = sql + ssb.GetDefaultOrder();
				}

				Rms.ORMap.QueryAgent qa = new QueryAgent();
				DataTable tb = qa.ExecSqlForDataSet(sql).Tables[0];
				qa.Dispose();

				this.dgList.DataSource = tb;
				this.dgList.DataBind();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�����û��б�ҳ�����"));
			}
		}*/

		#region dataGrid���Ʋ���
		private void BindDataGrid()
		{
			if(Session["UserMessageCache"]==null)
			{
				LoadAllUserData();
			}
			//(DataView)Session["UserMessageCache"];
			BindDataGrid((DataView)Session["UserMessageCache"]);
		}
		private void BindDataGrid(DataView dv)
		{
			dgList.DataSource = dv;
			dgList.DataBind();
		}
		private void dgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			int index = e.NewPageIndex;
			this.dgList.CurrentPageIndex = index;
			BindDataGrid();
		}

		private void dgList_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			BLL.GridSort.SortCommand((DataGrid)source, ViewState, source, e);
			((DataGrid)source).CurrentPageIndex = 0;
			SortUserData();
		}

		private void dgList_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			BLL.GridSort.ItemCreate((DataGrid)sender, ViewState, sender, e);
		}
		#endregion

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			SelectUserDataFromDB();
		}

		protected void BT_ShFromResult_ServerClick(object sender, System.EventArgs e)
		{
			SelectUserDataFromResult();
		}


        protected void Button1_Click(object sender, EventArgs e)
        {
           // Response.Redirect("~/SystemUsers/ImportUsers.aspx");
        }
}
}
