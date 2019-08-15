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

using Rms.ORMap;
using RmsPM.DAL;
using RmsPM.DAL.QueryStrategy;
using RmsPM.DAL.EntityDAO;
using Rms.Web;


namespace RmsPM.Web.Document
{
	/// <summary>
	/// DocumentList 的摘要说明。
	/// </summary>
	public partial class DocumentList : PageBase
	{
		protected System.Web.UI.WebControls.Image Image1;
		protected System.Web.UI.HtmlControls.HtmlTableCell trAddDocument;
		protected System.Web.UI.HtmlControls.HtmlImage ImgSearch;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadDataGrid();
			}
		}

		private void IniPage()
		{
			try 
			{
				this.txtAct.Value = Request.QueryString["action"];
                this.txtStatus.Value = Request.QueryString["Status"];

				switch (this.txtAct.Value.ToLower()) 
				{
					case "view":
						//					this.lbTitle.Text = "文档查询";
						SetReadOnly();
						break;
				}

                string status = Request["Status"] + "" ;
				if ( status.IndexOf("1") >= 0 )
					this.chkStatus1.Checked = true;
				if ( status.IndexOf("0") >= 0 )
					this.chkStatus0.Checked = true;

                //权限
                this.btnAdd.Visible = base.user.HasRight("100102");

                //状态是否只读
                string StatusReadonly = Request.QueryString["StatusReadonly"] + "";
                if (StatusReadonly == "1")
                {
                    this.chkStatus0.Disabled = true;
                    this.chkStatus1.Disabled = true;
                    //                    if (!this.chkStatus0.Checked) this.chkStatus0.Visible = false;
  //                  if (!this.chkStatus1.Checked) this.chkStatus1.Visible = false;
                }

                //只显示某些类型的文档 06.7.5
                string GroupCode = Request.QueryString["GroupCode"] + "";
                if (GroupCode != "")
                {
                    int GroupCount = 0;
                    string GroupName = "";
                    string GroupNameHint = "";
                    string GroupFullID = "";
                    string[] arrGroupCode = GroupCode.Split(","[0]);
                    foreach (string item in arrGroupCode)
                    {
                        GroupCount++;

                        if (GroupNameHint != "") GroupNameHint += "\n";
                        if (GroupFullID != "") GroupFullID += ",";

                        string FullName = BLL.SystemGroupRule.GetSystemGroupFullName(item);
                        GroupFullID += BLL.SystemGroupRule.GetSystemGroupFullID(item);

                        GroupNameHint += FullName;

                        //页面上只显示前两个类别名称
                        if (GroupCount <= 2)
                        {
                            if (GroupName != "") GroupName += ",&nbsp;&nbsp;";
                            GroupName += FullName;
                        }
                        else if (GroupCount == 3)
                        {
                            GroupName += ",&nbsp;&nbsp;...";
                        }
                    }

                    this.lblDocumentTypeName.Text = GroupName;
                    this.lblDocumentTypeName.Attributes["title"] = GroupNameHint;
                    this.txtGroupFullID.Value = GroupFullID;
                }
                else
                {
                    this.lblDocumentTypeName.Text = "所有文档";
                }
            }
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}

		}

		private void SetReadOnly() 
		{
			this.trAddDocument.Visible = false;
			this.dgList.Columns[this.dgList.Columns.Count-1].Visible = false;
		}

		private void LoadDataGrid()
		{
			try
			{
//				string DocumentTypeCode = Request.QueryString["DocumentTypeCode"]+"";

				DAL.QueryStrategy.DocumentStrategyBuilder sb = new DocumentStrategyBuilder();

//				if (DocumentTypeCode != "")
//				{
//					sb.AddStrategy( new Strategy( DocumentStrategyName.DocumentTypeCodeFull ,DocumentTypeCode) );
//					this.lblDocumentTypeName.Text = BLL.DocumentRule.Instance().GetDocumentTypeName(DocumentTypeCode);
//				}
//				else 
//				{
//					this.lblDocumentTypeName.Text = "所有文档";
//				}

				if (this.txtGroupFullID.Value != "")
				{
                    sb.AddStrategy(new Strategy(DocumentStrategyName.GroupFullIDs, this.txtGroupFullID.Value));
                }

                string title = this.txtSearchTitle.Value.Trim();
                if (title.Length > 0)
                    sb.AddStrategy(new Strategy(DAL.QueryStrategy.DocumentStrategyName.Title, title));

                //状态
                ArrayList arStatus = new ArrayList();
                if (this.chkStatus0.Checked)
                    arStatus.Add("0");
                if (this.chkStatus1.Checked)
                    arStatus.Add("1");
                string status = BLL.ConvertRule.GetArrayLinkString(arStatus);
                if (status != "")
                    sb.AddStrategy(new Strategy(DocumentStrategyName.Status, status));

                if (this.txtAdvSearch.Value != "none")
                {
                    string CreateDate_begin = this.dtCreateDate_begin.Value.Trim();
                    string CreateDate_end = this.dtCreateDate_end.Value.Trim();
                    string ModifyDate_begin = this.dtModifyDate_begin.Value.Trim();
                    string ModifyDate_end = this.dtModifyDate_end.Value.Trim();
                    string DocumentID = this.txtSearchDocumentID.Value.Trim();
                    string author = this.txtSearchAuthor.Value.Trim();
                    string CreatePerson = this.ucCreatePerson.Value.Trim();
                    string ModifyPerson = this.ucModifyPerson.Value.Trim();

                    string FixedType = this.sltFixedType.Value.Trim();
                    string Code = this.txtCode.Value.Trim();

                    ArrayList ar = new ArrayList();
                    ar.Add(CreateDate_begin);
                    ar.Add(CreateDate_end);

                    ArrayList ar2 = new ArrayList();
                    ar2.Add(ModifyDate_begin);
                    ar2.Add(ModifyDate_end);

                    sb.AddStrategy(new Strategy(DAL.QueryStrategy.DocumentStrategyName.CreateDateRange, ar));
                    sb.AddStrategy(new Strategy(DAL.QueryStrategy.DocumentStrategyName.ModifyDateRange, ar2));

                    if (DocumentID.Length > 0)
                        sb.AddStrategy(new Strategy(DAL.QueryStrategy.DocumentStrategyName.DocumentID, "%" + DocumentID + "%"));
                    if (author.Length > 0)
                        sb.AddStrategy(new Strategy(DAL.QueryStrategy.DocumentStrategyName.Author, author));
                    if (CreatePerson.Length > 0)
                        sb.AddStrategy(new Strategy(DAL.QueryStrategy.DocumentStrategyName.CreatePerson, CreatePerson));
                    if (ModifyPerson.Length > 0)
                        sb.AddStrategy(new Strategy(DAL.QueryStrategy.DocumentStrategyName.ModifyPerson, ModifyPerson));

                    if (Code.Length > 0)
                    {
                        if (FixedType.Length > 0)
                        {
                            ArrayList arTmp = new ArrayList();
                            arTmp.Add(FixedType);
                            arTmp.Add(Code);
                            sb.AddStrategy(new Strategy(DAL.QueryStrategy.DocumentStrategyName.RelationKey, arTmp));
                        }
                        else
                        {
                            //					ssb.AddStrategy( new Strategy(DAL.QueryStrategy.DocumentStrategyName.Code, Code));
                        }
                    }
                }

				//权限
				ArrayList arA = new ArrayList();
				arA.Add("100101");
				arA.Add(user.UserCode);
				arA.Add(user.BuildStationCodes());
				sb.AddStrategy( new Strategy( DAL.QueryStrategy.DocumentStrategyName.AccessRange,arA));

				//排序
				string sortsql = BLL.GridSort.GetSortSQL(ViewState);
				if (sortsql == "")
				{
					//缺省排序
					sb.AddOrder("CreateDate",false);
				}

				string sql = sb.BuildQueryViewString();

				if (sortsql != "")
				{
					//点列标题排序
					sql = sql + " order by " + sortsql;
				}

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData("Document",sql);
				qa.Dispose();

				this.dgList.DataSource = entity.CurrentTable;
				this.dgList.DataBind();

                this.GridPagination1.RowsCount = entity.CurrentTable.Rows.Count.ToString();
                
                entity.Dispose();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示列表出错：" + ex.Message));
			}
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
			this.dgList.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemCreated);
			this.dgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgList_PageIndexChanged);
			this.dgList.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgList_SortCommand);
			this.dgList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemDataBound);

		}
		#endregion

		private void dgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.dgList.CurrentPageIndex = e.NewPageIndex;
			LoadDataGrid();
		}

		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
//			System.Web.UI.WebControls.Image img;

			if ((e.Item.ItemType == ListItemType.Item)
				|| (e.Item.ItemType == ListItemType.EditItem)
				|| (e.Item.ItemType == ListItemType.AlternatingItem)
				)
			{
				string DocumentCode = this.dgList.DataKeys[e.Item.ItemIndex].ToString();

//				//附件
//				string AttachCount = DataBinder.Eval(e.Item.DataItem, "AttachCount").ToString();
//				if (AttachCount == "0") 
//				{
//					img = (System.Web.UI.WebControls.Image)e.Item.FindControl("img1");
//					img.Visible = false;
//				}
//
//				HtmlAnchor an = (HtmlAnchor)e.Item.FindControl("A1");
//				an.Attributes.Add("keyvalue", DocumentCode);
				
			}
		}

		/*
		public void ShowAttach(Object sender, EventArgs e)
		{
			string s = JavaScript.ScriptStart + "\n";
			s = s + "var v_showmenu = true;" + "\n";

			string DocumentCode = ((HtmlAnchor)sender).Attributes["keyvalue"];

			EntityData entity = DAL.EntityDAO.DocumentDAO.GetAttachmentByDocumentCode(DocumentCode);
			int i = -1;
			foreach(DataRow dr in entity.CurrentTable.Rows) 
			{
				i++;
				s = s + "Items[" + i.ToString() + "] = new Array(2);";
				s = s + "Items[" + i.ToString() + "][0] = '" + dr["title"].ToString() + "';";
				s = s + "Items[" + i.ToString() + "][1] = '';";
				s = s + "Items[" + i.ToString() + "][2] = 'ViewAttach(" + dr["AttachmentCode"].ToString() + ");';";

			}
			entity.Dispose();

			s = s + JavaScript.ScriptEnd + "\n";
			Page.RegisterStartupScript("ShowMenu", s);
		}
		*/

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			this.dgList.CurrentPageIndex = 0;
			LoadDataGrid();
		}

		private void dgList_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			BLL.GridSort.SortCommand((DataGrid)source, ViewState, source, e);
			((DataGrid)source).CurrentPageIndex = 0;
			LoadDataGrid();
		}

		private void dgList_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			BLL.GridSort.ItemCreate((DataGrid)sender, ViewState, sender, e);
		}

        protected void GridPagination1_PageIndexChange(object sender, System.EventArgs e)
        {
            LoadDataGrid();
        }
        protected void dgList_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            EntityData entity = null;
            try
            {
                entity = new EntityData("Document");
                string documentcode = dgList.DataKeys[e.Item.ItemIndex].ToString();
                entity = DocumentDAO.GetDocumentByCode(documentcode);
                DAL.EntityDAO.DocumentDAO.DeleteDocument(entity);

                Response.Write(Rms.Web.JavaScript.ScriptStart);
                Response.Write("window.navigate('DocumentList.aspx');");
                Response.Write(Rms.Web.JavaScript.ScriptEnd);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
        }
}
}
