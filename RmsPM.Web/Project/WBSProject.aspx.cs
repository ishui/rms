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
using RmsPM.DAL;
using RmsPM.DAL.EntityDAO;
using RmsPM.Web;
using Rms.Web;
using Rms.ORMap;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// ��Ŀ��Ϣ�������ֽ������뵼��
	/// </summary>
	public partial class WBSProject : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if (!this.IsPostBack)
			{
				InitPage();
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

		private void InitPage()
		{
			ViewState["ProjectCode"] = Request["ProjectCode"].ToString();
			string ProjectCode = (string)ViewState["ProjectCode"];
			EntityData entity = DAL.EntityDAO.ProjectDAO.GetProjectByCode(ProjectCode);

			if (entity.HasRecord())
			{
				DataRow dr = entity.CurrentRow;

				this.lblTitle.Text = entity.GetString("ProjectName");
				this.lblProjectName.Text = entity.GetString("ProjectName");
				this.lblProjectShortName.Text = entity.GetString("ProjectShortName");
				this.LabelCity.Text = entity.GetString("City");
				this.LabelAfforestingRate.Text = BLL.MathRule.GetDecimalShowString(dr["AfforestingRate"]);
				this.LabelArea.Text = entity.GetString("Area");
				this.LabelBlockID.Text = entity.GetString("BlockID");
				this.LabelBlockName.Text = entity.GetString("BlockName");
				this.LabelBuildingDensity.Text = BLL.MathRule.GetDecimalShowString(dr["BuildingDensity"]);
				this.LabelBuildingSpaceForVolumeRate.Text = BLL.MathRule.GetDecimalShowString(dr["BuildingSpaceForVolumeRate"]);
				this.LabelBuildingSpaceNotVolumeRate.Text = BLL.MathRule.GetDecimalShowString(dr["BuildingSpaceNotVolumeRate"]);
				this.LabelPlannedVolumeRate.Text = BLL.MathRule.GetDecimalShowString(dr["PlannedVolumeRate"]);

				this.LabelBuildSpace.Text = BLL.MathRule.GetDecimalShowString(dr["BuildSpace"]);
				this.LabelRemark.Text = entity.GetString("Remark").Replace("\n", "<br>");

				this.LabelTotalBuildingSpace.Text = BLL.MathRule.GetDecimalShowString(dr["TotalBuildingSpace"]);
				this.LabelTotalFloorSpace.Text = BLL.MathRule.GetDecimalShowString(dr["TotalFloorSpace"]);
				this.lblDevelopUnit.Text = entity.GetString("DevelopUnit");
				this.lblJD.Text = entity.GetString("JD");
				this.lblJDXZ.Text = entity.GetString("JDXZ");
				this.lblJDBM.Text = entity.GetString("JDBM");
				this.lblProjectAddress.Text = entity.GetString("ProjectAddress"); 
				this.lblStatus.Text = entity.GetString("Status"); 
				this.LabelSubjectSet.Text = BLL.SubjectRule.GetSubjectSetName(entity.GetString("SubjectSetCode"));

				this.lblkgDate.Text = entity.GetDateTimeOnlyDate("kgDate");
				this.lbljgDate.Text = entity.GetDateTimeOnlyDate("jgDate");
				this.lblSalProjectName.Text = BLL.ProjectRule.GetSalProjectName(entity.GetString("SalProjectCode"));
			}

		}
	}
}
