<%@ Page language="c#" Inherits="RmsPM.Web.Project.ProjectList" CodeFile="ProjectList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ProjectList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span id="spanTitle" runat="server">项目汇总>项目概况</span></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnAdd" onclick="Add()" type="button" value="新 增" name="btnAdd"
							runat="server">
					</td>
				</TR>
				<tr>
					<td class="table" vAlign="top">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0" class="search-area">
							<tr>
								<td>
									<table>
										<tr>
											<td nowrap>项目名称：</td>
											<td nowrap><input id="txtSearchProjectName" type="text" size="20" name="txtSearchProjectName" runat="server"
													class="input"></td>
											<td nowrap>项目状态：</td>
											<td nowrap><SELECT class="select" id="sltSearchStatus" name="sltSearchStatus" runat="server">
													<option selected value="">--请选择--</option>
												</SELECT></td>
											<td nowrap>开工年份：</td>
											<td nowrap><input id="txtSearchKgYear" type="text" class="input" size="4" name="txtSearchKgYear" runat="server"></td>
											<td nowrap>竣工年份：</td>
											<td nowrap><input id="txtSearchJgYear" type="text" class="input" size="4" name="txtSearchJgYear" runat="server"></td>
											<td><input class="submit" id="btnSearch" type="button" value="搜 索" name="btnSearch" runat="server" onserverclick="btnSearch_ServerClick"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="table" valign="top">
						<div style="position:absolute;OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<asp:datagrid id="dgList" runat="server" CssClass="list" CellPadding="0" GridLines="Horizontal"
								AllowPaging="False" PageSize="15" AutoGenerateColumns="False" AllowSorting="True" Width="100%">
								<AlternatingItemStyle CssClass=""></AlternatingItemStyle>
								<ItemStyle CssClass=""></ItemStyle>
								<HeaderStyle CssClass="list-title"></HeaderStyle>
								<FooterStyle CssClass="list-title"></FooterStyle>
								<Columns>
									<asp:TemplateColumn ItemStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderText="项目名称" SortExpression="ProjectName">
										<HeaderStyle Wrap="False"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
										<ItemTemplate>
											<a href="#" onclick="View(this.Code);return false;" Code='<%#  DataBinder.Eval(Container.DataItem, "ProjectCode") %>'><%#  DataBinder.Eval(Container.DataItem, "ProjectName") %></a>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Status" HeaderText="阶段" SortExpression="Status">
										<HeaderStyle Wrap="False"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="kgDate" HeaderText="开工年月" DataFormatString="{0:yyyy-MM}" SortExpression="kgDate">
										<HeaderStyle Wrap="False"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="jgDate" HeaderText="竣工年月" DataFormatString="{0:yyyy-MM}" SortExpression="jgDate">
										<HeaderStyle Wrap="False"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ProjectAddress" HeaderText="项目地址" SortExpression="ProjectAddress">
										<HeaderStyle Wrap="False"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Area" HeaderText="地区" SortExpression="Area">
										<HeaderStyle Wrap="False"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="JD" HeaderText="基地" SortExpression="jd">
										<HeaderStyle Wrap="False"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DevelopUnit" HeaderText="开发单位" SortExpression="DevelopUnit">
										<HeaderStyle Wrap="False"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="占地面积<br>(平米)" SortExpression="TotalFloorSpace">
										<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
										<ItemTemplate>
											<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "TotalFloorSpace")) %>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="建筑面积<br>(平米)" SortExpression="TotalBuildingSpace">
										<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
										<ItemTemplate>
											<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "TotalBuildingSpace")) %>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="容积率" SortExpression="PlannedVolumeRate">
										<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
										<ItemTemplate>
											<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "PlannedVolumeRate"))%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="可售面积<br>(平米)" SortExpression="BuildingSpaceForVolumeRate">
										<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
										<ItemTemplate>
											<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container.DataItem, "BuildingSpaceForVolumeRate"))%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="总户数" SortExpression="HouseCount">
										<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "HouseCount", "{0:#,##}") %>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
									CssClass="ListHeadTr"></PagerStyle>
							</asp:datagrid>
						</div>
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
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script language="javascript">
<!--
//新增
function Add()
{
	OpenCustomWindow("ProjectModify.aspx?FromUrl=" + escape(window.location.href), "项目修改", 780, 560);
}
	
function View(ProjectCode)
{
	window.navigate('ProjectInfo.aspx?FromUrl=" + escape(window.location) + "&ProjectCode='+ProjectCode);
}

//-->
		</script>
	</body>
</HTML>
