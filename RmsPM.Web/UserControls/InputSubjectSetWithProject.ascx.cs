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
	/// InputSubjectSetWithProject 的摘要说明。
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
		///		设计器支持所需的方法 - 不要使用代码编辑器
		///		修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		/// <summary>
		/// 表名
		/// </summary>
		public string TableName 
		{
			get {return this.txtTableName.Value;}
			set {this.txtTableName.Value = value;}
		}

		/// <summary>
		/// 表的关键字段名
		/// </summary>
		public string KeyFieldName 
		{
			get {return this.txtKeyFieldName.Value;}
			set {this.txtKeyFieldName.Value = value;}
		}

		/// <summary>
		/// 表的关联代码字段名
		/// </summary>
		public string CodeFieldName 
		{
			get {return this.txtCodeFieldName.Value;}
			set {this.txtCodeFieldName.Value = value;}
		}

        /// <summary>
        /// 帐套编号
        /// </summary>
        public string SubjectSetCode
        {
            get { return this.txtSubjectSetCode.Value; }
            set { this.txtSubjectSetCode.Value = value; }
        }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectCode
        {
            get { return this.txtProjectCode.Value; }
            set { this.txtProjectCode.Value = value; }
        }

        /// <summary>
        /// 是否只有集团财务编码（不分项目）
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
				ApplicationLog.WriteLog(this.ToString(),ex,"初始化页面失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面失败：" + ex.Message));
			}
		}

		/// <summary>
		/// 显示财务编码列表
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
				ApplicationLog.WriteLog(this.ToString(),ex,"显示列表失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示列表失败：" + ex.Message));
			}
		}

		/// <summary>
		/// 保存财务编码列表
		/// </summary>
		/// <param name="tb"></param>
		public void SaveData(DataTable tb, string RelationCode) 
		{
			try
			{
                //保存到临时表
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

                //删除
                int icount = tb.Rows.Count;
				for(int i=icount-1;i>=0;i--)
				{
                    DataRow dr = tb.Rows[i];
                    string SubjectSetCode = BLL.ConvertRule.ToString(dr["SubjectSetCode"]);
                    string ProjectCode = BLL.ConvertRule.ToString(dr["ProjectCode"]);

                    if (tbTemp.Select("SubjectSetCode = '" + SubjectSetCode + "' and ProjectCode = '" + ProjectCode + "'").Length == 0)
    					dr.Delete();
				}

                //新增或修改
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
