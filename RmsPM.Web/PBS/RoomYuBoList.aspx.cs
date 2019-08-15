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
using RmsPM.DAL.QueryStrategy;
using RmsPM.BLL;
using Rms.Web;

namespace RmsPM.Web.PBS
{
	/// <summary>
	/// RoomYuBoList 的摘要说明。
	/// </summary>
	public partial class RoomYuBoList : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
				LoadDataGrid();
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
			this.dgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgList_PageIndexChanged);

		}
		#endregion

		private void IniPage() 
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtIOType.Value = Request.QueryString["IOType"];

				RmsPM.BLL.PageFacade.LoadPBSTypeSelectFirstLevel(this.sltSearchCodeName,"");
//				RmsPM.BLL.PageFacade.LoadDictionarySelect(this.sltSearchOutAspect,"去向","");

				string title = "";
				string outstate = "";

				outstate = this.txtIOType.Value;
//				outstate = BLL.ProductRule.TransTempRoomOutIOType(this.txtIOType.Value, true);

				if (outstate == "") 
				{
					throw new Exception("参数“出入库类型”不能为空");
				}

				switch (outstate) 
				{
					case "1":
						outstate = "入库";
						break;

					case "2":
						outstate = "出库";
						break;

					case "3":
						outstate = "退库";
						break;

					case "4":
						outstate = "预拨";
						break;

//					default:
//						throw new Exception("未知的出入库类型");
				}

				title = outstate + "单";
				this.spanTitle.InnerText = title;
				this.txtOutState.Value = outstate;

				this.dgList.Columns[3].HeaderText = this.txtOutState.Value + "日期";
				this.spanOutDate.InnerText = this.txtOutState.Value;

				//隐藏“去向”
				if ((this.txtOutState.Value != "出库") && (this.txtOutState.Value != "预拨") && (this.txtOutState.Value != "调拨") )
				{
					this.spanOutAspect.Style["display"] = "none";
					this.spanOutAspect2.Style["display"] = "none";
					this.dgList.Columns[4].Visible = false;
				}

				//权限
				this.btnAdd.Visible = base.user.HasRight("011102");
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadDataGrid() 
		{
			try 
			{
				TempRoomOutStrategyBuilder sb = new TempRoomOutStrategyBuilder("SelectAllIncludeBuildDtl");

				sb.AddStrategy( new Strategy(  TempRoomOutStrategyName.Out_State,this.txtOutState.Value ));

				string ProjectCode = this.txtProjectCode.Value;
				if (ProjectCode != "")
					sb.AddStrategy( new Strategy( TempRoomOutStrategyName.ProjectCode, ProjectCode));

				if (this.txtSearchCurYear.Value != "")
					sb.AddStrategy( new Strategy( TempRoomOutStrategyName.CurYear, this.txtSearchCurYear.Value));

				if ( this.txtSearchOutDateBegin.Value != "" || this.txtSearchOutDateEnd.Value != "" )
				{
					ArrayList ar = new ArrayList();
					ar.Add(this.txtSearchOutDateBegin.Value);
					ar.Add(this.txtSearchOutDateEnd.Value);
					sb.AddStrategy( new Strategy( TempRoomOutStrategyName.OutDateRange,ar ));
				}

				if (this.sltSearchCheckState.Value.Trim() != "")
					sb.AddStrategy( new Strategy( TempRoomOutStrategyName.CheckState, this.sltSearchCheckState.Value));

				if (this.sltSearchCodeName.Value.Trim() != "")
					sb.AddStrategy( new Strategy( TempRoomOutStrategyName.CodeName, this.sltSearchCodeName.Value));

				if (this.txtSearchOutAspect.Value.Trim() != "")
					sb.AddStrategy( new Strategy( TempRoomOutStrategyName.OutAspect, this.txtSearchOutAspect.Value));

				string BuildingName = this.txtSearchBuildingName.Value;
				if (BuildingName != "")
					sb.AddStrategy(new Strategy(TempRoomOutStrategyName.InBuildingName, BuildingName));

				string ChamberName = this.txtSearchChamberName.Value;
				if (ChamberName != "")
					sb.AddStrategy(new Strategy(TempRoomOutStrategyName.InChamberName, ChamberName));

				string RoomName = this.txtSearchRoomName.Value;
				if (RoomName != "")
					sb.AddStrategy(new Strategy(TempRoomOutStrategyName.InRoomName, RoomName));

				sb.AddOrder("CodeName",true);
				sb.AddOrder("CurYear",false);
				sb.AddOrder("SumNo",false);

				string sql = sb.BuildMainQueryString();

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "TempRoomOut",sql );
				qa.Dispose();

				dgList.DataSource = entity;
				dgList.DataBind();
				entity.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void dgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.dgList.CurrentPageIndex = e.NewPageIndex;
			LoadDataGrid();
		}

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			LoadDataGrid();
		}
	}
}
