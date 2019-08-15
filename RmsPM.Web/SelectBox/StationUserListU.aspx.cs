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
	/// StationUserListU ��ժҪ˵����
	/// </summary>
	public partial class StationUserListU : System.Web.UI.Page
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
			string unitCode = Request["UnitCode"]+"";
			string stationCode = Request["StationCode"]+"";

			try
			{
				DataTable dt = new DataTable();
				dt.Columns.Add("RelationCode" );
				dt.Columns.Add("AccessRangeType");
				dt.Columns.Add("ImageFileName");
				dt.Columns.Add("Name");

				if ( stationCode != "" ) 
				{
					EntityData entity = BLL.SystemRule.GetUserByStation(stationCode);
					foreach ( DataRow dr in entity.CurrentTable.Rows)
					{
						DataRow drNew = dt.NewRow();
						drNew["RelationCode"]=dr["UserCode"];
						drNew["Name"]=dr["UserName"];
						drNew["AccessRangeType"]=0;
						drNew["ImageFileName"]="user.gif";
						dt.Rows.Add(drNew);
					}
					entity.Dispose();
				}
				else if ( unitCode != "" )
				{
					EntityData entity;

					if (unitCode == "NoStationUser") 
					{
						//����δ���ڵ���Ա
						entity = BLL.SystemRule.GetUsersNoStation();
					}
					else 
					{
						//ȡ�����µ������û��������Ӳ��ţ�
						entity = BLL.SystemRule.GetUsersByUnitEx(unitCode);
					}

					foreach ( DataRow dr in entity.CurrentTable.Rows)
					{
						DataRow drNew = dt.NewRow();
						drNew["RelationCode"]=dr["UserCode"];
						drNew["Name"]=dr["UserName"];
						drNew["AccessRangeType"]=0;
						drNew["ImageFileName"]="user.gif";
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
