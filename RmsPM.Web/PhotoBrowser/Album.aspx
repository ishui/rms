<%@ Register TagPrefix="uc1" TagName="PhotoBrowser" Src="PhotoBrowser.ascx" %>
<%@ Page language="c#" validateRequest=false Inherits="RmsPM.Web.PhotoBrowser.Album" CodeFile="Album.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
    <title>WebForm1</title>   
  </HEAD>
<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" style="background-color:#e4eff6">	
    <form id="Form1" method="post" runat="server">
    <table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">项目管理>工程进度>
									实景照片</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
    <uc1:PhotoBrowser id=PhotoBrowser1 runat="server"></uc1:PhotoBrowser>

    </form>
	<div>
	<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td background="../images/corl_bg.gif"><IMG height="12" src="../images/corl.gif" width="12"></td>
								<td width="12"><IMG height="12" src="../images/corr.gif" width="12"></td>
							</tr>
						</table>
	</div>
<!-- The script tag for dw_tooltip.js must be placed at the end of the document, 
     just before the close body tag. -->

	
  </body>
</HTML>
