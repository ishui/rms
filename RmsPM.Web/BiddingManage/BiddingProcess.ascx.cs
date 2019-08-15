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
    /// ��̬������
    /// </summary>
    public class CTemplateColumn : ITemplate
    {

        private string colname;

        public CTemplateColumn(string cname)
        {

            colname = cname;

        }

        //Ϊ��ʹ�ýӿڱ���ʵ�ֵķ���

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
	///		BiddingProcess ��ժҪ˵����
	/// </summary>
	public partial class BiddingProcess : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
		}
		public void InitControl(string BiddingCode)
		{
            DataTable dt = BLL.Bidding.BiddingProcess(BiddingCode);
            this.dgList.DataSource = dt;
            this.dgList.AutoGenerateColumns = false;

            for (int i = 1; i < dt.Columns.Count - 7; i++)
            {
                AddTemplateColumn("��" + i + "�α���/Ԫ", "��" + i + "�α���/Ԫ", this.dgList, 5 + i);
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
		///		�����֧������ķ��� - ��Ҫʹ�ô���༭��
		///		�޸Ĵ˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}
