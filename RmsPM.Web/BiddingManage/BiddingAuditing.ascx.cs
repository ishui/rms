namespace RmsPM.Web.BiddingManage
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using RmsPM.Web.WorkFlowControl;
	using Rms.ORMap;

	/// <summary>
	///		BiddingAuditing 的摘要说明。
	/// </summary>
	public partial class BiddingAuditing : BiddingControlBase
	{
		protected System.Web.UI.HtmlControls.HtmlGenericControl OperableDiv;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdProjectName;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdContractNember;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdBiddingTitle;
      


		/// <summary>
		/// 业务代码
		/// </summary>
		private string _BiddingCode = "";

		private ModuleState _State2 = ModuleState.Unbeknown;
		private ModuleState _State3 = ModuleState.Unbeknown;
		private ModuleState _State4 = ModuleState.Unbeknown;
		private ModuleState _State5 = ModuleState.Unbeknown;

        private ModuleState _MainState = ModuleState.Unbeknown;
        private ModuleState _Attachstate = ModuleState.Unbeknown;
		public ModuleState State2
		{
			get
			{
				if ( _State2 == ModuleState.Unbeknown )
				{
					if(this.ViewState["_State2"] != null)
						return (ModuleState)this.ViewState["_State2"];
					return ModuleState.Unbeknown;
				}
				return _State2;
			}
			set
			{
				_State2 = value;
				this.ViewState["_State2"] = value;
			}
		}

        public string MaxMoney
        {
            get
            {
                if (this.ViewState["MaxMoney"]==null)
                    return "0";
                return this.ViewState["MaxMoney"].ToString();
            }
        }

		public ModuleState State3
		{
			get
			{
				if ( _State3 == ModuleState.Unbeknown )
				{
					if(this.ViewState["_State3"] != null)
						return (ModuleState)this.ViewState["_State3"];
					return ModuleState.Unbeknown;
				}
				return _State3;
			}
			set
			{
				_State3 = value;
				this.ViewState["_State3"] = value;
			}
		}
		public ModuleState State4
		{
			get
			{
				if ( _State4 == ModuleState.Unbeknown )
				{
					if(this.ViewState["_State4"] != null)
						return (ModuleState)this.ViewState["_State4"];
					return ModuleState.Unbeknown;
				}
				return _State4;
			}
			set
			{
				_State4 = value;
				this.ViewState["_State4"] = value;
			}
		}
		public ModuleState State5
		{
			get
			{
				if ( _State5 == ModuleState.Unbeknown )
				{
					if(this.ViewState["_State5"] != null)
						return (ModuleState)this.ViewState["_State5"];
					return ModuleState.Unbeknown;
				}
				return _State5;
			}
			set
			{
				_State5 = value;
				this.ViewState["_State5"] = value;
			}
		}
		/// <summary>
		/// 合约部意见设置
		/// </summary>
		public ModuleState SetAgreementMessage
		{
			get
			{
				if(this.ViewState["_SetAgreementMessage"] != null)
						return (ModuleState)this.ViewState["_SetAgreementMessage"];
				else
					return ModuleState.Unbeknown;
			}
			set
			{
				this.ViewState["_SetAgreementMessage"] = value;
			}
		}
		/// <summary>
		/// 工程部意见设置
		/// </summary>
		public ModuleState SetProjectMessage
		{
			get
			{
				if(this.ViewState["_SetProjectMessage"] != null)
					return (ModuleState)this.ViewState["_SetProjectMessage"];
				else
				return ModuleState.Unbeknown;
			}
			set
			{
				this.ViewState["_SetProjectMessage"] = value;
			}
		}
		/// <summary>
		/// 工程部意见设置
		/// </summary>
		public ModuleState SetAdviserMessage
		{
			get
			{
				if(this.ViewState["_SetProjectMessage"] != null)
					return (ModuleState)this.ViewState["_SetProjectMessage"];
				else
					return ModuleState.Unbeknown;
			}
			set
			{
				this.ViewState["_SetProjectMessage"] = value;
			}
		}		
		/// <summary>
		/// 业务代码
		/// </summary>
		public string BiddingCode
		{
			get
			{
				if ( _BiddingCode == "" )
				{
					if(this.ViewState["_BiddingCode"] != null)
						return this.ViewState["_BiddingCode"].ToString();
					return "";
				}
				return _BiddingCode;
			}
			set
			{
				_BiddingCode = value;
				this.ViewState["_BiddingCode"] = value;
			}
		}
		public bool SupplierSelectedFlag
		{
			get
			{
				bool Flag = false;
				for(int i=0;i<this.Repeater1.Items.Count;i++)
				{
					if(((HtmlInputCheckBox)this.Repeater1.Items[i].FindControl("chkAuditing")).Checked)
					{
						Flag = true;
					}
				}
				return Flag;
			}
		}


        /// <summary>
        /// 附件1
        /// </summary>
        public ModuleState MainState
        {
            get
            {
                if (_MainState == ModuleState.Unbeknown)
                {
                    if (this.ViewState["_MainState"] != null)
                        return (ModuleState)this.ViewState["_MainState"];
                    return ModuleState.Unbeknown;
                }
                return _MainState;
            }
            set
            {
                _MainState = value;
                this.ViewState["_MainState"] = value;
            }
        }

        /// <summary>
        /// 附件1
        /// </summary>
        public ModuleState Attachstate
        {
            get
			{
                if (_Attachstate == ModuleState.Unbeknown)
				{
                    if (this.ViewState["_Attachstate"] != null)
                        return (ModuleState)this.ViewState["_Attachstate"];
					return ModuleState.Unbeknown;
				}
                return _Attachstate;
			}
			set
			{
                _Attachstate = value;
                this.ViewState["_Attachstate"] = value;
			}
        }

		/// ****************************************************************************
		/// <summary>
		/// 组件加载
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// ****************************************************************************
		protected void Page_Load(object sender, System.EventArgs e)
		{
            AttachMentAdd1.AttachMentType = "BiddingReturnModify1";
            AttachMentList1.AttachMentType = "BiddingReturnModify1";
         
            if (this.ApplicationCode != "")
            {
                AttachMentAdd1.MasterCode = this.ApplicationCode;
                AttachMentList1.MasterCode = this.ApplicationCode;
               
            }
		}

		/// ****************************************************************************
		/// <summary>
		/// 组件初始化
		/// </summary>
		/// ****************************************************************************
		public void InitControl()
		{
			if(this.State == ModuleState.Sightless)//不可见的
			{
				this.Visible = false;
			}
			else if(this.State == ModuleState.Operable)//可操作的
			{   
               
				LoadData(true);
                
			}
			else if(this.State == ModuleState.Eyeable)//可见的
			{
              
				LoadData(false);
			}
			else if(this.State == ModuleState.Sightless)//其它,隐藏商务标及金额
			{
              
				LoadData(false);
			}
			else if(this.State == ModuleState.Begin)//可见的
			{
              
				LoadData(false);
			}
			else if(this.State == ModuleState.End)//可见的
			{
             
				LoadData(false);
			}
			else
			{
				this.Visible = false;
			}



            //以下为判断控件
            if (this.MainState == ModuleState.Sightless)//不可见的
            {
                this.trOperableFile.Visible = false;
                this.trEyeableFile.Visible = false;
            }
            else if (this.MainState == ModuleState.Operable)//可操作的
            {
                this.trOperableFile.Visible = true;
                this.trEyeableFile.Visible = false;
               

            }
            else if (this.MainState == ModuleState.Eyeable)//可见的
            {

                this.trOperableFile.Visible = false;
                this.trEyeableFile.Visible = true;
            }
            else if (this.MainState == ModuleState.Sightless)//其它,隐藏商务标及金额
            {
                this.trOperableFile.Visible = false;
                this.trEyeableFile.Visible = true;
               
            }
            else if (this.MainState == ModuleState.Begin)//可见的
            {
                this.trOperableFile.Visible = false;
                this.trEyeableFile.Visible = true;
               
            }
            else if (this.MainState == ModuleState.End)//可见的
            {
                this.trOperableFile.Visible = false;
                this.trEyeableFile.Visible = true;
              
            }
            else
            {
                this.trOperableFile.Visible = false;
                this.trEyeableFile.Visible = false;
            }

            if (Attachstate == ModuleState.Unbeknown && Attachstate == ModuleState.Sightless)
            {
                this.trOperableFile.Visible = false;
                this.trEyeableFile.Visible = false;
            }

		}
		/// ****************************************************************************
		/// <summary>
		/// 数据加载
		/// </summary>
		/// ****************************************************************************
		private void LoadData(bool Flag)
		{
            BLL.BiddingManage bm = new BLL.BiddingManage();
			BLL.BiddingReturn cBiddingReturn = new BLL.BiddingReturn();
			if(this.ApplicationCode != "")
			{
				cBiddingReturn.BiddingEmitCode = this.ApplicationCode;
                BLL.BiddingEmit cBiddingEmit = new BLL.BiddingEmit();
                cBiddingEmit.BiddingEmitCode = cBiddingReturn.BiddingEmitCode;
				this.BiddingCode = cBiddingEmit.BiddingCode;
                bm.BiddingCode = this.BiddingCode;
                this.ApplicationCode = bm.GetLastBiddingEmitCode();
                cBiddingReturn.BiddingEmitCode = this.ApplicationCode;
			}
			else
			{
                bm.BiddingCode = this.BiddingCode;
                this.ApplicationCode = bm.GetLastBiddingEmitCode();
				cBiddingReturn.BiddingEmitCode = this.ApplicationCode;
			}

			this.ProjectCode = bm.ProjectCode;

			DataTable dt = cBiddingReturn.GetBiddingReturns();
			//获取最后的压价信息
			dt = BLL.BiddingSystem.GetAuditingMessage(dt,this.BiddingCode,this.ApplicationCode);
			DataView dv1 = new DataView(dt);
			//按价格排
			DataView dv2 = new DataView(dt);
			dv2.Sort = "BiddingDtlCode,Money";
			int le = dv1.Table.Rows.Count;
			//DataRow dr
			dv1.Table.Columns.Add("myState",System.Type.GetType("System.String"));
			int j=0;
			int k=0;
            decimal tempMoney = 0;
			foreach(DataRowView drv2 in dv2)
			{
				j++;
				foreach(DataRowView drv1 in dv1)
				{
                    if (drv2["BiddingReturnCode"] == drv1["BiddingReturnCode"] && drv2["BiddingDtlCode"] == drv1["BiddingDtlCode"])
					{
						drv1["myState"]=drv2["State"];

                        if (System.Convert.ToDecimal(drv1["Money"]) > tempMoney)
                        {
                            this.ViewState["MaxMoney"] = drv1["Money"].ToString();
                            tempMoney = System.Convert.ToDecimal(drv1["Money"]);
                        }
						break;
					}
				}
			}
			this.Repeater1.DataSource = dv1;
			this.Repeater1.DataBind();
			for(int i=0;i<this.Repeater1.Items.Count;i++)
			{
				
				if(Flag)
				{
					((HtmlInputCheckBox)this.Repeater1.Items[i].FindControl("chkAuditing")).Visible = true;
					((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanAuditing")).Visible = false;
					((HtmlInputCheckBox)this.Repeater1.Items[i].FindControl("chkAuditing")).Checked = (dt.Rows[i]["Flag"].ToString() == "1");
				}
				else
				{
					((HtmlInputCheckBox)this.Repeater1.Items[i].FindControl("chkAuditing")).Visible = false;
					((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanAuditing")).Visible = true;
					((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanAuditing")).InnerHtml = this.SpanText("1",dt.Rows[i]["Flag"].ToString());
				}
				if(this.State1 == ModuleState.Operable)
				{
                   
					for( k=0;k<((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioDesign")).Items.Count;k++ )
					{
						ListItem ud_Item = ((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioDesign")).Items[k];
						if ( ud_Item.Value == dt.Rows[i]["Design"].ToString() )
						{
							((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioDesign")).Items[k].Selected = true;
							break;
						}
					}
//					((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioDesign")).SelectedValue = dt.Rows[i]["Design"].ToString();
					((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioDesign")).Visible = true;
					((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanDesign")).Visible = false;
				}
				else
				{
                   
					((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanDesign")).InnerHtml = this.SpanText("2",dt.Rows[i]["Design"].ToString());
					((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioDesign")).Visible = false;
					((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanDesign")).Visible = true;
				}
				if(this.State2 == ModuleState.Operable)
				{
					for( k=0;k<((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioProject")).Items.Count;k++ )
					{
						ListItem ud_Item = ((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioProject")).Items[k];
						if ( ud_Item.Value == dt.Rows[i]["Design"].ToString() )
						{
							((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioProject")).Items[k].Selected = true;
							break;
						}
					}

					((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioProject")).Visible = true;
					((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanProject")).Visible = false;
				}
				else
				{
					((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanProject")).InnerHtml = this.SpanText("2",dt.Rows[i]["Project"].ToString());
					((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioProject")).Visible = false;
					((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanProject")).Visible = true;
				}
				if(this.State3 == ModuleState.Operable)
				{
					for( k=0;k<((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioConsultant")).Items.Count;k++ )
					{
						ListItem ud_Item = ((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioConsultant")).Items[k];
						if ( ud_Item.Value == dt.Rows[i]["Design"].ToString() )
						{
							((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioConsultant")).Items[k].Selected = true;
							break;
						}
					}
					((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioConsultant")).Visible = true;
					((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanConsultant")).Visible = false;
				}
				else
				{
					((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanConsultant")).InnerHtml = this.SpanText("2",dt.Rows[i]["Consultant"].ToString());
					((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioConsultant")).Visible = false;
					((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanConsultant")).Visible = true;
				}
				if(this.State4 == ModuleState.Operable)
				{
					((HtmlGenericControl)this.Repeater1.Items[i].FindControl("txtState")).Visible = true;
					((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanState")).Visible = false;
				}
				else
				{
					((HtmlGenericControl)this.Repeater1.Items[i].FindControl("txtState")).Visible = false;
					((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanState")).Visible = true;
				}
				//商务标报价是否显示
				if(this.State5 == ModuleState.Sightless)
				{
					((HtmlGenericControl)this.Repeater1.Items[i].FindControl("txtState")).InnerHtml = "&nbsp;";
					((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanState")).InnerHtml = "&nbsp;";
					((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spMoney")).InnerHtml = "&nbsp;";
				}
				//显示评选结果
				if(this.SetAgreementMessage ==ModuleState.Sightless)
				{
					((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanDesign")).Visible = false;
				}
				if(this.SetProjectMessage ==ModuleState.Sightless)
				{
					((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanProject")).Visible = false;
				}
				if(this.SetAdviserMessage ==ModuleState.Sightless)
				{
					((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanConsultant")).Visible = false;
				}
			}
			dt.Dispose();
		}

		/// ****************************************************************************
		/// <summary>
		/// 提交数据
		/// </summary>
		/// ****************************************************************************
		public void SubmitData()
		{
			DAL.QueryStrategy.BiddingReturnStrategyBuilder sb = new RmsPM.DAL.QueryStrategy.BiddingReturnStrategyBuilder();
			sb.AddStrategy( new Strategy( DAL.QueryStrategy.BiddingReturnStrategyName.BiddingEmitCode,this.ApplicationCode));
				
			string sql = sb.BuildMainQueryString();

			EntityData entity = new EntityData("BiddingReturn");
			dao.FillEntity(sql, entity);

			for(int i=0;i<this.Repeater1.Items.Count;i++)
			{
				string _BiddingReturnCode = ((HtmlInputText)this.Repeater1.Items[i].FindControl("txtBiddingReturnCode")).Value.Trim();
				string _Design = ((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioDesign")).SelectedValue;
				string _Project = ((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioProject")).SelectedValue;
				string _Consultant = ((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioConsultant")).SelectedValue;
				string _State = ((HtmlGenericControl)this.Repeater1.Items[i].FindControl("txtState")).InnerText.Trim();
				string _Flag = "0";
				
				if(((HtmlInputCheckBox)this.Repeater1.Items[i].FindControl("chkAuditing")).Checked)
				{
					_Flag = "1";
					//string flag="1";
					try
					{
						string supplier=((HtmlInputText)this.Repeater1.Items[i].FindControl("hiddenSupplierCode")).Value.Trim();
						BLL.BiddingEmit bemit = new RmsPM.BLL.BiddingEmit();
						bemit.IsLowOfPriceByBiddingCode(_BiddingReturnCode);
					}
					catch(Exception ex)
					{
						Response.Write(Rms.Web.JavaScript.Alert(true,ex.Message));
					}
				}

				DataRow[] dr = entity.CurrentTable.Select("BiddingReturnCode="+_BiddingReturnCode);
				if(_Design != "" && this.State1 == ModuleState.Operable)
				{
					dr[0]["Design"] = _Design;
				}
				if(_Project != "" && this.State2 == ModuleState.Operable)
				{
					dr[0]["Project"] = _Project;
				}
				if(_Consultant != "" && this.State3 == ModuleState.Operable)
				{
					dr[0]["Consultant"] = _Consultant;
				}
				if(_State != "" && this.State4 == ModuleState.Operable)
				{
					dr[0]["State"] = _State;
				}
				if(this.State == ModuleState.Operable)
				{
					dr[0]["Flag"] = _Flag;
				}
			}

			dao.SubmitEntity(entity);
            if (this.MainState == ModuleState.Operable)
            {
                this.AttachMentAdd1.SaveAttachMent(this.ApplicationCode);
            }
			if(this.State == ModuleState.Operable)
			{
				BLL.Bidding bidding = new BLL.Bidding();
				bidding.BiddingCode = this.BiddingCode;
				bidding.dao = this.dao;
				bidding.State = "4";
				bidding.BiddingSubmit();
			}
		}

		public string SpanText(string Type,string Value)
		{
			string returnText = "&nbsp;";
			if(Type == "1")
			{
				if(Value == "1")
					returnText = "√";
			}
			else
			{
				if(Value == "0")
					returnText = "不符合";
				if(Value == "1")
					returnText = "符合";
			}
			return returnText;
		}
		

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		设计器支持所需的方法 - 不要使用代码编辑器
		///		修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		#region 在流程中初始化,及保存数据等
		public void InitOnWorkFlow(RmsPM.Web.WorkFlowControl.WorkFlowToolbar WorkFlowToolbar1)
		{
			ApplicationCode = WorkFlowToolbar1.ApplicationCode;
			State = WorkFlowToolbar1.GetModuleState("SupplierSelect");
			State1 = WorkFlowToolbar1.GetModuleState("Select1");
			State2 = WorkFlowToolbar1.GetModuleState("Select2");
			State3 = WorkFlowToolbar1.GetModuleState("Select3");
			State4 = WorkFlowToolbar1.GetModuleState("Select4");
			State5 = WorkFlowToolbar1.GetModuleState("Select5");
			SetAgreementMessage = WorkFlowToolbar1.GetModuleState("合约部");
			SetProjectMessage = WorkFlowToolbar1.GetModuleState("工程部");
			SetAdviserMessage = WorkFlowToolbar1.GetModuleState("顾问公司");
			BiddingCode = Request["BiddingCode"]+"";
			UserCode = ((User)Session["User"]).UserCode;
			InitControl();
		}
		#endregion

    

	}
}
