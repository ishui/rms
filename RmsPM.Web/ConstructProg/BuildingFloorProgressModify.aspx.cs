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
using RmsPM.DAL;
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using Rms.Web;
using RmsPM.BLL;

namespace RmsPM.Web.ConstructProg
{
	/// <summary>
	/// BuildingFloorProgressModify ��ժҪ˵����
	/// </summary>
	public partial class BuildingFloorProgressModify :PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtPBSUnitCode;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack )
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage()
		{
			try 
			{
				this.txtBuildingFloorCode.Value = Request.QueryString["BuildingFloorCode"];
				this.txtWBSCode.Value = Request.QueryString["WBSCode"];
				this.txtVisualProgressCode.Value = Request.QueryString["VisualProgressCode"];
				this.txtAct.Value = Request.QueryString["action"];
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
				if (!user.HasRight("030303"))  //¥��������дȨ��
				{
					Response.Redirect( "../RejectAccess.aspx?OperationCode=030303" );
					Response.End();
				}

				if ((this.txtBuildingFloorCode.Value.Trim() == "") || (this.txtWBSCode.Value.Trim() == "")  || (this.txtVisualProgressCode.Value.Trim() == ""))
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "δ����¥����롢���������������ȴ���"));
					return;
				}

				//ȡ¥��
				EntityData entityFloor = DAL.EntityDAO.ProductDAO.GetBuildingFloorByCode(this.txtBuildingFloorCode.Value);
				entityFloor.Dispose();
				if (!entityFloor.HasRecord()) 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "¥�㲻����"));
					return;
				}

				this.txtBuildingCode.Value = entityFloor.GetString("BuildingCode");
				this.txtProjectCode.Value = entityFloor.GetString("ProjectCode");
				this.lblFloorName.Text = entityFloor.GetString("FloorName");
				this.lblBuildingName.Text = BLL.ProductRule.GetBuildingName(this.txtBuildingCode.Value);

				//ȡ����
				this.lblVisualProgressName.Text = BLL.WBSRule.GetWBSName(this.txtVisualProgressCode.Value);
				this.lblTaskName.Text = BLL.WBSRule.GetWBSName(this.txtWBSCode.Value);

				//�Ƿ��й�������޸�Ȩ��
				if (!WBSRule.IsTaskModify(this.txtVisualProgressCode.Value, user.UserCode))
				{
					Response.Redirect( "../RejectAccess.aspx?OperationName=������[" + lblVisualProgressName.Text + "]�޸�" );
					Response.End();
				}

				//ȡ����
				EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingFloorProgressByBuildingFloorWBSCode (this.txtBuildingFloorCode.Value, this.txtWBSCode.Value);
				entity.Dispose();
				if (entity.HasRecord()) 
				{
					this.txtProgressCode.Value = entity.GetString("ProgressCode");

					this.sltStatus.Value = entity.GetInt("Status").ToString();
					this.txtCompletePercent.Value = entity.GetInt("CompletePercent").ToString();

					this.txtPStartDate.Value = entity.GetDateTimeOnlyDate("PStartDate");
					this.txtPEndDate.Value = entity.GetDateTimeOnlyDate("PEndDate");
					this.txtStartDate.Value = entity.GetDateTimeOnlyDate("StartDate");
					this.txtEndDate.Value = entity.GetDateTimeOnlyDate("EndDate");
				}
				else 
				{
					//����ʱ��ȱʡֵ
					this.sltStatus.Value = "0";
				}

				//ȱʡʵ�ʿ�ʼ����������Ϊ���죨����״̬�ĳɡ������С�������ɡ�ʱ��ȱʡ���ڣ�����ʱ��״̬Ϊ��δ��ʼ��Ҫ�Զ���գ�
				if (this.txtStartDate.Value == "")
					this.txtStartDate.Value = DateTime.Today.ToString("yyyy-MM-dd");
				if (this.txtEndDate.Value == "")
					this.txtEndDate.Value = DateTime.Today.ToString("yyyy-MM-dd");
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
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

		/// <summary>
		/// ����
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write("window.opener.location = window.opener.location;");
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string Hint = "";
				if (!CheckValid(ref Hint)) 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
					return;
				}

				Save();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�������" + ex.Message));
				return;
			}

			GoBack();
		}

		/// <summary>
		/// ����
		/// </summary>
		private void Save() 
		{
			try 
			{
				string BuildingFloorCode = this.txtBuildingFloorCode.Value;
				string WBSCode = this.txtWBSCode.Value;

				EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingFloorProgressByBuildingFloorWBSCode(BuildingFloorCode, WBSCode);
				bool isNew = !entity.HasRecord();
				DataRow dr;

				if (isNew) 
				{
					dr = entity.CurrentTable.NewRow();
					this.txtProgressCode.Value = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("BuildingFloorProgressCode");

					dr["ProgressCode"] = this.txtProgressCode.Value;
					dr["BuildingFloorCode"] = BuildingFloorCode;
					dr["WBSCode"] = WBSCode;
					dr["BuildingCode"] = txtBuildingCode.Value;
					dr["ProjectCode"] = txtProjectCode.Value;
					dr["VisualProgressCode"] = txtVisualProgressCode.Value;

					entity.CurrentTable.Rows.Add(dr);
				}
				else 
				{
					dr = entity.CurrentRow;
				}

				int Status = BLL.ConvertRule.ToInt(this.sltStatus.Value);
				dr["Status"] = Status;

				dr["PStartDate"] = BLL.ConvertRule.ToDate(this.txtPStartDate.Value.Trim());
				dr["PEndDate"] = BLL.ConvertRule.ToDate(this.txtPEndDate.Value.Trim());

				switch (Status) 
				{
					case 0:
						//δ���
						dr["StartDate"] = DBNull.Value;
						dr["EndDate"] = DBNull.Value;
						dr["CompletePercent"] = 0;
						break;

					case 1:
						//������
						dr["StartDate"] = BLL.ConvertRule.ToDate(this.txtStartDate.Value.Trim());
						dr["EndDate"] = DBNull.Value;
						dr["CompletePercent"] = BLL.ConvertRule.ToInt(this.txtCompletePercent.Value);
						break;

					case 2:
						//�����
						dr["StartDate"] = BLL.ConvertRule.ToDate(this.txtStartDate.Value.Trim());
						dr["EndDate"] = BLL.ConvertRule.ToDate(this.txtEndDate.Value.Trim());
						dr["CompletePercent"] = 100;
						break;

					default:
						break;
				}

				dr["ModiDate"] = DateTime.Now;
				dr["ModiPerson"] = base.user.UserCode;

				DAL.EntityDAO.ProductDAO.SubmitAllBuildingFloorProgress(entity);
				entity.Dispose();

				//���¹��������ɽ���
				BLL.ConstructProgRule.UpdateTaskPercentByConstructProg(this.txtBuildingCode.Value, WBSCode);
                //����ʱ��
                BLL.WBSRule.UpdateTaskData(this.txtBuildingCode.Value, WBSCode);

			}
			catch ( Exception ex )
			{
				throw ex;
			}
		}

		/// <summary>
		/// ��Ч�Լ��
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			if ( this.sltStatus.Value.Trim() == "" )
			{
				Hint = "������״̬ �� ";
				return false;
			}

			if ( this.txtCompletePercent.Value != "" )
			{
				if ( ! Rms.Check.StringCheck.IsInt(txtCompletePercent.Value))
				{
					Hint = "��ɰٷֱȱ��������� �� ";
					return false;
				}

				int CompletePercent = BLL.ConvertRule.ToInt(this.txtCompletePercent.Value);
				if ((CompletePercent < 0) || (CompletePercent > 100))
				{
					Hint = "��ɰٷֱȱ���λ�� 0 �� 100 ֮�� �� ";
					return false;
				}
			}

			return true;
		}

	}
}
