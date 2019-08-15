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
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using Rms.Web;

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// PaymentCheck ��ժҪ˵����
	/// </summary>
	public partial class PaymentCheck : PageBase
	{

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage()
		{
			try
			{
				this.txtPaymentCode.Value = Request.QueryString["PaymentCode"];
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadData()
		{
			string paymentCode = this.txtPaymentCode.Value;

			try
			{
				if ( paymentCode == "")
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "������"));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
					return;
				}

				EntityData entity = DAL.EntityDAO.PaymentDAO.GetStandard_PaymentByCode(paymentCode);
				if ( entity.HasRecord())
				{
					this.txtProjectCode.Value = entity.GetString("ProjectCode");
					this.txtIsContract.Value = entity.GetInt("IsContract").ToString();
					this.txtContractCode.Value = entity.GetString("ContractCode");
					this.txtStatus.Value = entity.GetInt("Status").ToString();

                    //�ɱ����������˺��Զ����ɸ�� xyq 2018.8.2
                    if (entity.GetString("Payer") == "�ɱ��������")
                    {
                        this.txtAutoCreatePayout.Value = "1";
                        this.trAutoCreatePayout.Visible = true;
                    }
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "��������"));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
					return;
				}

				ShowCheckResult(entity);

				entity.Dispose();
			}
			catch (  Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ���ݳ���" + ex.Message));
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

		}
		#endregion

		private int ShowCheckResult(EntityData entity) 
		{
			int bResult = -1;

			try 
			{
				string PaymentCode = this.txtPaymentCode.Value;
				DataTable tbPayment = entity.Tables["Payment"];
				DataTable tbPaymentItem = entity.Tables["PaymentItem"];

				DataTable tbResult = BLL.PaymentRule.CreatePaymentCheckResultTable();
/* �޸ĺ�ͬ���ü�鹦�ܣ������������Ƿ񳬳����ã���Ϊ���븴�ӣ�ȡ���������������������*/
				if (this.txtContractCode.Value != "") 
				{
                    //�����ϸ���ܳ�����ͬ����������
                    string Hint = BLL.PaymentRule.CheckPaymentCostLimit("", this.txtContractCode.Value, null,ref tbResult);
                    if (Hint.Length > 0)
                    {
                        bResult = -1;
                    }
				} 
                
				if (tbResult.Rows.Count == 0) 
				{
					this.lblResultOk.Visible = true;
					this.trErr.Style["display"] = "none";
					bResult = 1;
				}
				else 
				{
					DataView dvWarn = new DataView(tbResult, "ErrLevel=0","",DataViewRowState.CurrentRows);
					DataView dvErr = new DataView(tbResult, "ErrLevel=1","",DataViewRowState.CurrentRows);

					if (dvErr.Count > 0) 
					{
						this.lblResultErr.Visible = true;
						this.divErr.Style["display"] = "block";
						this.dgList.DataSource = dvErr;
						this.dgList.DataBind();
					}

					if (dvWarn.Count > 0) 
					{
						if (dvErr.Count == 0) 
						{
							bResult = 0;
							this.lblResultWarn.Visible = true;
						}
						this.divWarn.Style["display"] = "block";
						this.dgWarn.DataSource = dvWarn;
						this.dgWarn.DataBind();
					}

				}

				if (bResult >= 0) 
				{
					this.trOpinion.Style["display"] = "";
					this.btnSave.Style["display"] = "";
				}

				// 2005.06.9 unm ��Ȫ����,�û�����������,����������б����ʾ
				this.trOpinion.Style["display"] = "";

			}
			catch (  Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ���ݳ���" + ex.Message));
			}

			return bResult;
		}

		/// <summary>
		/// ���
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				//���ʱ��У��һ��
				EntityData entity = DAL.EntityDAO.PaymentDAO.GetStandard_PaymentByCode(this.txtPaymentCode.Value);
				int bResult = ShowCheckResult(entity);
				entity.Dispose();

				switch (bResult)
				{
					case -1:
						return;
				}

				string PaymentCode = this.txtPaymentCode.Value;
				BLL.PaymentRule.CheckPayment(PaymentCode, this.txtCheckOpinion.Value, base.user.UserCode);
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "���ʧ�ܣ�" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return;
			}

            //�ɱ����������˺��Զ����ɸ�� 
            if (this.txtAutoCreatePayout.Value == "1")
            {
                try
                {
                    BLL.PaymentRule.AutoCreatePayoutFromPayment(this.txtPaymentCode.Value, user.UserCode);
                }
                catch (Exception ex)
                {
                    Response.Write(JavaScript.Alert(true, "�Զ����ɸ��ʧ�ܣ�" + ex.Message));
                    Response.Write(JavaScript.WinClose(true));
                    ApplicationLog.WriteLog(this.ToString(), ex, "");
                }
            }
            
            GoBack();
		}

		/// <summary>
		/// ����
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write("window.opener.location = window.opener.location;");
			Response.Write("if (window.opener.opener) window.opener.opener.location = window.opener.opener.location;");
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

	}
}
