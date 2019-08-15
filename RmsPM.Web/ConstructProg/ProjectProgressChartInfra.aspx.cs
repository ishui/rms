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
	/// ProjectProgressChartInfra
	/// </summary>
	public partial class ProjectProgressChartInfra : System.Web.UI.Page
	{

//		protected decimal MonthPixel = 50;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			BLL.FileIO.CreateDir(Server.MapPath(this.UltraChart1.ChartImagesPath));

			if (!Page.IsPostBack) 
			{
				IniPage();
				LoadChart();
			}
		}

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		�����֧������ķ��� - ��Ҫʹ�ô���༭��
		///		�޸Ĵ˷��������ݡ�
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
				ApplicationLog.WriteLog(this.ToString(),ex,"��ʼ��ҳ��ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ��ʧ�ܣ�" + ex.Message));
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

				//�ͻ����¼�
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
						if (BLL.ConvertRule.ToString(dr["Task"]) == "ʵ��")
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

						//�ͻ����¼� begin---------------
						sno++;
						DataRow drMy = tbMy.NewRow();
						drMy["sno"] = sno;
						drMy["row"] = row;
						drMy["col"] = col;
						tbMy.Rows.Add(drMy);


						//�ͻ����¼� end---------------
					}
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/*
		private DataTable GetGanttDataTable()
		{
			DataTable table = new DataTable();
			table.Columns.AddRange(
				new DataColumn[] {
									 new DataColumn("Series", typeof(string)),
									 new DataColumn("Task", typeof(string)),
									 new DataColumn("Start", typeof(DateTime)),
									 new DataColumn("End", typeof(DateTime)),
									 new DataColumn("ID", typeof(int)),
									 new DataColumn("LinkTo", typeof(int)),
									 new DataColumn("PercentComplete", typeof(double)),
									 new DataColumn("Owner", typeof(string))
								 });
			DateTime now = DateTime.Now;

			table.Rows.Add(new object[] {"Implement", "Phase 4", now.AddDays(12),         now.AddDays(15), 9, -1, 0,   "David"});
			table.Rows.Add(new object[] {"Implement", "Phase 3", now.AddDays(8),          now.AddDays(10), 8,  9, 0,   "David"});
			table.Rows.Add(new object[] {"Implement", "Phase 2", now.AddDays(5),          now.AddDays(7) , 7,  8, 25,  "David"});
			table.Rows.Add(new object[] {"Implement", "Phase 1", now.AddDays(1),          now.AddDays(5) , 6,  7, 100, "David"});
			table.Rows.Add(new object[] {"Design", "Create diagram", now.AddDays(12),     now.AddDays(20), 5, -1, 33,  "Bill"});
			table.Rows.Add(new object[] {"Design", "Outline plans",      now.AddDays(6),  now.AddDays(12), 4,  5, 100, "Bill"});
			table.Rows.Add(new object[] {"Plan", "Review",               now.AddDays(2),  now.AddDays(4),  3,  4, 0,   "Bill"});
			table.Rows.Add(new object[] {"Plan", "Delegate effort",      now,             now.AddDays(2),  2,  3, 0,   "Bill"});
			table.Rows.Add(new object[] {"Plan", "Determine milestones", now.AddDays(8),  now.AddDays(12), 1, -1, 25,  "Thomas"});
			table.Rows.Add(new object[] {"Plan", "Establish goals",      now,             now.AddDays(6),  0,  1, 100, "Thomas"});
			return table;
		}
		*/

		private void LoadChart() 
		{
			try 
			{
				string WBSCode = this.txtWBSCode.Value;

				if (WBSCode != "") 
				{
					DataSet ds = BLL.ConstructProgRule.GetProjectProgressChartDataTable(WBSCode);

					this.dgLabelY.DataSource = ds.Tables[0];
					this.dgLabelY.DataBind();

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
						SetChartDataPoint(this.UltraChart1, tbGantt, "Start", "End", "Row", "Column");
					}

					int RowCount = ds.Tables[0].Rows.Count;
					int RowHeight = BLL.ConvertRule.ToInt(this.txtChartRowHeight.Value);
					int TopHeight = BLL.ConvertRule.ToInt(this.txtChartTopHeight.Value);
					int BottomHeight = BLL.ConvertRule.ToInt(this.txtChartBottomHeight.Value);

					this.UltraChart1.Height = BLL.ConstructProgRule.GetGanttChartHeight(RowCount, RowHeight, TopHeight, BottomHeight);
					this.txtChartDataHeight.Value = BLL.ConstructProgRule.GetGanttDataHeight(RowCount, RowHeight).ToString();

					this.UltraChart1.DataSource = tbGantt;
					this.UltraChart1.Data.DataBind();

					if (tbGantt.Rows.Count == 0)
					{
						this.UltraChart1.Visible = false;
					}
					else
					{
						this.UltraChart1.Visible = true;
					}
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ʾͼ��ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾͼ��ʧ�ܣ�" + ex.Message));
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
