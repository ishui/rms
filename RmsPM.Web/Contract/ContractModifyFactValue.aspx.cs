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
using RmsPM.BLL;
using RmsPM.DAL.QueryStrategy;
using Infragistics.WebUI.WebDataInput;

namespace RmsPM.Web.Contract
{
	/// <summary>
	/// ContractModifyFactValue 的摘要说明。
	/// </summary>
	public partial class ContractModifyFactValue : PageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if ( !IsPostBack )
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
			this.dgFactValueList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgFactValueList_DeleteCommand);
			this.dgFactValueList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgFactValueList_ItemDataBound);

		}
		#endregion

		private void IniPage()
		{
			try
			{
				string projectCode = Request["ProjectCode"]+"";
				string contractCode = Request["ContractCode"]+"";

				ArrayList ar = user.GetResourceRight(contractCode,"Contract");
				if ( ! ar.Contains("050112"))
				{
					Response.Redirect( "../RejectAccess.aspx" );
					Response.End();
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog( this.ToString(),ex,"初始化页面错误。");
                Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面错误：" + ex.Message));
			}
		}

		private void LoadData()
		{
			try
			{
				string contractCode=Request.QueryString["ContractCode"]+"";
				this.txtContractCode.Value = contractCode;
				string projectCode = Request["ProjectCode"] + "";
				EntityData entity=null;

				entity=DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(contractCode);

				AddNewValueEmptyRow(entity,contractCode,"ContractProduction","ContractProductionCode",5,1);

				// 基本信息
				entity.SetCurrentTable("Contract");
				this.lblRemark.Text = entity.GetString("Remark");
				this.lblContractID.Text = entity.GetString("ContractID");
				this.lblContractName.Text = entity.GetString("ContractName");
				this.lblContractPersonName.Text = BLL.SystemRule.GetUserName(entity.GetString("ContractPerson"));
                this.lblSystemGroup.Text = BLL.SystemGroupRule.GetSystemGroupName(entity.GetString("Type"));
				this.lblUnit.Text = BLL.SystemRule.GetUnitName(entity.GetString("UnitCode"));
				this.lblThirdParty.Text = entity.GetString("ThirdParty");

				//合同产值
				entity.SetCurrentTable("ContractProduction");

				//实际
				BindFactValueList(entity.CurrentTable);

//				Session["ContractAct"]=action;
				Session["ContractEntity"]=entity;
				entity.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog( this.ToString(),ex,"加载合同数据错误");
				Response.Write(Rms.Web.JavaScript.Alert(true, "加载合同数据出错：" + ex.Message));
			}

		}

		/// <summary>
		/// 显示合同产值（约定、实际）
		/// </summary>
		private void BindFactValueList(DataTable tb)
		{
			try 
			{
				DataView dv = new DataView(tb, "", "", DataViewRowState.CurrentRows);

				dv.RowFilter = "IsFact=1";
				ViewState["SumFactValue"] = BLL.MathRule.SumColumn(tb.Select("IsFact=1"),"ProductionValue");
				this.dgFactValueList.DataSource = dv;
				this.dgFactValueList.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示实际产值出错：" + ex.Message));
			}
		}

		private void dgFactValueList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Footer) 
			{
				//显示合计实际产值
				((Label)e.Item.FindControl("lblSumFactValue")).Text = BLL.MathRule.GetDecimalShowString(ViewState["SumFactValue"]);
			}
		}

		/// <summary>
		/// 删除实际产值
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void dgFactValueList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				string msg = SaveToSession();

				string contractCode = this.txtContractCode.Value;
				string action=Request.QueryString["Act"]+"";
				string projectCode = Request["ProjectCode"] + "";

				string codeTemp = e.Item.Cells[0].Text;

				EntityData entity = (EntityData)Session["ContractEntity"];
				foreach ( DataRow dr in entity.Tables["ContractProduction"].Select(String.Format( "ContractProductionCode='{0}'" ,codeTemp)))
				{
					dr.Delete();
				}

				BindFactValueList(entity.Tables["ContractProduction"]);

				Session["ContractEntity"]=entity;
				entity.Dispose();
				if ( msg != "" )
				{
					Response.Write( Rms.Web.JavaScript.Alert(true,msg));
				}

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除约定产值出错：" + ex.Message));
			}
		}

		private string  SaveToSession()
		{

			string alertMsg = "";
			try
			{
				string contractCode = this.txtContractCode.Value;
				string projectCode = Request["ProjectCode"] + "";
				

				EntityData entity = (EntityData)Session["ContractEntity"];

				foreach ( DataGridItem li in this.dgFactValueList.Items)
				{
					string ContractProductionCode = li.Cells[0].Text;

					string FactProductionDate = ((AspWebControl.Calendar)li.FindControl("dtFactProductionDate")).Value;
					WebNumericEdit txtFactValue = (WebNumericEdit)li.FindControl("txtFactValue");


					foreach ( DataRow dr in entity.Tables["ContractProduction"].Select(String.Format( "ContractProductionCode='{0}'"  ,ContractProductionCode )))
					{
						dr["ProductionValue"] = txtFactValue.ValueDecimal;

						if ( FactProductionDate != "" )
							dr["ProductionDate"] = FactProductionDate;
						else
							dr["ProductionDate"] = System.DBNull.Value;
					}
				}

				Session["ContractEntity"]=entity;
				entity.Dispose();
				
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "保存出错：" + ex.Message));
			}
			return alertMsg;

		}

		private void AddNewValueEmptyRow( EntityData entity , string contractCode, string tableName, string keyColumnName, int rows ,int IsFact )
		{
			for ( int i=0;i<rows;i++)
			{
				DataRow dr = entity.GetNewRecord(tableName);
				dr[keyColumnName]=DAL.EntityDAO.SystemManageDAO.GetNewSysCode(keyColumnName);
				dr["ContractCode"]=contractCode;

				if ( IsFact == 0) 
				{
					//约定产值
					dr["IsFact"] = 0;
				}
				else
				{
					//实际产值
					dr["IsFact"] = 1;
				}

				entity.AddNewRecord(dr,tableName);
			}
		}

		/// <summary>
		/// 新增实际产值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnNewFactValueItem_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string contractCode = this.txtContractCode.Value;
				string msg = SaveToSession();
				EntityData entity = (EntityData)Session["ContractEntity"];
				AddNewValueEmptyRow(entity,contractCode,"ContractProduction","ContractProductionCode",5,1);

				BindFactValueList(entity.Tables["ContractProduction"]);

				Session["ContractEntity"]=entity;
				entity.Dispose();
				if ( msg != "" )
				{
					Response.Write( Rms.Web.JavaScript.Alert(true,msg));
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "新增实际产值出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 清除空记录
		/// </summary>
		private string ClearData()
		{
			try
			{
				string ErrMsg = "";
				string contractCode = this.txtContractCode.Value;
				EntityData entity = (EntityData)Session["ContractEntity"];

				// 清除产值为空的记录
				foreach ( DataRow dr in entity.Tables["ContractProduction"].Select("","",DataViewRowState.CurrentRows  ))
				{
					if (BLL.ConvertRule.ToString(dr["ProductionValue"]) == "0" && BLL.ConvertRule.ToString(dr["ProductionDate"]) == "")
					{
						dr.Delete();
						continue;
					}

					if (BLL.ConvertRule.ToString(dr["ProductionValue"]) == "0" || BLL.ConvertRule.ToString(dr["ProductionDate"]) == "" )
					{
						if ( BLL.ConvertRule.ToInt(dr["IsFact"]) == 0 )
						{
							ErrMsg = "请将约定产值填写完整！";
						}
						else
						{
							ErrMsg = "请将实际产值填写完整！";
						}
						break;
					}
				}

				Session["ContractEntity"]=entity;
				entity.Dispose();
				return ErrMsg;
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "清除空记录出错：" + ex.Message));
				return "清除空记录出错：" + ex.Message;
			}
		}


		/// <summary>
		/// 保存
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			string contractCode = this.txtContractCode.Value;
			string action=(string)Session["ContractAct"];
			string projectCode = Request["ProjectCode"] + "";
			try
			{
				string unitCode ="";
				
				string msg = SaveToSession();
				if ( msg != "" )
				{
					Response.Write( Rms.Web.JavaScript.Alert(true,msg));
					return;
				}

				string ClearMsg = ClearData();
				if ( ClearMsg != "" )
				{
					Response.Write( Rms.Web.JavaScript.Alert(true,ClearMsg));
					return;
				}


				EntityData entity = (EntityData)Session["ContractEntity"];
				entity.SetCurrentTable("Contract");
				unitCode = entity.GetString("UnitCode");
				DAL.EntityDAO.ContractDAO.SubmitAllStandard_Contract(entity);
				entity.Dispose();

				Session["ContractEntity"]=null;
				Session["ContractAct"]=null;
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "保存出错：" + ex.Message));
				return;
			}

			GoBack();
		}

		/// <summary>
		/// 返回
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);

			Response.Write("window.opener.location = window.opener.location;");
			//			Response.Write("window.parent.location.href='../Contract/ContractInfo.aspx?ContractCode="+contractCode+"&projectCode=" + projectCode + "';");
			Response.Write(Rms.Web.JavaScript.OpenerReload(false));
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}
	}
}
