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
	///		UCBiddingSupplierModify ��ժҪ˵����
	/// </summary>
	public partial class UCBiddingSupplierModify : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��ProjectInfo
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


		public event EventHandler SaveDataEvent;

		#region --- ˽������ --------------------------------------------------------------------------
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
		/// ��ʾ����
		/// </summary>
		private string _DoType = "";

		#endregion --------------------------------------------------------------------------

		#region --- ˽�з��� --------------------------------------------------------------------------

		#endregion --------------------------------------------------------------------------

		#region --- �������� --------------------------------------------------------------------------

       

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
		/// ��ʾ����
		/// </summary>
		public string DoType
		{
			get{return this._DoType;}
			set{this._DoType=value;}
		}

		#endregion --------------------------------------------------------------------------

		#region --- �������� --------------------------------------------------------------------------

		/// <summary>
		/// �ؼ���ʼ��
		/// </summary>
		public void IniControl()
		{
			try
			{
				this.EyeableDiv.Visible = false;
				this.OperableDiv.Visible = false;

				this.HideBiddingPrejudicationCode.Value = this._BiddingPrejudicationCode;
				this.HideBiddingSupplierCode.Value = this._BiddingSupplierCode;
                ////ѡ��Ӧ��
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
                        this.txtPerson.InnerHtml = "���̱���";
                        this.tdPerson.InnerHtml = "���̱���";
                        
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
		/// װ�ؿؼ�����
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
					//Response.Write(BLL.ProjectRule.GetSupplierName(cBiddingSupplier.SupplierCode)+"����:"+cBiddingSupplier.NominateUser);
					//Response.Write("���"+tdSupplierName.InnerHtml+"ʷ��ǰ��"+tdNominateUser.InnerHtml);

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
		/// �������
		/// </summary>
		/// <param name="Hint">������ʾ��Ϣ</param>
		public bool CheckData(out string Hint)
		{
			Hint = "";

			try
			{
				if ( ""==this.HideSupplierCode.Value.Trim() )
				{
					Hint = "��ѡ��Ӧ�̣�";
					return false;
				}

                //if ( ""==this.txtNominateUser.Value.Trim() )
                //{
                //    Hint = "����д�����ˣ�";
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
		/// ��������
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
				//��ѡ�����������¼
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
		/// ɾ������
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

		#region �������г�ʼ��,���������ݵ�
		/// <summary>
		/// �������г�ʼ��,������ ������� Code
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
