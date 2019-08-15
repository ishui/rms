namespace RmsPM.Web.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Rms.ORMap;

	/// <summary>
	///		InputSubjectSet ��ժҪ˵����
	/// </summary>
	public partial class InputSubjectSet : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
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
		///		�����֧������ķ��� - ��Ҫʹ�ô���༭��
		///		�޸Ĵ˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		/// <summary>
		/// ����
		/// </summary>
		public string TableName 
		{
			get {return this.txtTableName.Value;}
			set {this.txtTableName.Value = value;}
		}

		/// <summary>
		/// ��Ĺؼ��ֶ���
		/// </summary>
		public string KeyFieldName 
		{
			get {return this.txtKeyFieldName.Value;}
			set {this.txtKeyFieldName.Value = value;}
		}

		/// <summary>
		/// ��Ĺ��������ֶ���
		/// </summary>
		public string CodeFieldName 
		{
			get {return this.txtCodeFieldName.Value;}
			set {this.txtCodeFieldName.Value = value;}
		}

		private void IniPage() 
		{
			try
			{
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ʼ��ҳ��ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ��ʧ�ܣ�" + ex.Message));
			}
		}

		/// <summary>
		/// ��ʾ��������б�
		/// </summary>
		/// <param name="tb"></param>
		public void LoadData(DataTable tb) 
		{
			try
			{
				EntityData entitySubjectSet = BLL.FinanceRule.GetFinanceSubjectSet(tb);

				this.dgList.DataSource = entitySubjectSet;
				this.dgList.DataBind();

				entitySubjectSet.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ʾ�б�ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б�ʧ�ܣ�" + ex.Message));
			}
		}

		/// <summary>
		/// �����������б�
		/// </summary>
		/// <param name="tb"></param>
		public void SaveData(DataTable tb, string RelationCode) 
		{
			try
			{
				int icount = tb.Rows.Count;
				for(int i=icount-1;i>=0;i--)
				{
					tb.Rows[i].Delete();
				}

				foreach ( DataGridItem li in this.dgList.Items)
				{
					string SubjectSetCode = li.Cells[0].Text;
					string U8Code = ((HtmlInputText) li.FindControl("txtU8Code")).Value.Trim();

					if (U8Code != "") 
					{
						DataRow drNew = tb.NewRow();

						drNew[this.KeyFieldName] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode(this.KeyFieldName);
						drNew[this.CodeFieldName] = RelationCode;
						drNew["SubjectSetCode"] = SubjectSetCode;
						drNew["U8Code"]=U8Code;

						tb.Rows.Add(drNew);
					}
				}
			}
			catch ( Exception ex )
			{
                throw ex;
			}
		}

	}
}
