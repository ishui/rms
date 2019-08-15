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
	/// CostBudgetInfoTree ��ժҪ˵����
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
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
            }
		}

        private void LoadData()
        {
            try
            {
                if (this.txtCostBudgetBackupCode.Value != "")
                {
                    //ȡ���ݱ�
                    EntityData entityBackup = DAL.EntityDAO.CostBudgetDAO.GetV_CostBudgetBackupByCode(this.txtCostBudgetBackupCode.Value);
                    if (!entityBackup.HasRecord())
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, "��Ŀ���ñ��ݱ�����"));
                        Response.Write(Rms.Web.JavaScript.WinClose(true));
                        return;
                    }

                    //ȡ���ݵ�Ԥ�����ñ�
                    EntityData entitySet = DAL.EntityDAO.CostBudgetDAO.GetCostBudgetBackupSetByBackupCode(this.txtCostBudgetBackupCode.Value, this.txtCostBudgetSetCode.Value, true);
                    if (!entitySet.HasRecord())
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, "�����в�����Ԥ���"));
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
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾԤ������" + ex.Message));
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
        /// �󶨶�̬������ϸ
        /// </summary>
        private void BindDataGrid(BLL.CostBudgetDynamic dyn, bool IsScreenToTable)
        {
            try
            {
                DataTable tbDtl = dyn.tbHtml;

                //����Ԥ����������
                this.lblTargetCheckDate.Text = dyn.GetTargetCheckDate();
                string VerID = dyn.GetTargetVerID();
                this.txtHasTargetHis.Value = (BLL.ConvertRule.ToDecimal(VerID) > 0) ? "1" : "";

                if (VerID != "") VerID = "�汾" + VerID;

                //�汾������
                this.spanTargetVerID.InnerText = "";
                this.hrefTargetVerID.InnerText = "";
                if (this.txtHasTargetHis.Value == "1")  //����ʷ�汾
                {
                    this.hrefTargetVerID.InnerText = VerID;
                }
                else
                {
                    this.spanTargetVerID.InnerText = VerID;
                }

                /*
                //��ʷĿ�����
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
                    //��Ļ���ݱ��浽��ʱ��
                    tbDtl = ScreenToTable(false);
                }
                else
                {
                    //�۵�ȫ��������
                    BLL.CostBudgetPageRule.CollapseAll(tbDtl);

                    if (dyn.NeedApport) //�з�̯ʱ������һ���ܼ�
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
                Response.Write(Rms.Web.JavaScript.Alert(true, "�󶨶�̬������ϸ����" + ex.Message));
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
	}
}
