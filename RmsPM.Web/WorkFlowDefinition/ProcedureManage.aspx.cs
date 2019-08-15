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
using Rms.WorkFlow;
using Infragistics.WebUI.UltraWebGrid;
using System.Text;
using System.Configuration;

namespace RmsPM.Web.WorkFlowDefinition
{
	/// *******************************************************************************************
	/// <summary>
	/// ProcedureManage ��ժҪ˵��������������
	/// </summary>
	/// *******************************************************************************************
	public partial class ProcedureManage : PageBase
	{
		protected System.Web.UI.WebControls.DataGrid dgTask;
		protected System.Web.UI.WebControls.DataGrid dgRouter;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAddNewTask;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAddNewRouter;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spanTitle;
	
		/// *******************************************************************************************
		/// <summary>
		/// ҳ���ʼ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// *******************************************************************************************
		protected void Page_Load(object sender, System.EventArgs e)
		{
            //this.btnDeleteProcedure.Visible = false;
			if ( !IsPostBack)
			{
				IniPage();
				LoadData();
				LoadDataGrid();
			}

		}

		/// *******************************************************************************************
		/// <summary>
		/// ��ʼ��
		/// </summary>
		/// *******************************************************************************************
		private void IniPage()
		{
			string procedureCode = Request["ProcedureCode"]+"";
			if ( procedureCode=="")
				this.btnDeleteProcedure.Visible = false;

            this.DropProject.DataSource = new DataView(user.m_EntityDataAccessProject.CurrentTable, "", "ProjectName", DataViewRowState.CurrentRows);
            this.DropProject.DataTextField = "ProjectShortName";
            this.DropProject.DataValueField = "ProjectCode";
            this.DropProject.DataBind();
            ListItem li = new ListItem("--ѡ��--", "");
            this.DropProject.Items.Add(li);

            this.inputSystemGroup.ClassCode = "0902";
		}

		/// *******************************************************************************************
		/// <summary>
		/// ���ݼ���
		/// </summary>
		/// *******************************************************************************************
        private void LoadData()
        {
            string procedureCode = Request["ProcedureCode"] + "";
            Procedure procedure = null;

            if (procedureCode == "")
            {
                procedure = Rms.WorkFlow.DefinitionManager.NewProcedure();
                procedureCode = procedure.ProcedureCode;
                procedure.IsNew = true;
                procedure.CreateDate = DateTime.Now;
                procedure.CreateUser = user.UserCode;
            }
            else
            {
                procedure = Rms.WorkFlow.DefinitionManager.GetProcedureDifinition(procedureCode, false);
            }

            this.DropProject.SelectedIndex = this.DropProject.Items.IndexOf(this.DropProject.Items.FindByValue(procedure.ProjectCode));
            this.txtVerson.Value = procedure.VersionNumber.ToString();
            this.CheckActivity.Checked = (procedure.Activity == 1);

            this.txtProcedureName.Value = procedure.ProcedureName;
            this.txtApplicationPath.Value = procedure.ApplicationPath;
            this.txtApplicationInfoPath.Value = procedure.ApplicationInfoPath;
            this.txtDescription.Value = procedure.Description;
            this.sltType.SelectedIndex = this.sltType.Items.IndexOf(this.sltType.Items.FindByValue(procedure.Type.ToString()));
            if (procedure.SysType != null)
                this.inputSystemGroup.Value = procedure.SysType;
            this.txtProcedureNumber.Value = procedure.Remark;
            this.txtProcedureRemark.Value = procedure.ProcedureRemark;
            this.txtVersionDescription.Value = procedure.VersionDescription;

            Session["Procedure"] = procedure;
        }


		/// *******************************************************************************************
		/// <summary>
		/// �������ݰﶨ�б�
		/// </summary>
		/// *******************************************************************************************
		private void LoadDataGrid()
		{
			Procedure procedure = (Procedure)Session["Procedure"];

			UltraWebGrid UWGridCase3 = ((UltraWebGrid)this.UltraWebTab1.Tabs.GetTab(2).FindControl("UltraWebGrid3"));
			UltraWebGrid UWGridCase4 = ((UltraWebGrid)this.UltraWebTab1.Tabs.GetTab(3).FindControl("UltraWebGrid4"));
			DataGrid dgList1 = ((DataGrid)this.UltraWebTab1.Tabs.GetTab(1).FindControl("dgList"));

			DataSet procedureDS = Rms.WorkFlow.DefinitionManager.SaveProcedureDefinitionData(procedure);


			//����ﶨ
			TaskGridDataBound();
			
			//·�ɰﶨ
			RouterGridDataBound();
			
			

			//��ɫ�ﶨ
			UWGridCase3.DataSource = new DataView ( procedureDS.Tables["WorkFlowRole"],"","Remak",DataViewRowState.CurrentRows);
			UWGridCase3.DataBind();
			for (int i=0;i<UWGridCase3.Rows.Count;i++)
			{
				UWGridCase3.Rows[i].Cells[6].Value = "<a href='javascript:deleteRole(\""+(string)UWGridCase3.Rows[i].Cells[0].Value+"\")'>ɾ��</a>";
			}
			RoleGridDataBound();
			

			if(Request["Debug"]+"" == "1")
			{
				//���԰ﶨ
				UWGridCase4.DataSource = new DataView ( procedureDS.Tables["WorkFlowProcedureProperty"],"","Remak",DataViewRowState.CurrentRows);
				UWGridCase4.DataBind();
				dgList1.DataSource = new DataView ( procedureDS.Tables["WorkFlowProcedureProperty"],"","",DataViewRowState.CurrentRows);
				dgList1.DataBind();
				for (int i=0;i<UWGridCase4.Rows.Count;i++)
				{
					UWGridCase4.Rows[i].Cells[4].Value = "<a href='javascript:deleteProperty(\""+(string)UWGridCase4.Rows[i].Cells[0].Value+"\")'>ɾ��</a>";
				}
				this.RoleTypeBound(UWGridCase4.Columns[2].ValueList);
				this.UltraWebTab1.Tabs[3].Visible = true;
			}
			else
			{
				this.UltraWebTab1.Tabs[3].Visible = false;
			}

			procedureDS.Dispose();
		}
		/// *******************************************************************************************
		/// <summary>
		/// ������������
		/// </summary>
		/// *******************************************************************************************
		private void TaskGridDataBound()
		{
			Procedure procedure = (Procedure)Session["Procedure"];
			UltraWebGrid UWGridCase1 = ((UltraWebGrid)this.UltraWebTab1.Tabs.GetTab(0).FindControl("UltraWebGrid1"));

			DataSet procedureDS = Rms.WorkFlow.DefinitionManager.SaveProcedureDefinitionData(procedure);
			BLL.WorkFlowRule.FillRouterTaskName(procedureDS);

			//����ﶨ
			UWGridCase1.DataSource = new DataView ( procedureDS.Tables["WorkFlowTask"],"","SortID,TaskID",DataViewRowState.CurrentRows);
			UWGridCase1.DataBind();
			for (int i=0;i<UWGridCase1.Rows.Count;i++)
			{
				UWGridCase1.Rows[i].Cells[1].Value = "<a href='#' onclick='javascript:modifyTask("+(string)UWGridCase1.Rows[i].Cells[0].Value+");return false;'>"+(string)UWGridCase1.Rows[i].Cells[1].Value+"</a>";
			}
			ValueList contactTypeTaskType = UWGridCase1.Columns[3].ValueList;
			contactTypeTaskType.ValueListItems.Add(0,"һ��ڵ�");
			contactTypeTaskType.ValueListItems.Add(1,"��ʼ");
			contactTypeTaskType.ValueListItems.Add(2,"����");
			contactTypeTaskType.ValueListItems.Add(3,"�������");
			contactTypeTaskType.ValueListItems.Add(4,"��������");
			contactTypeTaskType.ValueListItems.Add(5,"��ǩ�ڵ�");
			ValueList contactTypeRole = UWGridCase1.Columns[4].ValueList;
			RoleBound(contactTypeRole,procedure);
			ValueList contactTypeProperty = UWGridCase1.Columns[5].ValueList;
			PropertyBound(contactTypeProperty,procedure);
			ValueList contactTypeSelectPerson = UWGridCase1.Columns[6].ValueList;
			contactTypeSelectPerson.ValueListItems.Add("NoSelect","����ѡ��");
			contactTypeSelectPerson.ValueListItems.Add("SinglePerson","��Ա��ѡ");
			contactTypeSelectPerson.ValueListItems.Add("MultiPerson","��Ա��ѡ");
			contactTypeSelectPerson.ValueListItems.Add("UnLimited","����ѡ��");
			
		}
        #region *** �ﶨ���̽�ɫ���� ***
        /// ****************************************************************************
        /// <summary>
        /// ���̽�ɫ�ﶨ
        /// </summary>
        /// <param name="ValueListCase">�ﶨ����</param>
        /// <param name="ProcedureCase">����ʵ��</param>
        /// ****************************************************************************
        public static void RoleBound(ValueList ValueListCase, Procedure ProcedureCase)
        {
            RoleBoundProcess(ValueListCase, "ValueList", ProcedureCase);
        }
        /// ****************************************************************************
        /// <summary>
        /// ���̽�ɫ�ﶨ
        /// </summary>
        /// <param name="HtmlSelectCase">�ﶨ����</param>
        /// <param name="ProcedureCase">����ʵ��</param>
        /// ****************************************************************************
        public static void RoleBound(HtmlSelect HtmlSelectCase, Procedure ProcedureCase)
        {
            RoleBoundProcess(HtmlSelectCase, "HtmlSelect", ProcedureCase);
        }
        /// ****************************************************************************
        /// <summary>
        /// ���̽�ɫ�ﶨ�߼�ʵ��
        /// </summary>
        /// <param name="ObjectCase">�ﶨ����</param>
        /// <param name="ObjectType">�ﶨ����</param>
        /// <param name="ProcedureCase">���̶���</param>
        /// ****************************************************************************
        private static void RoleBoundProcess(object ObjectCase, string ObjectType, Procedure ProcedureCase)
        {
            System.Collections.IDictionaryEnumerator ie = ProcedureCase.GetRoleEnumerator();

            DataTable dt = new DataTable();

            dt.Columns.Add("ItemText");
            dt.Columns.Add("ItemValue");
            dt.Columns.Add("Remak");


            while (ie.MoveNext())
            {
                DataRow drNew = dt.NewRow();

                Role RoleCase = (Role)ie.Value;
                drNew["ItemText"] = RoleCase.RoleName;
                drNew["ItemValue"] = RoleCase.WorkFlowRoleCode;
                drNew["Remak"] = RoleCase.Remak;

                dt.Rows.Add(drNew);
            }

            foreach (DataRow dr in dt.Select("", "Remak", System.Data.DataViewRowState.CurrentRows))
            {
                if (ObjectType == "HtmlSelect")
                {
                    ListItem ListItemCase = new ListItem();
                    ListItemCase.Text = dr["ItemText"].ToString();
                    ListItemCase.Value = dr["ItemValue"].ToString();
                    ((HtmlSelect)ObjectCase).Items.Add(ListItemCase);
                }
                if (ObjectType == "ValueList")
                {
                    ((ValueList)ObjectCase).ValueListItems.Add(dr["ItemValue"].ToString(), dr["ItemText"].ToString());
                }
            }
        }
        #endregion

        #region *** �ﶨ������������ ***
        /// ****************************************************************************
        /// <summary>
        /// �������԰ﶨ
        /// </summary>
        /// <param name="ValueListCase">�ﶨ����</param>
        /// <param name="ProcedureCase">����ʵ��</param>
        /// ****************************************************************************
        public static void PropertyBound(ValueList ValueListCase, Procedure ProcedureCase)
        {
            PropertyBoundProcess(ValueListCase, "ValueList", ProcedureCase);
        }
        /// ****************************************************************************
        /// <summary>
        /// �������԰ﶨ
        /// </summary>
        /// <param name="HtmlSelectCase">�ﶨ����</param>
        /// <param name="ProcedureCase">����ʵ��</param>
        /// ****************************************************************************
        public static void PropertyBound(HtmlSelect HtmlSelectCase, Procedure ProcedureCase)
        {
            PropertyBoundProcess(HtmlSelectCase, "HtmlSelect", ProcedureCase);
        }
        /// ****************************************************************************
        /// <summary>
        /// �������԰ﶨ�߼�ʵ��
        /// </summary>
        /// <param name="ObjectCase">�ﶨ����</param>
        /// <param name="ObjectType">�ﶨ����</param>
        /// <param name="ProcedureCase">���̶���</param>
        /// ****************************************************************************
        private static void PropertyBoundProcess(object ObjectCase, string ObjectType, Procedure ProcedureCase)
        {
            if (ObjectType == "HtmlSelect")
            {
                ListItem ListItemCase = new ListItem();
                ListItemCase.Text = "--ѡ��--";
                ListItemCase.Value = "";
                ((HtmlSelect)ObjectCase).Items.Add(ListItemCase);
            }
            if (ObjectType == "ValueList")
            {
                ((ValueList)ObjectCase).ValueListItems.Add("", "--ѡ��--");
            }

            System.Collections.IDictionaryEnumerator ie = ProcedureCase.GetPropertyEnumerator();
            while (ie.MoveNext())
            {
                Property PropertyCase = (Property)ie.Value;
                string ItemText = PropertyCase.ProcedurePropertyName;
                string ItemValue = PropertyCase.WorkFlowProcedurePropertyCode; ;

                if (ObjectType == "HtmlSelect")
                {
                    ListItem ListItemCase = new ListItem();
                    ListItemCase.Text = ItemText;
                    ListItemCase.Value = ItemValue;
                    ((HtmlSelect)ObjectCase).Items.Add(ListItemCase);
                }
                if (ObjectType == "ValueList")
                {
                    ((ValueList)ObjectCase).ValueListItems.Add(ItemValue, ItemText);
                }
            }
        }
        #endregion
		/// *******************************************************************************************
		/// <summary>
		/// ����·������
		/// </summary>
		/// *******************************************************************************************
		private void RouterGridDataBound()
		{

			Procedure procedure = (Procedure)Session["Procedure"];

			DataSet procedureDS = Rms.WorkFlow.DefinitionManager.SaveProcedureDefinitionData(procedure);
			BLL.WorkFlowRule.FillRouterTaskName(procedureDS);

			UltraWebGrid UWGridCase2 = ((UltraWebGrid)this.UltraWebTab1.Tabs.GetTab(1).FindControl("UltraWebGrid2"));
			UWGridCase2.DataSource = new DataView ( procedureDS.Tables["WorkFlowRouter"],"","SortID",DataViewRowState.CurrentRows);
			UWGridCase2.DataBind();

			for (int i=0;i<UWGridCase2.Rows.Count;i++)
			{
				UWGridCase2.Rows[i].Cells[6].Value = "<a href='javascript:deleteRouter("+(string)UWGridCase2.Rows[i].Cells[0].Value+")'>ɾ��</a>";
				foreach (DataRow drw in procedureDS.Tables["WorkFlowCondition"].Select("RouterCode='"+UWGridCase2.Rows[i].Cells[0].Value+"'"))
				{
					UWGridCase2.Rows[i].Cells[5].Value = drw["Description"].ToString();
				}
			}


			ValueList contactTypeFromTask=UWGridCase2.Columns[2].ValueList;
			ValueList contactTypeToTask=UWGridCase2.Columns[3].ValueList;

			System.Collections.IDictionaryEnumerator ie =  procedure.GetTaskEnumerator();
			DataTable dt = new DataTable();
			dt.Columns.Add("TaskCode");
			dt.Columns.Add("TaskName");
			dt.Columns.Add("TaskID");
			while (ie.MoveNext())
			{
				Task task = (Task)ie.Value;

				DataRow drNew = dt.NewRow();
				drNew["TaskCode"] = task.TaskCode;
				drNew["TaskName"] = task.TaskName;
				drNew["TaskID"] = task.TaskID;
				dt.Rows.Add(drNew);

//				contactTypeFromTask.ValueListItems.Add(task.TaskCode,task.TaskName);
//				contactTypeToTask.ValueListItems.Add(task.TaskCode,task.TaskName);
			}

			foreach ( DataRow dr in dt.Select("","TaskID",System.Data.DataViewRowState.CurrentRows) )
			{
				contactTypeFromTask.ValueListItems.Add(dr["TaskCode"].ToString(),dr["TaskName"].ToString());
				contactTypeToTask.ValueListItems.Add(dr["TaskCode"].ToString(),dr["TaskName"].ToString());
			}

		}
		/// *******************************************************************************************
		/// <summary>
		/// ��ɫ�б������ֶΰﶨ����ʽ��
		/// </summary>
		/// *******************************************************************************************
		private void RoleGridDataBound()
		{
			Procedure procedure = (Procedure)Session["Procedure"];

			UltraWebGrid UWGridCase3 = ((UltraWebGrid)this.UltraWebTab1.Tabs.GetTab(2).FindControl("UltraWebGrid3"));
			
			for(int i=0;i<UWGridCase3.Rows.Count;i++)
			{
				if((string)UWGridCase3.Rows[i].Cells[5].Value == "1")
				{
					UWGridCase3.Rows[i].Cells[2].Value = true;
				}
				else if((string)UWGridCase3.Rows[i].Cells[5].Value == "2")
				{
					UWGridCase3.Rows[i].Cells[2].Value = true;
					//UWGridCase3.Rows[i]. = ;
				}
				else
				{
					UWGridCase3.Rows[i].Cells[2].Value = false;
				}
				string UserCodes = "";
				string StationCodes = "";
				Role RoleCase = procedure.GetRole((string)UWGridCase3.Rows[i].Cells[0].Value);
				System.Collections.IDictionaryEnumerator ieRoleComprise = RoleCase.GetRoleCompriseEnumerator();
				
				while(ieRoleComprise.MoveNext())
				{
					RoleComprise RoleCompriseCase = (RoleComprise)ieRoleComprise.Value;
					if(RoleCompriseCase.RoleType == RoleType.Porson)
					{
						if(UserCodes == "")
							UserCodes = RoleCompriseCase.RoleCompriseItem;
						else
							UserCodes += ","+RoleCompriseCase.RoleCompriseItem;
					}
					if(RoleCompriseCase.RoleType == RoleType.Station)
					{
						if(StationCodes == "")
							StationCodes = RoleCompriseCase.RoleCompriseItem;
						else
							StationCodes += ","+RoleCompriseCase.RoleCompriseItem;
					}
				}

				UWGridCase3.Rows[i].Cells[4].Value = "<a href='#' onclick='javascript:SelectRoleComprise(\""+(string)UWGridCase3.Rows[i].Cells[0].Value+"\",\""+UserCodes+"\",\""+StationCodes+"\");return false;'>��ɫά��</a>";
			}
		}

		/// *******************************************************************************************
		/// <summary>
		/// �������̰�ť
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// *******************************************************************************************
		protected void btnSaveProcedure_ServerClick(object sender, System.EventArgs e)
		{
            try
            {
                if (!Rms.Check.StringCheck.IsNumber(this.txtVerson.Value))
                {
                    this.RegisterStartupScript("", "<script>alert('����汾�ű�������ֵ���ͣ���������λС��λ��');</script>");
                }
                else
                {
                    SaveRouterData();
                    SaveRoleData();
                    if (Request["Debug"] + "" == "1")
                    {
                        SavePropertyData();
                    }

                    Procedure procedure = (Procedure)Session["Procedure"];
                    procedure.ProcedureName = this.txtProcedureName.Value;
                    procedure.ApplicationPath = this.txtApplicationPath.Value;
                    //procedure.ApplicationInfoPath = this.txtApplicationPath.Value;
                    procedure.ApplicationInfoPath = this.txtApplicationInfoPath.Value;
                    procedure.Description = this.txtDescription.Value;
                    if (this.inputSystemGroup.Value != "")
                        procedure.SysType = inputSystemGroup.Value;
                    if (this.sltType.Value != "")
                        procedure.Type = int.Parse(this.sltType.Value);
                    procedure.Remark = this.txtProcedureNumber.Value;
                    procedure.VersionNumber = Decimal.Parse(this.txtVerson.Value);
                    procedure.ProjectCode = this.DropProject.SelectedValue;
                    procedure.ModifyUser = user.UserCode;
                    procedure.ModifyDate = DateTime.Now;
                    if (procedure.CreateUser == null)
                    {
                        procedure.CreateUser = user.UserCode;
                        procedure.CreateDate = DateTime.Now;
                    }
                    procedure.Activity = (this.CheckActivity.Checked) ? 1 : 0;
                    procedure.VersionDescription = txtVersionDescription.Value;
                    procedure.ProcedureRemark = txtProcedureRemark.Value;
                    Rms.WorkFlow.DefinitionManager.SaveProcedureDifinition(procedure);
                    //CloseWindow();
                    this.RegisterStartupScript("", "<script>alert('��ǰ���ñ���ɹ���');</script>");
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
		}

		/// *******************************************************************************************
		/// <summary>
		/// ɾ�����̰�ť
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// *******************************************************************************************
		protected void btnDeleteProcedure_ServerClick(object sender, System.EventArgs e)
		{
			/* ��ʱ���ε�ǰ����
             * string procedureCode = Request["ProcedureCode"]+"";
			if ( procedureCode == "" )
				return;

			try
			{
				Rms.WorkFlow.DefinitionManager.RemoveProcedureDefinition(procedureCode);
				RmsPM.BLL.WorkFlowRule.DeleteWorkFlowProcedure(procedureCode);
				CloseWindow();
			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
             * */
            if (BLL.WorkFlowRule.GetCaseCountByProcedureCode(Request["ProcedureCode"] + "") != 0)
            {
                this.RegisterClientScriptBlock("msgdeleteprocedure", "<script>alert('�Ѿ����ڵ�ǰ����Ӧ�ã����Բ�����ɾ����');</script>");
            }
            else
            {
                using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RmsPM.Data.ConnectionString"].ConnectionString))
                {
                    conn.Open();
                    System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = @"
delete from workflowprocedure where procedurecode='" + Request["ProcedureCode"] + @"'
delete from workflowprocedureproperty where procedurecode='" + Request["ProcedureCode"] + @"'
delete from workflowrouter where procedurecode='" + Request["ProcedureCode"] + @"'
delete from workflowcondition where procedurecode='" + Request["ProcedureCode"] + @"'
delete from workflowtask where procedurecode='" + Request["ProcedureCode"] + @"'
delete from workflowtaskactor where procedurecode='" + Request["ProcedureCode"] + @"'
delete from workflowrole where procedurecode='" + Request["ProcedureCode"] + @"' and RoleType='0'
delete from workflowrolecomprise where procedurecode='" + Request["ProcedureCode"] + @"' and workflowrolecode in (select workflowrolecode from workflowrole where RoleType='0')";
                    System.Data.SqlClient.SqlTransaction tran = conn.BeginTransaction();
                    comm.Transaction = tran;
                    try
                    {
                        comm.ExecuteNonQuery();
                        tran.Commit();
                        conn.Close();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        tran.Rollback();
                        conn.Close();
                        throw ex;
                    }
                    finally
                    {
                        conn.Close();
                    }
                    comm.Dispose();
                }
                CloseWindow();
            }
			
		}

		/// *******************************************************************************************
		/// <summary>
		/// �رմ��ڲ���������
		/// </summary>
		/// *******************************************************************************************
		private void CloseWindow()
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write(Rms.Web.JavaScript.OpenerReload(false));
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		/// *******************************************************************************************
		/// <summary>
		/// ���·��
		/// </summary>
		/// *******************************************************************************************
		private void SaveRouterData()
		{
			try
			{
				Procedure procedure =(Procedure) Session["Procedure"];
				UltraWebGrid UWGridCase = ((UltraWebGrid)this.UltraWebTab1.Tabs.GetTab(1).FindControl("UltraWebGrid2"));

				for (int i=0 ;i<UWGridCase.Rows.Count;i++)
				{
					bool isNew = (UWGridCase.Rows[i].Cells[0].Text == null);
					Router router = null;
					if ( isNew )
					{
						router = Rms.WorkFlow.DefinitionManager.NewRouter();
						router.ProcedureCode = procedure.ProcedureCode;
						procedure.AddNewRouter(router);
					}
					else
					{
						router = procedure.GetRouter(UWGridCase.Rows[i].Cells[0].Text);
					}

					//string aa = UWGridCase.Rows[i].Cells[2].Value.ToString();

					router.FromTaskCode = UWGridCase.Rows[i].Cells[2].Value.ToString();
					router.ToTaskCode = UWGridCase.Rows[i].Cells[3].Value.ToString();
					router.Description = UWGridCase.Rows[i].Cells[1].Value.ToString();
					//if ( Rms.Check.StringCheck.IsInt(this.txtSortID.Value ))
					router.SortID = int.Parse(UWGridCase.Rows[i].Cells[4].Value.ToString());

					/////////////////
					System.Collections.IDictionaryEnumerator ie = router.GetConditionEnumerator();
					bool RouterConditionIsNull = true;
					Condition condition = null;
					while(ie.MoveNext())
					{
						condition = (Condition)ie.Value;
						if(condition.ProcedureCode == router.ProcedureCode && condition.RouterCode == router.RouterCode)
						{
							if(UWGridCase.Rows[i].Cells[5].Value != null)
							{
								condition.Description = UWGridCase.Rows[i].Cells[5].Value.ToString();
							}
							else
							{
								condition.Description = "";
							}
							RouterConditionIsNull = false;
						}
					}
					if(RouterConditionIsNull)
					{
						condition = Rms.WorkFlow.DefinitionManager.NewCondition();
						condition.ProcedureCode = router.ProcedureCode;
						condition.RouterCode = router.RouterCode;
						if(UWGridCase.Rows[i].Cells[5].Value != null)
						{
							condition.Description = UWGridCase.Rows[i].Cells[5].Value.ToString();
						}
						else
						{
							condition.Description = "";
						}
						router.AddNewCondition(condition);
					}
					/////////////////
				}
				Session["Procedure"]=procedure;
			}
			catch(Exception ex)
			{
                throw ex;
			}
		}

		/// *******************************************************************************************
		/// <summary>
		/// ��ӽ�ɫ
		/// </summary>
		/// *******************************************************************************************
		private void SaveRoleData()
		{
			try
			{
				Procedure procedure =(Procedure) Session["Procedure"];
				UltraWebGrid UWGridCase = ((UltraWebGrid)this.UltraWebTab1.Tabs.GetTab(2).FindControl("UltraWebGrid3"));

				for (int i=0 ;i<UWGridCase.Rows.Count;i++)
				{
					bool isNew = (UWGridCase.Rows[i].Cells[0].Text == null);
					Role RoleCase = null;
					if ( isNew )
					{
						RoleCase = Rms.WorkFlow.DefinitionManager.NewRole();
						RoleCase.ProcedureCode = procedure.ProcedureCode;
						procedure.AddNewRole(RoleCase);
					}
					else
					{
						RoleCase = procedure.GetRole(UWGridCase.Rows[i].Cells[0].Text);
					}

					RoleCase.RoleName = (string)UWGridCase.Rows[i].Cells[1].Value;

					if((bool)UWGridCase.Rows[i].Cells[2].Value)
					{
						//RoleCase.ProcedureCode = "";???��ΪҪ���ڸ����д����Ӽ�ֵ���Ե�ǰ����ʹ�ø�ģʽ
						RoleCase.RoleType = "1";
					}
					else
					{
						RoleCase.RoleType = "0";
					}
					RoleCase.Remak = (string)UWGridCase.Rows[i].Cells[3].Value;
				}
				Session["Procedure"]=procedure;
			}
			catch(Exception ex)
			{
                throw ex;
			}
		}

		/// *******************************************************************************************
		/// <summary>
		/// �������
		/// </summary>
		/// *******************************************************************************************
		private void SavePropertyData()
		{
			try
			{
				Procedure procedure =(Procedure) Session["Procedure"];
				UltraWebGrid UWGridCase = ((UltraWebGrid)this.UltraWebTab1.Tabs.GetTab(3).FindControl("UltraWebGrid4"));

				for (int i=0 ;i<UWGridCase.Rows.Count;i++)
				{
					bool isNew = (UWGridCase.Rows[i].Cells[0].Text == null);
					Property PropertyCase = null;
					if ( isNew )
					{
						PropertyCase = Rms.WorkFlow.DefinitionManager.NewProperty();
						PropertyCase.ProcedureCode = procedure.ProcedureCode;
						procedure.AddNewProperty(PropertyCase);
					}
					else
					{
						PropertyCase = procedure.GetProperty(UWGridCase.Rows[i].Cells[0].Text);
					}

					PropertyCase.ProcedurePropertyName = (string)UWGridCase.Rows[i].Cells[1].Value;
					if((string)UWGridCase.Rows[i].Cells[2].Value == null || (string)UWGridCase.Rows[i].Cells[2].Value == "--ѡ��--")
					{
						PropertyCase.ProcedurePropertyType = "";
					}
					else
					{
						PropertyCase.ProcedurePropertyType = (string)UWGridCase.Rows[i].Cells[2].Value;
					}
					PropertyCase.Remak = (string)UWGridCase.Rows[i].Cells[3].Value;
				}
				Session["Procedure"]=procedure;
			}
			catch(Exception ex)
			{
                throw ex;
			}
		}

		#region *** �ﶨ�������� ***
		/// ****************************************************************************
		/// <summary>
		/// �ﶨ��������
		/// </summary>
		/// <param name="ValueListCase">�ﶨ����</param>
		/// ****************************************************************************
		private void RoleTypeBound(ValueList ValueListCase)
		{
			RoleTypeBoundProcess(ValueListCase,"ValueList");
		}
		/// ****************************************************************************
		/// <summary>
		/// �ﶨ��������
		/// </summary>
		/// <param name="HtmlSelectCase">�ﶨ����</param>
		/// ****************************************************************************
		private void RoleTypeBound(HtmlSelect HtmlSelectCase)
		{
			RoleTypeBoundProcess(HtmlSelectCase,"HtmlSelect");
		}
		/// ****************************************************************************
		/// <summary>
		/// �ﶨ���������߼�ʵ��
		/// </summary>
		/// <param name="ObjectCase">�ﶨ����</param>
		/// <param name="ObjectType">�ﶨ����</param>
		/// ****************************************************************************
		private void RoleTypeBoundProcess(object ObjectCase,string ObjectType)
		{
			if(ObjectType == "HtmlSelect")
			{
				ListItem ListItemCase = new ListItem();
				ListItemCase.Text = "--ѡ��--";
				ListItemCase.Value = "";
				((HtmlSelect)ObjectCase).Items.Add(ListItemCase);
			}
			if(ObjectType == "ValueList")
			{
				((ValueList)ObjectCase).ValueListItems.Add("","--ѡ��--");
			}


			Type rtype = typeof(RoleType);
			foreach(string s in Enum.GetNames(rtype))
			{
				string ItemText = "δ֪";
				string ItemValue = s;
				switch (s)
				{
					case "Unit":
						ItemText = "����";
						break;
					case "Porson":
						ItemText = "��";
						break;
					case "Station":
						ItemText = "��λ";
						break;
					case "All":
						ItemText = "����";
						break;
					case "Other":
						ItemText = "����";
						break;
					case "Int":
						ItemText = "��ֵ";
						break;
					default:
						ItemText = "δ֪";
						break;
				}
				if(ObjectType == "HtmlSelect")
				{
					ListItem ListItemCase = new ListItem();
					ListItemCase.Text = ItemText;
					ListItemCase.Value = s;
					((HtmlSelect)ObjectCase).Items.Add(ListItemCase);
				}
				if(ObjectType == "ValueList")
				{
					((ValueList)ObjectCase).ValueListItems.Add(ItemValue,ItemText);
				}
			}
		}
		#endregion

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


		/// *******************************************************************************************
		/// <summary>
		/// �����ɫ��ɵ�������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// *******************************************************************************************
		protected void btnRoleCompriseSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				Procedure procedure =(Procedure) Session["Procedure"];
				Role RoleCase = procedure.GetRole(this.RoleCompriseCode.Value);
				RoleCase.ClearRoleComprises();

				foreach ( string sss in this.RoleCompriseUserCodes.Value.Split( new char[]{','} ))
				{
					if( sss != "" )
					{
						RoleComprise RoleCompriseCase = null;
						RoleCompriseCase = Rms.WorkFlow.DefinitionManager.NewRoleComprise();

						RoleCase.AddNewRoleComprise(RoleCompriseCase);

                        RoleCompriseCase.ProcedureCode = procedure.ProcedureCode;
						RoleCompriseCase.RoleCode = RoleCase.WorkFlowRoleCode;
						RoleCompriseCase.RoleCompriseItem = sss;
						RoleCompriseCase.RoleType = RoleType.Porson;
					}
				}
				foreach ( string sss in this.RoleCompriseStationCodes.Value.Split( new char[]{','} ))
				{
					if( sss != "" )
					{
						RoleComprise RoleCompriseCase = null;
						RoleCompriseCase = Rms.WorkFlow.DefinitionManager.NewRoleComprise();

						RoleCase.AddNewRoleComprise(RoleCompriseCase);

                        RoleCompriseCase.ProcedureCode = procedure.ProcedureCode;
						RoleCompriseCase.RoleCode = RoleCase.WorkFlowRoleCode;
						RoleCompriseCase.RoleCompriseItem = sss;
						RoleCompriseCase.RoleType = RoleType.Station;
					}
				}
				Session["Procedure"]=procedure;
				this.RoleGridDataBound();

			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"����ҳ�����");
			}
		}

		/// <summary>
		/// ˢ��ҳ�����¼���Task����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnRefresh_ServerClick(object sender, System.EventArgs e)
		{
			this.TaskGridDataBound();
			this.RouterGridDataBound();
			
		
		}

		/// <summary>
		/// ɾ����ɫ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDeleteRole_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string RoleCode = this.DeleteObjectCode.Value;
				Procedure procedure =(Procedure) Session["Procedure"];
				procedure.RemoveRole(RoleCode);
				Session["Procedure"]=procedure;
				//��ɫ�ﶨ
				UltraWebGrid UWGridCase3 = ((UltraWebGrid)this.UltraWebTab1.Tabs.GetTab(2).FindControl("UltraWebGrid3"));
				DataSet procedureDS = Rms.WorkFlow.DefinitionManager.SaveProcedureDefinitionData(procedure);
				UWGridCase3.DataSource = new DataView ( procedureDS.Tables["WorkFlowRole"],"","",DataViewRowState.CurrentRows);
				UWGridCase3.DataBind();
				for (int i=0;i<UWGridCase3.Rows.Count;i++)
				{
					UWGridCase3.Rows[i].Cells[6].Value = "<a href='javascript:deleteRole(\""+(string)UWGridCase3.Rows[i].Cells[0].Value+"\")'>ɾ��</a>";
				}
				RoleGridDataBound();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"����ҳ�����");
			}
		
		}

		/// <summary>
		/// ɾ��·��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDeleteRouter_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string routerCode = this.DeleteObjectCode.Value;
				Procedure procedure =(Procedure) Session["Procedure"];
				procedure.RemoveRouter(routerCode,false);
				Session["Procedure"]=procedure;
				RouterGridDataBound();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"����ҳ�����");
			}
		
		}

		/// <summary>
		/// ɾ������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDeleteProperty_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string PropertyCode = this.DeleteObjectCode.Value;
				Procedure procedure =(Procedure) Session["Procedure"];
				procedure.RemoveProperty(PropertyCode);
				Session["Procedure"]=procedure;
				//���԰ﶨ
				UltraWebGrid UWGridCase4 = ((UltraWebGrid)this.UltraWebTab1.Tabs.GetTab(3).FindControl("UltraWebGrid4"));
				DataSet procedureDS = Rms.WorkFlow.DefinitionManager.SaveProcedureDefinitionData(procedure);
				UWGridCase4.DataSource = new DataView ( procedureDS.Tables["WorkFlowProcedureProperty"],"","",DataViewRowState.CurrentRows);
				UWGridCase4.DataBind();
				for (int i=0;i<UWGridCase4.Rows.Count;i++)
				{
					UWGridCase4.Rows[i].Cells[4].Value = "<a href='javascript:deleteProperty(\""+(string)UWGridCase4.Rows[i].Cells[0].Value+"\")'>ɾ��</a>";
				}
				this.RoleTypeBound(UWGridCase4.Columns[2].ValueList);
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"����ҳ�����");
			}
		
		}

	}
}
