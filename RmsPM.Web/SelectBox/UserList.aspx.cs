using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rms.ORMap;
using Rms.Web;
using RmsPM.DAL;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;

namespace RmsPM.Web.SelectBox
{
	/// <summary>
	/// UserList ��ժҪ˵����
	/// </summary>
	public partial class UserList : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (Request.QueryString["Type"] + "" == "Single")
			{
				this.dgList.Columns.Remove(this.dgList.Columns[this.dgList.Columns.Count-1]);
				this.dgList.Columns[2].Visible = false;
				this.tableButton.Visible = false;
			}
			else
			{
				this.dgList.Columns[1].Visible = false;
			}

			// �ڴ˴������û������Գ�ʼ��ҳ��
			if (!this.IsPostBack)
			{
				//���غ�����
				string ReturnFunc = Request.QueryString["ReturnFunc"] + "";
				if (ReturnFunc == "") 
				{
					ReturnFunc = "DoSelectUser";
				}
				ViewState["ReturnFunc"] = ReturnFunc;

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
		
		private void LoadData()
		{

			//��ʼ�����������������б�
			string unitCode = Request["UnitCode"]+"";
			string stationCode = Request["StationCode"]+"";

			try
			{
				EntityData entity;

				if (unitCode == "NoStationUser") 
				{
					//����δ���ڵ���Ա
					entity = BLL.SystemRule.GetUsersNoStation();
				}
				else 
				{
					//ȡ�����µ������û��������Ӳ��ţ�
					RmsPM.DAL.QueryStrategy.UserStrategyBuilder USB = new RmsPM.DAL.QueryStrategy.UserStrategyBuilder();
					if ( stationCode != "" ) 
						USB.AddStrategy( new Strategy (RmsPM.DAL.QueryStrategy.UserStrategyName.StationCode,stationCode));
					else if ( unitCode != "" ) 
						USB.AddStrategy( new Strategy (RmsPM.DAL.QueryStrategy.UserStrategyName.UnitCodeEx , unitCode));

					string s = USB.BuildMainQueryString();
					s = s + USB.GetDefaultOrder();
					QueryAgent QA = new QueryAgent();
					entity = QA.FillEntityData("SystemUser",s);
					QA.Dispose();
				}

				entity.Dispose();

				this.dgList.DataSource = entity.CurrentTable;
				this.dgList.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"������Ա�б�ʧ��");
			}
		}

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			StringBuilder strBuilder = new StringBuilder();

			foreach(DataGridItem oItem in this.dgList.Items)
			{
				System.Web.UI.WebControls.CheckBox chkPerson = (CheckBox)oItem.FindControl("chkPerson");
				string code = oItem.Cells[0].Text;
				if (chkPerson.Checked == true)
				{
					strBuilder.Append(code + "," );
				}
			}

			if (strBuilder.Length == 0)
			{
				Response.Write(JavaScript.ScriptStart);
				Response.Write("window.close();");
				Response.Write(JavaScript.ScriptEnd);
			}
			else
			{
				string Code = strBuilder.ToString().Remove(strBuilder.Length-1,1);
				Response.Write(JavaScript.ScriptStart);
				Response.Write("window.parent.opener.DoSelectUser('" + Code + "','1','"+Request.QueryString["Identity"]+"');");
				Response.Write("window.parent.close();");
				Response.Write(JavaScript.ScriptEnd);
			}
		}
	}
}
