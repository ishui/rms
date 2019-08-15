<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<%@ Page language="c#" Inherits="RmsPM.Web.SelectBox.SelectContracts" CodeFile="SelectContracts.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ѡ���ͬ</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script language="javascript">
			function doCancel()
			{
				window.close();
			}
			
			function DoSelectSupplierReturn ( code,name)
			{
				Form1.txtSupplierCode.value = code;
				Form1.txtSupplierName.value = name;
			}

			function openSelectSupplier()
			{
				OpenMiddleWindow( '../SelectBox/SelectSupplier.aspx?ProjectCode=<%=Request["ProjectCode"]%>','ѡ��Ӧ��' );
			}

			function openSelectUser()
			{
				OpenMiddleWindow( '../SelectBox/SelectPerson.aspx?ProjectCode=<%=Request["ProjectCode"]%>&flag=1','ѡ������' );
			}

			function DoSelectUser(userCode,userName)
			{
				Form1.txtUserCode.value = userCode;
				Form1.txtUserName.value = userName;
			}

		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">ѡ���ͬ</td>
				</tr>
				<TR>
					<TD vAlign="top" align="center">
						<table class="search-area" border="0">
							<tr>
								<td>���ƣ�</td>
								<td><input class="input" id="txtContractName" type="text" name="txtContractName" runat="server"></td>
								<td>��ţ�</td>
								<td><input class="input" id="txtContractID" type="text" name="txtContractID" runat="server"></td>
								<td>״̬��</td>
								<td><asp:checkboxlist id="cblStatus" runat="server" RepeatDirection="Horizontal">
										<asp:ListItem Value="0" Selected="True">����</asp:ListItem>
										<asp:ListItem Value="1">����</asp:ListItem>
										<asp:ListItem Value="2">����</asp:ListItem>
										<asp:ListItem Value="4">���</asp:ListItem>
									</asp:checkboxlist></td>
								<td><INPUT class="submit" id="btnSearch" type="button" value="�� ��" name="Button1" runat="server" onserverclick="btnSearch_ServerClick"></td>
							</tr>
							<tr>
								<td>��Ӧ�̣�</td>
								<td><input class="input" id="txtSupplierName" type="text" name="txtSupplierName" runat="server">
									<input id="txtSupplierCode" type="hidden" name="txtSupplierCode" runat="server">
									<A onclick="openSelectSupplier();" href="##"><IMG src="../images/ToolsItemSearch.gif" border="0"></A>
								</td>
								<td>���ţ�</td>
								<td><select id="sltUnit" name="sltUnit" runat="server">
										<option value="" selected>----��ѡ��----</option>
									</select>
								</td>
								<TD>��ͬ���ͣ�</TD>
								<TD><INPUT class="input" id="txtTypeName" readOnly type="text" name="txtTypeName" runat="server">
									<INPUT id="txtTypeCode" type="hidden" name="txtTypeCode" runat="server"> <A onclick="doSelectContractType();return false;" href="##">
										<IMG src="../images/ToolsItemSearch.gif" border="0"></A>
								</TD>
							</tr>
						</table>
						<asp:datagrid id="dgContractList" runat="server" AllowPaging="True" CssClass="list" Width="100%"
							CellPadding="2" GridLines="Horizontal" AllowSorting="True" AutoGenerateColumns="False" DataKeyField="ContractCode"
							PageSize="6">
							<FooterStyle CssClass="list-title"></FooterStyle>
							<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
							<HeaderStyle CssClass="list-title"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ContractCode" HeaderText="��ͬ���"></asp:BoundColumn>
								<asp:BoundColumn DataField="ContractName" HeaderText="��ͬ����"></asp:BoundColumn>
								<asp:BoundColumn DataField="ContractPerson" HeaderText="������"></asp:BoundColumn>
								<asp:TemplateColumn>
									<HeaderTemplate>
										��λ
									</HeaderTemplate>
									<ItemTemplate>
										<%# RmsPM.BLL.SystemRule.GetUnitName( DataBinder.Eval( Container,"DataItem.UnitCode" ).ToString() ) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderTemplate>
										��Ӧ��
									</HeaderTemplate>
									<ItemTemplate>
										<%# RmsPM.BLL.ProjectRule.GetSupplierName( DataBinder.Eval( Container,"DataItem.SupplierCode" ).ToString() ) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="ContractDate" HeaderText="��Чʱ��" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="ContractID" HeaderText="��ͬID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="ѡ��">
									<ItemTemplate>
										<FONT face="����"></FONT>&nbsp;
										<asp:CheckBox id="CheckBox1" runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle Visible="False" NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
								CssClass="ListHeadTr"></PagerStyle>
						</asp:datagrid><cc1:gridpagination id="GridPagination1" runat="server" DataGridId="dgContractList" ControlSourceUrl="../ControlSource/" onpageindexchange="GridPagination1_PageIndexChange_1"></cc1:gridpagination>
						<table id="tbButton" cellSpacing="10" width="100%" align="center" runat="server">
							<tr vAlign="bottom" align="center" width="100%">
								<td><input class="submit" id="SaveToolsButton" type="button" value="ȷ ��" name="SaveToolsButton"
										runat="server" onserverclick="SaveToolsButton_ServerClick"> <input class="submit" id="CancelToolsButton" onclick="doCancel();return false;" type="button"
										value="ȡ ��">
								</td>
							</tr>
						</table>
					</TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
