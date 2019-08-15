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

namespace RmsPM.Web.Systems
{
	/// <summary>
	/// UserInfo ��ժҪ˵����
	/// </summary>
	public partial class UserInfo : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTableCell c;


		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			
			if (!IsPostBack)
			{
				IniPage();
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

		private void IniPage()
		{
			this.txtRefreshScript.Value = Request["RefreshScript"] + "";
			string userCode = Request["UserCode"] + "";
		}

		private void LoadData()
		{
			string userCode=Request["UserCode"] + "";

			try
			{
				EntityData ds = SystemManageDAO.GetStandard_SystemUserByCode(userCode);
				if (ds.HasRecord())
				{
					this.lblUserID.Text=ds.GetString("UserID");
					this.lblUserName.Text=ds.GetString("UserName");

                    this.lblShortName.Text = ds.GetString("ShortUserName");
					this.lblEMail.Text= RmsPM.BLL.PageHelpDisplay.ChangeMessageForDisplay(ds.GetString("MailBox"),"</br>",';');
					this.lblPhone.Text=ds.GetString("Phone");
					this.lblMobile.Text=ds.GetString("Mobile");
					this.lblAddress.Text=ds.GetString("Address");
					this.lblFax.Text=ds.GetString("Fax");
					this.lblBirthDay.Text=ds.GetDateTimeOnlyDate("BirthDay");
					this.lblPhoneHome.Text = ds.GetString("PhoneHome");
					this.lblSortID.Text = ds.GetString("SortID");

					this.tdStationName.InnerHtml = BLL.SystemRule.GetUserStationNameHtml(userCode);


					this.lblSex.Text = ds.GetString("Sex");
					int status = ds.GetInt("Status");
					if ( status == 1 )
						this.lblU.Text = "����";
					else
						this.lblU.Text = "����";

				}

				//��ʾ�������
				this.lblSubjectSetDesc.Text = BLL.FinanceRule.GetFinanceSubjectSetDesc(ds.Tables["SystemUserSubjectSet"]);

				ds.Dispose();

//				RoleStrategyBuilder sb = new RoleStrategyBuilder();
//				sb.AddStrategy( new Strategy( RoleStrategyName.UserCode,userCode ));
//				string sql = sb.BuildMainQueryString();
//				QueryAgent qa = new QueryAgent();
//				EntityData entity = qa.FillEntityData("Role",sql);
//				this.dgList.DataSource = entity.CurrentTable;
//				this.dgList.DataBind();
//				entity.Dispose();
//				qa.Dispose();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ����"));
			}
		}


		private void WriteRefreshScript(  ) 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			if (this.txtRefreshScript.Value.Trim() != "") 
			{
				Response.Write( "window.opener." + this.txtRefreshScript.Value.Trim());
			}
			else 
			{
				Response.Write(Rms.Web.JavaScript.OpenerReload(false));
			}
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			string UserCode = Request["UserCode"] + "" ;
			try
			{
				EntityData entity = DAL.EntityDAO.SystemManageDAO.GetStandard_SystemUserByCode(UserCode);
				DAL.EntityDAO.SystemManageDAO.DeleteStandard_SystemUser(entity);
				entity.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ������"));
				return;
			}

			WriteRefreshScript();
		}


	}
}

