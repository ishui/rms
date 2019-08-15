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
	/// User 的摘要说明。
	/// </summary>
	public partial class User : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if (!IsPostBack)
			{
				DefaultSet();
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
			this.dgList.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemCreated);
			this.dgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgList_PageIndexChanged);
			this.dgList.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgList_SortCommand);

		}
		#endregion
		#region 属性及索引,初始设置
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
		#region 用户数据操作
		/// <summary>
		/// 加载所有的User信息
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
		/// 对用户进行排序
		/// </summary>
		private void SortUserData()
		{
			LoadAllUserData();
			string sortsql = BLL.GridSort.GetSortSQL(ViewState);
			Session["UserMessageCache"]=UserSystem.SortUserMessage((DataView)Session["UserMessageCache"],sortsql);
			BindDataGrid();
		}
		/// <summary>
		/// 在结果中进行查询
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
		/// 从数据中查找
		/// </summary>
		private void SelectUserDataFromDB()
		{
			LoadAllUserData();
			Session["UserResultMessageCache"]=UserSystem.SearchFromUserMessageResult((DataView)Session["UserMessageCache"],GetSelectSql());
			BindDataGrid(((DataView)Session["UserResultMessageCache"]));
		}
		/// <summary>
		/// 生成查询sql语句
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
				
				//排序
				string sortsql = BLL.GridSort.GetSortSQL(ViewState);
				if (sortsql == "")
				{
					//缺省排序
//					ssb.AddOrder("SortID",true);
//					ssb.AddOrder("UserName",true);
				}

				string sql = ssb.BuildMainQueryString();

				if (sortsql != "")
				{
					//点列标题排序
					sql = sql + " order by " + sortsql;
				}
				else 
				{
					//缺省排序
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "加载用户列表页面错误。"));
			}
		}*/

		#region dataGrid控制操作
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
