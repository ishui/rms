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
using Rms.Check;
using RmsPM.BLL;


namespace RmsPM.Web.PBS
{
	/// <summary>
	/// PBSDistrictModify ��ժҪ˵����
	/// </summary>
	public partial class PBSDistrictModify : PageBase
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
				this.txtBuildingCode.Value = Request.QueryString["BuildingCode"];
				this.txtParentCode.Value = Request.QueryString["ParentCode"];
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];

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

				EntityData entity = null;

				if (this.txtBuildingCode.Value != "") 
				{
					entity = ProductDAO.GetBuildingByCode(this.txtBuildingCode.Value);
					this.txtProjectCode.Value = entity.GetString("ProjectCode");
				}

                PageFacade.LoadPBSAreaSelect(this.sltParentCode, "", this.txtProjectCode.Value, entity);

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
					txtBuildingShortName.Value = entity.GetString("BuildingShortName");
					txtRemark.Value = entity.GetString("Remark");

                    this.txtBuildingDensity.Text = BLL.MathRule.GetDecimalNoPointShowString(dr["BuildingDensity"]);
                    this.txtBuildingSpaceForVolumeRate.Value = BLL.MathRule.GetDecimalNoPointShowString(dr["BuildingSpaceForVolumeRate"]);
                    this.txtBuildingSpaceNotVolumeRate.Value = BLL.MathRule.GetDecimalNoPointShowString(dr["BuildingSpaceNotVolumeRate"]);
                    this.TextBoxPlannedVolumeRate.Text = BLL.MathRule.GetDecimalShowString(dr["PlannedVolumeRate"]);

                    this.txtTotalBuildingSpace.Value = BLL.MathRule.GetDecimalNoPointShowString(dr["TotalBuildingSpace"]);
                    this.txtHouseBuildingSpace.Value = BLL.MathRule.GetDecimalNoPointShowString(dr["HouseBuildingSpace"]);
                    this.txtUnderBuildingSpace.Value = BLL.MathRule.GetDecimalNoPointShowString(dr["UnderBuildingSpace"]);

                    this.txtTotalFloorSpace.Value = BLL.MathRule.GetDecimalNoPointShowString(dr["TotalFloorSpace"]);
                    this.txtBuildSpace.Value = BLL.MathRule.GetDecimalNoPointShowString(dr["BuildSpace"]);

                    this.txtAfforestingRate.Text = BLL.MathRule.GetDecimalNoPointShowString(dr["AfforestingRate"]);
                    this.txtAfforestingSpace.Value = BLL.MathRule.GetDecimalNoPointShowString(dr["AfforestingSpace"]);
                    this.txtWaterSpace.Value = BLL.MathRule.GetDecimalNoPointShowString(dr["waterspace"]);//ˮ�����
                    this.txtPeripherySpace1.Value = BLL.MathRule.GetDecimalNoPointShowString(dr["peripheryspace"]);//��Χ���

                    this.txtParkingSpace.Text = BLL.MathRule.GetDecimalNoPointShowString(dr["ParkingSpace"]);
                    this.txtUnderParkingSpace.Text = BLL.MathRule.GetDecimalNoPointShowString(dr["UnderParkingSpace"]);
                    this.txtHouseCount.Text = BLL.MathRule.GetDecimalNoPointShowString(dr["HouseCount"]);
                }

				entity.Dispose();
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

			if (this.txtBuildingName.Value.Trim() == "") 
			{
				Hint = "��������������";
				return false;
			}

            Hint = CheckNumber(this.TextBoxPlannedVolumeRate.Text, "�ݻ���");
            if (Hint != "") return false;

            //���Ʋ����ظ�
            if (BLL.ProductRule.IsBuildingNameExists(txtBuildingName.Value, this.txtBuildingCode.Value, this.txtProjectCode.Value, this.sltParentCode.Value))
			{
                Hint = "���������Ѵ�����ͬ���������� �� ";
				return false;
			}

			return true;
		}

        private string CheckNumber(string val, string title)
        {
            string Hint = "";

            if (val != "")
            {
                if (!StringCheck.IsNumber(val))
                {
                    Hint = string.Format("{0}������Ч����ֵ��", title);
                    return Hint;
                }
            }

            return Hint;
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
					dr["IsArea"] = 1;
					dr["objectX"]=0;
					dr["objectY"]=0;

				}
				else
				{
					entity = ProductDAO.GetBuildingByCode(buildingCode);
					dr = entity.CurrentRow;
				}

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

				string OldBuildingName = BLL.ConvertRule.ToString(dr["BuildingName"]);

				dr["BuildingName"]=txtBuildingName.Value;
				dr["BuildingShortName"]=txtBuildingShortName.Value;

				dr["Remark"]=txtRemark.Value;

                dr["BuildingDensity"] = this.txtBuildingDensity.ValueDecimal;
                dr["BuildingSpaceForVolumeRate"] = this.txtBuildingSpaceForVolumeRate.ValueDecimal;
                dr["BuildingSpaceNotVolumeRate"] = this.txtBuildingSpaceNotVolumeRate.ValueDecimal;
                dr["PlannedVolumeRate"] = BLL.ConvertRule.ToDecimal(this.TextBoxPlannedVolumeRate.Text);

                dr["TotalBuildingSpace"] = this.txtTotalBuildingSpace.ValueDecimal;
                dr["HouseBuildingSpace"] = this.txtHouseBuildingSpace.ValueDecimal;
                dr["UnderBuildingSpace"] = this.txtUnderBuildingSpace.ValueDecimal;

                dr["TotalFloorSpace"] = this.txtTotalFloorSpace.ValueDecimal;
                dr["BuildSpace"] = this.txtBuildSpace.ValueDecimal;

                dr["AfforestingRate"] = this.txtAfforestingRate.ValueDecimal;
                dr["AfforestingSpace"] = this.txtAfforestingSpace.ValueDecimal;

                dr["ParkingSpace"] = this.txtParkingSpace.ValueDecimal;
                dr["UnderParkingSpace"] = this.txtUnderParkingSpace.ValueDecimal;
                dr["HouseCount"] = this.txtHouseCount.ValueDecimal;
                dr["waterspace"] = this.txtWaterSpace.ValueDecimal;
                dr["peripheryspace"] = this.txtPeripherySpace1.ValueDecimal;

                if (isNew)
				{
					entity.AddNewRecord(dr);
					ProductDAO.InsertBuilding(entity);
				}
				else
					ProductDAO.UpdateBuilding(entity);

				entity.Dispose();

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
