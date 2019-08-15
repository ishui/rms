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
	/// Building_Step3 的摘要说明。
	/// </summary>
	public partial class Building_Step3 : PageBase
	{
		
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
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
				this.txtDoorCount.Value = Request["DoorCount"];
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

				EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingByCode(BuildingCode);

				if (entity.HasRecord()) 
				{
						this.lblBuildingName.Text = entity.GetString("BuildingName");
						this.lblPBSTypeName.Text = BLL.PBSRule.GetPBSTypeFullName(entity.GetString("PBSTypeCode"));
					//						FloorCount = entity.GetInt("FloorCount");
//						this.lblFloorCount.Text = FloorCount.ToString();

					this.txtIsArea.Value = entity.GetInt("IsArea").ToString();
					this.txtProjectCode.Value = entity.GetString("ProjectCode");
					this.txtParentCode.Value = entity.GetString("ParentCode");
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "该楼栋不存在"));
				}

				entity.Dispose();

				//每梯户数
				int DoorCount = BLL.ConvertRule.ToInt(this.txtDoorCount.Value);
				if (DoorCount <= 0) 
				{
					DoorCount = 1;
				}

				this.dlBuild.DataSource = NewTableDoor(DoorCount);
				this.dlBuild.DataBind();

				//楼层数
				DataTable tbFloor = (DataTable)Session["tbFloor"];

				int FloorCount = tbFloor.Rows.Count;
				for(int i=0;i<FloorCount;i++)
				{
					DataRow drFloor = tbFloor.Rows[i];

					string strList="";
					for(int k=0;k<DoorCount;k++)
					{
						strList=strList+"<td>&nbsp;</td>";
					}

					drFloor["No3"] = strList;
				}

				this.dlBuild1.DataSource = tbFloor;
				this.dlBuild1.DataBind();

			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示出错：" + ex.Message));
			}
		}

		private DataTable NewTableDoor(int DoorCount)
		{
			DataTable dt;

			if ((Session["tbDoor"] == null) || (this.txtAct.Value.ToLower() != "prev")) 
			{
				dt = new DataTable();	
				
				dt.Columns.Add(new DataColumn("DoorName", typeof(String)));
				dt.Columns.Add(new DataColumn("DoorCode", typeof(String)));
				dt.Columns.Add(new DataColumn("RoomCount", typeof(String)));
				
				DataRow dr = null;

				for(int i=0;i<DoorCount;i++)
				{
					dr = dt.NewRow();
					dr["DoorCode"]=i + 1;
					dr["RoomCount"] = 1;
					dt.Rows.Add(dr);
				}

				Session["tbDoor"] = dt;
			}
			else 
			{
				dt = (DataTable)Session["tbDoor"];
			}

			return dt;
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

			DataTable tbTemp = new DataTable();
			tbTemp.Columns.Add("ChamberName", typeof(string));

			int iCount = this.dlBuild.Items.Count;
			for(int i=0;i<iCount;i++) 
			{
				HtmlInputText txtDoorName = (HtmlInputText)this.dlBuild.Items[i].FindControl("txtDoorName");
//				HtmlInputText txtDoorCode = (HtmlInputText)this.dlBuild.Items[i].FindControl("txtDoorCode");
				HtmlInputText txtRoomCount = (HtmlInputText)this.dlBuild.Items[i].FindControl("txtRoomCount");

                /* 门牌号不用必录 
				if (txtDoorName.Value.Trim() == "") 
				{
					Hint = "请输入门牌号";
					return false;
				}
                */

				if (txtRoomCount.Value.Trim() == "") 
				{
					Hint = "请输入每梯户数";
					return false;
				}

//				if (txtDoorCode.Value.Trim() == "") 
//				{
//					Hint = "请输入单元号";
//					return false;
//				}

				if ( ! Rms.Check.StringCheck.IsNumber(txtRoomCount.Value))
				{
					Hint = "每梯户数必须是数值 ！ ";
					return false;
				}

				if (BLL.ConvertRule.ToInt(txtRoomCount.Value) <= 0)
				{
					Hint = "每梯户数必须大于0 ！ ";
					return false;
				}

				//本楼栋的门牌号不能重复
                if (txtDoorName.Value != "")
                {
                    if (tbTemp.Select("ChamberName='" + txtDoorName.Value + "'").Length > 0)
                    {
                        Hint = string.Format("门牌号（{0}）不能重复 ！ ", txtDoorName.Value);
                        return false;
                    }
                }

				//允许相同的门牌号
//				if (BLL.ProductRule.IsChamberNameExists(txtDoorName.Value, "", this.txtProjectCode.Value))
//				{
//					Hint = string.Format("相同的门牌号（{0}）已存在 ！ ", txtDoorName.Value);
//					return false;
//				}

				//记录门牌号
				DataRow drTemp = tbTemp.NewRow();
				drTemp["ChamberName"] = txtDoorName.Value;
				tbTemp.Rows.Add(drTemp);
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

				DataTable tb = (DataTable)Session["tbDoor"];

				int iCount = this.dlBuild.Items.Count;
				for(int i=0;i<iCount;i++) 
				{
					HtmlInputText txtDoorName = (HtmlInputText)this.dlBuild.Items[i].FindControl("txtDoorName");
//					HtmlInputText txtDoorCode = (HtmlInputText)this.dlBuild.Items[i].FindControl("txtDoorCode");
					HtmlInputText txtRoomCount = (HtmlInputText)this.dlBuild.Items[i].FindControl("txtRoomCount");

					DataRow dr = tb.Rows[i];
					dr["DoorName"] = txtDoorName.Value;
//					dr["DoorCode"] = txtDoorCode.Value;
					dr["RoomCount"] = txtRoomCount.Value;
				}

				Session["tbDoor"] = tb;

				//				EntityData entity = ProductDAO.GetBuildingByCode(buildingCode);
				//				dr = entity.CurrentRow;
				//				ProductDAO.UpdateBuilding(entity);
				//
				//				entity.Dispose();

				Response.Write(JavaScript.ScriptStart);
				Response.Write(string.Format("window.location.href='Building_Step4.aspx?BuildingCode={0}';", buildingCode));
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
