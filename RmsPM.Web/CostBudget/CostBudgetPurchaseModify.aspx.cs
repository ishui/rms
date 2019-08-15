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
	/// CostBudgetPurchaseModify 的摘要说明。
	/// </summary>
	public partial class CostBudgetPurchaseModify : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
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

		}
		#endregion

		private void IniPage()
		{
			try
			{
//				this.txtPurchaseFlowCode.Value = Request.QueryString["PurchaseFlowCode"];
				this.txtPurchaseFlowDetailCode.Value = Request.QueryString["PurchaseFlowDetailCode"];
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtCostBudgetSetCode.Value = Request.QueryString["CostBudgetSetCode"];
				this.txtCostCode.Value = Request.QueryString["CostCode"];

				this.ucCostBudgetDtl.ProjectCode = this.txtProjectCode.Value;
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadData()
		{
			try
			{
				string PurchaseFlowDetailCode = this.txtPurchaseFlowDetailCode.Value;

				//新增时必须传入项目代码、预算设置表编号、费用项编号
				if (PurchaseFlowDetailCode == "")
				{
					if ((this.txtProjectCode.Value == "") || (this.txtCostBudgetSetCode.Value == "") || (this.txtCostCode.Value == ""))
					Response.Write(Rms.Web.JavaScript.Alert(true, "未传入项目代码或预算设置表编号或费用项编号，不能新增"));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
					return;
				}

				if ( PurchaseFlowDetailCode != "")
				{
					//修改

					//明细
					EntityData entityDtl = DAL.EntityDAO.PurchaseFlowDAO.GetPurchaseFlowDetailByCode(PurchaseFlowDetailCode);
					if ( entityDtl.HasRecord())
					{
						DataRow drDtl = entityDtl.CurrentRow;

						this.txtPurchaseFlowCode.Value = entityDtl.GetString("PurchaseCode");
						this.txtMoney.Value = BLL.StringRule.BuildShowNumberString(entityDtl.GetDecimal("Money"));
//						this.txtMoney.Value = BLL.StringRule.BuildShowNumberString(entityDtl.GetDecimal("Money"), "#,##0.####");
						this.txtDescription.Value = entityDtl.GetString("Description");

						this.txtCostCode.Value = entityDtl.GetString("CostCode");
						this.txtCostBudgetSetCode.Value = entityDtl.GetString("CostBudgetSetCode");

						//主表
						EntityData entity = DAL.EntityDAO.PurchaseFlowDAO.GetPurchaseFlowByCode(this.txtPurchaseFlowCode.Value);
						if ( entity.HasRecord())
						{
							DataRow dr = entity.CurrentRow;

							this.txtPurpose.Value = entity.GetString("Purpose");
							this.txtProjectCode.Value = entity.GetString("ProjectCode");

						}
						else 
						{
							Response.Write(Rms.Web.JavaScript.Alert(true, "合同计划不存在"));
							Response.Write(Rms.Web.JavaScript.WinClose(true));
							return;
						}
						entity.Dispose();

					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "合同计划明细不存在"));
						Response.Write(Rms.Web.JavaScript.WinClose(true));
						return;
					}
					entityDtl.Dispose();

				}
				else 
				{
					//新增

					//缺省值
//					this.dtPayDate.Value = DateTime.Today.ToString("yyyy-MM-dd");

				}

				//取费用项信息
				EntityData entityCBS = DAL.EntityDAO.CBSDAO.GetCBSByCode(this.txtCostCode.Value);
				if (entityCBS.HasRecord()) 
				{
					this.lblSortID.Text = entityCBS.GetString("SortID");
					this.lblCostName.Text = entityCBS.GetString("CostName");
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, string.Format("费用项{0}不存在", this.txtCostCode.Value)));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
					return;
				}
				entityCBS.Dispose();
			}
			catch (  Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示数据出错：" + ex.Message));
			}
		
		}

		/// <summary>
		/// 保存
		/// </summary>
		private void SavaData()
		{
			try
			{
				//主表
				EntityData entity = DAL.EntityDAO.PurchaseFlowDAO.GetPurchaseFlowByCode(this.txtPurchaseFlowCode.Value);
				DataRow dr = null;
				if (!entity.HasRecord()) //新增
				{
					this.txtPurchaseFlowCode.Value = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PurchaseFlow");

					dr = entity.CurrentTable.NewRow();
					dr["PurchaseFlowCode"] = this.txtPurchaseFlowCode.Value;
					dr["ProjectCode"] = this.txtProjectCode.Value;
					dr["CreateDate"] = DateTime.Today;

					dr["State"] = 0;

					entity.CurrentTable.Rows.Add(dr);
				}
				else
				{
					dr = entity.CurrentRow;
				}

				dr["Purpose"] = this.txtPurpose.Value;

				//明细
				EntityData entityDtl = DAL.EntityDAO.PurchaseFlowDAO.GetPurchaseFlowDetailByCode(this.txtPurchaseFlowDetailCode.Value);
				DataRow drDtl = null;
				if (!entityDtl.HasRecord())
				{
					this.txtPurchaseFlowDetailCode.Value = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PurchaseFlowDetail");

					drDtl = entityDtl.CurrentTable.NewRow();
					drDtl["PurchaseFlowCode"] = this.txtPurchaseFlowCode.Value;

					entityDtl.CurrentTable.Rows.Add(drDtl);
				}
				else 
				{
					drDtl = entityDtl.CurrentRow;
				}

				drDtl["Money"] = BLL.ConvertRule.ToDecimal(this.txtMoney.Value);
				drDtl["Description"] = this.txtDescription.Value;

				//提交
				using(StandardEntityDAO dao=new StandardEntityDAO("PurchaseFlow"))
				{
					dao.BeginTrans();
					try
					{
						//提交主表
						dao.SubmitEntity(entity);

						//提交明细
						dao.EntityName = "PurchaseFlowDetail";
						dao.SubmitEntity(entityDtl);

						dao.CommitTrans();
					}
					catch(Exception ex)
					{
						try 
						{
							//RollBackTrans会报错：此 SqlTransaction 已完成；它再也无法使用
							dao.RollBackTrans();
						}
						catch 
						{
						}

						throw ex;
					}
				}

				entity.Dispose();
				entityDtl.Dispose();
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// 有效性检查
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			if (this.txtPurpose.Value.Trim() == "")
			{
				Hint = "请输入合同名称";
				return false;
			}

			if (this.txtMaterialName.Value.Trim() == "")
			{
				Hint = "请输入物资名称";
				return false;
			}

			return true;
		}

		/// <summary>
		/// 返回
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);

			Response.Write("try {window.opener.RefreshPurchase();}");
			Response.Write("catch(e){window.opener.location = window.opener.location;}");

			//			Response.Write("window.opener.location = window.opener.location;");
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		/// <summary>
		/// 保存
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string Hint = "";
				if (!CheckValid(ref Hint)) 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
					return;
				}

				SavaData();

				GoBack();
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "保存失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

	}
}
