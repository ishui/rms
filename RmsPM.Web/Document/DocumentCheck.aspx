<%@ Page language="c#" Inherits="RmsPM.Web.Document.DocumentCheck" CodeFile="DocumentCheck.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>文档审核</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">文档审核</td>
				</tr>
				<tr >
					<td class="topic" vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr id="trOpinion" style="DISPLAY: none"  runat="server">
								<TD class="form-item" width="100">审核意见：</TD>
								<TD><textarea class="textarea" id="txtCheckOpinion" style="WIDTH: 100%" name="txtCheckOpinion"
										rows="5" runat="server"></textarea>
								</TD>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
    						<table cellPadding="0" width="100%" cellspacing="9">
	    						<tr>
	    						    <td>确定审核通过吗？</td>
	    						</tr> 
	    					</table>
						</div>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="10" width="100%">
							<tr>
								<td align="center"><input class="submit" id="btnSave" onclick="if (!Check()) return false;"
										type="button" value="确 定" name="btnSave" runat="server" onserverclick="btnSave_ServerClick"> <input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="取 消"
										name="btnCancel">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtDocumentCode" type="hidden" name="txtDocumentCode" runat="server">
		</form>
		<script language="javascript">
//审核
function Check()
{
	return true;
}
		</script>
	</body>
</HTML>
