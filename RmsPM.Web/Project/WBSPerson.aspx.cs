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
using RmsPM.BLL;
using RmsPM.DAL.EntityDAO;
using Rms.Web;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// ��ӡ��޸Ĺ���������Ա
	/// </summary>
	public partial class WBSPerson : System.Web.UI.Page
	{
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			InitPage();
			if (!IsPostBack)
			{
				LoadData();
			}
			if ((Request["hFlag"] +"" != "") && (Request["hCode"] + "" != ""))
			{
				ModifyUser();
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
			this.dgUserList.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgUserList_ItemCommand);
			this.SaveToolsButton.ServerClick += new System.EventHandler(this.SaveToolsButton_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void InitPage()
		{
			string UserAccess = Request.QueryString["UserAccess"] + "";

			this.trMaster.Visible = false;
			this.trMonitor.Visible = false;
			this.trExecuter.Visible = false;

			//��������˿ɵ���ִ����
			if (UserAccess == "2")
			{
				this.trExecuter.Visible = true;
			}
			//�ϼ���������˿ɵ��ȱ���������˺ͼල��
			else if (UserAccess == "4")
			{
				this.trMaster.Visible = true;
				this.trMonitor.Visible = true;
			}
		}
		/// <summary>
		/// ��ʼ����Ա�б�
		/// </summary>
		private void LoadData()
		{
			try
			{
				string WBSCode = Request.QueryString["WBSCode"] + "";

				EntityData entityUser = WBSDAO.GetTaskPersonByWBSCode(WBSCode);
				if(entityUser.HasRecord())
				{
					DataTable dt = entityUser.CurrentTable.Clone();
					foreach(DataRow dr in entityUser.CurrentTable.Rows)
					{
						switch (dr["Type"].ToString())
						{
							case "2":
								this.lblMaster.Text = BLL.SystemRule.GetUserName(dr["UserCode"].ToString());
								this.hMasterCode.Value = dr["UserCode"].ToString();
								this.txtMasterDetail.Value = dr["MainTask"].ToString();
								break;

							case "1":
								this.lblMonitor.Text = BLL.SystemRule.GetUserName(dr["UserCode"].ToString());
								this.hMonitorCode.Value = dr["UserCode"].ToString();
								this.txtMonitorDetail.Value = dr["MainTask"].ToString();
								break;

							case "0":
								dt.Rows.Add(dr.ItemArray);
								break;
						}
					}
					this.dgUserList.DataSource = dt;
					this.dgUserList.DataBind();
					Session["UserList"] = dt;
				}

				this.btnMaster.Value = (this.lblMaster.Text == "")?"����������":"����������";
				this.btnMonitor.Value = (this.lblMonitor.Text == "")?"�����ල��":"�����ල��";
			}
			catch( Exception ex)
			{
				ApplicationLog.WriteLog ( this.ToString(),ex,"");
			}
		}

		private void ModifyUser()
		{
			string Flag = Request["hFlag"] + "";
			string Code = Request["hCode"] + "";

			//����Ƿ���ԭ���û� �ظ�
			if (CheckRepeatUser(Code))
			{
				if (Flag == "2")
				{
					this.lblMaster.Text = BLL.SystemRule.GetUserName(Code);
					this.btnMaster.Value = "����������";
					this.hMasterCode.Value = Code;
				}
				else if (Flag == "1")
				{
					this.lblMonitor.Text = BLL.SystemRule.GetUserName(Code);
					this.btnMonitor.Value = " �����ල��";
					this.hMonitorCode.Value = Code;
				}
				else if (Flag == "0")
				{
					DataTable dtUser = ((DataTable)Session["UserList"]).Copy();
					DataRow dr;
					string[] strUserList = Code.Split(',');
					for (int i = 0;i <strUserList.Length; i++)
					{
						dr = dtUser.NewRow();
						dr["TaskPersonCode"] =  SystemManageDAO.GetNewSysCode("TaskPersonCode");
						dr["UserName"] = BLL.SystemRule.GetUserName(strUserList[i]);
						dr["UserCode"] = strUserList[i];
						dtUser.Rows.Add(dr);
					}
					this.dgUserList.DataSource  = dtUser;
					this.dgUserList.DataBind();
					Session["UserList"] = dtUser;
				}
			}
			else
			{
				Response.Write(JavaScript.ScriptStart);
				Response.Write("alert('��ԭ��ֵ�ظ���');");
				Response.Write(JavaScript.ScriptEnd);
			}
			this.hFlag.Value = "";
			this.hCode.Value = "";
		}


		private void SaveToolsButton_ServerClick(object sender, System.EventArgs e)
		{
			string WBSCode = Request.QueryString["WBSCode"];
			EntityData entityUser = WBSDAO.GetTaskPersonByWBSCode(WBSCode);
			WBSDAO.DeleteTaskPerson(entityUser);
			
			try
			{
				DataRow dr;

				//������
				dr = entityUser.GetNewRecord();
				dr["TaskPersonCode"] = SystemManageDAO.GetNewSysCode("TaskPersonCode");
				dr["WBSCode"] = WBSCode;
				dr["MainTask"] =this.txtMasterDetail.Value.Trim();
				dr["UserCode"] = this.hMasterCode.Value;
				dr["Type"] = 2;
				entityUser.AddNewRecord(dr);

				//�ල��
				dr = entityUser.GetNewRecord();
				dr["TaskPersonCode"] = SystemManageDAO.GetNewSysCode("TaskPersonCode");
				dr["WBSCode"] = WBSCode;
				dr["MainTask"] = this.txtMonitorDetail.Value.Trim();
				dr["UserCode"] = this.hMonitorCode.Value;
				dr["Type"] = 1;
				entityUser.AddNewRecord(dr);

				//ִ����
				System.Web.UI.WebControls.TextBox objText = new  TextBox();
				foreach(DataGridItem oDataGridItem in this.dgUserList.Items)
				{
					objText = (TextBox)oDataGridItem.FindControl("txtExecuterDetail");
					dr = entityUser.GetNewRecord();
					dr["TaskPersonCode"] = SystemManageDAO.GetNewSysCode("TaskPersonCode");
					dr["WBSCode"] = WBSCode;
					dr["UserCode"] = this.dgUserList.DataKeys[oDataGridItem.ItemIndex];
					dr["MainTask"] = objText.Text.Trim();
					dr["Type"] = 0;

					entityUser.AddNewRecord(dr);
				}
				WBSDAO.SubmitAllTaskPerson(entityUser);
				entityUser.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"���������Աʧ��");
			}

			Response.Write(JavaScript.ScriptStart);
			Response.Write("window.opener.Select('Base','');");
			Response.Write("window.close();");
			Response.Write(JavaScript.ScriptEnd);
		}


		/// <summary>
		/// �����ӵ��û��Ƿ���ԭ�������ظ�
		/// </summary>
		/// <returns></returns>
		private bool CheckRepeatUser(string UserCode)
		{	
			if (UserCode == this.hMasterCode.Value || UserCode == this.hMonitorCode.Value)
			{
				return false;
			}

			if (Session["UserList"] == null ) 
			{
				return true;
			}

			DataTable dt = ((DataTable)Session["UserList"]).Copy();
			foreach(DataRow dr in dt.Rows)
			{
				if (dr["UserCode"].ToString() == UserCode)
				{
					return false;
				}
			}
			return true;
		}

		//ɾ��ִ����
		private void dgUserList_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.CommandName == "Delete")
			{
				DataTable dt = ((DataTable)Session["UserList"]).Copy();
				
				foreach (DataRow dr in dt.Rows)
				{
					if (dr["UserCode"].ToString() == this.dgUserList.DataKeys[e.Item.ItemIndex].ToString())	
					{
						dt.Rows.Remove(dr);
						break;
					}
				}
				Session["UserList"] = dt;
				this.dgUserList.DataSource = dt;
				this.dgUserList.DataBind();
			}
		}


	}
}
