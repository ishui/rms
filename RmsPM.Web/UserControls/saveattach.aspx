<%@ Page language="c#" Inherits="RmsPM.Web.UserControls.SaveAttach" CodeFile="SaveAttach.aspx.cs" %>
<%@ Register Assembly="RadUpload.Net2" Namespace="Telerik.WebControls" TagPrefix="radU" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>上传文件</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
		<meta name="CODE_LANGUAGE" Content="C#" />
		<meta name="vs_defaultClientScript" content="JavaScript" />
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
		<link href="../Images/index.css" type="text/css" rel="stylesheet" />
	</head>
	<body background="../Images/bg.jpg" height="25">
		<form id="Form1" method="post" runat="server">
		 <radU:RadUpload id="fileUpload" runat="server" initialfileinputscount="1" maxfileinputscount="8"
              Language="en-US"      controlobjectsvisibility="Default"   Skin ="default"/>
			<div>
						<asp:Label ID="lblHint" runat="server"></asp:Label><br />
						<input type="button" value="确定" class="button" id="btnOK" name="btnOK" runat="server" onserverclick="btnOK_ServerClick"/>
						<input type="button" value="取消" class="button" onclick="window.close();" />
			</div>		
			<input type="hidden" id="txtAttachMentCode" name="txtAttachMentCode" runat="server" />
	
		</form>
	</body>
</html>
