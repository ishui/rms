<%@ Page language="c#" Inherits="RmsPM.Web.PBS.showFixWindow" CodeFile="showFixWindow.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>查看房型图</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
        <script>
			function sizeDialog()
			{
				dialogHeight = (parseInt(layout.offsetHeight) +35) 
				dialogWidth = (parseInt(layout.offsetWidth) +30) 
				window.resizeTo(dialogWidth, dialogHeight)
				
			}
			
			function iniBody()
			{
				window.setTimeout("sizeDialog()",1);
			}
		</script>
  </head>
  <body leftMargin="0" topMargin="0" onload="iniBody();">
	
    <form id="Form1" method="post" runat="server">
<table id="layout" border="0" align="center">
			<tr>
				<td id="tdList" valign="top"><img src=ShowPicture.aspx?fileid=<%=Request["FileID"]%> border=0 alt=></td>

			</tr>
		</table>
     </form>
	
  </body>
</html>
