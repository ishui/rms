<%@ Page language="c#" Codefile="UnitSelectProject.aspx.cs" AutoEventWireup="false" Inherits="RmsPM.Web.Systems.UnitSelectProject" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>选择项目</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/Style.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body class="mainbody">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr height="30">
					<td vAlign="middle">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<TD><asp:label id="lbTitle" runat="server" CssClass="TitleText">选择项目</asp:label>——部门：<asp:label id="lblUnitName" runat="server" CssClass="Label"></asp:label></TD>
							</tr>
						</table>
					</td>
				</tr>
				<!--
				<tr height="30">
					<td vAlign="top">
						<table cellSpacing="0" borderColorDark="#6c7893" cellPadding="0" width="100%" bgColor="#e1e5f4"
							borderColorLight="#ffffff" border="1">
							<tr>
								<td noWrap borderColorLight="#e1e5f4" bgColor="#e6e8ed" borderColorDark="#e1e5f4" height="1"><IMG height="1" src="../images/SPACER.gif" width="5"></td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
							<tr>
								<td><img height="1" src="" width="1"></td>
							</tr>
						</table>
						<table height="22" cellSpacing="0" cellPadding="0" width="100%" bgColor="#e6e8ed" border="0">
							<tr>
								<td>
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td width="50"></td>
											<td>姓名</td>
											<td><input id="txtSearchOwnName" type="text" size="10" name="txtSearchOwnName" runat="server"></td>
											<td><input class="buttontop" id="btnSearch" type="button" value="搜索" name="btnSearch" runat="server"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="100%" bgColor="#6c7893" border="0">
							<tbody>
								<tr>
									<td><img height="1" src="" width="1"></td>
								</tr>
							</tbody>
						</table>
						<table cellSpacing="0" borderColorDark="#6c7893" cellPadding="0" width="100%" bgColor="#e1e5f4"
							borderColorLight="#ffffff" border="1">
							<tr>
								<td noWrap borderColorLight="#e1e5f4" bgColor="#e6e8ed" borderColorDark="#e1e5f4" height="1"><IMG height="1" src="images/SPACER.gif" width="5"></td>
							</tr>
						</table>
					</td>
				</tr>
				-->
				<tr height="100%">
					<td vAlign="top" borderColor="#6c7893" align="center">
						<DIV style="BORDER-RIGHT: 0px; PADDING-RIGHT: 0px; BORDER-TOP: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; OVERFLOW: auto; BORDER-LEFT: 0px; WIDTH: 100%; PADDING-TOP: 0px; BORDER-BOTTOM: 0px; HEIGHT: 100%">
							<table height="100%" cellSpacing="0" borderColorDark="#ffffff" cellPadding="0" width="100%"
								borderColorLight="#6c7893" border="1">
								<tr>
									<td vAlign="top"><asp:datagrid id="dgList" runat="server" CellPadding="2" GridLines="Horizontal" AutoGenerateColumns="False"
											AllowSorting="True" Width="100%" DataKeyField="ProjectCode" OnSelectedIndexChanged="dgList_SelectedIndexChanged">
											<ItemStyle CssClass="ListBodyTr"></ItemStyle>
											<HeaderStyle CssClass="ListHeadTr"></HeaderStyle>
											<FooterStyle CssClass="ListHeadTr"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="选择">
													<HeaderStyle Width="60px"></HeaderStyle>
													<ItemTemplate>
														<INPUT id="chk" type="checkbox" name="chk" runat="server">
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="ProjectName" HeaderText="项目名称"></asp:BoundColumn>
												<asp:BoundColumn DataField="City" HeaderText="城市"></asp:BoundColumn>
												<asp:BoundColumn DataField="Area" HeaderText="地区"></asp:BoundColumn>
												<asp:BoundColumn DataField="BlockName" HeaderText="地块"></asp:BoundColumn>
												<asp:BoundColumn DataField="TotalFloorSpace" HeaderText="总占地面积"></asp:BoundColumn>
											</Columns>
										</asp:datagrid>&nbsp;
									</td>
								</tr>
							</table>
						</DIV>
					</td>
				</tr>
				<tr>
					<td colSpan="2"><asp:label id="lblMessage" runat="server" ForeColor="Red"></asp:label></td>
				</tr>
				<TR height="50">
					<TD vAlign="middle" noWrap borderColorLight="#e1e5f4" align="center" borderColorDark="#e1e5f4"
						colSpan="2">
						<table id="Table5" cellSpacing="0" cellPadding="0" width="90%" border="0">
							<TR>
								<TD width="20%">&nbsp;</TD>
								<TD width="30%"><INPUT class="button" id="btnOK" type="button" value="确定" name="btnOK" runat="server"></TD>
								<TD width="20%">&nbsp;</TD>
								<TD width="30%"><INPUT class="button" onclick="window.close();" type="button" value="取消"></TD>
							</TR>
						</table>
					</TD>
				</TR>
			</table>
			<INPUT id="txtUnitCode" type="hidden" name="txtUnitCode" runat="server"><INPUT id="txtRefreshScript" type="hidden" name="txtRefreshScript" runat="server">
		</form>
	</body>
</HTML>
