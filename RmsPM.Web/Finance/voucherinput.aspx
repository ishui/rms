<%@ Page language="c#" Inherits="RmsPM.Web.Finance.VoucherInput" CodeFile="VoucherInput.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>凭证导入</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="white"
				id="tableFull">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">从Excel导入付款凭证</td>
				</tr>
				<tr>
					<td height="25" class="note">注意：使用从用友财务软件导出的凭证文件，格式使用文本文件&nbsp;
					</td>
				</tr>
				<tr height="100%">
					<td vAlign="top" align="center">
						<iframe src="../Cost/SavingWating.htm" style="DISPLAY:none" id="iframeSave" frameBorder="no"
							width="100%" scrolling="auto" height="100%"></iframe>
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form" id="tableMain">
							<tr>
								<td class="form-item">文件：</td>
								<td><input type="file" class="textbox" id="txtFile" name="txtFile" style="WIDTH: 269px; HEIGHT: 21px"
										size="25" runat="server"><font color="red">*</font></td>
							</tr>
							<tr>
								<td class="form-item">请款单类型：</td>
								<TD>
									<uc1:InputSystemGroup id="inputSystemGroupPayment" runat="server"></uc1:InputSystemGroup><font color="red">*</font>
								</TD>
							</tr>
							<tr>
								<td class="form-item">付款单类型：</td>
								<TD>
									<uc1:InputSystemGroup id="inputSystemGroupPayout" runat="server"></uc1:InputSystemGroup><font color="red">*</font>
								</TD>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%" cellspacing="10" id="tableButton">
							<tr>
								<td align="center">
									<input id="btnSave" name="btnSave" type="button" class="submit" value="确 定" runat="server"
										onclick="doSave();" onserverclick="btnSave_ServerClick"> <input id="btnCancel" name="btnCancel" type="button" class="submit" value="取 消" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
		<script language="javascript">
<!--

	function doSave()
	{
		document.all("iframeSave").style.display = "";
		document.all("tableMain").style.display = "none";
		document.all("tableButton").style.display = "none";
	}


//-->
		</script>
	</body>
</HTML>
