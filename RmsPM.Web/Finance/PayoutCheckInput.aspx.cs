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

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// PayoutCheckInput ��ժҪ˵����
	/// </summary>
	public partial class PayoutCheckInput : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlGenericControl ContractNameTemp;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAddFromCost;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage()
		{
			try
			{
				this.txtPayoutCode.Value = Request.QueryString["PayoutCode"];
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadData()
		{
			string payoutCode = this.txtPayoutCode.Value;

			try
			{
				if ( payoutCode == "")
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "�޸����"));
					return;
				}

				EntityData entity = DAL.EntityDAO.PaymentDAO.GetStandard_PayoutByCode(payoutCode);
				if ( entity.HasRecord())
				{
                    DataRow dr = entity.CurrentRow;

					this.txtProjectCode.Value = entity.GetString("ProjectCode");
                    this.txtSubjectSetCode.Value = entity.GetString("SubjectSetCode");
                    this.lblMoney.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(dr["Money"]), "Ԫ");
                    this.lblPayoutID.Text = entity.GetString("PayoutID");
                }
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "���������"));
					return;
				}

                this.ucInputSubject.ProjectCode = this.txtProjectCode.Value;
                this.ucInputSubject.Value = entity.GetString("SubjectCode");

                DataTable tbDtl = BLL.PaymentRule.GeneratePayoutItemTable(payoutCode);
                BLL.PaymentRule.VoucherDetailAddColumnSubjectName(tbDtl, this.txtSubjectSetCode.Value);
                BindDataGrid(tbDtl);
                
                entity.Dispose();
			}
			catch (  Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ���ݳ���" + ex.Message));
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
        /// ��ʾ�����ϸ
        /// </summary>
        private void BindDataGrid(DataTable tb)
        {
            try
            {
                string[] arrField = { "ItemCash", "TotalPayoutCash", "RemainItemCash", "PayoutCash", "PayoutMoney" };
                decimal[] arrSum = BLL.MathRule.SumColumn(tb, arrField);
                this.dgList.Columns[3].FooterText = arrSum[0].ToString("N");
                this.dgList.Columns[4].FooterText = arrSum[1].ToString("N");
                this.dgList.Columns[5].FooterText = arrSum[2].ToString("N");
                this.dgList.Columns[6].FooterText = arrSum[3].ToString("N");
                this.dgList.Columns[8].FooterText = arrSum[4].ToString("N");

                this.dgList.DataSource = tb;
                this.dgList.DataBind();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ������ϸ����" + ex.Message));
            }
        }

        /// <summary>
        /// ��Ч�Լ��
        /// </summary>
        /// <param name="Hint"></param>
        /// <returns></returns>
        private bool CheckValid(ref string Hint, DataTable tbDtl)
        {
            Hint = "";

            string SubjectCode = this.ucInputSubject.Value;

            if ((SubjectCode == null) || (SubjectCode.Trim().Length == 0))
            {
                Hint = "�����������Ŀ ��";
                return false;
            }

            Hint = BLL.SubjectRule.CheckSubject(SubjectCode, txtSubjectSetCode.Value, string.Format("��Ŀ��š�{0}��", SubjectCode));
            if (Hint != "")
                return false;

            foreach (DataRow dr in tbDtl.Rows)
            {
                string PayoutItemCode = BLL.ConvertRule.ToString(dr["PayoutItemCode"]);
                decimal PayoutMoney = BLL.ConvertRule.ToDecimal(dr["PayoutMoney"]);

                //���θ����Ϊ0ʱ�ż��
                if (PayoutMoney != 0)
                {
                    SubjectCode = BLL.ConvertRule.ToString(dr["SubjectCode"]);

                    if ((SubjectCode == null) || (SubjectCode.Trim().Length == 0))
                    {
                        Hint = "�������Ŀ��� ��";
                        return false;
                    }

                    Hint = BLL.SubjectRule.CheckSubject(SubjectCode, txtSubjectSetCode.Value, string.Format("��Ŀ��š�{0}��", SubjectCode));
                    if (Hint != "")
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// ��Ļ���ݱ��浽��ʱ��
        /// </summary>
        /// <returns></returns>
        private DataTable ScreenToTable(bool isBindGrid)
        {
            DataTable tb = BLL.PaymentRule.GeneratePayoutItemTable(this.txtPayoutCode.Value);
            BLL.PaymentRule.VoucherDetailAddColumnSubjectName(tb, this.txtSubjectSetCode.Value);
            tb.Columns.Add("SubjectHint", typeof(String));

            foreach (DataGridItem item in this.dgList.Items)
            {
                HtmlInputHidden txtPayoutItemCode = (HtmlInputHidden)item.FindControl("txtPayoutItemCode");
                string PayoutItemCode = txtPayoutItemCode.Value;

                RmsPM.Web.UserControls.InputSubject ucInputSubject = (RmsPM.Web.UserControls.InputSubject)item.FindControl("ucInputSubject");

                DataRow[] drs = tb.Select("PayoutItemCode='" + PayoutItemCode + "'");
                if (drs.Length <= 0)
                    throw new Exception("δ�ҵ������ϸ");

                DataRow dr = drs[0];

                dr["SubjectCode"] = ucInputSubject.Value;
                dr["SubjectName"] = ucInputSubject.Text;
                dr["SubjectHint"] = ucInputSubject.Hint;
            }

            if (isBindGrid)
            {
                this.dgList.DataSource = tb;
                this.dgList.DataBind();
            }

            return tb;
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Save(DataTable tbDtl)
        {
            string PayoutCode = this.txtPayoutCode.Value;
            string projectCode = this.txtProjectCode.Value;

            try
            {
                EntityData entity = DAL.EntityDAO.PaymentDAO.GetStandard_PayoutByCode(PayoutCode);
                DataRow dr = entity.CurrentRow;

                dr["SubjectCode"] = this.ucInputSubject.Value;

                SaveDetail(entity, tbDtl);

                DAL.EntityDAO.PaymentDAO.SubmitAllStandard_Payout(entity);
                entity.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ���渶���ϸ
        /// </summary>
        private void SaveDetail(EntityData entity, DataTable tb)
        {
            try
            {
                entity.SetCurrentTable("Payout");
                string PayoutCode = entity.GetString("PayoutCode");
                string ProjectCode = entity.GetString("ProjectCode");

                //�޸�
                foreach (DataRow dr in tb.Rows)
                {
                    string PayoutItemCode = dr["PayoutItemCode"].ToString();
                    DataRow drNew;
                    DataRow[] drs;

                    //������ϸ
                    entity.SetCurrentTable("PayoutItem");
                    drs = entity.CurrentTable.Select("PayoutItemCode='" + PayoutItemCode + "'");

                    if (drs.Length == 0)
                    {
                        throw new Exception("δ�ҵ������ϸ");
                    }
                    else
                    {
                        drNew = drs[0];
                    }

                    drNew["SubjectCode"] = BLL.ConvertRule.ToString(dr["SubjectCode"]);
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                throw ex;
            }
        }

        /// <summary>
		/// ���
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string PayoutCode = this.txtPayoutCode.Value;

                //�µ���ϸ��
                DataTable tbDtl = ScreenToTable(true);
                if (tbDtl == null) return;

                string Hint = "";
                if (!CheckValid(ref Hint, tbDtl))
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
//                    BindDataGrid(tbDtl);
                    return;
                }

                Save(tbDtl);

                BLL.PaymentRule.CheckPayout(PayoutCode, this.txtCheckOpinion.Value, base.user.UserCode);
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "���ʧ�ܣ�" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return;
			}

			GoBack();
		}

		/// <summary>
		/// ����
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write("window.opener.location = window.opener.location;");
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

        protected void dgList_ItemCreated(object sender, DataGridItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    RmsPM.Web.UserControls.InputSubject ucInputSubject = (RmsPM.Web.UserControls.InputSubject)e.Item.FindControl("ucInputSubject");

                    ucInputSubject.ProjectCode = this.txtProjectCode.Value;

                    break;
            }
        }
        protected void dgList_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.AlternatingItem:
                case ListItemType.Item:
                    RmsPM.Web.UserControls.ExchangeRateControl ucItemCash = (RmsPM.Web.UserControls.ExchangeRateControl)e.Item.FindControl("ucItemCash");

                    DataRowView ud_drvItem = (DataRowView)e.Item.DataItem;


                    break;
            }
        }
}
}
