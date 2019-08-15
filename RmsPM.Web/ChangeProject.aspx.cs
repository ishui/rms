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

namespace RmsPM.Web
{
	/// <summary>
	/// ChangeProject 的摘要说明。
	/// </summary>
	public partial class ChangeProject : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
//				this.txtJgYear.Value=DateTime.Now.Year.ToString();
//				this.txtKgYear.Value=DateTime.Now.Year.ToString();
				this.SelectStatus.Items.Clear();
				this.SelectStatus.Items.Add(new ListItem("",""));
				this.SelectStatus.Items.Add(new ListItem("未来项目","-1"));
				this.SelectStatus.Items.Add(new ListItem("当前项目","0"));
				this.SelectStatus.Items.Add(new ListItem("结案项目","1"));
				this.SelectStatus.Value="0";

				iniPage();
			}
		}

		private void iniPage()
		{
			try
			{
				string projectName=this.txtProjectNname.Value;

				int kgYear=0;
			
				int jgYear=0;
				
				if (this.txtKgYear.Value.Trim().Length>0)
				{
					kgYear=int.Parse(this.txtKgYear.Value);
				}
				else
				

					if (this.txtJgYear.Value.Trim().Length>0)
				{
					jgYear=int.Parse(this.txtJgYear.Value);
					
				}
				
				RmsPM.DAL.QueryStrategy.ProjectStrategyBuilder ssb= new RmsPM.DAL.QueryStrategy.ProjectStrategyBuilder();
				if ( projectName.Length > 0 )
					ssb.AddStrategy( new Strategy(RmsPM.DAL.QueryStrategy.ProjectStrategyName.ProjectNameLike,projectName));
				if ( kgYear> 0 )
					ssb.AddStrategy( new Strategy(RmsPM.DAL.QueryStrategy.ProjectStrategyName.kgYear,kgYear.ToString()));
				if ( jgYear > 0 )
					ssb.AddStrategy( new Strategy(RmsPM.DAL.QueryStrategy.ProjectStrategyName.jgYear,jgYear.ToString()));
				if (this.SelectStatus.Value!="")
				{
					ssb.AddStrategy( new Strategy(RmsPM.DAL.QueryStrategy.ProjectStrategyName.Status,this.SelectStatus.Value));
				}
				
				ssb.AddOrder("kgDate",false);
				//ssb.AddOrder("Status",true);

				string sql = ssb.BuildMainQueryString();

				Rms.ORMap.QueryAgent qa = new QueryAgent();
				DataSet ds = qa.ExecSqlForDataSet(sql);
				qa.Dispose();
				
				this.dgList.DataSource = new DataView(ds.Tables[0],"","",DataViewRowState.CurrentRows);
				this.dgList.DataBind();

			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"加载项目列表错误。");
			}
		}

		private void LoadDataGrid()
		{
			try
			{
				string projectName=this.txtProjectNname.Value;

				int kgYear=0;
			
				int jgYear=0;
				
				if (this.txtKgYear.Value.Trim().Length>0)
				{
					kgYear=int.Parse(this.txtKgYear.Value);
				}
				else
				

				if (this.txtJgYear.Value.Trim().Length>0)
				{
					jgYear=int.Parse(this.txtJgYear.Value);
					
				}
				
				RmsPM.DAL.QueryStrategy.ProjectStrategyBuilder ssb= new RmsPM.DAL.QueryStrategy.ProjectStrategyBuilder();
				if ( projectName.Length > 0 )
					ssb.AddStrategy( new Strategy(RmsPM.DAL.QueryStrategy.ProjectStrategyName.ProjectNameLike,projectName));
				if ( kgYear> 0 )
					ssb.AddStrategy( new Strategy(RmsPM.DAL.QueryStrategy.ProjectStrategyName.kgYear,kgYear.ToString()));
				if ( jgYear > 0 )
					ssb.AddStrategy( new Strategy(RmsPM.DAL.QueryStrategy.ProjectStrategyName.jgYear,jgYear.ToString()));

				if (this.SelectStatus.Value!="")
				{
					ssb.AddStrategy( new Strategy(RmsPM.DAL.QueryStrategy.ProjectStrategyName.Status,this.SelectStatus.Value));
				}
				
				ssb.AddOrder("kgDate",false);
				//ssb.AddOrder("Status",true);
				string sql = ssb.BuildMainQueryString();

				Rms.ORMap.QueryAgent qa = new QueryAgent();
				DataSet ds = qa.ExecSqlForDataSet(sql);
				qa.Dispose();
				
				this.dgList.DataSource = new DataView(ds.Tables[0],"","",DataViewRowState.CurrentRows);
				this.dgList.DataBind();

			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"加载项目列表错误。");
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

		protected void Button1_ServerClick(object sender, System.EventArgs e)
		{
			this.dgList.CurrentPageIndex = 0;
			LoadDataGrid();
		}

		protected void Button2_ServerClick(object sender, System.EventArgs e)
		{
			
			Response.Write(JavaScript.ScriptStart);
			Response.Write("window.opener.DoSelectProject('"+this.hidPorjectCode.Value+"');");
			Response.Write("window.close();");
			Response.Write(JavaScript.ScriptEnd);

		}

		private void dgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			
			this.dgList.CurrentPageIndex = e.NewPageIndex;
			LoadDataGrid();
		}

		
	}
}
