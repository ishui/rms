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
using Rms.Web;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;

namespace RmsPM.Web.ConstructProg
{
	/// <summary>
	/// GroundWorkLocation ��ժҪ˵����
	/// </summary>
	public partial class GroundWorkLocation : PageBase
	{
		protected System.Web.UI.WebControls.DataList dlBuild;
		protected System.Web.UI.HtmlControls.HtmlImage imgMain;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPprojectCode;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				IniPage();
				LoadChart();
			}
		}

		private void IniPage() 
		{
			try 
			{
				this.txtGroundWorkCode.Value = Request.QueryString["GroundWorkCode"];
				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
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
			this.dgList.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.dgList_ItemDataBound);

		}
		#endregion

		private void LoadChart() 
		{
			try 
			{
				string GroundWorkCode = this.txtGroundWorkCode.Value;

				if (GroundWorkCode != "") 
				{
					EntityData entity = DAL.EntityDAO.ConstructDAO.GetGroundWorkByCode(GroundWorkCode);
					if (entity.HasRecord()) 
					{
						this.txtWBSCode.Value = entity.GetString("WBSCode");
						this.txtProjectCode.Value = entity.GetString("ProjectCode");

						//��ʾ����ͼ
                        EntityData entityA = RmsPM.DAL.EntityDAO.DAOFactory.GetAttachmentDAO().GetAttachMentByMasterCode("GroundWork", this.txtWBSCode.Value);
						if (entityA.HasRecord()) 
						{
							string AttachMentCode = entityA.GetString("AttachMentCode");
							this.imgBg.Src = "../Project/ShowAttachPicture.aspx?AttachMentCode=" + AttachMentCode;
						}
						entityA.Dispose();
					}
					entity.Dispose();
				}

				LoadChartDtl();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ʾͼ��ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾͼ��ʧ�ܣ�" + ex.Message));
			}
		}

		/// <summary>
		/// ��ʾ����ͼ
		/// </summary>
		private void LoadChartDtl()
		{
			try 
			{
				string WBSCode = this.txtWBSCode.Value;

				DataTable tb = new DataTable();
				tb.Columns.Add("WBSCode");
				tb.Columns.Add("TaskName");
				tb.Columns.Add("AttachMentCode");
				tb.Columns.Add("ImageName");
				tb.Columns.Add("Status", typeof(int));
				tb.Columns.Add("CompletePercent", typeof(int));
				tb.Columns.Add("CompleteFlag", typeof(int));
				tb.Columns.Add("Color");

				tb.Columns.Add("ObjectX", typeof(int));
				tb.Columns.Add("ObjectY", typeof(int));

				int defaultX = 20;
				int defaultY = 20;

				//��1����������Ϊ��������
				EntityData entity = DAL.EntityDAO.WBSDAO.GetChildTask(WBSCode);
				foreach(DataRow dr in entity.CurrentTable.Rows)
				{
					string aWBSCode = dr["WBSCode"].ToString();

					DataRow drNew = tb.NewRow();

					drNew["WBSCode"] = dr["WBSCode"];
					drNew["TaskName"] = dr["TaskName"];

					//״̬
					drNew["Status"] = dr["Status"];
					drNew["CompletePercent"] = dr["CompletePercent"];

					//ȡͼƬ
                    EntityData entityAttach = RmsPM.DAL.EntityDAO.DAOFactory.GetAttachmentDAO().GetAttachMentByMasterCode("GroundWork", aWBSCode);
					if (entityAttach.HasRecord()) 
					{
						drNew["AttachMentCode"] = entityAttach.GetString("AttachMentCode");
						drNew["ImageName"] = entityAttach.GetString("FileName");
					}

					int x = 0;
					int y = 0;

					//ȡ�����λ��
					EntityData entityG = DAL.EntityDAO.ConstructDAO.GetGroundWorkByWBSCode(aWBSCode);
					if (entityG.HasRecord()) 
					{
						x = entityG.GetInt("ObjectX");
						y = entityG.GetInt("ObjectY");
					}
					else 
					{
						//����ȱʡλ��
						x = defaultX;
						y = defaultY;

						defaultX = x;
						defaultY = y + 40;
					}
					entityG.Dispose();

					drNew["ObjectX"] = x;
					drNew["ObjectY"] = y;

					tb.Rows.Add(drNew);
				}
				entity.Dispose();

				this.dgList.DataSource = tb;
				this.dgList.DataBind();

				this.dgList2.DataSource = tb;
				this.dgList2.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
			}
		}

		/// <summary>
		/// ����λ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				string GroundWorkCode = this.txtGroundWorkCode.Value;
				string location = this.txtSaveLocation.Value;

				EntityData entity = DAL.EntityDAO.ConstructDAO.GetGroundWorkByParentCode(GroundWorkCode);

				string[] arrL = location.Split("$".ToCharArray());
				for (int i=0;i<arrL.Length;i++)
				{
					if (arrL[i].Length>0)
					{
						string[] arrB = arrL[i].Split("|".ToCharArray());
						string WBSCode = arrB[0];
						int x = BLL.ConvertRule.ToInt(arrB[1]);
						int y = BLL.ConvertRule.ToInt(arrB[2]);

						DataRow drNew = null;
						DataRow[] drs = entity.CurrentTable.Select("WBSCode='" + WBSCode + "'");
						if (drs.Length > 0) 
						{
							drNew = drs[0];
						}
						else 
						{
							drNew = entity.CurrentTable.NewRow();

							drNew["GroundWorkCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("GroundWorkCode");
							drNew["WBSCode"] = WBSCode;
							drNew["ProjectCode"] = this.txtProjectCode.Value;
							drNew["ParentCode"] = GroundWorkCode;

							entity.CurrentTable.Rows.Add(drNew);
						}

						drNew["ObjectX"] = x;
						drNew["ObjectY"] = y;
					}
				}

				DAL.EntityDAO.ConstructDAO.SubmitAllGroundWork(entity);
				entity.Dispose();
			}
			catch(Exception ex) 
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�������" + ex.Message));
				return;
			}

			GoBack();
		}

		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			try 
			{
				string AttachMentCode = ((HtmlInputHidden)e.Item.FindControl("txtAttachMentCode")).Value;
//				string Color = ((HtmlInputHidden)e.Item.FindControl("txtColor")).Value;

				HtmlImage img = (HtmlImage)e.Item.FindControl("imgItem");

				if (AttachMentCode != "") 
				{
					img.Src = "../Project/ShowAttachPicture.aspx?AttachMentCode=" + AttachMentCode;
//					img.Style["background-color"] = Color;
					img.Style["display"] = "";
				}
				else 
				{
					img.Style["display"] = "none";
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
			}
		}

		/// <summary>
		/// ����
		/// </summary>
		private void GoBack() 
		{
			string s = Rms.Web.JavaScript.ScriptStart
				+ "GoBack();"
				+ Rms.Web.JavaScript.ScriptEnd;
			this.RegisterStartupScript("start", s);

//			if (this.txtFromUrl.Value != "") 
//			{
//				Response.Write(string.Format("window.location = '{0}';", this.txtFromUrl.Value));
//			}
//			else 
//			{
//				Response.Write("window.history.go(-1);");
//			}
//			Response.Write(Rms.Web.JavaScript.WinClose(false));
		}

	}
}
