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
using Rms.Check;
using RmsPM.DAL;
using RmsPM.DAL.QueryStrategy;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// ProjectModify ��ժҪ˵����
	/// </summary>
	public partial class ProjectModify : PageBase
	{
		protected System.Web.UI.WebControls.TextBox TextboxHoueBuildingSpace;
		protected System.Web.UI.WebControls.TextBox TextboxBsBuildingSpace;
	
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
				this.txtFromUrl.Value = Request.QueryString["FromUrl"];

                if ("shimaopm" == this.up_sPMNameLower)
                {
                    this.TextBoxProjectName.Enabled = false;
                    bool IsSystemor = false;
                    
                    EntityData entityRole = DAL.EntityDAO.OBSDAO.GetStationByUserCode(this.user.UserCode);
                    for (int i = 0; i < entityRole.CurrentTable.Rows.Count; i++)
                    {
                        if ("ϵͳ����Ա" == entityRole.CurrentTable.Rows[i]["StationName"].ToString())
                        {
                            this.TextBoxProjectName.Enabled = true;
                            IsSystemor = true;
                            break;
                        }
                    }
                    if (!IsSystemor)
                    {
                        worningChange.InnerText = "��ϵͳ����Ա�����޸���Ŀ����";
                    }
                    entityRole.Dispose();
                }
				//RmsPM.BLL.PageFacade.LoadSubjectSetSelect(this.sltSubjectSet,"");

				RmsPM.BLL.PageFacade.LoadDictionarySelect(this.sltDevelopUnit,"���赥λ","");

//                if (AvailableFunction.isAvailableFunction("0604"))  //��Ӫ��ϵͳ�ӿ�
                if (BLL.ConvertRule.ToString(Application["SalServiceUrl"]) != "")  //��Ӫ��ϵͳ�ӿ�
                {
                    try
                    {
                        RmsPM.BLL.PageFacade.LoadSalSystemProjectSelect(this.sltSalProjectCode, "");
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
                    this.sltSalProjectCode.Visible = false;
                }

				BLL.PageFacade.LoadProjectStatusSelect(this.SelectStatus, "0");

				UnitStrategyBuilder sb = new UnitStrategyBuilder();
				sb.AddStrategy( new Strategy( UnitStrategyName.UnitType,"'��˾','����'" ));
				sb.AddOrder("FullCode",false);
				string sql = sb.BuildMainQueryString();
				QueryAgent qa = new QueryAgent();
				EntityData unit = qa.FillEntityData("Unit",sql);
				qa.Dispose();

                //foreach ( DataRow dr in unit.CurrentTable.Rows)
                //{
                //    this.sltUnit.Items.Add( new ListItem( dr["UnitName"].ToString(),dr["UnitCode"].ToString() ));
                //}
				unit.Dispose();


			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ʼ��ҳ�����");
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

		private void LoadData()
		{
			string projectCode = this.txtProjectCode.Value;


			if ( projectCode == "" ) 
			{
				//����ʱ��ȱʡֵ
				this.sltUnit.Value = "000000";
				return;
			}

			try
			{

				EntityData entity = RmsPM.DAL.EntityDAO.ProjectDAO.GetProjectByCode(projectCode);

				if ( entity.HasRecord())
				{

					DataRow dr = entity.CurrentRow;
					this.TextBoxCity.Text = entity.GetString("City");
					this.TextBoxArea.Text = entity.GetString("Area");
					this.TextBoxBlockID.Text = entity.GetString("BlockID");
					this.TextBoxBlockName.Text = entity.GetString("BlockName");

					this.TextBoxProjectName.Text = entity.GetString("ProjectName");
					this.TextBoxProjectShortName.Text = entity.GetString("ProjectShortName");
					this.TextBoxRemark.Text = entity.GetString("Remark");
                    this.txtProjectID.Text = entity.GetString("ProjectID");
                    this.RadioUseShortUserName.SelectedValue = entity.GetString("IsUseShortName");
                    if (!this.user.HasRight("010105"))
                    {
                        this.ShortUserTitle.Visible = false;
                        this.ShortUserValue.Visible = false;
                    }
                        //this.ShortUserName.InnerHtml = "";
                   

                    this.txtBuildingDensity.Text = BLL.MathRule.GetDecimalNoPointShowString(dr["BuildingDensity"]);
                    this.txtBuildingSpaceForVolumeRate.Value = BLL.MathRule.GetDecimalNoPointShowString(dr["BuildingSpaceForVolumeRate"]);
                    this.txtBuildingSpaceNotVolumeRate.Value = BLL.MathRule.GetDecimalNoPointShowString(dr["BuildingSpaceNotVolumeRate"]);
					this.TextBoxPlannedVolumeRate.Text = BLL.MathRule.GetDecimalShowString(dr["PlannedVolumeRate"]);

                    this.txtTotalBuildingSpace.Value = BLL.MathRule.GetDecimalNoPointShowString(dr["TotalBuildingSpace"]);
                    this.txtHouseBuildingSpace.Value = BLL.MathRule.GetDecimalNoPointShowString(dr["HouseBuildingSpace"]);
                    //this.txtBsBuildingSpace.Value = BLL.MathRule.GetDecimalNoPointShowString(dr["BsBuildingSpace"]);
                    this.txtUnderBuildingSpace.Value = BLL.MathRule.GetDecimalNoPointShowString(dr["UnderBuildingSpace"]);

                    this.txtTotalFloorSpace.Value = BLL.MathRule.GetDecimalNoPointShowString(dr["TotalFloorSpace"]);
                    this.txtBuildSpace.Value = BLL.MathRule.GetDecimalNoPointShowString(dr["BuildSpace"]);
//					this.TextBoxUnderFloorSpace.Text = BLL.MathRule.GetDecimalShowString(dr["UnderFloorSpace"]);

                    this.txtAfforestingRate.Text = BLL.MathRule.GetDecimalNoPointShowString(dr["AfforestingRate"]);
                    this.txtAfforestingSpace.Value = BLL.MathRule.GetDecimalNoPointShowString(dr["AfforestingSpace"]);
                    //this.txtCenterAfforestingRate.Text = BLL.MathRule.GetDecimalNoPointShowString(dr["CenterAfforestingRate"]);
                    //this.txtCenterAfforestingSpace.Value = BLL.MathRule.GetDecimalNoPointShowString(dr["CenterAfforestingSpace"]);
                    this.txtWaterSpace1.Value = BLL.MathRule.GetDecimalNoPointShowString(dr["waterspace"]);//ˮ�����
                    this.txtPeripherySpace1.Value = BLL.MathRule.GetDecimalNoPointShowString(dr["peripheryspace"]);//��Χ���

                    this.txtParkingSpace.Text = BLL.MathRule.GetDecimalNoPointShowString(dr["ParkingSpace"]);
                    this.txtUnderParkingSpace.Text = BLL.MathRule.GetDecimalNoPointShowString(dr["UnderParkingSpace"]);
                    this.txtHouseCount.Text = BLL.MathRule.GetDecimalNoPointShowString(dr["HouseCount"]);

					this.sltDevelopUnit.Value = entity.GetString("DevelopUnit");
					this.ProjectAddress.Text=entity.GetString("ProjectAddress");

					this.kgDate.Value=entity.GetDateTime("kgDate").ToString();
					this.jgDate.Value=entity.GetDateTime("jgDate").ToString();
					this.PlanStartDate.Value=entity.GetDateTime("PlanStartDate").ToString();
					this.PlanEndDate.Value=entity.GetDateTime("PlanEndDate").ToString();

					this.SelectStatus.Value = entity.GetString("Status");

                    if (this.sltSalProjectCode.Visible) //��Ӫ��ϵͳ�ӿ�
                    {
                        this.sltSalProjectCode.Value = entity.GetString("SalProjectCode");
                    }

					//this.txtHouseUse.Text = entity.GetString("HouseUse");
					this.txtPTFeeType.Text = entity.GetString("PTFeeType");
					this.txtPTFeeVoucherID.Text = entity.GetString("PTFeeVoucherID");

					this.txtDevelopUnitAddress.Text = entity.GetString("DevelopUnitAddress");

					// ���빤��������
					this.ucManager.Value = entity.GetString("Manager");
					//					this.LoadUser(projectCode);

					//this.sltSubjectSet.Value = entity.GetString("SubjectSetCode");
					//this.sltUnit.Value = 
					string unitCode = RmsPM.BLL.SystemRule.GetProjectUnitCode(projectCode);
					if (unitCode != "") 
					{
						EntityData entityUnit = RmsPM.DAL.EntityDAO.OBSDAO.GetUnitByCode(unitCode);
						if (entityUnit.HasRecord()) 
						{
							string parentUnitCode = entityUnit.CurrentRow["ParentUnitCode"].ToString();
							this.sltUnit.Value = parentUnitCode;
						}
						entityUnit.Dispose();
					}
				}

				entity.Dispose();
				


			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog ( this.ToString(),ex,"");
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

			if (this.TextBoxProjectName.Text.Trim() == "") 
			{
				Hint = "��������Ŀ���ƣ�";
				return false;
			}

			//��Ŀ���Ʋ����ظ�
			if (BLL.ProjectRule.IsProjectNameExists(this.TextBoxProjectName.Text, this.txtProjectCode.Value))
			{
				Hint = "��ͬ����Ŀ�����Ѵ��� �� ";
				return false;
			}

			if (this.SelectStatus.Value == "") 
			{
				Hint = "��������Ŀ�׶Σ�";
				return false;
			}

			if (this.ucManager.Value == ":") 
			{
				Hint = "��ѡ����Ŀ�ܼ࣡";
				return false;
			}

//			if (this.ucManager.Hint != "") 
//			{
//				Hint = "��Ŀ��������������" + this.ucManager.Hint;
//				return false;
//			}

			if (this.kgDate.Value == "") 
			{
				Hint = "�����뿪�����ڣ�";
				return false;
			}

			if (this.jgDate.Value == "") 
			{
				Hint = "�����뿢�����ڣ�";
				return false;
			}

			//			string parentUnitCode = this.sltUnit.Value;
//			if ( parentUnitCode == "" )
//			{
//				Hint = "��Ŀ�����й���Ĺ�˾";
//				return false;
//			}

			/*
			Hint = CheckNumber(this.TextBoxTotalFloorSpace.Text, "��ռ�����");
			if (Hint != "")		return false;
		
			Hint = CheckNumber(this.TextBoxBuildSpace.Text, "����ռ�����");
			if (Hint != "")		return false;

			Hint = CheckNumber(this.TextBoxTotalBuildingSpace.Text, "�ܽ������");
			if (Hint != "")		return false;

			Hint = CheckNumber(this.TextBoxHouseBuildingSpace.Text, "סլ�������");
			if (Hint != "")		return false;

			Hint = CheckNumber(this.TextBoxBsBuildingSpace.Text, "��ҵ�������");
			if (Hint != "")		return false;

			Hint = CheckNumber(this.TextBoxUnderBuildingSpace.Text, "���½������");
			if (Hint != "")		return false;
			*/

			Hint = CheckNumber(this.TextBoxPlannedVolumeRate.Text, "�ݻ���");
			if (Hint != "")		return false;

			/*
			Hint = CheckNumber(this.TextBoxBuildingDensity.Text, "�����ܶ�");
			if (Hint != "")		return false;

			Hint = CheckNumber(this.TextBoxBuildingSpaceForVolumeRate.Text, "�����۽������");
			if (Hint != "")		return false;

			Hint = CheckNumber(this.TextBoxBuildingSpaceNotVolumeRate.Text, "�������۽������");
			if (Hint != "")		return false;

			Hint = CheckNumber(this.TextBoxAfforestingSpace.Text, "�̵����");
			if (Hint != "")		return false;

			Hint = CheckNumber(this.TextBoxAfforestingRate.Text, "�̻���");
			if (Hint != "")		return false;

			Hint = CheckNumber(this.TextBoxCenterAfforestingSpace.Text, "�����̵����");
			if (Hint != "")		return false;

			Hint = CheckNumber(this.TextBoxCenterAfforestingRate.Text, "�����̻���");
			if (Hint != "")		return false;

			Hint = CheckNumber(this.TextBoxHouseCount.Text, "�ܻ���");
			if (Hint != "")		return false;
			*/

			return true;
		}

		private string CheckNumber(string val, string title) 
		{
			string Hint = "";

			if ( val != "" )
			{
				if ( !StringCheck.IsNumber(val)) 
				{
					Hint = string.Format("{0}������Ч����ֵ��", title);
					return Hint;
				}
			}

			return Hint;
		}

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			string projectCode = this.txtProjectCode.Value;
			bool isNew = ( projectCode == "" );

			string Hint = "";
			if (!CheckValid(ref Hint)) 
			{
				Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
				return;
			}

			try
			{
				string parentUnitCode = this.sltUnit.Value;

				//string subjectSetCode = "";
				string parentFullCode = "";
				int parentDeep = 0;
				EntityData parentUnit = DAL.EntityDAO.OBSDAO.GetUnitByCode(parentUnitCode);
				if (parentUnit.HasRecord()) 
				{
					//subjectSetCode = parentUnit.GetString("SubjectSetCode");
					parentFullCode = parentUnit.GetString("FullCode");
					parentDeep = parentUnit.GetInt("Deep");
				}
				parentUnit.Dispose();

				EntityData entity = null;
				DataRow dr = null;
				if ( isNew )
				{
					entity = new EntityData("Project");
					dr = entity.GetNewRecord();
					projectCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ProjectCode");
					dr["ProjectCode"] = projectCode;
				}
				else
				{
					entity = RmsPM.DAL.EntityDAO.ProjectDAO.GetProjectByCode(projectCode);
					dr = entity.CurrentRow;
				}
                if (this.user.HasRight("010105"))
                    dr["IsUseShortName"] = this.RadioUseShortUserName.SelectedValue;

				dr["City"] = this.TextBoxCity.Text;

				dr["Area"] = this.TextBoxArea.Text;
				dr["BlockID"] = this.TextBoxBlockID.Text;
				dr["BlockName"] = this.TextBoxBlockName.Text;

				//dr["SubjectSetCode"] = subjectSetCode;
				dr["Status"] = this.SelectStatus.Value;
				dr["DevelopUnit"] =this.sltDevelopUnit.Value;
//				dr["JD"] = this.sltJD.Value;
//				dr["JDXZ"] = this.sltJDXZ.Value;
//				dr["JDBM"] = this.txtJDBM.Text;
				dr["ProjectAddress"]=this.ProjectAddress.Text;

                if (this.sltSalProjectCode.Visible) //��Ӫ��ϵͳ�ӿ�
                {
                    dr["SalProjectCode"] = this.sltSalProjectCode.Value;
                }

				dr["kgDate"] = BLL.ConvertRule.ToDate(this.kgDate.Value);
				dr["jgDate"] = BLL.ConvertRule.ToDate(this.jgDate.Value);
				dr["PlanStartDate"] = BLL.ConvertRule.ToDate(this.PlanStartDate.Value);
				dr["PlanEndDate"] = BLL.ConvertRule.ToDate(this.PlanEndDate.Value);
				
				dr["ProjectName"] = this.TextBoxProjectName.Text;
				dr["ProjectShortName"] = this.TextBoxProjectShortName.Text;
				dr["Remark"] = this.TextBoxRemark.Text;
                dr["ProjectID"] = this.txtProjectID.Text;

				dr["BuildingDensity"] = this.txtBuildingDensity.ValueDecimal;
				dr["BuildingSpaceForVolumeRate"] = this.txtBuildingSpaceForVolumeRate.ValueDecimal;
				dr["BuildingSpaceNotVolumeRate"] = this.txtBuildingSpaceNotVolumeRate.ValueDecimal;
				dr["PlannedVolumeRate"] = BLL.ConvertRule.ToDecimal(this.TextBoxPlannedVolumeRate.Text);

				dr["TotalBuildingSpace"] = this.txtTotalBuildingSpace.ValueDecimal;
				dr["HouseBuildingSpace"] = this.txtHouseBuildingSpace.ValueDecimal;
				//dr["BsBuildingSpace"] = this.txtBsBuildingSpace.ValueDecimal;
				dr["UnderBuildingSpace"] = this.txtUnderBuildingSpace.ValueDecimal;

				dr["TotalFloorSpace"] = this.txtTotalFloorSpace.ValueDecimal;
				dr["BuildSpace"] = this.txtBuildSpace.ValueDecimal;
//				dr["UnderFloorSpace"] = BLL.ConvertRule.ToDecimal(this.TextBoxUnderFloorSpace.Text);

				dr["AfforestingRate"] = this.txtAfforestingRate.ValueDecimal;
				dr["AfforestingSpace"] = this.txtAfforestingSpace.ValueDecimal;
				//dr["CenterAfforestingRate"] = this.txtCenterAfforestingRate.ValueDecimal;
				//dr["CenterAfforestingSpace"] = this.txtCenterAfforestingSpace.ValueDecimal;

				dr["ParkingSpace"] = this.txtParkingSpace.ValueDecimal;
				dr["UnderParkingSpace"] = this.txtUnderParkingSpace.ValueDecimal;
				dr["HouseCount"] = this.txtHouseCount.ValueDecimal;
                dr["waterspace"] = this.txtWaterSpace1.ValueDecimal;
                dr["peripheryspace"] = this.txtPeripherySpace1.ValueDecimal;

				//dr["HouseUse"] = this.txtHouseUse.Text;
				dr["PTFeeType"] = this.txtPTFeeType.Text;
				dr["PTFeeVoucherID"] = this.txtPTFeeVoucherID.Text;

				dr["DevelopUnitAddress"] = this.txtDevelopUnitAddress.Text.Trim();

				//��Ŀ������
				dr["Manager"] = this.ucManager.Value;

				if ( isNew )
				{
					entity.AddNewRecord(dr);
					DAL.EntityDAO.ProjectDAO.InsertProject(entity);
				}
				else
				{
					RmsPM.DAL.EntityDAO.ProjectDAO.UpdateProject(entity);
				}
				entity.Dispose();

				//��������ϵͳ��Ŀʱ����֤����ϵͳ��Ŀ�ͱ�ϵͳ��Ŀһһ��Ӧ
				string SalProjectCode = BLL.ConvertRule.ToString(dr["SalProjectCode"]);
				BLL.ProjectRule.UpdateUniSalProjectCode(SalProjectCode, projectCode);

				// ������Ŀ��ʱ����unit�������һ���ڵ�
				bool isNewUnit = true;
				EntityData unit = null;
				DataRow drUnit = null;
				string unitCode = "";
				if ( !isNew )
				{
					unit = DAL.EntityDAO.OBSDAO.GetUnitByProjectCode(projectCode);
					if (unit.HasRecord()) 
					{
						unitCode = unit.GetString("UnitCode");
						drUnit = unit.CurrentRow;
						isNewUnit = false;
					}
				}

				if (isNewUnit) 
				{
					unit = new EntityData("Unit");
					drUnit = unit.GetNewRecord();
					unitCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("UnitCode");
					drUnit["UnitCode"] = unitCode;
					unit.AddNewRecord(drUnit);
					drUnit["UnitType"] = "��Ŀ";
				}

				drUnit["UnitName"] = this.TextBoxProjectName.Text;
				drUnit["ParentUnitCode"] = parentUnitCode;
				drUnit["Deep"] = parentDeep++;
				string fullCode="";
				if ( parentFullCode == "" )
					fullCode = unitCode;
				else
					fullCode = parentFullCode + "-" + unitCode;
				drUnit["FullCode"] = fullCode;

				drUnit["RelaCode"]=projectCode;
				//drUnit["SubjectSetCode"] = subjectSetCode;
				drUnit["SelfAccount"] = 0;
		
				//���沿��
				if ( isNewUnit )
					DAL.EntityDAO.OBSDAO.InsertUnit(unit);
				else
					DAL.EntityDAO.OBSDAO.UpdateUnit(unit);

				unit.Dispose();

				EntityData entity1 = DAL.EntityDAO.WBSDAO.GetTaskByProject(projectCode);
				DataRow[] dr1 = entity1.CurrentTable.Select(" ParentCode='' and flag=1","",System.Data.DataViewRowState.CurrentRows);
				if(dr1.Length>0)
				{
					/*
					// ������Ӹ�����
					this.UpDateTaskMaster(dr1[0]["WBSCode"].ToString());

					// ���Ȩ����Դ					
					this.SaveRS(dr1[0]["WBSCode"].ToString(),this.ucManager.Value,"","070101,070102,070103,070104,070105,070106,070107,070108,070109,070110");// ��ʼӵ�й�����ȫ��Ȩ��
					*/
				}
				else
				{
					// ���湤�����ʼ��������
					EntityData entityTask = DAL.EntityDAO.WBSDAO.GetAllTask();
					DataRow drTask = entityTask.GetNewRecord();
					string strTmp = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("WBS");
					drTask["WBSCode"] = strTmp;
					drTask["TaskName"] = this.TextBoxProjectName.Text;
					drTask["ProjectCode"] = projectCode;
					drTask["ParentCode"] = "";
					drTask["Deep"] = 0;
					drTask["SortID"] = "01";
					drTask["FullCode"] = strTmp;
					drTask["PlannedStartDate"] = this.kgDate.Value;
					drTask["PlannedFinishDate"] = this.jgDate.Value;
					drTask["CompletePercent"] = 0;
					drTask["Status"] = "0";
					drTask["Flag"] = "1";
					entityTask.AddNewRecord(drTask);
					DAL.EntityDAO.WBSDAO.InsertTask(entityTask);			
					entityTask.Dispose();

					/*
					// �˴���Ӹ�����
					this.AddTaskMaster(strTmp);

					// ���Ȩ����Դ					
					this.SaveRS(strTmp,this.ucManager.Value,"","070101,070102,070103,070104,070105,070106,070107,070108,070109,070110");// ��ʼӵ�й�����ȫ��Ȩ��
					*/

					//����ˢ�µ�ǰ�û�Ȩ�� 2005.7.28
					User user = new User(base.user.UserCode);
					Session["User"] = user;
				}

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(JavaScript.Alert(true, "����ʧ�ܣ�" + ex.Message));
				return;
			}

			GoBack();

		}

		/*
		/// <summary>
		/// ���뵱ǰ��Ŀ�ĳ�ʼ���ڵ�
		/// </summary>
		private void LoadUser(string strProjectCode)
		{
			EntityData entity = DAL.EntityDAO.WBSDAO.GetTaskByProject(strProjectCode);
			DataRow[] dr = entity.CurrentTable.Select(" ParentCode='' and flag=1","",System.Data.DataViewRowState.CurrentRows);
			string strWBSCode = "";
			if(dr.Length>0)
				strWBSCode = dr[0]["WBSCode"].ToString();
			if(strWBSCode.Length>0)
			{
				EntityData entityUser = WBSDAO.GetTaskPersonByWBSCode(strWBSCode);
				if (entityUser.HasRecord())
				{
					DataTable dtUserNew = entityUser.CurrentTable.Copy();					
					for (int i = 0; i < dtUserNew.Rows.Count; i++)
					{
						if (dtUserNew.Rows[i]["Type"].ToString() == "2"&&dtUserNew.Rows[i]["RoleType"].ToString() == "0") // ����/��
						{
							//��ucManager����txtUsers  2004.12.27
//							this.txtUsers.Value += (this.txtUsers.Value == "")?"":",";
//							this.txtUsers.Value += dtUserNew.Rows[i]["UserCode"].ToString();
//							this.spanSelectName.InnerText  +=(this.spanSelectName.InnerText == "")?"":",";
//							this.spanSelectName.InnerText += BLL.SystemRule.GetUserName(dtUserNew.Rows[i]["UserCode"].ToString());
						}						
					}
				}
				entityUser.Dispose();
			}
		}
*/

		/*
		/// <summary>
		/// ��ӳ�ʼ��Ŀ�ĸ�����
		/// </summary>
		/// <param name="strWBSCode"></param>
		private void AddTaskMaster(string strWBSCode)
		{
			string strUser = this.ucManager.Value;
			if(strUser.Length>0)
			{
				string[] arUser = strUser.Split(',');
				EntityData entityUser = WBSDAO.GetAllTaskPerson();	
				foreach(string sUser in arUser)
				{
					DataRow drUser = entityUser.GetNewRecord();
					drUser["WBSCode"] = strWBSCode;
					drUser["TaskPersonCode"] = SystemManageDAO.GetNewSysCode("TaskPerson");
					drUser["UserCode"] = sUser;
					drUser["RoleType"] = "0"; // 0������
					drUser["Type"] = "9"; // 9=�����ˣ��ڴ��趨Ĭ��Ϊ������ĸ�����
					drUser["ExecuteCode"] = "";
					entityUser.AddNewRecord(drUser);
					WBSDAO.InsertTaskPerson(entityUser);
				}				
				entityUser.Dispose();
			}
		}
		*/

		/*
		/// <summary>
		/// ������Ŀ�ĸ�����
		/// </summary>
		/// <param name="strWBSCode"></param>
		private void UpDateTaskMaster(string strWBSCode)
		{
			EntityData entityDel = WBSDAO.GetTaskPersonByWBSCode(strWBSCode);
			if(entityDel.HasRecord())
			{
				for(int i=0;i<entityDel.CurrentTable.Rows.Count;i++)
				{
					if(entityDel.CurrentTable.Rows[i]["Type"].ToString()=="9") //9=������
					{
						EntityData entityUser = WBSDAO.GetTaskPersonByCode(entityDel.CurrentTable.Rows[i]["TaskPersonCode"].ToString());
						WBSDAO.DeleteTaskPerson(entityUser);
					}
				}
			}

			string strUser = this.ucManager.Value;
			if(strUser.Length>0)
			{
				string[] arUser = strUser.Split(',');
				EntityData entityUser = WBSDAO.GetAllTaskPerson();	
				foreach(string sUser in arUser)
				{
					DataRow drUser = entityUser.GetNewRecord();
					drUser["WBSCode"] = strWBSCode;
					drUser["TaskPersonCode"] = SystemManageDAO.GetNewSysCode("TaskPerson");
					drUser["UserCode"] = sUser;
					drUser["RoleType"] = "0"; // 0������
					drUser["Type"] = "9"; // 9=�����ˣ��ڴ��趨Ĭ��Ϊ������ĸ�����
					drUser["ExecuteCode"] = "";
					entityUser.AddNewRecord(drUser);
					WBSDAO.InsertTaskPerson(entityUser);
				}				
				entityUser.Dispose();
			}
		}
		*/

		/*
		/// <summary>
		/// ���Ȩ����Դ
		/// </summary>
		private void SaveRS(string strMasterCode,string strUser,string strStation,string strOption)
		{			
			// �����˷�������ʱ�����Լ���Ȩ��
			if(strUser.Length>0&&strUser.IndexOf(base.user.UserCode)<0)
				strUser+=","+base.user.UserCode;

			ArrayList arOperator = new ArrayList();
			if(strUser.Length>0)
			{
				foreach(string strTUser in strUser.Split(','))
				{
					if(strTUser=="") continue;
					AccessRange acRang = new AccessRange();
					acRang.AccessRangeType = 0;
					acRang.RelationCode = strTUser;
					acRang.Operations = strOption;
					arOperator.Add(acRang);
				}
			}			
			if(strStation.Length>0)
			{
				foreach(string strTStation in strStation.Split(','))
				{
					if(strTStation=="") continue;
					AccessRange acRang = new AccessRange();
					acRang.AccessRangeType = 1;
					acRang.RelationCode = strStation;
					acRang.Operations = strOption;
					arOperator.Add(acRang);
				}
			}
			
			if(arOperator.Count>0)
				BLL.ResourceRule.SetResourceAccessRange(strMasterCode,strOption.Substring(0,4),"",arOperator);
		}
		*/

		/// <summary>
		/// ����
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write("window.opener.location = window.opener.location;");
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}
	}
}
