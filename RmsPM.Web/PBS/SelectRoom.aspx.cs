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
using RmsPM.DAL.QueryStrategy;
using RmsPM.BLL;
using Rms.Web;

namespace RmsPM.Web.PBS
{
	/// <summary>
	/// SelectRoom ��ժҪ˵����
	/// </summary>
	public partial class SelectRoom : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlSelect sltModelCode;
		protected System.Web.UI.HtmlControls.HtmlSelect sltPBSTypeCode;
		protected System.Web.UI.HtmlControls.HtmlSelect sltInvState;
		protected System.Web.UI.HtmlControls.HtmlSelect sltOutState;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
				LoadEmptyDataGrid();
			}

			//ѡ��ȷ���󣬵��ø����ڵĽ��պ���
			string s = Rms.Web.JavaScript.ScriptStart;
			s += "function ReturnToParentWindow(code)" + "\n";
			s += "{" + "\n";
			s += "window.opener." + this.txtReturnFunc.Value + "(code);" + "\n";
			s += "}" + "\n";
			s += Rms.Web.JavaScript.ScriptEnd;
			Page.RegisterStartupScript("start", s);
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

		private void IniPage() 
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtReturnFunc.Value = Request["ReturnFunc"];
				this.txtDefaultInvState.Value = Request["InvState"];
				this.txtDefaultPBSTypeCode.Value = Request["PBSTypeCode"];

				if (this.txtReturnFunc.Value == "") 
				{
					this.txtReturnFunc.Value = "SelectRoomReturn";
				}

				((SearchRoom)this.tbSearchRoom).SetProject(this.txtProjectCode.Value);
				((SearchRoom)this.tbSearchRoom).SetDefaultInvState(this.txtDefaultInvState.Value);
				((SearchRoom)this.tbSearchRoom).SetDefaultPBSTypeCode(this.txtDefaultPBSTypeCode.Value);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadEmptyDataGrid() 
		{
			try 
			{
				EntityData entity = new EntityData("V_ROOM");
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

		private void LoadDataGrid() 
		{
			try 
			{
				RoomStrategyBuilder sb = new RoomStrategyBuilder("V_ROOM");

				string ProjectCode = this.txtProjectCode.Value;
				if (ProjectCode != "")
					sb.AddStrategy( new Strategy( RoomStrategyName.ProjectCode, ProjectCode));

				if (this.tbSearchRoom.Visible) 
				{
					((SearchRoom)this.tbSearchRoom).AddSearch(sb);
				}

				sb.AddOrder("BuildingName", true);
				sb.AddOrder("ChamberCode", true);
				sb.AddOrder("FloorIndex", true);
				sb.AddOrder("RoomName", true);

				string sql = sb.BuildMainQueryString();

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "V_ROOM",sql );
				qa.Dispose();

				dgList.Columns[2].FooterText = entity.CurrentTable.Rows.Count.ToString() + " ��";
				dgList.Columns[5].FooterText = BLL.MathRule.SumColumn(entity.CurrentTable,"BuildArea").ToString("0.####");
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

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			this.dgList.CurrentPageIndex = 0;
			LoadDataGrid();
		}

		private void dgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.dgList.CurrentPageIndex = e.NewPageIndex;
			LoadDataGrid();
		}

	}
}
