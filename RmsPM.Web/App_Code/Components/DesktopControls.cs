	using System;
	using System.IO;
	using System.ComponentModel;
	using System.Configuration;
	using System.Collections;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using Rms.ORMap;

	namespace RmsPM.Web.Components
	{

		//*********************************************************************
		//
		// PortalModuleControl Class
		//
		// The PortalModuleControl class defines a custom base class inherited by all
		// desktop portal modules within the Portal.
		// 
		// The PortalModuleControl class defines portal specific properties
		// that are used by the portal framework to correctly display portal modules
		//
		//*********************************************************************
		#region 控件基类
		public class BaseControl : UserControl 
		{
			#region 读取当前用户信息
			private User _user = null;
			protected User user
			{
				get
				{
					GetUserMessage();
					return _user;
				}
			}

			public void GetUserMessage()
			{
				if ((Session["User"] == null) && (ConfigurationSettings.AppSettings["IsDebug"] == "1") && (ConfigurationSettings.AppSettings["IsDebug"] != ""))
				{
					Session["User"] = new User(ConfigurationSettings.AppSettings["DebugUser"]);
				}

				if ( Session["User"] != null )
				{
					this._user = (User)Session["User"];
				}
				else
				{
					//				string url = "..\\Default.aspx";

					//登录页面
					string server = Request.ServerVariables["server_name"];
					string dir = ConfigurationSettings.AppSettings["VirtualDirectory"];
					dir = dir.Replace("/", "");
					string url = string.Format("http://{0}/{1}/Default.aspx", server, dir);

					Response.Write( Rms.Web.JavaScript.ScriptStart);
					Response.Write (String.Format( @"  if ( window.parent == null ) window.open('{0}','a'); else  window.parent.open('{0}','a');  ", url) );
					//				Response.Write ( @"  if ( window.parent == null ) window.open('.\\Default.aspx'); else  window.parent.open('.\\Default.aspx');  " );
					//				Response.Write( Rms.Web.JavaScript.WinOpenMax(false,@"\Default.aspx",""));
					//				Response.Write( Rms.Web.JavaScript.WinClose(false));
					Response.Write ( @"  if ( window.parent == null ) { window.opener=null;  window.close() ; } else  { window.parent.opener = null; window.parent.close(); } " );
					Response.Write( Rms.Web.JavaScript.ScriptEnd);
					Response.End();
				}
			}
			#endregion
			#region 当前角色岗位
			public bool IsInRole(string id)
			{
				User myUser = new User(user.UserCode);
				if(myUser.HasOperationRight(id))// 080101为通知新增权限
					return true;
				else
					return false;
			}
			/// <summary>
			/// 取得指定用户的用户岗位
			/// </summary>
			/// <param name="strUser"></param>
			/// <returns></returns>
			public string GetUserStation(string strUser)
			{
				string strRole = "";
				try
				{
					EntityData entityRole = DAL.EntityDAO.OBSDAO.GetStationByUserCode(strUser);
					for(int i=0;i<entityRole.CurrentTable.Rows.Count;i++)
					{
						if(strRole.Length>1)  strRole+=",";
						strRole+=entityRole.CurrentTable.Rows[i]["StationCode"].ToString();
					}
				}
				catch(Exception ex)
				{
					ApplicationLog.WriteLog(this.ToString(),ex,"取得用户角色失败");
				}
				return strRole;
			}
			#endregion
		}
		#endregion
		#region 显示控件
		public class ShowControl : Control
		{
			private object _cacheTime=0;
			private string _cacheKey;
			private string _cachedOutput = "";
			private object _controlSrc;		


			public int CacheTime
			{
				get
				{
					return Convert.ToInt32(_cacheTime);
				}
			}
			public string CacheKey
			{
				get
				{
					return _controlSrc.ToString()+_cacheTime.ToString();
				}
				set
				{
					_cacheKey=value;
				}
			}

			public string ControlSrc
			{
				get
				{
					return _controlSrc.ToString();
				}
				set
				{
					_controlSrc=value;
				}
			}
			public ShowControl(object controlSrc,object cacheTime ):base()
			{
				_cacheTime = cacheTime;
				_controlSrc = controlSrc;				
			}
			public ShowControl() : base()
			{
			}
			protected override void CreateChildControls() 
			{

				if (CacheTime > 0) 
				{
					_cachedOutput = (String) Context.Cache[CacheKey];
					if (_cachedOutput == null) 
					{
						CreateMyControl();
					}
				}
				else
				{
					CreateMyControl();
				}
				
			}
			protected void CreateMyControl()
			{
				//base.CreateChildControls();
				BaseControl myControl = (BaseControl)Page.LoadControl(ControlSrc);
				this.Controls.Add(myControl);
			}
			protected override void Render(HtmlTextWriter output) 
			{

				// If no caching is specified, render the child tree and return 

				if (CacheTime == 0) 
				{
					base.Render(output);
					return;
				}
				else
				{
			
					if (_cachedOutput == null) 
					{

						TextWriter tempWriter = new StringWriter();
						base.Render(new HtmlTextWriter(tempWriter));
						_cachedOutput = tempWriter.ToString();

						Context.Cache.Insert(CacheKey, _cachedOutput, null, DateTime.Now.AddSeconds(CacheTime), TimeSpan.Zero);
					}

					// Output the user control's content

					output.Write(_cachedOutput);
				}
			}		
		}
		#endregion 
	}



