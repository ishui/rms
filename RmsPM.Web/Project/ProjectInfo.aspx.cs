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
using System.Configuration;

using Rms.ORMap;
using RmsPM.DAL;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// ProjectInfo 的摘要说明。
	/// </summary>
	public partial class ProjectInfo : PageBase
	{
		protected System.Web.UI.WebControls.Label lblManager;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage()
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];

				//权限
				this.btnModify.Visible = base.user.HasRight("010103");
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"加载项目信息页面错误。");
				Response.Write(Rms.Web.JavaScript.Alert(true, "加载项目信息页面错误。"));
			}
		}

		private void LoadData()
		{
			try
			{
				EntityData entity = RmsPM.DAL.EntityDAO.ProjectDAO.GetProjectByCode(this.txtProjectCode.Value);

				if ( entity.HasRecord())
				{
					DataRow dr = entity.CurrentRow;

					this.lblProjectName.Text = entity.GetString("ProjectName");
					this.lblProjectShortName.Text = entity.GetString("ProjectShortName");
					this.LabelCity.Text = entity.GetString("City");
					this.LabelArea.Text = entity.GetString("Area");
					this.LabelBlockID.Text = entity.GetString("BlockID");
					this.LabelBlockName.Text = entity.GetString("BlockName");
                    this.lblProjectID.Text = entity.GetString("ProjectID");
                    this.LabelUseShortUserName.Text = entity.GetString("IsUseShortName") == "1" ? "是" : "否";
                    if (!this.user.HasRight("010105"))
                    {
                        this.ShortUserTitle.Visible=false;
                        this.ShortUserValue.Visible = false;
                    }

                    this.LabelBuildingDensity.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["BuildingDensity"]), "%");
                    this.LabelBuildingSpaceForVolumeRate.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["BuildingSpaceForVolumeRate"]), "平米");
                    this.LabelBuildingSpaceNotVolumeRate.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["BuildingSpaceNotVolumeRate"]), "平米");
					
                    this.LabelPlannedVolumeRate.Text = BLL.MathRule.GetDecimalShowString(dr["PlannedVolumeRate"]);

                    this.LabelTotalBuildingSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["TotalBuildingSpace"]), "平米");
                    this.LabelHouseBuildingSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["HouseBuildingSpace"]), "平米");
					//this.LabelBsBuildingSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(dr["BsBuildingSpace"]), "平米");
                    this.LabelUnderBuildingSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["UnderBuildingSpace"]), "平米");

                    this.LabelTotalFloorSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["TotalFloorSpace"]), "平米");
                    this.LabelBuildSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["BuildSpace"]), "平米");
//					this.LabelUnderFloorSpace.Text = BLL.MathRule.GetDecimalShowString(dr["UnderFloorSpace"]);

                    this.LabelAfforestingRate.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["AfforestingRate"]), "%");
                    this.LabelAfforestingSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["AfforestingSpace"]), "平米");
                    //this.LabelCenterAfforestingRate.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["CenterAfforestingRate"]), "%");
                    //this.LabelCenterAfforestingSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["CenterAfforestingSpace"]), "平米");
                    this.LabelwaterSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["waterspace"]), "平米");//水面面积
                    this.Labelperipheryspace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["peripheryspace"]), "平米");//外围面积

                    this.LabelParkingSpace.Text = BLL.MathRule.GetDecimalNoPointShowString(dr["ParkingSpace"]);

                    this.LabelUnderParkingSpace.Text = BLL.MathRule.GetDecimalNoPointShowString(dr["UnderParkingSpace"]);

                    this.LabelHouseCount.Text = BLL.MathRule.GetDecimalNoPointShowString(dr["HouseCount"]);
                    
					this.lblDevelopUnit.Text = entity.GetString("DevelopUnit");
					this.lblDevelopUnitAddress.Text = entity.GetString("DevelopUnitAddress");
					this.lblProjectAddress.Text = entity.GetString("ProjectAddress"); 

					this.LabelSubjectSet.Text = BLL.SubjectRule.GetSubjectSetName(entity.GetString("SubjectSetCode"));

					this.lblStatus.Text = entity.GetString( "Status" );

					this.lblkgDate.Text = entity.GetDateTimeOnlyDate("kgDate");
					this.lbljgDate.Text = entity.GetDateTimeOnlyDate("jgDate");
					this.lblPlanStartDate.Text = entity.GetDateTimeOnlyDate("PlanStartDate");
					this.lblPlanEndDate.Text = entity.GetDateTimeOnlyDate("PlanEndDate");

                    //if (AvailableFunction.isAvailableFunction("0604"))  //有营销系统接口
                    if (BLL.ConvertRule.ToString(Application["SalServiceUrl"]) != "")  //有营销系统接口
                    {
                        try
                        {
                            this.lblSalProjectName.Text = BLL.ProjectRule.GetSalProjectName(entity.GetString("SalProjectCode"));
                        }
                        catch (Exception ex)
                        {
                            ApplicationLog.WriteLog(this.ToString(), ex, "初始化页面错误");
                            Response.Write(Rms.Web.JavaScript.Alert(true, "营销系统接口出错：" + ex.Message));
                        }
                    }
                    else //无营销
                    {
                        this.lblSalProjectName0.Visible = false;
                    }

					this.LabelRemark.Text = entity.GetString("Remark").Replace("\n", "<br>");

					//this.lblHouseUse.Text = entity.GetString("HouseUse");
					this.lblPTFeeType.Text = entity.GetString("PTFeeType");
					this.lblPTFeeVoucherID.Text = entity.GetString("PTFeeVoucherID");

//					this.lblManager.Text = BLL.SystemRule.GetUserName(entity.GetString("Manager"));
					this.ucManager.Value = entity.GetString("Manager");
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "项目不存在"));
				}

				entity.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog ( this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
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


	}
}
