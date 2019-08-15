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
	/// StationModify ��ժҪ˵����
	/// </summary>
	public partial class StationModify : PageBase
	{



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
			this.dgList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgList_DeleteCommand);

		}
		#endregion

		private void IniPage()
		{
			try 
			{
				string unitCode = Request.QueryString["UnitCode"];
				string fullCode = DAL.EntityDAO.OBSDAO.GetUnitFullCode(unitCode);
				this.txtStationCode.Value = Request.QueryString["StationCode"];
				this.txthUnit.Value = unitCode;
				this.txtUnitName.Value = BLL.SystemRule.GetUnitName(unitCode);
                
                EntityData ds1 = OBSDAO.GetStandard_UnitByCode(unitCode);
                //this.rblRoleLevel.SelectedValue = ds1.GetString("UnitType");

				EntityData units = DAL.EntityDAO.OBSDAO.GetAllUnit();
				foreach ( string tempCode in fullCode.Split(new char[]{'-'}))
				{
					if ( tempCode != "" )
					{
						DataRow[] drsSelect = units.CurrentTable.Select( String.Format("UnitCode='{0}'" ,tempCode ));
						if ( drsSelect.Length>0)
						{
							this.sltAccessRangeUnit.Items.Add( new ListItem( (string)drsSelect[0]["UnitName"],(string)drsSelect[0]["UnitCode"] ));
						}
					}
				}
				units.Dispose();
				BLL.PageFacade.LoadAllRoleSelect(this.sltRole,"");

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "����ҳ�����"));
			}
		}

		private void LoadData()
		{
			string stationCode = txtStationCode.Value;
			string unitCode = this.txthUnit.Value;

			try
			{

				bool isNew = (stationCode=="");
				EntityData ds = null;

				if ( isNew )
				{
                    
					ds = new EntityData("Standard_Station");
					DataRow dr = ds.GetNewRecord();
					dr["StationCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("StationCode");
					dr["UnitCode"] = this.txthUnit.Value;
					ds.AddNewRecord(dr);
					ds.SetCurrentRow(0);
				}
				else
                    ds = OBSDAO.GetStandard_StationByCode(stationCode);
                       //ds = OBSDAO.GetStandard_UnitByCode(unitCode);
                EntityData ds1 = OBSDAO.GetStandard_UnitByCode(unitCode);
				if (ds.HasRecord())
				{
					this.txtStationName.Text = ds.GetString("StationName");
					this.txtDescription.Text = ds.GetString("Description");
					this.txthUnit.Value = ds.GetString("UnitCode");

                    try
					{
                        switch (ds1.GetString("UnitType"))
                        {
                            case "��Ŀ":
                                rblRoleLevel.SelectedValue="0";
                                break;
                            case "����":
                                rblRoleLevel.SelectedValue="3";
                                break ;
                             default :
                                rblRoleLevel.SelectedValue="4";
                                break;
                        }
						//this.rblRoleLevel.SelectedValue = ds.GetInt("RoleLevel").ToString();    //�޸�ǰ
					}
					catch
					{}
                    sltAccessRangeUnit.Value = unitCode;
					this.sltRole.Value = ds.GetString("RoleCode");
					this.sltAccessRangeUnit.Value = ds.GetString("AccessRangeUnitCode");
				}

				this.txtUnitName.Value = BLL.SystemRule.GetUnitName(this.txthUnit.Value);

				//��¼��ǰ���û�����
				this.txtReturnUserCodes.Value = BLL.ConvertRule.Concat(ds.Tables["UserRole"], "UserCode", ",");

				this.dgList.DataSource=ds.Tables["UserRole"];
				this.dgList.DataBind();
				Session["StationEntityData"] = ds;
				ds.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "����ҳ�����"));
			}
		}

		private void WriteRefreshScript( string stationCode  ) 
		{

			Response.Write(Rms.Web.JavaScript.ScriptStart);
			if (this.txtRefreshScript.Value.Trim() != "") 
			{
				Response.Write( "window.opener." + this.txtRefreshScript.Value.Trim());
				Response.Write(Rms.Web.JavaScript.WinClose(false));
			}
			else 
			{
				Response.Write(Rms.Web.JavaScript.OpenerReload(false));
				Response.Write(Rms.Web.JavaScript.WinClose(false));
			}
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}


		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			if( this.txtStationName.Text.Trim() == "" )
			{
				Response.Write(Rms.Web.JavaScript.Alert( true, "�������λ���� ��" ));
				return;
			}

			if ( this.txthUnit.Value.Trim() == "" )
			{
				Response.Write(Rms.Web.JavaScript.Alert(true,"��ѡ���� ��"));
				return;
			}
			string stationCode = "";

			try
			{
				EntityData ds = (EntityData)Session["StationEntityData"];
				stationCode=ds.GetString("StationCode");
				string unitCode = this.txthUnit.Value;
				DataRow dr = ds.CurrentRow;
				dr["UnitCode"] = unitCode;
				dr["StationName"] = this.txtStationName.Text;
				dr["RoleCode"] = this.sltRole.Value;
				dr["Description"] = this.txtDescription.Text;
				int roleLevel = int.Parse( this.rblRoleLevel.SelectedValue);
				dr["RoleLevel"] = roleLevel;
				if ( roleLevel == 3 )
				{
					dr["AccessRangeUnitCode"]=this.sltAccessRangeUnit.Value;
					dr["AccessRangeUnitFullCode"]=DAL.EntityDAO.OBSDAO.GetUnitFullCode(this.sltAccessRangeUnit.Value);
				}
				else
				{
					dr["AccessRangeUnitCode"]="";
					dr["AccessRangeUnitFullCode"]="";
				}

				DAL.EntityDAO.OBSDAO.SubmitAllStandard_Station(ds);
				Session["StationEntityData"] = null;
				ds.Dispose();
			}
			catch( Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�������"));
				return;
			}
	
			WriteRefreshScript( stationCode  );
		}

		protected void btnRefreshConfigUser_ServerClick(object sender, System.EventArgs e)
		{
			string[] codes = this.txtReturnUserCodes.Value.Split(new char[]{','});

			try
			{

				EntityData ds = (EntityData)Session["StationEntityData"];

				//���ԭ���¼
				int c = ds.Tables["UserRole"].Rows.Count;
				for(int i=c-1;i>=0;i--)
				{
					ds.Tables["UserRole"].Rows[i].Delete();
				}

				string stationCode=ds.GetString("StationCode");

				foreach ( string code in codes )
				{
					if ( code!="")
					{
						if ( ds.Tables["UserRole"].Select(String.Format( "UserCode='{0}'" ,code)).Length==0)
						{
							DataRow newDr = ds.GetNewRecord("UserRole");
							newDr["UserRoleCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("UserRoleCode");
							newDr["UserCode"]=code;
							newDr["StationCode"]=stationCode;
							ds.AddNewRecord(newDr,"UserRole");
						}
					}
				}

				//��¼��ǰ���û�����
				this.txtReturnUserCodes.Value = BLL.ConvertRule.Concat(ds.Tables["UserRole"], "UserCode", ",");

				this.dgList.DataSource=ds.Tables["UserRole"];
				this.dgList.DataBind();
				Session["StationEntityData"] = ds;
				ds.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ����"));
			}
		}

		private void dgList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				EntityData ds = (EntityData)Session["StationEntityData"];
				string userCode = e.Item.Cells[0].Text;
				foreach ( DataRow dr in ds.Tables["UserRole"].Select(String.Format( "UserCode='{0}'" ,userCode)))
				{
					dr.Delete();
				}

				//��¼��ǰ���û�����
				this.txtReturnUserCodes.Value = BLL.ConvertRule.Concat(ds.Tables["UserRole"], "UserCode", ",");

				this.dgList.DataSource=ds.Tables["UserRole"];
				this.dgList.DataBind();
				Session["StationEntityData"] = ds;
				ds.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ������"));
			}
		
		}
        protected void txtRole_TextChanged(object sender, EventArgs e)
        {
            sltRole.Items.Clear();
            //sltRole.SelectedIndex = 0;
            BLL.PageFacade.LoadAllRoleSelect(this.sltRole, "");
            for (int i = sltRole.Items.Count - 1; i >=0 ; i--)
            {
                if (!sltRole.Items[i].Text.Contains(txtRole.Text.Trim()))
                    sltRole.Items.Remove(sltRole.Items[i]);
                else
                {
                }
            }
        }
}
}

