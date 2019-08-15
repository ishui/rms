//===========================================================================
// ���ļ�����Ϊ ASP.NET 2.0 Web ��Ŀת����һ�����޸ĵġ�
// �����Ѹ��ģ��������޸�Ϊ���ļ���App_Code\Migrated\workflowcontrol\Stub_workflowopinion_ascx_cs.cs���ĳ������ 
// �̳С�
// ������ʱ�������������� Web Ӧ�ó����е�������ʹ�øó������󶨺ͷ��� 
// ��������ҳ��
// ����������ҳ��workflowcontrol\workflowopinion.ascx��Ҳ���޸ģ��������µ�������
// �йش˴���ģʽ�ĸ�����Ϣ����ο� http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
namespace RmsPM.Web.WorkFlowControl
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using Rms.ORMap;
	using RmsPM.Web.WorkFlowControl;

	/// <summary>
	///		PurchaseControl3 ��ժҪ˵����
	/// </summary>
	public partial class Migrated_WorkFlowOpinion : WorkFlowOpinion
	{

		private string _OpinionType = null;
		protected System.Web.UI.HtmlControls.HtmlAnchor DivManageButton;
		private bool _IsTextBox = true;

		/// <summary>
		/// �������
		/// </summary>
//		public string OpinionType
//		{
		override public string OpinionType
		{
			get
			{
				if ( _OpinionType == null )
				{
					if(this.ViewState["_OpinionType"] != null)
						return this.ViewState["_OpinionType"].ToString();
					return "";
				}
				return _OpinionType;
			}
			set
			{
				_OpinionType = value;
				this.ViewState["_OpinionType"] = value;
			}
		}
		/// <summary>
		/// �������
		/// </summary>
//		public string OpinionName
//		{
		override public string OpinionName
		{
			get
			{
				return this.lblOpinionName.Text;
			}
			set
			{
				this.lblOpinionName.Text = value;
				this.lblOpinionNameClass.Text = value;
			}
		}
		/// <summary>
		/// �Ƿ�Ϊ��ͨ�����
		/// </summary>
//		public bool IsTextBox
//		{
		override public bool IsTextBox
		{
			get
			{
				if(this.ViewState["_IsTextBox"] != null)
					return (bool)this.ViewState["_IsTextBox"];
				return _IsTextBox;
			}
			set
			{
				_IsTextBox = value;
				this.ViewState["_IsTextBox"] = value;
			}
		}
		/// <summary>
		/// ��ʾ����
		/// </summary>
//		public string Value
//		{
		override public string Value
		{
			get
			{
				if(this.txtOpinion.Visible == true)
					return this.txtOpinion.Value;
				else
					return this.textareaOpinion.Value;
			}
			set
			{
				this.txtOpinion.Value = value;
				this.textareaOpinion.Value = value;
			}
		}
		/// <summary>
		/// ������Ƿ����
		/// </summary>
//		public bool DisabledText
//		{
		override public bool DisabledText
		{
			get
			{
				return this.txtOpinion.Disabled;
			}
			set
			{
				this.txtOpinion.Disabled = value;
			}
		}
//		public ModuleState DISPLAY
//		{
		override public ModuleState DISPLAY
		{
			set
			{
				if(value == ModuleState.Sightless)
					this.MatterDiv.Attributes["style"] = "display:none";
				else
					this.MatterDiv.Attributes["style"] = "display:block";
			}
		}

		/// ****************************************************************************
		/// <summary>
		/// �������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// ****************************************************************************
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//this.DivManageButton.HRef = "javascript:DivManage('"+this.ID+"_MatterDiv');";
			//this.MatterDiv.Attributes["style"] = "block";
		}
		/// <summary>
		/// �����ʼ��
		/// </summary>
//		public void InitControl()
		override public void InitControl()
		{
			if(this.State == ModuleState.Sightless)//���ɼ���
			{
				this.Visible = false;
			}
			else if(this.State == ModuleState.Operable)//�ɲ�����
			{
				LoadData(true);
				this.lblOpinion.Visible = false;
				this.divOpinion.Visible = false;
				if(this.IsTextBox)
				{
					this.lblOpinionNameClass.Visible = false;
					this.lblOpinionName.Visible = true;
					this.txtOpinion.Visible = true;
					this.textareaOpinion.Visible = false;
					this.OpinionUserAndDate.Visible = false;
				}
				else
				{
					this.lblOpinionNameClass.Visible = true;
					this.lblOpinionName.Visible = false;
					this.txtOpinion.Visible = false;
					this.textareaOpinion.Visible = true;
					this.OpinionUserAndDate.Visible = true;
				}
			}
			else if(this.State == ModuleState.Eyeable)//�ɼ���
			{
				LoadData(false);
				this.txtOpinion.Visible = false;
				this.textareaOpinion.Visible = false;
				if(this.IsTextBox)
				{
					this.lblOpinionNameClass.Visible = false;
					this.lblOpinionName.Visible = true;
					this.lblOpinion.Visible = true;
					this.divOpinion.Visible = false;
					this.OpinionUserAndDate.Visible = false;
				}
				else
				{
					this.lblOpinionNameClass.Visible = true;
					this.lblOpinionName.Visible = false;
					this.lblOpinion.Visible = false;
					this.divOpinion.Visible = true;
					this.OpinionUserAndDate.Visible = true;
				}
			}
			else if(this.State == ModuleState.Begin)//�ɼ���
			{
				LoadData(false);
				this.txtOpinion.Visible = false;
				this.textareaOpinion.Visible = false;
				if(this.IsTextBox)
				{
					this.lblOpinionNameClass.Visible = false;
					this.lblOpinionName.Visible = true;
					this.lblOpinion.Visible = true;
					this.divOpinion.Visible = false;
					this.OpinionUserAndDate.Visible = false;
				}
				else
				{
					this.lblOpinionNameClass.Visible = true;
					this.lblOpinionName.Visible = false;
					this.lblOpinion.Visible = false;
					this.divOpinion.Visible = true;
					this.OpinionUserAndDate.Visible = true;
				}
			}
			else if(this.State == ModuleState.End)//�ɼ���
			{
				LoadData(false);
				this.txtOpinion.Visible = false;
				this.textareaOpinion.Visible = false;
				if(this.IsTextBox)
				{
					this.lblOpinionNameClass.Visible = false;
					this.lblOpinionName.Visible = true;
					this.lblOpinion.Visible = true;
					this.divOpinion.Visible = false;
					this.OpinionUserAndDate.Visible = false;
				}
				else
				{
					this.lblOpinionNameClass.Visible = true;
					this.lblOpinionName.Visible = false;
					this.lblOpinion.Visible = false;
					this.divOpinion.Visible = true;
					this.OpinionUserAndDate.Visible = true;
				}
			}
			else
			{
				this.Visible = false;
			}
		}
		/// ****************************************************************************
		/// <summary>
		/// ���ݼ���
		/// </summary>
		/// ****************************************************************************
		private void LoadData(bool Flag)
		{
			if(this.ApplicationCode != "")
			{
				string OpinionText = "";
				string OpinionUser = "";
				string OpinionDate = "";
				DAL.QueryStrategy.PurchaseFlowOpinionStrategyBuilder sb = new RmsPM.DAL.QueryStrategy.PurchaseFlowOpinionStrategyBuilder();
				sb.AddStrategy( new Strategy( DAL.QueryStrategy.PurchaseFlowOpinionStrategyName.ObjectCode,this.ApplicationCode));
				sb.AddStrategy( new Strategy( DAL.QueryStrategy.PurchaseFlowOpinionStrategyName.OpinionType,this.OpinionType));

				string sql = sb.BuildMainQueryString();
				QueryAgent QA = new QueryAgent();
				EntityData entity = QA.FillEntityData("PurchaseFlowOpinion",sql);
				if(entity.HasRecord())
				{
					OpinionText = entity.CurrentRow["OpinionText"].ToString();
					OpinionUser = entity.CurrentRow["OpinionUser"].ToString();
					OpinionDate = entity.GetDateTimeOnlyDate("OpinionDate");
					
				}

				QA.Dispose();
				entity.Dispose();

				if( Flag )
				{
					if(this.IsTextBox)
					{
						this.txtOpinion.Value = OpinionText;
					}
					else
					{
						if(this.textareaOpinion.Value == "")
							this.textareaOpinion.Value = OpinionText;
						this.OpinionUser.InnerHtml = ((User)Session["User"]).UserName;
						this.OpinionDate.InnerHtml = DateTime.Now.ToShortDateString();
					}
				}
				else
				{
					if(this.IsTextBox)
					{
						this.lblOpinion.Text = OpinionText;
					}
					else
					{
						this.divOpinion.InnerHtml = OpinionText.Replace("\n", "<br>");
						this.OpinionUser.InnerHtml = OpinionUser;
						this.OpinionDate.InnerHtml = OpinionDate;
					}
				}
			}
			else
			{
				if( Flag )
				{
					if(this.IsTextBox)
					{
						this.txtOpinion.Value = "";
					}
					else
					{
						if(this.textareaOpinion.Value == "")
							this.textareaOpinion.Value = "";
						this.OpinionUser.InnerHtml = ((User)Session["User"]).UserName;
						this.OpinionDate.InnerHtml = DateTime.Now.ToShortDateString();
					}
				}
			}

		}

		/// ****************************************************************************
		/// <summary>
		/// ��֯ҵ��ʵ������
		/// </summary>
		/// <returns>ҵ������ʵ�����</returns>
		/// ****************************************************************************
		private EntityData BuildData(bool flag)
		{
			string PurchaseFlowOpinionCode = "";
			DAL.QueryStrategy.PurchaseFlowOpinionStrategyBuilder sb = new RmsPM.DAL.QueryStrategy.PurchaseFlowOpinionStrategyBuilder();
			sb.AddStrategy( new Strategy( DAL.QueryStrategy.PurchaseFlowOpinionStrategyName.ObjectCode,this.ApplicationCode));
			sb.AddStrategy( new Strategy( DAL.QueryStrategy.PurchaseFlowOpinionStrategyName.OpinionType,this.OpinionType));

			string sql = sb.BuildMainQueryString();

			EntityData entityopinion = new EntityData("PurchaseFlowOpinion");
			dao.FillEntity(sql, entityopinion);

			if(entityopinion.Tables[0].Rows.Count > 0)
				PurchaseFlowOpinionCode = entityopinion.CurrentRow["PurchaseFlowOpinionCode"].ToString();

			entityopinion.Dispose();
						
			bool NewRecordFlag = false;
			EntityData entity = DAL.EntityDAO.PurchaseFlowDAO.GetPurchaseFlowOpinionByCode(dao, PurchaseFlowOpinionCode);
			DataRow dr;
			if(PurchaseFlowOpinionCode == "")
			{
				NewRecordFlag = true;
				PurchaseFlowOpinionCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PurchaseFlowOpinion");
				dr = entity.GetNewRecord();
			}
			else
			{
				dr = entity.CurrentRow;
			}

			if(flag)
			{
				dr["PurchaseFlowOpinionCode"] = PurchaseFlowOpinionCode;
				dr["ObjectCode"] = this.ApplicationCode;
				dr["OpinionType"] = this.OpinionType;
				if(this.IsTextBox)
				{
					dr["OpinionText"] = this.txtOpinion.Value.Trim();
					dr["OpinionUser"] = this.txtOpinion.Value.Trim();
					dr["OpinionDate"] = DateTime.Now.ToShortDateString();
				}
				else
				{
					dr["OpinionText"] = this.textareaOpinion.Value.Trim();
					dr["OpinionUser"] = this.OpinionUser.InnerHtml;
					dr["OpinionDate"] = this.OpinionDate.InnerHtml;
				}
			
				if(NewRecordFlag)
				{
					entity.AddNewRecord(dr);
				}
			}
			return entity;
		}
		/// ****************************************************************************
		/// <summary>
		/// �ύ����
		/// </summary>
		/// ****************************************************************************
//		public void SubmitData()
		override public void SubmitData()
		{
			if(this.ApplicationCode != "")
			{
				if(this.dao == null)
				{
					DAL.EntityDAO.PurchaseFlowDAO.SubmitAllPurchaseFlowOpinion(this.BuildData(true));
				}
				else
				{
					dao.EntityName = "PurchaseFlowOpinion";
					dao.SubmitEntity(this.BuildData(true));
				}
			}
			else
			{
				Rms.Web.JavaScript.Alert(true,"û����Ҫ��д����ĵ��ݣ�");
			}
		}
		/// ****************************************************************************
		/// <summary>
		/// ɾ��
		/// </summary>
		/// ****************************************************************************
//		public void DeleteData()
		override public void DeleteData()
		{
			if(this.ApplicationCode != "")
			{
				if(this.dao == null)
				{
					DAL.EntityDAO.PurchaseFlowDAO.DeletePurchaseFlowOpinion(this.BuildData(false));
				}
				else
				{
					dao.EntityName = "PurchaseFlowOpinion";
					dao.SubmitEntity(this.BuildData(false));
				}
			}
			else
			{
				Rms.Web.JavaScript.Alert(true,"û����Ҫ��д����ĵ��ݣ�");
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
		///		�����֧������ķ��� - ��Ҫʹ�ô���༭��
		///		�޸Ĵ˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion


	}
}
