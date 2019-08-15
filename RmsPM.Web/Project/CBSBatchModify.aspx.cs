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

namespace RmsPM.Web.Project
{
	/// <summary>
	/// CBSBatchModify ��ժҪ˵����
	/// </summary>
	public partial class CBSBatchModify : PageBase
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( ! this.IsPostBack )
			{
				LoadData();
			}
		}

		private void LoadData()
		{
			string costCode = Request["CostCode"] + "";
			string projectCode = Request["ProjectCode"] + "";
			string fullCode = "";
			try
			{
				EntityData allCost = DAL.EntityDAO.CBSDAO.GetCBSByProject(projectCode);
				DataRow[] drs0 = allCost.CurrentTable.Select("CostCode='" +  costCode + "'" );
				fullCode = (string) drs0[0]["FullCode"];

				//������Ч��
				int iCount = allCost.CurrentTable.Rows.Count;
				for ( int i=0;i<iCount;i++)
				{
					allCost.SetCurrentRow(i);
					int deep = (int)allCost.CurrentRow["Deep"];
					string name = (string) allCost.CurrentRow["CostName"];
					for ( int j=1; j<deep ; j++)
						name = ""+name;

					allCost.CurrentRow["CostName"] = name;
				}

				this.repeat1.DataSource = new DataView( allCost.CurrentTable,String.Format( "FullCode like '{0}%' " , fullCode),"FullCode" ,DataViewRowState.CurrentRows );
				this.repeat1.DataBind();
				allCost.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
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

		}
		#endregion

		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			string projectCode = Request["ProjectCode"] + "";

			try
			{
				EntityData allCost = DAL.EntityDAO.CBSDAO.GetCBSByProject(projectCode);
				foreach ( RepeaterItem li in this.repeat1.Items )
				{
					System.Web.UI.HtmlControls.HtmlInputText txtSortID = (HtmlInputText)li.FindControl("txtSortID");
					System.Web.UI.HtmlControls.HtmlInputText txtCostAllocationDescription =(HtmlInputText)  li.FindControl("txtCostAllocationDescription");
					System.Web.UI.HtmlControls.HtmlInputText txtDescription =(HtmlInputText)  li.FindControl("txtDescription");
					RmsPM.Web.UserControls.InputSubject ucInputSubject = (RmsPM.Web.UserControls.InputSubject) li.FindControl("ucInputSubject");

					string costCode = ((HtmlInputHidden)li.FindControl("txtCostCode")).Value;
					string costName = ((HtmlInputHidden)li.FindControl("txtCostName")).Value;
					
					string sortID = txtSortID.Value.Trim();
					if ( sortID == "" )
					{
						Response.Write( Rms.Web.JavaScript.Alert(true,costName + "�� ������д�������� !"));
						return;
					}

					string subjectCode = ucInputSubject.Value.Trim();
					if (subjectCode != "" )
					{
						string subjectName = BLL.SubjectRule.GetSubjectName(subjectCode,base.SubjectSetCode);
						if ( subjectName == "" )
						{
							Response.Write(Rms.Web.JavaScript.Alert(true,costName + "�� �����������Ŀ��� ��"));
							return;
						}
					}

					DataRow[] drs = allCost.CurrentTable.Select( String.Format( "CostCode='{0}'" ,costCode) );
					if ( drs.Length>0)
					{
						int iChildCount = (int)drs[0]["ChildCount"];

						// ��ϸ�ڵ���Ҫ��Ӧ��Ŀ
						if ( iChildCount == 0  && subjectCode=="" )
						{
							Response.Write(Rms.Web.JavaScript.Alert(true,costName + "�� ��ϸ������Ҫ��Ӧ����Ŀ ��"));
							return;
						}

						drs[0]["SortID"] = sortID;
						drs[0]["SubjectCode"] = subjectCode;
						drs[0]["CostAllocationDescription"] = txtCostAllocationDescription.Value;
						drs[0]["Description"] = txtDescription.Value;
					}
				}
				DAL.EntityDAO.CBSDAO.UpdateCBS(allCost);
				allCost.Dispose();
				Response.Write(JavaScript.ScriptStart);
				Response.Write(JavaScript.Alert(false,"�����޸���� ��"));
				Response.Write(JavaScript.OpenerReload(false));
				Response.Write("window.close();");
				Response.Write(JavaScript.ScriptEnd);
				Response.End();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog( this.ToString(),ex,"");
			}
		}



	}
}
