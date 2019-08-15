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
	///		Control_rpNotice ��ժҪ˵����
	/// </summary>
	public partial class Control_rpNotice : Components.BaseControl
	{

		int intListNoticeNum=4;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			DefaultSet();
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
		///		�����֧������ķ��� - ��Ҫʹ�ô���༭��
		///		�޸Ĵ˷��������ݡ�
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
                
				ApplicationLog.WriteLog(this.ToString(),ex,"��ȡ��ҳ����ʧ��");
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
		#region ֪ͨ�Ĵ���
		private void LoadNotice()
		{
			NoticeStrategyBuilder noticeBuilder = new NoticeStrategyBuilder();
			ArrayList arA = new ArrayList();
			arA.Add("080104");
			arA.Add(base.user.UserCode);
			arA.Add(user.BuildStationCodes());
            
            
            //ArrayList arB = new ArrayList();//Ϊ״̬��ֵ
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
            
            entityNotice.CurrentTable.Columns.Add("NoticeClassTitle");//�Ķ�
            for(int i=0;i<entityNotice.CurrentTable.Rows.Count;i++)
            {       //�Ķ�
                if (entityNotice.CurrentTable.Rows[i]["NoticeClass"].ToString() != string.Empty)
                {  
                    entityNotice.CurrentTable.Rows[i]["NoticeClassTitle"] = entityNotice.CurrentTable.Rows[i]["NoticeClass"].ToString() + ":" + entityNotice.CurrentTable.Rows[i]["Title"].ToString(); 
                }
                else
                {
                    entityNotice.CurrentTable.Rows[i]["NoticeClassTitle"] = "" + entityNotice.CurrentTable.Rows[i]["Title"].ToString();

                }
                //�Ķ�
                string strTmp = entityNotice.CurrentTable.Rows[i]["NoticeClassTitle"].ToString();
				if(strTmp.Length>20)
                    entityNotice.CurrentTable.Rows[i]["NoticeClassTitle"] = strTmp.Substring(0, 20) + "...";//�Ķ�
			}
			rpNotice.DataSource = entityNotice;
			rpNotice.DataBind();
			entityNotice.Dispose();

			// ��鵱ǰ�û�Ȩ��
			this.hylNewNotice.Visible = (this.IsInRole("080101"))?true:false;
		}
		#endregion

        //�жϸ��û��Ƿ��Ѿ���������֪ͨ
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
