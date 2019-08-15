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
	/// ProjectInfo ��ժҪ˵����
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

				//Ȩ��
				this.btnModify.Visible = base.user.HasRight("010103");
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"������Ŀ��Ϣҳ�����");
				Response.Write(Rms.Web.JavaScript.Alert(true, "������Ŀ��Ϣҳ�����"));
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
                    this.LabelUseShortUserName.Text = entity.GetString("IsUseShortName") == "1" ? "��" : "��";
                    if (!this.user.HasRight("010105"))
                    {
                        this.ShortUserTitle.Visible=false;
                        this.ShortUserValue.Visible = false;
                    }

                    this.LabelBuildingDensity.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["BuildingDensity"]), "%");
                    this.LabelBuildingSpaceForVolumeRate.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["BuildingSpaceForVolumeRate"]), "ƽ��");
                    this.LabelBuildingSpaceNotVolumeRate.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["BuildingSpaceNotVolumeRate"]), "ƽ��");
					
                    this.LabelPlannedVolumeRate.Text = BLL.MathRule.GetDecimalShowString(dr["PlannedVolumeRate"]);

                    this.LabelTotalBuildingSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["TotalBuildingSpace"]), "ƽ��");
                    this.LabelHouseBuildingSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["HouseBuildingSpace"]), "ƽ��");
					//this.LabelBsBuildingSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(dr["BsBuildingSpace"]), "ƽ��");
                    this.LabelUnderBuildingSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["UnderBuildingSpace"]), "ƽ��");

                    this.LabelTotalFloorSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["TotalFloorSpace"]), "ƽ��");
                    this.LabelBuildSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["BuildSpace"]), "ƽ��");
//					this.LabelUnderFloorSpace.Text = BLL.MathRule.GetDecimalShowString(dr["UnderFloorSpace"]);

                    this.LabelAfforestingRate.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["AfforestingRate"]), "%");
                    this.LabelAfforestingSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["AfforestingSpace"]), "ƽ��");
                    //this.LabelCenterAfforestingRate.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["CenterAfforestingRate"]), "%");
                    //this.LabelCenterAfforestingSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["CenterAfforestingSpace"]), "ƽ��");
                    this.LabelwaterSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["waterspace"]), "ƽ��");//ˮ�����
                    this.Labelperipheryspace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["peripheryspace"]), "ƽ��");//��Χ���

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

                    //if (AvailableFunction.isAvailableFunction("0604"))  //��Ӫ��ϵͳ�ӿ�
                    if (BLL.ConvertRule.ToString(Application["SalServiceUrl"]) != "")  //��Ӫ��ϵͳ�ӿ�
                    {
                        try
                        {
                            this.lblSalProjectName.Text = BLL.ProjectRule.GetSalProjectName(entity.GetString("SalProjectCode"));
                        }
                        catch (Exception ex)
                        {
                            ApplicationLog.WriteLog(this.ToString(), ex, "��ʼ��ҳ�����");
                            Response.Write(Rms.Web.JavaScript.Alert(true, "Ӫ��ϵͳ�ӿڳ���" + ex.Message));
                        }
                    }
                    else //��Ӫ��
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
					Response.Write(Rms.Web.JavaScript.Alert(true, "��Ŀ������"));
				}

				entity.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog ( this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
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
