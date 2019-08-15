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
using RmsPM.DAL;
using RmsPM.BLL;
using Rms.Web;

namespace RmsPM.Web.PBS
{
	/// <summary>
	/// PBSTypeLayoutModify 的摘要说明。
	/// </summary>
	public partial class PBSTypeLayoutModify : PageBase
	{
	
		private int m_RowIndex = 0;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				LoadData();
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

			IniPage();
			CreateDataGrid();
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		private void IniPage() 
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadData() 
		{
			try 
			{
				EntityData entity = DAL.EntityDAO.ProjectDAO.GetProjectByCode(this.txtProjectCode.Value);
				if (entity.HasRecord()) 
				{
					DataRow dr = entity.CurrentRow;

					this.lblAfforestingRate.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(dr["AfforestingRate"]), "%");
					this.lblBuildingDensity.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(dr["BuildingDensity"]), "%");
					this.lblBuildingSpaceForVolumeRate.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(dr["BuildingSpaceForVolumeRate"]), "平米");
					this.lblBuildingSpaceNotVolumeRate.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(dr["BuildingSpaceNotVolumeRate"]), "平米");
					this.lblBuildSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(dr["BuildSpace"]), "平米");
					this.lblPlannedVolumeRate.Text = BLL.MathRule.GetDecimalShowString(dr["PlannedVolumeRate"]);
					this.lblTotalBuildingSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(dr["TotalBuildingSpace"]), "平米");
					this.lblTotalFloorSpace.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(dr["TotalFloorSpace"]), "平米");
				}
				entity.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}


		private void CreateDataGrid() 
		{
			try
			{
				EntityData entity = DAL.EntityDAO.PBSDAO.GetPBSTypeByProject(this.txtProjectCode.Value);

				HtmlTableRow row;
				HtmlTableCell cell;

				PBSTypeLayout.LayoutInfo info = new PBSTypeLayout.LayoutInfo();

				//第一层
				DataRow[] drs = entity.CurrentTable.Select("ParentCode=''");
				foreach(DataRow dr in drs) 
				{
					string code = dr["PBSTypeCode"].ToString();
					string name = dr["PBSTypeName"].ToString();

					//第二层
					DataRow[] drs2 = entity.CurrentTable.Select("ParentCode='" + code + "'");
					int iCount2 = drs2.Length;
					for(int i=0;i<iCount2;i++)
					{
						DataRow dr2 = drs2[i];

						string code2 = dr2["PBSTypeCode"].ToString();
						string name2 = dr2["PBSTypeName"].ToString();

						row = NewRow();

						//合并大类
						if (i == 0) 
						{
							cell = NewCell(row, name, "center", "cap", "");
							cell.RowSpan = iCount2;
							//							cell.RowSpan = iCount2 + 1;
						}

						NewCell(row, name2, "center", "cap", "");

						SetRow(row, info, "", code2);
						tbDtl.Rows.Add(row);
					}

					//第二层小计
					if (iCount2 > 0) 
					{
						row = NewRow();
						row.Style.Add("display", "none");
						NewCell(row, "小计", "center", "sum", "");

						SetRow(row, info, "sum", "");
						tbDtl.Rows.Add(row);
					}
				}

				//总计
				row = NewRow();
				row.Style.Add("display", "none");
				cell = NewCell(row, "总计", "center", "sum", "");
				cell.ColSpan = 2;
				SetRow(row, info, "sum", "");
				tbDtl.Rows.Add(row);

				entity.Dispose();
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
				EntityData entityLayout = DAL.EntityDAO.PBSDAO.GetPBSTypeLayoutByProject(this.txtProjectCode.Value);
				TableToScreen(entityLayout.CurrentTable);
				entityLayout.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private HtmlTableCell NewCell(HtmlTableRow row, string val, string align, string celltype, string id) 
		{
			HtmlTableCell cell = new HtmlTableCell();
			cell.Align = align;

			switch (celltype.ToLower()) 
			{
				case "cap":
					cell.Attributes.Add("class", "list-c");
					cell.InnerText = val;
					break;

				case "sum":
					cell.Attributes.Add("class", "sum-item");
					cell.InnerText = val;
					break;

				default:
					HtmlInputText txt = new HtmlInputText();
					cell.Controls.Add(txt);

					txt.ID = "txt" + id + m_RowIndex.ToString();
					txt.Name = "txt" + id + m_RowIndex.ToString();
					txt.Value = val;
					txt.Style.Add("width", "100%");
					txt.Style.Add("text-align", "right");
					txt.Attributes.Add("class", "input");
					txt.Attributes.Add("name", "txt" + id + m_RowIndex.ToString());

//					cell.Attributes.Add("class", "list-c");

//					if ((id == "VolumeRate") || (id == "SaleRate")) 
					if ((id == "SaleRate")) 
					{
						Label lbl = new Label();
						lbl.Text = "%";
						cell.Controls.Add(lbl);
						txt.Style["width"] = "80%";
					}

					//输入框事件
					switch (id) 
					{
						case "FloorSpace":
							txt.Attributes.Add("onblur", string.Format("CalcBuildingSpace(txtFloorSpace{0}, txtVolumeRate{0}, txtBuildingSpace{0})", m_RowIndex));
							break;

						case "VolumeRate":
							goto case "FloorSpace";

						case "BuildingSpace":
							txt.Attributes.Add("onblur", string.Format("CalcSaleArea(txtBuildingSpace{0}, txtSaleRate{0}, txtSaleArea{0})", m_RowIndex));
							break;

						case "SaleRate":
							goto case "BuildingSpace";

						case "SaleArea":
							txt.Attributes.Add("onblur", string.Format("CalcHouseCount(txtSaleArea{0}, txtHouseAreaAvg{0}, txtHouseCount{0})", m_RowIndex));
							break;

						case "HouseAreaAvg":
							goto case "SaleArea";

//						case "HouseCount":
//							txt.Attributes.Add("onblur", string.Format("CalcHouseAreaAvg(txtSaleArea{0}, txtHouseAreaAvg{0}, txtHouseCount{0})", m_RowIndex));
//							break;

					}

					break;
			}

			row.Cells.Add(cell);
			return cell;
		}

		private HtmlTableRow NewRow() 
		{
			HtmlTableRow row = new HtmlTableRow();
			row.Height = "30";
			return row;
		}

		private void SetRow(HtmlTableRow row, PBSTypeLayout.LayoutInfo info, string celltype, string PBSTypeCode) 
		{
			if (PBSTypeCode != "") 
			{
				m_RowIndex++;

				HtmlInputHidden txtPBSTypeCode = new HtmlInputHidden();
				txtPBSTypeCode.Name = "txtPBSTypeCode" + m_RowIndex.ToString();
				txtPBSTypeCode.ID = txtPBSTypeCode.Name;
				txtPBSTypeCode.Value = PBSTypeCode;
				row.Cells[0].Controls.Add(txtPBSTypeCode);
			}

			HtmlTableCell cell;

			cell = NewCell(row, BLL.MathRule.GetDecimalShowString(info.FloorSpace), "right", celltype, "FloorSpace");
			cell = NewCell(row, BLL.MathRule.GetDecimalShowString(info.VolumeRate), "right", celltype, "VolumeRate");
			cell = NewCell(row, BLL.MathRule.GetDecimalShowString(info.BuildingSpace), "right", celltype, "BuildingSpace");
			cell = NewCell(row, BLL.MathRule.GetDecimalShowString(info.SaleRate), "right", celltype, "SaleRate");
			cell = NewCell(row, BLL.MathRule.GetDecimalShowString(info.SaleArea), "right", celltype, "SaleArea");

			cell = NewCell(row, "", "right", "cap", "");
			cell.Style.Add("display", "none");

			cell = NewCell(row, BLL.MathRule.GetDecimalShowString(info.HouseAreaAvg), "right", celltype, "HouseAreaAvg");
			cell = NewCell(row, BLL.MathRule.GetIntShowString(info.HouseCount), "right", celltype, "HouseCount");
		}

//		private void ClearDataGrid() 
//		{
//			int count = this.tbDtl.Rows.Count;
//			for (int i=count-1;i>0;i--) 
//			{
//				this.tbDtl.Rows.Remove(this.tbDtl.Rows[i]);
//			}
//		}

		private void TableToScreen(DataTable tbLayout) 
		{
			try 
			{
				int count = this.tbDtl.Rows.Count;
				int RowIndex = 0;
				int StartCol = 0;

				PBSTypeLayout.LayoutInfo total = new PBSTypeLayout.LayoutInfo();
				HtmlTableRow row;

				for (int i=1;i<count;i++) 
				{
					RowIndex++;
					row = this.tbDtl.Rows[i];
				
					if (row.Cells.Count >= 10)
						StartCol = 1;
					else
						StartCol = 0;

					HtmlInputHidden txtPBSTypeCode = null;

					try 
					{
						txtPBSTypeCode = (HtmlInputHidden)row.Cells[0].Controls[1];//.FindControl("txtPBSTypeCode" + RowIndex.ToString());
					}
					catch 
					{
					}

					if ((txtPBSTypeCode != null) && (txtPBSTypeCode.Value.Trim() != ""))
					{
						HtmlInputText txtFloorSpace = (HtmlInputText)row.Cells[StartCol + 1].Controls[0];//.FindControl("txtFloorSpace" + RowIndex.ToString());
						HtmlInputText txtBuildingSpace = (HtmlInputText)row.Cells[StartCol + 3].Controls[0];//.FindControl("txtFloorSpace" + RowIndex.ToString());
						HtmlInputText txtVolumeRate = (HtmlInputText)row.Cells[StartCol + 2].Controls[0];//.FindControl("txtFloorSpace" + RowIndex.ToString());
						HtmlInputText txtSaleRate = (HtmlInputText)row.Cells[StartCol + 4].Controls[0];//.FindControl("txtFloorSpace" + RowIndex.ToString());
						HtmlInputText txtSaleArea = (HtmlInputText)row.Cells[StartCol + 5].Controls[0];//.FindControl("txtFloorSpace" + RowIndex.ToString());
						//					HtmlInputText txtFloorSpace = (HtmlInputText)row.Cells[StartCol + 6].Controls[0];//.FindControl("txtFloorSpace" + RowIndex.ToString());
						HtmlInputText txtHouseAreaAvg = (HtmlInputText)row.Cells[StartCol + 7].Controls[0];//.FindControl("txtFloorSpace" + RowIndex.ToString());
						HtmlInputText txtHouseCount = (HtmlInputText)row.Cells[StartCol + 8].Controls[0];//.FindControl("txtFloorSpace" + RowIndex.ToString());

						string PBSTypeCode = txtPBSTypeCode.Value.Trim();

						DataRow[] drsLayout = tbLayout.Select("PBSTypeCode='" + PBSTypeCode + "'");
						if (drsLayout.Length > 0) 
						{
							txtFloorSpace.Value = BLL.MathRule.GetDecimalShowString(drsLayout[0]["FloorSpace"]);
							txtBuildingSpace.Value = BLL.MathRule.GetDecimalShowString(drsLayout[0]["BuildingSpace"]);
							txtVolumeRate.Value = BLL.MathRule.GetDecimalShowString(drsLayout[0]["VolumeRate"]);
							txtSaleRate.Value = BLL.MathRule.GetDecimalShowString(drsLayout[0]["SaleRate"]);
							txtSaleArea.Value = BLL.MathRule.GetDecimalShowString(drsLayout[0]["SaleArea"]);
							txtHouseAreaAvg.Value = BLL.MathRule.GetDecimalShowString(drsLayout[0]["HouseAreaAvg"]);
							txtHouseCount.Value = BLL.ConvertRule.ToDecimal(drsLayout[0]["HouseCount"]).ToString("#");
						}
					}

				}
			}
			catch ( Exception ex )
			{
				throw ex;
			}
		}

		private DataTable ScreenToTable() 
		{
			try 
			{
				EntityData entity = new EntityData("PBSTypeLayout");
				DataTable tb = entity.CurrentTable;

				int count = this.tbDtl.Rows.Count;
				int RowIndex = 0;
				int StartCol = 0;
				DataRow dr;

				for (int i=1;i<count;i++) 
				{
					RowIndex++;
					HtmlTableRow row = this.tbDtl.Rows[i];
				
					if (row.Cells.Count >= 10)
						StartCol = 1;
					else
						StartCol = 0;

					HtmlInputHidden txtPBSTypeCode = null;

					try 
					{
						txtPBSTypeCode = (HtmlInputHidden)row.Cells[0].Controls[1];//.FindControl("txtPBSTypeCode" + RowIndex.ToString());
					}
					catch 
					{
					}

					if ((txtPBSTypeCode != null) && (txtPBSTypeCode.Value.Trim() != ""))
					{
						HtmlInputText txtFloorSpace = (HtmlInputText)row.Cells[StartCol + 1].Controls[0];//.FindControl("txtFloorSpace" + RowIndex.ToString());
						HtmlInputText txtBuildingSpace = (HtmlInputText)row.Cells[StartCol + 3].Controls[0];//.FindControl("txtFloorSpace" + RowIndex.ToString());
						HtmlInputText txtVolumeRate = (HtmlInputText)row.Cells[StartCol + 2].Controls[0];//.FindControl("txtFloorSpace" + RowIndex.ToString());
						HtmlInputText txtSaleRate = (HtmlInputText)row.Cells[StartCol + 4].Controls[0];//.FindControl("txtFloorSpace" + RowIndex.ToString());
						HtmlInputText txtSaleArea = (HtmlInputText)row.Cells[StartCol + 5].Controls[0];//.FindControl("txtFloorSpace" + RowIndex.ToString());
						//					HtmlInputText txtFloorSpace = (HtmlInputText)row.Cells[StartCol + 6].Controls[0];//.FindControl("txtFloorSpace" + RowIndex.ToString());
						HtmlInputText txtHouseAreaAvg = (HtmlInputText)row.Cells[StartCol + 7].Controls[0];//.FindControl("txtFloorSpace" + RowIndex.ToString());
						HtmlInputText txtHouseCount = (HtmlInputText)row.Cells[StartCol + 8].Controls[0];//.FindControl("txtFloorSpace" + RowIndex.ToString());

						string PBSTypeCode = txtPBSTypeCode.Value.Trim();

						dr = entity.CurrentTable.NewRow();
						dr["SystemID"] = PBSTypeCode;
						dr["PBSTypeCode"] = PBSTypeCode;
						dr["ProjectCode"] = this.txtProjectCode.Value;
						SetFieldValue(dr, "FloorSpace", BLL.ConvertRule.ToDecimal(txtFloorSpace.Value.Trim()));
						SetFieldValue(dr, "BuildingSpace", BLL.ConvertRule.ToDecimal(txtBuildingSpace.Value.Trim()));
						SetFieldValue(dr, "VolumeRate", BLL.ConvertRule.ToDecimal(txtVolumeRate.Value.Trim()));
						SetFieldValue(dr, "SaleRate", BLL.ConvertRule.ToDecimal(txtSaleRate.Value.Trim()));
						SetFieldValue(dr, "SaleArea", BLL.ConvertRule.ToDecimal(txtSaleArea.Value.Trim()));
						SetFieldValue(dr, "HouseAreaAvg", BLL.ConvertRule.ToDecimal(txtHouseAreaAvg.Value.Trim()));
						SetFieldValue(dr, "HouseCount", BLL.ConvertRule.ToInt(txtHouseCount.Value.Trim()));

						entity.CurrentTable.Rows.Add(dr);
					}

				}

				entity.Dispose();
				return tb;
			}
			catch (Exception ex) 
			{
				throw ex;
			}
		}

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				string ProjectCode = this.txtProjectCode.Value;
				DataTable tbTemp = ScreenToTable();
				int count = tbTemp.Rows.Count;
				DataRow dr;

				for (int i=0;i<count;i++) 
				{
					DataRow drTemp = tbTemp.Rows[i];
					string code = drTemp["PBSTypeCode"].ToString();

					EntityData entity = DAL.EntityDAO.PBSDAO.GetPBSTypeLayoutByProjectPBSType(ProjectCode, code);
					if (entity.HasRecord()) 
					{
						dr = entity.CurrentTable.Rows[0];
					}
					else 
					{
						dr = entity.CurrentTable.NewRow();
						dr["SystemID"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PBSTypeLayoutSystemID");
						dr["PBSTypeCode"] = code;
						dr["ProjectCode"] = this.txtProjectCode.Value;
					}

					dr["FloorSpace"] = drTemp["FloorSpace"];
					dr["BuildingSpace"] = drTemp["BuildingSpace"];
					dr["VolumeRate"] = drTemp["VolumeRate"];
					dr["SaleRate"] = drTemp["SaleRate"];
					dr["SaleArea"] = drTemp["SaleArea"];
					dr["HouseAreaAvg"] = drTemp["HouseAreaAvg"];
					dr["HouseCount"] = drTemp["HouseCount"];

					if (entity.HasRecord()) 
					{
						DAL.EntityDAO.PBSDAO.UpdatePBSTypeLayout(entity);
					}
					else 
					{
						entity.CurrentTable.Rows.Add(dr);
						DAL.EntityDAO.PBSDAO.InsertPBSTypeLayout(entity);
					}

					entity.Dispose();
				}

				GoBack();
			}
			catch (Exception ex) 
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "保存失败：" + ex.Message));
			}
		}

		private void SetFieldValue(DataRow dr, string field, object val) 
		{
			if (val == null) 
			{
				dr[field] = DBNull.Value;
			}
			else 
			{
				dr[field] = val;
			}
		}

		/// <summary>
		/// 返回
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write(string.Format(" window.location.href = '{0}';", this.txtFromUrl.Value));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
			Response.End();
		}

		/// <summary>
		/// 汇总更新
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnRead_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				DataTable tbBuilding = BuildingTotalGroupByPBSTypeCode(this.txtProjectCode.Value);
				if (tbBuilding.Rows.Count == 0) 
				{
					return;
				}

				DataTable tbLayout = ScreenToTable();

				int count = tbLayout.Rows.Count - 1;

				for (int i=0;i<count;i++) 
				{
					DataRow drLayout = tbLayout.Rows[i];
					string PBSTypeCode = drLayout["PBSTypeCode"].ToString();
					string PBSTypeName = BLL.PBSRule.GetPBSTypeName(PBSTypeCode);

					PBSTypeLayout.LayoutInfo info = new PBSTypeLayout.LayoutInfo();
					info.SetInfo(drLayout);

					DataRow[] drs = tbBuilding.Select("PBSTypeCode='" + PBSTypeCode + "'");
					if (drs.Length > 0) 
					{
						info.BuildingSpace = BLL.ConvertRule.ToDecimal(drs[0]["Area"].ToString());
						info.HouseCount = BLL.ConvertRule.ToDecimal(drs[0]["HouseCount"].ToString());

						info.CalcVolumeRate();
						info.CalcSaleArea();
						info.CalcHouseAreaAvg();

					}
					else 
					{
						info.BuildingSpace = 0;
						info.HouseCount = 0;
					}

					info.SaveToDataRow(drLayout);

				}

				TableToScreen(tbLayout);
			}
			catch (Exception ex) 
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "汇总更新失败：" + ex.Message));
			}
		}

		private DataTable BuildingTotalGroupByPBSTypeCode(string ProjectCode) 
		{
			try
			{
				QueryAgent qa = new Rms.ORMap.QueryAgent();

				try 
				{
					DataSet ds = qa.ExecSqlForDataSet("select PBSTypeCode, sum(isnull(Area, 0)) as Area"
						+ ", sum(isnull(House1Count, 0) + isnull(House2Count, 0) + isnull(House3Count, 0) + isnull(House4Count, 0) + isnull(House5Count, 0)) as HouseCount"
						+ " from V_Building where ProjectCode = '" + ProjectCode + "' group by PBSTypeCode");

					return ds.Tables[0];
				}
				finally
				{
					qa.Dispose();
				}
			}
			catch ( Exception ex )
			{
				throw ex;
			}

		}

		protected void btnRollback_ServerClick(object sender, System.EventArgs e)
		{
			LoadDataGrid();
		}
	}
}
