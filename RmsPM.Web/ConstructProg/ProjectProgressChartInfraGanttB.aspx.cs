using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using Rms.ORMap;
using RmsPM.DAL.QueryStrategy;
using RmsPM.BLL;
using Rms.Web;
using Infragistics.WebUI.UltraWebChart;
using Infragistics.UltraChart.Core;
using Infragistics.UltraChart.Core.Layers;

namespace RmsPM.Web.ConstructProg
{
	/// <summary>
	/// ProjectProgressChartInfraGanttB
	/// </summary>
	public partial class ProjectProgressChartInfraGanttB : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Repeater dgLabelY;

		//		protected decimal MonthPixel = 50;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				BLL.FileIO.CreateDir(Server.MapPath(this.UltraChart1.ChartImagesPath));

				IniPage();
				LoadChart();
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
		///		设计器支持所需的方法 - 不要使用代码编辑器
		///		修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.UltraChart1.ChartDrawItem += new Infragistics.UltraChart.Shared.Events.ChartDrawItemEventHandler(this.UltraChart1_ChartDrawItem);

		}
		#endregion

		private void IniPage() 
		{
			try
			{
				this.txtWBSCode.Value = Request.QueryString["WBSCode"];
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"初始化页面失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面失败：" + ex.Message));
			}
		}

		private static void InitEmptyGanttData(DataTable tb, string StartField, string EndField)
		{
			if (tb.Rows.Count > 0)
			{
				tb.Rows[0][StartField] = DateTime.Parse("1900-1-1");
				tb.Rows[0][EndField] = tb.Rows[0][StartField];
			}
		}

		private static bool IsGanttDataEmpty(DataTable tb, string StartField, string EndField)
		{
			try 
			{
				bool r = true;

				foreach(DataRow dr in tb.Rows)
				{
					if ((dr[StartField] != DBNull.Value) && (dr[StartField].ToString() != "")
						&& (dr[EndField] != DBNull.Value) && (dr[EndField].ToString() != "")
						)
					{
						r = false;
						return r;
					}
				}

				return r;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		private DataTable IniMyChartTable()
		{
			try 
			{
				if (Session["tbMyChart"] != null)
				{
					((DataTable)Session["tbMyChart"]).Dispose();
				}

				DataTable tb = new DataTable();
				tb.Columns.Add("sno", typeof(int));
				tb.Columns.Add("row", typeof(int));
				tb.Columns.Add("col", typeof(int));
				tb.Columns.Add("x", typeof(int));
				tb.Columns.Add("y", typeof(int));

				Session["tbMyChart"] = tb;

				return tb;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		private void WriteMyChartScript()
		{
			try 
			{
				if (Session["tbMyChart"] == null) return;

				DataTable tb = (DataTable)Session["tbMyChart"];

				if (tb.Rows.Count <= 0) return;

				string script = Rms.Web.JavaScript.ScriptStart;
				script += "function MyChartClientEvent()" + "\n";
				script += "{" + "\n";
				script += "var chart = document.all(\"RxActImg_UltraChart1\");" + "\n";
				script += "var objItem;" + "\n";

				foreach(DataRow dr in tb.Rows)
				{
					int row = BLL.ConvertRule.ToInt(dr["row"]);
					int col = BLL.ConvertRule.ToInt(dr["col"]);
					int x = BLL.ConvertRule.ToInt(dr["x"]);
					int y = BLL.ConvertRule.ToInt(dr["y"]);

					string coords = string.Format("{0},{1},{2},{3}", x, y, x+10, y+10);

					script += "\n";
					script += "objItem=document.createElement('area');" + "\n";
					script += string.Format("objItem.setAttribute('id', 'MyChartShape_r{0}c{1}', 0);", row, col) + "\n";
					script += "objItem.setAttribute('shape', 'rect', 0);" + "\n";
					script += "objItem.setAttribute('coords','" + coords + "',0);" + "\n";
					script += "objItem.setAttribute('row', '" + row + "', 0);" + "\n";
					script += "objItem.setAttribute('col', '" + col + "', 0);" + "\n";

					script += "objItem.attachEvent('onmouseover', MyChartMouseOver);" + "\n";
					script += "objItem.attachEvent('onmouseout', MyChartMouseOut);" + "\n";
					script += "objItem.attachEvent('onclick', MyChartClick);" + "\n";

					script += "chart.appendChild(objItem);" + "\n";

				}

				script += "}" + "\n";
				script += "MyChartClientEvent();" + "\n";
				script += Rms.Web.JavaScript.ScriptEnd;

				//客户端事件
				Page.RegisterStartupScript("MyChartClientEvent", script);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		private void SetChartDataPoint(UltraChart chart, DataTable tb, string StartField, string EndField, string RowField, string ColumnField)
		{
			try 
			{
				DataTable tbMy = IniMyChartTable();
				int sno = 0;

				foreach(DataRow dr in tb.Rows)
				{
					string start = BLL.ConvertRule.ToDateString(dr[StartField], "yyyy-MM-dd");
					string end = BLL.ConvertRule.ToDateString(dr[EndField], "yyyy-MM-dd");

					if ((start == end) && (start != ""))
					{
						Infragistics.UltraChart.Resources.Appearance.EllipseAnnotation ann = new Infragistics.UltraChart.Resources.Appearance.EllipseAnnotation();

						int row = (int)dr[RowField];
						int col = (int)dr[ColumnField];

						Color color;
						if (BLL.ConvertRule.ToString(dr["Task"]) == "实际")
							color = Color.Salmon;
						else
							color = Color.LimeGreen;

						ann.FillColor = color;
						ann.OutlineThickness = 3;
						ann.Text = " ";
						ann.Width = 8;
						ann.Height = 8;

						/*
						int x = 195;
						int y = 26;
						ann.Location.Type = Infragistics.UltraChart.Shared.Styles.LocationType.Pixels;
						ann.Location.LocationX = x;
						ann.Location.LocationY = y;
						*/;

						ann.Location.Type = Infragistics.UltraChart.Shared.Styles.LocationType.RowColumn;
						ann.Location.Row = row;
						ann.Location.Column = col;
																		//						ann.Text = string.Format("{0},{1}", row, col);

						chart.Annotations.Add(ann);

						//客户端事件 begin---------------
						sno++;
						DataRow drMy = tbMy.NewRow();
						drMy["sno"] = sno;
						drMy["row"] = row;
						drMy["col"] = col;
						tbMy.Rows.Add(drMy);


						//客户端事件 end---------------
					}
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// 父项条形图两端显示倒三角形
		/// </summary>
		/// <param name="chart"></param>
		/// <param name="tb"></param>
		/// <param name="StartField"></param>
		/// <param name="EndField"></param>
		/// <param name="RowField"></param>
		/// <param name="ColumnField"></param>
		private void SetChartParentBar(UltraChart chart, DataTable tb, string StartField, string EndField, string RowField, string ColumnField)
		{
			try 
			{
				foreach(DataRow dr in tb.Rows)
				{
					Infragistics.UltraChart.Resources.Appearance.BoxAnnotation ann = new Infragistics.UltraChart.Resources.Appearance.BoxAnnotation();

					int row = (int)dr[RowField];
					int col = (int)dr[ColumnField];

					ann.FillColor = Color.Transparent;
					ann.Text = " ";
					ann.Width = 20;
					ann.Height = 20;
//					ann.PE.fi

					/*
					int x = 195;
					int y = 26;
					ann.Location.Type = Infragistics.UltraChart.Shared.Styles.LocationType.Pixels;
					ann.Location.LocationX = x;
					ann.Location.LocationY = y;
					*/;

					ann.Location.Type = Infragistics.UltraChart.Shared.Styles.LocationType.RowColumn;
					ann.Location.Row = row;
					ann.Location.Column = col;

					chart.Annotations.Add(ann);

				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		private void LoadChart() 
		{
			try 
			{
				string WBSCode = this.txtWBSCode.Value;

//				if (WBSCode != "") 
//				{
					DataSet ds = (DataSet)Session["dsGantt"];

					//折叠工作项时，删除其子项
					string CollapseWBSCode = BLL.ConvertRule.ToString(Request.QueryString["CollapseWBSCode"]);
					if (CollapseWBSCode != "")
					{
						string[] arrCollapseWBSCode = CollapseWBSCode.Split(",".ToCharArray());
						foreach(string code in arrCollapseWBSCode) 
						{
							BLL.ConstructProgRule.CollapseProjectProgressChartDataTable(code, ds);
						}
					}

					BLL.ConstructProgRule.ResetProjectProgressGantt(ds, "B");

					DataTable tbGantt = ds.Tables[1];
					DataTable tbInfo = ds.Tables[2];

					this.txtChartWBSCode.Value = BLL.ConvertRule.ToString(tbInfo.Rows[0]["ChartWBSCode"]);
					this.txtChartCompletePercent.Value = BLL.ConvertRule.ToString(tbInfo.Rows[0]["ChartCompletePercent"]);
					this.txtChartStatusName.Value = BLL.ConvertRule.ToString(tbInfo.Rows[0]["ChartStatusName"]);

					if (IsGanttDataEmpty(tbGantt, "Start", "End"))
					{
						InitEmptyGanttData(tbGantt, "Start", "End");
					}
					else
					{
//						SetChartDataPoint(this.UltraChart1, tbGantt, "Start", "End", "Row", "Column");
//						SetChartParentBar(this.UltraChart1, tbGantt, "Start", "End", "Row", "Column");
					}

					int RowCount = ds.Tables[0].Rows.Count;
					int RowHeight = BLL.ConvertRule.ToInt(this.txtChartRowHeight.Value);
					int TopHeight = BLL.ConvertRule.ToInt(this.txtChartTopHeight.Value);
					int BottomHeight = BLL.ConvertRule.ToInt(this.txtChartBottomHeight.Value);


					this.UltraChart1.Height = BLL.ConstructProgRule.GetGanttChartHeight(RowCount, RowHeight, TopHeight, BottomHeight);
					this.txtChartHeight.Value = this.UltraChart1.Height.ToString();
					this.txtChartDataHeight.Value = BLL.ConstructProgRule.GetGanttDataHeight(RowCount, RowHeight).ToString();
					
					this.UltraChart1.DataSource = tbGantt;
					this.UltraChart1.Data.DataBind();

					if (tbGantt.Rows.Count == 0)
					{
						this.UltraChart1.Visible = false;
                        this.lblHint.Text = "无工作子项，不能显示进度图";
					}
					else
					{
						this.UltraChart1.Visible = true;
                        this.lblHint.Text = "";
					}
//				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"显示图表失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示图表失败：" + ex.Message));
			}
		}

		private void UltraChart1_ChartDrawItem(object sender, Infragistics.UltraChart.Shared.Events.ChartDrawItemEventArgs e)
		{
			if (e.Primitive.ToString() == "Infragistics.UltraChart.Core.Primitives.Ellipse")
			{
				Infragistics.UltraChart.Core.Primitives.Ellipse ell = (Infragistics.UltraChart.Core.Primitives.Ellipse)e.Primitive;

				if (Session["tbMyChart"] != null)
				{
					DataTable tb = (DataTable)Session["tbMyChart"];
					DataRow dr = null;

					bool isLast= false;
					int c = tb.Rows.Count;
					for (int i=0;i<c;i++)
					{
						if (tb.Rows[i]["x"] == DBNull.Value)
						{
							dr = tb.Rows[i];

							if (i==c-1)
							{
								isLast = true;
							}

							break;
						}
					}

					if (dr != null)
					{
						dr["x"] = ell.p1.X;
						dr["y"] = ell.p1.Y;
					}

					if (isLast)
					{
						WriteMyChartScript();
					}
				}
			}

			/*
			if (e.Primitive.ToString() == "Infragistics.UltraChart.Core.Primitives.Ellipse")
			{
				Infragistics.UltraChart.Core.Primitives.Ellipse ell = (Infragistics.UltraChart.Core.Primitives.Ellipse)e.Primitive;
				Response.Write(e.Primitive.ToString() + ":" + ell.p1.X.ToString() + "," + ell.p1.Y.ToString() + "<br>");
				
				//				Response.Write(e.Primitive.ToString());
				//				Response.Write(":" + e.Primitive.PE.ToString() + "<br>");
			}
			else if (e.Primitive.ToString() == "Infragistics.UltraChart.Core.Primitives.Text")
			{
				Infragistics.UltraChart.Core.Primitives.Text ell = (Infragistics.UltraChart.Core.Primitives.Text)e.Primitive;
				Response.Write(e.Primitive.ToString() + ":" + ell.bounds.Location.X.ToString() + "," + ell.bounds.Location.Y.ToString() + ":" + ell.GetTextString() + "<br>");
			}
			else
			{
				Response.Write(e.Primitive.ToString() + "<br>");
			}
			*/
		}

	}
}
