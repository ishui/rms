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

namespace RmsPM.Web.SelectBox
{
	/// <summary>
	/// SelectSystemGroup ��ժҪ˵����
	/// </summary>
	public partial class SelectSystemGroup : PageBase
	{
	
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				string classCode = Request["ClassCode"]+"";
				this.lblClassName.Text = "classCode:" + classCode + " : " +  BLL.SystemRule.GetFunctionStructureName(classCode);

				//���غ�����
				string ReturnFunc = Request.QueryString["ReturnFunc"] + "";
				if (ReturnFunc == "") 
				{
					ReturnFunc = "getReturnSelectGroup";
				}
				ViewState["ReturnFunc"] = ReturnFunc;
                //this.lblClassName.Text = this.lblClassName.Text + " : " + ViewState["ReturnFunc"];

            }
		}
		public string ProjectCode
		{
			get
			{
				return Request["ProjectCode"]+"";
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
