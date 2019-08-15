<%@ Register TagPrefix="cc2" Namespace="RmsPM.WebControls.ToolsBar" Assembly="RmsPM.WebControls" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Systems.Dictionary" CodeFile="Dictionary.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Dictionary</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span id="spanTitle" runat="server">
										系统管理&nbsp;- 字典管理</span></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="table" vAlign="top" align="center">
						<table border="0" cellspacing="0" cellpadding="5" width="80%" id="tableInput" runat="server">
							<TR>
								<td align="right">源项目：<input type="text" id="txtInputProjectName" runat="server" class="input">
									<input id="btnInput" onclick="javascript:if(!window.confirm('导入字典项将清除本项目中原来的字典项，您确定吗 ？')) return false;"
										type="button" value="导入字典" runat="server" class="submit" onserverclick="btnInput_ServerClick"></td>
							</TR>
						</table>
						<br>
						<table style="BORDER-COLLAPSE: collapse" borderColor="#3366cc" cellSpacing="0" cellPadding="5"
							width="80%" height="540" align="center" border="1">
							<TR height="30">
								<TD align="center" width="25%">字典名</TD>
								<TD align="center">字典项：<br>
									<asp:Label ID="lblReMark" Runat="server"></asp:Label></TD>
							</TR>
							<tr bgColor="#fdfdfd" valign="top">
								<td vAlign="top" align="center" width="25%">
									<div style="OVERFLOW: auto;HEIGHT: 100%">
										<asp:datagrid id="dgName" runat="server" CellPadding="2" CellSpacing="2" BorderStyle="None" BorderWidth="0"
											GridLines="Horizontal" AutoGenerateColumns="False" PageSize="15" Width="100%" AllowPaging="False" onselectedindexchanged="dgName_SelectedIndexChanged">
											<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="DictionaryNameCode"></asp:BoundColumn>
												<asp:ButtonColumn DataTextField="Name" HeaderText="" CommandName="Select" DataTextFormatString="{0}">
													<HeaderStyle Width="40px"></HeaderStyle>
												</asp:ButtonColumn>
											</Columns>
											<PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid>
									</div>
								</td>
								<td vAlign="top" align="left" bgColor="#fdfdfd"><BR>
									<table cellpadding="0" cellspacing="0" height="100%" width="100%">
										<tr>
											<td>
												<table height="20" cellSpacing="0" cellPadding="5" width="95%" border="0">
													<tr>
														<td><asp:textbox id="txtName" runat="server" CssClass="input"></asp:textbox>&nbsp;
															<asp:ImageButton id="btnAddNew" runat="server" ImageUrl="../Images/Add.gif" BorderWidth="0" BorderStyle="None"
																AlternateText="新增" Visible="False"></asp:ImageButton>
															&nbsp;&nbsp;<asp:ImageButton id="btnSave" runat="server" ImageUrl="../Images/ToolsItemSave.gif" BorderWidth="0"
																BorderStyle="None" AlternateText="保存" Visible="False"></asp:ImageButton>
															&nbsp;&nbsp;<asp:ImageButton id="btnUp" runat="server" ImageUrl="../Images/Arrow_up.gif" BorderWidth="0" BorderStyle="None"
																AlternateText="上移" Visible="False"></asp:ImageButton>
															&nbsp;&nbsp;<asp:ImageButton id="btnDown" runat="server" ImageUrl="../Images/Arrow_down.gif" BorderWidth="0"
																BorderStyle="None" AlternateText="下移" Visible="False"></asp:ImageButton>
															&nbsp;&nbsp;<asp:ImageButton id="btnDelete" runat="server" ImageUrl="../Images/Del.gif" BorderWidth="0" BorderStyle="None"
																AlternateText="删除" Visible="False"></asp:ImageButton>
														</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr height="100%">
											<td>
												<div style="OVERFLOW: auto;HEIGHT: 100%">
													<asp:datagrid id="dgItem" runat="server" CssClass="list" CellPadding="0" GridLines="Horizontal"
														AllowSorting="True" AutoGenerateColumns="False" PageSize="15" Width="100%">
														<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
														<HeaderStyle CssClass="list-title"></HeaderStyle>
														<FooterStyle CssClass="list-title"></FooterStyle>
														<SelectedItemStyle CssClass="list-selected"></SelectedItemStyle>
														<Columns>
															<asp:BoundColumn Visible="False" DataField="DictionaryItemCode"></asp:BoundColumn>
															<asp:ButtonColumn DataTextField="Name" HeaderText="名称" CommandName="Modify">
																<HeaderStyle Width="30px"></HeaderStyle>
															</asp:ButtonColumn>
														</Columns>
														<PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
															CssClass="ListHeadTr"></PagerStyle>
													</asp:datagrid>
												</div>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<BR>
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
	</body>
</HTML>
