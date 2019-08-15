<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<%@ Page language="c#" Inherits="RmsPM.Web.SelectBox.SelectContract" CodeFile="SelectContract.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
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
								<td><input class="input" id="txtContractName" type="text" runat="server" NAME="txtContractName"></td>
								<td>��ţ�</td>
								<td><input class="input" id="txtContractID" type="text" runat="server" NAME="txtContractID"></td>
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
								<td><input type="text" id="txtSupplierName" runat="server" class="input" NAME="txtSupplierName">
									<input type="hidden" id="txtSupplierCode" runat="server" NAME="txtSupplierCode">
									<a href="##" onclick="openSelectSupplier();"><IMG src="../images/ToolsItemSearch.gif" border="0"></a>
								</td>
								<td>���ţ�</td>
								<td><select id="sltUnit" runat="server" NAME="sltUnit">
										<option value="" selected>----��ѡ��----</option>
									</select>
								</td>
								<TD>��ͬ���ͣ�</TD>
								<TD><INPUT class="input" id="txtTypeName" type="text" runat="server" readOnly NAME="txtTypeName">
									<INPUT id="txtTypeCode" type="hidden" runat="server" NAME="txtTypeCode"> <a href="##" onclick="doSelectContractType();return false;">
										<img src="../images/ToolsItemSearch.gif" border="0"></a>
								</TD>
							</tr>
						</table>
						<asp:datagrid id="dgContractList" runat="server" DataKeyField="ContractCode" AutoGenerateColumns="False"
							AllowSorting="True" GridLines="Horizontal" CellPadding="2" Width="100%" CssClass="list" AllowPaging="True">
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
								<asp:TemplateColumn HeaderText="ѡ��">
									<ItemTemplate>
										<asp:CheckBox ID="chkContract" Runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible="False" DataField="ContractID" HeaderText="��ͬID"></asp:BoundColumn>
							</Columns>
							<PagerStyle Visible="False" NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
								CssClass="ListHeadTr"></PagerStyle>
						</asp:datagrid>
						<cc1:GridPagination id="GridPagination1" runat="server" ControlSourceUrl="../ControlSource/" DataGridId="dgContractList" onpageindexchange="GridPagination1_PageIndexChange_1"></cc1:GridPagination>
						<table id="tbButton" align="center" runat="server" cellspacing="10" width="100%">
							<tr align="center" width="100%" valign="bottom">
								<td><input class="submit" id="SaveToolsButton" type="button" value="ȷ ��" runat="server" onserverclick="SaveToolsButton_ServerClick">
									<input class="submit" id="CancelToolsButton" type="button" value="ȡ ��" onclick="doCancel();return false;">
								</td>
							</tr>
						</table>
					</TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
