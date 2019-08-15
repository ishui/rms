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
	/// Building_Step1 的摘要说明。
	/// </summary>
	public partial class Building_Step1 : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 删除楼栋
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除出错：" + ex.Message));
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
		/// 返回
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
		/// 有效性检查
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			if (this.sltPBSTypeCode.Value.Trim() == "") 
			{
				Hint = "请输入产品类型";
				return false;
			}

			if (this.txtFloorCount.Value.Trim() == "") 
			{
				Hint = "请输入总层数";
				return false;
			}

			if ( txtFloorCount.Value != "" )
				{
					if ( ! Rms.Check.StringCheck.IsNumber(txtFloorCount.Value))
					{
						Hint = "总层数必须是数值 ！ ";
						return false;
					}
				}
			
			return true;
		}

		/// <summary>
		/// 下一步
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

					//层数
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
				Response.Write(JavaScript.Alert(true, "保存失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}
	}
}
