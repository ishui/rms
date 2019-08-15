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

namespace RmsPM.Web.PBS
{
	/// <summary>
	/// PBSModify 的摘要说明。
	/// </summary>
	public partial class PBSModify : PageBase
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
//				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
				this.txtPBSTypeCode.Value = Request.QueryString["PBSTypeCode"];
				this.txtParentCode.Value = Request.QueryString["ParentCode"];
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtAct.Value = Request.QueryString["Action"];

				//新增时必须传入项目代码
				if ((this.txtPBSTypeCode.Value == "") && (this.txtProjectCode.Value == ""))
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "无项目代码，不能新增"));
					return;
				}

				if (this.txtPBSTypeCode.Value == "") 
				{
					//新增时，产品组合只能到二级
					if (this.txtParentCode.Value != "") 
					{
						EntityData entity = DAL.EntityDAO.PBSDAO.GetPBSTypeByCode(this.txtParentCode.Value);
						if (entity.HasRecord()) 
						{
							if (entity.GetInt("deep") > 1) 
							{
								Response.Write(Rms.Web.JavaScript.ScriptStart);
								Response.Write(Rms.Web.JavaScript.Alert(false, "产品组合只能到二级，不能继续新增"));
								Response.Write(Rms.Web.JavaScript.WinClose(false));
								Response.Write(Rms.Web.JavaScript.ScriptEnd);
							}
						}
						else 
						{
							Response.Write(Rms.Web.JavaScript.Alert(true, "产品组合不存在"));
							return;
						}
						entity.Dispose();
					}
				}

//				if (this.txtPBSTypeCode.Value == "") 
//				{
//					this.btnDelete.Visible = false;
//				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadData()
		{
			try
			{
				string code = this.txtPBSTypeCode.Value;
				if (code != "") 
				{
					EntityData entity = RmsPM.DAL.EntityDAO.PBSDAO.GetPBSTypeByCode(code);
					if (entity.HasRecord())
					{
						this.txtPBSTypeName.Value = entity.GetString("PBSTypeName");
						this.txtDescription.Value = entity.GetString("Description");
						this.txtParentCode.Value = entity.GetString("ParentCode");
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "产品组合不存在"));
					}
					entity.Dispose();
				}

				this.lblParentName.Text = BLL.PBSRule.GetPBSTypeName(this.txtParentCode.Value);
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示出错：" + ex.Message));
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

		}
		#endregion

		/// <summary>
		/// 保存
		/// </summary>
		/// <param name="code"></param>
		/// <param name="parentCode"></param>
		private void SavaData(string code,string parentCode)
		{
			try
			{				
				string curCode="";

				int deep=0;
				string fullID="";
				int sortID=0;

				if (parentCode.Length>0)
				{
				
					EntityData entityParent=PBSDAO.GetPBSTypeByCode(parentCode);		
					if (entityParent.HasRecord())
					{
						deep=entityParent.GetInt("Deep");
						fullID=entityParent.GetString("fullID");
					}
					entityParent.Dispose();

				}
				

				EntityData entity=RmsPM.DAL.EntityDAO.PBSDAO.GetPBSTypeByCode(code);
				DataRow dr=null;
				bool IsNew=false;

				if (entity.HasRecord())
				{
					IsNew=false;
					dr=entity.CurrentRow;

				}
				else
				{
					dr=entity.GetNewRecord();
					IsNew=true;
					curCode=SystemManageDAO.GetNewSysCode("PBSTypeCode");
					sortID=PBSDAO.GetPBSTypeMaxSortID(this.txtProjectCode.Value,parentCode);
					dr["PBSTypeCode"]=curCode;
					dr["Deep"]=deep+1;
					if (fullID.Length>0)
					{
						dr["FullID"]=fullID+"-"+curCode;
					}
					else
					{
						dr["FullID"]=curCode;
					}
					dr["ParentCode"]=parentCode;
					dr["ProjectCode"]=this.txtProjectCode.Value;
					dr["SortID"]=sortID+1;
				}
				
				
				dr["PBSTypeName"] = this.txtPBSTypeName.Value;
				dr["Description"] = this.txtDescription.Value;
				
				if ( IsNew )
				{
					entity.AddNewRecord(dr);
					PBSDAO.InsertPBSType(entity);
					
				}
				else
				{	
					PBSDAO.UpdatePBSType(entity);
					
				}

				entity.Dispose();
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// 有效性检查
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			if (this.txtPBSTypeName.Value.Trim() == "") 
			{
				Hint = "请输入名称";
				return false;
			}

			return true;
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

				string parentCode = this.txtParentCode.Value;
				string code = this.txtPBSTypeCode.Value;
				SavaData(code,parentCode);
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "保存失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return;
			}

			GoBack();
		}

		/// <summary>
		/// 返回
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write("window.opener.location = window.opener.location;");
			Response.Write(Rms.Web.JavaScript.WinClose(false));
//			string FromUrl = this.txtFromUrl.Value.Trim();
//			if (FromUrl != "") 
//			{
//				Response.Write(string.Format("window.location = '{0}';", FromUrl));
//			}
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string code = this.txtPBSTypeCode.Value;
				BLL.PBSRule.DeletePBSType(code);
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "删除失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}

			GoBack();
		}

	}
}
