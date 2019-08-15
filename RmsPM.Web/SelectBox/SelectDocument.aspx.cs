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
using RmsPM.DAL.EntityDAO;
using Rms.ORMap;
using Rms.Web;
using System.Text;
namespace RmsPM.Web.SelectBox
{
	/// <summary>
	/// SelectDocument ��ժҪ˵����
	/// </summary>
	public partial class SelectDocument : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��

			InitPage();
			if(!this.IsPostBack)
				LoadData();

		}

		private void InitPage()
		{
			if (!this.IsPostBack)
			{
				this.dtbStartDate.Value = "";
				this.dtbEndDate.Value ="";
			}	
		
			int TypeIndex = this.SelectType.SelectedIndex;
			this.SelectType.Items.Clear();
			this.SelectType.Items.Add(new ListItem("������ѡ�񣭣�",""));
			// ȡ�������ĵ�������,�ĵ����͵ĸ����Ϊ000006
			try
			{
				EntityData entityType = DAL.EntityDAO.DocumentDAO.GetDocumentTypeAllChildByParentCode("000006");
				if (entityType.HasRecord())
				{
					DataTable dtType;
					dtType = entityType.CurrentTable;				
					foreach (DataRow dr in dtType.Rows)
					{
						this.SelectType.Items.Add(new ListItem(dr["TypeName"].ToString(),dr["DocumentTypeCode"].ToString()));
					}
				}
				entityType.Dispose();
				this.SelectType.SelectedIndex = TypeIndex;
			}
			catch ( Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		private void LoadData()
		{
			//��ʼ�����������������б�
			string DocumentName = "";
			string DocumentPerson = "";
			string StartDate = "";
			string EndDate = "";
			string Type = "";
			
			if (this.txtDocumentName.Value.Trim() != "")
			{
				DocumentName = this.txtDocumentName.Value.Trim();
			}
			if (this.txtCreatePerson.Value.Trim() != "")
			{
				DocumentPerson = this.txtCreatePerson.Value.Trim();
			}
			Type = this.SelectType.Value;
			StartDate = this.dtbStartDate.Value;
			EndDate = this.dtbEndDate.Value;
			try
			{
				RmsPM.DAL.QueryStrategy.DocumentStrategyBuilder CSB = new RmsPM.DAL.QueryStrategy.DocumentStrategyBuilder();
			
				if (DocumentPerson.Length > 0 )
				{
					CSB.AddStrategy( new Strategy (RmsPM.DAL.QueryStrategy.DocumentStrategyName.Author,DocumentPerson));
				}
				if ( Type.Length > 0 )
				{
					ArrayList arParam = new ArrayList();
					arParam.Add("000006");
					arParam.Add(DocumentName);
					CSB.AddStrategy( new Strategy (RmsPM.DAL.QueryStrategy.DocumentStrategyName.RelationKey,arParam));
				}
				if ( StartDate.Length > 0 || EndDate.Length >0)
				{
					ArrayList arParam = new ArrayList();
					arParam.Add(StartDate);
					arParam.Add(EndDate);
					CSB.AddStrategy( new Strategy (RmsPM.DAL.QueryStrategy.DocumentStrategyName.CreateDateRange,arParam));
				}
					
				CSB.AddOrder("CreateDate",false);

				string Sql = CSB.BuildMainQueryString();

				QueryAgent QA = new QueryAgent();
				DataSet ds = QA.ExecSqlForDataSet(Sql);
				QA.Dispose();

				this.dgDocumentList.DataSource =new DataView(ds.Tables[0],"","",DataViewRowState.CurrentRows);
				this.dgDocumentList.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"������Ա�б�ʧ��");
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

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			LoadData();
		}

		protected void SaveToolsButton_ServerClick(object sender, System.EventArgs e)
		{
			System.Web.UI.WebControls.CheckBox chkContract = new CheckBox();
			StringBuilder strBuilder = new StringBuilder();
			foreach(DataGridItem oItem in this.dgDocumentList.Items)
			{
				chkContract = (CheckBox)oItem.FindControl("chkContract");
				if (chkContract.Checked == true)
				{
					strBuilder.Append(this.dgDocumentList.DataKeys[oItem.ItemIndex].ToString());
					strBuilder.Append(",");
				}
			}

			string Code = strBuilder.ToString();
			if (Code.Length > 0)
			{
				Code = Code.Substring(0,Code.Length - 1);
				Response.Write(JavaScript.ScriptStart);
				Response.Write("window.opener.SelectDocument('" + Code + "');");
				Response.Write("window.close();");
				Response.Write(JavaScript.ScriptEnd);
			}
			else
			{
				Response.Write(JavaScript.ScriptStart);
				Response.Write("window.close();");
				Response.Write(JavaScript.ScriptEnd);
			}
		}

	}
}
