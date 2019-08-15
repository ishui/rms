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
	/// InputSubjectSetWithProject ��ժҪ˵����
	/// </summary>
	public partial class InputSubjectSetWithProject : System.Web.UI.UserControl
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

        /// <summary>
        /// ���ױ��
        /// </summary>
        public string SubjectSetCode
        {
            get { return this.txtSubjectSetCode.Value; }
            set { this.txtSubjectSetCode.Value = value; }
        }

        /// <summary>
        /// ��Ŀ���
        /// </summary>
        public string ProjectCode
        {
            get { return this.txtProjectCode.Value; }
            set { this.txtProjectCode.Value = value; }
        }

        /// <summary>
        /// �Ƿ�ֻ�м��Ų�����루������Ŀ��
        /// </summary>
        public int IsGroup
        {
            get { return BLL.ConvertRule.ToInt(this.txtIsGroup.Value); }
            set { this.txtIsGroup.Value = value.ToString(); }
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
                EntityData entitySubjectSet = BLL.FinanceRule.GetFinanceSubjectSetWithProject(tb, SubjectSetCode, ProjectCode);

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
                //���浽��ʱ��
                DataTable tbTemp = tb.Clone();

                int sno = 0;
                foreach (DataGridItem li in this.dgList.Items)
                {
                    string SubjectSetCode = li.Cells[0].Text;
                    string ProjectCode = ((HtmlInputHidden)li.FindControl("txtProjectCode")).Value.Trim();
                    string U8Code = ((HtmlInputText)li.FindControl("txtU8Code")).Value.Trim();

                    if (U8Code != "")
                    {
                        sno++;
                        DataRow drNew = tbTemp.NewRow();

                        drNew[this.KeyFieldName] = sno;
                        drNew[this.CodeFieldName] = RelationCode;
                        drNew["SubjectSetCode"] = SubjectSetCode;
                        drNew["ProjectCode"] = ProjectCode;
                        drNew["U8Code"] = U8Code;

                        tbTemp.Rows.Add(drNew);
                    }
                }

                //ɾ��
                int icount = tb.Rows.Count;
				for(int i=icount-1;i>=0;i--)
				{
                    DataRow dr = tb.Rows[i];
                    string SubjectSetCode = BLL.ConvertRule.ToString(dr["SubjectSetCode"]);
                    string ProjectCode = BLL.ConvertRule.ToString(dr["ProjectCode"]);

                    if (tbTemp.Select("SubjectSetCode = '" + SubjectSetCode + "' and ProjectCode = '" + ProjectCode + "'").Length == 0)
    					dr.Delete();
				}

                //�������޸�
				foreach (DataRow drTemp in tbTemp.Rows)
				{
                    string SubjectSetCode = BLL.ConvertRule.ToString(drTemp["SubjectSetCode"]);
                    string ProjectCode = BLL.ConvertRule.ToString(drTemp["ProjectCode"]);

					DataRow drNew;

                    DataRow[] drs = tb.Select("SubjectSetCode = '" + SubjectSetCode + "' and ProjectCode = '" + ProjectCode + "'"); ;
                    if (drs.Length > 0)
                        drNew = drs[0];
                    else
                    {
                        drNew = tb.NewRow();

                        drNew[this.KeyFieldName] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode(this.KeyFieldName);
                        drNew[this.CodeFieldName] = RelationCode;
                        drNew["SubjectSetCode"] = SubjectSetCode;
                        drNew["ProjectCode"] = ProjectCode;

                        tb.Rows.Add(drNew);
                    }

                    drNew["U8Code"] = drTemp["U8Code"];
				}
			}
			catch ( Exception ex )
			{
                throw ex;
			}
		}

	}
}
