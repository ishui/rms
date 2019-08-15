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


namespace RmsPM.Web.Supplier
{
	/// <summary>
	/// Supplier ��ժҪ˵����
	/// </summary>
	public partial class SupplierListFrame : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if (!IsPostBack)
			{
				IniPage();
				BuildSearchString();
				LoadDataGrid();
			}
		}


		private void IniPage()
		{
		}

		private void LoadDataGrid() 
		{
			try 
			{
				string sql = (string)this.ViewState["SqlString"];
				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData("Supplier",sql);
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

			string SupplierTypeCode = Request["SupplierTypeCode"] +"";
			string SupplierName = Request["SupplierName"] + "";
			string Abbreviation = Request["Abbreviation"] + "";
			string AreaCode = Request["AreaCode"] + "";
			string Quality = Request["Quality"] + "";
			string RegisteredAddress = Request["RegisteredAddress"] + "";
			string ContractPerson = Request["ContractPerson"] + "";
			string IndustryType = Request["IndustryType"] + "";
			string Achievement = Request["Achievement"] + "";
			string CheckOpinion = Request["CheckOpinion"] + "";

			string chkSearch = Request["ChkSearch"]+"";
            //Ʒ�����
            string characterType = Request["CharacterType"] + "";
            //���õǼ�
            string CreditLevel = Request["CreditLevel"] + "";

            //�������
            string IsExistsContract = Request["IsExistsContract"] + "";
            string SellForm = Request["SellForm"] + "";
            string cccAttestation = Request["cccAttestation"] + "";
            string isoAttestation = Request["isoAttestation"] + "";
            string QualityLevel = Request["QualityLevel"] + "";
            string IsAuditted = "";
            //ʱ��������Ҫ���
            if (this.up_sPMNameLower == "shidaipm")
            {
                 IsAuditted = Request["IsAuditted"] + "";
            }
			SupplierStrategyBuilder sb = new SupplierStrategyBuilder();
			
			if (IsEmpty == "1")
				sb.AddStrategy( new Strategy( SupplierStrategyName.False));

			if ( SupplierName != "")
				sb.AddStrategy( new Strategy( SupplierStrategyName.SupplierName, "%"+SupplierName+"%" ));

			if ( Abbreviation != "")
				sb.AddStrategy( new Strategy( SupplierStrategyName.Abbreviation, "%"+Abbreviation+"%"));

			if ( AreaCode != "")
				sb.AddStrategy( new Strategy( SupplierStrategyName.AreaCode, "%"+AreaCode+"%"));

			if ( Quality != "")
				sb.AddStrategy( new Strategy( SupplierStrategyName.Quality, "%"+Quality+"%"));

			if ( RegisteredAddress != "")
				sb.AddStrategy( new Strategy( SupplierStrategyName.RegisteredAddress, "%"+RegisteredAddress+"%"));

			if ( ContractPerson != "")
				sb.AddStrategy( new Strategy( SupplierStrategyName.ContractPerson, "%"+ContractPerson+"%"));

			if ( IndustryType != "")
				sb.AddStrategy( new Strategy( SupplierStrategyName.IndustryType, "%"+IndustryType+"%"));

			if ( Achievement != "")
				sb.AddStrategy( new Strategy( SupplierStrategyName.Achievement, "%"+Achievement+"%"));

			if ( CheckOpinion != "")
				sb.AddStrategy( new Strategy( SupplierStrategyName.CheckOpinion, "%"+CheckOpinion+"%"));

			if ( chkSearch == "1"  && (SupplierTypeCode != "" ))
			{
				ArrayList arS = new ArrayList();
				arS.Add(SupplierTypeCode);
				arS.Add("0");
				sb.AddStrategy( new Strategy( SupplierStrategyName.SupplierTypeCodeEx, arS ));
			}

            if(characterType!="")
                sb.AddStrategy(new Strategy(SupplierStrategyName.CharacterType, "%" + characterType + "%"));
            if (CreditLevel != "")
                sb.AddStrategy(new Strategy(SupplierStrategyName.CreditLevel, "%" + CreditLevel + "%"));

            if (IsExistsContract != "")
                sb.AddStrategy(new Strategy(SupplierStrategyName.IsExistsContract, IsExistsContract));


            if (SellForm != "")
                sb.AddStrategy(new Strategy(SupplierStrategyName.SaleType, "%" + SellForm + "%"));
            if (cccAttestation != "")
                sb.AddStrategy(new Strategy(SupplierStrategyName.IsCCC, "%" + cccAttestation + "%"));
            if (isoAttestation != "")
                sb.AddStrategy(new Strategy(SupplierStrategyName.IsISO, "%" + isoAttestation + "%"));
            if (QualityLevel != "")
                sb.AddStrategy(new Strategy(SupplierStrategyName.QualityGrade, "%" + QualityLevel + "%"));
            if (IsAuditted != "")
                sb.AddStrategy(new Strategy(SupplierStrategyName.Status, IsAuditted));
			ArrayList arA = new ArrayList();
			arA.Add("140101");
			arA.Add(user.UserCode);
			arA.Add(user.BuildStationCodes());
			sb.AddStrategy( new Strategy( SupplierStrategyName.AccessRange,arA));


			//����
			string sortsql = BLL.GridSort.GetSortSQL(ViewState);

			string sql = sb.BuildMainQueryString();

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
