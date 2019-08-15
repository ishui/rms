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
	/// StationUserListS ��ժҪ˵����
	/// </summary>
	public partial class StationUserListS : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if (!this.IsPostBack)
			{
				LoadData();
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
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
		
		private void LoadData()
		{
			//��ʼ�����������������б�
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
				ApplicationLog.WriteLog(this.ToString(),ex,"������Ա�б�ʧ��");
			}
		}
	}
}
