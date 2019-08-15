<%@ Page language="c#" Inherits="RmsPM.Web.SelectBox.SelectCorp" CodeFile="SelectCorp.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>选择公司</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../images/checkbox.js"></SCRIPT>
		<script>
//单选
function doSelectCorp(code,name)
{
	window.opener.doSelectCorp(code,name);
	window.close();
}
		</script>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">选择公司</td>
				</tr>
				<tr>
					<td valign="top" align="center">
						<asp:DataList ID="dlCorp" Runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
							<ItemTemplate>
								<TABLE BORDER="0" CELLSPACING="5" CELLPADDING="5"  width=200>
									<TR>
										<TD nowrap>
											<a  class=choose  href="##" onclick="doSelectCorp(unitCode,unitName); return false;" unitCode='<%# DataBinder.Eval(Container, "DataItem.UnitCode") %>' 
											 unitName='<%# DataBinder.Eval(Container, "DataItem.UnitName") %>'>
												<%# DataBinder.Eval(Container, "DataItem.UnitName") %>
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
