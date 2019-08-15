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
using Rms.Web;

namespace RmsPM.Web.BiddingManage
{
	/// <summary>
	/// BiddingSupplierList ��ժҪ˵����
	/// </summary>
	public partial class BiddingSupplierList : PageBase
	{

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !Page.IsPostBack )
			{
				this.IniPage();
				this.LoadData();
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


		/// <summary>
		/// ҳ���ʼ��
		/// </summary>
		private void IniPage()
		{
			try
			{
				this.TabAdd.Visible = false;
				this.btnAdd.Visible = false;
				this.btnModify.Visible = false;
				this.btnSave.Visible = false;

				string strBiddingPrejudicationCode = Request.QueryString["BiddingPrejudicationCode"] + "";
				string strState = Request.QueryString["State"] + "";
				string strSelect = Request.QueryString["Select"] + "";

				if ( ""==strBiddingPrejudicationCode || ""==strState )
				{
					Response.Write( JavaScript.ScriptStart );
					Response.Write( JavaScript.Alert(false,"�Ƿ�����") );
					Response.Write( JavaScript.WinClose(false) );
					Response.Write( JavaScript.ScriptEnd );
					return;
				}

				this.HideBiddingPrejudicationCode.Value = strBiddingPrejudicationCode;

				if ( "view"==strState )
				{
				}
				else if ( "edit"==strState )
				{
					this.btnModify.Visible = true;
					this.TabAdd.Visible = true;
					this.btnAdd.Visible = true;
				}

				if ( "true"==strSelect )
				{
					this.btnSave.Visible = true;
				}

				//*** UCBiddingSupplierModify �ؼ���ʼ�� **************************************************************************
				this.UCBiddingSupplierModify1.BiddingPrejudicationCode = strBiddingPrejudicationCode;
				this.UCBiddingSupplierModify1.BiddingSupplierCode = "";
				this.UCBiddingSupplierModify1.DoType = "SingleModify";
				this.UCBiddingSupplierModify1.IniControl();
				//*****************************************************************************

				//*** UCBiddingSupplierList(�μ��ʸ�Ԥ��ĵ�λ����) �ؼ���ʼ�� **************************************************************************
				this.UCBiddingSupplierList1.BiddingPrejudicationCode = strBiddingPrejudicationCode;
				this.UCBiddingSupplierList1.CanSelect = this.btnSave.Visible;
				this.UCBiddingSupplierList1.CanModify = this.btnModify.Visible;
				this.UCBiddingSupplierList1.IniControl();
				//*****************************************************************************
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


		/// <summary>
		/// װ��ҳ������
		/// </summary>
		private void LoadData()
		{
			try
			{
				this.UCBiddingSupplierList1.LoadData();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


		/// <summary>
		/// ���水ť�¼�(Ԥ��ͨ��)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				this.UCBiddingSupplierList1.BiddingPrejudicationCode = this.HideBiddingPrejudicationCode.Value.Trim();
				this.UCBiddingSupplierList1.SaveData();
				//Response.Write( JavaScript.PageTo(true,"./BiddingSupplierList.aspx?BiddingPrejudicationCode="+this.HideBiddingPrejudicationCode.Value) );
				Response.Write( JavaScript.WinClose(true) );
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


		/// <summary>
		/// �༭��ť�¼�(�޸��б�����)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnModify_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				this.UCBiddingSupplierList1.ModifyData();
				this.UCBiddingSupplierList1.LoadData();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		protected void btnAdd_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string myHint = "";

				if ( this.UCBiddingSupplierModify1.CheckData(out myHint) )
				{
					this.UCBiddingSupplierModify1.SaveData();
					this.UCBiddingSupplierList1.LoadData();
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
