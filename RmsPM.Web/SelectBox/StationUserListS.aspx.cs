using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rms.ORMap;
using Rms.Web;
using RmsPM.DAL;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;

namespace RmsPM.Web.SelectBox
{
	/// <summary>
	/// StationUserListS 的摘要说明。
	/// </summary>
	public partial class StationUserListS : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if (!this.IsPostBack)
			{
				LoadData();
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
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
		
		private void LoadData()
		{
			//初始化符合条件的数据列表
//			string stationCode = Request["StationCode"]+"";

			this.txtUnitCode.Value = Request.QueryString["UnitCode"];
			this.txtProjectCode.Value = Request.QueryString["ProjectCode"];

			string unitCode = this.txtUnitCode.Value;

			try
			{
				DataTable dt = new DataTable();
				dt.Columns.Add("RelationCode" );
				dt.Columns.Add("AccessRangeType");
				dt.Columns.Add("ImageFileName");
				dt.Columns.Add("Name");
				dt.Columns.Add("UserCount");
				dt.Columns.Add("UnitCode");

//				if ( stationCode != "" ) 
//				{
//					EntityData entity = BLL.SystemRule.GetUserByStation(stationCode);
//					foreach ( DataRow dr in entity.CurrentTable.Rows)
//					{
//						DataRow drNew = dt.NewRow();
//						drNew["RelationCode"]=dr["UserCode"];
//						drNew["Name"]=dr["UserName"];
//						drNew["AccessRangeType"]=0;
//						drNew["ImageFileName"]="user.gif";
//						dt.Rows.Add(drNew);
//					}
//					entity.Dispose();
//				}

				if ( unitCode != "" )
				{
					EntityData entity = DAL.EntityDAO.OBSDAO.GetStationByUnitCode(unitCode);
					foreach ( DataRow dr in entity.CurrentTable.Rows)
					{
						DataRow drNew = dt.NewRow();
						drNew["RelationCode"]=dr["StationCode"];
						drNew["Name"]=dr["StationName"];
						drNew["UserCount"] = dr["UserCount"];
						drNew["AccessRangeType"]=1;
						drNew["ImageFileName"]="group.gif";
						drNew["UnitCode"] = unitCode;
						dt.Rows.Add(drNew);
					}
					entity.Dispose();
				}

				this.repeaterSU.DataSource =dt;
				this.repeaterSU.DataBind();
				dt.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"加载人员列表失败");
			}
		}
	}
}
