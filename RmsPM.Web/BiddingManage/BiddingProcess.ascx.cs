namespace RmsPM.Web.BiddingManage
{
	using System;
	using System.Data;
	using System.Drawing;
    using System.Web;
    using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

    /// <summary>
    /// 动态新增列
    /// </summary>
    public class CTemplateColumn : ITemplate
    {

        private string colname;

        public CTemplateColumn(string cname)
        {

            colname = cname;

        }

        //为了使用接口必须实现的方法

        public void InstantiateIn(Control container)
        {

            LiteralControl l = new LiteralControl();

            l.DataBinding += new EventHandler(this.OnDataBinding);

            container.Controls.Add(l);

        }

        public void OnDataBinding(object sender, EventArgs e)
        {

            LiteralControl l = (LiteralControl)sender;

            DataGridItem container = (DataGridItem)l.NamingContainer;

            l.Text = ((DataRowView)container.DataItem)[colname].ToString();

        }

    }



	/// <summary>
	///		BiddingProcess 的摘要说明。
	/// </summary>
	public partial class BiddingProcess : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
		}
		public void InitControl(string BiddingCode)
		{
            DataTable dt = BLL.Bidding.BiddingProcess(BiddingCode);
            this.dgList.DataSource = dt;
            this.dgList.AutoGenerateColumns = false;

            for (int i = 1; i < dt.Columns.Count - 7; i++)
            {
                AddTemplateColumn("第" + i + "次报价/元", "第" + i + "次报价/元", this.dgList, 5 + i);
            }

            this.dgList.DataBind();
		}

        private void AddTemplateColumn(string HeaderText, string DataSource,DataGrid dg,Int32 Index)
        {
            TemplateColumn tc = new TemplateColumn();
            tc.HeaderText = HeaderText;
            tc.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            tc.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            tc.ItemTemplate = new CTemplateColumn(DataSource);
            dg.Columns.AddAt(Index, tc);
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

		}
		#endregion
	}
}
