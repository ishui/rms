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
	/// Building_Step5 的摘要说明。
	/// </summary>
	public partial class Building_Step5 : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTableCell tdIndex2;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdIndex;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdIndex1;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage() 
		{
			try 
			{
				this.txtBuildingCode.Value = Request["BuildingCode"];
				this.txtAct.Value = Request["Action"];

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

				if (BuildingCode == "") 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "无楼栋编号"));
					return;
				}

				//楼栋信息
				EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingByCode(BuildingCode);

				if (entity.HasRecord()) 
				{
					this.lblBuildingName.Text = entity.GetString("BuildingName");
					this.lblPBSTypeName.Text = BLL.PBSRule.GetPBSTypeFullName(entity.GetString("PBSTypeCode"));

					this.txtIsArea.Value = entity.GetInt("IsArea").ToString();
					this.txtProjectCode.Value = entity.GetString("ProjectCode");
					this.txtParentCode.Value = entity.GetString("ParentCode");
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "该楼栋不存在"));
				}

				entity.Dispose();

				DataTable tbDoor = (DataTable)Session["tbDoor"];

				//门牌号
				int DoorCount = tbDoor.Rows.Count;
				this.txtDoorCount.Value = DoorCount.ToString();
				this.rpDoorName.DataSource = tbDoor;
				this.rpDoorName.DataBind();	
				
				DataTable tbFloor = (DataTable)Session["tbFloor"];
				DataTable tbModel = (DataTable)Session["tbModel"];
				DataTable tbRoom = (DataTable)Session["tbRoom"];

				//室号
				this.rpRoomCount.DataSource = tbRoom;
				this.rpRoomCount.DataBind();

				//楼层列表
				int FloorCount = tbFloor.Rows.Count;
				int RoomCount = tbRoom.Rows.Count;

				for (int l=0;l<tbFloor.Rows.Count;l++)
				{					
					DataRow drFloor = tbFloor.Rows[l];
					string FloorCode = drFloor["FloorCode"].ToString();

					string strList="";
					int idI=0;
					for (int m=0;m<RoomCount;m++)
					{
						DataRow drRoom = tbRoom.Rows[m];
						string RoomCode = drRoom["RoomCode"].ToString();

						DataRow drModel = tbModel.Select("FloorCode='" + FloorCode + "' and RoomCode='" + RoomCode + "'")[0];

						idI=idI+1;

						string hidId = "room_model_"+idI+"_"+(l+1);
//							string hidValue = Request.Form[hidId];

						string ModelCode = drModel["ModelCode"].ToString();
						string ModelName = BLL.PBSRule.GetModelName(ModelCode);

						strList=strList+"<td nowrap align=center>"+ModelName;
						strList=strList+"<input  id='"+hidId+"' name='"+hidId+"' value='"+ModelCode+"' type=hidden></td>";
					}
					drFloor["No3"]=strList;
				}
				this.dlBuild1.DataSource = tbFloor;
				this.dlBuild1.DataBind();
				
			}
			catch(Exception ex)
			{
				throw ex;
			}

		}

		private static string formatStr(string code,int num)
		{
			int loopTime=0;
			string resultStr="";
			loopTime=num-code.Length;
			resultStr=code;
			for (int i=1;i<=loopTime;i++)
			{
				resultStr="0"+resultStr;
			}
			return resultStr;

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
		/// 有效性检查
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			return true;
		}

		private void Save(string buildingCode) 
		{
			try
			{
				DataTable tbFloor = (DataTable)Session["tbFloor"];
				DataTable tbRoom = (DataTable)Session["tbRoom"];
				DataTable tbDoor = (DataTable)Session["tbDoor"];
				DataTable tbModel = (DataTable)Session["tbModel"];

				int FloorCount = tbFloor.Rows.Count;
				int RoomCount = tbRoom.Rows.Count;
				int DoorCount = tbDoor.Rows.Count;

				string FloorList = "";
				for (int i=0;i<FloorCount;i++) 
				{
					if (i != 0) 
					{
						FloorList = FloorList + ",";
					}
					FloorList = FloorList + tbFloor.Rows[i]["FloorName"].ToString();
				}

				string RoomList = "";
				for (int i=0;i<RoomCount;i++) 
				{
					if (i != 0) 
					{
						RoomList = RoomList + ",";
					}
					RoomList = RoomList + tbRoom.Rows[i]["RoomName"].ToString();
				}

				string DoorList = "";
				string RoomCountList = "";
				for (int i=0;i<DoorCount;i++) 
				{
					if (i != 0) 
					{
						DoorList = DoorList + ",";
						RoomCountList = RoomCountList + ",";
					}
					DoorList = DoorList + tbDoor.Rows[i]["DoorName"].ToString();
					RoomCountList = RoomCountList + tbDoor.Rows[i]["RoomCount"].ToString();
				}

				//更新楼栋
				EntityData entity = ProductDAO.GetBuildingByCode(buildingCode);
				if (entity.HasRecord()) 
				{
					DataRow dr = entity.CurrentRow;				
				
					dr["FloorList"] = FloorList;
					dr["Room_list"] = RoomList;
				
					ProductDAO.UpdateBuilding(entity);
					entity.Dispose();
				}

				
				string[] arrFloorList = FloorList.Split(",".ToCharArray());
				string[] arrDoorName = DoorList.Split(",".ToCharArray());
				string[] arrRoomCount = RoomCountList.Split(",".ToCharArray());
				string[] arrRoomName = RoomList.Split(",".ToCharArray());

				CreateBuildingAction.SavaChamberAndRoom(DoorCount,this.txtProjectCode.Value,buildingCode,arrFloorList,arrRoomCount,arrDoorName,FloorCount,arrRoomName, tbModel);

			}
			catch (Exception ex) 
			{
				throw ex;
			}
		}

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

				string buildingCode = this.txtBuildingCode.Value;

				DataTable tbFloor = (DataTable)Session["tbFloor"];
				DataTable tbRoom = (DataTable)Session["tbRoom"];
				DataTable tbModel = (DataTable)Session["tbModel"];

				Save(buildingCode);

				Response.Write(JavaScript.ScriptStart);
				Response.Write("window.location.href='../PBS/BuildingPart.aspx?BuildingCode="+buildingCode+"';");
//				Response.Write("window.parent.location.href='../PBS/Building_l.aspx?ProjectCode="+this.txtProjectCode.Value+"';");
//				Response.Write(string.Format("window.location.href='Building_Step5.aspx?BuildingCode={0}';", buildingCode));
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
