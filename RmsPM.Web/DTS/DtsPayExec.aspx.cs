using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rms.ORMap;
using Rms.Web;
using RmsPM.DAL;

namespace RmsPM.Web.DTS
{
	/// <summary>
	/// DtsPaySub 的摘要说明。
	/// </summary>
	public partial class DtsPaySub : PageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				DtsContinue();
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

		private void DtsContinue() 
		{
			string act = "" + Request["act"];
			if (act == "")
				return;

			if (Session["DtsSale"] == null) 
			{
				return;
			}

			string server = Request["server"];
			string database = Request["database"];

			DtsInfo dts = (DtsInfo)Session["DtsSale"];
			string ErrMess = "";
			string IsEof = "1";

			if (!dts.EOF) 
			{
				dts.CurrentIndex = dts.CurrentIndex + 1;
				DataRow dr = dts.DataSource.Rows[dts.CurrentIndex];
				string case_id = dr["case_id"].ToString();
				string case_name = dr["case_name"].ToString();

				try 
				{
//					BLL.DtsPayRule.DtsPaySingle(server, database, case_id);

                    BLL.DtsPayRule.DtsPaySingleByClient(case_id);

                    /*
                    if (dts.DataSource.TableName.ToUpper() == "CLIENT") //按客户导入
                    {
                        BLL.DtsPayRule.DtsPaySingleByClient(case_id);
                    }
                    else //按合同导入
                    {
                        BLL.DtsPayRule.DtsPaySingleByContract(case_id);
                    }
                    */
				}
				catch (Exception ex)
				{
					ErrMess = "“" + case_name + "”:" + ex.Message;
					dts.AddErr(ErrMess);
				}

				if (!dts.EOF) 
				{
					IsEof = "0";
				}

//				((DtsProgress)this.DtsProgress).SetCurrentIndex(dts.CurrentIndex);

			}

			//部分字符会引起js出错，替换掉
			ErrMess = ErrMess.Replace("'", " ");
			ErrMess = ErrMess.Replace("\n", " ");

			string script = "<script language='javascript'>\n"
				+ string.Format("window.parent.DtsExecReturn('{0}', '{1}', {2}, {3});\n", ErrMess, IsEof, dts.CurrentIndex, dts.ErrCount)
				+ "</script>\n";
			Page.RegisterStartupScript("DtsExecReturn", script);

		}

	}
}
