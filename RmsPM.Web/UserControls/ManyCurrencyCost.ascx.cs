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
	///		ManyCurrencyCost 的摘要说明。
	/// </summary>
	public partial class ManyCurrencyCost : System.Web.UI.UserControl
	{
		protected  StandardEntityDAO _dao = null;

		protected void Page_Load(object sender, System.EventArgs e)
		{
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
		///		设计器支持所需的方法 - 不要使用代码编辑器
		///		修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.DataGrid1.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_DeleteCommand);
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);

		}
		private void OnInitEvent()
		{
			this.btnSave.ServerClick += new System.EventHandler(this.btnSave_ServerClick);
			this.btnAdd.ServerClick += new System.EventHandler(this.btnAdd_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion

		/// <summary>
		/// 是否处于可编辑状态  true 编辑状态 false 显示状态
		/// </summary>
		public bool IsEditMode
		{
			get
			{
				if(ViewState["IsEditMode"]==null)
					return true;
				return (bool)ViewState["IsEditMode"];
			}

			set
			{
				ViewState["IsEditMode"] = value;
			}
		}
        public bool IsAllowAdd
        {
            get
            {

                if (ViewState["IsAllowAdd"] == null)
                    return true;
                return (bool)ViewState["IsAllowAdd"];
            }
            set
            {
                ViewState["IsAllowAdd"] = value;
            }
        }


		/// <summary>
		/// 总计价格字符串形式
		/// </summary>
		public string TotalMoney
		{
			get
			{
                if (divTotalMoney.InnerHtml == "")
                    return "0";
                return divTotalMoney.InnerHtml;
			}
		}
		/// <summary>
		/// 数据表
		/// </summary>
		public DataTable CashTable
		{
			get
			{
				if(ViewState["CashTable"]==null)
					return null;
				return (DataTable)ViewState["CashTable"];
			}
			set
			{
				ViewState["CashTable"] = value;
			}
		}
        /// <summary>
        /// 代码
        /// </summary>
        public string CashMessageCode
        {
            get
            {
                if (this.ViewState["CashMessageCode"] == null)
                    return "";
                return this.ViewState["CashMessageCode"].ToString();
            }
            set
            {
                this.ViewState["CashMessageCode"] = value;
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
		/// <summary>
		/// 初始化内存表格
		/// </summary>
		private void DefaultTable()
		{
			if(this.CashTable!=null)
			{
				if(CashTable.Rows.Count>0)
				{
					return;
				}
			}
			NewRows();
		}
		/// <summary>
		/// 读取信息
		/// </summary>
		/// <returns></returns>
		private void GetCostDetail()
		{
			BLL.Cash_Message cashMessage = new BLL.Cash_Message();
            cashMessage.CashMessageTypeCode = this.CashMessageCode;
            cashMessage.CashMessageType = "回标";
            DataTable dt = cashMessage.GetCash_Messages();
			BLL.Cash_Detail cd = new BLL.Cash_Detail();
            if (dt.Rows.Count != 0)
            {
                cd.Cash_MessageCode = dt.Rows[dt.Rows.Count-1]["CashMessageCode"].ToString();
                this.divTotalMoney.InnerHtml = BLL.MathRule.GetDecimalShowString(dt.Rows[dt.Rows.Count - 1]["CashMessageCashTotal"]);
                this.CashTable = cd.GetCash_Details();
            }
            else
            {
                this.divTotalMoney.InnerHtml = "0";
                this.CashTable = cd.GetCash_Details();
                this.CashTable.Clear();
            }
		}
		/// <summary>
		/// 保存数据
		/// </summary>
		private void SaveToDB()
		{
            BLL.Cash_Message cm = new RmsPM.BLL.Cash_Message();
            cm.CashMessageTypeCode = this.CashMessageCode;
            cm.CashMessageType = "回标";
            cm.CashMessageTemporaryMoney = "0";
            cm.CashMessageCashTotal = this.divTotalMoney.InnerHtml;
            cm.Cash_MessageSubmit();

			EntityData entity = new EntityData("Cash_Detail");
			DAL.QueryStrategy.Cash_MessageStrategyBuilder sb = new RmsPM.DAL.QueryStrategy.Cash_MessageStrategyBuilder();
			sb.AddStrategy( new Strategy( DAL.QueryStrategy.Cash_DetailStrategyName.Cash_MessageCode,this.CashMessageCode));
			string sql = sb.BuildMainQueryString();
			dao.FillEntity(sql, entity);
			entity.Tables.Clear();
            for (int i = 0; i < CashTable.Rows.Count; i++)
            {
                CashTable.Rows[i]["Cash_MessageCode"] = cm.CashMessageCode;
            }
			entity.Tables.Add(CashTable);
			entity.SetCurrentTable("Cash_Detail");
			dao.SubmitEntity(entity);
		}
		/// <summary>
		/// 增加新行
		/// </summary>
		private void NewRows()
		{
			DataRow dr = this.CashTable.NewRow();
			dr["CashDetialCode"]=DAL.EntityDAO.SystemManageDAO.GetNewSysCode("CashDetialCode");
			dr["RMB"]=0;
			this.CashTable.Rows.Add(dr);
		}
		/// <summary>
		/// 把数据保存到内存表中
		/// </summary>
        private void SaveToMemory()
        {
            if (!IsEditMode)
            {
                return;
            }
            int counts = DataGrid1.Items.Count;
            Decimal sum = 0;

            for (int i = 0; i < counts; i++)
            {
                UserControls.ExchangeRateControl dt_ExchangeRateControl = (UserControls.ExchangeRateControl)DataGrid1.Items[i].FindControl("ExchangeRateControl1");
                CashTable.Rows[i]["MoneyType"] = dt_ExchangeRateControl.MoneyType;
                CashTable.Rows[i]["ExchangeRate"] = dt_ExchangeRateControl.ExchangeRate;
                CashTable.Rows[i]["MoneyTypeID"] = dt_ExchangeRateControl.MoneyTypeValue;
                CashTable.Rows[i]["Cash"] = dt_ExchangeRateControl.Cash;
                CashTable.Rows[i]["RMB"] = dt_ExchangeRateControl.RMB;
                sum += Convert.ToDecimal(CashTable.Rows[i]["RMB"]);
            }
            this.divTotalMoney.InnerHtml = BLL.MathRule.GetDecimalShowString(sum.ToString());

        }

		private void BindDataGrid()
		{
			this.DataGrid1.DataSource=this.CashTable;
			this.DataGrid1.DataBind();
        }

		public void LoadData()
		{
            this.DataGrid1.Columns[5].Visible = IsEditMode;
            this.btnSave.Visible = false;
            this.btnAdd.Visible = false;
            this.btnClose.Visible = false;
            //this.btnQuery.Visible = this.IsEditMode;
            this.CostDetail.Visible = false;
			GetCostDetail();
			DefaultTable();
			BindDataGrid();
		}
        public void SumitData()
		{
			SaveToMemory();
			SaveToDB();
		}
		
		
		

		public void btnSave_ServerClick(object sender, System.EventArgs e)
		{
            try
            {
                SumitData();
                this.btnSave.Visible = false;
                this.btnAdd.Visible = false;
                this.btnClose.Visible = false;
                this.btnQuery.Visible = true;
                this.CostDetail.Visible = false;
            }
            catch (Exception ex)
            {
                Response.Write(Rms.Web.JavaScript.Alert(true,ex.Message));
            }
		}

		protected void btnAdd_ServerClick(object sender, System.EventArgs e)
		{

            try
            {
                SaveToMemory();
                NewRows();
                BindDataGrid();
            }
            catch (Exception ex)
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, ex.Message));
            }
            
            
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
					erc.MoneyType=myrow.Row["MoneyType"].ToString();
					erc.CashText=myrow.Row["Cash"].ToString();
                    //erc.Cash = decimal.Parse((myrow.Row["Cash"].ToString() == "")?"0":myrow.Row["Cash"].ToString());
					erc.RMBText=myrow.Row["RMB"].ToString();
					erc.ExchangeRateText = myrow.Row["ExchangeRate"].ToString();
					erc.EditMode=this.IsEditMode;
                    //erc.EditMode = true;
					erc.ValueChange="SetTotalMoney(this.id,'"+this.ClientID+"')";
					erc.BindControl();
					break;
				default:
					break;
			}
		}


        private void DataGrid1_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            SaveToMemory();
            int key = e.Item.ItemIndex; 
            CashTable.Rows.Remove(CashTable.Rows[key]);
            BindDataGrid();

        }

        protected void btnQuery_ServerClick(object sender, EventArgs e)
        {
            if (this.IsEditMode)
            {
                this.btnSave.Visible = true;
                this.btnQuery.Visible = false;
                this.btnClose.Visible = true;
                if (IsAllowAdd)
                {
                    this.btnAdd.Visible = true;
                }
                this.CostDetail.Visible = true;
            }
            else
            {
                this.btnSave.Visible = false;
                this.btnQuery.Visible = false;
                this.btnClose.Visible = true;
                this.btnAdd.Visible = false;
                this.CostDetail.Visible = true;
            }
        }
        protected void btnClose_ServerClick(object sender, EventArgs e)
        {
            this.btnSave.Visible = false;
            this.btnAdd.Visible = false;
            this.btnClose.Visible = false;
            this.btnQuery.Visible = true;
            this.CostDetail.Visible = false;
        }
}
}