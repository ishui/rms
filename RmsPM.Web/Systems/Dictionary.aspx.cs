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

namespace RmsPM.Web.Systems
{
	/// <summary>
	/// Dictionary 的摘要说明。
	/// </summary>
	public partial class Dictionary : PageBase
	{
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator0;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdBtnInput;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack )
			{
				IniPage();
			}
		}

		private void IniPage()
		{
			string projectCode=""+Request.QueryString["ProjectCode"];

			if ( projectCode == "" )
			{
				this.tableInput.Visible= false;
			}

			try
			{
				EntityData entity = RmsPM.DAL.EntityDAO.SystemManageDAO.GetDictionaryNameByProjectCode(projectCode);
				this.dgName.DataSource=entity.CurrentTable;
				this.dgName.DataBind();
				entity.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示出错"));
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
			this.btnAddNew.Click += new System.Web.UI.ImageClickEventHandler(this.btnAddNew_Click);
			this.btnSave.Click += new System.Web.UI.ImageClickEventHandler(this.btnSave_Click);
			this.btnDelete.Click += new System.Web.UI.ImageClickEventHandler(this.btnDelete_Click);
			this.btnUp.Click += new System.Web.UI.ImageClickEventHandler(this.btnUp_Click);
			this.btnDown.Click += new System.Web.UI.ImageClickEventHandler(this.btnDown_Click);
			this.dgItem.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgItem_ItemCommand);

		}
		#endregion

		private void dgItem_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string code = e.Item.Cells[0].Text;
			switch ( e.CommandName )
			{
//				case "Delete" :
//					DeleteDictionaryItem( code );
//					break;
				case "Modify" :
					this.dgItem.SelectedIndex = e.Item.ItemIndex;
					PreModidyDictionaryItem( code );
					break;
//				case "Up" :
//					UpItem(itemIndex);
//					break;
//				case "Down" :
//					DownItem(itemIndex);
//					break;

			}
		}



		private void PreModidyDictionaryItem ( string dictItemCode )
		{
			try
			{
				EntityData entity = RmsPM.DAL.EntityDAO.SystemManageDAO.GetDictionaryItemByCode(dictItemCode);
				if ( entity.HasRecord())
				{
					ViewState["DictionaryItemCode"] = dictItemCode;
					this.txtName.Text = entity.GetString("Name");

					this.btnSave.Visible = true;
					this.btnUp.Visible = true;
					this.btnDown.Visible = true;
					this.btnDelete.Visible = true;

					this.btnAddNew.Visible = false;
				}
				entity.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "移动出错"));
			}
		}

		private void DeleteDictionaryItem ( string dictionaryItemCode )
		{
			try
			{
				EntityData entity = RmsPM.DAL.EntityDAO.SystemManageDAO.GetDictionaryItemByCode(dictionaryItemCode);
				RmsPM.DAL.EntityDAO.SystemManageDAO.DeleteDictionaryItem(entity);
				entity.Dispose();
				LoadItemData(ViewState["DictionaryNameCode"].ToString(),-1);
				Clear();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除出错"));
			}
		}


		protected void dgName_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string dictNameCode = this.dgName.Items[this.dgName.SelectedIndex].Cells[0].Text;
			ViewState["DictionaryNameCode"] = dictNameCode;
			LoadItemData(dictNameCode,-1);
			Clear();
		}

		private void LoadItemData( string dictNameCode, int newIndex)
		{
			try
			{
				EntityData entity = RmsPM.DAL.EntityDAO.SystemManageDAO.GetStandard_DictionaryNameByCode(dictNameCode);
				entity.SetCurrentTable("DictionaryItem");
				this.dgItem.DataSource=entity.CurrentTable;
				this.dgItem.DataBind();

				if ( newIndex>=0 && newIndex <= entity.CurrentTable.Rows.Count-1)
					this.dgItem.SelectedIndex = newIndex;

				entity.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示出错"));
			}

		}

		private void Clear()
		{
			this.btnAddNew.Visible=true;
			this.btnUp.Visible = false;
			this.btnDown.Visible = false;
			this.btnDelete.Visible = false;
			this.btnSave.Visible = false;
			this.txtName.Text = "";
			ViewState["DictionaryItemCode"] = "";
		}

		private void btnDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			
			string dictionaryItemCode = this.dgItem.SelectedItem.Cells[0].Text;
			try
			{
				EntityData entity = RmsPM.DAL.EntityDAO.SystemManageDAO.GetDictionaryItemByCode(dictionaryItemCode);
				RmsPM.DAL.EntityDAO.SystemManageDAO.DeleteDictionaryItem(entity);
				entity.Dispose();
				LoadItemData(ViewState["DictionaryNameCode"].ToString(),-1);
				Clear();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除出错"));
			}
		}

		private void btnUp_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//string dictionaryItemCode = (string) ViewState["DictionaryNameCode"] ;
			int itemIndex = this.dgItem.SelectedIndex;
		
			if (itemIndex == 0)
				return;
			int newIndex = itemIndex-1;

			string projectCode = Request["ProjectCode"] + "";
			string dictNameCode = ViewState["DictionaryNameCode"].ToString();

			try
			{
				EntityData entity = RmsPM.DAL.EntityDAO.SystemManageDAO.GetStandard_DictionaryNameByCode(dictNameCode);
				entity.SetCurrentTable("DictionaryItem");

				int iCount = entity.CurrentTable.Rows.Count;
				for ( int i=0;i<iCount;i++)
				{
					entity.SetCurrentRow(i);
					if ( i == (itemIndex-1) )
						entity.CurrentRow["SortID"] = itemIndex;
					else if ( i == itemIndex)
						entity.CurrentRow["SortID"] = itemIndex - 1 ;
					else
						entity.CurrentRow["SortID"] = i ;

				}
				DAL.EntityDAO.SystemManageDAO.SubmitAllStandard_DictionaryName(entity);
				LoadItemData(dictNameCode,newIndex);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "移动出错"));
			}
		
		}

		private void btnDown_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{

			int itemIndex = this.dgItem.SelectedIndex;
			if (itemIndex >= this.dgItem.Items.Count)
				return;
			int newIndex = itemIndex+1;

			string projectCode = Request["ProjectCode"] + "";
			string dictNameCode = ViewState["DictionaryNameCode"].ToString();
			try
			{
				EntityData entity = RmsPM.DAL.EntityDAO.SystemManageDAO.GetStandard_DictionaryNameByCode(dictNameCode);
				entity.SetCurrentTable("DictionaryItem");

				int iCount = entity.CurrentTable.Rows.Count;
				for ( int i=0;i<iCount;i++)
				{
					entity.SetCurrentRow(i);
					if ( i == (itemIndex+1) )
						entity.CurrentRow["SortID"] = itemIndex;
					else if ( i == itemIndex )
						entity.CurrentRow["SortID"] = itemIndex + 1 ;
					else
						entity.CurrentRow["SortID"] = i ;

				}
				DAL.EntityDAO.SystemManageDAO.SubmitAllStandard_DictionaryName(entity);
				LoadItemData(dictNameCode,newIndex);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "移动出错"));
			}
		}


		protected void btnInput_ServerClick(object sender, System.EventArgs e)
		{
			string inputProjectName = this.txtInputProjectName.Value.Trim();
			string projectCode = Request["ProjectCode"] + "";

			if ( inputProjectName == "")
			{
				Response.Write( Rms.Web.JavaScript.Alert( true,"请输入项目源项目名称 ！"));
			}

			try
			{

				// 找到输入的源项目
				ProjectStrategyBuilder sb = new ProjectStrategyBuilder();
				sb.AddStrategy( new Strategy( ProjectStrategyName.ProjectName,inputProjectName ));
				string sql = sb.BuildMainQueryString();
				QueryAgent qa = new QueryAgent();
				EntityData inputProjectEntity = qa.FillEntityData("Project",sql);
				qa.Dispose();

				string inputProjectCode = "";
				if ( inputProjectEntity.HasRecord())
				{
					inputProjectCode = inputProjectEntity.GetString("ProjectCode");
				}
				inputProjectEntity.Dispose();

				if ( inputProjectCode=="" )
				{
					Rms.Web.JavaScript.Alert( true,"没有找到这个项目：" + inputProjectName + "， 请输入完整正确的名称 ！ "  );
					return;
				}

				// 删除本项目中的字典项
				EntityData entityItem = DAL.EntityDAO.SystemManageDAO.GetDictionaryItemByProjectCode(projectCode);
				entityItem.DeleteAllTableRow("DictionaryItem");
				DAL.EntityDAO.SystemManageDAO.DeleteDictionaryItem(entityItem);
				entityItem.Dispose();

				EntityData entityName = RmsPM.DAL.EntityDAO.SystemManageDAO.GetDictionaryNameByProjectCode(projectCode);
				entityName.DeleteAllTableRow("DictionaryName");
				DAL.EntityDAO.SystemManageDAO.DeleteDictionaryName(entityName);
				entityName.Dispose();

				// 复制源项目中的内容
				EntityData entityInputName = RmsPM.DAL.EntityDAO.SystemManageDAO.GetDictionaryNameByProjectCode(inputProjectCode);
				EntityData entity = new EntityData("Standard_DictionaryName");
				int iCount = entityInputName.CurrentTable.Rows.Count;

				for(int i=0;i<iCount;i++)
				{
					entityInputName.SetCurrentRow(i);
					string dictionaryNameCodeInput = entityInputName.GetString("DictionaryNameCode");
					EntityData entityInput = DAL.EntityDAO.SystemManageDAO.GetStandard_DictionaryNameByCode(dictionaryNameCodeInput);

					string dictionaryNameCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("DictionaryNameCode");
					entity.SetCurrentTable("DictionaryName");
					DataRow dr = entity.GetNewRecord();
					dr["Name"] =entityInput.CurrentRow["Name"];
					dr["Remark"] =entityInput.CurrentRow["Remark"];
					dr["ProjectCode"]=projectCode;
					dr["DictionaryNameCode"]=dictionaryNameCode;
					entity.AddNewRecord(dr);

					entity.SetCurrentTable("DictionaryItem");
					entityInput.SetCurrentTable("DictionaryItem");
					int iICount = entityInput.CurrentTable.Rows.Count;
					for ( int j=0;j<iICount;j++)
					{
						entityInput.SetCurrentRow(j);
						DataRow drItem = entity.Tables["DictionaryItem"].NewRow();
						drItem["DictionaryItemCode"]=DAL.EntityDAO.SystemManageDAO.GetNewSysCode("DictionaryItemCode");
						drItem["Name"] =entityInput.CurrentRow["Name"];
						drItem["SortID"] = entityInput.CurrentRow["SortID"]  ;
						drItem["ProjectCode"] =projectCode;
						drItem["DictionaryNameCode"] = dictionaryNameCode;
						entity.Tables["DictionaryItem"].Rows.Add(drItem);
					}
					entityInput.Dispose();
				}
				DAL.EntityDAO.SystemManageDAO.SubmitAllStandard_DictionaryName(entity);
				entity.Dispose();
				entityInputName.Dispose();

				Response.Write( Rms.Web.JavaScript.ScriptStart );
				Response.Write( Rms.Web.JavaScript.Alert(false,"导入成功 ！") );
				Response.Write( Rms.Web.JavaScript.PageTo(false,"Dictionary.aspx?ProjectCode=" + projectCode ));
				Response.Write( Rms.Web.JavaScript.ScriptEnd );
				Response.End();



			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "导入出错"));
			}

		}

		private void btnAddNew_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			string name = this.txtName.Text.Trim();
			if ( name == "" ) 
			{
				Response.Write(Rms.Web.JavaScript.Alert(true,"请填写字典项 ！"));
				return;
			}
			string dictNameCode = ViewState["DictionaryNameCode"].ToString();

			try
			{
				string projectCode=""+Request.QueryString["ProjectCode"];
				int MaxID = 0;
				EntityData entity = RmsPM.DAL.EntityDAO.SystemManageDAO.GetStandard_DictionaryNameByCode(dictNameCode);
				entity.SetCurrentTable("DictionaryItem");
				int iC = entity.CurrentTable.Rows.Count;
				if ( iC > 0 )
				{
					entity.SetCurrentRow(iC-1);
					MaxID = entity.GetInt("SortID");
				}
				MaxID ++;

				DataRow dr = entity.GetNewRecord(); 
				dr["DictionaryItemCode"] = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("DictionaryItemCode");
				dr["Name"] = this.txtName.Text;
				dr["SortID"] = MaxID;
				dr["ProjectCode"] =projectCode;
				dr["DictionaryNameCode"] = dictNameCode;

				entity.AddNewRecord(dr);
				RmsPM.DAL.EntityDAO.SystemManageDAO.SubmitAllStandard_DictionaryName(entity);
				entity.Dispose();
				LoadItemData(dictNameCode,-1);
				Clear();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog ( this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "新增出错"));
			}
		}

		private void btnSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			string name = this.txtName.Text.Trim();
			if ( name == "" ) 
				return;

			string dictNameCode = ViewState["DictionaryNameCode"].ToString();
			string dictItemCode = ViewState["DictionaryItemCode"].ToString();

			try
			{
				EntityData entity = RmsPM.DAL.EntityDAO.SystemManageDAO.GetDictionaryItemByCode(dictItemCode);
				if ( entity.HasRecord())
				{
					DataRow dr = entity.CurrentRow;
					dr["Name"] = this.txtName.Text;
					RmsPM.DAL.EntityDAO.SystemManageDAO.UpdateDictionaryItem(entity);
				}
				entity.Dispose();
				LoadItemData( dictNameCode,-1);
				Clear();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog ( this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "保存出错"));
			}
		}




	}
}
