namespace RmsPM.Web.DesignChange.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using RmsPM.BLL;

	/// <summary>
	///		DesignChangeControl ��ժҪ˵����
	/// </summary>
	public partial class DesignChangeControl : Components.ControlBase
	{
		protected AspWebControl.Calendar txtArrangedDate;
		protected Rms.ControlLb.TextBox_Lable TextBox_Lable2;
		protected Rms.ControlLb.TextBox_Lable TextBox_Lable3;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			AttachDefault();
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

		private void ExchangeTypes1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}

		#region ˽������ -----------------------------------------------
		BLL.Design_Message dm=new RmsPM.BLL.Design_Message();
		#endregion


		#region �������� -----------------------------------------------
		#endregion


		#region ˽�з��� -----------------------------------------------
		/// <summary>
		/// ������ʼ��
		/// </summary>
		private void AttachDefault()
		{
			AttachMentAdd1.AttachMentType="��Ʊ��";
			AttachMentAdd1.MasterCode=this.ApplicationCode;
			AttachMentAdd1.Visible=this.IsEditMode;
			this.AttachMentList1.AttachMentType="��Ʊ��";
			AttachMentList1.MasterCode=ApplicationCode;
			this.AttachMentList1.Visible=!this.IsEditMode;
		}
		/// <summary>
		/// ��������
		/// </summary>
		private void SaveData()
		{
			IsVaile();
			dm.DesignCode=this.ApplicationCode;
			dm.DesignName=TB_DesignName.Text;

			dm.DesignID=TB_DesignCode.Text;

			dm.DesignReason=TB_DesignReason.Text;


			dm.DesignRemark=TB_DesignRemark.Text;
			dm.DesignLastTime=Calendar_LB1.Value;
			dm.DesignState="0";
			dm.ProjectCode=Request["ProjectCode"]+"";
			dm.DesignJionTime=System.DateTime.Now.ToShortDateString();
			dm.DesignSupplier=this.InputSupplier1.Value;
			dm.ProjectName=this.InputSupplier1.Text;
			dm.ContractID=this.SelectBox1.Value;
			dm.ContractName=this.SelectBox1.Text;
			dm.ContractCode=this.SelectBox1.Code;
			dm.dao=dao;
			dm.Design_MessageSubmit();
			this.IsEditMode=false;
            ViewState["ApplicationCode"] = dm.DesignCode;
			ControlState();
			
		}
		/// <summary>
		/// ��֤
		/// </summary>
		protected void IsVaile()
		{
			if(InputSupplier1.Text=="ѡ���ͬ")
			{
				throw new Exception("��ѡ���ͬ");
			}
			//InputSearch1.Text
		}
		/// <summary>
		/// ��������
		/// </summary>
		override protected void LoadData()
		{
			TB_DesignName.IsEditMode=this.IsEditMode;
			if(this.ApplicationCode=="")
			{
				GetNewApplicationCode("DesignCode");
				return;
			}
			dm.dao=dao;			
			dm.DesignCode=this.ApplicationCode;
			TB_DesignName.Text=dm.DesignName;
			this.State=dm.DesignState;
            TB_DesignCode.Text = dm.DesignID;
			TB_DesignReason.Text=dm.DesignReason;
			TB_DesignRemark.Text=dm.DesignRemark;
			Calendar_LB1.Text=dm.DesignLastTime;
			SelectBox1.Code=dm.ContractID;
			SelectBox1.Text=dm.ContractName;
			SelectBox1.Value=dm.ContractCode;
			InputSupplier1.Value=dm.DesignSupplier;
			Lb_Supplier.InnerHtml=BLL.ProjectRule.GetSupplierName(dm.DesignSupplier);
			AttachDefault();
			ControlState();
		}
		/// <summary>
		/// ״̬����
		/// </summary>
		public void ControlState()
		{
			TB_DesignName.IsEditMode=this.IsEditMode;

			TB_DesignCode.IsEditMode=this.IsEditMode;

			TB_DesignReason.IsEditMode=this.IsEditMode;


			this.TB_DesignRemark.IsEditMode=this.IsEditMode;
			Calendar_LB1.IsEditMode=this.IsEditMode;
			Lb_Supplier.Visible=!IsEditMode;
			Lb_Contract.Visible=!IsEditMode;
			InputSupplier1.Visible=IsEditMode;
			SelectBox1.IsEditMode=this.IsEditMode;
		}
		private void UpdataState()
		{
			BLL.Design_MessageSystem.UpdateDesignState(this.ApplicationCode,this.State,dao);
		}
		#endregion


		#region �������� -----------------------------------------------
		/// <summary>
		/// �ؼ���ʼ��
		/// </summary>
		public void InitControl()
		{
			LoadData();
		}
		/// <summary>
		/// ������Ϣ
		/// </summary>
		override public void SumitData()
		{
			try
			{
				SaveData();
			}
			catch(Exception ex)
			{
				Response.Write(ex.Message);
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}
		#endregion
	}
}
