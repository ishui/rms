<%@ Page language="c#" Inherits="RmsPM.Web.SelectBox.SelectViseBox" CodeFile="selectViseBox.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="InputStationUser" Src="../UserControls/InputStationUser.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputSupplier" Src="../UserControls/InputSupplier.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputUnit" Src="../UserControls/InputUnit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputCostBudgetdtl" Src="../UserControls/InputCostBudgetdtl.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="Rms.ControlLb" Assembly="RmsPM.BLL" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>

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
		</script>
		
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25" style="width: 885px">ѡ��ǩ֤</td>
				</tr>
				<TR>
					<TD vAlign="top" align="center" style="width: 885px">
<table class="search-area" cellSpacing="0" cellPadding="0" border="0" width="100%">
	<tr>
		<td valign="top">
			<table width="100%">
					<tr width="100%">
						<td>ǩ֤���ƣ�</td>
						<td><INPUT class="input" id="txtViseName" type="text" size="12" name="TxtViseName" runat="server"></td>
						<td>ǩ֤��ţ�</td>
						<td><INPUT class="input" id="txtViseID" type="text" size="12" name="txtViseID" runat="server"></td>
						<td>ǩ֤���ͣ�</td>
						<td><uc1:InputSystemGroup ID="inputSystemGroup" runat="server" /></td>
						<td>�� �� �ˣ�</td>
		<td><uc1:inputstationuser id="InputStationUser" runat="server"></uc1:inputstationuser></td>
        			
		</tr>
			
		
	<tr>
		<td>���첿�ţ�</td>
		<td><uc1:InputUnit id="InputUnit" runat="server"></uc1:InputUnit></td>
		
		<td>�� �� �̣�</td>
		<td><uc1:inputsupplier id="InputSupplier" runat="server"></uc1:inputsupplier></td>
		
		<td colspan="4" align="center"><input class="submit" id="btnSearch" type="button" value="��   ��" name="btnSearch" runat="server" onserverclick="btnSearch_ServerClick"></td>
	</tr>
</table>
</td></tr></table>
						<asp:datagrid id="dgViseList" runat="server" DataKeyField="ViseCode" AutoGenerateColumns="False"
							AllowSorting="True" GridLines="Horizontal" CellPadding="2" Width="100%" CssClass="list" AllowPaging="True" Height="149px">
							<FooterStyle CssClass="list-title"></FooterStyle>
							<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
							<HeaderStyle CssClass="list-title"></HeaderStyle>
							<Columns>
                                <asp:TemplateColumn>
                                <ItemTemplate>
                                <input id="ViseCode" type="hidden" name="ViseCode" value='<%# DataBinder.Eval(Container,"DataItem.ViseCode") %>' runat="server" />
                                </ItemTemplate>
                                </asp:TemplateColumn>							
								<asp:BoundColumn DataField="ViseID" HeaderText="ǩ֤���"></asp:BoundColumn>
								<asp:BoundColumn DataField="ViseName" HeaderText="ǩ֤����"></asp:BoundColumn>
								<asp:TemplateColumn>
									<HeaderTemplate>
										������
									</HeaderTemplate>
									<ItemTemplate>
										<%# RmsPM.BLL.SystemRule.GetUserName( DataBinder.Eval( Container,"DataItem.VisePersonCode" ).ToString() ) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderTemplate>
										���첿��
									</HeaderTemplate>
									<ItemTemplate>
										<%# RmsPM.BLL.SystemRule.GetUnitName( DataBinder.Eval( Container,"DataItem.ViseDepartmentCode" ).ToString() ) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderTemplate>
										��Ӧ��
									</HeaderTemplate>
									<ItemTemplate>
										<%# RmsPM.BLL.ProjectRule.GetSupplierName( DataBinder.Eval( Container,"DataItem.ViseSupplierCode" ).ToString() ) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								    <asp:TemplateColumn>
                                    <ItemTemplate>                    
                                      <asp:CheckBox id="selectViseCheckBox" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
							</Columns>
							<PagerStyle Visible="False" NextPageText="��һҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��һҳ" HorizontalAlign="Right"
					CssClass="ListHeadtr"></PagerStyle>
						</asp:datagrid>
						<cc1:GridPagination id="gpControl" runat="server" DataGridId="dgViseList" ControlSourceUrl="../Images/GridPaginationSource/" onpageindexchange="gpControl_PageIndexChange"></cc1:GridPagination>
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

