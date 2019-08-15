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

namespace RmsPM.Web.Systems
{
    /// <summary>
    /// RoleModify ��ժҪ˵����
    /// </summary>
    public partial class RoleModify : PageBase
    {
        private string roleCode;
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                IniPage();
                LoadData();
            }
        }

        private void IniPage()
        {
            //			try
            //			{
            //				EntityData ds =  DAL.EntityDAO.SystemManageDAO.GetValidStandard_SystemClass();
            //				string moduleCodeTemp = "";
            //				foreach ( DataRow dr in ds.Tables["SystemClass"].Select("","ClassCode") )
            //				{
            //					if ( moduleCodeTemp == "" )
            //					{
            //						moduleCodeTemp = dr["moduleCode"].ToString();
            //					}
            //					else
            //					{
            //						string moduleCode = dr["moduleCode"].ToString();
            //						if ( moduleCode==moduleCodeTemp )
            //						{
            //							dr["ModuleName"]="";
            //						}
            //						else
            //						{
            //							moduleCodeTemp=moduleCode;
            //						}
            //					}
            //				}
            //				this.classRepeater.DataSource=ds.Tables["SystemClass"];
            //				Page.DataBind();
            //				ds.Dispose();
            //			}
            //			catch ( Exception ex )
            //			{
            //				ApplicationLog.WriteLog(this.ToString(),ex,"");
            //				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ����" + ex.Message));
            //			}
        }

        private void LoadData()
        {
            string roleCode = Request["RoleCode"] + "";

            try
            {
                EntityData entity = DAL.EntityDAO.SystemManageDAO.GetStandard_RoleByCode(roleCode);
                if (entity.HasRecord())
                {
                    this.txtRoleName.Text = entity.GetString("RoleName");
                    this.txtDescription.Text = entity.GetString("Description");
                    this.sortID.Text = entity.GetString("SortID");

                    //					entity.SetCurrentTable("RoleOperation");
                    //					string codes = "";
                    //					int iCount=entity.Tables["RoleOperation"].Rows.Count;
                    //					for ( int i=0;i<iCount;i++)
                    //					{
                    //						entity.SetCurrentRow(i);
                    //						string operationCode = entity.GetString("OperationCode");
                    //						codes += operationCode + "," ;
                    //					}
                    //					this.txtSelect.Value=codes;
                }
                entity.Dispose();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "���ؽ�ɫ����ʧ��");
                Response.Write(Rms.Web.JavaScript.Alert(true, "���ؽ�ɫ����ʧ�ܡ�"));
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

        private void CloseWindow(string roleCode)
        {
            if (this.txtRefreshScript.Value.Trim() != "")
            {
                Response.Write(Rms.Web.JavaScript.ScriptStart);
                Response.Write("window.opener." + this.txtRefreshScript.Value.Trim());
                Response.Write(Rms.Web.JavaScript.WinClose(false));
                Response.Write(Rms.Web.JavaScript.ScriptEnd);
            }
            else
            {
                Response.Write(Rms.Web.JavaScript.ScriptStart);
                Response.Write("window.opener.location = window.opener.location;");
                Response.Write(Rms.Web.JavaScript.PageTo(false, "RoleInfo.aspx?RoleCode=" + roleCode));
                Response.Write(Rms.Web.JavaScript.ScriptEnd);
            }
            Response.End();
        }

        protected void btnSave_ServerClick(object sender, System.EventArgs e)
        {
            string roleName = this.txtRoleName.Text.Trim();
            if (roleName == "")
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, "����д��ɫ����"));
                return;
            }

            string roleCode = Request["RoleCode"] + "";
            bool isNew = (roleCode == "");

            try
            {
                EntityData entity = null;
                DataRow dr = null;

                if (isNew)
                {
                    if (IsClash(roleName))
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, "���ڽ�ɫ������ͻ��"));
                        return;
                    }
                    entity = new EntityData("Standard_Role");
                    roleCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("RoleCode");
                    dr = entity.GetNewRecord();
                    dr["RoleCode"] = roleCode;
                    entity.AddNewRecord(dr);
                }
                else
                {
                    if (IsClash(roleName))
                    {
                        if (IsSameRoleCode(roleCode))
                        {
                            entity = DAL.EntityDAO.SystemManageDAO.GetStandard_RoleByCode(roleCode);
                            dr = entity.CurrentRow;
                        }
                        else
                        {
                            Response.Write(Rms.Web.JavaScript.Alert(true, "���ڽ�ɫ������ͻ��"));
                            return;
                        }        
                    }
                    entity = DAL.EntityDAO.SystemManageDAO.GetStandard_RoleByCode(roleCode);
                    dr = entity.CurrentRow;
                }

                dr["RoleName"] = roleName;
                dr["Description"] = this.txtDescription.Text;
                dr["Description"] = this.txtDescription.Text;
                dr["SortID"] = this.sortID.Text;

                //				entity.DeleteAllTableRow("RoleOperation");
                //				entity.SetCurrentTable("RoleOperation");
                //
                //				foreach ( string code in this.txtSelect.Value.Trim().Split(new char[]{','}) )
                //				{
                //					if ( code != "" )
                //					{
                //						DataRow drM = entity.GetNewRecord();
                //						drM["OperationCode"] = code;
                //						drM["RoleCode"] = roleCode;
                //						entity.AddNewRecord(drM);
                //					}
                //				}

                DAL.EntityDAO.SystemManageDAO.SubmitAllStandard_Role(entity);
                entity.Dispose();

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "�������");
                Response.Write(Rms.Web.JavaScript.Alert(true, "�������"));
                return;
            }

            CloseWindow(roleCode);

        }

        bool IsClash(string roleName)
        {
            DataTable entity = DAL.EntityDAO.SystemManageDAO.GetAllRole().CurrentTable as DataTable;
            foreach (DataRow dr in entity.Rows)
            {
                if (dr["RoleName"].ToString().Equals(roleName))
                {
                    RoleCode = dr["RoleCode"].ToString();
                    return true;
                }
            }
            return false;
            
        }

        bool IsSameRoleCode(string roleCode)
        {
            if (roleCode.Equals(RoleCode))
            {
                return true;
            }
            return false;
            
        }

        string RoleCode
        {
            get { return roleCode; }
            set { roleCode = value; }
        }

        




    }
}
