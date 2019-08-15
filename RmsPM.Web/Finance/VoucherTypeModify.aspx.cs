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
using Rms.Web;

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// VoucherTypeModify 的摘要说明。
	/// </summary>
	public partial class VoucherTypeModify : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlAnchor lnkAddNew;
		protected System.Web.UI.HtmlControls.HtmlTextArea txtRemark;
		protected System.Web.UI.WebControls.TextBox a1;
	
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
			this.txtCode.Value = Request["Code"]+"";
			this.txtOldCode.Value = Request["Code"]+"";
		}

		private void LoadData()
		{

			string Code = this.txtCode.Value;
			if ( Code == "" )
				return;

			try
			{
				EntityData entity = DAL.EntityDAO.PaymentDAO.GetVoucherTypeByCode(Code);
				if ( entity.HasRecord())
				{
					this.txtName.Value = entity.GetString("Name");
				}
				entity.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示出错：" + ex.Message));
			}
		}

		private void CloseWindow()
		{
			Response.Write( Rms.Web.JavaScript.ScriptStart );
			Response.Write( Rms.Web.JavaScript.OpenerReload(false) );
			Response.Write( Rms.Web.JavaScript.WinClose(false) );
			Response.Write( Rms.Web.JavaScript.ScriptEnd );
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
		/// 有效性检查
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			if (this.txtCode.Value.Trim() == "")
			{
				Hint = "请输入凭证类型编号 ！";
				return false;
			}

			if (this.txtName.Value.Trim() == "")
			{
				Hint = "请输入凭证类型名称 ！";
				return false;
			}

			return true;
		}

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

				EntityData entity = DAL.EntityDAO.PaymentDAO.GetVoucherTypeByCode(this.txtOldCode.Value);

				DataRow dr = null;
				bool isNew = !entity.HasRecord();
				if (entity.HasRecord())
				{
					dr = entity.CurrentRow;
				}
				else
				{
					dr = entity.GetNewRecord();
					dr["Code"] = this.txtCode.Value;
					entity.AddNewRecord(dr);
				}

//				dr["Code"] = this.txtCode.Value;
				dr["Name"] = this.txtName.Value;

				DAL.EntityDAO.PaymentDAO.SubmitAllVoucherType(entity);
				entity.Dispose();

				//修改凭证编号
				if ((!isNew) && (this.txtCode.Value != this.txtOldCode.Value))
				{
					string sql = string.Format("update VoucherType set Code = '{0}' where Code = '{1}'", this.txtCode.Value, this.txtOldCode.Value);
					QueryAgent qa = new QueryAgent();
					try 
					{
						qa.ExecuteSql(sql);
					}
					finally 
					{
						qa.Dispose();
					}
				}

				this.txtOldCode.Value = this.txtCode.Value;

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "保存出错：" + ex.Message));
				return;
			}

			CloseWindow();

		}

		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string Code = this.txtOldCode.Value;

				/*
				bool canDelete = true;
				UnitStrategyBuilder sb = new UnitStrategyBuilder();
				sb.AddStrategy(new Strategy( UnitStrategyName.SubjectSetCode,subjectSetCode ));
				string sql = sb.BuildMainQueryString();
				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData("Unit",sql);
				qa.Dispose();

				if ( entity.HasRecord())
					canDelete = false;
				entity.Dispose();

				if ( ! canDelete )
				{
					Response.Write(Rms.Web.JavaScript.Alert(true,"还有公司使用这个凭证类型，不能删除 ！"));
					return;
				}
				*/

				EntityData entity = DAL.EntityDAO.PaymentDAO.GetVoucherTypeByCode(Code);
				DAL.EntityDAO.PaymentDAO.DeleteVoucherType(entity);
				entity.Dispose();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除出错：" + ex.Message));
				return;
			}

			CloseWindow();
		}

	}
}
