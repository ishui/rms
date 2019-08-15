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

namespace RmsPM.Web.PBS
{
	/// <summary>
	/// PBSBuildingTree ��ժҪ˵����
	/// </summary>
	public partial class PBSBuildingTree : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAddChild;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnModify;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if (!Page.IsPostBack) 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				((BuildingTree)this.ucBuildingTree).SetParam(this.txtProjectCode.Value, "");

				//Ȩ��
				this.btnAddArea.Visible = base.user.HasRight("010302");
				this.btnAddBuilding.Visible = base.user.HasRight("010302");
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
	}
}
