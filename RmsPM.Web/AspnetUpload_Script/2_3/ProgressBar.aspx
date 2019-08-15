<%@ Page Language="c#" %>
<%@ Import Namespace="Bestcomy.Web.Controls.Upload" %>
<HTML>
	<HEAD runat="server">
		<title>
			<%=Percentage%>
			% completed</title>
		<base target="_self">
		<style>
    BODY { MARGIN: 0px; OVERFLOW: hidden; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: buttonface; BORDER-BOTTOM-STYLE: none }
    TD { FONT-SIZE: 11pt; FONT-FAMILY: Arial }
    </style>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table border="0" cellpadding="5" width="100%">
				<tr>
					<td align="center" valign="middle" height="100%">
						<table border="0" width="340">
							<tr>
								<td nowrap>Status:&nbsp;<asp:Label ID="txt_progressinfo" Runat="server" /></td>
							</tr>
							<tr>
								<td nowrap>File:&nbsp;<asp:Label ID="txt_filename" style="TEXT-OVERFLOW:ellipsis" Runat="server" Width="300" /></td>
							</tr>
						</table>
						<table border="0" cellspacing="1" cellpadding="0" style="BORDER-RIGHT:buttonhighlight 1px solid; BORDER-TOP:buttonshadow 1px solid; BORDER-LEFT:buttonshadow 1px solid; WIDTH:340px; BORDER-BOTTOM:buttonhighlight 1px solid; HEIGHT:22px">
							<tr>
								<td width="<%=Percentage%>%" bgcolor="highlight"></td>
								<td width="<%=100-Percentage%>%"></td>
							</tr>
						</table>
						<table border="0" cellspacing="0" cellpadding="1" width="340">
							<tr>
								<td nowrap>Transfer Rate:&nbsp;<asp:Label ID="txt_speed" Runat="server" /></td>
							</tr>
							<tr>
								<td nowrap>Time Remaining:&nbsp;<asp:Label ID="txt_leftTime" Runat="server" /></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="right" nowrap><asp:Button ID="btn_ok" Text="OK" Runat="server" Width="70" />&nbsp;&nbsp;<asp:Button ID="btn_cancle" Text="Cancel" OnClick="btn_cancle_Click" Runat="server" Width="70" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
<script language="c#" runat="server">

		private double percentage = 0;

		public double Percentage
		{
			get
			{
				return percentage;
			}
		}
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			
			btn_ok.Enabled=false;
			Progress proc = new Progress(Request["UploadID"]);
			if(proc.get_UploadStatus()!=Progress.UploadStatusEnum.Error)
			{
				txt_progressinfo.Text = proc.get_UploadStatus().ToString();
				txt_filename.Text = proc.get_FileName();
				percentage = proc.get_Percent();
				if(proc.get_UploadStatus() == Progress.UploadStatusEnum.Initializing)
				{
					Response.AppendHeader("Refresh","3");
				}
				else if(proc.get_UploadStatus() == Progress.UploadStatusEnum.Completed)
				{
					txt_filename.Text = proc.get_FileCount().ToString()+" file(s) uploaded successfully!";
					txt_speed.Text = proc.GetFormatString(proc.get_Speed())+"/Sec";
					txt_leftTime.Text = "Upload completely.";
					proc.Dispose();
					btn_ok.Attributes.Add("onclick","javascript:window.opener=self;window.close();return false;");
					btn_ok.Enabled=true;
				}
				else
				{				
					txt_speed.Text = proc.GetFormatString(proc.get_Speed())+"/Sec";
					txt_leftTime.Text = proc.get_LeftTime().ToString();
					Response.AppendHeader("Refresh","3");
				}
				if(proc.get_UploadStatus() == Progress.UploadStatusEnum.Completed)
					btn_cancle.Attributes.Add("onclick","javascript:window.opener=self;window.close();return false;");
				else
					btn_cancle.Attributes.Add("onclick","javascript:var win=null;if(window.dialogArguments==undefined){win=window.opener;}else{win=dialogArguments;}win.document.location.href=win.document.location.href;return true;");
			}
			else
			{
				proc.Dispose();
				if(!ClientScript.IsClientScriptBlockRegistered(this.GetType(),"closeScript"))
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "closeScript", System.Web.HttpUtility.HtmlDecode("&lt;script&gt;window.opener=self;window.close();&lt;/script&gt;"));
			}
		}
		
		protected void btn_cancle_Click(object sender, System.EventArgs e)
		{
			Progress proc = new Progress(Request["UploadID"]);
			proc.Abort();
			btn_cancle.Enabled = false;
		}
		
</script>
