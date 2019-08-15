namespace RmsPM.Web.PBS
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Rms.ORMap;

	/// <summary>
	///		UCBuildingFunctionList 的摘要说明。
	/// </summary>
	public partial class UCBuildingFunctionList : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
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
			this.dgList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemDataBound);

		}
		#endregion


		#region --- 私有属性 -------------------------------------------------------------

		/// <summary>
		/// 楼栋编号
		/// </summary>
		private string _BuildingCode = "";

		#endregion -------------------------------------------------------------

		#region --- 私有方法 -------------------------------------------------------------

		/// <summary>
		/// 楼栋功能列表合计
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			try
			{
				if ( ListItemType.Footer==e.Item.ItemType )
				{
					((Label)e.Item.FindControl("ftTotalFunctionNum")).Text = BLL.ConvertRule.ToString(ViewState["ftTotalFunctionNum"]);
					((Label)e.Item.FindControl("ftTotalFunctionArea")).Text = BLL.ConvertRule.ToString(ViewState["ftTotalFunctionArea"]);
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		#endregion -------------------------------------------------------------

		#region --- 公共属性 -------------------------------------------------------------

		/// <summary>
		/// 楼栋编号
		/// </summary>
		public string BuildingCode
		{
			get{return this._BuildingCode;}
			set{this._BuildingCode=value;}
		}

		#endregion -------------------------------------------------------------

		#region --- 公共方法 -------------------------------------------------------------

		/// <summary>
		/// 控件初始化
		/// </summary>
		public void IniControl()
		{
			try
			{
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


		/// <summary>
		/// 装载控件数据
		/// </summary>
		public void LoadDataList()
		{
			try
			{
				EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingFunctionByBuildingCode(this._BuildingCode);

				#region --- 合计 --------------------------------------------------------------

				int[] arrSumInt = BLL.MathRule.SumIntColumn( entity.CurrentTable, new string[] {"FunctionNum"});
				decimal[] arrSumDec = BLL.MathRule.SumColumn( entity.CurrentTable, new string[] {"FunctionArea"});

				ViewState["ftTotalFunctionNum"] = arrSumInt[0];
				ViewState["ftTotalFunctionArea"] = arrSumDec[0];

				#endregion --------------------------------------------------------------

				this.dgList.DataSource = entity.CurrentTable;
				this.dgList.DataBind();
				entity.Dispose();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		#endregion -------------------------------------------------------------


	}
}
