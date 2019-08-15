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
using System.IO;

using Rms.Web;
using Rms.ORMap;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// CBSTempletOut 的摘要说明。
	/// </summary>
	public partial class CBSTempletOut : PageBase
	{
		protected System.Web.UI.WebControls.Button ButtonSave;
		protected System.Web.UI.WebControls.Button Button1;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				loadData();
			}
		}
		
		private void loadData()
		{
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


		protected void btnOK_ServerClick(object sender, System.EventArgs e)
		{
			string projectCode=Request["ProjectCode"] + "" ; 
			string subjectSetCode = BLL.ProjectRule.GetSubjectSetCodeByProject(projectCode);

			try
			{

				if (this.txtFile.PostedFile.FileName == "") 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "请选择文件"));
					return;
				}

				StreamReader m_sr = new StreamReader(this.txtFile.PostedFile.InputStream, System.Text.Encoding.Default);

				//第1行是标题
				if (m_sr.Peek() >= 0) 
				{
					m_sr.ReadLine();
				}

				EntityData cbs = DAL.EntityDAO.CBSDAO.GetCBSByProject(projectCode);
//				EntityData cost = DAL.EntityDAO.CBSDAO.GetCostByProject(projectCode);

				while (m_sr.Peek() >= 0) 
				{
					string s = m_sr.ReadLine();

                    string[] sss = BLL.ImportRule.SplitCsvLine(s);

					if (sss.Length <= 1) continue;

					string sortID = sss[0];
					int re = 0;
					Math.DivRem( sortID.Length , 2 , out re ) ;
					if ( re == 1 )
						sortID = "0" + sortID;


					DataRow[] drsSelect = cbs.CurrentTable.Select( String.Format( "SortID='{0}'"  ,sortID ) );
					DataRow dr = null;

					bool isNew = ( drsSelect.Length  == 0 );
					if ( isNew )
					{
						dr = cbs.GetNewRecord();
						string costCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("CostCode");
						dr["CostCode"] = costCode;
						dr["SortID"]=sortID;
						dr["ProjectCode"]=projectCode;
						string parentFullCode = "";
						int parentDeep = 0;
						string parentCode = "";
						string parentSortID = "";
						if ( sortID.Length >= 2 )
						{
							parentSortID = sortID.Substring( 0 , sortID.Length - 2 );
							DataRow[] drsP = cbs.CurrentTable.Select( String.Format( "SortID='{0}'"  ,parentSortID ) );
							if ( drsP.Length > 0 )
							{
								parentDeep = (int) drsP[0]["Deep"];
								parentCode = (string) drsP[0]["CostCode"];
								parentFullCode = (string) drsP[0]["FullCode"];
							}
						}

						int deep = parentDeep + 1;
						dr["Deep"] = deep;
						dr["ParentCode"] = parentCode;
						dr["SubjectSetCode"]=subjectSetCode;
						if ( parentCode == "" )
							dr["FullCode"] =  costCode;
						else
							dr["FullCode"] = parentFullCode + "-" + costCode;
						
						cbs.AddNewRecord(dr);

						/*
						DataRow drCost = cost.GetNewRecord();
						drCost["CostItemCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("CostItemCode");
						drCost["CostCode"] = costCode;
						drCost["ProjectCode"] = projectCode;
						drCost["Flag"] = -1;
						drCost["TotalMoney"] = decimal.Zero;
						drCost["ModifyPerson"] = base.user.UserCode;
						drCost["ModifyDate"] = DateTime.Now.Date;

						// AccountPoint: 0 不作预算、1 做了预算、 2 不是控制点，是子预算统计上来的

						if ( deep == 1 )
							drCost["AccountPoint"] = 1;
						else
							drCost["AccountPoint"] = 0;

						cost.AddNewRecord(drCost);
						*/

					}
					else
					{
						dr = drsSelect[0];
					}

					dr["CostName"] = sss[1];

					if ( sss.Length>=3 )
						dr["SubjectCode"]=sss[2];
					if ( sss.Length >= 4 )
						dr["CostAllocationDescription"]=sss[3];
					if ( sss.Length >= 5)
						dr["Description"] = sss[4];

				}

				using(StandardEntityDAO dao=new StandardEntityDAO("CBS"))
				{
					dao.BeginTrans();
					try
					{
						dao.SubmitEntity(cbs);

						/*
						dao.EntityName = "Cost";
						dao.SubmitEntity(cost);
						*/

						dao.CommitTrans();
					}
					catch(Exception ex)
					{
						try 
						{
							dao.RollBackTrans();
						}
						catch 
						{
						}

						throw ex;
					}
				}

				cbs.Dispose();
//				cost.Dispose();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog (this.ToString(),ex,"");
				Response.Write(JavaScript.Alert(true, "导入出错：" + ex.Message));
				return;
			}

			Response.Write(JavaScript.ScriptStart);
			Response.Write(JavaScript.Alert(false,"导入完成 ！"));
			Response.Write(JavaScript.OpenerReload(false));
			Response.Write("window.close();");
			Response.Write(JavaScript.ScriptEnd);
		}
	}
}
