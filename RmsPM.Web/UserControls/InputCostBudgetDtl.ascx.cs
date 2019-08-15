namespace RmsPM.Web.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Rms.ORMap;

	/// <summary>
	/// InputCostBudgetDtl ��ժҪ˵����
	/// </summary>
	public partial class InputCostBudgetDtl : System.Web.UI.UserControl
	{

		public Boolean Enable
		{
			set 
			{ 
				hid_Enable.Checked = value; 
				div_SearchButton.Visible = hid_Enable.Checked;
			}

			get
			{
				return hid_Enable.Checked;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			SetDiv();

			if (this.Visible) 
			{
				this.txtInput.Attributes["ClientID"] = this.ClientID;

//				string reload = Rms.Web.JavaScript.ScriptStart;
//				reload += @"var ClientID = '" + this.ClientID + "';" + "\n" ;
//				reload += Rms.Web.JavaScript.ScriptEnd;
//				Response.Write(reload);

				if (!Page.IsPostBack) 
				{
					IniPage();
				}
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
            IniSelectAllLeaf();

		}
		#endregion

		private void SetDiv()
		{
			try 
			{
//				this.divName.InnerText = this.txtName.Value;
				this.divName.InnerText = this.txtFullName.Value;
				this.divHint.InnerText = this.txtHint.Value;
				this.divDesc.InnerText = this.txtDesc.Value;
			}
			catch(Exception ex) 
			{
				throw ex;
			}
		}

		private void IniPage() 
		{
			try
			{
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ʼ��ҳ��ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ��ʧ�ܣ�" + ex.Message));
			}
		}

        /// <summary>
        /// ��ʼ���Ƿ��ѡ�����нڵ�
        /// </summary>
        private void IniSelectAllLeaf()
        {
            try
            {
                if (this.txtSelectAllLeaf.Value == "")
                {
                    if (BLL.ConvertRule.ToString(System.Configuration.ConfigurationSettings.AppSettings["SelectCBSAllNode"]) == "1")
                    {
                        this.txtSelectAllLeaf.Value = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		/// <summary>
		/// ������Ŀ���룬��ʼ��
		/// </summary>
		/// <param name="ProjectCode"></param>
		private void SetProject(string ProjectCode)
		{
			try 
			{
				this.txtProjectCode.Value = ProjectCode;
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ʼ��ҳ��ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ��ʧ�ܣ�" + ex.Message));
			}
		}

		public string ProjectCode
		{
			get {return this.txtProjectCode.Value;}
			set {SetProject(value);}
		}

		/// <summary>
		/// ���ô���
		/// </summary>
		/// <param name="code"></param>
		private void SetCode(string CostBudgetSetCode, string CostCode) 
		{
			try 
			{
				this.txtCode.Value = CostCode;
				this.txtCostBudgetSetCode.Value = CostBudgetSetCode;
				this.txtName.Value = "";
				this.txtSortID.Value = "";
				this.txtPBSType.Value = "";
				this.txtPBSCode.Value = "";
				this.txtPBSName.Value = "";
				this.txtHint.Value = "";
				this.txtDesc.Value = "";

				if ((CostCode != "") && (CostBudgetSetCode != ""))
				{
					//Ԥ�����ñ�
					EntityData entity = DAL.EntityDAO.CostBudgetDAO.GetV_CostBudgetSetByCode(CostBudgetSetCode);
					if (entity.HasRecord()) 
					{
						this.txtCostBudgetSetCode.Value = entity.GetString("CostBudgetSetCode");
						this.txtPBSType.Value = entity.GetString("PBSType");
						this.txtPBSCode.Value = entity.GetString("PBSCode");
						this.txtPBSName.Value = entity.GetString("PBSName");

						//������
						EntityData entityCBS = DAL.EntityDAO.CBSDAO.GetCBSByCode(CostCode);
						if (entityCBS.HasRecord()) 
						{
							this.txtCode.Value = entityCBS.GetString("CostCode");
							this.txtName.Value = entityCBS.GetString("CostName");
							this.txtSortID.Value = entityCBS.GetString("SortID");
							this.txtFullName.Value = BLL.CBSRule.GetCostFullName(CostCode);

							if (!SelectAllLeaf) 
							{
								if (!BLL.CBSRule.CheckCBSLeafNode(this.txtCode.Value))
								{
									this.txtHint.Value = "����ĩ�������� ��";
								}
							}

							if (this.txtPBSName.Value != "")
							{
								this.txtDesc.Value = "��λ���̣�" + this.txtPBSName.Value;
							}
						}
					}
					entity.Dispose();
				}

				this.txtInput.Value = this.txtSortID.Value;
				SetDiv();
			}
			catch ( Exception ex )
			{
				throw ex;
			}
		}

		public string CostName 
		{
			get {return this.txtName.Value;}
		}

		public string Hint 
		{
			get {return this.txtHint.Value;}
		}

		public string SortID 
		{
			get {return this.txtSortID.Value;}
		}

		public string CostCode 
		{
			get {return this.txtCode.Value;}
			set {SetCode(CostBudgetSetCode, value);}
		}

		public string CostBudgetSetCode 
		{
			get {return this.txtCostBudgetSetCode.Value;}
			set {SetCode(value, CostCode);}
		}

		public string PBSType 
		{
			get {return this.txtPBSType.Value;}
            set { }
		}

		public string PBSCode 
		{
			get {return this.txtPBSCode.Value;}
            set { }
		}

		public string PBSName 
		{
			get {return this.txtPBSName.Value;}
		}

		/// <summary>
		/// �Ƿ������ѡ�������
		/// </summary>
		public bool SelectAllLeaf
		{
			get {return (this.txtSelectAllLeaf.Value == "1");}
			set {this.txtSelectAllLeaf.Value = (value?"1":"0");}
		}

	}
}
