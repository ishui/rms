namespace RmsPM.Web.DesignChange.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using RmsPM.BLL;

	/// <summary>
	///		DesignListControl ��ժҪ˵����
	/// </summary>
	public partial class DesignListControl : Components.ControlBase
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!this.IsPostBack)
			{
				OnitControl();
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

		#region ˽������ -----------------------------------------------
		#endregion


		#region �������� -----------------------------------------------
		#endregion


		#region ˽�з��� -----------------------------------------------
		protected override void LoadData()
		{
			BLL.Design_Message dm = new RmsPM.BLL.Design_Message();
			if(TB_Name.Text!="")
			{
				dm.DesignName="%"+TB_Name.Text+"%";
			}
			if(this.TB_Code.Text!="")
			{
				dm.DesignID="%"+TB_Code.Text+"%";
			}
			if(this.SelectBox1.Value!="")
			{
				
				dm.ContractID = "%"+SelectBox1.Value+"%";
			}
			if(this.InputStationUser1.UserCodes!="")
			{
				dm.DesignPerson = this.InputStationUser1.UserCodes;
			}
			dm.ProjectCode=Request["ProjectCode"];
			dm.DesignState=this.State;
			BindDataGrid(dm.GetDesign_Messages());
		}
		protected void BindDataGrid(DataTable dt)
		{
			this.DataGrid1.DataSource=dt;
			this.DataGrid1.DataBind();
			this.gpControl.RowsCount = dt.Rows.Count.ToString();
		}
		/// <summary>
		/// �õ���ֵͬ
		/// </summary>
		private void GetValue()
		{
			
		}
		#endregion


		#region �������� -----------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		override public void OnitControl()
		{
			trSearch.Visible=false;
			State=Request["State"]+"";
			LoadData();
		}
		#endregion


		#region �¼����� -----------------------------------------------
		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			GetValue();			
			OnitControl();
		}
		#endregion

		protected void gpControl_PageIndexChange(object sender, System.EventArgs e)
		{
			LoadData();		
		}


	}
}
