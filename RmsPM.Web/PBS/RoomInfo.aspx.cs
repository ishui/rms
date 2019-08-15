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
	/// RoomInfo ��ժҪ˵����
	/// </summary>
	public partial class RoomInfo : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage() 
		{
			try 
			{
				this.txtRoomCode.Value = Request.QueryString["RoomCode"];
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadData()
		{
			try
			{
				string code = this.txtRoomCode.Value;
				if (code != "") 
				{
					EntityData entity = RmsPM.DAL.EntityDAO.ProductDAO.GetV_ROOMByCode(code);
					if (entity.HasRecord())
					{
						this.txtProjectCode.Value = entity.GetString("ProjectCode");

						this.lblBuildingName.Text = entity.GetString("BuildingName");
						this.lblChamberName.Text = entity.GetString("ChamberName");
						this.lblRoomName.Text = entity.GetString("RoomName");
						this.txtModelCode.Value = entity.GetString("ModelCode");
						this.lblModelName.Text = entity.GetString("ModelName");
						this.lblPBSTypeName.Text = entity.GetString("PBSTypeFullName");

						this.lblInvState.Text = entity.GetString("InvState");
						this.lblInDate.Text = entity.GetDateTimeOnlyDate("InDate");
						this.lblOutDate.Text = entity.GetDateTimeOnlyDate("OutDate");

						this.lblOutState.Text = entity.GetString("OutState");
						this.lblOutAspect.Text = entity.GetString("OutAspect");

						this.lblPreBuildArea.Text = BLL.StringRule.AddUnit(BLL.StringRule.BuildShowNumberString(entity.GetDecimal("PreBuildArea")), "ƽ��");
						this.lblPreRoomArea.Text = BLL.StringRule.AddUnit(BLL.StringRule.BuildShowNumberString(entity.GetDecimal("PreRoomArea")), "ƽ��");
						this.lblBuildArea.Text = BLL.StringRule.AddUnit(BLL.StringRule.BuildShowNumberString(entity.GetDecimal("BuildArea")), "ƽ��");
						this.lblRoomArea.Text = BLL.StringRule.AddUnit(BLL.StringRule.BuildShowNumberString(entity.GetDecimal("RoomArea")), "ƽ��");

						this.lblSalState.Text = entity.GetString("SalState");
						this.txtContractCode.Value = entity.GetString("ContractCode");
						this.lblContractID.Text = entity.GetString("ContractID");
						this.lblTotalPayMoney.Text = BLL.StringRule.AddUnit(BLL.StringRule.BuildShowNumberString(entity.GetDecimal("TotalPayMoney")), "Ԫ");

						LoadDataGrid();
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "���䲻����"));
						return;
					}
					entity.Dispose();
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "δ���뷿�����"));
					return;
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ����" + ex.Message));
			}
		}

		private void LoadDataGrid() 
		{
			try 
			{
				TempRoomOutStrategyBuilder sb = new TempRoomOutStrategyBuilder();

				sb.AddStrategy( new Strategy( TempRoomOutStrategyName.ProjectCode, txtProjectCode.Value));
				sb.AddStrategy(new Strategy(TempRoomOutStrategyName.InRoomCode, txtRoomCode.Value));

				sb.AddOrder("Out_Date",false);

				string sql = sb.BuildMainQueryString();

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "TempRoomOut",sql );
				qa.Dispose();

				dgList.DataSource = entity;
				dgList.DataBind();
				entity.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
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
	}
}
