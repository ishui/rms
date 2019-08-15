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

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// FinanceInterfaceAnalysisSupplierList ��ժҪ˵����
	/// </summary>
    public partial class FinanceInterfaceAnalysisSupplierList : PageBase
	{

		protected System.Web.UI.HtmlControls.HtmlInputText txtVoucherID;
		protected System.Web.UI.HtmlControls.HtmlSelect sltAccountant;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAdd;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spanFinanceInterface;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadDataGrid();
			}
		}

		private void IniPage()
		{
			try 
			{
				this.txtSubjectSetCode.Value = Request.QueryString["SubjectSetCode"];

                string FinanceInterfaceSupplierCode = BLL.FinanceRule.GetFinanceInterfaceSupplierCode(this.txtSubjectSetCode.Value);
                ViewState["FinanceInterfaceSupplierCode"] = FinanceInterfaceSupplierCode;
                if (FinanceInterfaceSupplierCode.ToUpper() == "ByGroup".ToUpper())
                {
                    this.sltProject.Items.Clear();
                    this.sltProject.Items.Add(new ListItem("����", "group"));
                }
                else
                {
                    BLL.PageFacade.LoadProjectSelect(this.sltProject, "");
                }
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadDataGrid()
		{
			try
			{
                SupplierSubjectSetStrategyBuilder sb = new SupplierSubjectSetStrategyBuilder();
                sb.AddStrategy(new Strategy(SupplierSubjectSetStrategyName.SubjectSetCode, txtSubjectSetCode.Value));

                if (this.txtSupplierName.Value != "")
                    sb.AddStrategy(new Strategy(SupplierSubjectSetStrategyName.SupplierName, "%" + txtSupplierName.Value + "%"));

                if (this.sltProject.Value != "")
                {
                    if (this.sltProject.Value.ToLower() == "group")
                        sb.AddStrategy(new Strategy(SupplierSubjectSetStrategyName.ProjectCode, ""));
                    else
                        sb.AddStrategy(new Strategy(SupplierSubjectSetStrategyName.ProjectCode, this.sltProject.Value));
                }
                else
                {
                    if (BLL.ConvertRule.ToString(ViewState["FinanceInterfaceSupplierCode"]).ToUpper() != "ByGroup".ToUpper())
                    {
                        sb.AddStrategy(new Strategy(SupplierSubjectSetStrategyName.ProjectNotNull));
                    }
                }

                if (this.txtU8Code.Value != "")
                    sb.AddStrategy(new Strategy(SupplierSubjectSetStrategyName.U8Code, "%" + this.txtU8Code.Value + "%"));

                //����
                string sortsql = BLL.GridSort.GetSortSQL(ViewState);
                if (sortsql == "")
                {
                    //ȱʡ����
                    //				sb.AddOrder( "SupplierName", true);
                    //				sb.AddOrder( "ProjectName", true);
                }

				string sql = sb.BuildQueryViewString();

                if (sortsql != "")
                {
                    //���б�������
                    sql = sql + " order by " + sortsql;
                }
                
                QueryAgent qa = new QueryAgent();
				DataTable tb = qa.ExecSqlForDataSet(sql).Tables[0];
				qa.Dispose();

				this.dgList.DataSource = tb;
				this.dgList.DataBind();

                this.GridPagination1.RowsCount = tb.Rows.Count.ToString();

                tb.Dispose();
            }
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ����" + ex.Message));
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
            this.dgList.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemCreated);
            this.dgList.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgList_SortCommand);
        }
		#endregion

        protected void GridPagination1_PageIndexChange(object sender, System.EventArgs e)
        {
            LoadDataGrid();
        }

        protected void btnSearch_ServerClick(object sender, System.EventArgs e)
        {
            this.dgList.CurrentPageIndex = 0;
            LoadDataGrid();
        }

        private void dgList_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
        {
            try
            {
                BLL.GridSort.SortCommand((DataGrid)source, ViewState, source, e);
                ((DataGrid)source).CurrentPageIndex = 0;
                LoadDataGrid();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
            }
        }

        protected void btnRefresh_ServerClick(object sender, EventArgs e)
        {
            try
            {
                LoadDataGrid();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
            }
        }

        private void dgList_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            try
            {
                BLL.GridSort.ItemCreate((DataGrid)sender, ViewState, sender, e);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
            }
        }
    
    }
}
