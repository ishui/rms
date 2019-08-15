<%@ Page language="c#" Inherits="RmsPM.Web.ChangeProject" CodeFile="ChangeProject.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>选择项目</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Images/Style.css" type="text/css" rel="stylesheet">
		<script>
        function DoSelectProject(porjectCode)
        {
           Form1.hidPorjectCode.value=porjectCode;
           Form1.Button2.click();
           
        }
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD vAlign="top" align="center">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td noWrap borderColorLight="#e1e5f4" align="center" borderColorDark="#e1e5f4">
									<table cellSpacing="0" borderColorDark="#ffffff" borderColorLight="#6c7893" border="1">
										<tr>
											<td noWrap align="left">项目名称：<input id="txtProjectNname" style="WIDTH: 96px; HEIGHT: 22px" type="text" maxLength="25"
													size="10" name="txtProjectNname" runat="server"></td>
											<td align="center" width="10%"><input class="buttontop" id="Button1" type="button" value="搜 索" name="Button1" runat="server" onserverclick="Button1_ServerClick"></td>
										</tr>
										<tr>
											<td noWrap align="left" colspan="2">开工年份：<input id="txtKgYear" style="WIDTH: 47px; HEIGHT: 22px" type="text" maxLength="25" size="2"
													name="txtKgYear" runat="server">竣工年份：<input id="txtJgYear" style="WIDTH: 48px; HEIGHT: 22px" type="text" maxLength="25" size="2"
													name="txtJgYear" runat="server">项目状态：<SELECT class="Select" id="SelectStatus" style="WIDTH: 96px" name="sltSubjectSet" runat="server"></SELECT></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td vAlign="top" align="center" width="100%"><asp:datagrid id="dgList" runat="server" CellPadding="2" GridLines="Horizontal" AllowPaging="True"
										AutoGenerateColumns="False" AllowSorting="True" Width="100%" CssClass="Table">
										<AlternatingItemStyle CssClass="AlterGridTr"></AlternatingItemStyle>
										<ItemStyle CssClass="ListBodyTr"></ItemStyle>
										<HeaderStyle CssClass="ListHeadTr"></HeaderStyle>
										<FooterStyle CssClass="ListHeadTr"></FooterStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="项目名称">
												<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
												<ItemTemplate>
													<a href="#" onclick="DoSelectProject(this.Code);return false;" Code='<%# DataBinder.Eval(Container, "DataItem.ProjectCode") %>'><%# DataBinder.Eval(Container, "DataItem.ProjectName") %></a>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="kgDate" HeaderText="开期" DataFormatString="{0:yyyy/MM}"></asp:BoundColumn>
											<asp:BoundColumn DataField="jgDate" HeaderText="竣期" DataFormatString="{0:yyyy/MM}"></asp:BoundColumn>
										</Columns>
										<PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
											CssClass="ListHeadTr"></PagerStyle>
									</asp:datagrid></td>
							</tr>
						</table>
						<br>
						<INPUT class="buttontop" id="Button2" style="DISPLAY: none" type="button" name="Button2"
							runat="server" onserverclick="Button2_ServerClick"> <input id="hidPorjectCode" type="hidden" name="hidPorjectCode" runat="server">
					</TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
