namespace RmsPM.Web.EditControl
{
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
	using RmsPM.BLL;
	using RmsPM.Web.Components;
	using Rms.Web;
    using RmsOA.BFL;

	/// <summary>
	///		Control_UserDestop 的摘要说明。
	/// </summary>
	public partial class Control_UserDestop : BaseControl
	{
		//protected string userid;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{
				DefaultSet();
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
		#region 默认设置
		private void DefaultSet()
		{
			try
			{
				BindDataGrid1();
			}
			catch(Exception ex)
			{
				Response.Write(JavaScript.Alert(true,ex.Message));
				Bt_Update.Visible=false;
			}			
			//userid= user.UserCode; 
		}
		private void BindDataGrid1()
		{
			string station =BLL.SystemRule.GetStationListByUserCode(this.user.UserCode);			
			DataView dt = StyleOperation.GetUserControl(station,user.UserCode);
			BindDataGrid1(ReValueDataView(dt));
		}

        private DataView ReValueDataView(DataView dv)
        {
            DataTable backTable = new DataTable();
            DataTable tempTable = dv.ToTable();
            for (int i = 0; i < tempTable.Rows.Count; i++ )
            {
                if (!ControlIDINCollection(tempTable.Rows[i]["ControlID"].ToString()))
                {
                    tempTable.Rows.Remove(tempTable.Rows[i]);
                    i--;
                }
            }
            return tempTable.DefaultView;
        }
        private bool ControlIDINCollection(string id)
        {
            string deskType = Request.QueryString["DesktopType"];
            DesktopType ddt = DesktopType.OA;
            if (deskType.Equals(DesktopType.PM.ToString()))
            {
                ddt = DesktopType.PM;
            }
            foreach (string s in DeskTopTypeBFL.GetIDCollectionByDesktopType(ddt))
            {
                if (id.Equals(s))
                {
                    return true;
                }
            }
            return false;

        }
        private bool ControlIDINCollection(string id, DesktopType dt)
        {
            foreach (string s in DeskTopTypeBFL.GetIDCollectionByDesktopType(dt))
            {
                if (id.Equals(s))
                {
                    return true;
                }
            }
            return false;
        }
	
		private void BindDataGrid1(DataView dt)
		{
			DataGrid1.DataSource = dt;
			DataGrid1.DataBind();
		}
		#endregion
		#region 更改信息
		protected void Bt_Update_Click(object sender, System.EventArgs e)
		{
			Save_ControlMessage();
		}
		private void Save_ControlMessage()
		{
			try
			{
				DeleteUserControls();
				UpdateControlMessage();
				//Response.Write(JavaScript.Reload(true));
				Response.Write("<script>window.opener.location.reload(true)</script>");
				//Response.Write(" window.opener.location.reload(true)");
				//Response.Write(" window.close(); ");
				//Response.Write("</script>");
			}
			catch(Exception ex)
			{
				Response.Write(JavaScript.Alert(true,ex.Message));
			}
		}
		private void UpdateControlMessage()
		{
			foreach(DataGridItem dgi in this.DataGrid1.Items)
			{
				CheckBox cb =(CheckBox)dgi.FindControl("CB_Control");
				if(cb.Checked==true)
				{
					StyleOperation.InsertUserControl(user.UserCode,DataGrid1.DataKeys[dgi.ItemIndex]);
				}
			}
		}
		private void DeleteUserControls()
		{
			StyleOperation.DeleUserControl(user.UserCode);
		}		
		private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{			
		}
	}
	#endregion
}
