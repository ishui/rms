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
using RmsPM.BLL;
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using RmsPM.Web;
using Rms.ORMap;
using Rms.WorkFlow;


namespace RmsPM.Web.WorkFlowContral
{
	/// <summary>
	/// 
	/// </summary>
	public partial class WorkFlowStart :PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputHidden ProjectCode;

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
//			this.Contact.Visible = user.HasOperationRight("190101");//    ������ϵ��
			this.VehicleApply.Visible = user.HasOperationRight("190102");//�ó�����
			//this.ComputerMaintenance.Visible = user.HasOperationRight("190103");//�����ά��
			//this.Equipment.Visible = user.HasOperationRight("190104");//������豸����
//			this.SignFile.Visible = user.HasOperationRight("190105");//ǩ��
//			this.archives.Visible = user.HasOperationRight("190106");//����
//			this.OverTime.Visible = user.HasOperationRight("190107");//�Ӱ�
//			this.leave.Visible = user.HasOperationRight("190108");//��ٵ�
//			this.RetainExamine.Visible = user.HasOperationRight("190109");//Ա�������ڽ�������
//			this.StaffApply.Visible = user.HasOperationRight("190110");//��Ա����
//			this.SceneVisa.Visible = user.HasOperationRight("190111");//�ֳ�ǩ֤��
//			this.cachet.Visible = user.HasOperationRight("190112");//����(������)����
//			this.TechnologyCheck.Visible = user.HasOperationRight("190113");//�����˶���
//
//			this.DesignRework.Visible = user.HasOperationRight("190114");//����޸Ĳ���֪ͨ��
//			this.MidPayCertificate.Visible = user.HasOperationRight("190115");//���ڸ������뵥
//			this.ConstructDrawingAuditFlow.Visible = user.HasOperationRight("190116");//ʩ��ͼֽ�Ż�������ת��
//			this.ConstructPlanAuditFlow.Visible = user.HasOperationRight("190117");//ʩ������������ת��
//			this.InvitBidAuditFlow.Visible = user.HasOperationRight("190118");//��Ͷ��������ת��
//			this.OpinionReferAuditFlow.Visible = user.HasOperationRight("190119");//�����ѯ������ת��
//

			this.Purchase.Visible = user.HasOperationRight("190120");//���ʲɹ�����
			this.ChequeDrow.Visible = user.HasOperationRight("190121");//֧Ʊ���ã����� 
			this.Contract.Visible = user.HasOperationRight("190122");//��ͬ�������
			
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


	}
}
