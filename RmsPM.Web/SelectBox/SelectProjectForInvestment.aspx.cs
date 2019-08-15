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
using RmsPM.DAL;
using RmsPM.BLL;
using Rms.ORMap;
using Rms.Web;

namespace RmsPM.Web.SelectBox
{
	/// <summary>
	/// ChangeProject 的摘要说明。
	/// </summary>
	public partial class SelectProjectForInvestment : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton Button2;
		protected System.Web.UI.HtmlControls.HtmlInputText txtKGYear;
		protected System.Web.UI.HtmlControls.HtmlSelect SelectStatus;
		protected System.Web.UI.HtmlControls.HtmlInputText txtJGYear;
		protected System.Web.UI.HtmlControls.HtmlInputText txtProjectName;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadDataGrid();
			}
		}

		private void IniPage() 
		{
			try 
			{
				this.txtType.Value = Request.QueryString["Type"];
				this.txtAllowNull.Value = Request.QueryString["AllowNull"];

				this.txtType.Value = this.txtType.Value.ToLower();

				switch (this.txtType.Value) 
				{
					case "multi":
						//多选
						this.dgList.Columns[0].Visible = true;
						this.dgList.Columns[1].Visible = true;
						this.dgList.Columns[2].Visible = false;

						this.trMulti1.Style["display"] = "block";
						this.trSingle1.Style["display"] = "none";

						break;

					default:
						//单选
						//允许清除
						if (this.txtAllowNull.Value == "1") 
						{
							this.btnClear.Style["display"] = "";
						}

						break;
				}

				//				this.txtJgYear.Value=DateTime.Now.Year.ToString();
				//				this.txtKgYear.Value=DateTime.Now.Year.ToString();

				BLL.PageFacade.LoadProjectStatusSelect(this.sltSearchStatus, true);
			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"加载项目列表页面错误。");
				Response.Write(Rms.Web.JavaScript.Alert(true, "加载项目列表页面错误。"));
			}
		}

		private void LoadDataGrid()
		{
			try
			{
				int kgYear = 0;
				int jgYear = 0;
				
//				if (this.txtSearchKgYear.Value.Trim().Length>0)
//				{
//					kgYear = BLL.ConvertRule.ToInt(this.txtSearchKgYear.Value);
//				}
//
//				if (this.txtSearchJgYear.Value.Trim().Length>0)
//				{
//					jgYear = BLL.ConvertRule.ToInt(this.txtSearchJgYear.Value);
//				}
				
				RmsPM.DAL.QueryStrategy.ProjectStrategyBuilder ssb= new RmsPM.DAL.QueryStrategy.ProjectStrategyBuilder();
				if ( this.txtSearchProjectName.Value.Length > 0 )
					ssb.AddStrategy( new Strategy(RmsPM.DAL.QueryStrategy.ProjectStrategyName.ProjectNameLike,this.txtSearchProjectName.Value));
				if ( kgYear != 0 )
					ssb.AddStrategy( new Strategy(RmsPM.DAL.QueryStrategy.ProjectStrategyName.kgYear,kgYear.ToString()));
				if ( jgYear != 0 )
					ssb.AddStrategy( new Strategy(RmsPM.DAL.QueryStrategy.ProjectStrategyName.jgYear,jgYear.ToString()));

				if (this.sltSearchStatus.Value != "")
					ssb.AddStrategy( new Strategy(RmsPM.DAL.QueryStrategy.ProjectStrategyName.Status,this.sltSearchStatus.Value));
				
				//排序
				string sortsql = BLL.GridSort.GetSortSQL(ViewState);
				if (sortsql == "")
				{
					//缺省排序
					ssb.AddOrder("kgDate",false);
					ssb.AddOrder("ProjectName",true);
					//ssb.AddOrder("Status",true);
				}

				string sql = ssb.BuildMainQueryString();

				if (sortsql != "")
				{
					//点列标题排序
					sql = sql + " order by " + sortsql;
				}

				Rms.ORMap.QueryAgent qa = new QueryAgent();
				DataTable tb = qa.ExecSqlForDataSet(sql).Tables[0];
				qa.Dispose();
				
				this.dgList.DataSource = tb;
				this.dgList.DataBind();


				//////////////////////////////
				for(int i=0;i<dgList.Items.Count;i++)
				{
					dgList.Items[i].Cells[5].Text = "上海";
				}

			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"加载项目列表错误。");
				Response.Write(Rms.Web.JavaScript.Alert(true, "加载项目列表错误。"));
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
			this.dgList.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemCreated);
			this.dgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgList_PageIndexChanged);
			this.dgList.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgList_SortCommand);

		}
		#endregion

		private void dgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			
			this.dgList.CurrentPageIndex = e.NewPageIndex;
			LoadDataGrid();
		}

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			this.dgList.CurrentPageIndex = 0;
			LoadDataGrid();
		}

		private void dgList_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			BLL.GridSort.SortCommand((DataGrid)source, ViewState, source, e);
			((DataGrid)source).CurrentPageIndex = 0;
			LoadDataGrid();
		}

		private void dgList_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			BLL.GridSort.ItemCreate((DataGrid)sender, ViewState, sender, e);
		}

		protected void btnAdd_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				this.tdAdd.Style["display"] = "";
				this.tdAdd2.Style["display"] = "";

				string codes = this.txtAddCode.Value;
				string names = this.txtAddName.Value;

				DataTable tb = new DataTable();
				tb.Columns.Add("Code", typeof(string));
				tb.Columns.Add("Name", typeof(string));

				//原先有的
				int k = -1;
				foreach(DataGridItem item in dgListAdd.Items) 
				{
					k++;
					string code = dgListAdd.DataKeys[k].ToString();
					HtmlInputHidden txtName = (HtmlInputHidden)item.FindControl("txtName");
					string name = txtName.Value;

					DataRow dr = tb.NewRow();
					dr["Code"] = code;
					dr["Name"] = name;
					tb.Rows.Add(dr);
				}

				string[] arrcode = codes.Split(",".ToCharArray());
				string[] arrname = names.Split(",".ToCharArray());

				//添加
				int l = arrcode.Length;
				for(int i=0;i<l;i++) 
				{
					if (tb.Select("Code='" + arrcode[i] + "'").Length == 0) 
					{
						DataRow dr = tb.NewRow();
						dr["Code"] = arrcode[i];
						dr["Name"] = arrname[i];
						tb.Rows.Add(dr);
					}
				}

				this.txtAddCode.Value = BLL.ConvertRule.Concat(tb, "Code", ",");
				this.txtAddName.Value = BLL.ConvertRule.Concat(tb, "Name", ",");

				this.dgListAdd.DataSource = tb;
				this.dgListAdd.DataBind();
			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"加载添加列表错误。");
				Response.Write(Rms.Web.JavaScript.Alert(true, "加载添加列表错误。"));
			}
		}

		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				string codes = this.txtAddCode.Value;
				string[] arrcode = codes.Split(",".ToCharArray());

				DataTable tb = new DataTable();
				tb.Columns.Add("Code", typeof(string));
				tb.Columns.Add("Name", typeof(string));

				int i = -1;
				foreach(DataGridItem item in dgListAdd.Items) 
				{
					i++;
					string code = dgListAdd.DataKeys[i].ToString();
					HtmlInputHidden txtName = (HtmlInputHidden)item.FindControl("txtName");
					string name = txtName.Value;

					if (BLL.ConvertRule.FindArray(arrcode, code) < 0)
					{
						DataRow dr = tb.NewRow();
						dr["Code"] = code;
						dr["Name"] = name;
						tb.Rows.Add(dr);
					}
				}

				this.txtAddCode.Value = BLL.ConvertRule.Concat(tb, "Code", ",");
				this.txtAddName.Value = BLL.ConvertRule.Concat(tb, "Name", ",");

				this.dgListAdd.DataSource = tb;
				this.dgListAdd.DataBind();
			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"移除出错。");
				Response.Write(Rms.Web.JavaScript.Alert(true, "移除出错。"));
			}
		}

		
	}
}
