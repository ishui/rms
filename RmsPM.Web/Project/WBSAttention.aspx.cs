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
using Rms.Web;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;
using RmsPM.Web;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// WBSAttention ��ժҪ˵����
	/// </summary>
	public partial class WBSAttention : PageBase
	{
	
		EntityData entityAttention= null;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			try
			{			
				if(!this.IsPostBack)
				{
					InitPage();
					LoadData();
				}
			}		
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"�����ע�����б�ʧ��");
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
			this.dgAttention.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgAttention_DeleteCommand);

		}
		#endregion

		/// <summary>
		/// ��ʼ����������
		/// </summary>
		private void InitPage()
		{		
			ViewState["ProjectCode"] = Request["ProjectCode"].ToString();
		}

		/// <summary>
		/// ��������
		/// </summary>
		private void LoadData()
		{

			
			User objUser = (User)Session["User"];
			AttentionStrategyBuilder asb = new AttentionStrategyBuilder();
			ArrayList arA = new ArrayList();
			arA.Add("070110");
			arA.Add(objUser.UserCode);
			arA.Add(user.BuildStationCodes());
			asb.AddStrategy( new Strategy( DAL.QueryStrategy.AttentionStrategyName.AccessRange,arA));
			asb.AddStrategy( new Strategy( DAL.QueryStrategy.AttentionStrategyName.ProjectCode,(string)ViewState["ProjectCode"]));
			if(this.txtType.Value.Length>0) 
				asb.AddStrategy( new Strategy( DAL.QueryStrategy.AttentionStrategyName.AddModule,this.txtType.Value));
			if(this.txtTitle.Value.Length>0) 
			asb.AddStrategy( new Strategy( DAL.QueryStrategy.AttentionStrategyName.AddTitle,this.txtTitle.Value));
			if(this.dtStartDate.Value.Length>0||this.dtEndDate.Value.Length>0)
			{
				ArrayList arB = new ArrayList();
				arB.Add(this.dtStartDate.Value);
				arB.Add(this.dtEndDate.Value);
				asb.AddStrategy( new Strategy( DAL.QueryStrategy.AttentionStrategyName.AddTime,arB));
			}
			asb.AddOrder("AddTime",false);
			string sql = asb.BuildMainQueryString();						
			QueryAgent qa = new QueryAgent();
			entityAttention = qa.FillEntityData("TaskAttention",sql);
			qa.Dispose();
			this.dgAttention.DataSource = entityAttention;
			this.dgAttention.DataBind();
			this.tbNoAttention.Visible = (entityAttention.CurrentTable.Rows.Count > 0)?false:true;
			entityAttention.Dispose();
		}


		private void dgAttention_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				User objUser = (User)Session["User"];
				EntityData entity = WBSDAO.GetTaskAttentionByCode(this.dgAttention.DataKeys[e.Item.ItemIndex].ToString());
				WBSDAO.DeleteTaskAttention(entity);
				entity.Dispose();

				entityAttention.CurrentTable.Rows[e.Item.ItemIndex].Delete();
				DataView dv = entityAttention.CurrentTable.DefaultView;
				this.dgAttention.DataSource = dv;
				this.dgAttention.DataBind();
				this.tbNoAttention.Visible = (entityAttention.HasRecord())?false:true;
				entityAttention.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"״̬�ı�ʧ��");
			}
		}

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				this.LoadData();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"״̬�ı�ʧ��");
			}
		}

	}
}
