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
	/// PBSTypeLayout 的摘要说明。
	/// </summary>
	public partial class PBSTypeLayout : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
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
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		public class LayoutInfo
		{
			public decimal FloorSpace = 0;
			public decimal BuildingSpace = 0;
			public decimal VolumeRate = 0;
			public decimal SaleRate = 0;
			public decimal SaleArea = 0;
			public decimal HouseCount = 0;
			public decimal HouseAreaAvg = 0;

			public LayoutInfo() 
			{
			}

			public void Plus(LayoutInfo info) 
			{
				FloorSpace += info.FloorSpace;
				BuildingSpace += info.BuildingSpace;
				VolumeRate = 0;
				SaleRate = 0;
				SaleArea += info.SaleArea;
				HouseCount += info.HouseCount;
				HouseAreaAvg = 0;
			}

			/// <summary>
			/// 行 -> 类
			/// </summary>
			/// <param name="dr"></param>
			public void SetInfo(DataRow dr) 
			{
				FloorSpace = BLL.ConvertRule.ToDecimal(dr["FloorSpace"]);
				BuildingSpace = BLL.ConvertRule.ToDecimal(dr["BuildingSpace"]);
				VolumeRate = BLL.ConvertRule.ToDecimal(dr["VolumeRate"]);
				SaleRate = BLL.ConvertRule.ToDecimal(dr["SaleRate"]);
				SaleArea = BLL.ConvertRule.ToDecimal(dr["SaleArea"]);
				HouseCount = BLL.ConvertRule.ToInt(dr["HouseCount"]);
				HouseAreaAvg = BLL.ConvertRule.ToDecimal(dr["HouseAreaAvg"]);
			}

			/// <summary>
			/// 类 -> 行
			/// </summary>
			/// <param name="dr"></param>
			public void SaveToDataRow(DataRow dr) 
			{
				dr["FloorSpace"] = FloorSpace;
				dr["BuildingSpace"] = BuildingSpace;
				dr["VolumeRate"] = VolumeRate;
				dr["SaleRate"] = SaleRate;
				dr["SaleArea"] = SaleArea;
				dr["HouseCount"] = HouseCount;
				dr["HouseAreaAvg"] = HouseAreaAvg;
			}

			public decimal CalcSaleArea() 
			{
				//可售面积 = 建筑面积 *　可售率
				SaleArea = Math.Round(BuildingSpace * SaleRate / 100, 4);
				return SaleArea;
			}

			public decimal CalcHouseAreaAvg() 
			{
				//平均每户面积 = 可售面积 /　总户数
				if (HouseCount != 0) 
				{
					HouseAreaAvg = Math.Round(SaleArea / HouseCount, 4);
				}
				else 
				{
					HouseAreaAvg = 0;
				}

				return HouseAreaAvg;
			}

			public decimal CalcVolumeRate() 
			{
				//容积率 = 建筑面积 /　占地面积
				if (FloorSpace != 0) 
				{
					VolumeRate = Math.Round(BuildingSpace / FloorSpace * 100, 2);
				}
				else 
				{
					VolumeRate = 0;
				}

				return VolumeRate;
			}

		}

		private void IniPage() 
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];

				//权限
				this.btnModify.Visible = base.user.HasRight("010203");
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

		private void LoadDataGrid() 
		{
			try 
			{
				string ProjectCode = this.txtProjectCode.Value;
				EntityData entity = DAL.EntityDAO.PBSDAO.GetPBSTypeByProject(ProjectCode);
//				string CHref = "javascript:ShowPBSType('{0}');";

				LayoutInfo total = new LayoutInfo();
				HtmlTableRow row;
				HtmlTableCell cell;

				//第一层
				DataRow[] drs = entity.CurrentTable.Select("ParentCode=''");
				foreach(DataRow dr in drs) 
				{
					string code = dr["PBSTypeCode"].ToString();
					string name = dr["PBSTypeName"].ToString();

					string href = "";
//					string href = string.Format(CHref, code);

					LayoutInfo total2 = new LayoutInfo();

					//第二层
					DataRow[] drs2 = entity.CurrentTable.Select("ParentCode='" + code + "'");
					int iCount2 = drs2.Length;
					for(int i=0;i<iCount2;i++)
					{
						DataRow dr2 = drs2[i];

						string code2 = dr2["PBSTypeCode"].ToString();
						string name2 = dr2["PBSTypeName"].ToString();

						string href2 = "";
//						string href2 = string.Format(CHref, code2);

						row = NewRow();

						//合并大类
						if (i == 0) 
						{
							cell = NewCell(row, name, "center", true, false, href);
							cell.RowSpan = iCount2 + 1;
						}

						NewCell(row, name2, "center", true, false, href2);

						//取产品的规划信息
						LayoutInfo dtl = new LayoutInfo();

						EntityData entityLayout = DAL.EntityDAO.PBSDAO.GetPBSTypeLayoutByProjectPBSType(ProjectCode, code2);
						if (entityLayout.HasRecord()) 
						{
							dtl.FloorSpace = entityLayout.GetDecimal("FloorSpace");
							dtl.BuildingSpace = entityLayout.GetDecimal("BuildingSpace");
							dtl.VolumeRate = entityLayout.GetDecimal("VolumeRate");
							dtl.SaleRate = entityLayout.GetDecimal("SaleRate");
							dtl.SaleArea = entityLayout.GetDecimal("SaleArea");
							dtl.HouseCount = entityLayout.GetDecimal("HouseCount");
							dtl.HouseAreaAvg = entityLayout.GetDecimal("HouseAreaAvg");

							total2.Plus(dtl);
						}
						entityLayout.Dispose();

						SetRow(row, dtl, false);

						tbDtl.Rows.Add(row);
					}

					//计算产品比例
					SetAreaPercent(this.tbDtl.Rows.Count - iCount2, iCount2, total2);

					//第二层空时插一条空的小计
					if (iCount2 == 0) 
					{
						row = NewRow();

						cell = NewCell(row, name, "center", true, false, href);
						cell.RowSpan = iCount2 + 1;

						NewCell(row, "小计", "center", true, true, "");

						LayoutInfo dtl = new LayoutInfo();
						SetRow(row, dtl, true);

						tbDtl.Rows.Add(row);
					}

					//第二层小计
					if (iCount2 > 0) 
					{
						row = NewRow();
						row.Attributes.Add("class", "sum");
						NewCell(row, "小计", "center", true, true, "");

						SetRow(row, total2, true);
						tbDtl.Rows.Add(row);

						total.Plus(total2);
					}
				}

				//总计
				row = NewRow();
				row.Attributes.Add("class", "sum");
				cell = NewCell(row, "总计", "center", true, true, "");
				cell.ColSpan = 2;
				SetRow(row, total, true);
				tbDtl.Rows.Add(row);

				entity.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 计算并显示产品比例
		/// </summary>
		/// <param name="firstrow"></param>
		/// <param name="rowcount"></param>
		/// <param name="total"></param>
		private void SetAreaPercent(int firstrow, int rowcount, LayoutInfo total) 
		{
			if (total.BuildingSpace == 0) 
			{
				return;
			}

			int StartCol = 0;
			string s;
			HtmlTableRow row;
			HtmlTableCell cell;

			for (int i=0;i<rowcount;i++) 
			{
				row = this.tbDtl.Rows[firstrow + i];

				if (row.Cells.Count >= 10)
					StartCol = 1;
				else
					StartCol = 0;

				decimal val = 0;
				try 
				{
					val = decimal.Parse(row.Cells[StartCol + 3].InnerText);
				}
				catch 
				{
				}

				decimal per = 0;
				try 
				{
					per = Math.Round(val / total.BuildingSpace, 4) * 100;
				}
				catch 
				{
				}

				s = per.ToString("0.####");
				if (s != "") 
				{
					s = s + "%";
				}

				cell = row.Cells[StartCol + 6];
				cell.InnerText = s;

				if (cell.InnerText == "") 
				{
					cell.InnerHtml = "&nbsp;";
				}

			}
		}

		private HtmlTableCell NewCell(HtmlTableRow row, string val, string align, bool iscap, bool issum, string href) 
		{
			HtmlTableCell cell = new HtmlTableCell();

			if (href.Length > 0) 
			{
				cell.InnerHtml = string.Format("<a href=\"{0}\">{1}</a>", href, val);
			}
			else 
			{
				cell.InnerHtml = val;
			}

			if (cell.InnerHtml == "") 
			{
				cell.InnerHtml = "&nbsp;";
			}

			cell.Align = align;

			if (iscap) 
			{
				if (issum) 
				{
					cell.Attributes.Add("class", "sum-item");
				}
				else 
				{
					cell.Attributes.Add("class", "list-c");
				}
			}
			else 
			{
				if (issum) 
				{
					cell.Attributes.Add("class", "sum");
				}
				else 
				{
//					cell.Attributes.Add("class", "");
				}
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

		private void SetRow(HtmlTableRow row, LayoutInfo info, bool issum) 
		{
			HtmlTableCell cell;
			cell = NewCell(row, BLL.MathRule.GetDecimalShowString(info.FloorSpace), "right", false, issum, "");

			if (issum)
				cell = NewCell(row, "", "right", false, issum, "");
			else
				cell = NewCell(row, BLL.MathRule.GetDecimalShowString(info.VolumeRate), "right", false, issum, "");

			cell = NewCell(row, BLL.MathRule.GetDecimalShowString(info.BuildingSpace), "right", false, issum, "");

			if (issum)
				cell = NewCell(row, "", "right", false, issum, "");
			else
				cell = NewCell(row, BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(info.SaleRate), "%"), "right", false, issum, "");

			cell = NewCell(row, BLL.MathRule.GetDecimalShowString(info.SaleArea), "right", false, issum, "");
			cell = NewCell(row, "", "right", false, issum, "");

			if (issum)
				cell = NewCell(row, "", "right", false, issum, "");
			else
				cell = NewCell(row, BLL.MathRule.GetDecimalShowString(info.HouseAreaAvg), "right", false, issum, "");

			cell = NewCell(row, BLL.MathRule.GetIntShowString(info.HouseCount), "right", false, issum, "");
		}

		protected void btnPBSTypeImport_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				BLL.PBSRule.PBSTypeImport(this.txtProjectCode.Value);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "产品组合从模板导入出错：" + ex.Message));
				return;
			}

			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write(Rms.Web.JavaScript.Alert(false, "产品组合从模板导入成功"));
			Response.Write("window.location.href = window.location.href;");
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		protected void btnPBSTypeExport_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				BLL.PBSRule.PBSTypeExport(this.txtProjectCode.Value);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "产品组合导出成模板出错：" + ex.Message));
				return;
			}

			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write(Rms.Web.JavaScript.Alert(false, "产品组合导出成模板成功"));
			Response.Write("window.location.href = window.location.href;");
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		protected void btnPBSTypeImportAllProject_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				BLL.PBSRule.PBSTypeImportAllProject();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "所有项目的产品组合从模板导入出错：" + ex.Message));
				return;
			}

			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write(Rms.Web.JavaScript.Alert(false, "所有项目的产品组合从模板导入成功"));
			Response.Write("window.location.href = window.location.href;");
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

	}
}
