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
	/// Building_Step1 ��ժҪ˵����
	/// </summary>
	public partial class Building_Step1 : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			
			if(!IsPostBack)
			{
				IniPage();

				try 
				{
					if (this.txtAct.Value.ToLower() == "del")
					{
						DeleteBuilding(this.txtBuildingCode.Value);
					}
					else 
					{
						LoadData();
					}
				}
				catch (Exception ex)
				{
					ApplicationLog.WriteLog(this.ToString(),ex,"");
					throw ex;
				}
			}
		
		}

		private void IniPage() 
		{
			try 
			{
				this.txtParentCode.Value = Request["ParentCode"];
				this.txtProjectCode.Value = Request["ProjectCode"];
				this.txtBuildingCode.Value = Request["BuildingCode"];
				this.txtAct.Value = Request["Action"];
				this.txtIsArea.Value = Request["IsArea"];

				if (this.txtIsArea.Value == "") 
				{
					this.txtIsArea.Value = "2";
				}

				//				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadData()
		{
			try
			{
				string BuildingCode = this.txtBuildingCode.Value;
				string PBSTypeCode = "";
				string PBSUnitCode = "";

				if (BuildingCode != "") 
				{
					EntityData entity = ProductDAO.GetBuildingByCode(BuildingCode);
					if(entity.HasRecord())
					{
						this.txtIsArea.Value = entity.GetInt("IsArea").ToString();
						this.txtProjectCode.Value = entity.GetString("ProjectCode");

						lblBuildingName.Text = entity.GetString("BuildingName");
						txtFloorCount.Value = entity.GetInt("IFloorCount").ToString();

						PBSTypeCode = entity.GetString("PBSTypeCode");
						PBSUnitCode = entity.GetString("PBSUnitCode");
					}
					entity.Dispose();
				}

				PageFacade.LoadPBSTypeSelect(sltPBSTypeCode,"",this.txtProjectCode.Value);

				sltPBSTypeCode.Value = PBSTypeCode;
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ����" + ex.Message));
			}
		}

		/// <summary>
		/// ɾ��¥��
		/// </summary>
		/// <param name="BuildingCode"></param>
		/// <param name="ProjectCode"></param>
		private void DeleteBuilding(string BuildingCode)
		{
			if (BuildingCode == "") return;

			try
			{
				BLL.ProductRule.DeleteBuilding(BuildingCode);

				GoBack();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ������" + ex.Message));
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

		/// <summary>
		/// ��Ч�Լ��
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			if (this.sltPBSTypeCode.Value.Trim() == "") 
			{
				Hint = "�������Ʒ����";
				return false;
			}

			if (this.txtFloorCount.Value.Trim() == "") 
			{
				Hint = "�������ܲ���";
				return false;
			}

			if ( txtFloorCount.Value != "" )
				{
					if ( ! Rms.Check.StringCheck.IsNumber(txtFloorCount.Value))
					{
						Hint = "�ܲ�����������ֵ �� ";
						return false;
					}
				}
			
			return true;
		}

		/// <summary>
		/// ��һ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSubmit_ServerClick(object sender, System.EventArgs e)
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

					string parentCode = this.txtParentCode.Value;
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
				}
				else
				{
					entity = ProductDAO.GetBuildingByCode(buildingCode);
					dr = entity.CurrentRow;
				}

				dr["IFloorCount"] = BLL.ConvertRule.ToInt(txtFloorCount.Value);
				dr["FloorCount"] = System.Math.Abs(BLL.ConvertRule.ToInt(dr["IFloorCount"]));

				dr["PBSTypeCode"] = sltPBSTypeCode.Value;

				if ( isNew )
				{
					entity.AddNewRecord(dr);
					ProductDAO.InsertBuilding(entity);
				}
				else
					ProductDAO.UpdateBuilding(entity);

				entity.Dispose();

				Response.Write(JavaScript.ScriptStart);
				Response.Write(string.Format("window.location.href='Building_Step2.aspx?BuildingCode={0}';", buildingCode));
				Response.Write(JavaScript.ScriptEnd);
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "����ʧ�ܣ�" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}
	}
}
