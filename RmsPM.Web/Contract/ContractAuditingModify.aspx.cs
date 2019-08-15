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
using RmsPM.DAL;
using RmsPM.DAL.QueryStrategy;
using RmsPM.BLL;

namespace RmsPM.Web.Contract
{
	/// <summary>
	/// ContractAuditingModify ��ժҪ˵����
	/// </summary>
	public partial class ContractAuditingModify : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTable tableButton;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage()
		{
			string act = Request["Act"]+"";
			if ( act == "View")
			{
				this.trModifyCheckOpinion.Visible = false;
				this.btnSave.Visible = false;
				this.btnNoPass.Visible = false;
				this.btnCancel.Visible = false;
			}
			else
			{
				this.trViewCheckOpinion.Visible = false;
				this.btnClose.Visible = false;
				this.lblCheckPersonName.Text = user.UserName;
				this.lblCheckOpinionDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
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
			this.dgCostList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgCostList_ItemDataBound);

		}
		#endregion
		private void LoadData()
		{

			string contractCode=Request["ContractCode"] + "" ;
			string projectCode=Request["projectCode"] + "" ;

			try
			{
				int status = -1;

				EntityData entity=RmsPM.DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(contractCode); 
				if(entity.HasRecord())
				{
					status = entity.GetInt("Status");
					if(status==0)
					{
						LabelContractStatus.Text="�º�ͬ����";
					}
					else if(status==4)
					{
						LabelContractStatus.Text="�������";
						//this.ContractDate.Visible=false;
					}

					this.txtContractID.Value = entity.GetString("ContractID");
                    this.lblContractID.Text = entity.GetString("ContractID");

					LabelContractName.Text=entity.GetString("ContractName");
					string contractPerson = entity.GetString("ContractPerson");
					this.txtContractPerson.Value = contractPerson;
					this.txtContractPersonName.Value=BLL.SystemRule.GetUserName(contractPerson);
					this.ContractDate.Value=entity.GetDateTimeOnlyDate("ContractDate");

					string act = Request["Act"] + "";
					if ( act == "View" )
					{
						this.lblCheckOpinion.Text = entity.GetString("CheckOpinion");
						this.lblCheckOpinionDate.Text = entity.GetDateTimeOnlyDate("CheckDate");
						this.lblCheckPersonName.Text = BLL.SystemRule.GetUserName(entity.GetString("CheckPerson"));
					}

					// ���������еĺ�ͬ����Ҫ����ǰ�ķ���
					if ( status == 4 || status == 1 )
					{
						bool canPass = true; // �Ƿ�ͨ��
						// �ð汾�ĵ�ǰ��Ч�汾���
						string currentContractCode = BLL.ContractRule.GetCurrentContractVersionCode(contractCode);

						ViewState["SumMoney"] = BLL.MathRule.SumColumn(entity.Tables["ContractCost"],"Money");

						DataView dv = new DataView(entity.Tables["ContractCost"]);
						dgCostList.DataSource = dv;
						dgCostList.DataBind();

						// �������
						if ( ! canPass )
						{
							this.btnSave.Visible = false;
						}
					}
					else
					{
						this.tableList.Visible = false;
						this.dgCostList.Visible = false;

					}
				}
				entity.Dispose();

                if (this.up_sPMNameLower == "YefengPM".ToLower()) //��ͬ�����˺��Զ�����
                {
                    this.divContractIDEdit.Visible = false;
                    this.divContractIDView.Visible = true;

                    this.lblContractIDAutoCreate.Visible = (this.lblContractID.Text.Trim() == "");
                }
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"���غ�ͬ������ϸ����");
				Response.Write(Rms.Web.JavaScript.Alert(true, "���غ�ͬ������ϸ����" + ex.Message));

			}
		}

		private void dgCostList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			UserControls.InputCostBudgetDtl ucCostBudgetDtl;			

			switch ( e.Item.ItemType )
			{
				case ListItemType.Item:
					//��ʾ������͵�λ����
					ucCostBudgetDtl = (UserControls.InputCostBudgetDtl)e.Item.FindControl("ucCostBudgetDtl");
					((Label)e.Item.FindControl("lblCostName")).Text = ucCostBudgetDtl.CostName;
					((Label)e.Item.FindControl("lblPBSName")).Text = ucCostBudgetDtl.PBSName;
					break;

				case ListItemType.AlternatingItem:
					//��ʾ������͵�λ����
					ucCostBudgetDtl = (UserControls.InputCostBudgetDtl)e.Item.FindControl("ucCostBudgetDtl");
					((Label)e.Item.FindControl("lblCostName")).Text = ucCostBudgetDtl.CostName;
					((Label)e.Item.FindControl("lblPBSName")).Text = ucCostBudgetDtl.PBSName;
					break;

				case ListItemType.Footer:
					//��ʾ�ϼƽ��
					((Label)e.Item.FindControl("lblSumMoney")).Text = BLL.MathRule.GetDecimalShowString(ViewState["SumMoney"]);
					break;

				default:
					break;
			}


		}

		private void CloseWindow()
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write(Rms.Web.JavaScript.OpenerReload(false));
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
            if ((this.divContractIDEdit.Visible) && (this.txtContractID.Value.Trim() == ""))
			{
				Response.Write( Rms.Web.JavaScript.Alert(true,"����д��ͬ��� ��"));
				return;
			}

			if ( this.ContractDate.Value == "" )
			{
				Response.Write( Rms.Web.JavaScript.Alert(true,"����д��Чʱ�� ��"));
				return;
			}

			if ( this.txtContractPerson.Value == "" )
			{
				Response.Write( Rms.Web.JavaScript.Alert(true,"��ѡ������ ��"));
				return;
			}

			try
			{
				string contractCode = Request["ContractCode"] + "";

                //��ͬ�����˺��Զ�����
                if ((this.txtContractID.Value.Trim() == "") && (lblContractIDAutoCreate.Visible))
                {
                    this.txtContractID.Value = BLL.ContractRule.AutoCreateContractID(contractCode, this.up_sPMName);
                    this.lblContractID.Text = this.txtContractID.Value;
                }

				EntityData entity = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(contractCode);
				int status = entity.GetInt("Status");
				entity.CurrentRow["Status"] = 0;
				entity.CurrentRow["CheckPerson"]=base.user.UserCode;
				entity.CurrentRow["ContractID"]=this.txtContractID.Value;
				entity.CurrentRow["CheckOpinion"]=this.TextBoxCheckOpinion.Text;
				entity.CurrentRow["CheckDate"]=this.ContractDate.Value;
				entity.CurrentRow["ContractPerson"]=this.txtContractPerson.Value;
				if ( status != 4 )
				{
					if ( this.ContractDate.Value != "" )
						entity.CurrentRow["ContractDate"]=this.ContractDate.Value;
					else
						entity.CurrentRow["ContractDate"]=null;
				}

				// ����Ǳ������
				if ( status == 4)
				{
					string contractLabel = entity.GetString("ContractLabel");
					ContractStrategyBuilder sb = new ContractStrategyBuilder();
					sb.AddStrategy( new Strategy( ContractStrategyName.ContractLabel,contractLabel ));
					sb.AddStrategy( new Strategy( ContractStrategyName.Status,"0" ));
					string sql = sb.BuildMainQueryString();
					QueryAgent qa = new QueryAgent();
					EntityData con = qa.FillEntityData( "Contract",sql);
					qa.Dispose();
					string oldContractCode = con.GetString("ContractCode");
					EntityData oldEntity = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(oldContractCode);
					oldEntity.CurrentRow["Status"] = 6;
					DAL.EntityDAO.ContractDAO.UpdateStandard_Contract(oldEntity);

					//�޸�ԭ��ͬ���ĵ����º�ͬ

					EntityData entityTempDSB= DAL.EntityDAO.DocumentDAO.GetDocumentAllInfoByMainCode("000001",oldContractCode);
					foreach ( DataRow drDocument in entityTempDSB.CurrentTable.Rows )
						drDocument["code"]=contractCode;
					DAL.EntityDAO.DocumentDAO.UpdateDocumentConfig(entityTempDSB);
					entityTempDSB.Dispose();

					//�޸�ԭ��ͬ��Ͷ�굽�º�ͬ
					EntityData entityBidding=DAL.EntityDAO.ContractDAO.GetContractBiddingByContractCode(oldContractCode);
					foreach ( DataRow drBidding in entityBidding.CurrentTable.Rows)
						drBidding["ContractCode"]=contractCode;
					DAL.EntityDAO.ContractDAO.UpdateContractBidding(entityBidding);

					//�޸�ԭ��ͬ��Ͷ��Ĺ�Ӧ�̵��º�ͬ
					EntityData entitySupplier=DAL.EntityDAO.ContractDAO.GetBiddingSupplierByContractCode(oldContractCode);
					foreach ( DataRow drSupplier in entitySupplier.CurrentTable.Rows)
						drSupplier["ContractCode"]=contractCode;
					DAL.EntityDAO.ContractDAO.UpdateContractBiddingSupplier(entitySupplier);

					// ����ϸ, �������ӱ��е�AllocateCodeͨ��AllocateLabel �������º�ͬ��AllocateCode
					// ע�⣺ �������޸������ӱ����޸�����������Ҳ������ݡ�
					EntityData paymentItem = DAL.EntityDAO.PaymentDAO.GetPaymentItemByContractCode(oldContractCode);
					foreach ( DataRow drPaymentItem in paymentItem.CurrentTable.Rows)
					{
						string oldContractCostCode = (string)drPaymentItem["AllocateCode"];
						string contractCostLabel = (string) oldEntity.Tables["ContractCost"].Select(String.Format("ContractCostCode='{0}'",oldContractCostCode))[0]["ContractCostLabel"];
						string newContractCostCode = (string) entity.Tables["ContractCost"].Select(String.Format("ContractCostLabel='{0}'",contractCostLabel))[0]["ContractCostCode"];
						drPaymentItem["AllocateCode"] = newContractCostCode;
					}

					//�޸�ԭ��ͬ�ĸ����¼���º�ͬ
					EntityData entityPayment=DAL.EntityDAO.PaymentDAO.GetPaymentByContractCode(oldContractCode);
					foreach ( DataRow drPayment in entityPayment.CurrentTable.Rows)
						drPayment["ContractCode"]=contractCode;
					DAL.EntityDAO.PaymentDAO.UpdatePayment(entityPayment);

					DAL.EntityDAO.PaymentDAO.UpdatePaymentItem(paymentItem);
					paymentItem.Dispose();
					oldEntity.Dispose();

				}

				DAL.EntityDAO.ContractDAO.UpdateContract(entity);
				entity.Dispose();

				CloseWindow();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write( Rms.Web.JavaScript.Alert(true,"��˳���" + ex.Message ));
			}

		}

		protected void btnNoPass_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string ContractCode=Request["ContractCode"] + "";
				EntityData entity=DAL.EntityDAO.ContractDAO.GetContractByCode(ContractCode);
				//				int status = entity.GetInt("Status");
				entity.CurrentRow["Status"]=3;
				DAL.EntityDAO.ContractDAO.UpdateContract(entity);
				entity.Dispose();
				CloseWindow();

			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"δͨ��,�������");
				Response.Write( Rms.Web.JavaScript.Alert(true,"δͨ��,�������" + ex.Message ));

			}
		}


	}
}
