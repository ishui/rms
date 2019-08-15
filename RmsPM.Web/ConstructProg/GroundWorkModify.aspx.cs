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
	/// GroundWorkModify ��ժҪ˵����
	/// </summary>
	public partial class GroundWorkModify :PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtPBSUnitCode;
		protected System.Web.UI.WebControls.Label lblBuildingName;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtBuildingCode;
		protected System.Web.UI.WebControls.Label lblFloorName;
		protected System.Web.UI.WebControls.Label lblTaskName;
		protected AspWebControl.Calendar txtPStartDate;
		protected AspWebControl.Calendar txtPEndDate;
		protected AspWebControl.Calendar txtStartDate;
		protected AspWebControl.Calendar txtEndDate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtWBSCode;
		protected System.Web.UI.WebControls.Label lblVisualProgressName;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtVisualProgressCode;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtProgressCode;
		protected System.Web.UI.HtmlControls.HtmlSelect sltStatus;
		protected System.Web.UI.HtmlControls.HtmlInputText txtCompletePercent;
		protected System.Web.UI.HtmlControls.HtmlInputText txtBuildingFloorCode;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnSelectBuilding;
		protected System.Web.UI.WebControls.DataGrid dgList1;
	
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
				this.txtGroundWorkCode.Value = Request.QueryString["GroundWorkCode"];
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtAct.Value = Request.QueryString["action"];

				this.ucTask.ProjectCode = this.txtProjectCode.Value;
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
				string GroundWorkCode = this.txtGroundWorkCode.Value;

				if (GroundWorkCode != "") 
				{
					EntityData entity = DAL.EntityDAO.ConstructDAO.GetGroundWorkByCode(GroundWorkCode);
					if (entity.HasRecord()) 
					{
						this.txtProjectCode.Value = entity.GetString("ProjectCode");

						this.ucTask.ProjectCode = this.txtProjectCode.Value;
						this.ucTask.Value = entity.GetString("WBSCode");
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "��������ͼ������"));
						return;
					}
					entity.Dispose();
				}
				else 
				{
					//����ʱ���������
//					this.txtGroundWorkCode.Value = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("GroundWorkCode");
				}

				if (this.txtProjectCode.Value == "") 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "δ������Ŀ����"));
					return;
				}

				LoadImage();
				LoadDataGrid();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		/// <summary>
		/// ��ʾͼƬ
		/// </summary>
		private void LoadImage() 
		{
			try 
			{
				string WBSCode = this.ucTask.Value;

                EntityData entityAttach = RmsPM.DAL.EntityDAO.DAOFactory.GetAttachmentDAO().GetAttachMentByMasterCode("GroundWork", WBSCode);
				if (entityAttach.HasRecord()) 
				{
					this.hrefBg.Attributes["code"] = entityAttach.GetString("AttachMentCode");
					this.lblBgImageName.Text = entityAttach.GetString("FileName");
				}
				entityAttach.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾͼƬ����" + ex.Message));
			}
		}

		/// <summary>
		/// ��ʾ�б�
		/// </summary>
		private void LoadDataGrid()
		{
			try 
			{
				string WBSCode = this.ucTask.Value;

				DataTable tb = new DataTable();
				tb.Columns.Add("WBSCode");
				tb.Columns.Add("TaskName");
				tb.Columns.Add("AttachMentCode");
				tb.Columns.Add("ImageName");
				tb.Columns.Add("Deep");

				if (WBSCode != "") 
				{
					//��1����������Ϊ��������
					EntityData entity = DAL.EntityDAO.WBSDAO.GetChildTask(WBSCode);
					foreach(DataRow dr in entity.CurrentTable.Rows)
					{
						string aWBSCode = dr["WBSCode"].ToString();

						DataRow drNew = tb.NewRow();

						drNew["WBSCode"] = dr["WBSCode"];
						drNew["TaskName"] = dr["TaskName"];
						drNew["Deep"] = 1;

						tb.Rows.Add(drNew);

						//ȡͼƬ
                        EntityData entityAttach = RmsPM.DAL.EntityDAO.DAOFactory.GetAttachmentDAO().GetAttachMentByMasterCode("GroundWork", aWBSCode);
						if (entityAttach.HasRecord()) 
						{
							drNew["AttachMentCode"] = entityAttach.GetString("AttachMentCode");
							drNew["ImageName"] = entityAttach.GetString("FileName");
						}
						entityAttach.Dispose();
					}
					entity.Dispose();
				}

				this.dgList.DataSource = tb;
				this.dgList.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
			}
		}

		/*
		/// <summary>
		/// ��ʾ�б�
		/// </summary>
		private void LoadDataGrid()
		{
			try 
			{
				string WBSCode = this.ucTask.Value;

				DataTable tb = new DataTable();
				tb.Columns.Add("WBSCode");
				tb.Columns.Add("TaskName");
				tb.Columns.Add("AttachMentCode");
				tb.Columns.Add("ImageName");
				tb.Columns.Add("Deep");

				if (WBSCode != "") 
				{
					//��1����������Ϊ��������
					EntityData entity = DAL.EntityDAO.WBSDAO.GetChildTask(WBSCode);
					foreach(DataRow dr in entity.CurrentTable.Rows)
					{
						string aWBSCode = dr["WBSCode"].ToString();

						DataRow drNew = tb.NewRow();

						drNew["WBSCode"] = dr["WBSCode"];
						drNew["TaskName"] = dr["TaskName"];
						drNew["Deep"] = 1;

						tb.Rows.Add(drNew);

						//��2����������Ϊ�׶�
						EntityData entityB = DAL.EntityDAO.WBSDAO.GetChildTask(aWBSCode);
						foreach(DataRow drB in entityB.CurrentTable.Rows)
						{
							string bWBSCode = drB["WBSCode"].ToString();

							DataRow drNewB = tb.NewRow();

							drNewB["WBSCode"] = drB["WBSCode"];
							drNewB["TaskName"] = drB["TaskName"];
							drNewB["Deep"] = 2;

							//ȡͼƬ
							EntityData entityAttach = DAL.EntityDAO.WBSDAO.GetAttachMentByMasterCode("GroundWork", bWBSCode);
							if (entityAttach.HasRecord()) 
							{
								drNewB["AttachMentCode"] = entityAttach.GetString("AttachMentCode");
								drNewB["ImageName"] = entityAttach.GetString("FileName");
							}
							entityAttach.Dispose();

							tb.Rows.Add(drNewB);
						}
						entityB.Dispose();
					}
					entity.Dispose();
				}

				this.dgList.DataSource = tb;
				this.dgList.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
			}
		}
*/

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
				EntityData entity = DAL.EntityDAO.ConstructDAO.GetGroundWorkByCode(this.txtGroundWorkCode.Value);
				bool isNew = !entity.HasRecord();
				DataRow dr;

				if (isNew) 
				{
					dr = entity.CurrentTable.NewRow();

					this.txtGroundWorkCode.Value = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("GroundWorkCode");

					dr["GroundWorkCode"] = this.txtGroundWorkCode.Value;
					dr["ProjectCode"] = this.txtProjectCode.Value;
					dr["ParentCode"] = "";

					entity.CurrentTable.Rows.Add(dr);
				}
				else 
				{
					dr = entity.CurrentRow;
				}

				dr["WBSCode"] = this.ucTask.Value;

				DAL.EntityDAO.ConstructDAO.SubmitAllGroundWork(entity);
				entity.Dispose();
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

			if ( this.ucTask.Value.Trim() == "" )
			{
				Hint = "�����빤���� �� ";
				return false;
			}

			if (BLL.ConstructProgRule.IsGroundWorkTaskExists(this.txtGroundWorkCode.Value, this.ucTask.Value, this.txtProjectCode.Value))
			{
				Hint = "�ù���������ӵ���������ͼ �� ";
				return false;
			}

			return true;
		}

		//ˢ��
		protected void btnRefresh_ServerClick(object sender, System.EventArgs e)
		{
			LoadImage();
			LoadDataGrid();
		}

	}
}
