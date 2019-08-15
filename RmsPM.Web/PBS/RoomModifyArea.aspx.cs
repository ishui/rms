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
	/// RoomModifyArea 的摘要说明。
	/// </summary>
	public partial class RoomModifyArea : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();

				if (this.txtAct.Value.ToLower() == "building") 
				{
					LoadDataGridFromBuilding(this.txtBuildingCode.Value);
				}
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
			this.dgList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemDataBound);

		}
		#endregion

		private void IniPage() 
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtAct.Value = Request.QueryString["Action"];
				this.txtReturnScript.Value = Request.QueryString["ReturnScript"];
				this.txtBuildingCode.Value = Request.QueryString["BuildingCode"];

				if (this.txtBuildingCode.Value != "") 
				{
					this.txtProjectCode.Value = BLL.ProductRule.GetProjectCodeFromBuilding(this.txtBuildingCode.Value);
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadDataGridFromSelected() 
		{
			try 
			{
				string select = this.txtSelectRoomCode.Value.Trim();
				string[] arr = select.Split(",".ToCharArray());

				EntityData entity = new EntityData("V_ROOM");
				DataRow dr;

				//复制选择的记录
				foreach(string RoomCode in arr) 
				{
					EntityData entityR = DAL.EntityDAO.ProductDAO.GetV_ROOMByCode(RoomCode);

					if (entityR.HasRecord()) 
					{
						DataRow drR = entityR.CurrentRow;
						dr = entity.CurrentTable.NewRow();

						foreach(DataColumn col in entity.CurrentTable.Columns) 
						{
							if (entityR.CurrentTable.Columns.Contains(col.ColumnName))
							{
								dr[col.ColumnName] = drR[col.ColumnName];
							}
						}

						entity.CurrentTable.Rows.Add(dr);
					}

					entityR.Dispose();
				}

				DataTable tbBuilding = BLL.ConvertRule.GetDistinct(entity.CurrentTable, "BuildingCode", "");
				foreach(DataRow drBuilding in tbBuilding.Rows) 
				{
					string BuildingCode = drBuilding["BuildingCode"].ToString();
					string hint = BLL.ProductRule.CanModifyRoomArea(BuildingCode);
					if (hint != "") 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, hint));
						Response.Write(Rms.Web.JavaScript.WinClose(true));
						return;
					}
				}

				BindDataGrid(entity.CurrentTable);
				entity.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadDataGridFromBuilding(string BuildingCode) 
		{
			try 
			{
				//检查是否可修改
				string hint = BLL.ProductRule.CanModifyRoomArea(BuildingCode);
				if (hint != "") 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, hint));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
					return;
				}

				RoomStrategyBuilder sb = new RoomStrategyBuilder("V_ROOM");

				sb.AddStrategy( new Strategy( RoomStrategyName.ProjectCode, this.txtProjectCode.Value));
				sb.AddStrategy( new Strategy( RoomStrategyName.BuildingCode, BuildingCode));

				sb.AddOrder("BuildingName", true);
				sb.AddOrder("ChamberCode", true);
				sb.AddOrder("FloorIndex", true);
				sb.AddOrder("RoomName", true);

				string sql = sb.BuildMainQueryString();

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "V_ROOM",sql );
				qa.Dispose();

				BindDataGrid(entity.CurrentTable);
				entity.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void BindDataGrid(DataTable tb)
		{
			try 
			{
				string[] arrField = {"PreBuildArea", "PreRoomArea", "BuildArea", "RoomArea"};
				decimal[] arrSum = BLL.MathRule.SumColumn(tb, arrField);

				this.txtDetailCount.Value = tb.Rows.Count.ToString();
				this.txtSumPreBuildArea.Value = BLL.StringRule.BuildShowNumberString(arrSum[0]);
				this.txtSumPreRoomArea.Value = BLL.StringRule.BuildShowNumberString(arrSum[1]);
				this.txtSumBuildArea.Value = BLL.StringRule.BuildShowNumberString(arrSum[2]);
				this.txtSumRoomArea.Value = BLL.StringRule.BuildShowNumberString(arrSum[3]);

				dgList.DataSource = tb;
				dgList.DataBind();
			}
			catch (Exception ex) 
			{
				throw ex;
			}
		}

		protected void btnHiddenPost_ServerClick(object sender, System.EventArgs e)
		{
			LoadDataGridFromSelected();
		}

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				DataTable tbBuilding = new DataTable();
				tbBuilding.Columns.Add("BuildingCode", typeof(string));

				for(int i=0;i<this.dgList.Items.Count;i++)
				{
					HtmlInputText txtBuildArea = (HtmlInputText)dgList.Items[i].FindControl("txtBuildArea");
					HtmlInputText txtRoomArea = (HtmlInputText)dgList.Items[i].FindControl("txtRoomArea");
					HtmlInputText txtPreBuildArea = (HtmlInputText)dgList.Items[i].FindControl("txtPreBuildArea");
					HtmlInputText txtPreRoomArea = (HtmlInputText)dgList.Items[i].FindControl("txtPreRoomArea");

					if ( txtBuildArea != null )
					{
						string RoomCode = this.dgList.DataKeys[i].ToString();

						EntityData entity = DAL.EntityDAO.ProductDAO.GetRoomByCode(RoomCode);
						DataRow dr;

						if (entity.HasRecord())
						{
							dr = entity.CurrentRow;
						
							dr["BuildArea"] = BLL.ConvertRule.ToDecimalObj(txtBuildArea.Value);
							dr["RoomArea"] = BLL.ConvertRule.ToDecimalObj(txtRoomArea.Value);
							dr["PreBuildArea"] = BLL.ConvertRule.ToDecimalObj(txtPreBuildArea.Value);
							dr["PreRoomArea"] = BLL.ConvertRule.ToDecimalObj(txtPreRoomArea.Value);

							DAL.EntityDAO.ProductDAO.UpdateRoom(entity);							

							//记录楼栋编号
							string BuildingCode = BLL.ConvertRule.ToString(dr["BuildingCode"]);
							if (tbBuilding.Select("BuildingCode='" + BuildingCode + "'").Length == 0) 
							{
								DataRow drBuilding = tbBuilding.NewRow();
								drBuilding["BuildingCode"] = BuildingCode;
								tbBuilding.Rows.Add(drBuilding);
							}
						}

						entity.Dispose();
					}
					
				}

				//更新楼栋的实测面积
				BLL.ProductRule.UpdateBuildingTotalRoomArea(tbBuilding);
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "保存失败：" + ex.Message));
				return;
			}

			GoBack();
		}

		/// <summary>
		/// 返回
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);

			if (this.txtReturnScript.Value == "") 
			{
				Response.Write("window.opener.location = window.opener.location;");
			}
			else 
			{
				Response.Write("window.opener." + this.txtReturnScript.Value);
			}

			Response.Write(Rms.Web.JavaScript.WinClose(false));
			//			string FromUrl = this.txtFromUrl.Value.Trim();
			//			if (FromUrl != "") 
			//			{
			//				Response.Write(string.Format("window.location = '{0}';", FromUrl));
			//			}
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Footer) 
			{
				//显示合计金额
				((Label)e.Item.FindControl("lblSumPreBuildArea")).Text = this.txtSumPreBuildArea.Value;
				((Label)e.Item.FindControl("lblSumPreRoomArea")).Text = this.txtSumPreRoomArea.Value;
				((Label)e.Item.FindControl("lblSumBuildArea")).Text = this.txtSumBuildArea.Value;
				((Label)e.Item.FindControl("lblSumRoomArea")).Text = this.txtSumRoomArea.Value;
			}
		}

	}
}
