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
	/// PBSBuildInfo 的摘要说明。
	/// </summary>
	public partial class PBSBuildInfo : PageBase
	{
		protected System.Web.UI.WebControls.Label lblInvestTyp;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnGotoGraph;
		protected System.Web.UI.WebControls.DataGrid BSdgList;
		protected System.Web.UI.WebControls.DataGrid BFdgList;

		/// <summary>
		/// 楼栋户型列表控件
		/// </summary>
        protected UCBuildingModelList UCBuildingModelList1;

		/// <summary>
		/// 楼栋位置列表控件
		/// </summary>
        protected UCBuildingStationList UCBuildingStationList1;
		/// <summary>
		/// 楼栋功能列表控件
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


		/// <summary>
		/// 页面初始化
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

				//权限
				this.btnModify.Visible = user.HasRight("010303");
				this.btnDelete.Visible = user.HasRight("010304");
                this.btnGotoBuildingPart.Visible = user.HasRight("010311"); //楼栋结构查看

				//打开新窗口时显示“关闭”，否则显示“返回”
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

				//查看模式时，不可“修改”、“删除”
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
						lblHouseArea.Text = BLL.StringRule.AddUnit(BLL.StringRule.BuildShowNumberString(entity.GetDecimal("HouseArea"),"#,##0.00"), "平米");
						lblRoomArea.Text = BLL.StringRule.AddUnit(BLL.StringRule.BuildShowNumberString(entity.GetDecimal("RoomArea"),"#,##0.00"), "平米");

						lblDirection.Text = entity.GetString("Direction");
						lblUseType.Text = entity.GetString("UseType");
						lblInvestType.Text = entity.GetString("InvestType");
						lblWhither.Text = entity.GetString("Whither");

//						lblPBSUnitName.Text = BLL.PBSRule.GetPBSUnitName(entity.GetString("PBSUnitCode"));
						lblPBSTypeName.Text = BLL.PBSRule.GetPBSTypeFullName(entity.GetString("PBSTypeCode"));
						txtPBSUnitCode.Value = entity.GetString("PBSUnitCode");

                        //区域信息
                        lblDistrictRemark.Text = entity.GetString("Remark").Replace("\n", "<br>");
                        this.LabelBuildingDensity.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["BuildingDensity"]), "%");
                        this.LabelBuildingSpaceForVolumeRate.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["BuildingSpaceForVolumeRate"]), "平米");
                        this.LabelBuildingSpaceNotVolumeRate.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["BuildingSpaceNotVolumeRate"]), "平米");

                        this.LabelPlannedVolumeRate.Text = BLL.MathRule.GetDecimalShowString(dr["PlannedVolumeRate"]);

                        this.LabelTotalBuildingSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["TotalBuildingSpace"]), "平米");
                        this.LabelHouseBuildingSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["HouseBuildingSpace"]), "平米");
                        this.LabelUnderBuildingSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["UnderBuildingSpace"]), "平米");

                        this.LabelTotalFloorSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["TotalFloorSpace"]), "平米");
                        this.LabelBuildSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["BuildSpace"]), "平米");

                        this.LabelAfforestingRate.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["AfforestingRate"]), "%");
                        this.LabelAfforestingSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["AfforestingSpace"]), "平米");
                        this.LabelwaterSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["waterspace"]), "平米");//水面面积
                        this.Labelperipheryspace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalNoPointShowString(dr["peripheryspace"]), "平米");//外围面积

                        this.LabelParkingSpace.Text = BLL.MathRule.GetDecimalNoPointShowString(dr["ParkingSpace"]);
                        this.LabelUnderParkingSpace.Text = BLL.MathRule.GetDecimalNoPointShowString(dr["UnderParkingSpace"]);
                        this.LabelHouseCount.Text = BLL.MathRule.GetDecimalNoPointShowString(dr["HouseCount"]);

						/*
						//取单位工程信息
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
						//显示财务编码
						EntityData entitySubjectSet = DAL.EntityDAO.ProductDAO.GetBuildingSubjectSetByBuilding(BuildingCode);
						this.lblSubjectSetDesc.Text = BLL.FinanceRule.GetFinanceSubjectSetDesc(entitySubjectSet.CurrentTable);
						entitySubjectSet.Dispose();
						*/

						//取项目信息
						EntityData entityP = DAL.EntityDAO.ProjectDAO.GetProjectByCode(this.txtProjectCode.Value);
						if (entityP.HasRecord()) 
						{
							this.lblDevelopUnit.Text = entityP.GetString("DevelopUnit");
						}
						entityP.Dispose();
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "楼栋不存在"));
						return;
//						Response.End();
					}
					entity.Dispose();

				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "楼栋不存在"));
				}

				//填充楼栋下拉框
				BLL.PageFacade.LoadBuildingAndAreaSelect(this.sltBuilding, BuildingCode, this.txtProjectCode.Value);

				if (this.txtIsArea.Value == "1") 
				{
					this.trArea.Style["display"] = "";
					this.trBuildingTree.Style["display"] = "";
					this.trBuilding.Style["display"] = "none";
					this.spanTitle.InnerText = "区域";

					((BuildingTree)this.ucBuildingTree).SetParam(this.txtProjectCode.Value, this.txtBuildingCode.Value);
				}

				#region --- 控件初始化 ---------------------------------------------------------------------------------

				//*** 图片组 控件 *****************************************************
				this.UCPicGroup1.RootPath = "../";
				this.UCPicGroup1.MasterType = "BuildingPG";
				this.UCPicGroup1.MasterCode = BuildingCode;
				//***********************************************************************

				//*** 楼栋户型列表 控件 *****************************************************
				this.UCBuildingModelList1 = ((UCBuildingModelList)this.UltraWebTab1.Tabs.GetTab(0).FindControl("UCBuildingModelList1"));
				this.UCBuildingModelList1.BuildingCode = BuildingCode;
				this.UCBuildingModelList1.IniControl();
				//***********************************************************************

				//*** 楼栋位置列表 控件 *****************************************************
				this.UCBuildingStationList1 = ((UCBuildingStationList)this.UltraWebTab1.Tabs.GetTab(1).FindControl("UCBuildingStationList1"));
				this.UCBuildingStationList1.BuildingCode = BuildingCode;
				this.UCBuildingStationList1.IniControl();
				//***********************************************************************

				//*** 楼栋功能列表 控件 *****************************************************
				this.UCBuildingFunctionList1 = ((UCBuildingFunctionList)this.UltraWebTab1.Tabs.GetTab(2).FindControl("UCBuildingFunctionList1"));
				this.UCBuildingFunctionList1.BuildingCode = BuildingCode;
				this.UCBuildingFunctionList1.IniControl();
				//***********************************************************************

				#endregion ---------------------------------------------------------------------------------
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}


		/// <summary>
		/// 装载数据
		/// </summary>
		private void LoadData()
		{
			try
			{
				#region --- 装载控件数据 ----------------------------------------------------------------------

                //户型
                if (user.HasRight("010321")) //楼栋户型查看
                {
                    HtmlInputButton btnModelAdd = (HtmlInputButton)this.UltraWebTab1.Tabs.GetTab(0).FindControl("btnModelAdd");
                    btnModelAdd.Visible = user.HasRight("010322"); //楼栋户型维护
                    this.UCBuildingModelList1.LoadDataList();
                }
                else
                {
                    this.UltraWebTab1.Tabs[0].Visible = false;
                }

                //位置
                if (user.HasRight("010331")) //楼栋位置查看
                {
                    HtmlInputButton btnStationAdd = (HtmlInputButton)this.UltraWebTab1.Tabs.GetTab(1).FindControl("btnStationAdd");
                    btnStationAdd.Visible = user.HasRight("010332"); //楼栋位置维护
                    this.UCBuildingStationList1.LoadDataList();
                }
                else
                {
                    this.UltraWebTab1.Tabs[1].Visible = false;
                }

                //功能
                if (user.HasRight("010341")) //楼栋功能查看
                {
                    HtmlInputButton btnFunctionAdd = (HtmlInputButton)this.UltraWebTab1.Tabs.GetTab(2).FindControl("btnFunctionAdd");
                    btnFunctionAdd.Visible = user.HasRight("010342"); //楼栋功能维护
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
		/// 返回
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
				//缺省返回楼栋列表
				Response.Write(string.Format("window.location = '{0}';", "PBSBuildingTree.aspx?ProjectCode=" + this.txtProjectCode.Value));
			}
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
			Response.End();
		}

		/// <summary>
		/// 删除
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除出错：" + ex.Message));
				return;
			}

			GoBack();
		}


	}
}
