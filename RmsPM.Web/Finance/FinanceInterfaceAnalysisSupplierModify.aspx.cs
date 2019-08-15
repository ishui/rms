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
using RmsPM.DAL.QueryStrategy;
using RmsPM.BLL;

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// FinanceInterfaceAnalysisSupplierModify 的摘要说明。
	/// </summary>
	public partial class FinanceInterfaceAnalysisSupplierModify : PageBase
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
                this.txtSupplierCode.Value = Request.QueryString["SupplierCode"];
                this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
                this.txtIsGroup.Value = Request.QueryString["IsGroup"];
                this.txtSubjectSetCode.Value = Request.QueryString["SubjectSetCode"];
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

        private EntityData GetEntityData()
        {
            SupplierSubjectSetStrategyBuilder sb = new SupplierSubjectSetStrategyBuilder();
            sb.AddStrategy(new Strategy(SupplierSubjectSetStrategyName.SupplierCode, txtSupplierCode.Value));

            if (this.txtIsGroup.Value == "1")
            {
                sb.AddStrategy(new Strategy(SupplierSubjectSetStrategyName.ProjectCode, ""));
            }
            else
            {
                if (this.txtProjectCode.Value != "")
                    sb.AddStrategy(new Strategy(SupplierSubjectSetStrategyName.ProjectCode, this.txtProjectCode.Value));
            }

            if (this.txtSubjectSetCode.Value != "")
                sb.AddStrategy(new Strategy(SupplierSubjectSetStrategyName.SubjectSetCode, this.txtSubjectSetCode.Value));

            string sql = sb.BuildMainQueryString();

            QueryAgent qa = new QueryAgent();
            EntityData entity = qa.FillEntityData("SupplierSubjectSet", sql);
            qa.Dispose();

            return entity;
        }

		private void LoadData()
		{
			try
			{
                if (this.txtSupplierCode.Value == "")
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "未传入厂商编号"));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
					return;
				}

				this.lblSupplierName.Text = BLL.ProjectRule.GetSupplierName(this.txtSupplierCode.Value);

                EntityData entity = GetEntityData();
                this.ucInputSubjectSet.SubjectSetCode = this.txtSubjectSetCode.Value;
                this.ucInputSubjectSet.ProjectCode = this.txtProjectCode.Value;
                this.ucInputSubjectSet.IsGroup = BLL.ConvertRule.ToInt(this.txtIsGroup.Value);
                this.ucInputSubjectSet.LoadData(entity.CurrentTable);
                entity.Dispose();
            }
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
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
		private void SavaData()
		{
			try
			{
                //保存财务编码
                EntityData entity = GetEntityData();
                this.ucInputSubjectSet.SaveData(entity.CurrentTable, this.txtSupplierCode.Value);
                DAL.EntityDAO.ProjectDAO.SubmitAllSupplierSubjectSet(entity);
                entity.Dispose();
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

			return true;
		}

		/// <summary>
		/// 返回
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
            Response.Write("try {window.opener.Refresh();}");
            Response.Write("catch(e){window.opener.location = window.opener.location;}");
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

			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "保存失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
                return;
            }

            GoBack();
        }

	}
}
