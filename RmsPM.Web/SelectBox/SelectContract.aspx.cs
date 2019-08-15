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
	/// SelectContract 的摘要说明。
	/// </summary>
	public partial class SelectContract : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputText txtUnitName;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtUnitCode;
		protected System.Web.UI.HtmlControls.HtmlInputText txtUserName;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtUserCode;
		
	
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
			string projectCode= Request["ProjectCode"] + "";
			RmsPM.DAL.QueryStrategy.ContractStrategyBuilder CSB=new RmsPM.DAL.QueryStrategy.ContractStrategyBuilder();
			string status = BLL.PageFacade.GetListGroupSelectedValues(this.cblStatus);
			if ( status != "" )
				CSB.AddStrategy( new Strategy( DAL.QueryStrategy.ContractStrategyName.Status,status));
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
			StringBuilder strBuilder = new StringBuilder();
			StringBuilder strName = new StringBuilder();
			StringBuilder strID = new StringBuilder();

			foreach(DataGridItem oItem in this.dgContractList.Items)
			{
				System.Web.UI.WebControls.CheckBox chkContract = (CheckBox)oItem.FindControl("chkContract");
				if (chkContract.Checked == true)
				{
					string contractCode = oItem.Cells[0].Text;
					strBuilder.Append(contractCode );
					strBuilder.Append(",");

					string contractName = oItem.Cells[1].Text;
					strName.Append(contractName );
					strName.Append(",");

					string contractID = oItem.Cells[7].Text;
					strID.Append(contractID );
					strID.Append(",");
				}
			}

			string Code = strBuilder.ToString();
			string Name = strName.ToString();
			string ID = strID.ToString();
			if (Code.Length > 0)
			{
				string returnType = Request["Flag"]+"";
				if(returnType=="CodeAndName")
				{
					Code = Code.Substring(0,Code.Length - 1);					
					Name = Name.Substring(0,Name.Length - 1);
					Response.Write(JavaScript.ScriptStart);
					Response.Write("window.opener.SelectContract('" + Code + "','"+Name+"');");
					Response.Write("window.opener.GetReturnValue('" +Name + "','"+Code+"'','"+ID+"','\"\"','\"\"');");
					Response.Write("window.close();");
					Response.Write(JavaScript.ScriptEnd);
				}
				else if(returnType=="")
				{
					//if(Code.Length>1)
					//{
					//	Response.Write(JavaScript.Alert(true,"只能选一个"));
					//	return;
				//	}
					string[] codeList = Code.Split(',');
					string[] nameList = Name.Split(',');
					string[] idList = ID.Split(',');
					//if(codeList.Length>1)
					//{
					//	Response.Write(JavaScript.Alert(true,"只能选一个"));
					//	return;
					//}
					//Code = Code.Substring(0,Code.Length - 1);					
					//Name = Name.Substring(0,Name.Length - 1);					
					Response.Write(JavaScript.ScriptStart);
					//Response.Write("window.opener.SelectContract('" + Code + "','"+Name+"');");
					Response.Write("window.opener.GetReturnValue('" +nameList[0] + "','"+codeList[0]+"','"+idList[0]+"','\"\"','\"\"');");
					Response.Write("window.close();");
					Response.Write(JavaScript.ScriptEnd);
				}
				else
				{
					Code = Code.Substring(0,Code.Length - 1);
					Response.Write(JavaScript.ScriptStart);
					Response.Write("window.opener.SelectContract('" + Code + "');");
					Response.Write("window.close();");
					Response.Write(JavaScript.ScriptEnd);
				}
			}
			else
			{
				Response.Write(JavaScript.ScriptStart);
				Response.Write("window.close();");
				Response.Write(JavaScript.ScriptEnd);
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


	}
}

