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
using RmsPM.DAL.QueryStrategy;
using RmsPM.BLL;
using Rms.Web;

namespace RmsPM.Web.Sal
{
	/// <summary>
	/// SalBudgetModify ��ժҪ˵����
	/// </summary>
	public partial class SalBudgetModify : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlGenericControl spanTitle;

		private Repeater[] arrdgList;
		private void SetArrdgList()
		{
			if (arrdgList == null) arrdgList =  new Repeater[5];

			arrdgList[0] = this.dgList;
			arrdgList[1] = this.dgListArea;
			arrdgList[2] = this.dgListPrice;
			arrdgList[3] = this.dgListMoney;
			arrdgList[4] = this.dgListRcvMoney;
		}
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			SetArrdgList();
			ViewState["HasLoadGrid"] = 0;

			if (!Page.IsPostBack) 
			{
				IniPage();
				LoadData();
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

		}
		#endregion

		private void IniPage() 
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtYear.Value = Request.QueryString["Year"];
				this.txtRefreshScript.Value = Request.QueryString["RefreshScript"];

				this.txtProjectName.Value = BLL.ProjectRule.GetProjectName(this.txtProjectCode.Value);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadData() 
		{
			try 
			{
				string ProjectCode = this.txtProjectCode.Value;
				int IYear = BLL.ConvertRule.ToInt(this.txtYear.Value);

				this.lblBudgetName.Text = string.Format("{1}{0}������ۼƻ�", IYear, this.txtProjectName.Value);

				EntityData entity = DAL.EntityDAO.SalDAO.GetSalBudgetByProjectYear(ProjectCode, IYear);
				DataRow dr;
				bool isExists = entity.HasRecord();

				if (entity.HasRecord()) 
				{
					dr = entity.CurrentRow;
				}
				else 
				{
					//����ʱ��ȱʡֵ
					dr = entity.CurrentTable.NewRow();
					dr["IYear"] = IYear;
					dr["StartMonth"] = 1;
					dr["PeriodMonth"] = 12;
					dr["AfterPeriod"] = 2;
				}

				this.txtBudgetCode.Value = BLL.ConvertRule.ToString(dr["BudgetCode"]);

				this.lblPeriodMonthDesc.Text = BLL.SalRule.GetSalBudgetPeriodMonthDesc(dr);
				this.lblAfterPeriodDesc.Text = BLL.SalRule.GetSalBudgetAfterPeriodDesc(dr);

				entity.Dispose();

				LoadDataGrid();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ��ȼƻ�����" + ex.Message));
			}
		}

		private void LoadDataGrid() 
		{
			try 
			{
				string ProjectCode = this.txtProjectCode.Value;
				int IYear = BLL.ConvertRule.ToInt(this.txtYear.Value);

				DataTable tb = BLL.SalBudgetRule.GenerateSalBudgetDtlTableInfra(ProjectCode, IYear);

				tb.Columns.Add("TrID");
				tb.Columns.Add("TrHtml");

				string AllImageID = "";

				foreach(DataRow dr in tb.Rows)
				{
					string ItemName = BLL.ConvertRule.ToString(dr["ItemName"]);
					string ItemDesc = BLL.ConvertRule.ToString(dr["ItemDesc"]);
					string FieldName = BLL.ConvertRule.ToString(dr["FieldName"]);
					int IsAct = BLL.ConvertRule.ToInt(dr["IsAct"]);
					string IsActName = BLL.ConvertRule.ToString(dr["IsActName"]);
					int Deep = BLL.ConvertRule.ToInt(dr["Deep"]);
					string PBSTypeCode = BLL.ConvertRule.ToString(dr["PBSTypeCode"]);
					string PBSTypeName = BLL.ConvertRule.ToString(dr["PBSTypeName"]);
					string ParentCode = BLL.ConvertRule.ToString(dr["ParentCode"]);
					decimal val;

					string id = FieldName + "_" + IsAct.ToString() + "_" + PBSTypeCode;

					string html = "";
//					html += "<tr id='mytr" + id + "' PBSTypeCode='" + PBSTypeCode + "' ParentCode='" + ParentCode + "' Deep='" + Deep + "' FieldName='" + FieldName + "', IsAct='" + IsAct.ToString() + "'>";

					if (Deep == 0) //��Ʒ���͸����
					{
						/*
						if (IsAct == 0)  //�ƻ�
						{
							//�ƻ����ӽ����
							int AllCountPlan = tb.Select(string.Format("ItemName='{0}' and IsAct={1}", ItemName, IsAct)).Length;
							int rowspanPlan = AllCountPlan + 1;

							//ʵ�ʵ��ӽ����
							int AllCountAct = tb.Select(string.Format("ItemName='{0}' and IsAct=1", ItemName)).Length;
							int rowspanAct = AllCountAct + 1;

							html += "<td align='center' class='list-c' nowrap rowspan='" + (rowspanPlan + rowspanAct).ToString() + "'>" + ItemDesc + "</td>"
								+ "<td align='center' class='list-c' nowrap rowspan='" + rowspanPlan.ToString() + "'>" + IsActName + "</td>"
								;
						}
						else  //ʵ��
						{
							int AllCount = tb.Select(string.Format("ItemName='{0}' and IsAct={1}", ItemName, IsAct)).Length;
							int rowspan = AllCount + 1;

							html += "<td align='center' class='list-c' nowrap rowspan='" + rowspan.ToString() + "'>" + IsActName + "</td>"
								;
						}
						*/


						int AllCount = tb.Select(string.Format("ItemName='{0}' and IsAct={1}", ItemName, IsAct)).Length;
						int rowspan = AllCount + 1;

						html += "<td align='center' class='list-c' nowrap rowspan='" + rowspan.ToString() + "'>" + ItemDesc + "</td>"
							+ "<td align='center' class='list-c' nowrap rowspan='" + rowspan.ToString() + "'>�ƻ�</td>"
							;
					}
					else
					{
					}

					//��Ʒ�����Ƿ����ӽ��
					int ChildCount;
					if (Deep == 0)
					{
						ChildCount = tb.Select(string.Format("ItemName='{0}' and IsAct={1} and ParentCode='' and Deep > 0", ItemName, IsAct)).Length;
					}
					else
					{
						ChildCount = tb.Select(string.Format("ItemName='{0}' and IsAct={1} and ParentCode='{2}'", ItemName, IsAct, PBSTypeCode)).Length;
					}

					//չ�����۵�
					string image = "<span style=\"width:15px\">";
					if (ChildCount > 0)
					{
						image += "<img exp=1 id='myimg" + id + "' src=\"../Images/Minus.gif\" PBSTypeCode='" + PBSTypeCode + "' Deep='" + Deep.ToString() + "' FieldName='" + FieldName + "' IsAct='" + IsAct.ToString() + "' onclick=\"TreeExpand(this);\">";

						if (AllImageID != "") AllImageID += ",";
						AllImageID += "myimg" + id;
					}
					else
					{
					}
					image += "</span>";

					//����
					string space = "";
					for(int i=0;i<Deep;i++)
					{
						space += "&nbsp;&nbsp;";
					}

					//��Ʒ����
					html += "<td nowrap>"
						+ space + image + PBSTypeName
						+ "<input type=\"hidden\" name=\"txtPBSTypeCode_" + id + "\" value=\"" + PBSTypeCode + "\">"
						+ "</td>";

					//��ǰ�ۼ�
					val = BLL.ConvertRule.ToDecimal(dr["y0"]);
					html += "<td align='right'>" + BLL.SalBudgetRule.FormatSalBudgetFieldValue(val, FieldName) + "</td>";

					/*
					//����12����
					for(int i=1;i<=12;i++)
					{
						val = BLL.ConvertRule.ToDecimal(dr["m" + i.ToString()]);
						html += "<td>"
							+ "<input type=\"text\" class=\"input-nember\" size=\"8\" name=\"txtM" + i.ToString() + "_" + id.ToString() + "\""
							+ " value=\"" + BLL.SalBudgetRule.FormatSalBudgetFieldValue(val, FieldName) + "\">"
							+ "</td>";
					}

					//������ȼƻ�
					val = BLL.ConvertRule.ToDecimal(dr["m0"]);
					html += "<td>"
						+ "<input type=\"text\" class=\"input-nember\" size=\"8\" name=\"txtM0" + "_" + id.ToString() + "\""
						+ " value=\"" + BLL.SalBudgetRule.FormatSalBudgetFieldValue(val, FieldName) + "\">"
						+ "</td>";

					//��ȼƻ�
					for(int i=1;i<=2;i++)
					{
						val = BLL.ConvertRule.ToDecimal(dr["y" + i.ToString()]);
						html += "<td>"
							+ "<input type=\"text\" class=\"input-nember\" size=\"8\" name=\"txtY" + i.ToString() + "_" + id.ToString() + "\""
							+ " value=\"" + BLL.SalBudgetRule.FormatSalBudgetFieldValue(val, FieldName) + "\">"
							+ "</td>";
					}
					*/

//					html += "</tr>";

					dr["TrID"] = id;
					dr["TrHtml"] = html;
				}

				this.txtAllImageID.Value = AllImageID;

				BindDataGrid(tb);

				ViewState["HasLoadGrid"] = 1;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ��ȼƻ���ϸ����" + ex.Message));
			}
		}

		private void BindDataGrid(DataTable tb)
		{
			try 
			{
//				Session["tbSalBudgetList"] = tb;

				//				DataView dv = new DataView(tb, "Deep=0", "sno, IsAct, PBSTypeSortID", DataViewRowState.CurrentRows);
				
				//�ƻ�
				DataView dv = new DataView(tb, "IsAct=0 and FieldName='HouseCount'", "sno, IsAct, PBSTypeFullID", DataViewRowState.CurrentRows);
				this.dgList.DataSource = dv;
				this.dgList.DataBind();

				DataView dvArea = new DataView(tb, "IsAct=0 and FieldName='HouseArea'", "sno, IsAct, PBSTypeFullID", DataViewRowState.CurrentRows);
				this.dgListArea.DataSource = dvArea;
				this.dgListArea.DataBind();

				DataView dvPrice = new DataView(tb, "IsAct=0 and FieldName='Price'", "sno, IsAct, PBSTypeFullID", DataViewRowState.CurrentRows);
				this.dgListPrice.DataSource = dvPrice;
				this.dgListPrice.DataBind();

				DataView dvMoney = new DataView(tb, "IsAct=0 and FieldName='Money'", "sno, IsAct, PBSTypeFullID", DataViewRowState.CurrentRows);
				this.dgListMoney.DataSource = dvMoney;
				this.dgListMoney.DataBind();

				DataView dvRcvMoney = new DataView(tb, "IsAct=0 and FieldName='RcvMoney'", "sno, IsAct, PBSTypeFullID", DataViewRowState.CurrentRows);
				this.dgListRcvMoney.DataSource = dvRcvMoney;
				this.dgListRcvMoney.DataBind();

				/*
				//ʵ��
				DataView dvAct = new DataView(tb, "IsAct=1 and FieldName='HouseCount'", "sno, IsAct, PBSTypeFullID", DataViewRowState.CurrentRows);
				this.dgListAct.DataSource = dvAct;
				this.dgListAct.DataBind();

				DataView dvAreaAct = new DataView(tb, "IsAct=1 and FieldName='HouseArea'", "sno, IsAct, PBSTypeFullID", DataViewRowState.CurrentRows);
				this.dgListAreaAct.DataSource = dvAreaAct;
				this.dgListAreaAct.DataBind();

				DataView dvPriceAct = new DataView(tb, "IsAct=1 and FieldName='Price'", "sno, IsAct, PBSTypeFullID", DataViewRowState.CurrentRows);
				this.dgListPriceAct.DataSource = dvPriceAct;
				this.dgListPriceAct.DataBind();

				DataView dvMoneyAct = new DataView(tb, "IsAct=1 and FieldName='Money'", "sno, IsAct, PBSTypeFullID", DataViewRowState.CurrentRows);
				this.dgListMoneyAct.DataSource = dvMoneyAct;
				this.dgListMoneyAct.DataBind();

				DataView dvRcvMoneyAct = new DataView(tb, "IsAct=1 and FieldName='RcvMoney'", "sno, IsAct, PBSTypeFullID", DataViewRowState.CurrentRows);
				this.dgListRcvMoneyAct.DataSource = dvRcvMoneyAct;
				this.dgListRcvMoneyAct.DataBind();
				*/
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ��ȼƻ���ϸ����" + ex.Message));
			}
		}

		/// <summary>
		/// ��Ч�Լ��
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string hint) 
		{
			hint = "";

			foreach(Repeater dgList in arrdgList)
			{
				foreach(RepeaterItem item in dgList.Items)
				{
					HtmlInputHidden txtFieldName = (HtmlInputHidden)item.FindControl("txtFieldName");
					string FieldName = txtFieldName.Value;

					//����
					for(int m=0;m<=12;m++)
					{
						HtmlInputText txt = (HtmlInputText)item.FindControl("txtM" + m.ToString());

						hint = BLL.SalBudgetRule.CheckValidVal(txt.Value, FieldName);
						if (hint != "") return false;
					}

					//������ȼƻ�
					for(int y=1;y<=2;y++)
					{
						HtmlInputText txt = (HtmlInputText)item.FindControl("txtY" + y.ToString());

						hint = BLL.SalBudgetRule.CheckValidVal(txt.Value, FieldName);
						if (hint != "") return false;
					}
				}
			}

			/*

			//ȡ��Ʒ�����ֵ�
			DataTable tbPBSType = BLL.SalBudgetRule.GetSalPBSType();

			foreach(string FieldName in BLL.SalBudgetRule.BudgetFields)
			{
				foreach(DataRow dr in tbPBSType.Rows) //��Ʒ����
				{
					string PBSTypeCode = dr["PBSTypeCode"].ToString();

					//����
					for(int m=0;m<=12;m++)
					{
						string InputName = "txtM" + m.ToString() + "_" + FieldName + "_0_" + PBSTypeCode;
						string val = Request.Form[InputName];

						hint = BLL.SalBudgetRule.CheckValidVal(val, FieldName);
						if (hint != "") return false;
					}

					//������ȼƻ�
					for(int y=1;y<=2;y++)
					{
						string InputName = "txtY" + y.ToString() + "_" + FieldName + "_0_" + PBSTypeCode;
						string val = Request.Form[InputName];

						hint = BLL.SalBudgetRule.CheckValidVal(val, FieldName);
						if (hint != "") return false;
					}
				}
			}
			*/

			return true;
		}

		/*
		private void SaveOneYm(EntityData entity, int y, int m, string InputName) 
		{
			try 
			{
				string ProjectCode = this.txtProjectCode.Value;
				string BudgetCode = this.txtBudgetCode.Value;

				DataRow[] drs = entity.CurrentTable.Select("IYear=" + y.ToString() + " and IMonth=" + m.ToString());
				DataRow dr;
				bool isNew = (drs.Length == 0);

				if (isNew) 
				{
					dr = entity.CurrentTable.NewRow();
					dr["BudgetCode"] = BudgetCode;
					dr["SystemID"] = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("SalBudgetSystemID");
					dr["ProjectCode"] = ProjectCode;
					dr["IYear"] = y;
					dr["IMonth"] = m;

					entity.CurrentTable.Rows.Add(dr);
				}
				else 
				{
					dr = drs[0];
				}

				int iCount = this.dgList.Items.Count;
				for(int i=0;i<iCount;i++) 
				{
					HtmlInputHidden txtFieldName = (HtmlInputHidden)this.dgList.Items[i].FindControl("txtFieldName");
					HtmlInputText txtM = (HtmlInputText)this.dgList.Items[i].FindControl(InputName);

					string FieldName = txtFieldName.Value;

					dr[FieldName] = BLL.ConvertRule.ToDecimalObj(txtM.Value);
				}
			}
			catch (Exception ex) 
			{
				throw ex;
			}
		}
		*/

		/// <summary>
		/// �������ۼƻ�
		/// </summary>
		private void Save()
		{
			try 
			{
				string ProjectCode = this.txtProjectCode.Value;
				int IYear = BLL.ConvertRule.ToInt(this.txtYear.Value);

				EntityData entity = DAL.EntityDAO.SalDAO.GetSalBudgetByProjectYear(ProjectCode, IYear);
				DataRow dr = null;
				bool isNew = !entity.HasRecord();

				if (isNew) 
				{
					dr = entity.CurrentTable.NewRow();

					this.txtBudgetCode.Value = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("SalBudgetCode");
					dr["BudgetCode"] = this.txtBudgetCode.Value;
					dr["ProjectCode"] = ProjectCode;
					dr["IYear"] = IYear;
					dr["StartMonth"] = 1;
					dr["PeriodMonth"] = 12;
					dr["AfterPeriod"] = 2;

					entity.CurrentTable.Rows.Add(dr);
				}
				else 
				{
					dr = entity.CurrentRow;
					this.txtBudgetCode.Value = BLL.ConvertRule.ToString(dr["BudgetCode"]);
				}

				dr["ModiDate"] = DateTime.Now;
				dr["ModiPerson"] = base.user.UserCode;

				DAL.EntityDAO.SalDAO.SubmitAllSalBudget(entity);

				entity.Dispose();

				SaveDtl();
			}
			catch ( Exception ex )
			{
				throw ex;
			}
		}

		/// <summary>
		/// �������ۼƻ���ϸ
		/// </summary>
		private void SaveDtl() 
		{
			try 
			{
				string ProjectCode = this.txtProjectCode.Value;
				int IYear = BLL.ConvertRule.ToInt(this.txtYear.Value);
				string BudgetCode = this.txtBudgetCode.Value;

				//ȡ��Ʒ�����ֵ�
//				DataTable tbPBSType = BLL.SalBudgetRule.GetSalPBSType();

				//������ϸ
				EntityData entityDtl = DAL.EntityDAO.SalDAO.GetSalBudgetDtlByProjectYear(ProjectCode, IYear);

				//������ȼƻ���ϸ
				EntityData[] entityDtlY = new EntityData[2];
				entityDtlY[0] = DAL.EntityDAO.SalDAO.GetSalBudgetDtlByProjectYM(ProjectCode, IYear + 1, 0);
				entityDtlY[1] = DAL.EntityDAO.SalDAO.GetSalBudgetDtlByProjectYM(ProjectCode, IYear + 2, 0);

				foreach(Repeater dgList in arrdgList)
				{
					foreach(RepeaterItem item in dgList.Items)
					{
						HtmlInputHidden txtFieldName = (HtmlInputHidden)item.FindControl("txtFieldName");
						HtmlInputHidden txtPBSTypeCode = (HtmlInputHidden)item.FindControl("txtPBSTypeCode");

						string FieldName = txtFieldName.Value;
						string PBSTypeCode = txtPBSTypeCode.Value;

						//����
						for(int m=0;m<=12;m++)
						{
							HtmlInputText txt = (HtmlInputText)item.FindControl("txtM" + m.ToString());

							BLL.SalBudgetRule.SaveSalBudgetOneDtl(ProjectCode, BudgetCode, entityDtl, IYear, m, PBSTypeCode, FieldName, txt.Value);
						}

						//������ȼƻ�
						for(int y=1;y<=2;y++)
						{
							HtmlInputText txt = (HtmlInputText)item.FindControl("txtY" + y.ToString());

							BLL.SalBudgetRule.SaveSalBudgetOneDtl(ProjectCode, BudgetCode, entityDtl, IYear + y, 0, PBSTypeCode, FieldName, txt.Value);
						}
					}
				}

				/*
				foreach(string FieldName in BLL.SalBudgetRule.BudgetFields)
				{
					foreach(DataRow dr in tbPBSType.Rows) //��Ʒ����
					{
						string PBSTypeCode = dr["PBSTypeCode"].ToString();

						//����
						for(int m=0;m<=12;m++)
						{
							string InputName = "txtM" + m.ToString() + "_" + FieldName + "_0_" + PBSTypeCode;
							string val = Request.Form[InputName];

							BLL.SalBudgetRule.SaveSalBudgetOneDtl(ProjectCode, BudgetCode, entityDtl, IYear, m, PBSTypeCode, FieldName, val);
						}

						//������ȼƻ�
						for(int y=1;y<=2;y++)
						{
							string InputName = "txtY" + y.ToString() + "_" + FieldName + "_0_" + PBSTypeCode;
							string val = Request.Form[InputName];

							BLL.SalBudgetRule.SaveSalBudgetOneDtl(ProjectCode, BudgetCode, entityDtlY[y-1], IYear + y, 0, PBSTypeCode, FieldName, val);
						}
					}
				}
				*/

				DAL.EntityDAO.SalDAO.SubmitAllSalBudgetDtl(entityDtl);
				foreach(EntityData entity in entityDtlY) 
				{
					DAL.EntityDAO.SalDAO.SubmitAllSalBudgetDtl(entity);
				}

				entityDtl.Dispose();
				foreach(EntityData entity in entityDtlY) 
				{
					entity.Dispose();
				}

				/*
				//���浱��
				EntityData entity = DAL.EntityDAO.SalDAO.GetSalBudgetDtlByProjectYear(ProjectCode, IYear);
				for(int m=0;m<=12;m++) 
				{
					SaveOneYm(entity, IYear, m, "txtM" + m.ToString());
				}
				DAL.EntityDAO.SalDAO.SubmitAllSalBudgetDtl(entity);
				entity.Dispose();

				//�������
				for(int i=1;i<=2;i++) 
				{
					entity = DAL.EntityDAO.SalDAO.GetSalBudgetDtlByProjectYear(ProjectCode, IYear);
					int y = IYear + i;

					SaveOneYm(entity, y, 0, "txtY" + i.ToString());

					DAL.EntityDAO.SalDAO.SubmitAllSalBudgetDtl(entity);
					entity.Dispose();
				}
				*/
			}
			catch ( Exception ex )
			{
				throw ex;
			}
		}

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string Hint = "";
				if (!CheckValid(ref Hint)) 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
					return;
				}

//				Response.Write(Rms.Web.JavaScript.Alert(true, Request.Form["txtPBSTypeCode_HouseArea_0_PA000201"]));
				Save();
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "����ʧ�ܣ�" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return;
			}

			GoBack();
		}

		/// <summary>
		/// ����
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			if (this.txtRefreshScript.Value.Trim() != "")
			{
				Response.Write(string.Format("window.opener.{0}", this.txtRefreshScript.Value));
			}
			else 
			{
				Response.Write("window.opener.location = window.opener.location;");
			}
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}
	}
}
