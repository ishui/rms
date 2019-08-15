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
	/// Building_Step2 ��ժҪ˵����
	/// </summary>
	public partial class Building_Step2 : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtPprojectCode;
	
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
				this.txtAct.Value = Request["Action"];
				this.txtDoorCount.Value = Request["DoorCount"];

				if (BLL.ConvertRule.ToInt(this.txtDoorCount.Value) <=0) 
				{
					this.txtDoorCount.Value = "1";
				}

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
				int FloorCount = 0;

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
					FloorCount = entity.GetInt("IFloorCount");
					this.lblFloorCount.Text = FloorCount.ToString();

					this.txtIsArea.Value = entity.GetInt("IsArea").ToString();
					this.txtProjectCode.Value = entity.GetString("ProjectCode");
					this.txtParentCode.Value = entity.GetString("ParentCode");
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "��¥��������"));
				}

				entity.Dispose();

				this.dlBuild.DataSource = NewTableFloor(FloorCount);
				this.dlBuild.DataBind();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ����" + ex.Message));
			}
		}

		private DataTable NewTableFloor(int IFloorCount)
		{
			DataTable dt;

			if ((Session["tbFloor"] == null) || (this.txtAct.Value.ToLower() != "prev")) 
			{
				dt = new DataTable();	
				
				dt.Columns.Add(new DataColumn("FloorCode", typeof(String)));
				dt.Columns.Add(new DataColumn("FloorName", typeof(String)));
				dt.Columns.Add(new DataColumn("No3", typeof(String)));
				dt.Columns.Add(new DataColumn("No4", typeof(String)));
				
				DataRow dr=null;

				int FloorCount = Math.Abs(IFloorCount);
				for(int i=FloorCount;i>=1;i--)
				{
					dr = dt.NewRow();
					dr["FloorCode"]=i;
					dr["FloorName"]=i;
					dt.Rows.Add(dr);
				}

				Session["tbFloor"] = dt;
			}
			else 
			{
				dt = (DataTable)Session["tbFloor"];
			}

			return dt;
//			dv = new DataView(dt);
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

			if (this.txtDoorCount.Value.Trim() == "") 
			{
				Hint = "������¥����";
				return false;
			}

			if ( txtDoorCount.Value != "" )
			{
				if ( ! Rms.Check.StringCheck.IsNumber(txtDoorCount.Value))
				{
					Hint = "¥������������ֵ �� ";
					return false;
				}

				if (BLL.ConvertRule.ToInt(txtDoorCount.Value) <= 0)
				{
					Hint = "¥�����������0 �� ";
					return false;
				}
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
				int DoorCount = BLL.ConvertRule.ToInt(this.txtDoorCount.Value);

				DataTable tb = (DataTable)Session["tbFloor"];

				int iCount = this.dlBuild.Items.Count;
				for(int i=0;i<iCount;i++) 
				{
					HtmlInputText txtFloorName = (HtmlInputText)this.dlBuild.Items[i].FindControl("txtFloorName");
					DataRow dr = tb.Rows[i];
					dr["FloorName"] = txtFloorName.Value;
				}

				Session["tbFloor"] = tb;

			//				EntityData entity = ProductDAO.GetBuildingByCode(buildingCode);
			//				dr = entity.CurrentRow;
			//				ProductDAO.UpdateBuilding(entity);
			//
			//				entity.Dispose();

				Response.Write(JavaScript.ScriptStart);
				Response.Write(string.Format("window.location.href='Building_Step3.aspx?BuildingCode={0}&DoorCount={1}';", buildingCode, DoorCount));
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
