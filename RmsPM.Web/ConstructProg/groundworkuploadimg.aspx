<%@ Page language="c#" Inherits="RmsPM.Web.ConstructProg.GroundWorkUploadImg" CodeFile="GroundWorkUploadImg.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>上传平面图</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftmargin="0" topmargin="0" scroll="no">
		<form action="" id="Form1" name="Form1" enctype="multipart/form-data" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25" id="tdTitle"
						runat="server">上传平面图</td>
				</tr>
				<tr height="100%">
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<tr id="trBuildingName" runat="server">
								<td class="form-item" id="tdBuildingName" runat="server">楼栋：</td>
								<td><asp:Label id="lblBuildingName" Runat="server"></asp:Label></td>
							</tr>
							<tr>
								<td width="60" class="form-item">图片：</td>
								<td><input type="file" name="file1" class="input" runat="server" id="File1" style="WIDTH:100%"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%" cellspacing="10">
							<tr>
								<td align="center">
									<input id="btnSave" name="btnSave" type="button" class="submit" value="确 定" runat="server" onserverclick="btnSave_ServerClick">
									<input id="btnCancel" name="btnCancel" type="button" class="submit" value="取 消" onclick="window.close();">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input type="hidden" id="txtProjectCode" name="txtProjectCode" runat="server"> <input type="hidden" id="txtParentCode" name="txtParentCode" runat="server">
		</form>
	</body>
</HTML>
