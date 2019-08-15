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
using RmsPM.DAL.EntityDAO;
using Rms.ORMap;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.Remind
{
	/// <summary>
	/// NoticeList : 显示通知的列表。
	/// <author>unm</author>
	/// <date>2004/11/15</date>
	/// <version>1.5</version>
	/// </summary>
	public partial class NoticeList : PageBase
	{
		protected string strRole = "";
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				// 在此处放置用户代码以初始化页面
				if (!this.IsPostBack)
				{
					InitPage();
					LoadData();
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"载入通知失败");
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

		private void InitPage()
        {   //新增个性化判断，测试通过删除此注释．
            if (this.up_sPMNameLower == "tianyangoa")
            {
                this.lblNoticeClass.Visible = true;
                this.DDLNoticeClass.Visible = true;
                RmsPM.BLL.PageFacade.LoadDictionarySelect(this.DDLNoticeClass, "通知类型", "");//通知类别改成通知类型
            }
            else
            {
                this.lblNoticeClass.Visible = false;
                this.DDLNoticeClass.Visible = false;
            }

		}

		private void LoadData()
		{
            string DocType = Request["DocType"].ToString()+"";
            

            
            NoticeStrategyBuilder noticeBuilder = new NoticeStrategyBuilder();
            noticeBuilder.AddStrategy(new Strategy(DAL.QueryStrategy.NoticeStrategyName.status, "1"));
            if (DocType!="")
                noticeBuilder.AddStrategy(new Strategy(DAL.QueryStrategy.NoticeStrategyName.Type, DocType));


                if (!user.HasRight("080105"))//判断通知监控
                {
                    ArrayList arA = new ArrayList();
                    arA.Add("080104");
                    arA.Add(user.UserCode);
                    arA.Add(user.BuildStationCodes());
                    noticeBuilder.AddStrategy(new Strategy(DAL.QueryStrategy.NoticeStrategyName.AccessRange, arA));
                }
                
                //查询通知
                string title = this.TB_NoticeTitle.Text.Trim();
                if (title.Length > 0)
                {
                    noticeBuilder.AddStrategy(new Strategy(DAL.QueryStrategy.NoticeStrategyName.Title, "%" + title + "%"));
                }
                string name = this.SP_Notice.Value.Trim();
                if (name.Length > 0)
                {   

                    noticeBuilder.AddStrategy(new Strategy(DAL.QueryStrategy.NoticeStrategyName.SubmitPerson,  name));
                }

                if (this.dtNoticeDateBegin.Value != "" || this.dtNoticeDateEnd.Value != "")
                {
                     ArrayList al = new ArrayList();
                     al.Add(this.dtNoticeDateBegin.Value);
                     al.Add(this.dtNoticeDateEnd.Value);
                     noticeBuilder.AddStrategy(new Strategy(DAL.QueryStrategy.NoticeStrategyName.SubmitDate, al));
                }
                string noticeClass = this.DDLNoticeClass.Value.Trim();
                if (noticeClass.Length > 0)
                {
                    noticeBuilder.AddStrategy(new Strategy(DAL.QueryStrategy.NoticeStrategyName.NoticeClass,noticeClass));
                }
                
                noticeBuilder.AddOrder("SubmitDate", false);
			    string sql = noticeBuilder.BuildMainQueryString();
               
			    QueryAgent qa = new QueryAgent();			
			    EntityData entityNotice = qa.FillEntityData("Notice",sql);
			    qa.Dispose();

                entityNotice.CurrentTable.Columns.Add("NoticeClassTitle");//改动
                for (int i = 0; i < entityNotice.CurrentTable.Rows.Count; i++)
                {       //改动
                    if (entityNotice.CurrentTable.Rows[i]["NoticeClass"].ToString() != string.Empty)
                    {
                        entityNotice.CurrentTable.Rows[i]["NoticeClassTitle"] = entityNotice.CurrentTable.Rows[i]["NoticeClass"].ToString() + ":" + entityNotice.CurrentTable.Rows[i]["Title"].ToString();
                    }
                    else
                    {
                        entityNotice.CurrentTable.Rows[i]["NoticeClassTitle"] = "" + entityNotice.CurrentTable.Rows[i]["Title"].ToString();

                    }
                    //改动
                    string strTmp = entityNotice.CurrentTable.Rows[i]["NoticeClassTitle"].ToString();
                    if (strTmp.Length > 20)
                        entityNotice.CurrentTable.Rows[i]["NoticeClassTitle"] = strTmp.Substring(0, 20) + "...";//改动
                }
			    this.dgNoticeList.DataSource= DisPoseNotice(entityNotice.CurrentTable);
			    this.dgNoticeList.DataBind();
            
            if (DocType == "99")
            {
                lblTitle.InnerText  = "公告";
                ((System.Web.UI.HtmlControls.HtmlInputButton)this.Form1.FindControl("btnAddNew")).Value = "新增公告";
            }
            else
            {
                lblTitle.InnerText = "通知";
                ((System.Web.UI.HtmlControls.HtmlInputButton)this.Form1.FindControl("btnAddNew")).Value = "新增通知";
            }

         
			this.tdNewNotice.Visible = user.HasOperationRight("080101");// 080101为通知新增权限
			bool IsInRole = user.HasOperationRight("080102");// 080102为通知修改权限
			strRole = (IsInRole)?"Modify":"View";
		}
		private DataTable DisPoseNotice(DataTable dt)
		{
			for(int i=0;i<dt.Rows.Count;i++)
			{
				dt.Rows[i]["SubmitPerson"] = BLL.SystemRule.GetUserName(dt.Rows[i]["SubmitPerson"].ToString());
			}
			return dt;
		}

        //判断该用户是否已经读过该条通知
        public bool UserHasReadThisNotice(string strNoticeCode)
        {
            bool isRead = false;
            User u = (User)Session["User"];
            string strSelect = "select * from UserLookedNotice where noticecode='" + strNoticeCode + "' and usercode='" + u.UserCode + "'";
            QueryAgent qa = new QueryAgent();
            DataSet ds = qa.ExecSqlForDataSet(strSelect);
            if (ds.Tables[0].Rows.Count > 0)
            {
                isRead = true;
            }
            qa.Dispose();
            return isRead;
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            this.LoadData();  
        }
}
}
