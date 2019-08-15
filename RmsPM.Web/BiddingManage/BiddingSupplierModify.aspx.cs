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
	/// BiddingSupplierModify ��ժҪ˵����
	/// </summary>
	public partial class BiddingSupplierModify : PageBase
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
				string strBiddingPrejudicationCode = Request.QueryString["BiddingPrejudicationCode"] + "";
				string strBiddingSupplierCode = Request.QueryString["BiddingSupplierCode"] + "";
				string strDoType = Request.QueryString["DoType"] + "";
				string strState = Request.QueryString["State"] + "";

				this.HideProjectCode.Value = base.project.ProjectCode;
				this.HideBiddingPrejudicationCode.Value = strBiddingPrejudicationCode;
				this.HideBiddingSupplierCode.Value = strBiddingSupplierCode;

				if ( "view"==strState )
				{
					this.btnSave.Visible = false;
					this.btnToModify.Visible = false;
					this.btnDelete.Visible = false;
				}
				else if ( "edit"==strState )
				{
					if ( ""!=strBiddingSupplierCode )
					{
						this.btnDelete.Visible = true;
					}
					else
					{
						this.btnDelete.Visible = false;
					}

					if ( "SingleView"==strDoType )
					{
						this.btnToModify.Visible = true;
					}
					else
					{
						this.btnToModify.Visible = false;
					}

					this.btnSave.Visible = !this.btnToModify.Visible;
				}
				else
				{
					this.btnSave.Visible = false;
					this.btnToModify.Visible = false;
					this.btnDelete.Visible = false;
				}

				//*** �ؼ���ʼ�� *****************************************************
				this.UCBiddingSupplierModify1.BiddingPrejudicationCode = strBiddingPrejudicationCode;
				this.UCBiddingSupplierModify1.BiddingSupplierCode = strBiddingSupplierCode;
				this.UCBiddingSupplierModify1.DoType = strDoType;
				this.UCBiddingSupplierModify1.IniControl();
				//********************************************************
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
				string strBiddingSupplierCode = Request.QueryString["BiddingSupplierCode"] + "";

				if ( ""!=strBiddingSupplierCode )
				{
					this.UCBiddingSupplierModify1.LoadData();
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


		/// <summary>
		/// ���水ť�¼�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string myHint = "";

				if ( this.UCBiddingSupplierModify1.CheckData(out myHint) )
				{
					this.UCBiddingSupplierModify1.SaveData();

					Response.Write( JavaScript.ScriptStart );
					Response.Write( JavaScript.OpenerReload(false) );
					Response.Write( JavaScript.WinClose(false) );
					Response.Write( JavaScript.ScriptEnd );
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


		/// <summary>
		/// ɾ����ť�¼�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string strBiddingSupplierCode = Request.QueryString["BiddingSupplierCode"] + "";

				if ( ""!=strBiddingSupplierCode )
				{
					this.UCBiddingSupplierModify1.DeleteData();
				}
				Response.Write( JavaScript.ScriptStart );
				Response.Write( JavaScript.OpenerReload(false) );
				Response.Write( JavaScript.WinClose(false) );
				Response.Write( JavaScript.ScriptEnd );
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


	}
}
