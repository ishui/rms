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
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL;
using Rms.Web;
using RmsPM.BLL;


namespace RmsPM.Web.PBS
{
	/// <summary>
	/// PBSBuildModify ��ժҪ˵����
	/// </summary>
	public partial class PBSBuildModify : PageBase
	{
		protected RmsPM.WebControls.ToolsBar.ToolsButton ToolsButtonCancel;
		protected System.Web.UI.HtmlControls.HtmlTable tdArea;
		protected System.Web.UI.HtmlControls.HtmlTable tdBuilding;
		protected System.Web.UI.WebControls.TextBox txtConstructUnit;
	

		protected void Page_Load(object sender, System.EventArgs e)
		{
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
			try 
			{
				//				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
				this.txtBuildingCode.Value = Request.QueryString["BuildingCode"];
				this.txtParentCode.Value = Request.QueryString["ParentCode"];
				this.txtIsArea.Value = Request.QueryString["IsArea"];
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];

				if (this.txtIsArea.Value == "") 
				{
					this.txtIsArea.Value = "2";
				}

				//����ʱ���봫����Ŀ����
				if ((this.txtBuildingCode.Value == "") && (this.txtProjectCode.Value == ""))
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "����Ŀ���룬��������"));
					Response.End();
				}

				if (this.txtBuildingCode.Value == "") 
				{
					this.btnDelete.Visible = false;
				}

				PageFacade.LoadDictionarySelect(this.sltDirection,"¥������","");
				PageFacade.LoadDictionarySelect(this.sltUseType,"¥��ʹ������","");
				PageFacade.LoadDictionarySelect(this.sltInvestType,"Ͷ������","");
				PageFacade.LoadDictionarySelect(this.sltWhither,"ȥ��","");

				EntityData entity = null;

				if (this.txtBuildingCode.Value != "") 
				{
					entity = ProductDAO.GetBuildingByCode(this.txtBuildingCode.Value);
					this.txtProjectCode.Value = entity.GetString("ProjectCode");
				}

				PageFacade.LoadPBSTypeSelect(this.sltPBSTypeCode, "", this.txtProjectCode.Value);
				PageFacade.LoadPBSAreaSelect(this.sltParentCode, "", this.txtProjectCode.Value);

				if (this.txtBuildingCode.Value == "") 
				{
					//����¥��ȱʡֵ
					this.sltParentCode.Value = this.txtParentCode.Value;
				}

				LoadData(entity);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadData(EntityData entity)
		{
			string BuildingCode = this.txtBuildingCode.Value;

			try
			{
                //����ʱ����ת����������ҳ��
                if ((BuildingCode.Trim() == "") && (this.txtIsArea.Value == "1"))
                {
                    Response.Redirect("PBSDistrictModify.aspx?BuildingCode=" + BuildingCode + "&ProjectCode=" + this.txtProjectCode.Value + "&ParentCode=" + this.txtParentCode.Value);
                }
                
                if (entity == null) 
				{
					entity = ProductDAO.GetBuildingByCode(BuildingCode);
				}

				if(entity.HasRecord())
				{
					DataRow dr = entity.CurrentRow;

					this.txtProjectCode.Value = entity.GetString("ProjectCode");
					this.txtParentCode.Value = entity.GetString("ParentCode");
					this.sltParentCode.Value = this.txtParentCode.Value;

					txtBuildingName.Value = entity.GetString("BuildingName");
					//txtBuildingShortName.Value = entity.GetString("BuildingShortName");
					txtAreaName.Value = entity.GetString("BuildingName");
					txtFloorCount.Value = BLL.MathRule.GetIntShowString(entity.GetInt("IFloorCount"));
					txtRemark.Value = entity.GetString("Remark");
					this.txtHouseArea.Value = BLL.StringRule.BuildShowNumberString(entity.GetDecimal("HouseArea"));

					sltDirection.Value=entity.GetString("Direction");
					sltUseType.Value=entity.GetString("UseType");
					sltInvestType.Value=entity.GetString("InvestType");
					sltWhither.Value=entity.GetString("Whither");

					this.sltPBSTypeCode.Value = entity.GetString("PBSTypeCode");

					this.txtIsArea.Value = entity.GetInt("IsArea").ToString();

                    //����ʱ����ת�������޸�ҳ��
                    if (this.txtIsArea.Value == "1")
                    {
                        Response.Redirect("PBSDistrictModify.aspx?BuildingCode=" + BuildingCode);
                    }

//					string unitProject = entity.GetString("UnitProject");
//					if ( unitProject == "��λ����" )
//						this.chkUnitProject.Checked = true;
				
				}

				entity.Dispose();

				/*
				//��ʾ�������
				EntityData entitySubjectSet = DAL.EntityDAO.ProductDAO.GetBuildingSubjectSetByBuildingCode(BuildingCode);
				this.ucInputSubjectSet.LoadData(entitySubjectSet.CurrentTable); 
				entitySubjectSet.Dispose();
				*/

				if (this.txtIsArea.Value == "1") 
				{
					this.trArea.Style["display"] = "block";
					this.trBuilding.Style["display"] = "none";
					this.spanTitle.InnerText = "����";
				}

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		/// <summary>
		/// ��Ч�Լ��
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			string BuildingName = "";

			if (this.txtIsArea.Value == "1") 
			{
				BuildingName = this.txtAreaName.Value;

				if (this.txtAreaName.Value.Trim() == "") 
				{
					Hint = "��������������";
					return false;
				}
			}
			else 
			{
				BuildingName = this.txtBuildingName.Value;

				if (this.txtBuildingName.Value.Trim() == "") 
				{
					Hint = "������¥������";
					return false;
				}

				if (this.sltPBSTypeCode.Value.Trim() == "") 
				{
					Hint = "�������Ʒ����";
					return false;
				}

				if (this.txtFloorCount.Text.Trim() == "") 
				{
					Hint = "�������ܲ���";
					return false;
				}

				/*
				if ( txtFloorCount.Value != "" )
				{
					if ( ! Rms.Check.StringCheck.IsInt(txtFloorCount.Value))
					{
						Hint = "�ܲ������������� �� ";
						return false;
					}
				}

				if ( this.txtHouseArea.Value != "" )
				{
					if ( ! Rms.Check.StringCheck.IsNumber(txtHouseArea.Value))
					{
						Hint = "�ƻ������������ֵ �� ";
						return false;
					}
				}
				*/

			}

			//¥�����Ʋ����ظ�
            if (BLL.ProductRule.IsBuildingNameExists(BuildingName, this.txtBuildingCode.Value, this.txtProjectCode.Value, this.sltParentCode.Value))
			{
                Hint = "���������Ѵ�����ͬ��¥������ �� ";
				return false;
			}

			return true;
		}

		/// <summary>
		/// ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string Hint = "";
				if (!CheckValid(ref Hint)) 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
					return;
				}

				EntityData entity=null;
				DataRow dr = null;

				string buildingCode = this.txtBuildingCode.Value;

				bool isNew = ( buildingCode == "" );
				bool isNewPBSUnit = false;

				if ( isNew )
				{
					entity = new  EntityData("Building");
					dr=entity.GetNewRecord();
					buildingCode = SystemManageDAO.GetNewSysCode("BuildingCode");
					dr["BuildingCode"] = buildingCode;
					dr["ProjectCode"] = this.txtProjectCode.Value;
					dr["IsArea"] = BLL.ConvertRule.ToInt(this.txtIsArea.Value);
					dr["objectX"]=0;
					dr["objectY"]=0;

				}
				else
				{
					entity = ProductDAO.GetBuildingByCode(buildingCode);
					dr = entity.CurrentRow;
				}

				string OldPBSUnitCode = BLL.ConvertRule.ToString(dr["PBSUnitCode"]);
				int OldFloorCount = BLL.ConvertRule.ToInt(dr["FloorCount"]);

				string parentCode = this.sltParentCode.Value;
				dr["ParentCode"] = parentCode;

				//����
				int layer = 0;
				string fullID = "";
				if (parentCode.Length>0)
				{
					EntityData entityParent = ProductDAO.GetBuildingByCode(parentCode);
					if (entityParent.HasRecord())
					{
						layer = entityParent.GetInt("layer");
						fullID = entityParent.GetString("fullID");
					}
					entityParent.Dispose();
				}

				layer = layer + 1;
				if (fullID == "") 
				{
					fullID = buildingCode;
				}
				else
				{
					fullID = fullID + "-" + buildingCode;
				}

				dr["layer"] = layer;
				dr["FullID"] = fullID;

				if (this.txtIsArea.Value == "1") 
				{
					dr["BuildingName"]=txtAreaName.Value;
				}
				else 
				{
					string OldBuildingName = BLL.ConvertRule.ToString(dr["BuildingName"]);

					dr["BuildingName"]=txtBuildingName.Value;

					//dr["BuildingShortName"]=txtBuildingShortName.Value;

					dr["HouseArea"] = this.txtHouseArea.ValueDecimal;
					//				dr["ToBuildArea"] = 0;
					//				dr["OtherArea"] = 0;

					dr["FloorCount"] = System.Math.Abs(txtFloorCount.ValueDecimal);
					dr["IFloorCount"] = txtFloorCount.ValueDecimal;

					dr["Remark"]=txtRemark.Value;

					dr["Direction"]=sltDirection.Value;
					dr["UseType"]=sltUseType.Value;
					dr["InvestType"]=this.sltInvestType.Value;
					dr["Whither"]=this.sltWhither.Value;

					/*
					if (this.sltPBSUnitCode.Value == "0") 
					{
						//�Զ�������λ����
						isNewPBSUnit = true;
						EntityData entityU = DAL.EntityDAO.PBSDAO.GetPBSUnitByCode("");
						DataRow drU = entityU.CurrentTable.NewRow();

						string PBSUnitCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PBSUnitCode");
						drU["PBSUnitCode"] = PBSUnitCode;
						drU["PBSUnitName"] = BLL.ConvertRule.ToString(dr["BuildingName"]);
						drU["ProjectCode"] = BLL.ConvertRule.ToString(dr["ProjectCode"]);

						//����ʱ���������Ϊ��δ������
						drU["VisualProgress"] = BLL.PBSRule.GetFirstVisualProgress();

						//ȱʡ�ƻ�����������=��Ŀ����������
						EntityData entityP = DAL.EntityDAO.ProjectDAO.GetProjectByCode(BLL.ConvertRule.ToString(drU["ProjectCode"]));
						if (entityP.HasRecord()) 
						{
							DataRow drP = entityP.CurrentRow;
							drU["PStartDate"] = BLL.ConvertRule.ToDate(drP["kgDate"]);
							drU["PEndDate"] = BLL.ConvertRule.ToDate(drP["jgDate"]);
						}
						entityP.Dispose();

						entityU.CurrentTable.Rows.Add(drU);
						DAL.EntityDAO.PBSDAO.InsertPBSUnit(entityU);
						entityU.Dispose();

						dr["PBSUnitCode"] = PBSUnitCode;
					}
					else 
					{
						dr["PBSUnitCode"] = this.sltPBSUnitCode.Value;

						//¥�������޸ĺ��Զ����µ�λ��������
						string PBSUnitCode = BLL.ConvertRule.ToString(dr["PBSUnitCode"]);
						if (PBSUnitCode != "") 
						{
							EntityData entityU = DAL.EntityDAO.PBSDAO.GetPBSUnitByCode(PBSUnitCode);
							if (entityU.HasRecord()) 
							{
								DataRow drU = entityU.CurrentRow;

								if (BLL.ConvertRule.ToString(drU["PBSUnitName"]) == OldBuildingName) 
								{
									drU["PBSUnitName"] = BLL.ConvertRule.ToString(dr["BuildingName"]);

									DAL.EntityDAO.PBSDAO.UpdatePBSUnit(entityU);
								}
							}
							entityU.Dispose();
						}
					}
					*/

					dr["PBSTypeCode"] = this.sltPBSTypeCode.Value;
				}

				if ( isNew )
				{
					entity.AddNewRecord(dr);
					ProductDAO.InsertBuilding(entity);
				}
				else
					ProductDAO.UpdateBuilding(entity);

				entity.Dispose();

				/*
				//����������
				EntityData entitySubjectSet = DAL.EntityDAO.ProductDAO.GetBuildingSubjectSetByBuildingCode(dr["BuildingCode"].ToString());
				this.ucInputSubjectSet.SaveData(entitySubjectSet.CurrentTable, dr["BuildingCode"].ToString());
				DAL.EntityDAO.ProductDAO.SubmitAllBuildingSubjectSet(entitySubjectSet);
				entitySubjectSet.Dispose();
				*/

				if (this.txtIsArea.Value != "1") 
				{
					if (isNewPBSUnit) 
					{
						//����ȱʡ��ȼƻ�
						BLL.ConstructRule.InsertDefaultConstructAnnualPlan(txtProjectCode.Value);
					}

					//����ƻ�Ͷ���Զ�����
					string NewPBSUnitCode = BLL.ConvertRule.ToString(dr["PBSUnitCode"]);
					int NewFloorCount = BLL.ConvertRule.ToInt(dr["FloorCount"]);

					if ((NewPBSUnitCode != OldPBSUnitCode) || (NewFloorCount != OldFloorCount))
					{
						BLL.ConstructRule.UpdateConstructAnnualPlanInvest(NewPBSUnitCode);

						if ((OldPBSUnitCode != NewPBSUnitCode) && (OldPBSUnitCode != "")) 
						{
							BLL.ConstructRule.UpdateConstructAnnualPlanInvest(OldPBSUnitCode);
						}
					}
				}
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "����ʧ�ܣ�" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return;
			}

			GoBack();
		}

		/// <summary>
		/// ɾ��¥��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			string BuildingCode = this.txtBuildingCode.Value;

			if (BuildingCode == "") return;

			try
			{
				BLL.ProductRule.DeleteBuilding(BuildingCode);
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ������" + ex.Message));
				return;
			}

			GoBack();
		}

		/// <summary>
		/// ����
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write("window.opener.location = window.opener.location;");
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			//			string FromUrl = this.txtFromUrl.Value.Trim();
			//			if (FromUrl != "") 
			//			{
			//				Response.Write(string.Format("window.location = '{0}';", FromUrl));
			//			}
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

	}
}
