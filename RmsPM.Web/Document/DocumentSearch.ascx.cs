namespace RmsPM.Web.Document
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;
	using Rms.ORMap;
	using RmsPM.BLL;
	using RmsPM.DAL.EntityDAO;
	using RmsPM.Web;
	using Rms.Web;

	/// <summary>
	/// ��ѯ
	/// </summary>
	public partial class DocumentSearch : System.Web.UI.UserControl
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton btnSearch;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
//			if (this.Visible) 
//			{
				string reload = JavaScript.ScriptStart;
				reload += @"var ClientID = '" + this.ClientID + "';" + "\n" ;
				reload += JavaScript.ScriptEnd;
				Response.Write(reload);

				if (!Page.IsPostBack) 
//				if (this.txtHasIniPage.Value == "")
				{
					IniPage();
				}
//			}
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

		private void IniPage() 
		{
			try
			{
				BLL.PageFacade.LoadFixedDocumentTypeCodeSelect(this.sltFixedType,"");

				this.txtHasIniPage.Value = "1";
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"�����ĵ���ѯ����ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�����ĵ���ѯ����ʧ�ܣ�" + ex.Message));
			}
		}

		/// <summary>
		/// ��Ӳ�ѯ����
		/// </summary>
		/// <param name="ssb"></param>
		public void AddSearch(Rms.ORMap.StandardQueryStringBuilder ssb) 
		{
			string CreateDate_begin = this.dtCreateDate_begin.Value.Trim();
			string CreateDate_end = this.dtCreateDate_end.Value.Trim();
			string ModifyDate_begin = this.dtModifyDate_begin.Value.Trim();
			string ModifyDate_end = this.dtModifyDate_end.Value.Trim();
			string title = this.txtSearchTitle.Value.Trim();
			string DocumentID = this.txtSearchDocumentID.Value.Trim();
			string author = this.txtSearchAuthor.Value.Trim();
			string CreatePerson = this.ucCreatePerson.Value.Trim();
			string ModifyPerson = this.ucModifyPerson.Value.Trim();

			string FixedType = this.sltFixedType.Value.Trim();
			string Code = this.txtCode.Value.Trim();

			ArrayList ar = new ArrayList();
			ar.Add(CreateDate_begin);
			ar.Add(CreateDate_end);

			ArrayList ar2 = new ArrayList();
			ar2.Add(ModifyDate_begin);
			ar2.Add(ModifyDate_end);

			ssb.AddStrategy( new Strategy( DAL.QueryStrategy.DocumentStrategyName.CreateDateRange,ar ));
			ssb.AddStrategy( new Strategy( DAL.QueryStrategy.DocumentStrategyName.ModifyDateRange,ar2 ));

			if (title.Length > 0)
				ssb.AddStrategy( new Strategy(DAL.QueryStrategy.DocumentStrategyName.Title, title));
			if (DocumentID.Length > 0)
				ssb.AddStrategy( new Strategy(DAL.QueryStrategy.DocumentStrategyName.DocumentID, DocumentID));
			if (author.Length > 0)
				ssb.AddStrategy( new Strategy(DAL.QueryStrategy.DocumentStrategyName.Author, author));
			if (CreatePerson.Length > 0)
				ssb.AddStrategy( new Strategy(DAL.QueryStrategy.DocumentStrategyName.CreatePerson, CreatePerson));
			if (ModifyPerson.Length > 0)
				ssb.AddStrategy( new Strategy(DAL.QueryStrategy.DocumentStrategyName.ModifyPerson, ModifyPerson));

			if (Code.Length > 0) 
			{
				if (FixedType.Length > 0) 
				{
					ArrayList arTmp = new ArrayList();
					arTmp.Add(FixedType);
					arTmp.Add(Code);
					ssb.AddStrategy( new Strategy(DAL.QueryStrategy.DocumentStrategyName.RelationKey, arTmp));
				}
				else 
				{
//					ssb.AddStrategy( new Strategy(DAL.QueryStrategy.DocumentStrategyName.Code, Code));
				}
			}

		}
	}
}
