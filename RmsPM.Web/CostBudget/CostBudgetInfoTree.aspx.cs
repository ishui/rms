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
using RmsPM.DAL.QueryStrategy;
using Rms.Web;

namespace RmsPM.Web.CostBudget
{
	/// <summary>
	/// CostBudgetInfoTree 的摘要说明。
	/// </summary>
	public partial class CostBudgetInfoTree : PageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!Page.IsPostBack)
            {
                IniPage();
                LoadData();
            }
		}

		private void IniPage()
		{
            try
            {
                this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
                this.txtCostBudgetSetCode.Value = Request.QueryString["CostBudgetSetCode"];
                this.txtCostBudgetBackupCode.Value = Request.QueryString["CostBudgetBackupCode"];
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
            }
		}

        private void LoadData()
        {
            try
            {
                if (this.txtCostBudgetBackupCode.Value != "")
                {
                    //取备份表
                    EntityData entityBackup = DAL.EntityDAO.CostBudgetDAO.GetV_CostBudgetBackupByCode(this.txtCostBudgetBackupCode.Value);
                    if (!entityBackup.HasRecord())
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, "项目费用备份表不存在"));
                        Response.Write(Rms.Web.JavaScript.WinClose(true));
                        return;
                    }

                    //取备份的预算设置表
                    EntityData entitySet = DAL.EntityDAO.CostBudgetDAO.GetCostBudgetBackupSetByBackupCode(this.txtCostBudgetBackupCode.Value, this.txtCostBudgetSetCode.Value, true);
                    if (!entitySet.HasRecord())
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, "备份中不含该预算表"));
                        Response.Write(Rms.Web.JavaScript.WinClose(true));
                        return;
                    }

                    this.txtCostBudgetBackupSetCode.Value = entitySet.GetString("CostBudgetBackupSetCode");

                    entitySet.Dispose();
                    entityBackup.Dispose();
                }

                BLL.CostBudgetDynamic dyn = GenerateDynamic();
                Session[SessionEntityID] = dyn;

                BindDataGrid(dyn, false);

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "显示预算表出错：" + ex.Message));
            }
        }

        public string SessionEntityID
        {
            get
            {
                if (this.txtCostBudgetBackupSetCode.Value != "")
                {
                    //					return "";
                    return "CostBudgetDynamic_backup";
                }
                else
                {
                    return "CostBudgetDynamic_" + this.txtCostBudgetSetCode.Value;
                }
            }
        }

        private BLL.CostBudgetDynamic GenerateDynamic()
        {
            try
            {
                string StartY = "";
                string EndY = "";

                BLL.CostBudgetDynamic dyn = new RmsPM.BLL.CostBudgetDynamic(this.txtProjectCode.Value, this.txtCostBudgetSetCode.Value, this.txtCostBudgetBackupSetCode.Value);
                dyn.StartY = StartY;
                dyn.EndY = EndY;

                dyn.Generate();

                return dyn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 绑定动态费用明细
        /// </summary>
        private void BindDataGrid(BLL.CostBudgetDynamic dyn, bool IsScreenToTable)
        {
            try
            {
                DataTable tbDtl = dyn.tbHtml;

                //已批预算的审核日期
                this.lblTargetCheckDate.Text = dyn.GetTargetCheckDate();
                string VerID = dyn.GetTargetVerID();
                this.txtHasTargetHis.Value = (BLL.ConvertRule.ToDecimal(VerID) > 0) ? "1" : "";

                if (VerID != "") VerID = "版本" + VerID;

                //版本号链接
                this.spanTargetVerID.InnerText = "";
                this.hrefTargetVerID.InnerText = "";
                if (this.txtHasTargetHis.Value == "1")  //有历史版本
                {
                    this.hrefTargetVerID.InnerText = VerID;
                }
                else
                {
                    this.spanTargetVerID.InnerText = VerID;
                }

                /*
                //历史目标费用
                string TargetHisHead1 = "";
                string TargetHisHead2 = "";
                dyn.GenerateTargetHisHead(ref TargetHisHead1, ref TargetHisHead2);

                ViewState["TargetHisHead1"] = TargetHisHead1;
                ViewState["TargetHisHead2"] = TargetHisHead2;

                ViewState["HasTargetChange"] = dyn.HasTargetChange;
                if (dyn.HasTargetChange)
                {
                    this.spanListTitleTargetMoneyDesc.InnerText = dyn.TargetChangeDesc;
                    this.spanListTitleTargetMoney.Style["display"] = "";
                }
                else
                {
                    this.spanListTitleTargetMoneyDesc.InnerText = "";
                    this.spanListTitleTargetMoney.Style["display"] = "none";
                }

                if (IsScreenToTable)
                {
                    //屏幕数据保存到临时表
                    tbDtl = ScreenToTable(false);
                }
                else
                {
                    //折叠全部费用项
                    BLL.CostBudgetPageRule.CollapseAll(tbDtl);

                    if (dyn.NeedApport) //有分摊时，多了一级总计
                    {
                        BLL.CostBudgetPageRule.ExpandDeep(tbDtl, 3);
                    }
                    else
                    {
                        BLL.CostBudgetPageRule.ExpandDeep(tbDtl, 2);
                    }
                }
                */
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "绑定动态费用明细出错：" + ex.Message));
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
	}
}
