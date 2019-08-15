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

namespace RmsPM.Web.PBS
{
	/// <summary>
	/// BuildingFunctionModify ��ժҪ˵����
	/// </summary>
	public partial class BuildingFunctionModify : PageBase
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
				string strBuildingCode = Request.QueryString["BuildingCode"] + "";
				string strBuildingFunctionCode = Request.QueryString["CellCode"] + "";
				string strDoType = Request.QueryString["DoType"] + "";

				this.HideBuildingCode.Value = strBuildingCode;
				this.HideBuildingFunctionCode.Value = strBuildingFunctionCode;

				if ( "SingleModify"==strDoType )
				{
					this.btnToModify.Visible = false;
					this.btnSave.Visible = true;
				}
				else
				{
					this.btnToModify.Visible = true;
					this.btnSave.Visible = false;
				}

				if ( ""==strBuildingFunctionCode )
				{
					this.btnDelete.Visible = false;
				}
				else
				{
					this.btnDelete.Visible = true;
				}

                //Ȩ��
                if (!user.HasRight("010342")) this.btnToModify.Visible = false;
                if (!user.HasRight("010342")) this.btnDelete.Visible = false;
                
                this.UCBuildingFunction1.BuildingCode = strBuildingCode;
				this.UCBuildingFunction1.BuildingFunctionCode = strBuildingFunctionCode;
				this.UCBuildingFunction1.DoType = strDoType;
				this.UCBuildingFunction1.IniControl();

			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


		/// <summary>
		/// װ������
		/// </summary>
		private void LoadData()
		{
			try
			{
				string strBuildingCode = Request.QueryString["BuildingCode"] + "";
				string strBuildingFunctionCode = Request.QueryString["CellCode"] + "";
				string strDoType = Request.QueryString["DoType"] + "";

				this.UCBuildingFunction1.LoadData();
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
				this.UCBuildingFunction1.SaveData();

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


		/// <summary>
		/// ɾ����ť�¼�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				this.UCBuildingFunction1.DeleteData();

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
