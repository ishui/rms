namespace RmsPM.Web.DeskTopControl
{
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
	using RmsPM.BLL;

	/// <summary>
	///		Control_rpNotice 的摘要说明。
	/// </summary>
	public partial class Control_rpNotice : Components.BaseControl
	{

		int intListNoticeNum=4;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			DefaultSet();
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
		///		设计器支持所需的方法 - 不要使用代码编辑器
		///		修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
		private void DefaultSet()
		{
			try
			{
				LoadNotice();
			}
			catch(Exception ex)
			{
                
				ApplicationLog.WriteLog(this.ToString(),ex,"获取首页数据失败");
			}
		}

        private bool _IsOther = false;
        public bool IsOther
        {
            get
            {
                return _IsOther;
            }
            set
            {
                _IsOther = value;
            }
        }
		#region 通知的处理
		private void LoadNotice()
		{
			NoticeStrategyBuilder noticeBuilder = new NoticeStrategyBuilder();
			ArrayList arA = new ArrayList();
			arA.Add("080104");
			arA.Add(base.user.UserCode);
			arA.Add(user.BuildStationCodes());
            
            
            //ArrayList arB = new ArrayList();//为状态赋值
            //arB.Add("1");
            noticeBuilder.AddStrategy(new Strategy(DAL.QueryStrategy.NoticeStrategyName.status,"1,2"));
            if (_IsOther)
            {
                noticeBuilder.AddStrategy(new Strategy(DAL.QueryStrategy.NoticeStrategyName.Type, "99"));
            }
            else
            {
                noticeBuilder.AddStrategy(new Strategy(DAL.QueryStrategy.NoticeStrategyName.Type, "1"));
            }
            noticeBuilder.AddStrategy(new Strategy(DAL.QueryStrategy.NoticeStrategyName.AccessRange, arA));
			noticeBuilder.AddOrder("SubmitDate",false);
			string sql = noticeBuilder.BuildMainQueryString();
           
            
            //sql += " and status='1'";
			QueryAgent qa = new QueryAgent();
			qa.SetTopNumber(this.intListNoticeNum);
			EntityData entityNotice = qa.FillEntityData("Notice",sql);
			qa.Dispose();
            
            entityNotice.CurrentTable.Columns.Add("NoticeClassTitle");//改动
            for(int i=0;i<entityNotice.CurrentTable.Rows.Count;i++)
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
				if(strTmp.Length>20)
                    entityNotice.CurrentTable.Rows[i]["NoticeClassTitle"] = strTmp.Substring(0, 20) + "...";//改动
			}
			rpNotice.DataSource = entityNotice;
			rpNotice.DataBind();
			entityNotice.Dispose();

			// 检查当前用户权限
			this.hylNewNotice.Visible = (this.IsInRole("080101"))?true:false;
		}
		#endregion

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
	}
}
