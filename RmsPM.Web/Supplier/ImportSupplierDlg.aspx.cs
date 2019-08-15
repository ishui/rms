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
using System.IO;
using Rms.ORMap;
using RmsPM.BLL;
using Rms.Web;

namespace RmsPM.Web.Supplier
{
	/// <summary>
	/// ImportSupplierDlg 的摘要说明。
	/// </summary>
	public partial class ImportSupplierDlg : PageBase
	{
		protected System.Web.UI.WebControls.Label Label1;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
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
			this.dgList.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgList_SortCommand);

		}
		#endregion

		private void IniPage() 
		{
			try 
			{
				BLL.SupplierImport imp = new SupplierImport();
				this.lblFieldDesc.Text = imp.GetDefineFieldDesc();

				//取所有的系统类别及其全名
				QueryAgent qa = new Rms.ORMap.QueryAgent();
				try 
				{
					DataTable tbAllSystemGroup = qa.ExecSqlForDataSet("select dbo.GetSystemGroupFullName(GroupCode) as FullName, * from SystemGroup where ClassCode = '1401'").Tables[0];
					Session["tbAllSystemGroup"] = tbAllSystemGroup;
				}
				finally
				{
					qa.Dispose();
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 导入
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnOK_ServerClick(object sender, System.EventArgs e)
		{
			if (this.txtFile.PostedFile.FileName == "") 
			{
				Response.Write(Rms.Web.JavaScript.Alert(true, "请选择文件"));
				return;
			}

			int succ_count = 0;
			int err_count = 0;

			try
			{
				this.txtResult.Value = "";
				StreamReader m_sr = new StreamReader(this.txtFile.PostedFile.InputStream, System.Text.Encoding.Default);
				int r = 0;

				//第1行是标题
				if (m_sr.Peek() >= 0) 
				{
					m_sr.ReadLine();
					r++;
				}

				BLL.SupplierImport imp = new SupplierImport();
				EntityData entity = DAL.EntityDAO.ProjectDAO.GetAllSupplier();

				while (m_sr.Peek() >= 0) 
				{
					string s = m_sr.ReadLine();
					r++;

					string hint = "";
					string SupplierName = "";
					string SupplierTypeFullName = "";
					DataRow dr = imp.ImportSupplierSingle(s, entity, ref hint, ref SupplierName, ref SupplierTypeFullName, false, (DataTable)Session["tbAllSystemGroup"]);

					if (dr == null)
					{
						//出错
						err_count++;
						this.txtResult.Value = this.txtResult.Value + string.Format("第 {0} 行 {1}：", r, SupplierName) + hint + "\n";
					}
					else 
					{
						//成功
						succ_count++;
					}
				}

				DAL.EntityDAO.ProjectDAO.SubmitAllSupplier(entity);
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(JavaScript.Alert(true, "导入出错：" + ex.Message));
				return;
			}

			if (err_count > 0) 
			{
				this.txtResult.Value = string.Format("导入完成，{0}条成功， {1}条出错：", succ_count, err_count) + "\n" + this.txtResult.Value;
			}
			else 
			{
				this.txtResult.Value = string.Format("{0}条导入成功", succ_count);
			}

			//导入有错时记日志
			ApplicationLog.WriteFile("厂商导入.log", this.ToString(), this.txtResult.Value);

			RefreshParent();
			//			Response.Write(JavaScript.WinClose(false));
		}

		private void RefreshParent()
		{
			Response.Write(JavaScript.ScriptStart);
			Response.Write("window.opener.location = window.opener.location;");
			Response.Write(JavaScript.ScriptEnd);
		}

		/// <summary>
		/// 清空
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnClear_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				//清空厂商
				BLL.SupplierRule.DeleteAllSupplier();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(JavaScript.Alert(true, "清空出错：" + ex.Message));
				return;
			}

			Response.Write(JavaScript.Alert(true,"厂商已清空"));
			RefreshParent();
		}

		private void ReadStream()
		{
			DataTable tbStream = new DataTable();
			tbStream.Columns.Add("rowid", typeof(int));
			tbStream.Columns.Add("text");

			StreamReader m_sr = new StreamReader(this.txtFile.PostedFile.InputStream, System.Text.Encoding.Default);

			int r = 0;

			//第1行是标题
			if (m_sr.Peek() >= 0) 
			{
				m_sr.ReadLine();
				r++;
			}

			while (m_sr.Peek() >= 0) 
			{
				string s = m_sr.ReadLine();
				r++;

				DataRow drNew = tbStream.NewRow();
				drNew["rowid"] = r;
				drNew["text"] = s;
				tbStream.Rows.Add(drNew);
			}

			Session["tbImportSupplierStream"] = tbStream;
		}

		/// <summary>
		/// 显示要导入的厂商列表
		/// </summary>
		private void ShowImportList()
		{
			try 
			{
				if (Session["tbImportSupplierStream"] == null)
					throw new Exception("超时，请重新导入");
					
				DataTable tbStream = (DataTable)Session["tbImportSupplierStream"];

				BLL.SupplierImport imp = new SupplierImport();
//				EntityData entity = new EntityData("Supplier");
				EntityData entity = DAL.EntityDAO.ProjectDAO.GetAllSupplier();

				//增加预导入结果列
				entity.CurrentTable.Columns.Add("SupplierTypeFullName");
				entity.CurrentTable.Columns.Add("rowid", typeof(int));
				entity.CurrentTable.Columns.Add("text");
				entity.CurrentTable.Columns.Add("ImportHint");
				entity.CurrentTable.Columns.Add("ImportResult", typeof(int));
				entity.CurrentTable.Columns.Add("ImportResultName");
				entity.CurrentTable.Columns.Add("IsImport", typeof(int));

				int add_count = 0;
				int edit_count = 0;
				int err_count = 0;

				foreach(DataRow drStream in tbStream.Rows)
				{
					string s = BLL.ConvertRule.ToString(drStream["text"]);
					int r = BLL.ConvertRule.ToInt(drStream["rowid"]);

					string hint = "";
					string SupplierName = "";
					string SupplierTypeFullName = "";
					DataRow dr = imp.ImportSupplierSingle(s, entity, ref hint, ref SupplierName, ref SupplierTypeFullName, true, (DataTable)Session["tbAllSystemGroup"]);

					if (dr == null) 
					{
						//出错
						err_count++;
						dr = entity.CurrentTable.NewRow();
						dr["SupplierCode"] = "ERR_" + r.ToString();
						dr["ImportHint"] = hint;
						dr["ImportResult"] = -1;
						dr["ImportResultName"] = "有错";
						entity.CurrentTable.Rows.Add(dr);
					}
					else 
					{
						//成功
						if (dr.RowState == DataRowState.Added)
						{
							add_count++;
							dr["ImportResult"] = 1;
							dr["ImportResultName"] = "新增";
						}
						else
						{
							edit_count++;
							dr["ImportResult"] = 2;
							dr["ImportResultName"] = "修改";
						}
					}

					dr["SupplierTypeFullName"] = SupplierTypeFullName;
					dr["rowid"] = r;
					dr["text"] = s;
					dr["IsImport"] = 1;
				}
				
				this.lblCountAdd.Text = add_count.ToString();
				this.lblCountEdit.Text = edit_count.ToString();
				this.lblCountErr.Text = err_count.ToString();

				Session["entityImportSupplier"] = entity;
				BindImportList();
//				entity.Dispose();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(JavaScript.Alert(true, "显示要导入的厂商列表出错：" + ex.Message));
			}
		}

		private void BindImportList()
		{
			EntityData entity = (EntityData)Session["entityImportSupplier"];

			//排序
			string sortsql = BLL.GridSort.GetSortSQL(ViewState);
			if (sortsql == "") 
			{
				sortsql = "rowid";
			}

			DataView dv = new DataView(entity.CurrentTable, "IsImport = 1", sortsql, DataViewRowState.CurrentRows);

			this.dgList.DataSource = dv;
			this.dgList.DataBind();
		}

		public static string GetImportResultColor(object objImportResult)
		{
			string color = "";

			int ImportResult = BLL.ConvertRule.ToInt(objImportResult);

			switch(ImportResult) 
			{
				case -1: //有错
					color = "red";
					break;

				case 1: //新增
					color = "blue";
					break;

				case 2: //修改
					color = "";
					break;
			}

			return color;
		}

		private string CheckStep(int step)
		{
			string hint = "";

			if (step == 1)
			{
				if (this.txtFile.PostedFile.FileName == "") 
				{
					hint =  "请选择文件";
					return hint;
				}

				ReadStream();
			}

			return hint;
		}

		private void DoStep(int step)
		{
			try 
			{
				if(step == 2)
				{
					ShowImportList();
				}
			}
			catch(Exception ex) 
			{
				throw ex;
			}
		}

		/// <summary>
		/// 下一步
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnNext_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				int step = BLL.ConvertRule.ToInt(this.txtStep.Value);

				string hint = CheckStep(step);
				if (hint != "")
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, hint));
					return;
				}

				step++;

				DoStep(step);

				this.txtStep.Value = step.ToString();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(JavaScript.Alert(true, "下一步出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 上一步
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnPrior_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				int step = BLL.ConvertRule.ToInt(this.txtStep.Value);
				step--;

				this.txtStep.Value = step.ToString();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(JavaScript.Alert(true, "上一步出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 继续
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnContinue_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				int step = 1;

				this.txtStep.Value = step.ToString();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(JavaScript.Alert(true, "继续出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 完成
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnComplete_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				int step = 9;

				this.txtStep.Value = step.ToString();

				int succ_count = 0;
				int err_count = 0;
				int succ_add_count = 0;

				try
				{
					this.txtResult.Value = "";

					if (Session["tbImportSupplierStream"] == null)
						throw new Exception("超时，请重新导入");
					
					DataTable tbStream = (DataTable)Session["tbImportSupplierStream"];

					BLL.SupplierImport imp = new SupplierImport();
					EntityData entity = DAL.EntityDAO.ProjectDAO.GetAllSupplier();

					foreach(DataRow drStream in tbStream.Rows)
					{
						string s = BLL.ConvertRule.ToString(drStream["text"]);
						int r = BLL.ConvertRule.ToInt(drStream["rowid"]);

						string hint = "";
						string SupplierName = "";
						string SupplierTypeFullName = "";
						DataRow dr = imp.ImportSupplierSingle(s, entity, ref hint, ref SupplierName, ref SupplierTypeFullName, false, (DataTable)Session["tbAllSystemGroup"]);

						if (dr == null) 
						{
							//出错
							err_count++;
							this.txtResult.Value = this.txtResult.Value + string.Format("第 {0} 行 {1}：", r, SupplierName) + hint + "\n";
						}
						else 
						{
							//成功
							succ_count++;

							if (dr.RowState == DataRowState.Added) //新增
							{
								succ_add_count++;
							}
						}
					}

					DAL.EntityDAO.ProjectDAO.SubmitAllSupplier(entity);
				}
				catch(Exception ex)
				{
					ApplicationLog.WriteLog(this.ToString(),ex,"");
					Response.Write(JavaScript.Alert(true, "导入出错：" + ex.Message));
					return;
				}

				int succ_edit_count = succ_count - succ_add_count;

				if (err_count > 0) 
				{
					this.txtResult.Value = string.Format("导入完成，{0}条成功（新增{1}条、修改{2}条），{3}条出错：", succ_count, succ_add_count, succ_edit_count, err_count) + "\n" + this.txtResult.Value;
				}
				else 
				{
					this.txtResult.Value = string.Format("导入完成，{0}条导入成功（新增{1}条、修改{2}条）", succ_count, succ_add_count, succ_edit_count);
				}

				//导入有错时记日志
				ApplicationLog.WriteFile("厂商导入.log", this.ToString(), this.txtResult.Value);

				RefreshParent();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(JavaScript.Alert(true, "完成出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 另存为csv
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDownloadCsv_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				if (Session["entityImportSupplier"] == null)
					throw new Exception("超时，请重新导入");
					
				EntityData entity = (EntityData)Session["entityImportSupplier"];
				DataView dv = new DataView(entity.CurrentTable, "IsImport = 1", "rowid", DataViewRowState.CurrentRows);

				RmsPM.BLL.FileIO io = new FileIO(Response, Request, Server, Session);
				io.SaveFileName = "SupplierImportPrepare.csv";

				StreamWriter w = new StreamWriter(io.SaveFileNamePhy, false, System.Text.Encoding.Default);
				try 
				{
					BLL.SupplierImport imp = new SupplierImport();
					string s;

					//第1行是列标题
					s = imp.GetDefineFieldDesc();
					s += ",预导入结果,错误提示";
					w.WriteLine(s);

					int count = imp.tbDefine.Rows.Count;

					//数据
					foreach(DataRowView drv in dv) 
					{
						DataRow dr = drv.Row;
						s = "";

						s = BLL.ConvertRule.ToString(dr["text"]);
						string[] arr = BLL.ImportRule.SplitCsvLine(s);

						//用“,”补足列数
						for(int k=arr.Length;k<count;k++) 
						{
							s += ",";
						}

						/*
						//按字段定义重新写入
						for (int i=0;i<count;i++) 
						{
							DataRow drDefine = imp.tbDefine.Rows[i];
							string FieldName = drDefine["FieldName"].ToString();

							if (i > 0)
								s += ",";

							if (FieldName != "")
							{
								s += BLL.ConvertRule.ToString(dr[FieldName]);
							}
						}
						*/

						//写入预导入结果
						s += "," + BLL.ConvertRule.ToString(dr["ImportResultName"]);

						s += "," + BLL.ConvertRule.ToString(dr["ImportHint"]);

						w.WriteLine(s);
					}
				}
				finally 
				{
					w.Close();
				}

				io.ShowClient();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(JavaScript.Alert(true, "另存为csv出错：" + ex.Message));
			}
		}

		private void dgList_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{
				BLL.GridSort.SortCommand((DataGrid)source, ViewState, source, e);
				((DataGrid)source).CurrentPageIndex = 0;
				BindImportList();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void dgList_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			try
			{
				BLL.GridSort.ItemCreate((DataGrid)sender, ViewState, sender, e);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}	
	}

}
