<%@ Page language="c#" Inherits="RmsPM.Web.Systems.DepartmentInfo" CodeFile="DepartmentInfo.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>部门信息</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">

	function OpenStation(stationCode)
	{
		OpenLargeWindow('StationInfo.aspx?UnitCode=<%=Request["UnitCode"]%>&StationCode=' + stationCode ,'岗位信息');
	}
	
	function doModifyUnit()
	{
		OpenMiddleWindow('DepartmentModify.aspx?action=Modify&UnitCode=<%=Request["UnitCode"]%>','编辑部门');
	}
	
	function OpenUnit( unitCode )
	{
		OpenMiddleWindow( 'DepartmentInfo.aspx?UnitCode=' + unitCode ,'部门信息' );
	}
	
	function addNewUnit()
	{
		OpenMiddleWindow( 'DepartmentModify.aspx?Action=Insert&UnitCode=<%=Request["UnitCode"]%>','新增部门');
	}
/*	
	function addNewRole()
	{
		OpenFullWindow( 'RoleModify.aspx?UnitCode=<%=Request["UnitCode"]%>','新增角色');
	}
*/	
	function addNewStation()
	{
		OpenLargeWindow('StationModify.aspx?UnitCode=<%=Request["UnitCode"]%>','新增岗位');
	}
	
	function ModifyProject(ProjectCode)
	{
	   OpenMiddleWindow('ProjectModify.aspx?action=Modify&UnitCode=<%=Request["UnitCode"]%>&ProjectCode='+ProjectCode,'编辑项目');
	}
		</SCRIPT>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">部门信息</td>
				</tr>
				<tr id="trToolBar" runat="server">
					<td class="tools-area" vAlign="top"><IMG src="../images/btn_li.gif" align="absMiddle">
						<input class="button" id="btnModify" onclick="doModifyUnit();return false;" type="button"
							value="修 改" name="btnModify" runat="server"> <input class="button" id="btnDelete" onclick="javascript:if(!window.confirm('确实要删除这条记录吗？')) return false;"
							type="button" value="删 除" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick"> <input style="DISPLAY:none" class="button" id="btnClose" onclick="window.close();" type="button"
							value="关 闭" name="btnClose" runat="server">
					</td>
				</tr>
				<tr>
					<td>
						<TABLE class="form" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="form-item" width="20%">部门名称：</TD>
								<TD width="30%"><asp:label id="lblName" runat="server"></asp:label></TD>
								<TD width="20%" class="form-item">部门编号：</TD>
								<TD width="30%"><asp:label id="lblSortID" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="form-item" width="20%">部门类型：</TD>
								<TD><asp:label id="lblUnitType" runat="server"></asp:label></TD>
								<TD class="form-item">部门负责人：</TD>
								<TD><asp:label id="lblPrincipal" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="form-item">财务编码：</TD>
								<TD id="tdsubjectsetdesc" runat="server"><asp:Label Runat="server" ID="lblSubjectSetDesc"></asp:Label></TD>
								<td class="form-item" runat="server" id="lblSubjectsettd">帐套：</td>
								<td><asp:label id="lblSelfAccount" runat="server"></asp:label>&nbsp;
									<asp:label id="lblSubjectSet" runat="server"></asp:label></td>
							</TR>
							<TR>
								<TD class="form-item">备注：</TD>
								<TD><asp:label id="lblRemark" runat="server"></asp:label></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				<tr height="100%">
					<td>
						<table id="tableList" cellSpacing="0" cellpadding="5" width="100%" runat="server" height="100%">
							<tr>
								<td>
									<table cellSpacing="0" cellpadding="0" width="100%" height="100%">
										<tr>
											<td vAlign="top" align="center">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr>
														<td class="intopic" width="200">子部门</td>
														<td><input class="button-small" id="btnAddNewUnit" onclick="addNewUnit(); return false;" type="button"
																value="新增部门" name="btnConfigUser" runat="server"></td>
													</tr>
												</table>
											</td>
										</tr>
										<tr height="100%">
											<td valign="top">
												<div style="OVERFLOW:auto; WIDTH:100%; HEIGHT:100%">
													<asp:datagrid id="dgListChildUnit" runat="server" CssClass="list" CellPadding="0" GridLines="Horizontal"
														AllowSorting="True" AutoGenerateColumns="False" PageSize="15" Width="100%">
														<HeaderStyle CssClass="list-title"></HeaderStyle>
														<FooterStyle CssClass="list-title"></FooterStyle>
														<Columns>
															<asp:TemplateColumn HeaderText="部门名称">
																<ItemTemplate>
																	<a href="##" onclick="OpenUnit(code)" code='<%# DataBinder.Eval(Container.DataItem, "UnitCode")%>'>
																		<%# DataBinder.Eval(Container.DataItem, "UnitName")%>
																		(<%# DataBinder.Eval(Container.DataItem, "UserCount")%>) </a>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="负责人">
																<ItemTemplate>
																	<%# RmsPM.BLL.SystemRule.GetUserName( DataBinder.Eval(Container.DataItem, "Principal").ToString() )%>
																</ItemTemplate>
															</asp:TemplateColumn>
														</Columns>
														<PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
															CssClass="ListHeadTr"></PagerStyle>
													</asp:datagrid>
												</div>
											</td>
										</tr>
									</table>
								</td>
								<td>
									<table cellSpacing="0" cellpadding="0" width="100%" height="100%">
										<tr>
											<td vAlign="top" align="center">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr>
														<td class="intopic" width="200">岗位</td>
														<td><input class="button-small" id="btnNewStation" onclick="addNewStation(); return false;"
																type="button" value="新增岗位" runat="server" NAME="btnNewStation"></td>
													</tr>
												</table>
											</td>
										</tr>
										<tr height="100%">
											<td valign="top">
												<div style="OVERFLOW:auto; WIDTH:100%; HEIGHT:100%">
													<asp:datagrid id="dgConfig" runat="server" AllowPaging="False" Width="100%" PageSize="15" AutoGenerateColumns="False"
														AllowSorting="True" GridLines="Horizontal" CellPadding="0" CssClass="list">
														<AlternatingItemStyle CssClass=""></AlternatingItemStyle>
														<ItemStyle CssClass=""></ItemStyle>
														<HeaderStyle CssClass="list-title"></HeaderStyle>
														<FooterStyle CssClass="list-title"></FooterStyle>
														<Columns>
															<asp:TemplateColumn HeaderText="岗位名称">
																<ItemTemplate>
																	<a href="##" onclick="OpenStation(code)" code='<%# DataBinder.Eval(Container.DataItem, "StationCode")%>'>
																		<%# DataBinder.Eval(Container.DataItem, "StationName")%>
																		(<%# DataBinder.Eval(Container.DataItem, "UserCount")%>) </a>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="权限范围">
																<ItemTemplate>
																	<%#  DataBinder.Eval(Container.DataItem, "RoleLevelName") %>
																</ItemTemplate>
															</asp:TemplateColumn>
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
					</td>
				</tr>
			</table>
			<INPUT id="txtRefreshScript" type="hidden" name="txtRefreshScript" runat="server"><INPUT id="txtAct" type="hidden" name="txtAct" runat="server">
		</form>
	</body>
</HTML>
