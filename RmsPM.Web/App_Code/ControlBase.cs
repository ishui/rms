using System;
using System.Configuration;
using System.Web;

namespace RmsPM.Web
{
	/// <summary>
	/// ControlBase 的摘要说明。
	/// </summary>
	public class ControlBase : System.Web.UI.UserControl
	{

		public ControlBase()
		{
		}

		override protected void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.InitEventHandler();
		}

		virtual protected void InitEventHandler()
		{
		}
	}
}