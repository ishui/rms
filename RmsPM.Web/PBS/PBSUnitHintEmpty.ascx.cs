namespace RmsPM.Web.PBS
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		PBSUnitHintEmpty ��ժҪ˵����
	/// </summary>
	public partial class PBSUnitHintEmpty : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
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

		public void SetProject(string ProjectCode)
		{
			try 
			{
				DataTable tb = BLL.PBSRule.GetBuildingNoPBSUnit(ProjectCode);
				if (tb.Rows.Count > 0) 
				{
					this.lblHint.Text = BLL.ConvertRule.Concat(tb, "BuildingName", ",");
					this.tbHint.Style["display"] = "";
				}
				else 
				{
					this.tbHint.Style["display"] = "none";
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}
	}
}
