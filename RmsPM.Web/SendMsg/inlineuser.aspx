<%@ Page language="c#" Inherits="RmsPM.Web.SendMsg.InlineUser" CodeFile="InlineUser.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>InlineUser</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span id="spanTitle" runat="server">&nbsp;在线用户</span></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="table" vAlign="top" align="center"><FONT face="宋体">
							<asp:datagrid id="dgList" runat="server" Width="100%" PageSize="15" AutoGenerateColumns="False"
								GridLines="Horizontal" CellPadding="0" CssClass="list" AllowPaging="True" OnPageIndexChanged="dgList_PageIndexChanged">
								<HeaderStyle CssClass="list-title"></HeaderStyle>
								<FooterStyle CssClass="list-title"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="用户名">
										<ItemTemplate>
											<a href="#" onclick="javascript:gourl('<%# DataBinder.Eval(Container, "DataItem.Key") %>');return false;"><%# DataBinder.Eval(Container, "DataItem.Value") %></a>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
									CssClass="ListHeadTr" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></FONT>
					</td>
				</tr>
				<tr>
					<td height="12">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td background="../images/corl_bg.gif"><IMG height="12" src="../images/corl.gif" width="12"></td>
								<td width="12"><IMG height="12" src="../images/corr.gif" width="12"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
			</table>
		</form>
		<script>
	function gourl ( usercode )
	{
	var newcodes=usercode.split(',');
	
		OpenLargeWindow('../SendMsg/SendMsgModify.aspx?UserCode='+newcodes[0],'发送消息');
		
	}
	
		</script>
	</body>
</HTML>
