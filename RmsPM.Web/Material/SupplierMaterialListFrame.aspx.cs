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
using RmsPM.BLL;
using RmsPM.DAL.QueryStrategy;
using RmsPM.DAL.EntityDAO;
using RmsPM.Web;
using Rms.ORMap;


namespace RmsPM.Web.Material
{
	/// <summary>
	/// SupplierMaterialListFrame ��ժҪ˵����
	/// </summary>
	public partial class SupplierMaterialListFrame : PageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack)
			{
				IniPage();
				BuildSearchString();
				LoadDataGrid();
			}
		}

		private void IniPage()
		{
            //ֻ��ʾĳ��֦��
            this.txtRootGroupCode.Value = Request.QueryString["RootGroupCode"];
        }

		private void LoadDataGrid() 
		{
			try 
			{
				string sql = (string)this.ViewState["SqlString"];

//                Response.Write(sql);

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData("SupplierMaterial",sql);
				qa.Dispose();
				dgList.DataSource = entity.CurrentTable;
				dgList.DataBind();

				int RecordCount = entity.CurrentTable.Rows.Count;
				this.GridPagination1.RowsCount = RecordCount.ToString();
				this.lblRecordCount.Text = RecordCount.ToString();

				entity.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
			}
		}

		private void BuildSearchString()
		{
			string IsEmpty = Request["IsEmpty"] + "";

			string MaterialTypeCode = Request["MaterialTypeCode"] +"";
            string Unit = Request["Unit"] + "";

            string Price0 = Request["Price0"] + "";
            string Price1 = Request["Price1"] + "";

            string SupplierCode = Request["SupplierCode"] + "";
            string SupplierName = Request["SupplierName"] + "";

            string Brand = Request["Brand"] + "";
            string Model = Request["Model"] + "";
            string Spec = Request["Spec"] + "";
            string Nation = Request["Nation"] + "";
            string AreaCode = Request["AreaCode"] + "";
            string SampleID = Request["SampleID"] + "";

			string chkSearch = Request["ChkSearch"]+"";

            string RootGroupCode = this.txtRootGroupCode.Value;

			SupplierMaterialStrategyBuilder sb = new SupplierMaterialStrategyBuilder();
			
			if (IsEmpty == "1")
				sb.AddStrategy( new Strategy( SupplierMaterialStrategyName.False));

            if (Unit != "")
                sb.AddStrategy(new Strategy(SupplierMaterialStrategyName.Unit, "%" + Unit + "%"));

            if (Price0.Trim() != "" || Price1.Trim() != "")
            {
                ArrayList ar = new ArrayList();
                ar.Add(Price0.Trim());
                ar.Add(Price1.Trim());
                sb.AddStrategy(new Strategy(SupplierMaterialStrategyName.PriceRange, ar));
            }

            if (SupplierCode != "")
                sb.AddStrategy(new Strategy(SupplierMaterialStrategyName.SupplierCode, SupplierCode));

            if (SupplierName != "")
                sb.AddStrategy(new Strategy(SupplierMaterialStrategyName.SupplierName, "%" + SupplierName + "%"));

            if (Brand != "")
                sb.AddStrategy(new Strategy(SupplierMaterialStrategyName.Brand, "%" + Brand + "%"));

            if (Model != "")
                sb.AddStrategy(new Strategy(SupplierMaterialStrategyName.Model, "%" + Model + "%"));

            if (Spec != "")
                sb.AddStrategy(new Strategy(SupplierMaterialStrategyName.Spec, "%" + Spec + "%"));

            if (Nation != "")
                sb.AddStrategy(new Strategy(SupplierMaterialStrategyName.Nation, "%" + Nation + "%"));

            if (AreaCode != "")
				sb.AddStrategy( new Strategy( SupplierMaterialStrategyName.AreaCode, "%"+AreaCode+"%"));

            if (SampleID != "")
                sb.AddStrategy(new Strategy(SupplierMaterialStrategyName.SampleID, "%" + SampleID + "%"));

            if (chkSearch == "1" && (MaterialTypeCode != ""))
			{
				ArrayList arS = new ArrayList();
				arS.Add(MaterialTypeCode);
				arS.Add("0");
				sb.AddStrategy( new Strategy( SupplierMaterialStrategyName.GroupCodeEx, arS ));
			}

            //ֻ��ʾĳ��֦��
            if (RootGroupCode != "")
            {
                ArrayList arS = new ArrayList();
                arS.Add(RootGroupCode);
                arS.Add("0");
                sb.AddStrategy(new Strategy(SupplierMaterialStrategyName.GroupCodeEx, arS));
            }

			ArrayList arA = new ArrayList();
			arA.Add(user.UserCode);
			arA.Add(user.BuildStationCodes());
			sb.AddStrategy( new Strategy( SupplierMaterialStrategyName.AccessRange,arA));

//			sb.AddStrategy( new Strategy( SupplierMaterialStrategyName.SubjectSetCode,subjectSetCode ));

			//����
			string sortsql = BLL.GridSort.GetSortSQL(ViewState);

			string sql = sb.BuildQueryViewString();

			if (sortsql != "")
			{
				//���б�������
				sql = sql + " order by " + sortsql;
			}

			this.ViewState.Add("SqlString",sql);
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
			this.dgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgList_PageIndexChanged);
			this.dgList.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgList_SortCommand);

		}
		#endregion

		private void dgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.dgList.CurrentPageIndex = e.NewPageIndex;
			LoadDataGrid();
		}

		protected void GridPagination1_PageIndexChange(object sender, System.EventArgs e)
		{
			try
			{
				this.LoadDataGrid();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
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
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void dgList_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{
				BLL.GridSort.SortCommand((DataGrid)source, ViewState, source, e);
				((DataGrid)source).CurrentPageIndex = 0;
				BuildSearchString();
				LoadDataGrid();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}



	}
}
