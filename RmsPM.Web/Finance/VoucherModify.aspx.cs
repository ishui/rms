using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Rms.ORMap;
using RmsPM.DAL;
using RmsPM.BLL;
using RmsPM.DAL.QueryStrategy;
using Rms.Web;
using Infragistics.WebUI.WebDataInput;

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// VoucherModify ��ժҪ˵����
	/// </summary>
	public partial class VoucherModify : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlAnchor lnkAddNew;
		protected RmsPM.WebControls.ToolsBar.ToolsButton btnBatchSubjectD;
		protected System.Web.UI.WebControls.TextBox a1;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser WebDateChooser1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtDetailCount;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		/// <summary>
		/// ��ʼ��ƾ֤���Ƿ���޸�
		/// </summary>
		private void IniVoucherIDModify()
		{
			try 
			{
				//�Ƿ���޸�ƾ֤��
				ViewState["VoucherIDModify"] = BLL.SystemRule.GetProjectConfigValue(BLL.SystemRule.m_ConfigVoucherIDModify);

				string VoucherIDModify = (string)ViewState["VoucherIDModify"];
				if (VoucherIDModify == "1")
				{
					this.txtVoucherID.ReadOnly = false;
					this.txtVoucherID.BorderStyle = BorderStyle.NotSet;
				}

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void IniPage()
		{
			try 
			{
				IniVoucherIDModify();

				this.txtVoucherCode.Value = Request.QueryString["VoucherCode"];
				this.txtAct.Value = Request.QueryString["Act"];
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtRefreshScript.Value = Request.QueryString["RefreshScript"];
				this.txtParam.Value = Request.QueryString["Param"];

				//�������ݲ�����Session����
				string RelaCodeSession = Request.QueryString["RelaCodeSession"] + "";
				if (RelaCodeSession.Trim() != "") 
				{
					if (Session[RelaCodeSession] != null) 
					{
						this.txtRelaCode.Value = Session[RelaCodeSession].ToString();
						Session[RelaCodeSession] = null;
					}
				}
				else 
				{
					this.txtRelaCode.Value = Request["RelaCode"] + "";
				}

				this.dtbMakeDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
				BLL.PageFacade.LoadVoucherTypeSelect(this.sltVoucherType,"");

				switch (this.txtAct.Value.Trim().ToUpper()) 
				{
						//���������������Ϊ��������������һ��ƾ֤
					case "SALADD":
						//������					this.tdModify.Visible = false;
						break;

					default:
						break;
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadData()
		{
			string projectCode = Request["ProjectCode"]+"";
			string voucherCode = this.txtVoucherCode.Value;
			string RelaCode = this.txtRelaCode.Value;
			string act = this.txtAct.Value.Trim();
			string Param = this.txtParam.Value.Trim();
			DataTable dt = null;
			string hint = "";

			try
			{
				bool isNew = (voucherCode == "");
				if (isNew) 
				{
					if (this.txtProjectCode.Value == "") 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "����ʱ���봫����Ŀ����"));
						Response.Write(Rms.Web.JavaScript.WinClose(true));
					}
				}

				switch (act.ToUpper())
				{
					case "SALADD":
						//����������������Ƿ�������ƾ֤
						if (BLL.PaymentRule.IsExistsVoucherFromSal(RelaCode))
						{
							Response.Write(Rms.Web.JavaScript.Alert(true, "������������������ƾ֤������"));
							Response.Write(Rms.Web.JavaScript.WinClose(true));
							return;
						}
						break;

					case "SALJZADD":
						//����������ۺ�ͬ�Ƿ�������ƾ֤
						if (BLL.PaymentRule.IsExistsVoucherFromSalJZ(RelaCode))
						{
							Response.Write(Rms.Web.JavaScript.Alert(true, "�������ۺ�ͬ�����ɽ�תƾ֤������"));
							Response.Write(Rms.Web.JavaScript.WinClose(true));
							return;
						}
						break;

					case "SALCBADD":
						//����Ƿ������ɳɱ�ƾ֤
						if (BLL.SalRule.GetSalCBVoucherCode(RelaCode, this.txtProjectCode.Value) != "")
						{
							Response.Write(Rms.Web.JavaScript.Alert(true, "�ô��������ɳɱ�ƾ֤������"));
							Response.Write(Rms.Web.JavaScript.WinClose(true));
							return;
						}
						break;

					case "SALJTADD":
						//����Ƿ������ɼ���˰��ƾ֤
						if (BLL.SalRule.GetSalJTVoucherCode(RelaCode, this.txtProjectCode.Value) != "")
						{
							Response.Write(Rms.Web.JavaScript.Alert(true, "�ô��������ɼ���˰��ƾ֤������"));
							Response.Write(Rms.Web.JavaScript.WinClose(true));
							return;
						}
						break;

					case "PAYADD":
						//��鸶���Ƿ��ƾ֤
						hint = BLL.PaymentRule.CanBuildVoucherFromPayment(RelaCode);
						if (hint != "")
						{
							Response.Write(Rms.Web.JavaScript.Alert(true, hint));
							Response.Write(Rms.Web.JavaScript.WinClose(true));
							return;
						}
						break;

					case "BUILDINGCBADD":
						//���¥���Ƿ������ɳɱ�ƾ֤
						if (BLL.PaymentRule.GetBuildingCBVoucherCode(RelaCode, this.txtProjectCode.Value) != "")
						{
							Response.Write(Rms.Web.JavaScript.Alert(true, "��¥�������ɳɱ�ƾ֤������"));
							Response.Write(Rms.Web.JavaScript.WinClose(true));
							return;
						}
						break;

				}

				// �޸�һ���Ѿ����ڵ�ƾ֤
				if (!isNew)
				{
					this.txtIsNew.Value = "0";
					EntityData voucher = DAL.EntityDAO.PaymentDAO.GetVoucherByCode(voucherCode);
					this.sltVoucherType.Value = voucher.GetString("VoucherType");
					this.txtVoucherID.Text = voucher.GetString("VoucherID");
					this.dtbMakeDate.Value = voucher.GetDateTimeOnlyDate("MakeDate");
					this.txtReceiptCount.Value = voucher.GetInt("ReceiptCount").ToString();
					this.txtProjectCode.Value = voucher.GetString("ProjectCode");
                    this.txtSubjectSetCode.Value = voucher.GetString("SubjectSetCode");

					EntityData entityDtl = DAL.EntityDAO.PaymentDAO.GetV_VoucherDetailByVoucherCode(voucherCode);
					dt = entityDtl.CurrentTable;

					switch (act.ToUpper()) 
					{
						case "SALADD":
							//���������������Ϊ��������������һ��ƾ֤
							BLL.PaymentRule.BuildVoucherDetailTableFromSal(dt, RelaCode, Param, this.txtProjectCode.Value);
							break;

						case "SALJZADD":
							//�����ۺ�ͬ������Ϊ������ͬ������һ��ת��ƾ֤
							BLL.PaymentRule.BuildVoucherDetailTableFromSalJieZhuan(dt, RelaCode, Param, this.txtProjectCode.Value);
							break;

						case "SALCBADD":
							//�����۳ɱ�������һ��������һ��ƾ֤
							decimal cost = BLL.ConvertRule.ToDecimal(Param);

							BLL.PaymentRule.BuildVoucherDetailTableFromSalCB(dt, RelaCode, cost, this.txtProjectCode.Value);
							break;

						case "SALJTADD":
							//���������������һ��������һ�ż���˰��ƾ֤
							decimal money = BLL.ConvertRule.ToDecimal(Param);

							BLL.PaymentRule.BuildVoucherDetailTableFromSalJT(dt, RelaCode, money, this.txtProjectCode.Value);
							break;

						case "PAYADD":
							//�Ӹ��������Ϊ����������һ��ƾ֤
							int ReceiptCount = 0;
							BLL.VoucherRule.BuildVoucherDetailTableFromPayment(dt, RelaCode, this.txtProjectCode.Value, ref ReceiptCount, base.up_sPMName);
							this.txtReceiptCount.Value = ReceiptCount.ToString();
							break;

						case "BUILDINGCBADD":
							//��¥���ɱ����������������һ��ƾ֤
							BLL.PaymentRule.BuildVoucherDetailTableFromBuildingCB(dt, RelaCode, this.txtProjectCode.Value);
							break;

//						case "BUILDINGJTADD":
//							//��¥���ɱ����������������һ�ż���˰��ƾ֤
//							BLL.PaymentRule.BuildVoucherDetailTableFromBuildingJT(dt, RelaCode, this.txtProjectCode.Value);
//							break;

					}

					entityDtl.Dispose();
					voucher.Dispose();
				}
				else 
				{
					//����ƾ֤
					this.txtIsNew.Value = "1";
                    this.txtSubjectSetCode.Value = BLL.ProjectRule.GetSubjectSetCodeByProject(txtProjectCode.Value);

					if (this.txtVoucherID.ReadOnly)
					{
						//����ʱ��������ƾ֤��ţ�ƾ֤��=ƾ֤���
						this.txtVoucherID.Text = "(�ύʱ����)";
					}

//					voucherCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("VoucherCode");
//					this.txtVoucherCode.Value = voucherCode;
//					this.txtVoucherID.Text = voucherCode;

					switch (act.ToUpper()) 
					{
						case "SALADD":
							//���������������Ϊ��������������һ��ƾ֤
							int iReturn = 0;
							dt = BLL.PaymentRule.BuildVoucherDetailTableFromSal(RelaCode, Param, this.txtProjectCode.Value, ref iReturn);

							if (iReturn == 1) 
							{
								//���Ϊ�����и���ƾ֤��
								this.sltVoucherType.Value = "4";
							}
							else 
							{
								//���Ϊ����������ƾ֤��
								this.sltVoucherType.Value = "3";
							}

							break;

						case "SALJZADD":
							//�����ۺ�ͬ������Ϊ������ͬ������һ��ת��ƾ֤
							dt = BLL.PaymentRule.BuildVoucherDetailTableFromSalJieZhuan(RelaCode, Param, this.txtProjectCode.Value);

							//���Ϊ��ת��ƾ֤��
							this.sltVoucherType.Value = "5";

							break;

						case "SALCBADD":
							//�����۳ɱ�������һ��������һ��ƾ֤
							decimal cost = 0;
							try 
							{
								cost = decimal.Parse(Param);
							}
							catch 
							{
							}

							dt = BLL.PaymentRule.BuildVoucherDetailTableFromSalCB(RelaCode, cost, this.txtProjectCode.Value);

							//���Ϊ��ת��ƾ֤��
							this.sltVoucherType.Value = "5";
							break;

						case "SALJTADD":
							//���������������һ��������һ�ż���˰��ƾ֤
							decimal money = 0;
							try 
							{
								money = decimal.Parse(Param);
							}
							catch 
							{
							}

							dt = BLL.PaymentRule.BuildVoucherDetailTableFromSalJT(RelaCode, money, this.txtProjectCode.Value);

							//���Ϊ��ת��ƾ֤��
							this.sltVoucherType.Value = "5";

							break;

						case "PAYADD":
							//�Ӹ��������Ϊ����������һ��ƾ֤
							int ReceiptCount = 0;
                            dt = BLL.VoucherRule.BuildVoucherDetailTableFromPayment(RelaCode, this.txtProjectCode.Value, ref ReceiptCount, base.up_sPMName);
							this.txtReceiptCount.Value = ReceiptCount.ToString();

							//���Ϊ�����и���ƾ֤��
							this.sltVoucherType.Value = "4";

							break;

						case "BUILDINGCBADD":
							//��¥���ɱ����������������һ��ƾ֤
							dt = BLL.PaymentRule.BuildVoucherDetailTableFromBuildingCB(RelaCode, this.txtProjectCode.Value);

							//���Ϊ��ת��ƾ֤��
							this.sltVoucherType.Value = "5";

							break;

//						case "BUILDINGJTADD":
//							//��¥���ɱ����������������һ�ż���˰��ƾ֤
//							dt = BLL.PaymentRule.BuildVoucherDetailTableFromBuildingJT(RelaCode, this.txtProjectCode.Value);
//
//							//���Ϊ��ת��ƾ֤��
//							this.sltVoucherType.Value = "5";
//
//							break;

						default:
							EntityData entity = new  EntityData("VoucherDetail");
							dt = entity.CurrentTable;

							//����ʱ��ȱʡ��ʾ2����ϸ
							for(int i=0;i<2;i++) 
							{
								AddDtl(dt);
							}

							entity.Dispose();
							break;
					}
				}

				//�б���ʾ����
				BLL.PaymentRule.VoucherDetailAddColumnSuplName(dt, projectCode);
				BLL.PaymentRule.VoucherDetailAddColumnCustName(dt);
				BLL.PaymentRule.VoucherDetailAddColumnUFUnitName(dt);
				BLL.PaymentRule.VoucherDetailAddColumnUFProjectName(dt);
				BLL.PaymentRule.VoucherDetailAddColumnSubjectName(dt, this.txtSubjectSetCode.Value);
				BLL.VoucherRule.VoucherDetailAddColumnPaymentCheckPersonName(dt);
				BLL.VoucherRule.VoucherDetailAddColumnPBSName(dt);
				dt.Columns.Add("SubjectHint", typeof(String));

				Session["VoucherDetailTable"] = dt;

				BindDataGrid(dt);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void BindDataGrid(object objTable)
		{
			try 
			{
				if (objTable  == null )
					return;

				DataTable dt = (DataTable)objTable;

				string[] arrField = {"DebitMoney", "CrebitMoney"};
				decimal[] arrSum = BLL.MathRule.SumColumn(dt, arrField);

				this.txtTotalDebitMoney.Value = arrSum[0].ToString("N");
				this.txtTotalCrebitMoney.Value = arrSum[1].ToString("N");

				//			this.dgList.Columns[4].FooterText = debitMoneySum.ToString("N");
				//			this.dgList.Columns[5].FooterText = crebitMoneySum.ToString("N");

				this.dgList.DataSource = dt;
				this.dgList.DataBind();
			}
			catch (Exception ex) 
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
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
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    
			this.dgList.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemCreated);
			this.dgList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemDataBound);

		}
		#endregion

		protected void btnReload_ServerClick(object sender, System.EventArgs e)
		{
			BindDataGrid(Session["VoucherDetailTable"]);
		}

		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			//�跽�������ò�ͬ��ɫ����
			if ((e.Item.ItemType == ListItemType.Item) 
				|| (e.Item.ItemType == ListItemType.AlternatingItem ))
			{
				WebNumericEdit txtCrebitMoney = (WebNumericEdit)e.Item.FindControl("txtCrebitMoney");
				decimal val = txtCrebitMoney.ValueDecimal;

				if (val == 0)
				{
					e.Item.CssClass = "ItemGridTr1";
					e.Item.Attributes["myclass"] = e.Item.CssClass;
				}
				else 
				{
					e.Item.CssClass = "ItemGridTr2";
					e.Item.Attributes["myclass"] = e.Item.CssClass;
				}
			}

			//��ʾ�ϼ�
			if (e.Item.ItemType == ListItemType.Footer) 
			{
				Label lblTotalDebitMoney = (Label)e.Item.FindControl("lblTotalDebitMoney");
				lblTotalDebitMoney.Text = this.txtTotalDebitMoney.Value;

				Label lblTotalCrebitMoney = (Label)e.Item.FindControl("lblTotalCrebitMoney");
				lblTotalCrebitMoney.Text = this.txtTotalCrebitMoney.Value;
			}
		}

		/// <summary>
		/// ����ɾ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnHiddenBatchDelete_ServerClick(object sender, System.EventArgs e)
		{
			DataTable dt = SaveToTempTable(false);
			if (dt == null)
				return;

			string codes = this.txtSelect.Value.Trim();
			string[] arrcode = codes.Split(",".ToCharArray());

			foreach(string code in arrcode) 
			{
				DataRow[] drs = dt.Select("VoucherDetailCode='" + code + "'");
				if (drs.Length > 0) 
				{
					dt.Rows.Remove(drs[0]);
//					drs[0].Delete();
//					drs[0].AcceptChanges();
				}
			}

			Session["VoucherDetailTable"] = dt;
			BindDataGrid(dt);
		}

		/// <summary>
		/// ��Ļ���ݱ��浽session��ʱ��
		/// </summary>
		/// <returns></returns>
		private DataTable SaveToTempTable(bool isBindGrid) 
		{
			try 
			{
				DataTable dt = (DataTable)Session["VoucherDetailTable"];

				//			dt.Rows.Clear();

				foreach (DataGridItem item in this.dgList.Items)
				{
					if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem) 
					{
						TextBox txtSummary = (TextBox)item.FindControl("txtSummary");
						RmsPM.Web.UserControls.InputSubject ucInputSubject = (RmsPM.Web.UserControls.InputSubject)item.FindControl("ucInputSubject");

//						TextBox txtDebitMoney = (TextBox)item.FindControl("txtDebitMoney");
//						TextBox txtCrebitMoney = (TextBox)item.FindControl("txtCrebitMoney");

						WebNumericEdit txtDebitMoney = (WebNumericEdit)item.FindControl("txtDebitMoney");
						WebNumericEdit txtCrebitMoney = (WebNumericEdit)item.FindControl("txtCrebitMoney");

						TextBox txtContractID = (TextBox)item.FindControl("txtContractID");
						HtmlInputHidden txtContractCode = (HtmlInputHidden)item.FindControl("txtContractCode");

						HtmlInputHidden txtSupplyCode = (HtmlInputHidden)item.FindControl("txtSupplyCode");
						HtmlInputHidden txtSupplyName = (HtmlInputHidden)item.FindControl("txtSupplyName");

						HtmlInputHidden txtCustCode = (HtmlInputHidden)item.FindControl("txtCustCode");
						HtmlInputHidden txtCustName = (HtmlInputHidden)item.FindControl("txtCustName");

						RmsPM.Web.UserControls.InputUnit ucUFUnit = (RmsPM.Web.UserControls.InputUnit)item.FindControl("ucUFUnit");
						RmsPM.Web.UserControls.InputUser ucPaymentCheckPerson = (RmsPM.Web.UserControls.InputUser)item.FindControl("ucPaymentCheckPerson");
						RmsPM.Web.UserControls.InputPBS ucPBS = (RmsPM.Web.UserControls.InputPBS)item.FindControl("ucPBS");

						HtmlInputHidden txtUFProjectCode = (HtmlInputHidden)item.FindControl("txtUFProjectCode");
						HtmlInputHidden txtUFProjectName = (HtmlInputHidden)item.FindControl("txtUFProjectName");

						TextBox txtBillNo = (TextBox)item.FindControl("txtBillNo");

						HtmlInputHidden txtVoucherDetailCode = (HtmlInputHidden)item.FindControl("txtVoucherDetailCode");
						string VoucherDetailCode = txtVoucherDetailCode.Value;

						DataRow dr = dt.Select("VoucherDetailCode='" + VoucherDetailCode + "'")[0];

						dr["VoucherDetailCode"] = VoucherDetailCode;
						dr["Summary"] = txtSummary.Text;
						dr["SubjectCode"] = ucInputSubject.Value;

						dr["SubjectName"] = ucInputSubject.Text;
						dr["SubjectHint"] = ucInputSubject.Hint;

						dr["ContractID"] = txtContractID.Text;
						dr["ContractCode"] = txtContractCode.Value;

						dr["SupplyCode"] = txtSupplyCode.Value.Trim();
						dr["SupplyName"] = txtSupplyName.Value;

						dr["CustCode"] = txtCustCode.Value.Trim();
						dr["CustName"] = txtCustName.Value;

						dr["UFUnitCode"] = ucUFUnit.Value;
						dr["UFUnitName"] = ucUFUnit.Text;

						dr["PaymentCheckPerson"] = ucPaymentCheckPerson.Value;
						dr["PaymentCheckPersonName"] = ucPaymentCheckPerson.Text;

						dr["PBSType"] = ucPBS.PBSType;
						dr["PBSCode"] = ucPBS.Value;
						dr["PBSName"] = ucPBS.Text;

						dr["UFProjectCode"] = txtUFProjectCode.Value.Trim();
						dr["UFProjectName"] = txtUFProjectName.Value;

						dr["BillNo"] = txtBillNo.Text;

						dr["DebitMoney"] = txtDebitMoney.ValueDecimal;
						dr["CrebitMoney"] = txtCrebitMoney.ValueDecimal;
					}
				}

				Session["VoucherDetailTable"] = dt;

				if (isBindGrid) 
				{
					BindDataGrid(dt);
				}

				return dt;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		private void dgList_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
			{
				RmsPM.Web.UserControls.InputSubject ucInputSubject = (RmsPM.Web.UserControls.InputSubject)e.Item.FindControl("ucInputSubject");
				ucInputSubject.ProjectCode = this.txtProjectCode.Value;
                ucInputSubject.SubjectSetCode = this.txtSubjectSetCode.Value;

				RmsPM.Web.UserControls.InputPBS ucPBS = (RmsPM.Web.UserControls.InputPBS)e.Item.FindControl("ucPBS");
				ucPBS.ProjectCode = this.txtProjectCode.Value;
			}

//			Button btnCalcSum;
//
//			if (e.Item.ItemType == ListItemType.Footer)
//			{
//				btnCalcSum = (Button)e.Item.FindControl("btnCalcSum");
//				btnCalcSum.Click += new System.EventHandler(this.btnCalcSum_Click);
//			}
		}

		/// <summary>
		/// ��Ч�Լ��
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint, DataTable tbDtl, bool isSubmit) 
		{
			Hint = "";

			if (!this.txtVoucherID.ReadOnly) 
			{
				if (this.txtVoucherID.Text.Trim() == "")
				{
					Hint = "������ƾ֤�� ��";
					return false;
				}

				//���ƾ֤�ű���Ψһ
				if (BLL.PaymentRule.IsVoucherIDExists(this.txtVoucherID.Text, this.txtVoucherCode.Value, this.txtProjectCode.Value))
				{
					Hint = "��ͬ��ƾ֤���Ѵ��� �� ";
					return false;
				}
			}

			if (this.sltVoucherType.Value.Trim() == "")
			{
				Hint = "������ƾ֤���� ��";
				return false;
			}

			if (this.dtbMakeDate.Value.Trim() == "")
			{
				Hint = "�������Ƶ����� ��";
				return false;
			}

			if (this.txtReceiptCount.Value != "" )
			{
				if ( !Rms.Check.StringCheck.IsInt(this.txtReceiptCount.Value))
				{
					Response.Write(Rms.Web.JavaScript.Alert(true,"������������������ ��"));
					return false;
				}
			}

			if (isSubmit) 
			{
				//�����ϸ
				DataTable tbResult = BLL.VoucherRule.CreateVoucherCheckResultTable();
				BLL.VoucherRule.GetVoucherDetailCheckResult(tbResult, tbDtl, txtSubjectSetCode.Value, false);
				if (tbResult.Rows.Count > 0) 
				{
					Hint = BLL.ConvertRule.ToString(tbResult.Rows[0]["desc"]);
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// �ݴ�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				//������ϸ����ʱ��
				DataTable dt = SaveToTempTable(true);
				if (dt == null)
					return;

				//�ݴ滹��ȷ��
				bool isSubmit = (sender != this.btnSave);

				string Hint = "";
				if (!CheckValid(ref Hint, dt, isSubmit)) 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
					return;
				}

				string voucherCode = this.txtVoucherCode.Value;
				bool isNew = (this.txtIsNew.Value.Trim() == "1" );

				EntityData voucher = null;
				DataRow dr = null ;
				//��������
				if ( isNew )
				{
					voucher = new EntityData( "Standard_Voucher" );;
					dr = voucher.GetNewRecord();

//					voucherCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("VoucherCode");
					voucherCode = BLL.PaymentRule.GetNextVoucherCode();
					this.txtVoucherCode.Value = voucherCode;

					if (this.txtVoucherID.ReadOnly)
					{
						this.txtVoucherID.Text = this.txtVoucherCode.Value;
					}

					dr["VoucherCode"] = voucherCode;
					dr["ProjectCode"] = this.txtProjectCode.Value;
                    dr["SubjectSetCode"] = this.txtSubjectSetCode.Value;

					voucher.AddNewRecord(dr);

					this.txtIsNew.Value = "0";
				}
				else
				{
					voucher = DAL.EntityDAO.PaymentDAO.GetStandard_VoucherByCode(voucherCode);
					dr=voucher.CurrentRow;
				}

				dr["VoucherID"] = this.txtVoucherID.Text;
				dr["VoucherType"] = this.sltVoucherType.Value;
				dr["Accountant"] = base.user.UserCode;
				dr["AccountDate"] = DateTime.Now;
				dr["MakeDate"] = BLL.ConvertRule.ToDate(this.dtbMakeDate.Value);
				dr["TotalMoney"] = BLL.MathRule.SumColumn(dt, "DebitMoney");
				dr["ReceiptCount"] = BLL.ConvertRule.ToIntObj(this.txtReceiptCount.Value);

				//�޸ĺ�״̬��ɡ�����
				dr["Status"] = 0;

				// �����ӱ�
				voucher.SetCurrentTable("VoucherDetail");

				//����ʽ��ѭ����ɾ����ʱ���ݱ�û�е���
				int iCount = voucher.CurrentTable.Rows.Count;
				for ( int i=0; i<iCount; i++) 
				{
					DataRow drDtl = voucher.CurrentTable.Rows[i];
					if (dt.Select("VoucherDetailCode = '" + drDtl["VoucherDetailCode"].ToString() + "'").Length <= 0) 
					{
						drDtl.Delete();
					}
				}

				//����ʱ���ݱ��е�ƾ֤��¼ѭ��
				iCount = dt.Rows.Count;
				for ( int i=0; i<iCount; i++)
				{
					DataRow drDtl;
					DataRow drTemp = dt.Rows[i];

					DataRow[] drs = voucher.CurrentTable.Select("VoucherDetailCode = '" + drTemp["VoucherDetailCode"].ToString() + "'");
					if (drs.Length <= 0) 
					{
						//����
						drDtl = voucher.GetNewRecord();
						drDtl["VoucherDetailCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("VoucherDetailCode");
						voucher.AddNewRecord(drDtl);
					}
					else 
					{
						//�޸�
						drDtl = drs[0];
					}

					drDtl["VoucherCode"] = voucherCode;
					drDtl["ProjectCode"] = this.txtProjectCode.Value;
					drDtl["SubjectCode"] = dt.Rows[i]["SubjectCode"];
					drDtl["DebitMoney"] = dt.Rows[i]["DebitMoney"];
					drDtl["CrebitMoney"] = dt.Rows[i]["CrebitMoney"];
					drDtl["Remark"] = dt.Rows[i]["Remark"];
					drDtl["Summary"] = dt.Rows[i]["Summary"];
					drDtl["PaymentType"] = dt.Rows[i]["PaymentType"];
					drDtl["RelaType"] = dt.Rows[i]["RelaType"];
					drDtl["RelaCode"] = dt.Rows[i]["RelaCode"];
					drDtl["ContractCode"] = dt.Rows[i]["ContractCode"];
					drDtl["ContractID"] = dt.Rows[i]["ContractID"];
					drDtl["SupplyCode"] = dt.Rows[i]["SupplyCode"];
					drDtl["CustCode"] = dt.Rows[i]["CustCode"];
					drDtl["UFUnitCode"] = dt.Rows[i]["UFUnitCode"];
					drDtl["PaymentCheckPerson"] = dt.Rows[i]["PaymentCheckPerson"];
					drDtl["PBSType"] = dt.Rows[i]["PBSType"];
					drDtl["PBSCode"] = dt.Rows[i]["PBSCode"];
					drDtl["UFProjectCode"] = dt.Rows[i]["UFProjectCode"];
					drDtl["BillNo"] = dt.Rows[i]["BillNo"];
				}

				DAL.EntityDAO.PaymentDAO.SubmitAllStandard_Voucher(voucher);
				voucher.Dispose();
				
//				// �������paymentCode �����ĸ�� ���paymentΪ�Ѿ����� , ������ ƾ֤���
//				if ( paymentCode != "" )
//				{
//					EntityData payment = DAL.EntityDAO.PaymentDAO.GetPaymentByCode(paymentCode);
//					if ( payment.HasRecord())
//					{
//						payment.CurrentRow["Status"] = 2;
//						payment.CurrentRow["VoucherID"] = this.txtVoucherID.Text;
//						payment.CurrentRow["Accountant"] = this.user.UserCode;
//						payment.CurrentRow["AccountDate"] = DateTime.Now.Date;;
//
//					}
//					DAL.EntityDAO.PaymentDAO.UpdatePayment(payment);
//					payment.Dispose();
//				}

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�������" + ex.Message));
				return;
			}

			GoBack(this.txtVoucherCode.Value, sender, e);
		}

		/// <summary>
		/// �����ˢ�¸�����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GoBack(string voucherCode, object sender, System.EventArgs e) 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);

			switch (this.txtAct.Value.Trim().ToUpper()) 
			{
				case "SALADD":
					if (this.txtRefreshScript.Value.Trim() != "")
					{
						Response.Write(string.Format("window.opener.{0}", this.txtRefreshScript.Value));
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.OpenerReload(false));
					}
					//					Response.Write(@" window.opener.location = window.opener.location;");
					break;

				case "SALJZADD":
					goto case "SALADD";

				case "SALCBADD":
					goto case "SALADD";

				case "SALJTADD":
					goto case "SALADD";

				default:
					Response.Write(Rms.Web.JavaScript.OpenerReload(false));
//					Response.Write(@" window.opener.navigate('VoucherInfo.aspx?VoucherCode="+ voucherCode +"'); ");
					break;
			}

			//�����ر�
			if (sender.Equals(this.btnOK)) 
			{
				Response.Write(Rms.Web.JavaScript.WinClose(false));
			}

			Response.Write(Rms.Web.JavaScript.ScriptEnd);

			if (sender.Equals(this.btnOK)) 
			{
				Response.End();
			}

		}

		/// <summary>
		/// ȷ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnOK_ServerClick(object sender, System.EventArgs e)
		{
			btnSave_ServerClick(sender, e);
		}

		/// <summary>
		/// ����һ����ϸ
		/// </summary>
		/// <param name="dt"></param>
		private void AddDtl(DataTable dt) 
		{
			try 
			{
				DataRow dr = dt.NewRow();

				int sno = BLL.ConvertRule.ToInt(this.txtDetailSno.Value.Trim()) + 1;
				this.txtDetailSno.Value = sno.ToString();

				dr["VoucherDetailCode"] = -sno;
				dt.Rows.Add(dr);
			}
			catch (Exception ex) 
			{
				throw ex;
			}
		}

		/// <summary>
		/// ������ϸ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnAddDtl_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				//��������
				int AddDtlCount = BLL.ConvertRule.ToInt(this.txtAddDtlCount.Value);
				if (AddDtlCount <= 0) 
				{
					AddDtlCount = 1;
					this.txtAddDtlCount.Value = AddDtlCount.ToString();
				}

				DataTable dt = SaveToTempTable(false);
				if (dt == null)
					return;

				for(int i=0;i<AddDtlCount;i++) 
				{
					AddDtl(dt);
				}

				Session["VoucherDetailTable"] = dt;

				//			this.dgList.EditItemIndex = dt.Rows.Count - 1;
				BindDataGrid(dt);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��������" + ex.Message));
			}
		}

		/// <summary>
		/// �����޸�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnBatchEdit_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				SaveToTempTable(true);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�����޸ĳ���" + ex.Message));
				return;
			}

			string s = Rms.Web.JavaScript.ScriptStart
				+ "OpenBatchEdit();"
				+ Rms.Web.JavaScript.ScriptEnd;
			Page.RegisterStartupScript("start", s);
		}
	}
}
