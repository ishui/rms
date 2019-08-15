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

namespace RmsPM.Web.Material
{
	/// <summary>
	/// ImportSupplierMaterialDlg ��ժҪ˵����
	/// </summary>
	public partial class ImportSupplierMaterialDlg : PageBase
	{
		protected System.Web.UI.WebControls.Label Label1;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
			}
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
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
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
                BLL.SupplierMaterialImport imp = new SupplierMaterialImport();
				this.lblFieldDesc.Text = imp.GetDefineFieldDesc();

				//ȡ���е�ϵͳ�����ȫ��
				QueryAgent qa = new Rms.ORMap.QueryAgent();
				try 
				{
					DataTable tbAllSystemGroup = qa.ExecSqlForDataSet("select dbo.GetSystemGroupFullName(GroupCode) as FullName, * from SystemGroup where ClassCode = '1413'").Tables[0];
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
				Response.Write(JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		/// <summary>
		/// ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnOK_ServerClick(object sender, System.EventArgs e)
		{
			if (this.txtFile.PostedFile.FileName == "") 
			{
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ѡ���ļ�"));
				return;
			}

			int succ_count = 0;
			int err_count = 0;

			try
			{
				this.txtResult.Value = "";
				StreamReader m_sr = new StreamReader(this.txtFile.PostedFile.InputStream, System.Text.Encoding.Default);
				int r = 0;

				//��1���Ǳ���
				if (m_sr.Peek() >= 0) 
				{
					m_sr.ReadLine();
					r++;
				}

				BLL.SupplierMaterialImport imp = new SupplierMaterialImport();
				EntityData entity = DAL.EntityDAO.MaterialDAO.GetAllSupplierMaterial();

				while (m_sr.Peek() >= 0) 
				{
					string s = m_sr.ReadLine();
					r++;

					string hint = "";
                    string SupplierName = "";
					string SupplierMaterialGroupFullName = "";
                    DataRow dr = imp.ImportSupplierMaterialSingle(s, entity, ref hint, ref SupplierName, ref SupplierMaterialGroupFullName, false, (DataTable)Session["tbAllSystemGroup"]);

					if (dr == null)
					{
						//����
						err_count++;
                        this.txtResult.Value = this.txtResult.Value + string.Format("�� {0} �� {1}��", r, "") + hint + "\n";
					}
					else 
					{
						//�ɹ�
						succ_count++;
					}
				}

				DAL.EntityDAO.MaterialDAO.SubmitAllSupplierMaterial(entity);
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(JavaScript.Alert(true, "�������" + ex.Message));
				return;
			}

			if (err_count > 0) 
			{
				this.txtResult.Value = string.Format("������ɣ�{0}���ɹ��� {1}������", succ_count, err_count) + "\n" + this.txtResult.Value;
			}
			else 
			{
				this.txtResult.Value = string.Format("{0}������ɹ�", succ_count);
			}

			//�����д�ʱ����־
			ApplicationLog.WriteFile("���̲��ϵ���.log", this.ToString(), this.txtResult.Value);

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
		/// ���
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnClear_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				//��ճ��̲���
				BLL.MaterialRule.DeleteAllSupplierMaterial();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(JavaScript.Alert(true, "��ճ���" + ex.Message));
				return;
			}

			Response.Write(JavaScript.Alert(true,"���̲��������"));
			RefreshParent();
		}

		private void ReadStream()
		{
			DataTable tbStream = new DataTable();
			tbStream.Columns.Add("rowid", typeof(int));
			tbStream.Columns.Add("text");

			StreamReader m_sr = new StreamReader(this.txtFile.PostedFile.InputStream, System.Text.Encoding.Default);

			int r = 0;

			//��1���Ǳ���
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

			Session["tbImportSupplierMaterialStream"] = tbStream;
		}

		/// <summary>
		/// ��ʾҪ����ĳ��̲����б�
		/// </summary>
		private void ShowImportList()
		{
			try 
			{
				if (Session["tbImportSupplierMaterialStream"] == null)
					throw new Exception("��ʱ�������µ���");

                DataTable tbStream = (DataTable)Session["tbImportSupplierMaterialStream"];

				BLL.SupplierMaterialImport imp = new SupplierMaterialImport();
//				EntityData entity = new EntityData("SupplierMaterial");
				EntityData entity = DAL.EntityDAO.MaterialDAO.GetAllSupplierMaterial();

				//����Ԥ��������
                entity.CurrentTable.Columns.Add("GroupFullName");
                entity.CurrentTable.Columns.Add("SupplierName");
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
					string GroupFullName = "";
                    DataRow dr = imp.ImportSupplierMaterialSingle(s, entity, ref hint, ref SupplierName, ref GroupFullName, true, (DataTable)Session["tbAllSystemGroup"]);

					if (dr == null) 
					{
						//����
						err_count++;
						dr = entity.CurrentTable.NewRow();
						dr["SupplierMaterialCode"] = "ERR_" + r.ToString();
						dr["ImportHint"] = hint;
						dr["ImportResult"] = -1;
						dr["ImportResultName"] = "�д�";
						entity.CurrentTable.Rows.Add(dr);
					}
					else 
					{
						//�ɹ�
						if (dr.RowState == DataRowState.Added)
						{
							add_count++;
							dr["ImportResult"] = 1;
							dr["ImportResultName"] = "����";
						}
						else
						{
							edit_count++;
							dr["ImportResult"] = 2;
							dr["ImportResultName"] = "�޸�";
						}
					}

					dr["GroupFullName"] = GroupFullName;
                    dr["SupplierName"] = SupplierName;
                    dr["rowid"] = r;
					dr["text"] = s;
					dr["IsImport"] = 1;
				}
				
				this.lblCountAdd.Text = add_count.ToString();
				this.lblCountEdit.Text = edit_count.ToString();
				this.lblCountErr.Text = err_count.ToString();

				Session["entityImportSupplierMaterial"] = entity;
				BindImportList();
//				entity.Dispose();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(JavaScript.Alert(true, "��ʾҪ����ĳ��̲����б����" + ex.Message));
			}
		}

		private void BindImportList()
		{
			EntityData entity = (EntityData)Session["entityImportSupplierMaterial"];

			//����
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
				case -1: //�д�
					color = "red";
					break;

				case 1: //����
					color = "blue";
					break;

				case 2: //�޸�
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
					hint =  "��ѡ���ļ�";
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
		/// ��һ��
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
				Response.Write(JavaScript.Alert(true, "��һ������" + ex.Message));
			}
		}

		/// <summary>
		/// ��һ��
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
				Response.Write(JavaScript.Alert(true, "��һ������" + ex.Message));
			}
		}

		/// <summary>
		/// ����
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
				Response.Write(JavaScript.Alert(true, "��������" + ex.Message));
			}
		}

		/// <summary>
		/// ���
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

					if (Session["tbImportSupplierMaterialStream"] == null)
						throw new Exception("��ʱ�������µ���");

                    DataTable tbStream = (DataTable)Session["tbImportSupplierMaterialStream"];

					BLL.SupplierMaterialImport imp = new SupplierMaterialImport();
					EntityData entity = DAL.EntityDAO.MaterialDAO.GetAllSupplierMaterial();

					foreach(DataRow drStream in tbStream.Rows)
					{
						string s = BLL.ConvertRule.ToString(drStream["text"]);
						int r = BLL.ConvertRule.ToInt(drStream["rowid"]);

						string hint = "";
                        string SupplierName = "";
						string GroupFullName = "";
                        DataRow dr = imp.ImportSupplierMaterialSingle(s, entity, ref hint, ref SupplierName, ref GroupFullName, false, (DataTable)Session["tbAllSystemGroup"]);

						if (dr == null) 
						{
							//����
							err_count++;
                            this.txtResult.Value = this.txtResult.Value + string.Format("�� {0} �� {1}��", r, "") + hint + "\n";
						}
						else 
						{
							//�ɹ�
							succ_count++;

							if (dr.RowState == DataRowState.Added) //����
							{
								succ_add_count++;
							}
						}
					}

					DAL.EntityDAO.MaterialDAO.SubmitAllSupplierMaterial(entity);
				}
				catch(Exception ex)
				{
					ApplicationLog.WriteLog(this.ToString(),ex,"");
					Response.Write(JavaScript.Alert(true, "�������" + ex.Message));
					return;
				}

				int succ_edit_count = succ_count - succ_add_count;

				if (err_count > 0) 
				{
					this.txtResult.Value = string.Format("������ɣ�{0}���ɹ�������{1}�����޸�{2}������{3}������", succ_count, succ_add_count, succ_edit_count, err_count) + "\n" + this.txtResult.Value;
				}
				else 
				{
					this.txtResult.Value = string.Format("������ɣ�{0}������ɹ�������{1}�����޸�{2}����", succ_count, succ_add_count, succ_edit_count);
				}

				//�����д�ʱ����־
				ApplicationLog.WriteFile("���̲��ϵ���.log", this.ToString(), this.txtResult.Value);

				RefreshParent();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(JavaScript.Alert(true, "��ɳ���" + ex.Message));
			}
		}

		/// <summary>
		/// ���Ϊcsv
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDownloadCsv_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				if (Session["entityImportSupplierMaterial"] == null)
					throw new Exception("��ʱ�������µ���");

                EntityData entity = (EntityData)Session["entityImportSupplierMaterial"];
				DataView dv = new DataView(entity.CurrentTable, "IsImport = 1", "rowid", DataViewRowState.CurrentRows);

				RmsPM.BLL.FileIO io = new FileIO(Response, Request, Server, Session);
				io.SaveFileName = "SupplierMaterialImportPrepare.csv";

				StreamWriter w = new StreamWriter(io.SaveFileNamePhy, false, System.Text.Encoding.Default);
				try 
				{
					BLL.SupplierMaterialImport imp = new SupplierMaterialImport();
					string s;

					//��1�����б���
					s = imp.GetDefineFieldDesc();
					s += ",Ԥ������,������ʾ";
					w.WriteLine(s);

					int count = imp.tbDefine.Rows.Count;

					//����
					foreach(DataRowView drv in dv) 
					{
						DataRow dr = drv.Row;
						s = "";

						s = BLL.ConvertRule.ToString(dr["text"]);
						string[] arr = BLL.ImportRule.SplitCsvLine(s);

						//�á�,����������
						for(int k=arr.Length;k<count;k++) 
						{
							s += ",";
						}

						/*
						//���ֶζ�������д��
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

						//д��Ԥ������
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
				Response.Write(JavaScript.Alert(true, "���Ϊcsv����" + ex.Message));
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}	
	}

}
