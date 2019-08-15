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
	/// ContractMaterialModify 的摘要说明。
	/// </summary>
	public partial class ContractMaterialModify : PageBase
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
			this.dgList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgList_DeleteCommand);
			this.dgList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemDataBound);

		}
		#endregion

		private void IniPage()
		{
			try
			{
				string projectCode = Request["ProjectCode"]+"";
				string contractCode = Request["ContractCode"]+"";

				ArrayList ar = user.GetResourceRight(contractCode,"Contract");
                if (!ar.Contains("050141"))  //合同材料需求修改
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

//				AddEmptyRow(entity,contractCode,"ContractMaterial","ContractMaterialCode",5);

				// 基本信息
				entity.SetCurrentTable("Contract");
				this.lblContractID.Text = entity.GetString("ContractID");
				this.lblContractName.Text = entity.GetString("ContractName");
                this.lblSystemGroup.Text = BLL.SystemGroupRule.GetSystemGroupName(entity.GetString("Type"));
				this.lblUnit.Text = BLL.SystemRule.GetUnitName(entity.GetString("UnitCode"));

				//材料需求
				entity.SetCurrentTable("ContractMaterial");
				BindList(entity.CurrentTable);

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
		/// 显示材料需求
		/// </summary>
		private void BindList(DataTable tb)
		{
			try 
			{
				DataView dv = new DataView(tb, "", "", DataViewRowState.CurrentRows);

				ViewState["SumQty"] = BLL.MathRule.SumColumn(tb,"Qty");
				this.dgList.DataSource = dv;
				this.dgList.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示材料需求出错：" + ex.Message));
			}
		}

		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Footer) 
			{
				//显示合计
                ((Label)e.Item.FindControl("lblSumQty")).Text = BLL.MathRule.GetDecimalShowString(ViewState["SumQty"]);
			}
		}

		/// <summary>
		/// 删除材料需求
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void dgList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				string msg = SaveToSession();

				string codeTemp = e.Item.Cells[0].Text;
                int materialcode=new int();
				EntityData entity = (EntityData)Session["ContractEntity"];
				foreach ( DataRow dr in entity.Tables["ContractMaterial"].Select(String.Format( "ContractMaterialCode='{0}'" ,codeTemp)))
				{
                    materialcode = RmsPM.BLL.ConvertRule.ToInt(dr["MaterialCode"]);
					dr.Delete();

				}
                //删除相关月度计划的材料
                foreach (DataRow dr in entity.Tables["ContractMaterialMonth"].Select(String.Format("MaterialCode={0}", materialcode)))
                {
                    dr.Delete();
                }


				BindList(entity.Tables["ContractMaterial"]);

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
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除材料需求出错：" + ex.Message));
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

				foreach ( DataGridItem li in this.dgList.Items)
				{
					string ContractMaterialCode = li.Cells[0].Text;

					foreach ( DataRow dr in entity.Tables["ContractMaterial"].Select(String.Format( "ContractMaterialCode='{0}'"  ,ContractMaterialCode )))
					{
                        dr["MaterialCode"] = BLL.ConvertRule.ToInt(((RmsPM.Web.UserControls.InputMaterial)li.FindControl("InputMaterial")).Value);
                        dr["Qty"] = BLL.ConvertRule.ToDecimalObj(((WebNumericEdit)li.FindControl("txtQty")).Value);
                        dr["Price"] = BLL.ConvertRule.ToDecimalObj(((WebNumericEdit)li.FindControl("txtPrice")).Value);
                        dr["Money"] = Math.Round(BLL.ConvertRule.ToDecimal(dr["Qty"]) * BLL.ConvertRule.ToDecimal(dr["Price"]), 2);
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

        private void AddEmptyRow(EntityData entity, string contractCode, string tableName, string keyColumnName, int rows)
		{
			for ( int i=0;i<rows;i++)
			{
				DataRow dr = entity.GetNewRecord(tableName);
				dr[keyColumnName]=DAL.EntityDAO.SystemManageDAO.GetNewSysCode(keyColumnName);
				dr["ContractCode"]=contractCode;

				entity.AddNewRecord(dr,tableName);
			}
		}

		/// <summary>
		/// 新增材料需求
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnNewItem_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string contractCode = this.txtContractCode.Value;
				string msg = SaveToSession();
				EntityData entity = (EntityData)Session["ContractEntity"];
				AddEmptyRow(entity,contractCode,"ContractMaterial","ContractMaterialCode",1);

				BindList(entity.Tables["ContractMaterial"]);

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
				Response.Write(Rms.Web.JavaScript.Alert(true, "新增材料需求出错：" + ex.Message));
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

				// 清除材料为空的记录
				foreach ( DataRow dr in entity.Tables["ContractMaterial"].Select("","",DataViewRowState.CurrentRows  ))
				{
					if (BLL.ConvertRule.ToInt(dr["MaterialCode"]) == 0 && BLL.ConvertRule.ToString(dr["Qty"]) == "")
					{
						dr.Delete();
						continue;
					}

                    if (BLL.ConvertRule.ToInt(dr["MaterialCode"]) == 0)
                    {
                        ErrMsg = "请填写材料名称！";
                        break;
                    }

                    if (BLL.ConvertRule.ToDecimal(dr["Qty"]) == 0)
                    {
                        ErrMsg = "请填写数量！";
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
