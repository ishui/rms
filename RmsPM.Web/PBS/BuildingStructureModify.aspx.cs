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
	/// BuildingStructureModify 的摘要说明。
	/// </summary>
	public partial class BuildingStructureModify : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTableRow trListBuild;
		protected System.Web.UI.HtmlControls.HtmlGenericControl fontMainUrl;


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

				string Tilte = "&nbsp;";
				bool needCheck = false;

				switch (this.txtAct.Value.ToLower()) 
				{
					case "structure":
						needCheck = true;
						Tilte="修改楼栋结构";
						break;

					case "base":
						needCheck = true;
						Tilte="修改室号";
						break;

					case "room_model":
						Tilte="修改户型";
						break;
				}

				this.tdTitle.InnerHtml = Tilte;

				if (needCheck) 
				{
					//检查楼栋结构是否可修改
					string hint = BLL.ProductRule.CanModifyBuildingStructure(txtBuildingCode.Value);
					if (hint != "") 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, hint));
						Response.Write(Rms.Web.JavaScript.HistoryTo(true, -1));
						return;
					}
				}
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
				string act = this.txtAct.Value;
				string BuildingCode = this.txtBuildingCode.Value;

				if (BuildingCode == "") 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "无楼栋编号"));
					return;
				}

				string buildingName="";
				int floorCount=0;
				string floorList="";
				string roomList="";
				string projectCode="";
				string unitProject="";
	
				//楼栋信息
				EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingByCode(BuildingCode);

				if (entity.HasRecord()) 
				{
					this.lblBuildingName.Text = entity.GetString("BuildingName");
					this.lblPBSTypeName.Text = BLL.PBSRule.GetPBSTypeFullName(entity.GetString("PBSTypeCode"));

					this.txtIsArea.Value = entity.GetInt("IsArea").ToString();
					this.txtProjectCode.Value = entity.GetString("ProjectCode");
					this.txtParentCode.Value = entity.GetString("ParentCode");

					buildingName=entity.GetString("BuildingName");
					floorCount=entity.GetInt("FloorCount");
					floorList=entity.GetString("FloorList");
					roomList=entity.GetString("Room_List");
					projectCode=entity.GetString("ProjectCode");
					unitProject=entity.GetString("UnitProject");
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "该楼栋不存在"));
					return;
				}

				entity.Dispose();

				DataTable dt1=new DataTable();				
				dt1.Columns.Add(new DataColumn("No3", typeof(String)));				
				dt1.Columns.Add(new DataColumn("No4", typeof(String)));	
				DataRow dr1=null;
				DataView dv1=new DataView();
				
				string[] arrFloorList=floorList.Split(",".ToCharArray());
				string[] arrRoomList=roomList.Split(",".ToCharArray());
				int intFloorList=arrFloorList.Length+1;
				int intRoomList=arrRoomList.Length+1;
				
				EntityData entity1=DAL.EntityDAO.ProductDAO.getChamberByBuildingCode(BuildingCode);
				if (entity1.HasRecord())
				{
					for (int i=0;i<=entity1.CurrentTable.Rows.Count-1;i++)
					{
						dr1 = dt1.NewRow();
						dr1[0]=entity1.CurrentTable.Rows[i]["RoomCount"];
						if (act=="base")
						{
							string list1="<input name=door_name type=text floor_count='"+intFloorList.ToString()+"' room_count='"+intRoomList.ToString()+"' value='"+entity1.CurrentTable.Rows[i]["ChamberName"]+"' size=10 maxlength=32 style=width:100%><br>";
							dr1[1]=list1;	
						}
						else
						{
							dr1[1]=entity1.CurrentTable.Rows[i]["RoomList"];
						}

						dt1.Rows.Add(dr1);
					}
					dv1 = new DataView(dt1);					
				}				
				entity1.Dispose();
				this.repeat1.DataSource=dv1;
				this.repeat1.DataBind();
				
				DataTable dt2=new DataTable();				
				dt2.Columns.Add(new DataColumn("No3", typeof(String)));				
				DataRow dr2=null;
				DataView dv2=new DataView();
			
				for(int j=0;j<=arrRoomList.Length-1;j++)
				{
					dr2 = dt2.NewRow();
					int intI=j+1;
					string list1="";
					if (act=="base")
					{
						list1+="<input name=room_list x="+j+" type=text value="+arrRoomList[j]+" floor_count="+intFloorList+" room_count="+intRoomList+" size=8 maxlength=32 style=width:100% onchange=doBaseRoomNameChange(this);>";
					}
					else
					{
						list1+=arrRoomList[j];						
					}
					if (act=="room_model")
					{
						list1+="<BR>"+SelectModelTypeX(intI.ToString(),"",projectCode,intFloorList,intRoomList);
					}
					if (act=="building_dim")
					{
						
						list1+="<BR><input name=room_building_dim onchange=doBuildingDimChange0(this); type=text id=room_building_dim size=6 x="+intI+" style=width:100% floor_count="+intFloorList+" room_count="+intRoomList+"> ";
					}
					if (act=="room_dim")
					{
						list1+="<BR><input name=room_building_dim onchange=doRoomDimChange0(this); type=text id=room_building_dim size=6 x="+intI+" style=width:100% floor_count="+intFloorList+" room_count="+intRoomList+">";
					}
					dr2[0]=list1;

					dt2.Rows.Add(dr2);
				}
				dv2 = new DataView(dt2);		
				this.dlBuild3.DataSource=dv2;
				this.dlBuild3.DataBind();
				
				DataTable dt3=new DataTable();				
				dt3.Columns.Add(new DataColumn("No3", typeof(String)));				
				dt3.Columns.Add(new DataColumn("No4", typeof(String)));		
				DataRow dr3=null;
				DataView dv3=new DataView();

				string nullRoom="";
				for(int k=floorCount;k>=1;k--)
				{

					dr3 = dt3.NewRow();
					string list1="";
					if (act=="base")
					{
						list1+="<input name=floor_list y="+k+" floor_count="+intFloorList+" room_count="+intRoomList+" type=text value="+arrFloorList[floorCount-k]+" size=8 maxlength=32 style=width:100% onChange=doBaseFloorNameChange(this);> ";
						
					}
					else
					{
						list1+=arrFloorList[floorCount-k];
//						list1+=k;
					}
					if(act=="room_model")
					{
						//横向不需要户型一次性选择  xyq 2004.9.10
//						list1+="<BR>"+SelectModelTypeX(k.ToString(),"",projectCode,intFloorList,intRoomList);
						
					}
					if (act=="building_dim")
					{
						
						list1+="<br><input name=room_building_dim onchange=doBuildingDimChange1(this); type=text id=room_building_dim size=6 y="+k+"  floor_count="+intFloorList+" room_count="+intRoomList+"> ";
						
					}
					if (act=="room_dim")
					{
						list1+="<br><input name=room_building_dim onchange=doRoomDimChange1(this); type=text id=room_building_dim size=6 y="+k+" floor_count="+intFloorList+" room_count="+intRoomList+">";
						
					}
					dr3[0]=list1;					
					
					string strList="";
					
					for(int l=1;l<=arrRoomList.Length;l++)
					{
						if (arrRoomList[l-1].Length>0)
						{
							int col=1;
							int row=1;
							int floorNum=0;
							int roomNum=0;
							string roomName="";
							string roomCode="";
							string modelCode="";
							float buildDim=0;
							float roomDim=0;
							string floorName="";
							

							EntityData entity2=DAL.EntityDAO.ProductDAO.GetRoomByBuildingCodeAndPos(BuildingCode,l,k);
							if (entity2.HasRecord())
							{
								col=entity2.GetInt("CellSpan");
								row=entity2.GetInt("RowSpan");
								floorNum=entity2.GetInt("FloorIndex");
								roomNum=entity2.GetInt("RoomIndex");
								roomName=entity2.GetString("RoomName");
								roomCode=entity2.GetString("RoomCode");
								modelCode=entity2.GetString("ModelCode");
								floorName=entity2.GetString("FloorName");

								buildDim=float.Parse(entity2.GetDecimal("BuildArea").ToString());
								roomDim=float.Parse(entity2.GetDecimal("RoomArea").ToString());


								if ((col>1)||(row>1))
								{
									for(int m=1;m<=row;m++)
									{
										for(int n=1;n<=col;n++)
										{
											if ((m>1)||(n>1))
											{
												int intF=floorNum-m+1;
												int intR=roomNum+n-1;
												nullRoom=nullRoom+"|"+intF.ToString()+":"+intR+"|";
											}
										}
									}
								}
							}
							string curIndexOf="|"+k+":"+l+"|";
							//Response.Write (nullRoom+"===="+curIndexOf+"<br>");
							if (nullRoom.IndexOf(curIndexOf)==-1)
							{
								
								
								if (!entity2.HasRecord())
								{
									strList+="<td align='center' nowrap rowspan='1' colspan='1' style='cursor:hand' onClick='doModifyBuilding(this);' rs='' cs=''  y='"+k+"' x='"+l+"' room_id=''>-</td>";
								}
								else
								{
									strList+="<td align=center nowrap rowspan="+row+" colspan="+col+" style='cursor:hand' onMouseOver=changeBgColor(this,'#D0E8FF') onMouseOut=changeBgColor(this,'#F3F5F8') > ";
									list1="";
									if (act=="base")
									{
										list1+="<input name=room_name_"+k+"_"+l+" value="+roomName+" size=8 maxlength=32 style=width:100%> ";
										
									}
									if (act=="structure")
									{
										list1+="<a href=# room_id="+roomCode+" y="+k+" x="+l+" rs="+row+" cs="+col+" onClick=doModifyBuilding(this);return false;>"+roomName+"</a>";
										
									}
									if ((act!="base")&&(act!="structure"))
									{
										list1+=roomName;
										

									}
									if(act=="room_model")
									{
										list1+="<br>"+SelectModelTypeXY(k.ToString(),l.ToString(),modelCode,projectCode);
										
									}
									if (act=="building_dim")
									{
						
										list1+="<br>"+"<input name=building_dim_"+k+"_"+l+" value="+buildDim+" onchange=doBuildingDimChanges(this); type=text id=building_dim__"+k+"_"+l+" size=6 x="+k+" > ";
										
									}
									if (act=="room_dim")
									{
										list1+="<br>"+"<input name=room_dim_"+k+"_"+l+" value="+roomDim+" onchange=doRoomDimChanges(this); type=text id=room_dim_"+k+"_"+l+" size=6 x="+k+" > ";
										
									}
									strList+=list1;
									strList+="</td>";
								}
								

							}
							else
							{
							}
							entity2.Dispose();
						}
					}
					dr3[1]=strList;
					dt3.Rows.Add(dr3);
				}
				dv3 = new DataView(dt3);		
				this.dlBuild1.DataSource=dv3;
				this.dlBuild1.DataBind();
				
				
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		private static string SelectOptionList(string selectedValue,string projectCode)
		{
			try
			{
				string resultStr="";
				EntityData entity=RmsPM.DAL.EntityDAO.PBSDAO.GetPBSTypeByProject(projectCode);
				if (entity.HasRecord())
				{
					string layer="";
					int curRows=0;
					for(int i=0;i<=entity.CurrentTable.Rows.Count-1;i++)
					{
						curRows=curRows+1;
						string treePic="";
						if (curRows>entity.CurrentTable.Rows.Count-1)
						{
							treePic="└──";
						}
						else
						{
							treePic="├──";
						}
						for(int j=1;j<int.Parse(entity.CurrentTable.Rows[i]["deep"].ToString());j++)
						{

							layer=layer+"    "+treePic;
						}
						
						entity.SetCurrentRow(i);
						if (selectedValue==entity.GetString("PBSTypeCode"))
						{
							resultStr+="<option value='"+entity.GetString("PBSTypeCode")+"' selected>"+layer+entity.GetString("PBSTypeName")+"</option>";
						}
						else
						{
							resultStr+="<option value='"+entity.GetString("PBSTypeCode")+"'>"+layer+entity.GetString("PBSTypeName")+"</option>";
						}
						layer="";
					}
				}
				entity.Dispose();

				return resultStr;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		private static string SelectModelTypeX(string k,string selectedValue,string projectCode,int intFloorList,int intRoomList)
		{
			try
			{
				string resultStr="";
				resultStr="<select name='room_model' x="+k+" id='room_model' onChange=doRoomModelChange(this); floor_count="+intFloorList.ToString()+" room_count="+intRoomList.ToString()+" >";
				resultStr+="<option value=''></option>";
				EntityData entity=RmsPM.DAL.EntityDAO.ProductDAO.GetModelByProjectCode(projectCode);
				if (entity.HasRecord())
				{
					for(int i=0;i<=entity.CurrentTable.Rows.Count-1;i++)
					{
						entity.SetCurrentRow(i);
						if (selectedValue==entity.GetString("ModelCode"))
						{
							resultStr+="<option value='"+entity.GetString("ModelCode")+"' selected>"+entity.GetString("ModelName")+"</option>";
						}
						else
						{
							resultStr+="<option value='"+entity.GetString("ModelCode")+"'>"+entity.GetString("ModelName")+"</option>";
						
						}

					}
				}
				entity.Dispose();

				resultStr+="</select>";
				return resultStr;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		private static string SelectModelTypeY(string k,string selectedValue,string projectCode,int intFloorList,int intRoomList)
		{
			try
			{
				string resultStr="";
				resultStr="<select name='room_model' y="+k+" id='room_model' onChange=doFloorModelChange(this); floor_count="+intFloorList.ToString()+" room_count="+intRoomList.ToString()+" >";
				resultStr+="<option value=''></option>";
				EntityData entity=RmsPM.DAL.EntityDAO.ProductDAO.GetModelByProjectCode(projectCode);
				if (entity.HasRecord())
				{
					for(int i=0;i<=entity.CurrentTable.Rows.Count-1;i++)
					{
						entity.SetCurrentRow(i);
						if (selectedValue==entity.GetString("ModelCode"))
						{
							resultStr+="<option value='"+entity.GetString("ModelCode")+"' selected>"+entity.GetString("ModelName")+"</option>";
						}
						else
						{
							resultStr+="<option value='"+entity.GetString("ModelCode")+"'>"+entity.GetString("ModelName")+"</option>";
						
						}

					}
				}
				entity.Dispose();

				resultStr+="</select>";
				return resultStr;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		private static string SelectModelTypeXY(string k,string l,string selectedValue,string projectCode)
		{
			try
			{
				string resultStr="";
				resultStr="<select name='room_model_"+k+"_"+l+"'  id='room_model_"+k+"_"+l+"'  >";
				resultStr+="<option value=''></option>";
				EntityData entity=RmsPM.DAL.EntityDAO.ProductDAO.GetModelByProjectCode(projectCode);
				if (entity.HasRecord())
				{
					for(int i=0;i<=entity.CurrentTable.Rows.Count-1;i++)
					{
						entity.SetCurrentRow(i);
						if (selectedValue==entity.GetString("ModelCode"))
						{
							resultStr+="<option value='"+entity.GetString("ModelCode")+"' selected>"+entity.GetString("ModelName")+"</option>";
						}
						else
						{
							resultStr+="<option value='"+entity.GetString("ModelCode")+"'>"+entity.GetString("ModelName")+"</option>";
						
						}

					}
				}
				entity.Dispose();

				resultStr+="</select>";
				return resultStr;
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
