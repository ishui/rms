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
	///		Control_Work_rpExecute ��ժҪ˵����
	/// </summary>
	public partial class Control_Work_rpExecute : Components.BaseControl
	{

		/// <summary>
		/// ��������Ĭ��ȡ�ü�¼��
		/// </summary>
		private int intListExecuteNum = 4; 
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
			LoadData();
		}
		private void LoadData()
		{
			try
			{
				string TaskEnable = BLL.ConvertRule.ToString(System.Configuration.ConfigurationSettings.AppSettings["TaskEnable"]);
				if (TaskEnable != "0")			
				LoadExecute();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ȡ��ҳ����ʧ��");
			}
		}
		#region ��������Ĵ���
		/// <summary>
		/// ��ȡ���������б��ַ���Χ֮�ڵ��˶����Կ���
		/// </summary>
		private void LoadExecute()
		{			
			TaskExecuteStrategyBuilder asb = new TaskExecuteStrategyBuilder();
			ArrayList arA = new ArrayList();
			arA.Add("070202");
			arA.Add(user.UserCode);
			arA.Add(user.BuildStationCodes());
			asb.AddStrategy( new Strategy( DAL.QueryStrategy.TaskExecuteStrategyName.AccessRange,arA));
			asb.AddOrder("ExecuteDate",false);
			string sql = asb.BuildMainQueryString();
			QueryAgent qa = new QueryAgent();
			qa.SetTopNumber(this.intListExecuteNum);
			EntityData entityExecute = qa.FillEntityData("TaskExecute",sql);
			qa.Dispose();
			//EntityData entityExecute = WBSDAO.GetTaskExecuteDeskTop(((User)Session["User"]).UserCode);
			//EntityData entityWBS = null;
			if (entityExecute.HasRecord())
			{ 
                //�����ʱ���鿴��1��������Ŀ
                this.imgOpenMoreTaskExecute.Attributes["ProjectCode"] = entityExecute.CurrentTable.Rows[0]["ProjectCode"].ToString();

				DataTable dtExecuteNew = entityExecute.CurrentTable;		
				dtExecuteNew.Columns.Add("Img",System.Type.GetType("System.String"));
				dtExecuteNew.Columns.Add("TaskName",System.Type.GetType("System.String"));
				foreach(DataRow dr in dtExecuteNew.Rows)
				{					
					try
					{
						string tmp = dr["WBSCode"].ToString();
						EntityData entityWBS = WBSDAO.GetV_TaskByCode(tmp);
						if(entityWBS.HasRecord())
						{
							dr["WBSCode"] = entityWBS.CurrentTable.Rows[0]["WBSCode"].ToString();
							dr["TaskName"] = entityWBS.CurrentTable.Rows[0]["TaskName"].ToString();
							string strName = entityWBS.CurrentTable.Rows[0]["TaskName"].ToString();
							if(strName.Length>8)
								dr["TaskName"] = strName.Substring(0,8)+"...";
							if(entityWBS.CurrentTable.Rows[0]["ImportantLevel"].ToString()=="1")
								dr["Img"] =  "<img src=\"images/icon_important.gif\" width=\"17\" height=\"18\">";
							string strTmp = dr["Detail"].ToString();
							if(strTmp.Length>8)
								dr["Detail"] = Server.HtmlEncode(dr["Detail"].ToString().Substring(0,8)+"...");
						}
					}
					catch(Exception ex)
					{
						throw new Exception(ex.Message+":��ȡ���������б�ʧ��",ex);
					}
				}
				DataView dv = dtExecuteNew.DefaultView;
				dv.Sort = "ExecuteDate desc";
				this.rpExecute.DataSource = dv;
				this.rpExecute.DataBind();
			}
			entityExecute.Dispose();
		}

		#endregion
	}
}
