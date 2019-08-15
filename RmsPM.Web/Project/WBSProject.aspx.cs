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
	/// 项目信息、工作分解树导入导出
	/// </summary>
	public partial class WBSProject : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if (!this.IsPostBack)
			{
				InitPage();
			}
		}

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
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
