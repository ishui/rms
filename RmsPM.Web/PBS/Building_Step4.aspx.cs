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
	/// Building_Step4 的摘要说明。
	/// </summary>
	public partial class Building_Step4 : PageBase
	{
		
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
				DataRow dr;

				//门牌数
				int DoorCount = tbDoor.Rows.Count;
				this.txtDoorCount.Value = DoorCount.ToString();
				this.rpDoorName.DataSource = tbDoor;
				this.rpDoorName.DataBind();	

				//每梯户数
				DataTable tbRoom = new DataTable();	
				tbRoom.Columns.Add(new DataColumn("RoomName", typeof(String)));
				tbRoom.Columns.Add(new DataColumn("ColHtml", typeof(String)));
				
				int idI=0;
				for (int j=0;j<DoorCount;j++)
				{
					int RoomCountTemp = int.Parse(tbDoor.Rows[j]["RoomCount"].ToString());

					for(int k=1;k<=RoomCountTemp;k++)
					{
						idI = idI+1;
						dr = tbRoom.NewRow();
						dr["RoomName"] = formatStr(k.ToString(),2);
						dr["ColHtml"] = LoadSelect(idI.ToString(),this.txtProjectCode.Value);
						tbRoom.Rows.Add(dr);
					}
				}

				this.rpRoomCount.DataSource = tbRoom;
				this.rpRoomCount.DataBind();

				//session表：room
				NewTableRoom(tbDoor);

				//楼层列表
				DataTable tbFloor = (DataTable)Session["tbFloor"];
				tbRoom = (DataTable)Session["tbRoom"];

				int FloorCount = tbFloor.Rows.Count;
				int RoomCount = tbRoom.Rows.Count;

				for (int l=0;l<FloorCount;l++)
				{					
					DataRow drFloor = tbFloor.Rows[l];

					string strList="";
					idI=0;
					for (int m=0;m<RoomCount;m++)
					{
						idI=idI+1;
						strList=strList+"<td nowrap>"+LoadSelect1(idI.ToString()+"_"+ (l + 1).ToString(),this.txtProjectCode.Value)+"</td>";
					}
					drFloor["No3"]=strList;
				}
				this.dlBuild1.DataSource = tbFloor;
				this.dlBuild1.DataBind();

				NewTableModel(tbFloor, (DataTable)Session["tbRoom"]);
			}
			catch(Exception ex)
			{
				throw ex;
			}

		}

		private DataTable NewTableRoom(DataTable tbDoor)
		{
			DataTable dt;

			if ((Session["tbRoom"] == null) || (this.txtAct.Value.ToLower() != "prev")) 
			{
				dt = new DataTable();	
				
				dt.Columns.Add(new DataColumn("RoomCode", typeof(String)));
				dt.Columns.Add(new DataColumn("DoorName", typeof(String)));
				dt.Columns.Add(new DataColumn("DoorCode", typeof(String)));
				dt.Columns.Add(new DataColumn("RoomName", typeof(String)));
				
				DataRow dr = null;

				int DoorCount = tbDoor.Rows.Count;

				int idI = 0;
				for (int j=0;j<DoorCount;j++)
				{
					DataRow drDoor = tbDoor.Rows[j];
					int RoomCount = int.Parse(drDoor["RoomCount"].ToString());

					for(int k=1;k<=RoomCount;k++)
					{
						idI = idI+1;
						dr = dt.NewRow();
						dr["RoomCode"] = idI.ToString();
						dr["DoorName"] = drDoor["DoorName"].ToString();
						dr["DoorCode"] = drDoor["DoorCode"].ToString();
						dr["RoomName"] = formatStr(k.ToString(),2);
						dt.Rows.Add(dr);
					}
				}

				Session["tbRoom"] = dt;
			}
			else 
			{
				dt = (DataTable)Session["tbRoom"];
			}

			return dt;
		}

		private DataTable NewTableModel(DataTable tbFloor, DataTable tbRoom)
		{
			DataTable dt;

			if ((Session["tbModel"] == null) || (this.txtAct.Value.ToLower() != "prev")) 
			{
				dt = new DataTable();	
				
				dt.Columns.Add(new DataColumn("FloorCode", typeof(String)));
				dt.Columns.Add(new DataColumn("RoomCode", typeof(String)));
				dt.Columns.Add(new DataColumn("ModelCode", typeof(String)));
				
				DataRow dr = null;

				int FloorCount = tbFloor.Rows.Count;
				int RoomCount = tbRoom.Rows.Count;

				for (int i=0;i<FloorCount;i++)
				{
					DataRow drFloor = tbFloor.Rows[i];

					for(int k=0;k<RoomCount;k++)
					{
						DataRow drRoom = tbRoom.Rows[k];

						dr = dt.NewRow();
						dr["FloorCode"] = drFloor["FloorCode"].ToString();
						dr["RoomCode"] = drRoom["RoomCode"].ToString();
						dt.Rows.Add(dr);
					}
				}

				Session["tbModel"] = dt;
			}
			else 
			{
				dt = (DataTable)Session["tbModel"];
			}

			return dt;
		}

		/// <summary>
		/// 单元编号
		/// </summary>
		/// <param name="k"></param>
		/// <param name="projectCode"></param>
		/// <returns></returns>
		private static string LoadSelect(string k,string projectCode)
		{
			try
			{
				string resultStr="";
				resultStr="<select name='room_model_"+k+"' id='room_model_"+k+"' onChange=doChangeModelCreate('"+k+"',this);>";
				resultStr+="<option value=''></option>";
				EntityData entity=RmsPM.DAL.EntityDAO.ProductDAO.GetModelByProjectCode(projectCode);
				if (entity.HasRecord())
				{
					for(int i=0;i<=entity.CurrentTable.Rows.Count-1;i++)
					{
						entity.SetCurrentRow(i);
						resultStr+="<option value='"+entity.GetString("ModelCode")+"'>"+entity.GetString("ModelName")+"</option>";

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

		private static string LoadSelect1(string k,string projectCode)
		{
			try
			{
				string resultStr="";
				resultStr="<select name='room_model_"+k+"' id='room_model_"+k+"' runat='server'>";
				resultStr+="<option value=''></option>";
				EntityData entity=RmsPM.DAL.EntityDAO.ProductDAO.GetModelByProjectCode(projectCode);
				if (entity.HasRecord())
				{
					for(int i=0;i<=entity.CurrentTable.Rows.Count-1;i++)
					{
						entity.SetCurrentRow(i);
						resultStr+="<option value='"+entity.GetString("ModelCode")+"'>"+entity.GetString("ModelName")+"</option>";

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

			int iCount = this.rpRoomCount.Items.Count;
			for(int i=0;i<iCount;i++) 
			{
				HtmlInputText txtRoomName = (HtmlInputText)this.rpRoomCount.Items[i].FindControl("txtRoomName");
				HtmlInputText txtDoorCode = (HtmlInputText)this.rpRoomCount.Items[i].FindControl("txtDoorCode");
				HtmlInputText txtRoomCount = (HtmlInputText)this.rpRoomCount.Items[i].FindControl("txtRoomCount");

				if (txtRoomName.Value.Trim() == "") 
				{
					Hint = "请输入室号";
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

				string buildingCode = this.txtBuildingCode.Value;

				DataTable tbFloor = (DataTable)Session["tbFloor"];
				DataTable tbRoom = (DataTable)Session["tbRoom"];
				DataTable tbModel = (DataTable)Session["tbModel"];

				//记录单元名称
				int RoomCount = this.rpRoomCount.Items.Count;
				for(int i=0;i<RoomCount;i++) 
				{
					
					HtmlInputText txtRoomName = (HtmlInputText)this.rpRoomCount.Items[i].FindControl("txtRoomName");

					DataRow dr = tbRoom.Rows[i];
					dr["RoomName"] = txtRoomName.Value;
				}

				//记录每层的户型
				int FloorCount = tbFloor.Rows.Count;
				for(int i=0;i<FloorCount;i++) 
				{
					string FloorCode = tbFloor.Rows[i]["FloorCode"].ToString();
//					string FloorCode = (i + 1).ToString();

//					HtmlInputHidden txtModelCode = (HtmlInputHidden)this.dlBuild1.Items[i].FindControl("txtModelCode");
//					string[] arrModelCode = txtModelCode.Value.Split(".".ToCharArray());
//
//					for (int j=0;j<RoomCount;j++) 
//					{
//						string RoomCode = (j + 1).ToString();
//
//						DataRow dr = tbModel.Select("FloorCode='" + FloorCode + "' and RoomCode='" + RoomCode + "'")[0];
//						dr["ModelCode"] = arrModelCode[j];
//					}

					for (int j=0;j<RoomCount;j++) 
					{
						string sltName = string.Format("room_model_{0}_{1}", j+1, i+1);
						string ModelCode = Request.Form[sltName].ToString();

						string RoomCode = (j + 1).ToString();

						DataRow dr = tbModel.Select("FloorCode='" + FloorCode + "' and RoomCode='" + RoomCode + "'")[0];
						dr["ModelCode"] = ModelCode;
					}
				}

				Session["tbRoom"] = tbRoom;
				Session["tbModel"] = tbModel;

				//				EntityData entity = ProductDAO.GetBuildingByCode(buildingCode);
				//				dr = entity.CurrentRow;
				//				ProductDAO.UpdateBuilding(entity);
				//
				//				entity.Dispose();

				Response.Write(JavaScript.ScriptStart);
				Response.Write(string.Format("window.location.href='Building_Step5.aspx?BuildingCode={0}';", buildingCode));
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
