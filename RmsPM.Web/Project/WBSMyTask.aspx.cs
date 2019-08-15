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
using RmsPM.Web;
using RmsPM.DAL;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// ָ����ǰ���ڣ���ǰ�£������ڣ����µĹ�������
	/// limiao, version 1.0;
	/// unm,2004/11/5 version 2.0
	/// </summary>
	public partial class WBSMyTask : PageBase
	{
		protected string strProjectCode = "";
		protected string strType = "";
		protected string strUser = "";

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				// �ڴ˴������û������Գ�ʼ��ҳ��
				InitPage();

				LoadData();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"�����ҵĹ���ʧ��");
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
			this.dgTask.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgTask_SortCommand);

		}
		#endregion

		/// <summary>
		/// ��ʼ����������
		/// </summary>
		private void InitPage()
		{
//			this.strStatus = Request["Status"]+"";
//			this.lstStatus.SelectedIndex = ( this.strStatus=="")?0:int.Parse(Request["Status"]+"") + 1;
//			if(this.strStatus=="") this.strStatus = "1";
			this.strType = Request["Type"]+"";
			if(this.strType=="") this.strType = "ThisWeek";
			this.strProjectCode = Request.QueryString["ProjectCode"] + "";

			// ��ʼ���û�ѡ��,��ʱĬ�����Լ�
			User objUser = (User)Session["User"];
			try
			{
				this.strUser = Request["myUserTask"]+"";
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
			if(this.strUser=="")
				this.strUser = objUser.UserCode;
		}

		/// <summary>
		/// ��������
		/// </summary>
		private void LoadData()
		{
			string strCondition= "";
			if(this.strType=="ThisWeek")
			{				
				int intWeek = (int)DateTime.Now.DayOfWeek;
				DateTime dTimeStart = DateTime.Now.Date.AddDays(-intWeek);
				DateTime dTimeEnd = DateTime.Now.Date.AddDays(7-intWeek);
				// ��ǰʱ����ʱ����ڲ�
				strCondition = "PlannedStartDate<'"+dTimeStart+"' and PlannedFinishDate>'"+dTimeEnd;
				// ����ʱ��εļ�¼
				strCondition += "' or (PlannedStartDate<'"+dTimeEnd+"' and PlannedStartDate>'"+dTimeStart;
				strCondition += "') or (PlannedFinishDate<'"+dTimeStart+"' and PlannedFinishDate>'"+dTimeEnd+"')";

				//this.ThisWeek.Attributes.CssStyle["class"] = "submit";
				this.lblTime.Text = dTimeStart.ToString("yyyy-MM-dd")+"��"+dTimeEnd.ToString("yyyy-MM-dd");
			}
			else if(this.strType=="ThisMonth")
			{
				int intDay = DateTime.Now.Day;
				DateTime dtMonthStart = DateTime.Now.Date.AddDays(-intDay);
				TimeSpan sMonthDays = DateTime.Now.AddMonths(1)-DateTime.Now;
				int intMonthDays = sMonthDays.Days;
				DateTime dtMonthEnd = DateTime.Now.Date.AddDays(intMonthDays-intDay);
				// ��ǰʱ����ʱ����ڲ�
				strCondition = "(PlannedStartDate<'"+dtMonthStart+"' and PlannedFinishDate>'"+dtMonthEnd;
				// ����ʱ��εļ�¼
				strCondition += "') or (PlannedStartDate<'"+dtMonthEnd+"' and PlannedStartDate>'"+dtMonthStart;
				strCondition += "') or (PlannedFinishDate<'"+dtMonthStart+"' and PlannedFinishDate>'"+dtMonthEnd+"')";

				this.lblTime.Text = dtMonthStart.ToString("yyyy-MM-dd")+"��"+dtMonthEnd.ToString("yyyy-MM-dd");
			}
			else if(this.strType=="NextWeek")
			{
				int intWeek = (int)DateTime.Now.DayOfWeek;
				DateTime dTimeStart = DateTime.Now.Date.AddDays(-intWeek+7);
				DateTime dTimeEnd = DateTime.Now.Date.AddDays(7-intWeek+7);
				// ��ǰʱ����ʱ����ڲ�
				strCondition = "(PlannedStartDate<'"+dTimeStart+"' and PlannedFinishDate>'"+dTimeEnd;
				// ����ʱ��εļ�¼
				strCondition += "') or (PlannedStartDate<'"+dTimeEnd+"' and PlannedStartDate>'"+dTimeStart;
				strCondition += "') or (PlannedFinishDate<'"+dTimeStart+"' and PlannedFinishDate>'"+dTimeEnd+"')";

				this.lblTime.Text = dTimeStart.ToString("yyyy-MM-dd")+"��"+dTimeEnd.ToString("yyyy-MM-dd");
			
			}
			else if(this.strType=="NextMonth")
			{
				// ���µ�����
				TimeSpan sMonthDays = DateTime.Now.AddMonths(2)-DateTime.Now.AddMonths(1);
				int intMonthDays = sMonthDays.Days;
				int intDay = DateTime.Now.Day;
				DateTime dtMonthStart = DateTime.Now.Date.AddMonths(1).AddDays(-intDay);			
				DateTime dtMonthEnd = DateTime.Now.Date.AddMonths(1).AddDays(intMonthDays-intDay);
				// ��ǰʱ����ʱ����ڲ�
				strCondition = "(PlannedStartDate<'"+dtMonthStart+"' and PlannedFinishDate>'"+dtMonthEnd;
				// ����ʱ��εļ�¼
				strCondition += "') or (PlannedStartDate<'"+dtMonthEnd+"' and PlannedStartDate>'"+dtMonthStart;
				strCondition += "') or (PlannedFinishDate<'"+dtMonthStart+"' and PlannedFinishDate>'"+dtMonthEnd+"')";

				this.lblTime.Text = dtMonthStart.ToString("yyyy-MM-dd")+"��"+dtMonthEnd.ToString("yyyy-MM-dd");
			}
			else if(this.strType=="All")
			{
				strCondition = "1=1";
				this.lblTime.Text = "";
			}

			DAL.QueryStrategy.WBSStrategyBuilder myTaskStrategyBuilder = new RmsPM.DAL.QueryStrategy.WBSStrategyBuilder();
			ArrayList arA = new ArrayList();
			arA.Add("070107");
			arA.Add(base.user.UserCode);
			arA.Add("0, 2");  //�ҵĹ���ֻ��ʾ�����ˡ�¼����
			myTaskStrategyBuilder.AddStrategy(new Strategy(DAL.QueryStrategy.WBSStrategyName.AccessRange,arA));
//			myTaskStrategyBuilder.AddStrategy(new Strategy(DAL.QueryStrategy.WBSStrategyName.UserAccess,this.strUser));
			myTaskStrategyBuilder.AddStrategy(new Strategy(DAL.QueryStrategy.WBSStrategyName.ProjectCode,this.strProjectCode));

            //����ʾ��ȡ���Ĺ��� 2006.8.28
            myTaskStrategyBuilder.AddStrategy(new Strategy(DAL.QueryStrategy.WBSStrategyName.StatusNot, "3"));
            
            string m_QuerySQL = myTaskStrategyBuilder.BuildMainQueryString();
			if(strCondition.Length>0)
				m_QuerySQL += " and ("+strCondition+") order by PlannedStartDate desc";
          //  Rms.LogHelper.LogHelper.Debug(m_QuerySQL);
			QueryAgent qa = new QueryAgent();
			try 
			{
				DataSet dsMyTask = qa.ExecSqlForDataSet(m_QuerySQL);
				DataView dv = dsMyTask.Tables[0].DefaultView;
				dv.Table.Columns.Add("Master");
				dv.Table.Columns.Add("StatusName",System.Type.GetType("System.String"));
				foreach(DataRowView drv in dv)
				{
					drv["Master"] = this.GetMaster(drv["WBSCode"].ToString());
					drv["StatusName"] = this.GetStatusImg(drv["Status"].ToString())+"&nbsp;&nbsp;"+drv["SortID"].ToString()+"&nbsp;&nbsp;"+BLL.StringRule.TruncText(drv["TaskName"].ToString(),15);
				}
				this.dgTask.DataSource = dv;
				this.dgTask.DataBind();
				this.trNoTask.Visible = (dgTask.Items.Count>0)?false:true;
			}
			finally 
			{
				qa.Dispose();
			}
		}

		private string GetMaster(string strWBSCode)
		{
			string strMaster = "";
			EntityData entityUser = WBSDAO.GetTaskPersonByWBSCode(strWBSCode);
			if (entityUser.HasRecord())
			{
				DataTable dtUserNew = entityUser.CurrentTable.Copy();

                for (int i = 0; i < dtUserNew.Rows.Count; i++)
                {
                    if (dtUserNew.Rows[i]["Type"].ToString() == "9" && dtUserNew.Rows[i]["RoleType"].ToString() == "0") // ������
                    {
                        strMaster += (strMaster == "") ? "" : ",";
                        strMaster += BLL.SystemRule.GetUserName(dtUserNew.Rows[i]["UserCode"].ToString());
                    }
                }
                for (int i = 0; i < dtUserNew.Rows.Count; i++)
                {
                    if (dtUserNew.Rows[i]["Type"].ToString() == "9" && dtUserNew.Rows[i]["RoleType"].ToString() == "1") // �����λ
                    {
                        strMaster += (strMaster == "") ? "" : ",";
                        strMaster += BLL.SystemRule.GetStationName(dtUserNew.Rows[i]["UserCode"].ToString());
                    }
                }
			}
			entityUser.Dispose();
			return strMaster;
		}

		private string GetStatusImg(string strVal)
		{
			switch(strVal)
			{
				case "0":
					return "<img src=\"../Images/icon_unbegin.gif\" title=\"δ��ʼ����\" border=0 align=\"absmiddle\">";
				case "1":
					return "<img src=\"../Images/icon_going.gif\" title=\"�����й���\" border=0 align=\"absmiddle\">";
				case "2":
					return "<img src=\"../Images/icon_pause.gif\" title=\"����ͣ����\" border=0 align=\"absmiddle\">";
				case "3":
					return "<img src=\"../Images/icon_cancel.gif\" title=\"��ȡ������\" border=0 align=\"absmiddle\">";
				case "4":
					return "<img src=\"../Images/icon_over.gif\" title=\"����ɹ���\" border=0 align=\"absmiddle\">";
				default:
					return "<img src=\"../Images/icon_unbegin.gif\" title=\"δ��ʼ����\" border=0 align=\"absmiddle\">";
			}
		}


		//����������
		private void dgTask_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{
				DataView dv = (DataView)this.dgTask.DataSource;
                if (dv.Sort.IndexOf("DESC") < 0)
                    dv.Sort = e.SortExpression + " DESC";
				else
					dv.Sort = e.SortExpression + " ASC";
				this.dgTask.DataSource = dv;
				this.dgTask.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"�����ҵĹ���ʧ��");
			}
		}

	}
}
