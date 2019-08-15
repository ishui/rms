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
	/// SelectContracts 的摘要说明。
	/// </summary>
	public partial class SelectContracts : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
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

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
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
				ApplicationLog.WriteLog(this.ToString(),ex,"加载人员列表失败");
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
				Response.Write(Rms.Web.JavaScript.Alert(true,"最少选择一个有效合同"));
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
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
