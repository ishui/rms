namespace RmsPM.Web.PBS
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Rms.ORMap;
	using Rms.Web;
	using RmsPM.DAL.QueryStrategy;

	/// <summary>
	/// SelectBuildingCtrl ��ժҪ˵����
	/// </summary>
	public partial class SelectBuildingCtrl : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (this.Visible) 
			{
				string reload = Rms.Web.JavaScript.ScriptStart;
				reload += @"var BuildingCtrlClientID = '" + this.ClientID + "';" + "\n" ;
				reload += Rms.Web.JavaScript.ScriptEnd;
				Response.Write(reload);
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

		private string m_Multi = "";

		public string Multi 
		{
			get{return m_Multi;}
			set
			{
				m_Multi = value;
				this.txtMulti.Value = value;
			}
		}

		public void SetProject(string ProjectCode)
		{
			try 
			{
				if (this.txtProjectCode.Value != ProjectCode) 
				{
					this.txtProjectCode.Value = ProjectCode;
					LoadDataGrid();
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ʼ��ҳ��ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ��ʧ�ܣ�" + ex.Message));
			}
		}

		private void LoadDataGrid() 
		{
			try 
			{
				BuildingStrategyBuilder sb = new BuildingStrategyBuilder();

				string ProjectCode = this.txtProjectCode.Value;
				if (ProjectCode != "")
					sb.AddStrategy( new Strategy( BuildingStrategyName.ProjectCode, ProjectCode));
				sb.AddStrategy( new Strategy( BuildingStrategyName.IsArea, "2"));

				sb.AddOrder("BuildingName", true);

				string sql = sb.BuildMainQueryString();

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "Building",sql );
				qa.Dispose();

				//ѡ����
				if (this.m_Multi == "1") 
				{
					DataRow dr = entity.CurrentTable.NewRow();
					dr["BuildingCode"] = -1;
					dr["BuildingName"] = "��ѡ¥��...";
					entity.CurrentTable.Rows.InsertAt(dr, 0);
				}

				dgList.DataSource = entity;
				dgList.DataBind();
				entity.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

	}
}
