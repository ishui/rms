using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rms.ORMap;
using Rms.Web;
using RmsPM.DAL;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;

namespace RmsPM.Web.SelectBox
{
	/// <summary>
	/// SelectStationPerson ��ժҪ˵����
	/// </summary>
	public partial class SelectStaionPerson : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				if (!Page.IsPostBack) 
				{
					string ProjectCode = Request["ProjectCode"] +"" ;
					if (ProjectCode != "") 
					{
						this.txtRootUnitCode.Value = "-1";
						this.txtProjectCode.Value = ProjectCode;

						EntityData entity = DAL.EntityDAO.ProjectDAO.GetProjectByCode(ProjectCode);
						if (entity.CurrentTable.Rows.Count > 0) 
						{
							this.txtRootUnitCode.Value = entity.GetString("UnitCode");
						}
						entity.Dispose();
					}
				}
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
