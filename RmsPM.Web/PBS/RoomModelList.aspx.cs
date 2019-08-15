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
using RmsPM.DAL;
using Rms.Web;
using RmsPM.BLL;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.PBS
{
	/// <summary>
	/// RoomModelList ��ժҪ˵����
	/// </summary>
	public partial class RoomModelList : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				this.txtModelCode.Value = Request.QueryString["ModelCode"];
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];

				//Ȩ��
				this.btnAdd.Visible = base.user.HasRight("010502");

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
			try
			{
				ModelStrategyBuilder sb = new ModelStrategyBuilder();

				sb.AddStrategy( new Strategy( ModelStrategyName.ProjectCode, txtProjectCode.Value));

				sb.AddOrder("ModelName", true);
				string sql = sb.BuildQueryDoorNumString();

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "Model",sql );
				qa.Dispose();

				this.dgList.DataSource = entity.CurrentTable;
				this.dgList.DataBind();
				entity.Dispose();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

	}
}
