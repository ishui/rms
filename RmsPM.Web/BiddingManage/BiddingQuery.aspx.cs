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
using RmsPM.DAL.QueryStrategy;
using Rms.ORMap;

namespace RmsPM.Web.BiddingManage
{
	/// <summary>
	/// BiddingQuery 的摘要说明。
    /// 招投标计划查询
	/// </summary>
	public partial class BiddingQuery : PageBase
	{
	
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!this.IsPostBack)
            {
                LoadData();
            }
		}
        /// <summary>
        /// 数据加载'
        /// </summary>
        private void LoadData()
        {
            try
            {
                if (Request["ProjectCode"] + "" == "")
                {
                    return;
                }

                RmsPM.DAL.QueryStrategy.BiddingStrategyBuilder sb = new RmsPM.DAL.QueryStrategy.BiddingStrategyBuilder();

                if (txtTitle.Value.Trim() != "")
                    sb.AddStrategy(new Strategy(BiddingStrategyName.Title, txtTitle.Value.Trim()));



                if (this.txtAdvSearch.Value != "none")
                {
                    //预审日期
                    if (this.TxtPrejudicationDateS.Value != "" || this.TxtPrejudicationDateE.Value != "")
                    {
                        ArrayList ar = new ArrayList();
                        ar.Add(this.TxtPrejudicationDateS.Value);
                        ar.Add(this.TxtPrejudicationDateE.Value);
                        sb.AddStrategy(new Strategy(BiddingStrategyName.PrejudicationDate, ar));
                    }

                    //发标日期
                    if (this.TxtEmitDateS.Value != "" || this.TxtEmitDateE.Value != "")
                    {
                        ArrayList ar = new ArrayList();
                        ar.Add(this.TxtEmitDateS.Value);
                        ar.Add(this.TxtEmitDateE.Value);
                        sb.AddStrategy(new Strategy(BiddingStrategyName.EmitDate, ar));
                    }
                    //回标日期
                    if (this.TxtReturnDateS.Value != "" || this.TxtReturnDateE.Value != "")
                    {
                        ArrayList ar = new ArrayList();
                        ar.Add(this.TxtReturnDateS.Value);
                        ar.Add(this.TxtReturnDateE.Value);
                        sb.AddStrategy(new Strategy(BiddingStrategyName.ReturnDate, ar));
                    }


                    //定标日期
                    if (this.TxtConfirmDateS.Value != "" || this.TxtConfirmDateE.Value != "")
                    {
                        ArrayList ar = new ArrayList();
                        ar.Add(this.TxtConfirmDateS.Value);
                        ar.Add(this.TxtConfirmDateE.Value);
                        sb.AddStrategy(new Strategy(BiddingStrategyName.ConfirmDate, ar));
                    }
                }

                sb.AddStrategy(new Strategy(BiddingStrategyName.ProjectCode, Request["ProjectCode"] + ""));
                sb.AddStrategy(new Strategy(BiddingStrategyName.State, Request["State"] + "%"));
                if (this.Inputunit1.Value != "")
                {
                    sb.AddStrategy(new Strategy(BiddingStrategyName.BiddingRemark1, this.Inputunit1.Value));
                }

                string sql = sb.BuildMainQueryString();


                //点列标题排序
                sql = sql + " order by EmitDate desc";


                QueryAgent qa = new QueryAgent();
                EntityData entity = qa.FillEntityData("Bidding", sql);
                qa.Dispose();

               // this.InputCostBudgetDtl1.ProjectCode = this.project.ProjectCode;
                //if (txtType.Value.Trim() != "")
                // BiddingList1.Type = txtType.Value.Trim();
                if (txtTitle.Value.Trim() != "")
                    BiddingList1.Title = txtTitle.Value.Trim();
                this.BiddingList1.DtBidding = entity.CurrentTable;
                // if (this.InputCostBudgetDtl1.CostCode != "")
                // {
                //     BiddingList1.CostBudgetSetCode = this.InputCostBudgetDtl1.CostBudgetSetCode;
                //    BiddingList1.CostCode = this.InputCostBudgetDtl1.CostCode;
                // }

                BiddingList1.ProjectCode = Request["ProjectCode"] + "";
                BiddingList1.State = Request["State"] + "%";
                BiddingList1.DataBound();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "招投标计划搜索异常！");
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
        /// <summary>
        /// 搜索按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			LoadData();
		}
	}
}
