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
using AspWebControl;
using Infragistics.WebUI.WebDataInput;

namespace RmsPM.Web.ExchangeRate
{
	/// <summary>
	/// ExchangeRateModify 的摘要说明。
	/// </summary>
	public partial class ExchangeRateModify : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton btnHidSummaryChange;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面

			if ( !IsPostBack)
			{
				if ( ! user.HasRight("2302") && ! user.HasRight("2303"))
				{
					Response.Redirect( "../RejectAccess.aspx" );
					Response.End();
				}

				IniPage();
				LoadData();

				// 权限
				if ( ! user.HasRight("2302")) //汇率新增
				{
					this.btnAddDtl.Visible = false;
				}

				if ( ! user.HasRight("2304")) //汇率删除
				{
					this.btnDelete.Visible = false;
				}
                
			}
		}

		private void IniPage()
		{
			string ud_sAction = Request["act"]+"";
			try
			{
				switch ( ud_sAction )
				{
					case "Add":
						this.btnDelete.Visible = false;
						break;
					case  "Modify" :
						dgExchangeRateList.Columns[1].Visible = false;
						dgExchangeRateList.Columns[9].Visible = false;
						break;
				}

			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"初始化页面失败。");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面失败：" + ex.Message));
			}
		}

		private void LoadData()
		{
			string ud_sAction = Request["act"]+"";
			EntityData entity = GetEntity();

			if ( ud_sAction == "Add" )
			{
				AddNewEmptyRows(entity,5);
			}
			dgExchangeRateListBind(entity);
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
			this.dgExchangeRateList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgExchangeRateList_ItemDataBound);
			this.dgExchangeRateList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgExchangeRateList_DeleteCommand);

		}
		#endregion

		private EntityData GetEntity()
		{
			EntityData entity = new EntityData("ExchangeRate");

			entity.SetCurrentTable("ExchangeRate");

			foreach(DataGridItem ud_dgItem in dgExchangeRateList.Items )
			{
				DataRow dr = entity.CurrentTable.NewRow();
				dr["ExchangeRateCode"] = ud_dgItem.Cells[0].Text;
				dr["MoneyType"] = ((HtmlSelect)ud_dgItem.FindControl("sltMoneyType")).Value;
                if (dr["MoneyType"].ToString() != string.Empty)
                {
                    dr["RemittanceBuy"] = Decimal.Parse(((TextBox)ud_dgItem.FindControl("txtRemittanceBuy")).Text);
                    dr["CashBuy"] = Decimal.Parse(((TextBox)ud_dgItem.FindControl("txtCashBuy")).Text);
                    dr["RemittanceSell"] = Decimal.Parse(((TextBox)ud_dgItem.FindControl("txtRemittanceSell")).Text);
                    dr["CashSell"] = Decimal.Parse(((TextBox)ud_dgItem.FindControl("txtCashSell")).Text);
                    dr["RemittanceAverage"] = Decimal.Parse(((TextBox)ud_dgItem.FindControl("txtRemittanceAverage")).Text);
                    string ud_sCreateDate = ((AspWebControl.Calendar)ud_dgItem.FindControl("dtCreateDate")).Value;
                    if (ud_sCreateDate != "")
                    {
                        dr["CreateDate"] = ud_sCreateDate;
                    }
                    dr["Status"] = 0;

                    entity.CurrentTable.Rows.Add(dr);
                }
			}

			return entity;

		}

		private void InitMoneyType(System.Web.UI.HtmlControls.HtmlSelect pm_sltControl)
		{
			BLL.PageFacade.LoadDictionarySelect(pm_sltControl,"币种","");
			foreach(ListItem ud_Item in pm_sltControl.Items)
			{
				if ( ud_Item.Value.IndexOf("人民币") >= 0 )
				{
					pm_sltControl.Items.Remove(ud_Item);
					break;
				}
			}

		}

		private void dgExchangeRateList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			switch ( e.Item.ItemType )
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					System.Web.UI.HtmlControls.HtmlSelect ud_sltControl = (System.Web.UI.HtmlControls.HtmlSelect)e.Item.FindControl("sltMoneyType");

					InitMoneyType(ud_sltControl);
					break;
				default:
					break;
			}
		}

		private string ClearEmptyRows(EntityData entity)
		{
			string ErrMsg = "";
			entity.SetCurrentTable("ExchangeRate");
			try
			{
				foreach( DataRow dr in entity.CurrentTable.Select("","",System.Data.DataViewRowState.CurrentRows))
				{
					if ( dr["MoneyType"].ToString() != "" )
					{
						if ( dr["CreateDate"].ToString() == "" )
						{
							ErrMsg = "请输入日期！";
							break;
						}

						if ( (Decimal)dr["CashBuy"] == Decimal.Zero && (Decimal)dr["CashSell"] == Decimal.Zero &&
							(Decimal)dr["RemittanceBuy"] == Decimal.Zero && (Decimal)dr["RemittanceSell"] == Decimal.Zero &&
							(Decimal)dr["RemittanceAverage"] == Decimal.Zero )
						{
							ErrMsg = "请至少输入一项汇率！";
							break;
						}

						if ( (Decimal)dr["RemittanceBuy"] != Decimal.Zero && (Decimal)dr["RemittanceSell"] != Decimal.Zero )
						{ 
							if ( (Decimal)dr["RemittanceAverage"] != Decimal.Zero && (Decimal)dr["RemittanceAverage"] != ((Decimal)dr["RemittanceBuy"] + (Decimal)dr["RemittanceSell"])/2  )
							{
								ErrMsg = "中间价应为\"现汇买入价\"与\"现汇卖出价\"的平均值！";
								break;
							}

							if ( (Decimal)dr["RemittanceAverage"] == Decimal.Zero )
							{
								dr["RemittanceAverage"] = ((Decimal)dr["RemittanceBuy"] + (Decimal)dr["RemittanceSell"])/2 ;
							}

						}
					}
					else
					{
						if ( dr["CreateDate"].ToString() == "" )
						{
							ErrMsg = "请输入日期！";
							break;
						}

						if ( (Decimal)dr["CashBuy"] != Decimal.Zero || (Decimal)dr["CashSell"] != Decimal.Zero ||
							(Decimal)dr["RemittanceBuy"] != Decimal.Zero || (Decimal)dr["RemittanceSell"] != Decimal.Zero ||
							(Decimal)dr["RemittanceAverage"] != Decimal.Zero )
						{
							ErrMsg = "请输入币种！";
							break;
						}

						dr.Delete();
					}
				}

				return ErrMsg;
			}
			catch( Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "清除空记录出错：" + ex.Message));
				throw ex;
			}
		}

		private void AddNewEmptyRows(EntityData entity,int pm_iRows)
		{
			for ( int i=0;i<pm_iRows;i++ )
			{
				DataRow drAdd = entity.Tables["ExchangeRate"].NewRow();

				drAdd["ExchangeRateCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ExchangeRateCode");
				drAdd["Status"] = 0;
				drAdd["CreateDate"] = DateTime.Now.Date;

				entity.Tables["ExchangeRate"].Rows.Add(drAdd);
			}
		}

		private void dgExchangeRateListBind(EntityData entity)
		{
			dgExchangeRateList.DataSource = entity.Tables["ExchangeRate"];
			dgExchangeRateList.DataBind();
		}

		protected void btnAddDtl_ServerClick(object sender, System.EventArgs e)
		{
			EntityData entity = GetEntity();

			AddNewEmptyRows(entity,5);

			dgExchangeRateListBind(entity);
		}

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				EntityData entity = GetEntity();
	
				string ErrMsg = ClearEmptyRows(entity);

				if ( ErrMsg != "" )
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, ErrMsg));

				}
				else
				{
					DAL.EntityDAO.ExchangeRateDAO.SubmitAllExchangeRate(entity);
					GoBack();
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "保存记录出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 删除单一汇率
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void dgExchangeRateList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				EntityData entity = GetEntity();

				string ud_sExchangeRateCode = e.Item.Cells[0].Text;

				foreach ( DataRow dr in entity.Tables["ExchangeRate"].Select(String.Format( "ExchangeRateCode='{0}'" ,ud_sExchangeRateCode)))
				{
					dr.Delete();
				}

				dgExchangeRateListBind(entity);
				entity.Dispose();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除单一汇率出错：" + ex.Message));
			}
		}

		
		/// <summary>
		/// 返回
		/// </summary>
		private void GoBack() 
		{


			Response.Write(Rms.Web.JavaScript.ScriptStart);

			Response.Write("window.opener.location = window.opener.location;");
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
		
		}
	}
}
