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
using RmsPM.BLL;
using RmsPM.DAL.EntityDAO;
using RmsPM.Web;
using Rms.ORMap;
using RmsPM.DAL.QueryStrategy;


namespace RmsPM.Web.Material
{
	/// <summary>
	/// MaterialCost ��ժҪ˵����
	/// </summary>
	public partial class MaterialCost : PageBase
	{
		protected RmsPM.WebControls.ToolsBar.ToolsButton ToolsButtonContractEdit;
		protected System.Web.UI.WebControls.DataGrid dgList;
		protected RmsPM.WebControls.ToolsBar.ToolsButton ToolsbuttonImportSupl;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if (!IsPostBack)
			{
				IniPage();
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

		private void IniPage()
		{
            //ֻ��ʾĳ��֦��
            this.txtRootGroupCode.Value = Request.QueryString["RootGroupCode"];
            this.txtRootGroupName.Value = Request.QueryString["RootGroupName"];
            if ((this.txtRootGroupCode.Value == "") && (this.txtRootGroupName.Value != ""))
            {
                this.txtRootGroupCode.Value = BLL.SystemGroupRule.GetSystemGroupCodeByGroupName(this.txtRootGroupName.Value, "1411");
            }
            else if (this.txtRootGroupCode.Value != "")
            {
                this.txtRootGroupName.Value = BLL.SystemGroupRule.GetSystemGroupName(this.txtRootGroupCode.Value);
            }

            if (this.txtRootGroupName.Value != "")
            {
                this.lblTitle.Text = "(" + this.txtRootGroupName.Value + ")";
            }

//			if(!user.HasOperationRight("140102"))
//				this.btnNew.Visible = false;

			ArrayList ar = user.GetClassRight("MaterialCost");
			if ( ! ar.Contains("141102"))
				this.btnNew.Visible = false;

			if(!user.HasOperationRight("141105"))
				this.btnInputMaterialCost.Visible = false;
		}

	}
}
