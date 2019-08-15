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
using Rms.Web;

namespace RmsPM.Web.CostBudget
{
	/// <summary>
	/// CostTargetTreeData ��ժҪ˵����
	/// </summary>
	public partial class CostTargetTreeData : PageBase
	{
		/// <summary>
		/// ����ֶ�
		/// </summary>
        private string[] m_arrMoneyField = { "TotalBudgetMoney"};
        private string[] m_arrMoneyFieldOriginal = { "TotalBudgetMoney"};

		private decimal[] m_arrSum = new decimal[1];

		private string m_ProjectCode = "";
		private int m_Layer = 0;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
                m_ProjectCode = Request["ProjectCode"] + "";
				m_Layer = BLL.ConvertRule.ToInt(Request["Layer"]);

				string m_strGetType=Request.QueryString["GetType"]+"";				//���ݷ���
				string m_strNodeId=Request.QueryString["NodeId"]+"";				//���ڵ���
				string[] m_Layers=(Request.QueryString["Layers"]+"").Split('.');	//����չ��������
				string m_strSelectedLayer=Request.QueryString["SelectedLayer"]+"";	//����չ�������

				DataTable m_Table = new DataTable("CostBudget");

				m_Table.Columns.Add("Type");
				m_Table.Columns.Add("CostBudgetCode");
                m_Table.Columns.Add("CostBudgetSetCode");
                m_Table.Columns.Add("CostBudgetSetName");
                m_Table.Columns.Add("VerName");
                m_Table.Columns.Add("VerID");
                m_Table.Columns.Add("CostBudgetName");
                m_Table.Columns.Add("StatusName");
                m_Table.Columns.Add("ParentCode");
				m_Table.Columns.Add("Description");
				m_Table.Columns.Add("Layer");
				m_Table.Columns.Add("ChildNodesCount");
				m_Table.Columns.Add("ShowChildNodes");

				m_Table.Columns.Add("ShowSpan");
				m_Table.Columns.Add("ShowHref");

				m_Table.Columns.Add("CreatePersonName");
				m_Table.Columns.Add("CreateDate");
				m_Table.Columns.Add("ModifyPersonName");
				m_Table.Columns.Add("ModifyDate");

                m_Table.Columns.Add("IconName");
                m_Table.Columns.Add("IconDisplay");
                
                //����ֶ�
				foreach(string MoneyField in this.m_arrMoneyField)
				{
					m_Table.Columns.Add(MoneyField);
				}

				string sql = "";

				//ȡĿ����ñ�
				CostBudgetSetStrategyBuilder sb = new CostBudgetSetStrategyBuilder();
				sb.AddStrategy( new Strategy( CostBudgetSetStrategyName.ProjectCode, m_ProjectCode));

				//Ȩ��
				ArrayList arA = new ArrayList();
				arA.Add(user.UserCode);
				arA.Add(user.BuildStationCodes());
				sb.AddStrategy( new Strategy( DAL.QueryStrategy.CostBudgetSetStrategyName.AccessRange,arA));

				//ȱʡ������Ŀ��������������
				sb.AddOrder( "GroupSortID", true);
				sb.AddOrder( "PBSType", true);
				sb.AddOrder( "CostBudgetSetName", true);
				sql = sb.BuildQueryViewString();

				QueryAgent qa = new QueryAgent();
				DataTable m_DataTable = qa.ExecSqlForDataSet(sql).Tables[0];
				qa.Dispose();

                //����������
                if (!m_DataTable.Columns.Contains("DistrictCode"))
                    m_DataTable.Columns.Add("DistrictCode");

//                ApplicationLog.SetLogPath("d:\\Temp\\");

                string filter = "";
                if (m_strGetType == "")
                {
                    #region ȡ��һ��

                    //�ϼ���0
                    for (int i = 0; i < this.m_arrSum.Length; i++)
                        m_arrSum[i] = 0;

                    ArrayList al = new ArrayList();
                    foreach (DataRow dr in m_DataTable.Rows)
                    {
                        string GroupCode = BLL.ConvertRule.ToString(dr["GroupCode"]);
                        if ((GroupCode != "") && (al.IndexOf(GroupCode) < 0))
                        {
                            al.Add(GroupCode);
                        }
                    }

                    foreach(string GroupCode in al)
                    {
                        DataRow[] drs = m_DataTable.Select("GroupCode = '" + GroupCode + "'");
                        if (drs.Length > 0)
                        {
                            DataRow m_NewRow = m_Table.NewRow();
                            this.FillRowCostBudgetSetGroup(m_NewRow,drs[0],m_DataTable);
                            m_Table.Rows.Add(m_NewRow);
                        }
                    }

//                    AddSumRow(m_Table);

                    #endregion
                }
                /*
                else if (m_strGetType == "ChildNodes")
                {
                    #region ȡĳ�ڵ���Ŀ¼

                    filter = "ParentCode='" + m_strNodeId + "'";
                    DataView m_DV = new DataView(m_DataTable, filter, "", DataViewRowState.CurrentRows);
                    foreach (DataRowView m_Row in m_DV)
                    {
                        DataRow m_NewRow = m_Table.NewRow();

                        this.FillRow(m_NewRow, m_Row.Row, m_DataTable);

                        m_Table.Rows.Add(m_NewRow);
                    }
                    #endregion
                }
                */
                else if (m_strGetType == "ChildNodesOfGroup")
                {
                    #region Ԥ�������չ��

                    string GroupCode = m_strNodeId.Replace("G_", "");
                    filter = "GroupCode='" + GroupCode + "'";
                    DataView m_DV = new DataView(m_DataTable, filter, "", DataViewRowState.CurrentRows);

                    //��������������ʾ����
                    foreach (DataRowView m_Row in m_DV)
                    {
                        m_Row.Row["DistrictCode"] = BLL.CostBudgetRule.GetPBSDistrictCode(BLL.ConvertRule.ToString(m_Row.Row["PBSType"]), BLL.ConvertRule.ToString(m_Row.Row["PBSCode"]));
                    }
                    
                    DataView dvDistrict = BLL.ConvertRule.GetDistinct(m_DV, "DistrictCode");

                    //��ʾ����
                    foreach (DataRowView drvDistrict in dvDistrict)
                    {
                        DataRow drDistrict = drvDistrict.Row;
                        string DistrictCode = BLL.ConvertRule.ToString(drDistrict["DistrictCode"]);
                        if (DistrictCode != "")
                        {
                            DataRow m_NewRow = m_Table.NewRow();

                            this.FillRowDistrict(m_NewRow, DistrictCode, GroupCode);

                            m_Table.Rows.Add(m_NewRow);
                        }
                    }

                    //��ʾԤ���
					foreach(DataRowView m_Row in m_DV)
					{
                        if (BLL.ConvertRule.ToString(m_Row.Row["DistrictCode"]) == "")
                        {
                            string CostBudgetSetCode = BLL.ConvertRule.ToString(m_Row.Row["CostBudgetSetCode"]);
                            FillCostBudgetSet(CostBudgetSetCode, m_Row.Row, m_Table, m_DataTable);
                        }
					}

                    #endregion
                }
                else if (m_strGetType == "ChildNodesOfDistrict")
                {
                    #region ����չ��

                    string[] arrCode = m_strNodeId.Replace("D_", "").Split(":"[0]);
                    string GroupCode = arrCode[0];
                    string DistrictCode = arrCode[1];

                    filter = "GroupCode='" + GroupCode + "'";
                    DataView m_DV = new DataView(m_DataTable, filter, "", DataViewRowState.CurrentRows);

                    //��ʾԤ���
                    foreach (DataRowView m_Row in m_DV)
                    {
                        //�Ƿ�������µ�
                        string CurrentDistrictCode = BLL.CostBudgetRule.GetPBSDistrictCode(BLL.ConvertRule.ToString(m_Row.Row["PBSType"]), BLL.ConvertRule.ToString(m_Row.Row["PBSCode"]));

                        if (DistrictCode == CurrentDistrictCode)
                        {
                            string CostBudgetSetCode = BLL.ConvertRule.ToString(m_Row.Row["CostBudgetSetCode"]);
                            FillCostBudgetSet(CostBudgetSetCode, m_Row.Row, m_Table, m_DataTable);
                        }
                    }

                    #endregion
                }
                else if (m_strGetType == "ChildNodesOfTarget")
                {
                    #region Ŀ�����չ������ʾ��Ŀ����õ������汾

                    EntityData entityCurr = DAL.EntityDAO.CostBudgetDAO.GetCostBudgetByCode(m_strNodeId);
                    if (entityCurr.HasRecord())
                    {
                        string CostBudgetSetCode = entityCurr.GetString("CostBudgetSetCode");

                        filter = "CostBudgetSetCode='" + CostBudgetSetCode + "'";
                        DataView m_DV = new DataView(m_DataTable, filter, "", DataViewRowState.CurrentRows);
                        if (m_DV.Count > 0)
                        {
                            //ȡ��Ŀ����õ������汾
                            EntityData entityTarget = DAL.EntityDAO.CostBudgetDAO.GetCostBudgetByStatus(CostBudgetSetCode, 1, "0,1,2,3", true);
                            DataView dvTarget = new DataView(entityTarget.CurrentTable, string.Format("CostBudgetCode <> '{0}'", m_strNodeId), "VerID desc", DataViewRowState.CurrentRows);
                            foreach (DataRowView drvTarget in dvTarget)
                            {
                                DataRow m_NewRow = m_Table.NewRow();
                                this.FillRow(m_NewRow, m_DV[0].Row, m_DataTable, drvTarget.Row);
                                m_Table.Rows.Add(m_NewRow);
                            }

                            entityTarget.Dispose();
                        }
                    }
                    entityCurr.Dispose();

                    #endregion
                }
                /*
                else if (m_strGetType == "SingleNode")
                {
                    #region �����ڵ�

                    filter = "CostBudgetSetCode='" + Request.QueryString["CostBudgetSetCode"] + "" + "'";

                    DataView m_DV = new DataView(m_DataTable, filter, "  ", DataViewRowState.CurrentRows);
                    foreach (DataRowView m_Row in m_DV)
                    {
                        DataRow m_NewRow = m_Table.NewRow();

                        this.FillRow(m_NewRow, m_Row.Row, m_DataTable);

                        m_Table.Rows.Add(m_NewRow);

                    }
                    #endregion
                }
                */

                Response.Write(RmsPM.WebControls.TreeView.XmlTree.GetDataToXmlString(m_Table));
            }
			catch ( Exception ex )
			{
//                ApplicationLog.SetLogPath("d:\\Temp\\");
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}

			Response.End();
		}

        /// <summary>
        /// ���Ԥ���
        /// </summary>
        /// <param name="CostBudgetSetCode"></param>
        /// <param name="m_Table"></param>
        /// <param name="m_DataTable"></param>
        private void FillCostBudgetSet(string CostBudgetSetCode, DataRow m_Row, DataTable m_Table, DataTable m_DataTable)
        {
            //ȡĿ����ñ�
            EntityData entityTarget = DAL.EntityDAO.CostBudgetDAO.GetCostBudgetByStatus(CostBudgetSetCode, 1, "0,1,2,3", true);
            DataRow drTarget = null;

            //��ʾ���°汾��Ŀ�����
            DataView dvTarget = new DataView(entityTarget.CurrentTable, "", "VerID desc", DataViewRowState.CurrentRows);
            if (dvTarget.Count > 0)
                drTarget = dvTarget[0].Row;

            if (drTarget != null) //��Ŀ�����
            {
                DataRow m_NewRow = m_Table.NewRow();
                this.FillRow(m_NewRow, m_Row, m_DataTable, drTarget);
                m_Table.Rows.Add(m_NewRow);

                //�ж���汾ʱ�����Լ���չ��
                if (dvTarget.Count > 1)
                    m_NewRow["ChildNodesCount"] = "1";
            }
            else //δ��Ŀ�����
            {
                DataRow m_NewRow = m_Table.NewRow();
                this.FillRow(m_NewRow, m_Row, m_DataTable, drTarget);
                m_Table.Rows.Add(m_NewRow);

                //��������Ȩ�ޣ���������Ŀ�����
                if (user.HasRight("041202")) //�����½�
                {
                    m_NewRow["VerName"] = " - �½�Ŀ�����";
                }
                else
                {
                    //Ԥ�����ñ��ϲ���ʾ����
                    m_NewRow["ShowSpan"] = "";
                    m_NewRow["ShowHref"] = "none";
                }
            }

            entityTarget.Dispose();
        }

        private void FillColumn(DataRow m_NewRow, DataRow m_Row, DataTable m_DataTable)
        {
            int iColumnCount = m_Row.Table.Columns.Count;
            for (int i = 0; i < iColumnCount; i++)
            {
                string columnName = m_Row.Table.Columns[i].ColumnName;

                if (m_NewRow.Table.Columns.Contains(columnName))
                {
                    if (BLL.ConvertRule.FindArray(this.m_arrMoneyField, columnName, true) >= 0)  //���
                    {
                        if (columnName == "ContractPayPercent") //�ٷֱ�
                        {
                            m_NewRow[columnName] = BLL.StringRule.BuildShowPercentString(m_Row[columnName], "####");
                        }
                        else //���
                        {
                            m_NewRow[columnName] = BLL.CostBudgetPageRule.GetMoneyShowString(m_Row[columnName], BLL.CostBudgetPageRule.m_MoneyUnit.yuan);
                        }
                    }
                    else if ((columnName.ToLower() == "CreateDate".ToLower()) || (columnName.ToLower() == "ModifyDate".ToLower()))
                    {
                        m_NewRow[columnName] = BLL.ConvertRule.ToDateString(m_Row[columnName], "yyyy-MM-dd");
                    }
                    else
                    {
                        m_NewRow[columnName] = m_Row[columnName];
                    }
                }
            }
        }

        /// <summary>
        /// ��Ԥ�����
        /// </summary>
        /// <param name="m_NewRow"></param>
        /// <param name="m_Row"></param>
        /// <param name="m_DataTable"></param>
        private void FillRow(DataRow m_NewRow, DataRow m_Row, DataTable m_DataTable, DataRow drTarget)
        {
            m_NewRow["CostBudgetSetCode"] = m_Row["CostBudgetSetCode"];
            m_NewRow["CostBudgetSetName"] = m_Row["CostBudgetSetName"];

            if (drTarget != null)
                FillColumn(m_NewRow, drTarget, m_DataTable);

            if (drTarget != null)
            {
                //�а汾����ʱ����ʾ�汾���ƣ�������ʾ�汾��
                if (BLL.ConvertRule.ToString(m_NewRow["CostBudgetName"]) != "")
                    m_NewRow["VerName"] = " - " + BLL.ConvertRule.ToString(m_NewRow["CostBudgetName"]);
                else
                    m_NewRow["VerName"] = " - " + "�汾" + m_NewRow["VerID"];
            }

            m_NewRow["Layer"] = m_Layer;
            m_NewRow["ChildNodesCount"] = "0";//BLL.ConvertRule.ToInt(m_Row["ChildCount"]);
            m_NewRow["ShowChildNodes"] = "0";

            m_NewRow["ShowSpan"] = "none";
            m_NewRow["ShowHref"] = "";

            m_NewRow["IconDisplay"] = "none";
        }

        /// <summary>
        /// ��Ԥ���������
        /// </summary>
        /// <param name="m_NewRow"></param>
        /// <param name="m_Row"></param>
        /// <param name="m_DataTable"></param>
        private void FillRowCostBudgetSetGroup(DataRow m_NewRow, DataRow m_Row, DataTable m_DataTable)
        {
            m_NewRow["CostBudgetCode"] = "G_" + m_Row["GroupCode"];
            m_NewRow["CostBudgetSetCode"] = "";
            m_NewRow["CostBudgetSetName"] = m_Row["GroupName"];

            m_NewRow["Layer"] = m_Layer;
            m_NewRow["ChildNodesCount"] = "1";//BLL.ConvertRule.ToInt(m_Row["ChildCount"]);
            m_NewRow["ShowChildNodes"] = "0";

            m_NewRow["ShowSpan"] = "";
            m_NewRow["ShowHref"] = "none";

            m_NewRow["IconDisplay"] = "none";
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="m_NewRow"></param>
        /// <param name="DistrictCode"></param>
        private void FillRowDistrict(DataRow m_NewRow, string DistrictCode, string GroupCode)
        {
            m_NewRow["CostBudgetCode"] = "D_" + GroupCode + ":" + DistrictCode;
            m_NewRow["CostBudgetSetCode"] = "";
            m_NewRow["CostBudgetSetName"] = BLL.ProductRule.GetBuildingName(DistrictCode);

            m_NewRow["Layer"] = m_Layer;
            m_NewRow["ChildNodesCount"] = "1";
            m_NewRow["ShowChildNodes"] = "0";

            m_NewRow["ShowSpan"] = "";
            m_NewRow["ShowHref"] = "none";

            m_NewRow["IconName"] = "BuildingArea.gif";
            m_NewRow["IconDisplay"] = "";
        }

        private void FillSumMoneyField(DataRow m_NewRow, decimal[] m_arrSum, decimal ProjectArea)
        {
            int i = -1;
            foreach (string MoneyField in this.m_arrMoneyField)
            {
                i++;

                if (MoneyField == "ContractPayPercent")
                {
                    if (BLL.ConvertRule.ToDecimal(m_NewRow["ContractTotalMoney"]) == 0)
                    {
                        m_NewRow[MoneyField] = DBNull.Value;
                    }
                    else
                    {
                        m_NewRow[MoneyField] = BLL.MathRule.Round(BLL.ConvertRule.ToDecimal(m_NewRow["ContractPay1"]) / BLL.ConvertRule.ToDecimal(m_NewRow["ContractTotalMoney"]) * 100, 0);
                    }

                    m_NewRow[MoneyField] = BLL.StringRule.BuildShowPercentString(m_NewRow["ContractPayPercent"], "####");

                    //                    BLL.CostBudgetPageRule.CalcPercent(m_NewRow, null);
                }
                else if (MoneyField == "BudgetPrice") //Ԥ�㵥�����
                {
                    if (ProjectArea == 0)
                    {
                        m_NewRow[MoneyField] = DBNull.Value;
                    }
                    else
                    {
                        m_NewRow[MoneyField] = BLL.MathRule.Round(BLL.ConvertRule.ToDecimal(m_NewRow["BudgetMoney"]) / ProjectArea, 2);
                    }

                    m_NewRow[MoneyField] = BLL.CostBudgetPageRule.GetMoneyShowString(m_NewRow[MoneyField], BLL.CostBudgetPageRule.m_MoneyUnit.yuan);
                }
                else if (MoneyField == "ContractOriginalPrice") //���ǰ�������
                {
                    if (ProjectArea == 0)
                    {
                        m_NewRow[MoneyField] = DBNull.Value;
                    }
                    else
                    {
                        m_NewRow[MoneyField] = BLL.MathRule.Round(BLL.ConvertRule.ToDecimal(m_NewRow["ContractMoney"]) / ProjectArea, 2);
                    }

                    m_NewRow[MoneyField] = BLL.CostBudgetPageRule.GetMoneyShowString(m_NewRow[MoneyField], BLL.CostBudgetPageRule.m_MoneyUnit.yuan);
                }
                else if (MoneyField == "BuildingPrice") //�������
                {
                    if (ProjectArea == 0)
                    {
                        m_NewRow[MoneyField] = DBNull.Value;
                    }
                    else
                    {
                        m_NewRow[MoneyField] = BLL.MathRule.Round(BLL.ConvertRule.ToDecimal(m_NewRow["ContractMoney"]) / ProjectArea, 2);
                    }

                    m_NewRow[MoneyField] = BLL.CostBudgetPageRule.GetMoneyShowString(m_NewRow[MoneyField], BLL.CostBudgetPageRule.m_MoneyUnit.yuan);
                }
                else
                {
                    m_NewRow[MoneyField] = BLL.CostBudgetPageRule.GetMoneyShowString(m_arrSum[i], BLL.CostBudgetPageRule.m_MoneyUnit.yuan);
                }
            }
        }

        /// <summary>
        /// �Ӻϼ���
        /// </summary>
        /// <param name="m_Table"></param>
        private void AddSumRow(DataTable m_Table)
        {
            DataRow m_NewRow = m_Table.NewRow();

            m_NewRow["CostBudgetSetCode"] = "R_0";
            m_NewRow["CostBudgetSetName"] = "�ϼ�";

            //ȡ��Ŀ���
            decimal ProjectArea = 0;
            EntityData entityP = DAL.EntityDAO.CostBudgetDAO.GetCostBudgetSetByProjectCode(this.m_ProjectCode);
            DataRow[] drsP = entityP.CurrentTable.Select("PBSType='P' and BuildingArea <> 0");
            if (drsP.Length > 0)
            {
                ProjectArea = BLL.ConvertRule.ToDecimal(drsP[0]["BuildingArea"]);
            }
            entityP.Dispose();

            FillSumMoneyField(m_NewRow, m_arrSum, ProjectArea);

            m_NewRow["Layer"] = m_Layer;
            m_NewRow["ChildNodesCount"] = 0;
            m_NewRow["ShowChildNodes"] = "0";

            m_NewRow["ShowSpan"] = "";
            m_NewRow["ShowHref"] = "none";

            m_NewRow["IconDisplay"] = "none";

            m_Table.Rows.Add(m_NewRow);
        }

        private void FillMoneyField(DataRow m_NewRow, DataRow drFirst)
        {
            if (BLL.ConvertRule.ToString(drFirst["CostBudgetDtlCode"]) == "R_0")  //��1���ǻ��ܽ��
            {
                int i = -1;
                foreach (string MoneyField in this.m_arrMoneyField)
                {
                    i++;

                    if (m_NewRow.Table.Columns.Contains(MoneyField))
                    {
                        if (MoneyField == "ContractPayPercent")
                        {
                            m_NewRow[MoneyField] = BLL.StringRule.BuildShowPercentString(drFirst[this.m_arrMoneyFieldOriginal[i]], "####");
                        }
                        else
                        {
                            m_NewRow[MoneyField] = BLL.CostBudgetPageRule.GetMoneyShowString(drFirst[this.m_arrMoneyFieldOriginal[i]], BLL.CostBudgetPageRule.m_MoneyUnit.yuan);
                        }
                    }
                }
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
