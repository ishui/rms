namespace RmsPM.Web
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using RmsPM.BLL;
	using Rms.Web;
	//using TestDB.

	/// <summary>
	///		Control_CreatStyle ��ժҪ˵����
	/// </summary>
	public partial class Control_CreatStyle : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!this.IsPostBack)
			{
				DefaultSet();
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
		private void GetStationList()
		{
		}
		#region ��ʼ��Ϣ��ʾ
		private void DefaultSet()
		{
			BindDDL_StyleList();
			BindDDL_StationList();
			GetStyelByID();
		}
		private void GetStyelByID()
		{
			BindDDL_StyleList();
			DataTable Style = StyleOperation.GetStationConfig(this.DDL_StationList.SelectedValue.ToString(),"-1");
			DDL_StyleList.SelectedIndex = DDL_StyleList.Items.IndexOf(DDL_StyleList.Items.FindByValue(Style.Rows[0]["StyleID"].ToString()));
		}
		private void BindDDL_StyleList()
		{
			DataSet ds = StyleOperation.GetStyleList();
			DDL_StyleList.DataSource = ds;
			DDL_StyleList.DataTextField="StyleName";
			DDL_StyleList.DataValueField="StyleID";
			DDL_StyleList.DataBind();
		}
		private void BindDDL_StationList()
		{
			DataSet ds;
			ds = StyleOperation.GetStationList();
			DDL_StationList.DataSource=StyleOperation.GetStationList();
			DDL_StationList.DataTextField="StationName";
			DDL_StationList.DataValueField="StationCode";
			DDL_StationList.DataBind();
		}
		
		#endregion
		#region ����,ɾ����Ϣ
		protected void Bt_Sumit_Click(object sender, System.EventArgs e)
		{
			try
			{
				StyleOperation.SetStationStyle(this.DDL_StationList.SelectedValue,this.DDL_StyleList.SelectedValue);
				Response.Write(JavaScript.Alert(true,"���³ɹ�"));				
			}
			catch(Exception ex)
			{
				Response.Write(JavaScript.Alert(true,ex.Message));
			}
		}
		protected void DDL_StyleList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Bt_Sumit.Enabled=true;
			ShowStyleInfo();
		}
	
		#endregion
		#region ��ʾ��ʽ������	
		private void ShowStyleInfo()
		{
			//this.DDL_StyleList.SelectedValue
			DataSet Leftds = StyleOperation.GetLeftSytleByID(this.DDL_StyleList.SelectedValue);
			DataSet Rightds = StyleOperation.GetRightSytleByID(this.DDL_StyleList.SelectedValue);		
			LB_LeftBind(Leftds);
			LB_RightBind(Rightds);
		}
		private void LB_LeftBind(DataSet ds)
		{
			LB_Left.DataSource = ds;
			LB_Left.DataTextField = "ControlTitle";
			LB_Left.DataValueField = "ControlID";
			LB_Left.DataBind();
		}
		private void LB_RightBind(DataSet ds)
		{
			LB_Right.DataSource = ds;
			LB_Right.DataTextField = "ControlTitle";
			LB_Right.DataValueField = "ControlID";
			LB_Right.DataBind();
		}
		#endregion	

		protected void DDL_StationList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindDDL_StyleList();
			GetStyelByID();
		}
		//private void 
	}
}
