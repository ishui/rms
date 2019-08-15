using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rms.ORMap;
using Rms.Web;
using RmsPM.DAL;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;

namespace RmsPM.Web.SelectBox
{
	/// <summary>
	/// SelectContracts ��ժҪ˵����
	/// </summary>
	public partial class SelectContracts : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if (!this.IsPostBack)
			{
				IniPage();	
				BuildSearchString();
				LoadDataGrid();
			}		
		}

		private void IniPage()
		{
			string projectCode = Request["ProjectCode"]+"";
			string status = Request["Status"] + "";
			BLL.PageFacade.SetListGroupSelectedValues(this.cblStatus,status);
			BLL.PageFacade.LoadUnitSelect(this.sltUnit,"",projectCode);

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
		private void LoadDataGrid()
		{
			
			try
			{
				string sql = (string)this.ViewState["SearchString"];
				QueryAgent QA = new QueryAgent();
				EntityData ds = QA.FillEntityData("Contract",sql);
				QA.Dispose();

				this.dgContractList.DataSource =new DataView(ds.CurrentTable);
				this.dgContractList.DataBind();
				this.GridPagination1.RowsCount = ds.CurrentTable.Rows.Count.ToString();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"������Ա�б�ʧ��");
			}
		}

		private void BuildSearchString()
		{
			string projectCode=Request["ProjectCode"] + "";
			RmsPM.DAL.QueryStrategy.ContractStrategyBuilder CSB=new RmsPM.DAL.QueryStrategy.ContractStrategyBuilder();
			string status = BLL.PageFacade.GetListGroupSelectedValues(this.cblStatus);
			if ( status != "" )
				CSB.AddStrategy( new Strategy( DAL.QueryStrategy.ContractStrategyName.Status,status));
			if(projectCode!="")
			CSB.AddStrategy( new Strategy( DAL.QueryStrategy.ContractStrategyName.ProjectCode,projectCode));


			if ( this.txtContractName.Value.Trim() != "")
				CSB.AddStrategy( new Strategy( DAL.QueryStrategy.ContractStrategyName.ContractName, "%" + this.txtContractName.Value.Trim() +"%" ));

			if ( this.txtContractID.Value.Trim() != "")
				CSB.AddStrategy( new Strategy( DAL.QueryStrategy.ContractStrategyName.ContractID, "%" + this.txtContractID.Value.Trim() + "%" ));

			ArrayList arA = new ArrayList();
			arA.Add("050101");
			arA.Add(user.UserCode);
			arA.Add(user.BuildStationCodes());
			CSB.AddStrategy( new Strategy( DAL.QueryStrategy.ContractStrategyName.AccessRange,arA));

			if ( this.sltUnit.Value != "" )
				CSB.AddStrategy( new Strategy( DAL.QueryStrategy.ContractStrategyName.UnitCode,this.sltUnit.Value));

			if ( this.txtSupplierCode.Value != "" )
				CSB.AddStrategy( new Strategy( DAL.QueryStrategy.ContractStrategyName.SupplierCode, this.txtSupplierCode.Value ));

			if (  this.txtTypeCode.Value != "" )
				CSB.AddStrategy( new Strategy( DAL.QueryStrategy.ContractStrategyName.Type,this.txtTypeCode.Value));


			CSB.AddOrder( "ContractDate" ,false);
			string sql = CSB.BuildMainQueryString();
			this.ViewState.Add("SearchString",sql);
		}


		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			BuildSearchString();
			LoadDataGrid();
		}


		protected void SaveToolsButton_ServerClick(object sender, System.EventArgs e)
		{
			GetDataGridValue();
		}
		private void GetDataGridValue()
		{
			bool flag=false;
			foreach(DataGridItem oItem in this.dgContractList.Items)
			{
				CheckBox rb = (CheckBox)oItem.Cells[7].FindControl("CheckBox1");
				if(rb.Checked==true)
				{
					Response.Write(JavaScript.ScriptStart);
					//Response.Write("window.opener.SelectContract('" + Code + "','"+Name+"');");
					Response.Write("window.opener.GetReturnValue('" +oItem.Cells[1].Text + "','"+oItem.Cells[0].Text+"','"+oItem.Cells[6].Text+"','"+Request["id"]+"');");
					Response.Write("window.close();");
					Response.Write(JavaScript.ScriptEnd);
					flag=true;
					break;
				}
			}
			if(!flag)
			{
				Response.Write(Rms.Web.JavaScript.Alert(true,"����ѡ��һ����Ч��ͬ"));
			}
		}

		private void GridPagination1_PageIndexChange(object sender, System.EventArgs e)
		{
			try 
			{				
				BuildSearchString();
				LoadDataGrid();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		protected void GridPagination1_PageIndexChange_1(object sender, System.EventArgs e)
		{
			BuildSearchString();
			LoadDataGrid();
		}

		private void dgContractList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.AlternatingItem:
				case ListItemType.Item:
					RadioButton rb = (RadioButton)e.Item.FindControl("RadioButton1");
					break;
			}
		}
	}
}
