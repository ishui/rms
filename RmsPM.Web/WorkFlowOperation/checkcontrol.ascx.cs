//===========================================================================
// ���ļ�����Ϊ ASP.NET 2.0 Web ��Ŀת����һ�����޸ĵġ�
// �����Ѹ��ģ��������޸�Ϊ���ļ���App_Code\Migrated\workflowoperation\Stub_checkcontrol_ascx_cs.cs���ĳ������ 
// �̳С�
// ������ʱ�������������� Web Ӧ�ó����е�������ʹ�øó������󶨺ͷ��� 
// ��������ҳ��
// ����������ҳ��workflowoperation\checkcontrol.ascx��Ҳ���޸ģ��������µ�������
// �йش˴���ģʽ�ĸ�����Ϣ����ο� http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
namespace RmsPM.Web.WorkFlowOperation
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using RmsPM.Web.WorkFlowControl;

	/// <summary>
	///		CheckControl ��ժҪ˵����
	/// </summary>
	public partial class Migrated_CheckControl : CheckControl
	{

		#region --- ˽�г�Ա���� ---
		/// <summary>
		/// ģ��״̬
		/// </summary>
		private ModuleState _State = ModuleState.Unbeknown;


		#endregion --- ˽�г�Ա���� ---

		#region --- ���Լ��� ---

		/// <summary>
		/// ҵ�����
		/// </summary>
//		public string Result
//		{
		override public string Result
		{
			get
			{
				if (rdoContrachCheck.SelectedIndex == -1)
				{
					return "Unknow";
				}
				else
				{
					return rdoContrachCheck.SelectedItem.Value;
				}
			}
			set
			{
				foreach ( ListItem item in rdoContrachCheck.Items )
				{
					if ( item.Value == value )
					{
						item.Selected = true;
						break;
					}
				}
			}
		}

		/// <summary>
		/// ģ��״̬
		/// </summary>
//		public ModuleState State
//		{
		override public ModuleState State
		{
			get
			{
				if ( _State == ModuleState.Unbeknown )
				{
					if(this.ViewState["_State"] != null)
						return (ModuleState)this.ViewState["_State"];
					return ModuleState.Unbeknown;
				}
				return _State;
			}
			set
			{
				_State = value;
				this.ViewState["_State"] = value;
			}
		}
		#endregion --- ���Լ��� ---


		#region --- �������� ---

		/// <summary>
		/// �ؼ���ʼ��
		/// </summary>
//		public void InitControl()
		override public void InitControl()
		{
			try
			{
				this.Visible = true;

				switch ( this.State )
				{
					case ModuleState.Sightless://���ɼ���
						this.Visible = false;
						break;

					case  ModuleState.Operable://�ɲ�����
						LoadData();
						EyeableDiv.Visible = false;
						OperableDiv.Visible = true;
						break;

					case ModuleState.Eyeable://�ɼ���
						LoadData();
						OperableDiv.Visible = false;
						EyeableDiv.Visible = false;
						break;

					case  ModuleState.Begin://���ɼ���
						this.Visible = false;
						break;

					case ModuleState.End://���ɼ���
						this.Visible = false;
						break;

					default:
						this.Visible = false;
						break;
				}


			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


		/// <summary>
		/// װ�ؿؼ�����
		/// </summary>
//		public void LoadData()
		override public void LoadData()
		{
			try
			{
				if ( this.Result == "Unknow" )
				{
					lblResult.Text = "δ֪";
				}
				else
				{
					lblResult.Text = rdoContrachCheck.SelectedItem.Text;
				}

			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		#endregion --- �������� ---

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
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
