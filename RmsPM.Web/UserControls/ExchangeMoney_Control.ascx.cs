namespace RmsPM.Web.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Rms.ORMap;

	/// <summary>
	///		ExchangeMoney_Control 的摘要说明。
	/// </summary>
	public partial class ExchangeMoney_Control : System.Web.UI.UserControl
	{
		protected  StandardEntityDAO _dao=null;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{
				DefaultSet();
			}
		}
		#region 共公属性
		public bool EditMode
		{
			get
			{
				if(ViewState["EditMode"]==null)
				{
					return true;
				}
				else
				{
					return (bool)ViewState["EditMode"];
				}
			}

			set
			{
				ViewState["EditMode"] = value;
			}
		}
		public string Money
		{
			get
			{
				return Webnumericedit1.Value.ToString().Trim();
			}
			set
			{
				Webnumericedit1.Value=value;
				Lb_RMB.Text=value;
			}
		}
		/// <summary>
		/// 事务对象
		/// </summary>
		public StandardEntityDAO dao
		{
			get
			{
				if(_dao==null)
				{
					_dao=new StandardEntityDAO("BiddingReturnCost");
				}
				return this._dao;
			}
			set
			{
				_dao = value;
			}
		}
		#endregion

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
		///		设计器支持所需的方法 - 不要使用代码编辑器
		///		修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.DataGrid1.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_DeleteCommand);
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);

		}
		#endregion
		/// <summary>
		/// 表格信息
		/// </summary>
		public DataTable CashTable
		{
			get
			{
				if(ViewState["CashTable"]!=null)
					return (DataTable)ViewState["CashTable"];
				else
					return null;
			}
			set
			{
				ViewState["CashTable"]=value;

			}
		}
		public string BiddingReturnCode
		{
			get
			{
				if(ViewState["BiddingReturnCode"]!=null)
					return ViewState["BiddingReturnCode"].ToString();
				else
					return "";
			}
			set
			{
				ViewState["BiddingReturnCode"]=value;

			}
		}
		public void DefaultSet()
		{
			CreateTable();
			SimpleMode();
		}

		private void txtMoney_ValueChange(object sender, Infragistics.WebUI.WebDataInput.ValueChangeEventArgs e)
		{
			//int
			///Decimal
			///
			
			
		}
		#region 状态设置
		/// <summary>
		/// 查看详细信息样式
		/// </summary>
		private void DetailMode()
		{
			if(this.EditMode)
			{
				Webnumericedit1.Visible=true;
				Lb_RMB.Visible=false;
				Bt_AddMoney.Value="添加明细";
				Bt_AddMoney.Visible=false;
				btnSave.Visible=true;
				Bt_Add.Visible=true;
				this.Bt_Close.Visible=true;
				ReturnDetail.Visible=true;
			}
			else
			{
				Webnumericedit1.Visible=true;
				Lb_RMB.Visible=false;
				Bt_AddMoney.Value="查看明细";
				Bt_AddMoney.Visible=false;
				btnSave.Visible=false;
				Bt_Add.Visible=false;
				ReturnDetail.Visible=true;
				this.Bt_Close.Visible=true;
			}
		}
		/// <summary>
		/// 简单样式,只能查看金额
		/// </summary>
		private void SimpleMode()
		{
			if(this.EditMode)
			{
				Webnumericedit1.Visible=true;
				Bt_AddMoney.Value="添加明细";
				Lb_RMB.Visible=false;
				Bt_AddMoney.Visible=true;
				btnSave.Visible=false;
				Bt_Add.Visible=false;
				ReturnDetail.Visible=false;
				this.Bt_Close.Visible=false;
			}
			else
			{
				Webnumericedit1.Visible=false;
				Lb_RMB.Visible=true;
				if(this.CashTable.Rows.Count>0)
				{
					Bt_AddMoney.Value="查看明细";
					Bt_AddMoney.Visible=true;
				}
				else
				{
					Bt_AddMoney.Visible=false;
				}
				btnSave.Visible=false;
				Bt_Add.Visible=false;
				ReturnDetail.Visible=false;
				this.Bt_Close.Visible=false;
			}
		}		
		/// <summary>
		/// 只允许填定RMB
		/// </summary>
		/// <param name="bl"></param>
		private void OnlyForRMB(bool bl)
		{
			lb_MoneyMessage.InnerHtml="";
			this.Webnumericedit1.Enabled=true;			
			this.DataGrid1.Visible=!bl;
			this.btnSave.Visible=!bl;
			this.Bt_Add.Visible=!bl;
			this.Webnumericedit1.Visible=bl;
			this.Bt_AddMoney.Visible=bl;
			ReturnDetail.Visible=!bl;
		}
		#endregion
		private void CreateTable()
		{
			//if(CashTable!=null)
				//return;
			if(this.CashTable!=null)
			{
				if(CashTable.Rows.Count>0)
				{
					return;
				}
			}
			BLL.BiddingReturnCost brcost = new RmsPM.BLL.BiddingReturnCost();
			brcost.BiddingReturnCode = this.BiddingReturnCode;
			DataTable dt = brcost.GetBiddingReturnCosts();
			if(dt.Rows.Count>0)
			{
				this.CashTable = dt;
				return;
			}
			dt = new DataTable();			
			dt.Columns.Add("BiddingReturnCostCode",Type.GetType("System.String"));
			dt.Columns.Add("Cash",Type.GetType("System.Decimal"));
			dt.Columns.Add("MoneyType",Type.GetType("System.String"));
			dt.Columns.Add("MoneyTypeID",Type.GetType("System.String"));			
			dt.Columns.Add("ExchangeRate",Type.GetType("System.String"));
			dt.Columns.Add("Remark",Type.GetType("System.String"));
			//DataRow dr = dt.NewRow();
			//dr["BiddingReturnCostCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("BiddingReturnCostCode");
			//dr["ExchangeRate"] = Convert.ToDecimal("1");
			//dr["Cash"]=Convert.ToDecimal("0");
			//dt.Rows.Add(dr);
			CashTable=dt;
		}
		private void DataGrid1_Bind()
		{
			DataGrid1.DataSource = this.CashTable;
			DataGrid1.DataBind();
		}
		private void AddNewRow()
		{
			DataRow dr = CashTable.NewRow();
			dr["BiddingReturnCostCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("BiddingReturnCostCode");
			dr["Cash"]=Convert.ToDecimal("0");
			dr["ExchangeRate"] = Convert.ToDecimal("1");
			CashTable.Rows.Add(dr);
		}
		/// <summary>
		/// 保存到内存表中
		/// </summary>
		private void SaveCash()
		{
			int count = DataGrid1.Items.Count;
			Decimal total=0;
			for(int i=0;i<count;i++)
			{
				
				UserControls.ExchangeRateControl erc;
				erc = (UserControls.ExchangeRateControl)this.DataGrid1.Items[i].FindControl("ExchangeRateControl1");
				//erc.EditMode=this.EditMode;
				CashTable.Rows[i]["Cash"]=erc.CashText;
				CashTable.Rows[i]["BiddingReturnCostCode"]=this.DataGrid1.DataKeys[i];
				CashTable.Rows[i]["MoneyType"]=erc.MoneyType;
				CashTable.Rows[i]["MoneyTypeID"]=erc.MoneyTypeValue;//((Rms.ControlLb.MoneyList)this.DataGrid1.Items[i].FindControl("MoneyList1")).MoneyTypeID;
				CashTable.Rows[i]["ExchangeRate"]=erc.ExchangeRate;//((Infragistics.WebUI.WebDataInput.WebNumericEdit)this.DataGrid1.Items[i].FindControl("input_ExchangeRate")).Value.ToString();
				total+=Convert.ToDecimal(CashTable.Rows[i]["Cash"])*Convert.ToDecimal(CashTable.Rows[i]["ExchangeRate"]);
				CashTable.Rows[i]["Remark"]=((HtmlTextArea)this.DataGrid1.Items[i].FindControl("TB_Remark")).Value.Trim();
			}
			this.Webnumericedit1.Value=total.ToString();
		}
		private void SaveToDBBase()
		{
			DAL.QueryStrategy.BiddingReturnCostStrategyBuilder sb = new RmsPM.DAL.QueryStrategy.BiddingReturnCostStrategyBuilder();
			sb.AddStrategy( new Strategy( DAL.QueryStrategy.BiddingReturnCostStrategyName.BiddingReturnCode,this.BiddingReturnCode));
			string sql = sb.BuildMainQueryString();
			EntityData entity = new EntityData("BiddingReturnCost");
			dao.FillEntity(sql, entity);
			dao.DeleteAllRow(entity);
			//dao.FillEntity(entity);
			//entity.Tables[0].Rows.Clear();//.Clear();
			DataRow dr;
			int count = this.CashTable.Rows.Count;			
			for(int i=0;i<count;i++)
			{
				dr = entity.GetNewRecord();
				//this.BiddingReturnCode;
				dr["BiddingReturnCode"]=this.BiddingReturnCode;//CashTable.Rows[i]["Cash"];
				dr["Cash"]=CashTable.Rows[i]["Cash"];
				dr["BiddingReturnCostCode"]=this.CashTable.Rows[i]["BiddingReturnCostCode"];
				dr["MoneyType"]=CashTable.Rows[i]["MoneyType"];
				dr["MoneyTypeID"]=CashTable.Rows[i]["MoneyTypeID"];
				dr["ExchangeRate"]=CashTable.Rows[i]["ExchangeRate"];
				dr["Remark"]=CashTable.Rows[i]["Remark"];
				entity.AddNewRecord(dr);
			}
			try
			{
			
				dao.SubmitEntity(entity);
				entity.Dispose();
			}
			catch
			{
				 SaveToDBBase();
			}
		}
		private void DataGrid1_SelectedIndexChanged(object sender, System.EventArgs e)
		{		
		}

		private void ImageButton2_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
		}

		#region 数据操作
		protected void Bt_Add_ServerClick(object sender, System.EventArgs e)
		{
			SaveCash();
			AddNewRow();
			DataGrid1_Bind();
		}

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			SimpleMode();
			SaveCash();
			SaveToDBBase();			
		}

		protected void Bt_AddMoney_ServerClick(object sender, System.EventArgs e)
		{
			if(this.CashTable.Rows.Count<=0)
			{
				AddNewRow();
			}
			DetailMode();
			DataGrid1_Bind();
		}

		private void DataGrid1_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			SaveCash();
			int key =e.Item.ItemIndex; //Convert.ToInt32(DataGrid1.DataKeys[e.Item.ItemIndex]);
			CashTable.Rows.Remove(CashTable.Rows[key]);			
			DataGrid1_Bind();
		}
		private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{			
			switch (e.Item.ItemType)
			{
				case ListItemType.AlternatingItem:
				case ListItemType.Item:
					DataRowView myrow = ((DataRowView)e.Item.DataItem);
					UserControls.ExchangeRateControl erc;
					erc = (UserControls.ExchangeRateControl)e.Item.FindControl("ExchangeRateControl1");
					//if(myrow.Row["Cash"].ToString()!=""||myrow.Row["Cash"].ToString()!="0")
					//{
						erc.MoneyType=myrow.Row["MoneyType"].ToString();
						erc.CashText=myrow.Row["Cash"].ToString();
						erc.RMB=(Convert.ToDecimal(myrow.Row["Cash"]))*Convert.ToDecimal(myrow.Row["ExchangeRate"]);
						erc.ExchangeRateText = myrow.Row["ExchangeRate"].ToString();
						erc.ValueChange="TotalMoney('"+this.ClientID+"')";
					//}
					erc.IsShowTitle=false;
					erc.IsAllowModify=this.EditMode;
					erc.EditMode=this.EditMode;
					erc.BindControl();
					if(!this.EditMode)
					{
						e.Item.Cells[5].Visible=false;						
						Label lb = (Label)e.Item.FindControl("Lb_Remark");
						lb.Visible=true;
						((HtmlTextArea)e.Item.FindControl("TB_Remark")).Visible=false;


					}
					break;
				default:
					break;
			}
		}
		#endregion

		private void Button1_ServerClick(object sender, System.EventArgs e)
		{
		
		}

		protected void Bt_Close_ServerClick(object sender, System.EventArgs e)
		{
			SimpleMode();
		}

	}
}
