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
	/// CostBudgetTreeData ��ժҪ˵����
	/// </summary>
	public partial class CostBudgetTreeData : CostBudgetPageBase
	{
		/// <summary>
		/// ����ֶ�
		/// </summary>
        private string[] m_arrMoneyField = { "BudgetMoney", "BudgetChangeMoney", "ContractTotalMoney", "ContractMoney", "ContractChangeMoney", "ContractApplyMoney", "ContractAccountMoney", "ContractBudgetBalance", "ContractPay1", "ContractPayReal", "ContractPayBalance", "ContractPayPercent", "BudgetPrice", "ContractOriginalPrice", "BuildingPrice" };
        private string[] m_arrMoneyFieldOriginal = { "BudgetMoney", "BudgetChangeMoney", "ContractTotalMoney", "ContractMoney", "ContractChangeMoney", "ContractApplyMoney", "ContractAccountMoney", "ContractBudgetBalance", "ContractPay", "ContractPayReal", "ContractPayBalance", "ContractPayPercent", "BudgetPrice", "ContractOriginalPrice", "BuildingPrice" };

        private decimal[] m_arrSum = new decimal[15];

		private string m_ProjectCode = "";
		private string m_CostBudgetBackupCode = ""; //���ݱ���
		private int m_Layer = 0;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
                m_ProjectCode = Request["ProjectCode"] + "";
				m_CostBudgetBackupCode = Request["CostBudgetBackupCode"] + "";
				m_Layer = BLL.ConvertRule.ToInt(Request["Layer"]);

				string m_strGetType=Request.QueryString["GetType"]+"";				//���ݷ���
				string m_strNodeId=Request.QueryString["NodeId"]+"";				//���ڵ���
				string[] m_Layers=(Request.QueryString["Layers"]+"").Split('.');	//����չ��������
				string m_strSelectedLayer=Request.QueryString["SelectedLayer"]+"";	//����չ�������

				DataTable m_Table = new DataTable("CostBudgetSet");

				m_Table.Columns.Add("Type");
				m_Table.Columns.Add("CostBudgetSetCode");
				m_Table.Columns.Add("CostBudgetCode");
				m_Table.Columns.Add("ParentCode");
				m_Table.Columns.Add("CostBudgetSetName");
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

                //�����������
                m_Table.Columns.Add("AttributesContractBudgetBalance");
                
                //����ֶ�
				foreach(string MoneyField in this.m_arrMoneyField)
				{
					m_Table.Columns.Add(MoneyField);
				}

				string sql = "";

				if (m_CostBudgetBackupCode != "") //����
				{
					//ȡ���ݵ�Ԥ�����ñ�
					CostBudgetBackupSetStrategyBuilder sb = new CostBudgetBackupSetStrategyBuilder();
					sb.AddStrategy( new Strategy( CostBudgetBackupSetStrategyName.ProjectCode, m_ProjectCode));
					sb.AddStrategy( new Strategy( CostBudgetBackupSetStrategyName.CostBudgetBackupCode, m_CostBudgetBackupCode));

					//Ȩ��
					ArrayList arA = new ArrayList();
					arA.Add(user.UserCode);
					arA.Add(user.BuildStationCodes());
					sb.AddStrategy( new Strategy( DAL.QueryStrategy.CostBudgetBackupSetStrategyName.AccessRange,arA));

					//ȱʡ������Ŀ��������������
					sb.AddOrder( "GroupSortID", true);
					sb.AddOrder( "PBSType", true);
					sb.AddOrder( "CostBudgetSetName", true);
					sql = sb.BuildQueryViewString();
				}
				else
				{
					//ȡԤ���
					CostBudgetStrategyBuilder sb = new CostBudgetStrategyBuilder();
					sb.AddStrategy( new Strategy( CostBudgetStrategyName.ProjectCode, m_ProjectCode));

                    //��Ԥ�㣨��Ҫ��ʾ���㣩
                    sb.AddStrategy(new Strategy(CostBudgetStrategyName.SetType, BLL.CostBudgetRule.m_BaseSetType));

					//Ȩ��
					ArrayList arA = new ArrayList();
					arA.Add(user.UserCode);
					arA.Add(user.BuildStationCodes());
					sb.AddStrategy( new Strategy( DAL.QueryStrategy.CostBudgetStrategyName.AccessRange,arA));

					//ȱʡ������Ŀ��������������
					sb.AddOrder( "GroupSortID", true);
					sb.AddOrder( "PBSType", true);
					sb.AddOrder( "CostBudgetSetName", true);
					sql = sb.BuildQueryViewCostDynamicListString();
				}

				QueryAgent qa = new QueryAgent();
				DataTable m_DataTable = qa.ExecSqlForDataSet(sql).Tables[0];
				qa.Dispose();

                //����������
                if (!m_DataTable.Columns.Contains("DistrictCode"))
                    m_DataTable.Columns.Add("DistrictCode");

//                ApplicationLog.SetLogPath("d:\\Temp\\");

                if (m_strGetType == "")
                    GenerateGroupDyn(m_DataTable, true);
                else
                    GenerateGroupDyn(m_DataTable, false);

                string filter = "";
                if (m_strGetType == "")
                {
                    #region ȡ��һ��

                    //					DateTime t1 = DateTime.Now;

                    //�ϼ���0
                    for (int i = 0; i < this.m_arrSum.Length; i++)
                        m_arrSum[i] = 0;

                    BLL.CostBudgetGroupDynamic[] dynGroups = GetAllGroupDyn();

                    if (dynGroups != null)
                    {
                        foreach (BLL.CostBudgetGroupDynamic dynGroup in dynGroups)
                        {
                            DataRow[] drs = m_DataTable.Select("GroupCode = '" + dynGroup.GroupCode + "'");
                            if (drs.Length > 0)
                            {
                                DataRow m_NewRow = m_Table.NewRow();
                                this.FillRowCostBudgetSetGroup(m_NewRow,drs[0],m_DataTable);
                                m_Table.Rows.Add(m_NewRow);
                            }
                        }

                        AddSumRow(m_Table);
                    }


                    //					DateTime t2 = DateTime.Now;
                    //					TimeSpan t = t2.Subtract(t1);
                    //					ApplicationLog.WriteLog(this.ToString(),"ʱ�䣺" + t.Duration().ToString());

                    #endregion
                }
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
                            DataRow m_NewRow = m_Table.NewRow();

                            this.FillRow(m_NewRow, m_Row.Row, m_DataTable);

                            m_Table.Rows.Add(m_NewRow);
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
                            DataRow m_NewRow = m_Table.NewRow();

                            this.FillRow(m_NewRow, m_Row.Row, m_DataTable);

                            m_Table.Rows.Add(m_NewRow);
                        }
                    }

                    #endregion
                }
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

                Response.Write(RmsPM.WebControls.TreeView.XmlTree.GetDataToXmlString(m_Table));
            }
			catch ( Exception ex )
			{
//                ApplicationLog.SetLogPath("d:\\Temp\\");
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}

			Response.End();
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
                    else
                    {
                        m_NewRow[columnName] = m_Row[columnName];
                    }
                }
            }
        }

        /// <summary>
        /// ������������е�Ԥ���
        /// </summary>
        /// <param name="m_DataTable"></param>
        private void GenerateGroupDyn(DataTable m_DataTable, bool IsForce)
        {
            try
            {
                if ((Session["CostBudgetTreeGroupDyn"] == null) || (IsForce))
                {
                    ArrayList al = new ArrayList();
                    foreach (DataRow dr in m_DataTable.Rows)
                    {
                        string GroupCode = BLL.ConvertRule.ToString(dr["GroupCode"]);
                        if ((GroupCode != "") && (al.IndexOf(GroupCode) < 0))
                        {
                            al.Add(GroupCode);
                        }
                    }

                    BLL.CostBudgetGroupDynamic[] arrGroup = new RmsPM.BLL.CostBudgetGroupDynamic[al.Count];

                    int rowIndex = -1;
                    foreach (string GroupCode in al)
                    {
                        rowIndex++;
                        BLL.CostBudgetGroupDynamic dyn = new BLL.CostBudgetGroupDynamic(m_ProjectCode, GroupCode, m_CostBudgetBackupCode);
                        dyn.AutoRefreshHtml = false;
                        dyn.ShowContractAccountMoney = base.ShowContractAccountMoney;
                        dyn.Generate();

                        arrGroup[rowIndex] = dyn;
                    }

                    Session["CostBudgetTreeGroupDyn"] = arrGroup;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ȡ���е�Ԥ���
        /// </summary>
        /// <param name="m_DataTable"></param>
        /// <returns></returns>
        private BLL.CostBudgetGroupDynamic[] GetAllGroupDyn()
        {
            try
            {
                BLL.CostBudgetGroupDynamic[] arrGroup = null;

                /*
                if (Session["CostBudgetTreeGroupDyn"] == null)
                {
                    GenerateGroupDyn(m_DataTable);
                }
                */

                //ֱ��ȡSession
                if (Session["CostBudgetTreeGroupDyn"] != null)
                {
                    arrGroup = (BLL.CostBudgetGroupDynamic[])Session["CostBudgetTreeGroupDyn"];
                }

                return arrGroup;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ȡ���Ԥ���
        /// </summary>
        /// <returns></returns>
        private BLL.CostBudgetGroupDynamic GetGroupDyn(string GroupCode)
        {
            try
            {
                BLL.CostBudgetGroupDynamic dyn = null;

                BLL.CostBudgetGroupDynamic[] arrGroup = GetAllGroupDyn();
                if (arrGroup != null)
                {
                    foreach (BLL.CostBudgetGroupDynamic dynGroup in arrGroup)
                    {
                        if (dynGroup.GroupCode == GroupCode)  //��Ԥ�����
                        {
                            dyn = dynGroup;
                            break;
                        }
                    }
                }

                return dyn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ȡԤ���
        /// </summary>
        /// <param name="m_DataTable"></param>
        /// <returns></returns>
        private BLL.CostBudgetDynamic GetDyn(string CostBudgetSetCode, string GroupCode)
        {
            try
            {
                BLL.CostBudgetDynamic dyn = null;

                BLL.CostBudgetGroupDynamic dynGroup = GetGroupDyn(GroupCode);
                if (dynGroup != null)
                {
                    foreach (BLL.CostBudgetDynamic dynTemp in dynGroup.arrDyn)
                    {
                        if (dynTemp.CostBudgetSetCode == CostBudgetSetCode) //��Ԥ�����ñ�
                        {
                            dyn = dynTemp;
                            break;
                        }
                    }
                }

                return dyn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ��Ԥ�����
        /// </summary>
        /// <param name="m_NewRow"></param>
        /// <param name="m_Row"></param>
        /// <param name="m_DataTable"></param>
        private void FillRow(DataRow m_NewRow, DataRow m_Row, DataTable m_DataTable)
        {
            FillColumn(m_NewRow, m_Row, m_DataTable);

            BLL.CostBudgetDynamic dyn = null;

            dyn = GetDyn(BLL.ConvertRule.ToString(m_Row["CostBudgetSetCode"]), BLL.ConvertRule.ToString(m_Row["GroupCode"]));

            /*
			//�����ܽ��
			if (Session["CostBudgetTreeGroupDyn"] != null)
			{
				//ֱ��ȡSession
				BLL.CostBudgetGroupDynamic[] arrGroup = (BLL.CostBudgetGroupDynamic[])Session["CostBudgetTreeGroupDyn"];
				foreach(BLL.CostBudgetGroupDynamic dynGroup in arrGroup) 
				{
					if (dynGroup.GroupCode == BLL.ConvertRule.ToString(m_Row["GroupCode"]))  //��Ԥ�����
					{
						foreach(BLL.CostBudgetDynamic dynTemp in dynGroup.arrDyn) 
						{
							if (dynTemp.CostBudgetSetCode == BLL.ConvertRule.ToString(m_Row["CostBudgetSetCode"])) //��Ԥ�����ñ�
							{
								dyn = dynTemp;
								break;
							}
						}

						break;
					}
				}
			}
			else
			{
				//��ʱ
				dyn = new BLL.CostBudgetDynamic(m_ProjectCode, BLL.ConvertRule.ToString(m_NewRow["CostBudgetCode"]), BLL.ConvertRule.ToString(m_NewRow["CostBudgetSetCode"]));

				dyn.ShowApportion = false;
				dyn.ShowContractBudget = false;
				dyn.ShowTargetChange = false;

				//ֻ��ʾ��1��������
				dyn.MaxCBSDeep = 1;

				dyn.AutoRefreshHtml = false;

				dyn.Generate();
			}
            */

            if (dyn != null)
            {
                if (dyn.tb.Rows.Count > 0)
                {
                    DataRow drFirst = BLL.CostBudgetDynamic.GetR0(dyn.tb);
                    FillMoneyField(m_NewRow, drFirst);
                }
            }

            m_NewRow["Layer"] = m_Layer;
            m_NewRow["ChildNodesCount"] = "0";//BLL.ConvertRule.ToInt(m_Row["ChildCount"]);
            m_NewRow["ShowChildNodes"] = "0";

            m_NewRow["ShowSpan"] = "none";
            m_NewRow["ShowHref"] = "";

            m_NewRow["IconDisplay"] = "none";

            FillAttributesField(m_NewRow);
        }

        /// <summary>
        /// ��Ԥ���������
        /// </summary>
        /// <param name="m_NewRow"></param>
        /// <param name="m_Row"></param>
        /// <param name="m_DataTable"></param>
        private void FillRowCostBudgetSetGroup(DataRow m_NewRow, DataRow m_Row, DataTable m_DataTable)
        {
            m_NewRow["CostBudgetSetCode"] = "G_" + m_Row["GroupCode"];
            m_NewRow["CostBudgetSetName"] = m_Row["GroupName"];

            //�����ܽ��
            BLL.CostBudgetGroupDynamic dynGroup = GetGroupDyn(BLL.ConvertRule.ToString(m_Row["GroupCode"]));

            /*
			BLL.CostBudgetGroupDynamic dyn = new BLL.CostBudgetGroupDynamic(m_ProjectCode, BLL.ConvertRule.ToString(m_Row["GroupCode"]), m_CostBudgetBackupCode);
			dyn.AutoRefreshHtml = false;
			dyn.Generate();
            */

            if (dynGroup.tb.Rows.Count > 0)
            {
                //ȡ��̯ǰ�ϼ�
                DataRow drFirst = dynGroup.tb.Rows[0];
                FillMoneyField(m_NewRow, drFirst);

                //�ϼ��ۼ�
                int i = -1;
                foreach (string MoneyField in this.m_arrMoneyFieldOriginal)
                {
                    i++;

                    if ((MoneyField != "ContractPayPercent") && (MoneyField != "BudgetPrice") && (MoneyField != "ContractOriginalPrice") && (MoneyField != "BuildingPrice"))
                    {
                        this.m_arrSum[i] = this.m_arrSum[i] + BLL.ConvertRule.ToDecimal(drFirst[MoneyField]);
                    }
                }
            }

            m_NewRow["Layer"] = m_Layer;
            m_NewRow["ChildNodesCount"] = "1";//BLL.ConvertRule.ToInt(m_Row["ChildCount"]);
            m_NewRow["ShowChildNodes"] = "0";

            m_NewRow["ShowSpan"] = "none";
            m_NewRow["ShowHref"] = "";

            m_NewRow["IconDisplay"] = "none";

            FillAttributesField(m_NewRow);
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="m_NewRow"></param>
        /// <param name="DistrictCode"></param>
        private void FillRowDistrict(DataRow m_NewRow, string DistrictCode, string GroupCode)
        {
            m_NewRow["CostBudgetSetCode"] = "D_" + GroupCode + ":" + DistrictCode;
            m_NewRow["CostBudgetSetName"] = BLL.ProductRule.GetBuildingName(DistrictCode);

            //�ϼ���0
            for (int i = 0; i < this.m_arrSum.Length; i++)
                m_arrSum[i] = 0;

            //�����ܽ��
            BLL.CostBudgetGroupDynamic dynGroup = GetGroupDyn(GroupCode);

            if (dynGroup != null)
            {
                foreach (BLL.CostBudgetDynamic dyn in dynGroup.arrDyn)
                {
                    string tempDistrictCode = BLL.CostBudgetRule.GetPBSDistrictCode(dyn.entitySet.GetString("PBSType"), dyn.entitySet.GetString("PBSCode"));
                    if (tempDistrictCode == DistrictCode) //�������µ�Ԥ���
                    {
                        if (dyn.tb.Rows.Count > 0)
                        {
                            //ȡ��̯ǰ�ϼ�
                            DataRow drFirst = BLL.CostBudgetDynamic.GetR0(dyn.tb);

                            //����ۼӵ�����
                            int i = -1;
                            foreach (string MoneyField in this.m_arrMoneyFieldOriginal)
                            {
                                i++;

                                if ((MoneyField != "ContractPayPercent") && (MoneyField != "BudgetPrice") && (MoneyField != "ContractOriginalPrice") && (MoneyField != "BuildingPrice"))
                                {
                                    m_arrSum[i] = m_arrSum[i] + BLL.ConvertRule.ToDecimal(drFirst[MoneyField]);
                                }
                            }
                        }
                    }
                }
            }

            //ȡ�������
            decimal DistrictArea = 0;
            EntityData entityP = DAL.EntityDAO.CostBudgetDAO.GetCostBudgetSetByProjectCode(this.m_ProjectCode);
            DataRow[] drsP = entityP.CurrentTable.Select(string.Format("PBSType='B' and PBSCode='{0}' and BuildingArea <> 0", DistrictCode));
            if (drsP.Length > 0)
            {
                DistrictArea = BLL.ConvertRule.ToDecimal(drsP[0]["BuildingArea"]);
            }
            entityP.Dispose();

            FillSumMoneyField(m_NewRow, m_arrSum, DistrictArea);

            m_NewRow["Layer"] = m_Layer;
            m_NewRow["ChildNodesCount"] = "1";
            m_NewRow["ShowChildNodes"] = "0";

            m_NewRow["ShowSpan"] = "";
            m_NewRow["ShowHref"] = "none";

            m_NewRow["IconName"] = "BuildingArea.gif";
            m_NewRow["IconDisplay"] = "";

            FillAttributesField(m_NewRow);
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
            decimal ProjectArea = BLL.CostBudgetRule.GetCostBudgetProjectArea(this.m_ProjectCode);

            FillSumMoneyField(m_NewRow, m_arrSum, ProjectArea);

            m_NewRow["Layer"] = m_Layer;
            m_NewRow["ChildNodesCount"] = 0;
            m_NewRow["ShowChildNodes"] = "0";

            m_NewRow["ShowSpan"] = "";
            m_NewRow["ShowHref"] = "none";

            m_NewRow["IconDisplay"] = "none";

            FillAttributesField(m_NewRow);

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

        private void FillAttributesField(DataRow m_NewRow)
        {
            if (base.IsRemindContractBudgetBalance)
            {
                string style = BLL.CostBudgetPageRule.GetContractBudgetBalanceRemindStyle(m_NewRow["ContractBudgetBalance"]);
                if (style != "")
                    style = string.Format("style=\"{0}\"", style);
                m_NewRow["AttributesContractBudgetBalance"] = style;
            }
            else
            {
                m_NewRow["AttributesContractBudgetBalance"] = "";
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
