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
	/// BiddingQuery ��ժҪ˵����
    /// ��Ͷ��ƻ���ѯ
	/// </summary>
	public partial class BiddingQuery : PageBase
	{
	
        /// <summary>
        /// ҳ�����
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
        /// ���ݼ���'
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
                    //Ԥ������
                    if (this.TxtPrejudicationDateS.Value != "" || this.TxtPrejudicationDateE.Value != "")
                    {
                        ArrayList ar = new ArrayList();
                        ar.Add(this.TxtPrejudicationDateS.Value);
                        ar.Add(this.TxtPrejudicationDateE.Value);
                        sb.AddStrategy(new Strategy(BiddingStrategyName.PrejudicationDate, ar));
                    }

                    //��������
                    if (this.TxtEmitDateS.Value != "" || this.TxtEmitDateE.Value != "")
                    {
                        ArrayList ar = new ArrayList();
                        ar.Add(this.TxtEmitDateS.Value);
                        ar.Add(this.TxtEmitDateE.Value);
                        sb.AddStrategy(new Strategy(BiddingStrategyName.EmitDate, ar));
                    }
                    //�ر�����
                    if (this.TxtReturnDateS.Value != "" || this.TxtReturnDateE.Value != "")
                    {
                        ArrayList ar = new ArrayList();
                        ar.Add(this.TxtReturnDateS.Value);
                        ar.Add(this.TxtReturnDateE.Value);
                        sb.AddStrategy(new Strategy(BiddingStrategyName.ReturnDate, ar));
                    }


                    //��������
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


                //���б�������
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
                ApplicationLog.WriteLog(this.ToString(), ex, "��Ͷ��ƻ������쳣��");
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
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
        /// <summary>
        /// ������ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			LoadData();
		}
	}
}
