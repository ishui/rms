using System;
using Rms.ORMap;
using RmsPM.Web.WorkFlowControl;

namespace RmsPM.Web.WorkFlowOperation
{
	/// <summary>
	/// WorkFlowBase 的摘要说明。
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

		#region --- 私有成员集合 ---
		/// <summary>
		/// 业务代码
		/// </summary>
		private string _ApplicationCode = "";
		/// <summary>
		/// 业务主题
		/// </summary>
		private string _ApplicationTitle = "";
		/// <summary>
		/// 业务类型
		/// </summary>
		private string _ApplicationType = "";
		/// <summary>
		/// 模块状态
		/// </summary>
		private ModuleState _State = ModuleState.Unbeknown;
		/// <summary>
		/// 附件模块状态
		/// </summary>
		private ModuleState _AttachmentState = ModuleState.Unbeknown;
		/// <summary>
		/// 金额模块状态
		/// </summary>
		private ModuleState _MoneyState = ModuleState.Unbeknown;
		/// <summary>
		/// 项目代码
		/// </summary>
		private string _ProjectCode = "";
		/// <summary>
		/// 操作用户
		/// </summary>
		private string _UserCode = "";
		/// <summary>
		/// 事务对象
		/// </summary>
		private StandardEntityDAO _dao;

        /// <summary>
        /// 辅助状态
        /// </summary>
        private ModuleState _State1 = ModuleState.Unbeknown;

		#endregion --- 私有成员集合 ---

		#region --- 属性集合 ---

        /// <summary>
        /// 辅助状态1
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
		/// 业务代码
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
		/// 业务主题
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
		/// 业务类别
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
		/// 项目代码
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
		/// 操作用户
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
		/// 模块状态
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
		/// 附件模块状态
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
		/// 金额模块状态
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
		/// 事务对象
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


		#endregion --- 属性集合 ---

		#region --- 公共方法 ---

		/// <summary>
		/// 控件初始化
		/// </summary>
		public virtual void InitControl()
		{
			try
			{
				this.Visible = true;

				switch ( this.State )
				{
					case ModuleState.Sightless://不可见的
						this.Visible = false;
						break;

					case  ModuleState.Operable://可操作的
						this.LoadData();
                        this.RestoreStatus();
						this.up_EyeableDiv.Visible = false;
						this.up_OperableDiv.Visible = true;
						break;

					case ModuleState.Eyeable://可见的
						this.LoadData();
						this.up_OperableDiv.Visible = false;
						this.up_EyeableDiv.Visible = true;
						break;

					case  ModuleState.Begin://可见的
						this.LoadData();
						this.up_OperableDiv.Visible = false;
						this.up_EyeableDiv.Visible = true;
						break;

					case ModuleState.End://可见的
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
		/// 装载控件数据
		/// </summary>
		virtual public void LoadData()
		{
		}

        /// <summary>
        /// 还原业务数据状态
        /// </summary>
        virtual public string RestoreStatus()
        {
            return "";
        }
	
		/// <summary>
		/// 保存控件数据
		/// </summary>
		virtual public string SubmitData()
		{
			return "";
		}

        /// <summary>
        /// 显示业务链结
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
			// TODO: 在此处添加构造函数逻辑
			//
		}
	}
}
