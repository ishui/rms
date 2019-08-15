<%@ Page language="c#" Inherits="RmsPM.Web.SelectBox.SelectProjectCanAccess" CodeFile="SelectProjectCanAccess.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>选择项目</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../images/checkbox.js"></SCRIPT>
		<script>
//单选
function DoSelectProject(projectCode,projectName)
{
	window.opener.DoSelectProject(projectCode,projectName);
	window.close();
}

		</script>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">选择项目</td>
				</tr>
				<tr>
					<td valign="top" align="center">
						<asp:DataList ID="dlProject" Runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
							<ItemTemplate>
								<TABLE BORDER="0" CELLSPACING="5" CELLPADDING="5" width="200">
									<TR>
										<TD nowrap>
											<a class="choose" href="##" onclick="DoSelectProject(projectCode,projectName); return false;" projectCode='<%# DataBinder.Eval(Container, "DataItem.projectCode") %>' 
											 projectName='<%# DataBinder.Eval(Container, "DataItem.projectName") %>'>
												<%# DataBinder.Eval(Container, "DataItem.projectName") %>
											</a>
										</TD>
									</TR>
								</TABLE>
							</ItemTemplate>
						</asp:DataList>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
