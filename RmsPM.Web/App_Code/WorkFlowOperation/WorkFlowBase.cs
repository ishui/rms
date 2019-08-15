using System;
using Rms.ORMap;
using RmsPM.Web.WorkFlowControl;

namespace RmsPM.Web.WorkFlowOperation
{
	/// <summary>
	/// WorkFlowBase ��ժҪ˵����
	/// </summary>
	public class WorkFlowBase : System.Web.UI.UserControl
	{
		protected System.Web.UI.HtmlControls.HtmlGenericControl up_OperableDiv
		{
			get
			{
				return (System.Web.UI.HtmlControls.HtmlGenericControl)this.FindControl("OperableDiv");
			}
		}
		protected System.Web.UI.HtmlControls.HtmlGenericControl up_EyeableDiv
		{
			get
			{
				return (System.Web.UI.HtmlControls.HtmlGenericControl)this.FindControl("EyeableDiv");
			}
		}

		#region --- ˽�г�Ա���� ---
		/// <summary>
		/// ҵ�����
		/// </summary>
		private string _ApplicationCode = "";
		/// <summary>
		/// ҵ������
		/// </summary>
		private string _ApplicationTitle = "";
		/// <summary>
		/// ҵ������
		/// </summary>
		private string _ApplicationType = "";
		/// <summary>
		/// ģ��״̬
		/// </summary>
		private ModuleState _State = ModuleState.Unbeknown;
		/// <summary>
		/// ����ģ��״̬
		/// </summary>
		private ModuleState _AttachmentState = ModuleState.Unbeknown;
		/// <summary>
		/// ���ģ��״̬
		/// </summary>
		private ModuleState _MoneyState = ModuleState.Unbeknown;
		/// <summary>
		/// ��Ŀ����
		/// </summary>
		private string _ProjectCode = "";
		/// <summary>
		/// �����û�
		/// </summary>
		private string _UserCode = "";
		/// <summary>
		/// �������
		/// </summary>
		private StandardEntityDAO _dao;

        /// <summary>
        /// ����״̬
        /// </summary>
        private ModuleState _State1 = ModuleState.Unbeknown;

		#endregion --- ˽�г�Ա���� ---

		#region --- ���Լ��� ---

        /// <summary>
        /// ����״̬1
        /// </summary>
        public ModuleState State1
        {
            get
            {
                if (_State1 == ModuleState.Unbeknown)
                {
                    if (this.ViewState["_State1"] != null)
                        return (ModuleState)this.ViewState["_State1"];
                    return ModuleState.Unbeknown;
                }
                return _State1;
            }
            set
            {
                _State1 = value;
                this.ViewState["_State1"] = value;
            }
        }
        
        /// <summary>
		/// ҵ�����
		/// </summary>
		public string ApplicationCode
		{
			get
			{
				if ( _ApplicationCode == "" )
				{
					if(this.ViewState["_ApplicationCode"] != null)
						return this.ViewState["_ApplicationCode"].ToString();
					return "";
				}
				return _ApplicationCode;
			}
			set
			{
				_ApplicationCode = value;
				this.ViewState["_ApplicationCode"] = value;
			}
		}

		/// <summary>
		/// ҵ������
		/// </summary>
		public string ApplicationTitle
		{
			get
			{
				if ( _ApplicationTitle == "" )
				{
					if(this.ViewState["_ApplicationTitle"] != null)
						return this.ViewState["_ApplicationTitle"].ToString();
					return "";
				}
				return _ApplicationTitle;
			}
			set
			{
				_ApplicationTitle = value;
				this.ViewState["_ApplicationTitle"] = value;
			}
		}

		/// <summary>
		/// ҵ�����
		/// </summary>
		public string ApplicationType
		{
			get
			{
				if ( _ApplicationType == "" )
				{
					if(this.ViewState["_ApplicationType"] != null)
						return this.ViewState["_ApplicationType"].ToString();
					return "";
				}
				return _ApplicationType;
			}
			set
			{
				_ApplicationType = value;
				this.ViewState["_ApplicationType"] = value;
			}
		}

		/// <summary>
		/// ��Ŀ����
		/// </summary>
		public string ProjectCode
		{
			get
			{
				if ( _ProjectCode == "" )
				{
					if(this.ViewState["_ProjectCode"] != null)
						return this.ViewState["_ProjectCode"].ToString();
					return "";
				}
				return _ProjectCode;
			}
			set
			{
				_ProjectCode = value;
				this.ViewState["_ProjectCode"] = value;
			}
		}

		/// <summary>
		/// �����û�
		/// </summary>
		public string UserCode
		{
			get
			{
				if ( _UserCode == "" )
				{
					if(this.ViewState["_UserCode"] != null)
						return this.ViewState["_UserCode"].ToString();
					return "";
				}
				return _UserCode;
			}
			set
			{
				_UserCode = value;
				this.ViewState["_UserCode"] = value;
			}
		}


		/// <summary>
		/// ģ��״̬
		/// </summary>
		public ModuleState State
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

		/// <summary>
		/// ����ģ��״̬
		/// </summary>
		public ModuleState AttachmentState
		{
			get
			{
				if ( _AttachmentState == ModuleState.Unbeknown )
				{
					if(this.ViewState["_AttachmentState"] != null)
						return (ModuleState)this.ViewState["_AttachmentState"];
					return ModuleState.Unbeknown;
				}
				return _AttachmentState;
			}
			set
			{
				_AttachmentState = value;
				this.ViewState["_AttachmentState"] = value;
			}
		}

		/// <summary>
		/// ���ģ��״̬
		/// </summary>
		public ModuleState MoneyState
		{
			get
			{
				if ( _MoneyState == ModuleState.Unbeknown )
				{
					if(this.ViewState["_MoneyState"] != null)
						return (ModuleState)this.ViewState["_MoneyState"];
					return ModuleState.Unbeknown;
				}
				return _MoneyState;
			}
			set
			{
				_MoneyState = value;
				this.ViewState["_MoneyState"] = value;
			}
		}

		/// <summary>
		/// �������
		/// </summary>
		public StandardEntityDAO dao
		{
			get
			{
				return this._dao;
			}
			set
			{
				_dao = value;
			}
		}


		#endregion --- ���Լ��� ---

		#region --- �������� ---

		/// <summary>
		/// �ؼ���ʼ��
		/// </summary>
		public virtual void InitControl()
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
						this.LoadData();
                        this.RestoreStatus();
						this.up_EyeableDiv.Visible = false;
						this.up_OperableDiv.Visible = true;
						break;

					case ModuleState.Eyeable://�ɼ���
						this.LoadData();
						this.up_OperableDiv.Visible = false;
						this.up_EyeableDiv.Visible = true;
						break;

					case  ModuleState.Begin://�ɼ���
						this.LoadData();
						this.up_OperableDiv.Visible = false;
						this.up_EyeableDiv.Visible = true;
						break;

					case ModuleState.End://�ɼ���
						this.LoadData();
						this.up_OperableDiv.Visible = false;
						this.up_EyeableDiv.Visible = true;
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
		virtual public void LoadData()
		{
		}

        /// <summary>
        /// ��ԭҵ������״̬
        /// </summary>
        virtual public string RestoreStatus()
        {
            return "";
        }
	
		/// <summary>
		/// ����ؼ�����
		/// </summary>
		virtual public string SubmitData()
		{
			return "";
		}

        /// <summary>
        /// ��ʾҵ������
        /// </summary>
        virtual protected string ShowApplicationHyperLink(string pm_sShowName, string pm_sLinkURL)
        {
            string ud_sHyperLink;

            ud_sHyperLink = "<a href=\"##\" onclick=\"javascript:OpenFullWindow('" + pm_sLinkURL + "','');\">";
            ud_sHyperLink += pm_sShowName;
            ud_sHyperLink += "</a>";

            return ud_sHyperLink;


        }

		#endregion

		public WorkFlowBase()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
	}
}
