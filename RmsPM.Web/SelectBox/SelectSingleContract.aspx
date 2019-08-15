<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<%@ Page language="c#" Inherits="RmsPM.Web.SelectBox.SelectSingleContract" CodeFile="SelectSingleContract.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>选择合同</title>
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
				OpenMiddleWindow( '../SelectBox/SelectSupplier.aspx?ProjectCode=<%=Request["ProjectCode"]%>','选择供应商' );
			}

			function openSelectUser()
			{
				OpenMiddleWindow( '../SelectBox/SelectPerson.aspx?ProjectCode=<%=Request["ProjectCode"]%>&flag=1','选择负责人' );
			}

			function DoSelectUser(userCode,userName)
			{
				Form1.txtUserCode.value = userCode;
				Form1.txtUserName.value = userName;
			}
			function doSelectContractType()
			{
			
			}
			
			function DoSelectContract(ContractName,ContractCode)
			{
			    window.opener.InputContract_GetReturnValue(ContractName,ContractCode,'<%=Request["ID"]%>');
			    window.close();
			}
		</script>
        
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25" style="width: 885px">选择合同</td>
				</tr>
				<TR>
					<TD vAlign="top" align="center" style="width: 885px">
						<table class="search-area" border="0">
							<tr>
								<td>名称：</td>
								<td><input class="input" id="txtContractName" type="text" runat="server" NAME="txtContractName"></td>
								<td>编号：</td>
								<td><input class="input" id="txtContractID" type="text" runat="server" NAME="txtContractID"></td>
								<td>状态：</td>
								<td><asp:checkboxlist id="cblStatus" runat="server" RepeatDirection="Horizontal">
										<asp:ListItem Value="0" Selected="True">正常</asp:ListItem>
										<asp:ListItem Value="1">待审</asp:ListItem>
										<asp:ListItem Value="2">结算</asp:ListItem>
										<asp:ListItem Value="4">变更</asp:ListItem>
									</asp:checkboxlist></td>
								<td><INPUT class="submit" id="btnSearch" type="button" value="搜 索" name="Button1" runat="server" onserverclick="btnSearch_ServerClick"></td>
							</tr>
							<tr>
								<td>供应商：</td>
								<td><input type="text" id="txtSupplierName" runat="server" class="input" NAME="txtSupplierName">
									<input type="hidden" id="txtSupplierCode" runat="server" NAME="txtSupplierCode">
									<a href="##" onclick="openSelectSupplier();"><IMG src="../images/ToolsItemSearch.gif" border="0"></a>
								</td>
								<td>部门：</td>
								<td><select id="sltUnit" runat="server" NAME="sltUnit">
										<option value="" selected>----请选择----</option>
									</select>
								</td>
								<TD>合同类型：</TD>
								<TD><uc1:inputsystemgroup id="inputSystemGroup" runat="server"/></TD>
							</tr>
						</table>
						<asp:datagrid id="dgContractList" runat="server" DataKeyField="ContractCode" AutoGenerateColumns="False"
							AllowSorting="True" GridLines="Horizontal" CellPadding="2" Width="100%" CssClass="list" AllowPaging="True" Height="149px">
							<FooterStyle CssClass="list-title"></FooterStyle>
							<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
							<HeaderStyle CssClass="list-title"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ContractID" HeaderText="合同编号"></asp:BoundColumn>
								
								
				                <asp:TemplateColumn HeaderText="合同名称">
                                    <ItemTemplate>
                                        <a href="#" onclick='javascript:DoSelectContract("<%# DataBinder.Eval(Container, "DataItem.ContractName") %>","<%# DataBinder.Eval(Container, "DataItem.ContractCode") %>");'>
					                        <%# DataBinder.Eval(Container, "DataItem.ContractName") %>
				                        </a>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="经办人">
                                    <ItemTemplate>
                                        <%# RmsPM.BLL.SystemRule.GetUserName((string)DataBinder.Eval(Container, "DataItem.ContractPerson")) %>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderTemplate>
										单位
									</HeaderTemplate>
									<ItemTemplate>
										<%# RmsPM.BLL.SystemRule.GetUnitName( DataBinder.Eval( Container,"DataItem.UnitCode" ).ToString() ) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderTemplate>
										供应商
									</HeaderTemplate>
									<ItemTemplate>
										<%# RmsPM.BLL.ProjectRule.GetSupplierName( DataBinder.Eval( Container,"DataItem.SupplierCode" ).ToString() ) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:BoundColumn DataField="ContractDate" HeaderText="生效时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="ContractID" HeaderText="合同ID"></asp:BoundColumn>
							</Columns>
							<PagerStyle Visible="False" NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
					CssClass="ListHeadtr"></PagerStyle>
						</asp:datagrid>
						<cc1:GridPagination id="gpControl" runat="server" DataGridId="dgContractList" ControlSourceUrl="../Images/GridPaginationSource/" onpageindexchange="gpControl_PageIndexChange"></cc1:GridPagination>
						<table id="tbButton" align="center" runat="server" cellspacing="10" width="100%">
							<tr align="center" width="100%" valign="bottom">
								<td>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
			</table>
		</form>
		
	</body>
</HTML>
