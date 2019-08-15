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
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL;
using Rms.Web;
using RmsPM.BLL;

namespace RmsPM.Web.PBS
{
	/// <summary>
	/// Building_Step3 ��ժҪ˵����
	/// </summary>
	public partial class Building_Step3 : PageBase
	{
		
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage() 
		{
			try 
			{
				this.txtBuildingCode.Value = Request["BuildingCode"];
				this.txtDoorCount.Value = Request["DoorCount"];
				this.txtAct.Value = Request["Action"];

				//				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadData()
		{
			try
			{
				string BuildingCode = this.txtBuildingCode.Value;

				if (BuildingCode == "") 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "��¥�����"));
					return;
				}

				EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingByCode(BuildingCode);

				if (entity.HasRecord()) 
				{
						this.lblBuildingName.Text = entity.GetString("BuildingName");
						this.lblPBSTypeName.Text = BLL.PBSRule.GetPBSTypeFullName(entity.GetString("PBSTypeCode"));
					//						FloorCount = entity.GetInt("FloorCount");
//						this.lblFloorCount.Text = FloorCount.ToString();

					this.txtIsArea.Value = entity.GetInt("IsArea").ToString();
					this.txtProjectCode.Value = entity.GetString("ProjectCode");
					this.txtParentCode.Value = entity.GetString("ParentCode");
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "��¥��������"));
				}

				entity.Dispose();

				//ÿ�ݻ���
				int DoorCount = BLL.ConvertRule.ToInt(this.txtDoorCount.Value);
				if (DoorCount <= 0) 
				{
					DoorCount = 1;
				}

				this.dlBuild.DataSource = NewTableDoor(DoorCount);
				this.dlBuild.DataBind();

				//¥����
				DataTable tbFloor = (DataTable)Session["tbFloor"];

				int FloorCount = tbFloor.Rows.Count;
				for(int i=0;i<FloorCount;i++)
				{
					DataRow drFloor = tbFloor.Rows[i];

					string strList="";
					for(int k=0;k<DoorCount;k++)
					{
						strList=strList+"<td>&nbsp;</td>";
					}

					drFloor["No3"] = strList;
				}

				this.dlBuild1.DataSource = tbFloor;
				this.dlBuild1.DataBind();

			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ����" + ex.Message));
			}
		}

		private DataTable NewTableDoor(int DoorCount)
		{
			DataTable dt;

			if ((Session["tbDoor"] == null) || (this.txtAct.Value.ToLower() != "prev")) 
			{
				dt = new DataTable();	
				
				dt.Columns.Add(new DataColumn("DoorName", typeof(String)));
				dt.Columns.Add(new DataColumn("DoorCode", typeof(String)));
				dt.Columns.Add(new DataColumn("RoomCount", typeof(String)));
				
				DataRow dr = null;

				for(int i=0;i<DoorCount;i++)
				{
					dr = dt.NewRow();
					dr["DoorCode"]=i + 1;
					dr["RoomCount"] = 1;
					dt.Rows.Add(dr);
				}

				Session["tbDoor"] = dt;
			}
			else 
			{
				dt = (DataTable)Session["tbDoor"];
			}

			return dt;
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

		/// <summary>
		/// ��Ч�Լ��
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			DataTable tbTemp = new DataTable();
			tbTemp.Columns.Add("ChamberName", typeof(string));

			int iCount = this.dlBuild.Items.Count;
			for(int i=0;i<iCount;i++) 
			{
				HtmlInputText txtDoorName = (HtmlInputText)this.dlBuild.Items[i].FindControl("txtDoorName");
//				HtmlInputText txtDoorCode = (HtmlInputText)this.dlBuild.Items[i].FindControl("txtDoorCode");
				HtmlInputText txtRoomCount = (HtmlInputText)this.dlBuild.Items[i].FindControl("txtRoomCount");

                /* ���ƺŲ��ñ�¼ 
				if (txtDoorName.Value.Trim() == "") 
				{
					Hint = "���������ƺ�";
					return false;
				}
                */

				if (txtRoomCount.Value.Trim() == "") 
				{
					Hint = "������ÿ�ݻ���";
					return false;
				}

//				if (txtDoorCode.Value.Trim() == "") 
//				{
//					Hint = "�����뵥Ԫ��";
//					return false;
//				}

				if ( ! Rms.Check.StringCheck.IsNumber(txtRoomCount.Value))
				{
					Hint = "ÿ�ݻ�����������ֵ �� ";
					return false;
				}

				if (BLL.ConvertRule.ToInt(txtRoomCount.Value) <= 0)
				{
					Hint = "ÿ�ݻ����������0 �� ";
					return false;
				}

				//��¥�������ƺŲ����ظ�
                if (txtDoorName.Value != "")
                {
                    if (tbTemp.Select("ChamberName='" + txtDoorName.Value + "'").Length > 0)
                    {
                        Hint = string.Format("���ƺţ�{0}�������ظ� �� ", txtDoorName.Value);
                        return false;
                    }
                }

				//������ͬ�����ƺ�
//				if (BLL.ProductRule.IsChamberNameExists(txtDoorName.Value, "", this.txtProjectCode.Value))
//				{
//					Hint = string.Format("��ͬ�����ƺţ�{0}���Ѵ��� �� ", txtDoorName.Value);
//					return false;
//				}

				//��¼���ƺ�
				DataRow drTemp = tbTemp.NewRow();
				drTemp["ChamberName"] = txtDoorName.Value;
				tbTemp.Rows.Add(drTemp);
			}

			return true;
		}

		/// <summary>
		/// ��һ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSubmit_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string Hint = "";
				if (!CheckValid(ref Hint)) 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
					return;
				}

				string buildingCode = this.txtBuildingCode.Value;

				DataTable tb = (DataTable)Session["tbDoor"];

				int iCount = this.dlBuild.Items.Count;
				for(int i=0;i<iCount;i++) 
				{
					HtmlInputText txtDoorName = (HtmlInputText)this.dlBuild.Items[i].FindControl("txtDoorName");
//					HtmlInputText txtDoorCode = (HtmlInputText)this.dlBuild.Items[i].FindControl("txtDoorCode");
					HtmlInputText txtRoomCount = (HtmlInputText)this.dlBuild.Items[i].FindControl("txtRoomCount");

					DataRow dr = tb.Rows[i];
					dr["DoorName"] = txtDoorName.Value;
//					dr["DoorCode"] = txtDoorCode.Value;
					dr["RoomCount"] = txtRoomCount.Value;
				}

				Session["tbDoor"] = tb;

				//				EntityData entity = ProductDAO.GetBuildingByCode(buildingCode);
				//				dr = entity.CurrentRow;
				//				ProductDAO.UpdateBuilding(entity);
				//
				//				entity.Dispose();

				Response.Write(JavaScript.ScriptStart);
				Response.Write(string.Format("window.location.href='Building_Step4.aspx?BuildingCode={0}';", buildingCode));
				Response.Write(JavaScript.ScriptEnd);
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "����ʧ�ܣ�" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}
	}
}
