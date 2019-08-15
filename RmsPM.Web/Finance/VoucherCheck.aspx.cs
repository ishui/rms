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
	/// VoucherCheck ��ժҪ˵����
	/// </summary>
	public partial class VoucherCheck : PageBase
	{
        protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack)
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
			string VoucherCodes = this.txtVoucherCode.Value;
			string VoucherIDs = "";

			try
			{
				if ( VoucherCodes == "")
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "��ƾ֤��"));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
					return;
				}

				string[] arrVoucherCode = VoucherCodes.Split(","[0]);
				foreach(string VoucherCode in arrVoucherCode) 
				{
					EntityData entity = DAL.EntityDAO.PaymentDAO.GetStandard_VoucherByCode(VoucherCode);
					if ( entity.HasRecord())
					{
						this.txtProjectCode.Value = entity.GetString("ProjectCode");

						if (entity.GetInt("Status") != 0)
						{
							Response.Write(Rms.Web.JavaScript.Alert(true, string.Format("ƾ֤{0}��״̬���Ǵ��󣬲������", entity.GetString("VoucherID"))));
							Response.Write(Rms.Web.JavaScript.WinClose(true));
							return;
						}

						if (VoucherIDs != "") VoucherIDs += ",";
						VoucherIDs += entity.GetString("VoucherID");
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, string.Format("ƾ֤{0}������", VoucherCode)));
						Response.Write(Rms.Web.JavaScript.WinClose(true));
						return;
					}

					entity.Dispose();
				}

				this.lblVoucherID.Text = VoucherIDs;

				ShowCheckResult(VoucherCodes);
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
			this.btnSave.ServerClick += new System.EventHandler(this.btnSave_ServerClick);

		}
		#endregion

		private int ShowCheckResult(string VoucherCodes) 
		{
			int bResult = -1;

			try 
			{
				DataTable tbResult = BLL.VoucherRule.CreateVoucherCheckResultTable();
				tbResult.Columns.Add("VouchercODE");
				tbResult.Columns.Add("VoucherID");

				string[] arrVoucherCode = VoucherCodes.Split(","[0]);
				foreach(string VoucherCode in arrVoucherCode) 
				{
					DataTable tbResultTemp = BLL.VoucherRule.GetVoucherCheckResult(VoucherCode);

					foreach(DataRow drResultTemp in tbResultTemp.Rows) 
					{
						DataRow drNew = tbResult.NewRow();

						BLL.ConvertRule.DataRowCopy(drResultTemp, drNew, tbResultTemp, tbResult);

						drNew["VoucherCode"] = VoucherCode;
						drNew["VoucherID"] = BLL.PaymentRule.GetVoucherName(VoucherCode);

						tbResult.Rows.Add(drNew);
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
					this.btnSave.Style["display"] = "";
				}
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
		private void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				//���ʱ��У��һ��
				string VoucherCodes = this.txtVoucherCode.Value;
				int bResult = ShowCheckResult(VoucherCodes);

				switch (bResult)
				{
					case -1:
						return;
				}

				string[] arrVoucherCode = VoucherCodes.Split(","[0]);
				foreach(string VoucherCode in arrVoucherCode) 
				{
					BLL.VoucherRule.CheckVoucher(VoucherCode, base.user.UserCode);
				}
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "���ʧ�ܣ�" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return;
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
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

	}
}
