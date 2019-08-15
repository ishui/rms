namespace RmsPM.Web.BiddingManage
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Rms.Check;
	using Rms.Web;

	/// <summary>
	///		UCBiddingSupplierModify 的摘要说明。
	/// </summary>
	public partial class UCBiddingSupplierModify : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面ProjectInfo
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


		public event EventHandler SaveDataEvent;

		#region --- 私有属性 --------------------------------------------------------------------------
        private string _SupplierCode="" ;
        private string _SupplierName="" ;
		/// <summary>
		/// BiddingPrejudicationCode
		/// </summary>
		private string _BiddingPrejudicationCode = "";

		/// <summary>
		/// BiddingSupplierCode
		/// </summary>
		private string _BiddingSupplierCode = "";

		/// <summary>
		/// 显示类型
		/// </summary>
		private string _DoType = "";

		#endregion --------------------------------------------------------------------------

		#region --- 私有方法 --------------------------------------------------------------------------

		#endregion --------------------------------------------------------------------------

		#region --- 公共属性 --------------------------------------------------------------------------

       

        public string SupplierCode
        {
            get
            {

                if (_SupplierCode == "")
				{
                    if (this.ViewState["_SupplierCode"] != null)
                        return this.ViewState["_SupplierCode"].ToString();
					return "";
				}
                return _SupplierCode;
            }
            set
            {
                _SupplierCode = value;
                this.ViewState["_SupplierCode"] = value;
            }
        }

        public string SupplierName
        {
            get
            {
                if (_SupplierName == "")
                {
                    if (this.ViewState["_SupplierName"] != null)
                        return this.ViewState["_SupplierName"].ToString();
                    return "";
                }
                return _SupplierName;
            }
            set
            {
                _SupplierName = value;
                this.ViewState["_SupplierName"] = value;
            }
        }
            
		public string BiddingPrejudicationCode
		{
			get{return this._BiddingPrejudicationCode;}
			set{this._BiddingPrejudicationCode=value;}
		}

		public string BiddingSupplierCode
		{
			get{return this._BiddingSupplierCode;}
			set{this._BiddingSupplierCode=value;}
		}

		/// <summary>
		/// 显示类型
		/// </summary>
		public string DoType
		{
			get{return this._DoType;}
			set{this._DoType=value;}
		}

		#endregion --------------------------------------------------------------------------

		#region --- 公共方法 --------------------------------------------------------------------------

		/// <summary>
		/// 控件初始化
		/// </summary>
		public void IniControl()
		{
			try
			{
				this.EyeableDiv.Visible = false;
				this.OperableDiv.Visible = false;

				this.HideBiddingPrejudicationCode.Value = this._BiddingPrejudicationCode;
				this.HideBiddingSupplierCode.Value = this._BiddingSupplierCode;
                ////选择供应商
                string company = System.Configuration.ConfigurationManager.AppSettings["PMName"].ToLower();
                //if (this.SupplierCode == "" && this.SupplierName == "")
                //{
                //    switch (company)
                //    {
                //        case "tangchenpm":
                //        case "yefengpm":
                //        case "anjupm":
                //        case "shidaipm":
                //        case "gaokepm":
                //        case "zhudingpm":
                //            this.SupplierCode = "ucOperationControl_UCBiddingSupplierModify1_HideSupplierCode";
                //            this.SupplierName = "ucOperationControl_UCBiddingSupplierModify1_txtSupplierName";
                //            break;
                //        default:
                //            this.SupplierCode = "UCBiddingSupplierModify1_HideSupplierCode";
                //            this.SupplierName = "UCBiddingSupplierModify1_txtSupplierName";
                //            break;
                //    }
                //}


                this.SupplierCode = this.ClientID + "_HideSupplierCode";
                this.SupplierName = this.ClientID + "_txtSupplierName";
               
                switch (company)
                {
                    case "tangchenpm":
                        this.txtPerson.InnerHtml = "厂商报价";
                        this.tdPerson.InnerHtml = "厂商报价";
                        
                        break;
                }
				if ( "SingleView"==this._DoType )
				{
					this.EyeableDiv.Visible = true;
				}
				else if ( "SingleModify"==this._DoType )
				{
					this.OperableDiv.Visible = true;
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
		public void LoadData()
		{
			try
			{
				if ( ""!=this._BiddingSupplierCode )
				{
					BLL.BiddingSupplier cBiddingSupplier = new BLL.BiddingSupplier();
					cBiddingSupplier.BiddingSupplierCode = this._BiddingSupplierCode;

					this.HideSupplierCode.Value = cBiddingSupplier.SupplierCode;
					this.HideBiddingSupplierCode.Value = cBiddingSupplier.BiddingSupplierCode;
					this.HideBiddingPrejudicationCode.Value = cBiddingSupplier.BiddingPrejudicationCode;

					this.tdSupplierName.InnerHtml = BLL.ProjectRule.GetSupplierName(cBiddingSupplier.SupplierCode);
					this.tdNominateUser.InnerHtml = cBiddingSupplier.NominateUser;
					//Response.Write(BLL.ProjectRule.GetSupplierName(cBiddingSupplier.SupplierCode)+"名字:"+cBiddingSupplier.NominateUser);
					//Response.Write("表格"+tdSupplierName.InnerHtml+"史无前例"+tdNominateUser.InnerHtml);

					this.txtSupplierName.Value = BLL.ProjectRule.GetSupplierName(cBiddingSupplier.SupplierCode);
					this.txtNominateUser.Value = cBiddingSupplier.NominateUser;
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


		/// <summary>
		/// 检查数据
		/// </summary>
		/// <param name="Hint">错误提示信息</param>
		public bool CheckData(out string Hint)
		{
			Hint = "";

			try
			{
				if ( ""==this.HideSupplierCode.Value.Trim() )
				{
					Hint = "请选择供应商！";
					return false;
				}

                //if ( ""==this.txtNominateUser.Value.Trim() )
                //{
                //    Hint = "请填写提名人！";
                //    return false;
                //}

				return true;
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return false;
			}
		}


		/// <summary>
		/// 保存数据
		/// </summary>
		public void SaveData()
		{
			try
			{
				BLL.BiddingSupplier cBiddingSupplier = new BLL.BiddingSupplier();

				string strOrderCode = "0";
				if ( ""==this.HideBiddingSupplierCode.Value.Trim() )
				{
					cBiddingSupplier.BiddingPrejudicationCode = this.HideBiddingPrejudicationCode.Value;
					int rCount = cBiddingSupplier.GetBiddingSuppliers().Rows.Count;
					if ( 0<rCount )
					{
						strOrderCode = (BLL.ConvertRule.ToInt(cBiddingSupplier.GetBiddingSuppliers().Rows[rCount-1]["OrderCode"])+1).ToString();
					}
					else
					{
						strOrderCode = "1";
					}
				}
				else
				{
					cBiddingSupplier.BiddingSupplierCode = this.HideBiddingSupplierCode.Value;
					strOrderCode = cBiddingSupplier.OrderCode;
				}
				string ss =  this.HideBiddingSupplierCode.Value;
				cBiddingSupplier.BiddingSupplierCode = this.HideBiddingSupplierCode.Value;
				cBiddingSupplier.BiddingPrejudicationCode = this.HideBiddingPrejudicationCode.Value;
				cBiddingSupplier.SupplierCode = this.HideSupplierCode.Value;
				cBiddingSupplier.NominateUser = this.txtNominateUser.Value;
				cBiddingSupplier.NominateDate = DateTime.Today.ToString();
				cBiddingSupplier.UserCode = ((User)Session["User"]).UserCode;
				cBiddingSupplier.OrderCode = strOrderCode;
				cBiddingSupplier.State = "";
				cBiddingSupplier.Flag = "";
				cBiddingSupplier.BiddingSupplierSubmit();
				//在选择表中新增记录
				//BLL.Bidding_SupplierDepartmentIdea bsd = new RmsPM.BLL.Bidding_SupplierDepartmentIdea();
				//bsd.BiddingSupplierCode = this.HideBiddingPrejudicationCode.Value;
				//BLL.BiddingSystem.InsertDepartMent
				//BLL.Bidding_SupplierDepartmentIdea bidd = new RmsPM.BLL.Bidding_SupplierDepartmentIdea();
				//Rms.ORMap
				//BLL.SystemRule.get
				string code = DAL.EntityDAO.SystemManageDAO.GetSysCodeByName("BiddingSupplier");
				BLL.BiddingSystem.InsertDepartMent(HideBiddingPrejudicationCode.Value,code);


                RmsPM.BLL.BiddingGradeMessage cbiddingGradeMessage = new RmsPM.BLL.BiddingGradeMessage();
                cbiddingGradeMessage.BiddingGradeMessageCode = "";
                cbiddingGradeMessage.ApplicationCode = cBiddingSupplier.BiddingSupplierCode;
                cbiddingGradeMessage.BiddingGradeTypeCode = "100001";
                cbiddingGradeMessage.ProjectManage = this.txtNominateUser.Value;
                cbiddingGradeMessage.State = "1";
                cbiddingGradeMessage.BiddingGradeMessageAdd();

				//Response.Write(ss+"dfgsd"+this.HideBiddingSupplierCode.Value);
				//bidd.BiddingSupplierCode = this.HideBiddingSupplierCode.Value;
				//bidd.BiddingPrejudicationCode = this.HideBiddingPrejudicationCode.Value;
				//bidd.Bidding_SupplierDepartmentIdeaSubmit();
				
				txtSupplierName.Value="";
				HideSupplierCode.Value="";
				txtNominateUser.Value="";

			}
			catch(Exception ex)
			{
				Response.Write(Rms.Web.JavaScript.Alert(true,ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


		/// <summary>
		/// 删除数据
		/// </summary>
		public void DeleteData()
		{
			try
			{
				BLL.BiddingSupplier cBiddingSupplier = new BLL.BiddingSupplier();
				cBiddingSupplier.BiddingSupplierCode = this.HideBiddingSupplierCode.Value;
				cBiddingSupplier.BiddingSupplierDelete();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		#endregion --------------------------------------------------------------------------

		#region 在流程中初始化,及保存数据等
		/// <summary>
		/// 在流程中初始化,并返回 审批表的 Code
		/// </summary>
		/// <param name="WorkFlowToolbar1"></param>
		/// <returns>BiddingPrejudicationCode</returns>
		public string InitOnWorkFlow(RmsPM.Web.WorkFlowControl.WorkFlowToolbar WorkFlowToolbar1)
		{
			return "";
		}
		#endregion


		protected void btnAdd_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string myHint = "";

				if ( this.CheckData(out myHint) )
				{
					this.SaveData();
					this.SaveDataEvent(this,EventArgs.Empty);
					//txtSupplierName.Value
				}
				else
				{
					Response.Write( JavaScript.Alert(true,myHint) );
					return;
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

	}
}
