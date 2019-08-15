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
using Rms.Web;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.Construct
{
	/// <summary>
	/// RiskIndexModify 的摘要说明。
	/// </summary>
	public partial class RiskIndexModify : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage() 
		{
			try 
			{
//				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
				this.txtIndexCode.Value = Request.QueryString["IndexCode"];
				this.txtAct.Value = Request.QueryString["Action"];

				if (this.txtIndexCode.Value == "") 
				{
					this.btnDelete.Visible = false;
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadData()
		{
			try
			{
				string code = this.txtIndexCode.Value;
				if (code != "") 
				{
					EntityData entity = RmsPM.DAL.EntityDAO.ConstructDAO.GetRiskIndexByCode(code);
					if (entity.HasRecord())
					{
						this.txtIndexName.Value = entity.GetString("IndexName");
						this.txtIndexLevel.Value = entity.GetIntString("IndexLevel");
						this.chkIsDefault.Checked = (entity.GetInt("IsDefault") == 1);
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "风险指数不存在"));
					}
					entity.Dispose();
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示出错：" + ex.Message));
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

		/// <summary>
		/// 保存
		/// </summary>
		/// <param name="code"></param>
		/// <param name="parentCode"></param>
		private void SavaData()
		{
			try
			{				
				string IndexCode = this.txtIndexCode.Value;

				EntityData entity = DAL.EntityDAO.ConstructDAO.GetRiskIndexByCode(IndexCode);
				DataRow dr;
				bool isNew = !entity.HasRecord();


				if (isNew)
				{
					dr = entity.GetNewRecord();

					IndexCode = SystemManageDAO.GetNewSysCode("RiskIndexCode");
					this.txtIndexCode.Value = IndexCode;
					dr["IndexCode"] = IndexCode;
					entity.CurrentTable.Rows.Add(dr);
				}
				else
				{
					dr=entity.CurrentRow;
				}
				
				dr["IndexName"] = this.txtIndexName.Value;
				dr["IndexLevel"] = BLL.ConvertRule.ToInt(this.txtIndexLevel.Value);

				if (this.chkIsDefault.Checked) 
				{
					dr["IsDefault"] = 1;
				}
				else 
				{
					dr["IsDefault"] = 0;
				}
				
				DAL.EntityDAO.ConstructDAO.SubmitAllRiskIndex(entity);
				entity.Dispose();

				//更新其它记录的默认值
				if (this.chkIsDefault.Checked) 
				{
					RiskIndexStrategyBuilder sb = new RiskIndexStrategyBuilder();

					sb.AddStrategy( new Strategy( RiskIndexStrategyName.IsDefault,"1"));
					sb.AddStrategy( new Strategy( RiskIndexStrategyName.ExceptIndexCode,IndexCode));

					string sql = sb.BuildMainQueryString();

					QueryAgent qa = new QueryAgent();
					entity = qa.FillEntityData( "RiskIndex",sql );
					qa.Dispose();

					if (entity.HasRecord()) 
					{
						foreach(DataRow drTemp in entity.CurrentTable.Rows) 
						{
							drTemp["IsDefault"] = 0;
						}

						DAL.EntityDAO.ConstructDAO.SubmitAllRiskIndex(entity);
					}

				}
					
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

			if (this.txtIndexName.Value.Trim() == "") 
			{
				Hint = "请输入风险指数";
				return false;
			}

			if (this.txtIndexLevel.Value.Trim() == "") 
			{
				Hint = "请输入风险级别";
				return false;
			}

			if ( txtIndexLevel.Value != "" )
			{
				if ( ! Rms.Check.StringCheck.IsInt(txtIndexLevel.Value))
				{
					Hint = "风险级别必须是整数 ！ ";
					return false;
				}
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

				SavaData();
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "保存失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
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
			Response.Write(Rms.Web.JavaScript.WinClose(false));
//			string FromUrl = this.txtFromUrl.Value.Trim();
//			if (FromUrl != "") 
//			{
//				Response.Write(string.Format("window.location = '{0}';", FromUrl));
//			}
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				BLL.ConstructRule.DeleteRiskIndex(this.txtIndexCode.Value);
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "删除失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return;
			}

			GoBack();
		}

	}
}
