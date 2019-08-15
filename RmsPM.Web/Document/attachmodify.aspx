<%@ Page language="c#" Inherits="RmsPM.Web.Document.AttachModify" CodeFile="AttachModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>附件修改</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/Style.css" type="text/css" rel="stylesheet">
		<script language="javascript">
	function GetLastRight(s, val) {
		var i = s.lastIndexOf(val);
		if (i > 0)
			return s.substring(i + 1);
		else
			return s;
			
	}
	
	function GetFirstFileName(s) {
		var val = ".";
		var i = s.lastIndexOf(val);
		
		if (i > 0)
			return s.substring(0, i);
		else
			return s;
			
	}
	
	function FileNameChange() {
		var title, filename;
		
		filename = GetLastRight(document.all.txtFileName.value, "\\");
		
		document.all.txtBody.innerText = filename;
		document.all.txtBody2.value = document.all.txtBody.innerText;
		
//		if (Form1.txtTitle.value == "")
//		{
			title = GetFirstFileName(filename);
			Form1.txtTitle.value = title;
//		}
	}
	
	function Download() {
		if (document.all.txtFileName.value == "")
		{
			var AttachmentCode = Form1.txtAttachmentCode.value;
			var w = screen.width;
			var h = screen.height;
			window.open("AttachView.aspx?from=session&AttachmentCode=" + AttachmentCode, "" , "top="+(screen.height-h)/2+",left="+(screen.width-w)/2+",width="+w+",height="+h+",scrollbars=1,resizable=1,status:no;");
		}
		else
		{
			window.open(document.all.txtFileName.value, "", "scrollbars=1");
		}
	}
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25" id="tdTitle"
						runat="server">文档修改</td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							<TR>
								<TD width="100" class="form-item">文件：</TD>
								<TD><asp:hyperlink id="txtBody" runat="server" NavigateUrl="javascript:Download();" Font-Underline="True"
										ForeColor="Blue"></asp:hyperlink><INPUT id="txtFileName" style="WIDTH: 32px; HEIGHT: 22px" type="file" onchange="FileNameChange();"
										size="1" name="txtName" runat="server"></TD>
							</TR>
							<TR>
								<TD width="100" class="form-item">标题：</TD>
								<TD><input id="txtTitle" class="input" type="text" size="40" name="txtTitle" runat="server"></TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%" cellspacing="10">
							<tr>
								<td align="center">
									<input id="btnSave" name="btnSave" type="button" class="submit" value="确 定" runat="server" onserverclick="btnSave_ServerClick">
									<input id="btnCancel" name="btnCancel" type="button" class="submit" value="取 消" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</TABLE>
			<INPUT id="txtAct" type="hidden" name="txtAct" runat="server"> <INPUT id="txtDocumentCode" type="hidden" name="txtDocumentCode" runat="server">
			<INPUT id="txtAttachmentCode" type="hidden" name="txtAttachmentCode" runat="server">
			<INPUT id="txtBody2" type="hidden" size="1" name="txtBody2" runat="server">
		</form>
	</body>
</HTML>
