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
	///		Control_Work_rpAttention ��ժҪ˵����
	/// </summary>
	public partial class Control_Work_rpAttention : Components.BaseControl
	{

		int intListAttentionNum=4;
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
				LoadAttention();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ȡ��ҳ����ʧ��");
			}
		}
		
		#region �ҹ�ע�Ĺ���
		private void LoadAttention()
		{
			AttentionStrategyBuilder asb = new AttentionStrategyBuilder();
			ArrayList arA = new ArrayList();
			arA.Add("070110");
			arA.Add(user.UserCode);
			arA.Add(user.BuildStationCodes());
			asb.AddStrategy( new Strategy( DAL.QueryStrategy.AttentionStrategyName.AccessRange,arA));
			asb.AddOrder(" AddTime ",false);
			string sql = asb.BuildMainQueryString();
			QueryAgent qa = new QueryAgent();
			qa.SetTopNumber(this.intListAttentionNum);
			EntityData entityAttention = qa.FillEntityData("TaskAttention",sql);
			qa.Dispose();
			rpAttention.DataSource = entityAttention;					
			//			EntityData entityAttention = WBSDAO.GetAllTaskAttention();	
			if (entityAttention.HasRecord())
			{
                //�����ʱ���鿴��1��������Ŀ
                this.imgOpenMoreWBSAttention.Attributes["ProjectCode"] = entityAttention.CurrentTable.Rows[0]["ProjectCode"].ToString();

                DataTable dtAttentionNew = entityAttention.CurrentTable;
				dtAttentionNew.Columns.Add("Img",System.Type.GetType("System.String"));
				//				int i = 0;
				//				foreach(DataRow dr in dtAttentionNew.Rows)
				//				{
				//					if(dr["AddModule"].ToString()=="������Ϣ")
				//					{
				//						EntityData entityTask = WBSDAO.GetTaskByWBSCode(dr["MasterCode"].ToString());
				//						string strImportantLevel = entityTask.GetInt("ImportantLevel").ToString();
				//						if(strImportantLevel=="1")
				//							dr["Img"] =  "<img src=\"images/icon_important.gif\" width=\"17\" height=\"18\">";
				//					}
				//					if(dr["AddModule"].ToString()=="������Ϣ")
				//					{}
				//
				//					i++;
				//					if(i>this.intListAttentionNum) break;
				//				}
				this.rpAttention.DataSource = dtAttentionNew;
				this.rpAttention.DataBind();
			}			
			entityAttention.Dispose();
		}

		#endregion

	}
}
