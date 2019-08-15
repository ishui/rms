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

using Rms.Web;
using Rms.ORMap;
using RmsPM.DAL;
using RmsPM.BLL;
using RmsPM.Web;
using RmsPM.DAL.EntityDAO;

namespace RmsPM.Web.Remind
{
	/// <summary>
	/// TestDesktop 的摘要说明。
	/// </summary>
	public partial class TestDesktop : System.Web.UI.Page
	{
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if (!this.IsPostBack)
			{
				InitPage();
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void InitPage()
		{
			User objUser = (User)Session["User"];
			//通知
			EntityData entityNotice = RemindDAO.GetNoticeUserByUserCode(objUser.UserCode);
			if (entityNotice.HasRecord())
			{
				HtmlTable m_Table = this.tbNotice;
				HtmlTableRow m_Row;
				HtmlTableCell m_Cell;

				foreach(DataRow dr in entityNotice.CurrentTable.Rows)
				{
					m_Row = new HtmlTableRow();

					m_Cell = new HtmlTableCell();
					if (BLL.NoticeRule.GetFieldName(dr["NoticeCode"].ToString(),"Type") != "1")
					{
						m_Cell.InnerHtml = "<a href='#' onclick ='OpenNotice(" + dr["NoticeCode"].ToString() + ");return false;'>" + BLL.NoticeRule.GetNoticeName(dr["NoticeCode"].ToString()) + "</a>";
					}
					else
					{
						m_Cell.InnerHtml = "<a href='#' onclick =OpenTask('" + BLL.NoticeRule.GetFieldName(dr["NoticeCode"].ToString(),"RelatedHref")  + "');return false;>" + BLL.NoticeRule.GetNoticeName(dr["NoticeCode"].ToString()) + "</a>";
					}
					m_Row.Cells.Add(m_Cell);
					m_Table.Rows.Add(m_Row);
				}

			}
			entityNotice.Dispose();

			

//			EntityData entity = RemindDAO.GetAllRemindStrategy();
//
//			if (entity.HasRecord())
//			{
//				foreach(DataRow dr in entity.CurrentTable.Rows)
//				{
//					//获取所有超期工作
//					if (ds.Tables[0].Rows.Count > 0)
//					{
//						DataTable dt = DisposeTask(ds.Tables[0]);
//						string m_Condition = "";
//						m_Condition = " ((Status = 0 and PlannedStartDate <'" + DateTime.Now.ToShortDateString() + "')";
//						m_Condition += " or (Status <> 4 and PlannedFinishDate >'" + DateTime.Now.ToShortDateString() + "'))";
//						DateView dv = new DataView(dt,m_Condition,"",DataViewRowState.CurrentRows);
//					}
//
//
//				}
//					break;
//			}
			//EntityData entityNotice = RemindDAO.GetAllNotice();
		}

		private DataTable DisposeTask(System.Data.DataTable dtTask)
		{
			try
			{
				DataTable dtNew = dtTask.Copy();
				dtNew.Columns.Add("StatusName");
				dtNew.Columns.Add("ImportantName");
				dtNew.Columns.Add("Master");

				string[] strStatusList = {"未开始","进行中","暂停","取消","已完成"};
				string[] strImportantList = {"一般","重要"};
				for ( int i = 0 ; i < dtNew.Rows.Count ; i++)
				{
					dtNew.Rows[i]["StatusName"] = (dtNew.Rows[i]["Status"] == System.DBNull.Value)?"":strStatusList[int.Parse(dtNew.Rows[i]["Status"].ToString())];
					dtNew.Rows[i]["ImportantName"] = (dtNew.Rows[i]["ImportantLevel"] == System.DBNull.Value)?"":strImportantList[int.Parse(dtNew.Rows[i]["ImportantLevel"].ToString())];

					DAL.QueryStrategy.UserStrategyBuilder USB = new RmsPM.DAL.QueryStrategy.UserStrategyBuilder();
					USB.AddStrategy(new Strategy(DAL.QueryStrategy.UserStrategyName.WBSCode,dtNew.Rows[i]["WBSCode"].ToString()));
					QueryAgent QA = new QueryAgent();
					string sql = USB.BuildMainQueryString();

					DataSet ds = QA.ExecSqlForDataSet(sql);
					if (ds.Tables[0].Rows.Count > 0)
					{
						dtNew.Rows[i]["Master"] = ds.Tables[0].Rows[0]["UserName"];
					}
					else
					{
						dtNew.Rows[i]["Master"] = "";
					}
					QA.Dispose();
				}
				return dtNew;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return null;
			}
		}

	}
}
