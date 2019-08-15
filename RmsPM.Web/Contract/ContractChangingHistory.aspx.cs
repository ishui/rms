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

namespace RmsPM.Web.Contract
{
	/// <summary>
	/// ContractChangingHistory 的摘要说明。
	/// </summary>
	public partial class ContractChangingHistory : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!IsPostBack)
			{
				LoadData();
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
		private void LoadData()
		{
			try
			{
				string ContractCode=Request.QueryString["ContractCode"]+"";
				string ContractLabel="";

				EntityData entity=DAL.EntityDAO.ContractDAO.GetContractByCode(ContractCode);
				if(entity.HasRecord())
				{
					ContractLabel=entity.GetString("ContractLabel");
				}
				
				RmsPM.DAL.QueryStrategy.ContractStrategyBuilder CSB=new RmsPM.DAL.QueryStrategy.ContractStrategyBuilder();

				//显示该合同所有的变更记录
				CSB.AddStrategy( new Strategy( DAL.QueryStrategy.ContractStrategyName.NotOriginalContract));
//				CSB.AddStrategy( new Strategy( DAL.QueryStrategy.ContractStrategyName.Status,"6"));

				CSB.AddStrategy( new Strategy( DAL.QueryStrategy.ContractStrategyName.ContractLabel,ContractLabel));
				string sql = CSB.BuildMainQueryString();
				QueryAgent qa = new QueryAgent();
				EntityData entityTemp = qa.FillEntityData("Contract",sql);
				qa.Dispose();

				this.dgList.DataSource = entityTemp.CurrentTable;
				this.dgList.DataBind();

				entity.Dispose();
				entityTemp.Dispose();

			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"加载合同列表错误。");
			}
		}

		private void dgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.dgList.CurrentPageIndex=e.NewPageIndex;
			LoadData();
		}
	}
}
