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
	/// BuildingPart 的摘要说明。
	/// </summary>
	public partial class BuildingPart : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton Save;
		
			
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

				//权限
				this.btnAddWizard.Visible = base.user.HasRight("010312");
				this.btnModifyBuildingStructure.Visible = base.user.HasRight("010313");
				this.btnModifyRoomName.Visible = base.user.HasRight("010313");
				this.btnModifyRoomModel.Visible = base.user.HasRight("010313");
				this.btnRebuild.Visible = base.user.HasRight("010314");
				this.btnModifyRoomArea.Visible = base.user.HasRight("010315");

				//				this.txtFromUrl.Value = Request.QueryString["FromUrl"];

//				this.tbHint.Visible = true;
//				this.divMain.Visible = false;
//				this.tbMain2.Visible = false;
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
				this.txtHasLoad.Value = "1";
//				this.tbHint.Visible = false;
//				this.divMain.Visible = true;
//				this.tbMain2.Visible = true;

				string BuildingCode = this.txtBuildingCode.Value;
				bool IsRoomExists = false;

				if (BuildingCode == "") 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "无楼栋编号"));
					return;
				}

				//楼栋信息
				EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingByCode(BuildingCode);
				string roomList = "";
				int floorCount = 0;

				if (entity.HasRecord()) 
				{
					this.lblBuildingName.Text = entity.GetString("BuildingName");
					this.lblPBSTypeName.Text = BLL.PBSRule.GetPBSTypeFullName(entity.GetString("PBSTypeCode"));

					this.txtIsArea.Value = entity.GetInt("IsArea").ToString();
					this.txtProjectCode.Value = entity.GetString("ProjectCode");
					this.txtParentCode.Value = entity.GetString("ParentCode");

					roomList = entity.GetString("Room_List");
					floorCount = entity.GetInt("FloorCount");

				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "该楼栋不存在"));
					return;
				}

				entity.Dispose();

				//填充楼栋下拉框
				BLL.PageFacade.LoadBuildingSelect(this.sltBuilding, BuildingCode, this.txtProjectCode.Value);

				DataTable dt1=new DataTable();				
				dt1.Columns.Add(new DataColumn("No3", typeof(String)));				
				dt1.Columns.Add(new DataColumn("No4", typeof(String)));	
				DataRow dr1=null;
				DataView dv1=new DataView();

				EntityData entity1=DAL.EntityDAO.ProductDAO.getChamberByBuildingCode(BuildingCode);
				if (entity1.HasRecord())
				{
					IsRoomExists = true;

					for (int i=0;i<=entity1.CurrentTable.Rows.Count-1;i++)
					{
						dr1 = dt1.NewRow();
						dr1[0]=entity1.CurrentTable.Rows[i]["RoomCount"];
						dr1[1]=entity1.CurrentTable.Rows[i]["ChamberName"];
						dt1.Rows.Add(dr1);
					}
					dv1 = new DataView(dt1);					
				}
				entity1.Dispose();
				this.repeat1.DataSource=dv1;
				this.repeat1.DataBind();
				
				string[] arrRoomList=roomList.Split(",".ToCharArray());

				DataTable dt2=new DataTable();				
				dt2.Columns.Add(new DataColumn("No3", typeof(String)));				
				DataRow dr2=null;
				DataView dv2=new DataView();
			
				for(int j=0;j<=arrRoomList.Length-1;j++)
				{
					dr2 = dt2.NewRow();
					dr2[0]=arrRoomList[j];
					dt2.Rows.Add(dr2);
				}
				dv2 = new DataView(dt2);		
				this.dlBuild3.DataSource=dv2;
				this.dlBuild3.DataBind();
				
				DataTable dt3=new DataTable();				
				dt3.Columns.Add(new DataColumn("FloorCode", typeof(String)));
				dt3.Columns.Add(new DataColumn("FloorName", typeof(String)));
				dt3.Columns.Add(new DataColumn("No4", typeof(String)));
				DataRow dr3=null;
				DataView dv3=new DataView();

				string nullRoom="";
				
				EntityData entityRoom = DAL.EntityDAO.ProductDAO.GetRoomByBuildingCode(BuildingCode);

				for(int k=floorCount;k>=1;k--)
				{

					dr3 = dt3.NewRow();
					dr3["FloorCode"] = k;
					dr3["FloorName"] = BLL.ProductRule.GetFloorNameByBuildingFloorIndex(BuildingCode, k);
					
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
							string floorName="";
							string modelCode="";
							string buildArea="";
							string roomArea="";
							string preBuildArea = "";
							string preRoomArea = "";

							DataRow[] drsRoom = entityRoom.CurrentTable.Select(string.Format("FloorIndex={0} and RoomIndex={1}", k, l));
							DataRow drRoom = null;

							
							if (drsRoom.Length > 0)
							{
								drRoom = drsRoom[0];

								col = BLL.ConvertRule.ToInt(drRoom["CellSpan"]);
								row = BLL.ConvertRule.ToInt(drRoom["RowSpan"]);
								floorNum = BLL.ConvertRule.ToInt(drRoom["FloorIndex"]);
								roomNum = BLL.ConvertRule.ToInt(drRoom["RoomIndex"]);
								roomName = BLL.ConvertRule.ToString(drRoom["RoomName"]);
								roomCode = BLL.ConvertRule.ToString(drRoom["RoomCode"]);
								floorName = BLL.ConvertRule.ToString(drRoom["FloorName"]);
								modelCode = BLL.ConvertRule.ToString(drRoom["ModelCode"]);
								buildArea = BLL.ConvertRule.ToDecimal(drRoom["BuildArea"]).ToString("0.00");
								roomArea = BLL.ConvertRule.ToDecimal(drRoom["RoomArea"]).ToString("0.00");

								preBuildArea = BLL.ConvertRule.ToDecimal(drRoom["PreBuildArea"]).ToString("0.00");
								preRoomArea = BLL.ConvertRule.ToDecimal(drRoom["PreRoomArea"]).ToString("0.00");

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
								if (drsRoom.Length <= 0)
								{
									strList+="<td align='center' nowrap rowspan='1' colspan='1'>-</td>";
								}
								else
								{
									string modelName = "";
									string structure = "";
									string hint = "";

									//取户型信息
									if (modelCode != "") 
									{
										EntityData entityM = DAL.EntityDAO.ProductDAO.GetModelByCode(modelCode);
										if (entityM.HasRecord()) 
										{
											modelName = entityM.GetString("ModelName");
											structure = entityM.GetString("Structure");
										}
										entityM.Dispose();
									}
									hint = hint + "户型：" + modelName + "（" + structure + "）" + "<br>";

									//面积信息
									hint = hint + "实测建面：" + buildArea + "平米" + "<br>";
									hint = hint + "实测套面：" + roomArea + "平米" + "<br>";
									hint = hint + "预测建面：" + preBuildArea + "平米" + "<br>";
									hint = hint + "预测套面：" + preRoomArea + "平米" + "<br>";

									//房间状态用颜色区分
									string color = BLL.ProductRule.GetRoomColor(drRoom);

									//单元格上显示房间相关信息的提示
									strList+="<td align=center nowrap val='" + roomCode + "' rowspan="+row+" colspan="+col+" style='cursor:hand' class='list-i' hint='" + hint + "' onMouseOver=\"init(myjoybox, joyboxTable, linktitle, hint);RoomMouseOver(this)\" onMouseOut=\"mouseend();RoomMouseOut(this)\" onclick=\"OpenRoomInfo(this.val);\" style='background-color:" + color + "'>";
									strList+=roomName;
									strList+="</td>";
								}

							}
							else
							{

							}
						}
					}
					dr3["No4"]=strList;
					dt3.Rows.Add(dr3);
				}
				dv3 = new DataView(dt3);		
				this.dlBuild1.DataSource=dv3;
				this.dlBuild1.DataBind();
				
				//按房间状态求汇总数
				DataTable tbGroup = BLL.ProductRule.GetRoomCountGroupByState(entityRoom);
				this.repLegend.DataSource = tbGroup;
				this.repLegend.DataBind();

				entityRoom.Dispose();

				//显示按钮
				if (IsRoomExists) 
				{
					this.btnAddWizard.Style["display"] = "none";
					this.btnRebuild.Style["display"] = "";
					this.btnModifyBuildingStructure.Style["display"] = "";
					this.btnModifyRoomName.Style["display"] = "";
					this.btnModifyRoomModel.Style["display"] = "";
					this.btnModifyRoomArea.Style["display"] = "";
				}
				else 
				{
					this.btnAddWizard.Style["display"] = "";
					this.btnRebuild.Style["display"] = "none";
					this.btnModifyBuildingStructure.Style["display"] = "none";
					this.btnModifyRoomName.Style["display"] = "none";
					this.btnModifyRoomModel.Style["display"] = "none";
					this.btnModifyRoomArea.Style["display"] = "none";
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


		private void btnHidLoadData_ServerClick(object sender, System.EventArgs e)
		{
			LoadData();
		}

		/// <summary>
		/// 删除楼栋结构
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnRebuild_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string BuildingCode = this.txtBuildingCode.Value;

				//检查是否可删除楼栋结构
				string hint = BLL.ProductRule.CanModifyBuildingStructure(BuildingCode);
				if (hint != "") 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, hint));
					return;
				}

				BLL.ProductRule.DeleteBuildingStructure(BuildingCode);

				LoadData();
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
	}
}
