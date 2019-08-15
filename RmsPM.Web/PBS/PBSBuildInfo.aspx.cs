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
using Rms.ORMap;
using Rms.Web;

namespace RmsPM.Web.PBS
{
	/// <summary>
	/// PBSBuildInfo ��ժҪ˵����
	/// </summary>
	public partial class PBSBuildInfo : PageBase
	{
		protected System.Web.UI.WebControls.Label lblInvestTyp;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnGotoGraph;
		protected System.Web.UI.WebControls.DataGrid BSdgList;
		protected System.Web.UI.WebControls.DataGrid BFdgList;

		/// <summary>
		/// ¥�������б�ؼ�
		/// </summary>
        protected UCBuildingModelList UCBuildingModelList1;

		/// <summary>
		/// ¥��λ���б�ؼ�
		/// </summary>
        protected UCBuildingStationList UCBuildingStationList1;
		/// <summary>
		/// ¥�������б�ؼ�
		/// </summary>
        protected UCBuildingFunctionList UCBuildingFunctionList1;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
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
				this.txtOpenModal.Value = Request.QueryString["OpenModal"];
				this.txtAct.Value = Request.QueryString["Action"];
				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
				this.txtBuildingCode.Value = Request.QueryString["BuildingCode"];
				string BuildingCode = this.txtBuildingCode.Value.Trim();

				//Ȩ��
				this.btnModify.Visible = user.HasRight("010303");
				this.btnDelete.Visible = user.HasRight("010304");
                this.btnGotoBuildingPart.Visible = user.HasRight("010311"); //¥���ṹ�鿴

				//���´���ʱ��ʾ���رա���������ʾ�����ء�
				switch (this.txtOpenModal.Value.ToLower())
				{
					case "open":
						this.trA1.Style["display"] = "none";
						this.trA2.Style["display"] = "none";
						this.trA3.Style["display"] = "none";
						this.trA4.Style["display"] = "none";
						this.trB1.Style["display"] = "";
						this.trB2.Style["display"] = "";
						break;

					default:
						break;
				}

				//�鿴ģʽʱ�����ɡ��޸ġ�����ɾ����
				switch (this.txtAct.Value.ToLower()) 
				{
					case "view":
						this.trToolBar.Style["display"] = "none";
						break;

					default:
						break;
				}

				if (BuildingCode != "") 
				{
					EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingByCode(BuildingCode);
					if (entity.HasRecord()) 
					{
						DataRow dr = entity.CurrentRow;

						this.txtProjectCode.Value = entity.GetString("ProjectCode");
						this.txtParentCode.Value = entity.GetString("ParentCode");
						this.lblParentName.Text = BLL.ProductRule.GetBuildingName(this.txtParentCode.Value);
						this.txtIsArea.Value = entity.GetInt("IsArea").ToString();

						this.lblBuildingName.Text = entity.GetString("BuildingName");
						//this.lblBuildingShortName.Text = entity.GetString("BuildingShortName");
						this.lblAreaName.Text = entity.GetString("BuildingName");

						lblFloorCount.Text = entity.GetIntString("IFloorCount");
						lblRemark.Text = entity.GetString("Remark").Replace("\n", "<br>");
						lblHouseArea.Text = BLL.StringRule.AddUnit(BLL.StringRule.BuildShowNumberString(entity.GetDecimal("HouseArea"),"#,##0.00"), "ƽ��");
						lblRoomArea.Text = BLL.StringRule.AddUnit(BLL.StringRule.BuildShowNumberString(entity.GetDecimal("RoomArea"),"#,##0.00"), "ƽ��");

						lblDirection.Text = entity.GetString("Direction");
						lblUseType.Text = entity.GetString("UseType");
						lblInvestType.Text = entity.GetString("InvestType");
						lblWhither.Text = entity.GetString("Whither");

//						lblPBSUnitName.Text = BLL.PBSRule.GetPBSUnitName(entity.GetString("PBSUnitCode"));
						lblPBSTypeName.Text = BLL.PBSRule.GetPBSTypeFullName(entity.GetString("PBSTypeCode"));
						txtPBSUnitCode.Value = entity.GetString("PBSUnitCode");

                        //������Ϣ
                        lblDistrictRemark.Text = entity.GetString("Remark").Replace("\n", "<br>");
                        this.LabelBuildingDensity.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["BuildingDensity"]), "%");
                        this.LabelBuildingSpaceForVolumeRate.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["BuildingSpaceForVolumeRate"]), "ƽ��");
                        this.LabelBuildingSpaceNotVolumeRate.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["BuildingSpaceNotVolumeRate"]), "ƽ��");

                        this.LabelPlannedVolumeRate.Text = BLL.MathRule.GetDecimalShowString(dr["PlannedVolumeRate"]);

                        this.LabelTotalBuildingSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["TotalBuildingSpace"]), "ƽ��");
                        this.LabelHouseBuildingSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["HouseBuildingSpace"]), "ƽ��");
                        this.LabelUnderBuildingSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["UnderBuildingSpace"]), "ƽ��");

                        this.LabelTotalFloorSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["TotalFloorSpace"]), "ƽ��");
                        this.LabelBuildSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["BuildSpace"]), "ƽ��");

                        this.LabelAfforestingRate.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["AfforestingRate"]), "%");
                        this.LabelAfforestingSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["AfforestingSpace"]), "ƽ��");
                        this.LabelwaterSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["waterspace"]), "ƽ��");//ˮ�����
                        this.Labelperipheryspace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["peripheryspace"]), "ƽ��");//��Χ���

                        this.LabelParkingSpace.Text = BLL.MathRule.GetDecimalNoPointShowString(dr["ParkingSpace"]);
                        this.LabelUnderParkingSpace.Text = BLL.MathRule.GetDecimalNoPointShowString(dr["UnderParkingSpace"]);
                        this.LabelHouseCount.Text = BLL.MathRule.GetDecimalNoPointShowString(dr["HouseCount"]);

						/*
						//ȡ��λ������Ϣ
						if (txtPBSUnitCode.Value != "") 
						{
							EntityData entityU = DAL.EntityDAO.PBSDAO.GetPBSUnitByCode(entity.GetString("PBSUnitCode"));
							if (entityU.HasRecord()) 
							{
								this.lblPBSUnitName.Text = entityU.GetString("PBSUnitName");
								this.lblPStartDate.Text = entityU.GetDateTimeOnlyDate("PStartDate");
								this.lblPEndDate.Text = entityU.GetDateTimeOnlyDate("PEndDate");
								this.lblStartDate.Text = entityU.GetDateTimeOnlyDate("StartDate");
								this.lblEndDate.Text = entityU.GetDateTimeOnlyDate("EndDate");

								this.lblVisualProgress.Text = BLL.ConstructRule.GetVisualProgressName(entityU.GetString("VisualProgress"));

								this.lblConstructUnit.Text = entityU.GetString("ConstructUnit");
//								this.lblDevelopUnit.Text = entityU.GetString("DevelopUnit");
							}
							entityU.Dispose();
						}
						*/

						/*
						//��ʾ�������
						EntityData entitySubjectSet = DAL.EntityDAO.ProductDAO.GetBuildingSubjectSetByBuilding(BuildingCode);
						this.lblSubjectSetDesc.Text = BLL.FinanceRule.GetFinanceSubjectSetDesc(entitySubjectSet.CurrentTable);
						entitySubjectSet.Dispose();
						*/

						//ȡ��Ŀ��Ϣ
						EntityData entityP = DAL.EntityDAO.ProjectDAO.GetProjectByCode(this.txtProjectCode.Value);
						if (entityP.HasRecord()) 
						{
							this.lblDevelopUnit.Text = entityP.GetString("DevelopUnit");
						}
						entityP.Dispose();
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "¥��������"));
						return;
//						Response.End();
					}
					entity.Dispose();

				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "¥��������"));
				}

				//���¥��������
				BLL.PageFacade.LoadBuildingAndAreaSelect(this.sltBuilding, BuildingCode, this.txtProjectCode.Value);

				if (this.txtIsArea.Value == "1") 
				{
					this.trArea.Style["display"] = "";
					this.trBuildingTree.Style["display"] = "";
					this.trBuilding.Style["display"] = "none";
					this.spanTitle.InnerText = "����";

					((BuildingTree)this.ucBuildingTree).SetParam(this.txtProjectCode.Value, this.txtBuildingCode.Value);
				}

				#region --- �ؼ���ʼ�� ---------------------------------------------------------------------------------

				//*** ͼƬ�� �ؼ� *****************************************************
				this.UCPicGroup1.RootPath = "../";
				this.UCPicGroup1.MasterType = "BuildingPG";
				this.UCPicGroup1.MasterCode = BuildingCode;
				//***********************************************************************

				//*** ¥�������б� �ؼ� *****************************************************
				this.UCBuildingModelList1 = ((UCBuildingModelList)this.UltraWebTab1.Tabs.GetTab(0).FindControl("UCBuildingModelList1"));
				this.UCBuildingModelList1.BuildingCode = BuildingCode;
				this.UCBuildingModelList1.IniControl();
				//***********************************************************************

				//*** ¥��λ���б� �ؼ� *****************************************************
				this.UCBuildingStationList1 = ((UCBuildingStationList)this.UltraWebTab1.Tabs.GetTab(1).FindControl("UCBuildingStationList1"));
				this.UCBuildingStationList1.BuildingCode = BuildingCode;
				this.UCBuildingStationList1.IniControl();
				//***********************************************************************

				//*** ¥�������б� �ؼ� *****************************************************
				this.UCBuildingFunctionList1 = ((UCBuildingFunctionList)this.UltraWebTab1.Tabs.GetTab(2).FindControl("UCBuildingFunctionList1"));
				this.UCBuildingFunctionList1.BuildingCode = BuildingCode;
				this.UCBuildingFunctionList1.IniControl();
				//***********************************************************************

				#endregion ---------------------------------------------------------------------------------
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}


		/// <summary>
		/// װ������
		/// </summary>
		private void LoadData()
		{
			try
			{
				#region --- װ�ؿؼ����� ----------------------------------------------------------------------

                //����
                if (user.HasRight("010321")) //¥�����Ͳ鿴
                {
                    HtmlInputButton btnModelAdd = (HtmlInputButton)this.UltraWebTab1.Tabs.GetTab(0).FindControl("btnModelAdd");
                    btnModelAdd.Visible = user.HasRight("010322"); //¥������ά��
                    this.UCBuildingModelList1.LoadDataList();
                }
                else
                {
                    this.UltraWebTab1.Tabs[0].Visible = false;
                }

                //λ��
                if (user.HasRight("010331")) //¥��λ�ò鿴
                {
                    HtmlInputButton btnStationAdd = (HtmlInputButton)this.UltraWebTab1.Tabs.GetTab(1).FindControl("btnStationAdd");
                    btnStationAdd.Visible = user.HasRight("010332"); //¥��λ��ά��
                    this.UCBuildingStationList1.LoadDataList();
                }
                else
                {
                    this.UltraWebTab1.Tabs[1].Visible = false;
                }

                //����
                if (user.HasRight("010341")) //¥�����ܲ鿴
                {
                    HtmlInputButton btnFunctionAdd = (HtmlInputButton)this.UltraWebTab1.Tabs.GetTab(2).FindControl("btnFunctionAdd");
                    btnFunctionAdd.Visible = user.HasRight("010342"); //¥������ά��
                    this.UCBuildingFunctionList1.LoadDataList();
                }
                else
                {
                    this.UltraWebTab1.Tabs[2].Visible = false;
                }

				#endregion ----------------------------------------------------------------------
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


		/// <summary>
		/// ����
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			string FromUrl = this.txtFromUrl.Value.Trim();
			if (FromUrl != "") 
			{
				Response.Write(string.Format("window.location = '{0}';", FromUrl));
			}
			else 
			{
				//ȱʡ����¥���б�
				Response.Write(string.Format("window.location = '{0}';", "PBSBuildingTree.aspx?ProjectCode=" + this.txtProjectCode.Value));
			}
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
			Response.End();
		}

		/// <summary>
		/// ɾ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				string code = this.txtBuildingCode.Value.Trim();
				BLL.ProductRule.DeleteBuilding(code);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ������" + ex.Message));
				return;
			}

			GoBack();
		}


	}
}
