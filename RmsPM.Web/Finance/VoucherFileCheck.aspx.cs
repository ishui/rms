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
	/// VoucherFileCheck ��ժҪ˵����
	/// </summary>
	public partial class VoucherFileCheck : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlGenericControl ContractNameTemp;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAddFromCost;
	
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
				this.txtVoucherCode.Value = Request.QueryString["VoucherCode"];
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadData()
		{
			string VoucherCode = this.txtVoucherCode.Value;

			try
			{
				if ( VoucherCode == "")
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "��ƾ֤��"));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
					return;
				}

				ShowCheckResult();
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

		private void ShowCheckResult() 
		{
			try 
			{
				string VoucherCode = this.txtVoucherCode.Value;

				EntityData entity = DAL.EntityDAO.PaymentDAO.GetVoucherByCode(VoucherCode);
				if (!entity.HasRecord())
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "ƾ֤������"));
					return;
				}
				this.txtProjectCode.Value = entity.GetString("ProjectCode");
                this.txtSubjectSetCode.Value = entity.GetString("SubjectSetCode");
				entity.Dispose();

				DataTable tbResult = BLL.VoucherRule.CheckVoucherFile(VoucherCode);

				if (tbResult.Rows.Count == 0) 
				{
					Download();
				}
				else 
				{
					DataView dvWarn = new DataView(tbResult, "ErrLevel=0","",DataViewRowState.CurrentRows);
					DataView dvErr = new DataView(tbResult, "ErrLevel=1","",DataViewRowState.CurrentRows);

					if (dvErr.Count > 0) 
					{
						this.lblResultErr.Visible = true;
						this.divErr.Style["display"] = "";
						this.dgList.DataSource = dvErr;
						this.dgList.DataBind();
					}

					if (dvWarn.Count > 0) 
					{
						if (dvErr.Count == 0) 
						{
							this.lblResultWarn.Visible = true;
							this.btnSave.Style["display"] = "";
						}
						this.divWarn.Style["display"] = "";
						this.dgWarn.DataSource = dvWarn;
						this.dgWarn.DataBind();
					}

				}
			}
			catch (  Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ���ݳ���" + ex.Message));
			}
		}

		private void Download() 
		{
			try
			{

				string SaveFileNameHttp = BLL.VoucherRule.MakeVoucherFile(this.txtVoucherCode.Value, Server, this.txtSubjectSetCode.Value);
				if (SaveFileNameHttp != "")
				{
					Response.Write(Rms.Web.JavaScript.WinOpen(true, SaveFileNameHttp,"","","","","",true,true,false,true,true,true,false,false));
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "δ���ɵ����ļ�"));
				}
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "����ʧ�ܣ�" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return;
			}

			GoBack();
		}

		/// <summary>
		/// ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			Download();
		}

		/// <summary>
		/// ����
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write("window.opener.location = window.opener.location;");
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

	}
}
