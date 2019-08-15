namespace RmsPM.Web.Project
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using RmsPM.DAL.EntityDAO;
	using Rms.ORMap;
	using Rms.Web;

	/// <summary>
	/// <description>
	/// 	WBSAlertStatus ��ժҪ˵����
	///		��Ҫ������װ������״̬�ı���¼��ͷ���.
	///		ҳ����ʾʱ��button�¼�����ͬʱ�ṩ״̬�ı�ķ���
	/// </description>
	///	<author>unm</author>
	///	<date>2004/11/8</date>
	///	<version>1.0</version>
	///	<modify>
	///		<description></description>
	///		<author></author>	
	///		<date></date>
	///		<version></version>
	///	</modify>
	/// </summary>
	public partial class WBSAlertStatus : System.Web.UI.UserControl
	{
		protected string strStatus = "";
		protected string strTaskCode = "";
		private string strUserType = "";
		/// <summary>
		/// ��������
		/// </summary>
		public string TaskCode
		{
			set
			{
				this.strTaskCode = value;
			}
			get
			{
				return this.strTaskCode;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				// ��ǰ�û����ͣ������ˣ��ල�ˣ������˵�
				this.strUserType = (string)ViewState["UserType"];
				// �ڴ˴������û������Գ�ʼ��ҳ��
				EntityData entity = WBSDAO.GetV_TaskByCode(this.strTaskCode);
				if(entity.HasRecord())
					this.strStatus = entity.GetInt("Status").ToString();
				entity.Dispose();
				this.CheckStatus();

			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"״̬�ı�ʧ��");
			}
		}

		private void CheckStatus()
		{	
			this.btStart.Visible = false;
			this.btPause.Visible = false;
			this.btCancel.Visible = false;
			this.btFinish.Visible = false;
			switch(this.strStatus)
			{
				// �����еĲ�������ʼ����ͣ��ȡ�������
				case "0": // δ��ʼ
					this.btStart.Visible = true;
					this.btCancel.Visible = true;
					this.btFinish.Visible = true;
					break;
				case "1": // ������
					this.btPause.Visible = true;
					this.btCancel.Visible = true;
					this.btFinish.Visible = true;
					break;
				case "2": // ��ͣ
					this.btStart.Value = "��������";
					this.btStart.Visible = true;
					this.btCancel.Visible = true;
					this.btFinish.Visible = true;
					break;
				case "3": // ȡ��
//					this.btStart.Value = "��������";
//					this.btStart.Visible = true;
					break;
				case "4": // �����

					break;
			}
			// ֻ�������˿�����ͣ����,ȡ�����������
//			if(this.strUserType!="2")
//			{
//				this.btPause.Visible = false;
//				this.btCancel.Visible = false;
//				this.btFinish.Visible = false;
//			}
		}

		/// <summary>
		/// ״̬�����Ķ���WBSStatus��
		/// </summary>

		private void SetStatus(string strValue)
		{
		}

	
		public void SetStart()
		{
			this.SetStatus("1");
		}

		public void SetPause()
		{
			this.SetStatus("2");
		}

		public void SetCancel()
		{
			this.SetStatus("3");
		}

		public void SetFinish()
		{
			this.SetStatus("4");
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

		private void JSAction()
		{
			// ���ڵ���Ҫ�ٴ���������
			Response.Write(JavaScript.ScriptStart);
			Response.Write("window.location.href = '"+Request.Url.PathAndQuery+"';");
			Response.Write(JavaScript.ScriptEnd);
		}

		protected void btStart_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				this.SetStart();
				this.JSAction();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"״̬�ı�ʧ��");
			}
		}

		protected void btPause_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				this.SetPause();
				this.JSAction();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"״̬�ı�ʧ��");
			}
		}

		protected void btCancel_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				this.SetCancel();
				this.JSAction();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"״̬�ı�ʧ��");
			}
		}

		protected void btFinish_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				this.SetFinish();
				this.JSAction();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"״̬�ı�ʧ��");
			}
		}
	}
}
