<%@ Page language="c#" Inherits="RmsPM.Web.Systems.RoleInfo" CodeFile="RoleInfo.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>��ɫ��Ϣ</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
	
	function doModifyRole()
	{
		window.navigate('RoleModify.aspx?RoleCode=<%=Request["RoleCode"]%>','�༭��ɫ');
	}
	
	function OpenStation( code )
	{
		OpenMiddleWindow( 'StationInfo.aspx?StationCode=' + code ,'��λ��Ϣ' );
	}
	
		
		</SCRIPT>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">��ɫ��Ϣ</td>
				</tr>
				<tr>
					<td class="tools-area" vAlign="top">
						<IMG src="../images/btn_li.gif" align="absMiddle"> <input class="button" id="btnModify" onclick="doModifyRole();return false;" type="button"
							value="�� ��" name="btnModify" runat="server"> <input class="button" id="btnDelete" onclick="javascript:if(!window.confirm('ȷʵҪɾ��������¼��')) return false;"
							type="button" value="ɾ ��" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick"> <input class="button" id="btnClose" onclick="window.close();" type="button" value="�� ��"
							name="btnClose" runat="server"> <input class="button" id="btnAdd" type="button" value="��������"
							name="btnAdd" runat="server" onserverclick="btnAdd_ServerClick">
					</td>
				</tr>
				<tr>
					<td>
						<TABLE class="form" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="form-item" width="100">��ɫ���ƣ�</TD>
								<TD><asp:label id="lblRoleName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="form-item">˵����</TD>
								<TD><asp:label id="lblDescription" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="form-item" width="100">����</TD>
								<TD><asp:label id="sortID" runat="server"></asp:label></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td valign="top" height="100%">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="intopic" width="200">�� λ</td>
							</tr>
						</table>
						<asp:datagrid id="dgList" runat="server" Width="100%" PageSize="15" AutoGenerateColumns="False"
							AllowSorting="True" GridLines="Horizontal" CellPadding="0" CssClass="list">
							<FooterStyle CssClass="list-title"></FooterStyle>
							<HeaderStyle CssClass="list-title"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="��������">
									<ItemTemplate>
										<%# RmsPM.BLL.SystemRule.GetUnitFullName( DataBinder.Eval(Container.DataItem, "UnitCode").ToString() ) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="��λ">
									<ItemTemplate>
										<a href="##" onclick='OpenStation(code);' code='<%# DataBinder.Eval(Container.DataItem, "StationCode")%>'>
											<%# DataBinder.Eval(Container.DataItem, "StationName")%>
											��<%# DataBinder.Eval(Container.DataItem, "UserCount")%>
											�ˣ� </a>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<%#  DataBinder.Eval(Container.DataItem, "Description")  %>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle NextPageText="��һҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��һҳ" HorizontalAlign="Right"
								CssClass="ListHeadTr"></PagerStyle>
						</asp:datagrid>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="intopic" width="200">Ȩ ��</td>
								<td><input class="button" type="button" id="btnConfigRight" runat="server" onclick='configRoleRight();'
										value='����Ȩ��'></td>
							</tr>
						</table>
		<div><table cellSpacing="0" cellPadding="0" width="100%" border="1" class="table">
							<tr>
								<td>���</td>
								<td>����</td>
								<td>˵��</td>
								<td>��Ŀ�ر�˵��</td>
							</tr>
							<asp:repeater id="rptFunction" runat="server">
								<ItemTemplate>
									<tr>
										<td><b><%# DataBinder.Eval(Container.DataItem, "ShowCode") %>
												<%# DataBinder.Eval(Container.DataItem, "functionStructureCode") %>
												
												<%--<input type="checkbox" id='chkA'  value='<%# DataBinder.Eval(Container.DataItem, "FunctionStructureCode") %>'   checked="checked" >--%>
											</b>
										</td>
										<td>
											<%# DataBinder.Eval(Container.DataItem, "FunctionStructureName") %>
										</td>
										<td><%# DataBinder.Eval(Container.DataItem, "Description") %></td>
										<td><%# DataBinder.Eval(Container.DataItem, "ProjectSpecialDescription") %></td>
								</ItemTemplate>
							</asp:repeater></div>
					</td>
				</tr>
			</table>
			<INPUT id="txtRefreshScript" type="hidden" name="txtRefreshScript" runat="server">
			<script>
	function configRoleRight()
	{
		window.open( 'ConfigRoleRight.aspx?RoleCode=<%=Request["RoleCode"]%>' );
	}

			</script>
			
			
		</form>
	</body>
</HTML>
