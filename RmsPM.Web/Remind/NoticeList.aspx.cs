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
	/// NoticeList : ��ʾ֪ͨ���б�
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
				// �ڴ˴������û������Գ�ʼ��ҳ��
				if (!this.IsPostBack)
				{
					InitPage();
					LoadData();
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"����֪ͨʧ��");
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

		}
		#endregion

		private void InitPage()
        {   //�������Ի��жϣ�����ͨ��ɾ����ע�ͣ�
            if (this.up_sPMNameLower == "tianyangoa")
            {
                this.lblNoticeClass.Visible = true;
                this.DDLNoticeClass.Visible = true;
                RmsPM.BLL.PageFacade.LoadDictionarySelect(this.DDLNoticeClass, "֪ͨ����", "");//֪ͨ���ĳ�֪ͨ����
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


                if (!user.HasRight("080105"))//�ж�֪ͨ���
                {
                    ArrayList arA = new ArrayList();
                    arA.Add("080104");
                    arA.Add(user.UserCode);
                    arA.Add(user.BuildStationCodes());
                    noticeBuilder.AddStrategy(new Strategy(DAL.QueryStrategy.NoticeStrategyName.AccessRange, arA));
                }
                
                //��ѯ֪ͨ
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

                entityNotice.CurrentTable.Columns.Add("NoticeClassTitle");//�Ķ�
                for (int i = 0; i < entityNotice.CurrentTable.Rows.Count; i++)
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
                    if (strTmp.Length > 20)
                        entityNotice.CurrentTable.Rows[i]["NoticeClassTitle"] = strTmp.Substring(0, 20) + "...";//�Ķ�
                }
			    this.dgNoticeList.DataSource= DisPoseNotice(entityNotice.CurrentTable);
			    this.dgNoticeList.DataBind();
            
            if (DocType == "99")
            {
                lblTitle.InnerText  = "����";
                ((System.Web.UI.HtmlControls.HtmlInputButton)this.Form1.FindControl("btnAddNew")).Value = "��������";
            }
            else
            {
                lblTitle.InnerText = "֪ͨ";
                ((System.Web.UI.HtmlControls.HtmlInputButton)this.Form1.FindControl("btnAddNew")).Value = "����֪ͨ";
            }

         
			this.tdNewNotice.Visible = user.HasOperationRight("080101");// 080101Ϊ֪ͨ����Ȩ��
			bool IsInRole = user.HasOperationRight("080102");// 080102Ϊ֪ͨ�޸�Ȩ��
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

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            this.LoadData();  
        }
}
}
