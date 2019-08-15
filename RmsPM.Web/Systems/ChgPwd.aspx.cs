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
using Rms.Web;
using RmsPM.DAL;

namespace RmsPM.Web.Systems
{
	/// <summary>
	/// ChgPwd ��ժҪ˵����
	/// </summary>
	public partial class ChgPwd : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (Request["ModifyPwd"] + "" == "pwd")
            {
                this.pwdtable.Visible = true;
                this.owntable.Visible = false;
            }
            else
            {
                this.pwdtable.Visible = false;
                this.owntable.Visible = true;
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

		private bool CheckValid(string UserCode, string OldPwd, string NewPwd, string ConfirmPwd, ref string Hint,string MsgName) 
		{
			Hint = "";

			EntityData entity = DAL.EntityDAO.SystemManageDAO.GetSystemUserByCode(UserCode);

			try 
			{
				if (!entity.HasRecord()) 
				{
					Hint = "�û������Ҳ�����ǰ�û�";
					return false;
				}

                if (entity.GetString("Password") != OldPwd && MsgName == "����") 
				{
					Hint = "��"+MsgName+"����ȷ";
					return false;
				}
                if (entity.GetString("OwnName") != OldPwd && MsgName == "��������")
                {
                    Hint = "��" + MsgName + "����ȷ";
                    return false;
                }
			}
			finally 
			{
				entity.Dispose();
			}

			if (NewPwd != ConfirmPwd) 
			{
				Hint = "ȷ����"+MsgName+"����"+MsgName+"��һ��";
				return false;
			}

			return true;
		}

		protected void btnOK_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				string UserCode = this.user.UserCode;
				string OldPwd = this.txtOldPwd.Value.Trim();
				string NewPwd = this.txtNewPwd.Value.Trim();
				string ConfirmPwd = this.txtConfirmPwd.Value.Trim();
                string OldOwn = this.txtOldOwn.Value.Trim();
                string NewOwn = this.txtNewOwn.Value.Trim();
                string ConfirmOwn = this.txtConfirmOwn.Value.Trim();
				string Hint = "";
                if ( this.pwdtable.Visible == true)
                {
                    if (!CheckValid(UserCode, OldPwd, NewPwd, ConfirmPwd, ref Hint, "����"))
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
                        return;
                    }
                }
                if (this.owntable.Visible == true)
                {
                    if (!CheckValid(UserCode, OldOwn, NewOwn, ConfirmOwn, ref Hint, "��������"))
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
                        return;
                    }
                }
                if (OldPwd == "" && NewPwd == "" && ConfirmPwd == "")
                {
                    NewPwd = null;
                }
                if (OldOwn == "" && NewOwn == "" && ConfirmOwn == "")
                {
                    NewOwn = null;
                }


                BLL.SystemRule.UpdateUserPwd(UserCode, NewPwd, NewOwn);

				Response.Write(Rms.Web.JavaScript.WinClose(true));
			}
			catch (Exception ex) 
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�޸��������"));
			}
		}
	}
}
