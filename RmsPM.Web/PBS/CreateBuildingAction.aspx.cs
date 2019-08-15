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
	/// CreateBuildingAction 的摘要说明。
	/// </summary>
	public partial class CreateBuildingAction : PageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				string act=""+Request["ACT"];
				string buildingCode=""+Request["BuildingCode"];

				if (act=="building_modify_base")
				{					
					UpdataBase(buildingCode);
				}

				if (act=="building_modify_building")
				{
					Updata2(buildingCode);
				}
				if (act=="building_modify_room_model")
				{
					Updata3(buildingCode);
				}
				if (act=="building_modify_building_dim")
				{
					Updata4(buildingCode);
				}
				if (act=="building_modify_room_dim")
				{
					Updata5(buildingCode);
				}
				if(act=="room_splitX")
				{
					Updata6(""+Request["room_id"]);
				}
				if(act=="room_splitY")
				{
					Updata7(""+Request["room_id"]);
				}
				if(act=="room_uniteXLeft")
				{
					Updata8Left(""+Request["room_id"]);
				}
				if(act=="room_uniteX")
				{
					Updata8(""+Request["room_id"]);
				}
				if(act=="room_uniteY")
				{
					Updata9(""+Request["room_id"]);
				}
				if(act=="room_uniteYUp")
				{
					Updata9Up(""+Request["room_id"]);
				}
				if(act=="room_del")
				{
					Updata10(""+Request["room_id"]);
				}
				if(act=="room_new")
				{
					Updata11(""+Request["room_id"]);
				}
				
			}
		}

		private void UpdateSigleBuilding(string projectCode,string parentCode,string buildingName,int floorCount,string buildType,string curCode,string floorList,string roomlist,string unitProject)
		{
			try
			{
				
			
				EntityData entity=ProductDAO.GetBuildingByCode(curCode);
				DataRow dr=entity.CurrentRow;				
				
				dr["ProjectCode"]=projectCode;
				dr["FloorCount"]=floorCount;
				dr["FloorList"]=floorList;
				dr["Room_list"]=roomlist;
				dr["BuildingName"]=buildingName;	
				dr["BuildType"]=buildType;
				dr["UnitProject"]=unitProject;
				
				
				ProductDAO.UpdateBuilding(entity);
				entity.Dispose();
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		private void SaveBuilding(string projectCode,string parentCode,string buildingName,int floorCount,string buildType,string curCode,string floorList,string roomlist,string unitProject)
		{
			try
			{
				string fullID="";
				int deep=0;
				if (parentCode.Length>0)
				{
					EntityData entity1=ProductDAO.GetBuildingByCode(parentCode);
					if (entity1.HasRecord())
					{
						fullID=entity1.GetString("FullID");
						deep=entity1.GetInt("Layer");
					}
					entity1.Dispose();
				}
			
				EntityData entity=ProductDAO.GetBuildingByCode("");
				DataRow dr=entity.GetNewRecord();
				
				dr["BuildingCode"]=curCode;
				dr["ProjectCode"]=projectCode;
				dr["FloorCount"]=floorCount;
				dr["FloorList"]=floorList;
				dr["Room_list"]=roomlist;
				dr["BuildingName"]=buildingName;	
				dr["BuildType"]=buildType;
				dr["IsArea"]=2;
				dr["Layer"]=deep+1;
				dr["ParentCode"]=parentCode;
				dr["objectX"]=0;
				dr["objectY"]=0;
				dr["UnitProject"]=unitProject;
				if (parentCode.Length>0)
				{
					dr["FullID"]=fullID+"-"+curCode;
				}
				else
				{
					dr["FullID"]=curCode;
				}
				entity.AddNewRecord(dr);
				ProductDAO.InsertBuilding(entity);
				entity.Dispose();
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		public static void SavaChamberAndRoom(int doorCount,string projectCode,string buildingCode,string[] arrFlooName,string[] arrRoomCount,string [] arrRoomList,int floorCount,string[] arrRoomName, DataTable tbModel)
		{
			try
			{
				EntityData entity=DAL.EntityDAO.ProductDAO.GetChamberByCode("");
				DataRow dr=null;
				string curChamberCode="";
				string chamberCodes="";
				for (int i=1;i<=doorCount;i++)
				{
					dr=entity.GetNewRecord();
					curChamberCode=SystemManageDAO.GetNewSysCode("ChamberCode").ToString();
					dr["ChamberCode"]=curChamberCode;
					dr["ProjectCode"]=projectCode;
					dr["BuildingCode"]=buildingCode;
					dr["ChamberName"]=arrRoomList[i-1];
					dr["RoomCount"]=int.Parse(arrRoomCount[i-1].Trim());
					dr["RoomList"]=arrRoomList[i-1];
					entity.AddNewRecord(dr);
					chamberCodes=chamberCodes+curChamberCode+",";

				}
				ProductDAO.InsertChamber(entity);
				entity.Dispose();		
	
				string[] arrChamberCode=chamberCodes.Split(",".ToCharArray());
				int q=0;
				for(int i=0;i<=arrRoomCount.Length-1;i++)
				{
					q=q+int.Parse(arrRoomCount[i].Trim());
				}
				
				//户型
				EntityData entityModel = DAL.EntityDAO.ProductDAO.GetModelByProjectCode(projectCode);

				EntityData entity2=DAL.EntityDAO.ProductDAO.GetRoomByCode("");
				DataRow dr2=null;
				string curRoomCode="";
				for (int j=1;j<=floorCount;j++)
				{			
					for(int k=1;k<=q;k++)
					{
						string chamberCodeOfRoom="";
						int n=0;
						for(int l=0;l<=arrChamberCode.Length-1;l++)
						{
							if (arrChamberCode[l].Length>0)
							{
								for(int m=1;m<=int.Parse(arrRoomCount[l]);m++)
								{
									n=n+1;
									if (n==k)
									{
										chamberCodeOfRoom=arrChamberCode[l];
									}
								}
							}
						}
						
						string roomName=arrFlooName[j-1]+formatStr(arrRoomName[k-1].Trim(),2);

						DataRow drModel = tbModel.Select("FloorCode='" + j + "' and RoomCode='" + k + "'")[0];
						string ModelCode = BLL.ConvertRule.ToString(drModel["ModelCode"]);

						curRoomCode=SystemManageDAO.GetNewSysCode("RoomCode").ToString();
						dr2=entity2.GetNewRecord();
						
						dr2["RoomCode"]=curRoomCode;
						dr2["ProjectCode"]=projectCode;
						dr2["BuildingCode"]=buildingCode;
						dr2["ChamberCode"]=chamberCodeOfRoom;
						dr2["ModelCode"]=ModelCode;
						dr2["RoomName"]=roomName;
						dr2["FloorIndex"]=floorCount-j+1;
						dr2["FloorName"] = arrFlooName[j-1];
						dr2["RoomIndex"]=k;
						dr2["BuildArea"]=0;
						dr2["RoomArea"]=0;
						dr2["RowSpan"]=1;
						dr2["CellSpan"]=1;

						//预测面积从户型带入
						if (ModelCode != "") 
						{
							DataRow[] drsModel = entityModel.CurrentTable.Select("ModelCode='" + ModelCode + "'");
							if (drsModel.Length > 0) 
							{
								dr2["PreBuildArea"] = drsModel[0]["BuildArea"];
								dr2["PreRoomArea"] = drsModel[0]["RoomArea"];
							}
						}

						entity2.AddNewRecord(dr2);

					}
					ProductDAO.InsertRoom(entity2);
				}

				entityModel.Dispose();
				entity.Dispose();
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

		/// <summary>
		/// 修改室号
		/// </summary>
		/// <param name="buildingCode"></param>
		private void UpdataBase(string buildingCode)
		{
			try
			{
				EntityData entity=ProductDAO.GetBuildingByCode(buildingCode);
				if(entity.HasRecord())
				{
					DataRow dr=entity.CurrentRow;
					dr["FloorList"]=Request.Form["floor_list"];
					dr["Room_list"]=Request.Form["room_list"];

					ProductDAO.UpdateBuilding(entity);
				}
				entity.Dispose();

				//修改门牌号
				string door_name=""+Request["door_name"];
//门牌号清空时也要可以保存 2006.7.14
//				if (door_name.Length>0)
//				{
					string[] arrDoorName = door_name.Split(",".ToCharArray());
					EntityData entity1=ProductDAO.getChamberByBuildingCode(buildingCode);
					if(entity1.HasRecord())
					{
						DataRow dr1=null;
						for(int i=0;i<=entity1.CurrentTable.Rows.Count-1;i++)
						{
							dr1=entity1.CurrentTable.Rows[i];
							dr1["ChamberName"]=arrDoorName[i];
						}
						ProductDAO.UpdateChamber(entity1);
					}
					entity1.Dispose();
//				}
				
				string[] arrFloorList=Request.Form["floor_list"].Split(",".ToCharArray());
				string[] arrRoomList=Request.Form["room_list"].Split(",".ToCharArray());

				for (int j=1;j<=arrFloorList.Length;j++)
				{
					string FloorName = arrFloorList[arrFloorList.Length - j];

					for(int k=1;k<=arrRoomList.Length;k++)
					{
						EntityData entity2=ProductDAO.GetRoomByBuildingCodeAndPos(buildingCode,k,j);
						if(entity2.HasRecord())
						{
							DataRow dr2 = entity2.CurrentRow;
							dr2["RoomName"] = Request.Form["room_name_"+j+"_"+k];
							dr2["FloorName"] = FloorName;
							ProductDAO.UpdateRoom(entity2);
						}
						entity2.Dispose();
							
					}
				}
				
				string s = JavaScript.ScriptStart
					+ "GotoBuildingPart('"+buildingCode+"');"
					+ JavaScript.ScriptEnd;
				Page.RegisterStartupScript("goto", s);

//				Response.Write(JavaScript.ScriptStart);
//				Response.Write("window.location.href='../PBS/BuildingStructureModify.aspx?action=base&BuildingCode="+buildingCode+"';");
//				Response.Write("window.close();");
//				Response.Write(JavaScript.ScriptEnd);

			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		private void Updata2(string buildingCode)
		{
			try
			{
				EntityData entity=ProductDAO.GetBuildingByCode(buildingCode);
				if(entity.HasRecord())
				{
				}

			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// 修改户型
		/// </summary>
		/// <param name="buildingCode"></param>
		private void Updata3(string buildingCode)
		{
			try
			{
				string floorList="";
				string roomList="";
				EntityData entity=ProductDAO.GetBuildingByCode(buildingCode);
				if(entity.HasRecord())
				{
					floorList=entity.GetString("FloorList");
					roomList=entity.GetString("Room_list");
				}
				entity.Dispose();

				string[] arrFloorList=floorList.Split(",".ToCharArray());
				string[] arrRoomList=roomList.Split(",".ToCharArray());

				for (int j=1;j<=arrFloorList.Length;j++)
				{
					for(int k=1;k<=arrRoomList.Length;k++)
					{
						EntityData entity2=ProductDAO.GetRoomByBuildingCodeAndPos(buildingCode,k,j);
						if(entity2.HasRecord())
						{
							DataRow dr2=entity2.CurrentRow;
							dr2["ModelCode"]=Request.Form["room_model_"+j+"_"+k];
							ProductDAO.UpdateRoom(entity2);
						}
						entity2.Dispose();
							
					}
				}

				string s = JavaScript.ScriptStart
					+ "GotoBuildingPart('"+buildingCode+"');"
					+ JavaScript.ScriptEnd;
				Page.RegisterStartupScript("goto", s);

//				Response.Write(JavaScript.ScriptStart);
//				Response.Write("window.location.href='../PBS/BuildingStructureModify.aspx?action=room_model&BuildingCode="+buildingCode+"';");
//				Response.Write("window.close();");
//				Response.Write(JavaScript.ScriptEnd);

			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		private void Updata4(string buildingCode)
		{
			try
			{
				string floorList="";
				string roomList="";
				EntityData entity=ProductDAO.GetBuildingByCode(buildingCode);
				if(entity.HasRecord())
				{
					floorList=entity.GetString("FloorList");
					roomList=entity.GetString("Room_list");
				}
				entity.Dispose();

				string[] arrFloorList=floorList.Split(",".ToCharArray());
				string[] arrRoomList=roomList.Split(",".ToCharArray());

				for (int j=1;j<=arrFloorList.Length;j++)
				{
					for(int k=1;k<=arrRoomList.Length;k++)
					{
						string dim="0";
						string formValue=""+Request.Form["building_dim_"+j+"_"+k];
						if(formValue.Length>0)
						{
							dim=""+Request.Form["building_dim_"+j+"_"+k];
						}
						
						EntityData entity2=ProductDAO.GetRoomByBuildingCodeAndPos(buildingCode,k,j);
						if(entity2.HasRecord())
						{
							DataRow dr2=entity2.CurrentRow;
							dr2["BuildArea"]=dim;
							ProductDAO.UpdateRoom(entity2);
						}
						entity2.Dispose();
							
					}
				}

				string s = JavaScript.ScriptStart
					+ "GotoBuildingPart('"+buildingCode+"');"
					+ JavaScript.ScriptEnd;
				Page.RegisterStartupScript("goto", s);

//				Response.Write(JavaScript.ScriptStart);
//				Response.Write("window.location.href='../PBS/BuildingStructureModify.aspx?action=building_dim&BuildingCode="+buildingCode+"';");
//				Response.Write("window.close();");
//				Response.Write(JavaScript.ScriptEnd);

			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		private void Updata5(string buildingCode)
		{
			try
			{
				string floorList="";
				string roomList="";
				EntityData entity=ProductDAO.GetBuildingByCode(buildingCode);
				if(entity.HasRecord())
				{
					floorList=entity.GetString("FloorList");
					roomList=entity.GetString("Room_list");
				}
				entity.Dispose();

				string[] arrFloorList=floorList.Split(",".ToCharArray());
				string[] arrRoomList=roomList.Split(",".ToCharArray());

				for (int j=1;j<=arrFloorList.Length;j++)
				{
					for(int k=1;k<=arrRoomList.Length;k++)
					{
						string dim="0";
						string formValue=""+Request.Form["building_dim_"+j+"_"+k];
						if(formValue.Length>0)
						{
							
							dim=""+Request.Form["room_dim_"+j+"_"+k];
						}
						
						EntityData entity2=ProductDAO.GetRoomByBuildingCodeAndPos(buildingCode,k,j);
						if(entity2.HasRecord())
						{
							DataRow dr2=entity2.CurrentRow;
							dr2["RoomArea"]=dim;
							ProductDAO.UpdateRoom(entity2);
						}
						entity2.Dispose();
							
					}
				}

				string s = JavaScript.ScriptStart
					+ "GotoBuildingPart('"+buildingCode+"');"
					+ JavaScript.ScriptEnd;
				Page.RegisterStartupScript("goto", s);

//				Response.Write(JavaScript.ScriptStart);
//				Response.Write("window.location.href='../PBS/BuildingStructureModify.aspx?action=room_dim&BuildingCode="+buildingCode+"';");
//				Response.Write("window.close();");
//				Response.Write(JavaScript.ScriptEnd);

			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// 修改楼栋结构
		/// </summary>
		/// <param name="roomCode"></param>
		private void Updata6(string roomCode)
		{
			try
			{
				string buildingCode = Request["BuildingCode"];

				EntityData entity2=ProductDAO.GetRoomByCode(roomCode);
				if(entity2.HasRecord())
				{
					DataRow dr2=entity2.CurrentRow;
					dr2["CellSpan"]=int.Parse(dr2["CellSpan"].ToString())-1;
					ProductDAO.UpdateRoom(entity2);
				}
				entity2.Dispose();

//				string s = JavaScript.ScriptStart
//					+ "GotoBuildingPart('"+buildingCode+"');"
//					+ JavaScript.ScriptEnd;
//				Page.RegisterStartupScript("goto", s);

				Response.Write(JavaScript.ScriptStart);
				Response.Write("window.location.href='../PBS/BuildingStructureModify.aspx?action=structure&BuildingCode="+Request["BuildingCode"]+"';");
				Response.Write("window.close();");
				Response.Write(JavaScript.ScriptEnd);

			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		private void Updata7(string roomCode)
		{
			try
			{
				string buildingCode = Request["BuildingCode"];

				EntityData entity2=ProductDAO.GetRoomByCode(roomCode);
				if(entity2.HasRecord())
				{
					DataRow dr2=entity2.CurrentRow;
					dr2["RowSpan"]=int.Parse(dr2["RowSpan"].ToString())-1;
					ProductDAO.UpdateRoom(entity2);
				}
				entity2.Dispose();

//				string s = JavaScript.ScriptStart
//					+ "GotoBuildingPart('"+buildingCode+"');"
//					+ JavaScript.ScriptEnd;
//				Page.RegisterStartupScript("goto", s);

				Response.Write(JavaScript.ScriptStart);
				Response.Write("window.location.href='../PBS/BuildingStructureModify.aspx?action=structure&BuildingCode="+Request["BuildingCode"]+"';");
				Response.Write("window.close();");
				Response.Write(JavaScript.ScriptEnd);

			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// 向左合并
		/// </summary>
		/// <param name="roomCode"></param>
		private void Updata8Left(string roomCode)
		{
			try
			{
				string buildingCode = Request["BuildingCode"];

				int colspans=0;
				int rowspans=0;
				int colspan=0;
				int rowspan=0;
				string targetId="";
				EntityData entity=ProductDAO.GetRoomByCode(roomCode);
				if(entity.HasRecord())
				{
					colspans=entity.GetInt("CellSpan");
					rowspans=entity.GetInt("RowSpan");
				}
				else
				{
					Response.Write(JavaScript.Alert(true,"合并发生错误，请联系管理员！"));
					Response.Write(JavaScript.ScriptStart);
					
					Response.Write("window.history.go(-1)();");
					Response.Write(JavaScript.ScriptEnd);
					return;
				}
				entity.Dispose();

				//取左边房间，进行左边房间的向右合并，房间的名称还是用右面的房间名称
				string roomName = entity.GetString("RoomName");
				int floorIndex = entity.GetInt("FloorIndex");
				int roomIndex = entity.GetInt("RoomIndex");
				EntityData allRoom = ProductDAO.GetRoomByBuildingCode(Request["BuildingCode"]+"");
				DataRow[] drs = allRoom.CurrentTable.Select( "RoomIndex<" + roomIndex.ToString() + " and FloorIndex = " + floorIndex.ToString()," RoomIndex desc" );

				//没有合适的房间
				if ( drs.Length == 0 )
				{
					allRoom.Dispose();
					entity.Dispose();
					Response.Write(JavaScript.ScriptStart);
					Response.Write(JavaScript.Alert(false,"选定的物业左侧没有物业，不可合并！"));
					Response.Write("window.history.go(-1);");
					Response.Write(JavaScript.ScriptEnd);
					return;
				}
				DataRow upRow = drs[0];

				targetId = BLL.ConvertRule.ToString(upRow["RoomCode"]);
				colspan = BLL.ConvertRule.ToInt(upRow["CellSpan"]);
				rowspan = BLL.ConvertRule.ToInt(upRow["RowSpan"]);

				if(rowspans!=rowspan)
				{
					Response.Write(JavaScript.Alert(true,"选定的物业与目标房间的高度不同，不可合并！"));
					Response.Write(JavaScript.ScriptStart);
					
					Response.Write("window.history.go(-1);");
					Response.Write(JavaScript.ScriptEnd);
					return;
				}

				EntityData entity2=ProductDAO.GetRoomByCode(targetId);
				if(entity2.HasRecord())
				{
					DataRow dr2 =entity2.CurrentRow;
					dr2["CellSpan"] = BLL.ConvertRule.ToInt(dr2["CellSpan"])+1;
					dr2["RoomName"] = roomName;
					ProductDAO.UpdateRoom(entity2);
				}
				entity2.Dispose();

				EntityData entity3=ProductDAO.GetRoomByCode(roomCode);
				if (entity3.HasRecord())
				{
					ProductDAO.DeleteRoom(entity3);
				}
				entity3.Dispose();

				//				string s = JavaScript.ScriptStart
				//					+ "GotoBuildingPart('"+buildingCode+"');"
				//					+ JavaScript.ScriptEnd;
				//				Page.RegisterStartupScript("goto", s);

				Response.Write(JavaScript.ScriptStart);
				Response.Write("window.location.href='../PBS/BuildingStructureModify.aspx?action=structure&BuildingCode="+Request["BuildingCode"]+"';");
				Response.Write("window.close();");
				Response.Write(JavaScript.ScriptEnd);


			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// 向右合并
		/// </summary>
		/// <param name="roomCode"></param>
		private void Updata8(string roomCode)
		{
			try
			{
				string buildingCode = Request["BuildingCode"];

				int colspans=0;
				int rowspans=0;
				int colspan=0;
				int rowspan=0;
				string targetId="";
				EntityData entity=ProductDAO.GetRoomByCode(roomCode);
				if(entity.HasRecord())
				{
					colspans=entity.GetInt("CellSpan");
					rowspans=entity.GetInt("RowSpan");
				}
				else
				{
					Response.Write(JavaScript.Alert(true,"合并发生错误，请联系管理员！"));
					Response.Write(JavaScript.ScriptStart);
					
					Response.Write("window.history.go(-1)();");
					Response.Write(JavaScript.ScriptEnd);
					return;
				}
				entity.Dispose();
				EntityData entity1=ProductDAO.GetRoomByBuildingCodeAndPos(""+Request["BuildingCode"],int.Parse(""+Request["room_x"])+colspans,int.Parse(""+Request["room_y"]));
				if(entity1.HasRecord())
				{
					targetId=entity1.GetString("RoomCode");
					colspan=entity1.GetInt("CellSpan");
					rowspan=entity1.GetInt("RowSpan");
				}
				else
				{
					Response.Write(JavaScript.Alert(true,"选定的物业右侧没有物业，不可合并！"));
					Response.Write(JavaScript.ScriptStart);
					
					Response.Write("window.history.go(-1);");
					Response.Write(JavaScript.ScriptEnd);
					return;
				}
				entity1.Dispose();

				if(rowspans!=rowspan)
				{
					Response.Write(JavaScript.Alert(true,"选定的物业与目标房间的高度不同，不可合并！"));
					Response.Write(JavaScript.ScriptStart);
					
					Response.Write("window.history.go(-1);");
					Response.Write(JavaScript.ScriptEnd);
					return;
				}

				EntityData entity2=ProductDAO.GetRoomByCode(roomCode);
				if(entity2.HasRecord())
				{
					DataRow dr2=entity2.CurrentRow;
					dr2["CellSpan"]=colspans+1;
					ProductDAO.UpdateRoom(entity2);
				}
				entity2.Dispose();

				EntityData entity3=ProductDAO.GetRoomByCode(targetId);
				if (entity3.HasRecord())
				{
					ProductDAO.DeleteRoom(entity3);
				}
				entity3.Dispose();

//				string s = JavaScript.ScriptStart
//					+ "GotoBuildingPart('"+buildingCode+"');"
//					+ JavaScript.ScriptEnd;
//				Page.RegisterStartupScript("goto", s);

				Response.Write(JavaScript.ScriptStart);
				Response.Write("window.location.href='../PBS/BuildingStructureModify.aspx?action=structure&BuildingCode="+Request["BuildingCode"]+"';");
				Response.Write("window.close();");
				Response.Write(JavaScript.ScriptEnd);


			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// 向下合并
		/// </summary>
		/// <param name="roomCode"></param>
		private void Updata9(string roomCode)
		{
			try
			{
				string buildingCode = Request["BuildingCode"];

				int colspans=0;
				int rowspans=0;
				int colspan=0;
				int rowspan=0;
				string targetId="";
				EntityData entity=ProductDAO.GetRoomByCode(roomCode);
				if(entity.HasRecord())
				{
					colspans=entity.GetInt("CellSpan");
					rowspans=entity.GetInt("RowSpan");
				}
				else
				{
					Response.Write(JavaScript.Alert(true,"合并发生错误，请联系管理员！"));
					Response.Write(JavaScript.ScriptStart);
					
					Response.Write("window.history.go(-1);");
					Response.Write(JavaScript.ScriptEnd);
					return;
				}
				entity.Dispose();
				EntityData entity1=ProductDAO.GetRoomByBuildingCodeAndPos(""+Request["BuildingCode"],int.Parse(""+Request["room_x"]),int.Parse(""+Request["room_y"])-rowspans);
				if(entity1.HasRecord())
				{
					targetId=entity1.GetString("RoomCode");
					colspan=entity1.GetInt("CellSpan");
					rowspan=entity1.GetInt("RowSpan");
				}
				else
				{
					Response.Write(JavaScript.Alert(true,"选定的物业右侧没有物业，不可合并！"));
					Response.Write(JavaScript.ScriptStart);
					
					Response.Write("window.history.go(-1);");
					Response.Write(JavaScript.ScriptEnd);
					return;
				}
				entity1.Dispose();
				if(colspans!=colspan)
				{
					Response.Write(JavaScript.Alert(true,"选定的物业与目标房间水平位置的不同，不可合并！"));
					Response.Write(JavaScript.ScriptStart);
					
					Response.Write("window.history.go(-1);");
					Response.Write(JavaScript.ScriptEnd);
					return;
				}

				EntityData entity2=ProductDAO.GetRoomByCode(roomCode);
				if(entity2.HasRecord())
				{
					DataRow dr2=entity2.CurrentRow;
					dr2["RowSpan"]=rowspans+1;
					
					ProductDAO.UpdateRoom(entity2);
				}
				entity2.Dispose();

				EntityData entity3=ProductDAO.GetRoomByCode(targetId);
				if (entity3.HasRecord())
				{
					ProductDAO.DeleteRoom(entity3);
				}
				entity3.Dispose();

//				string s = JavaScript.ScriptStart
//					+ "GotoBuildingPart('"+buildingCode+"');"
//					+ JavaScript.ScriptEnd;
//				Page.RegisterStartupScript("goto", s);

				Response.Write(JavaScript.ScriptStart);
				Response.Write("window.location.href='../PBS/BuildingStructureModify.aspx?action=structure&BuildingCode="+Request["BuildingCode"]+"';");
				Response.Write("window.close();");
				Response.Write(JavaScript.ScriptEnd);


			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// 向上合并
		/// </summary>
		/// <param name="roomCode"></param>
		private void Updata9Up(string roomCode)
		{
			try
			{
				string buildingCode = Request["BuildingCode"];

				EntityData entity=ProductDAO.GetRoomByCode(roomCode);
				

				if( ! entity.HasRecord())
				{
					entity.Dispose();
					Response.Write(JavaScript.ScriptStart);
					Response.Write(JavaScript.Alert(false,"合并发生错误，请联系管理员！"));
					Response.Write("window.history.go(-1);");
					Response.Write(JavaScript.ScriptEnd);
					return;
				}

				//取上一个房间，进行上一个房间的向下合并，房间的名称还是用下面的房间名称
				string roomName = entity.GetString("RoomName");
				int floorIndex = entity.GetInt("FloorIndex");
				int roomIndex = entity.GetInt("RoomIndex");
				EntityData allRoom = ProductDAO.GetRoomByBuildingCode(Request["BuildingCode"]+"");
				DataRow[] drs = allRoom.CurrentTable.Select( "RoomIndex=" + roomIndex.ToString() + " and FloorIndex > " + floorIndex.ToString()," FloorIndex " );
				//EntityData upRoom=ProductDAO.GetRoomByBuildingCodeAndPos(""+Request["BuildingCode"],int.Parse(""+Request["room_x"]),int.Parse(""+Request["room_y"])+1);

				//没有合适的房间
				if ( drs.Length == 0 )
				{
					allRoom.Dispose();
					entity.Dispose();
					Response.Write(JavaScript.ScriptStart);
					Response.Write(JavaScript.Alert(false,"找不到合并的对象，请联系管理员！"));
					Response.Write("window.history.go(-1);");
					Response.Write(JavaScript.ScriptEnd);
					return;
				}
				DataRow upRow = drs[0];

				upRow["RowSpan"] = ((int)upRow["RowSpan"]) + 1;
				upRow["RoomName"] = roomName;
				ProductDAO.UpdateRoom(allRoom);
				ProductDAO.DeleteRoom(entity);
				allRoom.Dispose();
				entity.Dispose();

//				string s = JavaScript.ScriptStart
//					+ "GotoBuildingPart('"+buildingCode+"');"
//					+ JavaScript.ScriptEnd;
//				Page.RegisterStartupScript("goto", s);

				Response.Write(JavaScript.ScriptStart);
				Response.Write("window.location.href='../PBS/BuildingStructureModify.aspx?action=structure&BuildingCode="+Request["BuildingCode"]+"';");
				Response.Write("window.close();");
				Response.Write(JavaScript.ScriptEnd);


			}
			catch(Exception ex)
			{
				throw ex;
			}
		}




		private void Updata10(string roomCode)
		{
			string buildingCode = Request["BuildingCode"];

			try
			{
				EntityData entity3=ProductDAO.GetRoomByCode(roomCode);
				if (entity3.HasRecord())
				{
					ProductDAO.DeleteRoom(entity3);
				}
				entity3.Dispose();
			}
			catch(Exception ex)
			{
				throw ex;
			}

//			string s = JavaScript.ScriptStart
//				+ "GotoBuildingPart('"+buildingCode+"');"
//				+ JavaScript.ScriptEnd;
//			Page.RegisterStartupScript("goto", s);

			Response.Write(JavaScript.ScriptStart);
			Response.Write("window.location.href='../PBS/BuildingStructureModify.aspx?action=structure&BuildingCode="+Request["BuildingCode"]+"';");
			Response.Write("window.close();");
			Response.Write(JavaScript.ScriptEnd);

		}

		/// <summary>
		/// 新增房间
		/// </summary>
		/// <param name="roomCode"></param>
		private void Updata11(string roomCode)
		{
			try
			{
				try
				{
					string buildingCode = Request["BuildingCode"];

					string floorList="";
					string roomList="";
					string ProjectCode = "";
					EntityData entity=ProductDAO.GetBuildingByCode(""+Request["BuildingCode"]);
					if(entity.HasRecord())
					{						
						floorList=entity.GetString("FloorList");
						roomList=entity.GetString("Room_list");
						ProjectCode = entity.GetString("ProjectCode");
					}
					else 
					{
						Response.Write(JavaScript.Alert(true, "楼栋未找到"));
						return;
					}
					entity.Dispose();
					
					string[] arrFloorList=floorList.Split(",".ToCharArray());
					string[] arrRoomList=roomList.Split(",".ToCharArray());

					string chamberCode="";
					EntityData entity1=ProductDAO.getChamberByBuildingCode(""+Request["BuildingCode"]);
					if(entity1.HasRecord())
					{
						
						int inti=0;
						for(int i=1;i<=entity1.CurrentTable.Rows.Count;i++)
						{
							for(int m=1;m<=int.Parse(entity1.CurrentTable.Rows[i-1]["RoomCount"].ToString());m++)
							{
								inti+=1;
								if (int.Parse(""+Request["room_x"])==inti)
								{
									chamberCode=entity1.CurrentTable.Rows[i-1]["ChamberCode"].ToString();
								}

							}
						}
						
					}
					entity1.Dispose();
				
					EntityData entity2=ProductDAO.GetRoomByBuildingCodeAndPos(""+Request["BuildingCode"],int.Parse(""+Request["room_x"]),int.Parse(""+Request["room_y"]));
					if(entity2.HasRecord())
					{
						
						EntityData entity3=DAL.EntityDAO.ProductDAO.GetRoomByCode("");
						DataRow dr3=null;
						string curRoomCode=SystemManageDAO.GetNewSysCode("RoomCode").ToString();
						dr3=entity3.GetNewRecord();
						
						dr3["RoomCode"]=curRoomCode;
						dr3["ProjectCode"]=ProjectCode;
						dr3["BuildingCode"]=entity2.GetString("buildingCode");
						dr3["ChamberCode"]=chamberCode;
						dr3["ModelCode"]=entity2.GetString("ModelCode");
						dr3["RoomName"]=arrFloorList[arrFloorList.Length-int.Parse(""+Request["room_y"])]+arrRoomList[int.Parse(""+Request["room_x"])-1];
						dr3["FloorIndex"]=""+Request["room_y"];
						dr3["FloorName"] = dr3["FloorIndex"].ToString();
						dr3["RoomIndex"]=""+Request["room_x"];
						dr3["BuildArea"]=entity2.GetDecimal("BuildArea");
						dr3["RoomArea"]=entity2.GetDecimal("RoomArea");
						dr3["RowSpan"]=1;
						dr3["CellSpan"]=1;
						entity3.AddNewRecord(dr3);
						ProductDAO.InsertRoom(entity3);
						
					}
					else
					{
						EntityData entity3=DAL.EntityDAO.ProductDAO.GetRoomByCode("");
						DataRow dr3=null;
						string curRoomCode=SystemManageDAO.GetNewSysCode("RoomCode").ToString();
						dr3=entity3.GetNewRecord();
						
						dr3["RoomCode"]=curRoomCode;
						dr3["ProjectCode"]=ProjectCode;
						dr3["BuildingCode"]=""+Request["BuildingCode"];
						dr3["ChamberCode"]=chamberCode;
						//dr3["ModelCode"]=hidValue;
						dr3["RoomName"]=arrFloorList[arrFloorList.Length-int.Parse(""+Request["room_y"])]+arrRoomList[int.Parse(""+Request["room_x"])-1];
						dr3["FloorIndex"]=""+Request["room_y"];
						dr3["FloorName"]=dr3["FloorIndex"].ToString();
						dr3["RoomIndex"]=""+Request["room_x"];
						dr3["BuildArea"]=0;
						dr3["RoomArea"]=0;
						dr3["RowSpan"]=1;
						dr3["CellSpan"]=1;
						entity3.AddNewRecord(dr3);
						ProductDAO.InsertRoom(entity3);
					}
					entity2.Dispose();
				
//					string s = JavaScript.ScriptStart
//						+ "GotoBuildingPart('"+buildingCode+"');"
//						+ JavaScript.ScriptEnd;
//					Page.RegisterStartupScript("goto", s);

					Response.Write(JavaScript.ScriptStart);
					Response.Write("window.location.href='../PBS/BuildingStructureModify.aspx?action=structure&BuildingCode="+Request["BuildingCode"]+"';");
					Response.Write("window.close();");
					Response.Write(JavaScript.ScriptEnd);

				}
				catch(Exception ex)
				{
					throw ex;
				}
			}
			catch(Exception ex)
			{
				throw ex;
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
