<%@ Register TagPrefix="cc2" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<%@ Register TagPrefix="cc1" Namespace="Rms.ControlLb" Assembly="RmsPM.BLL" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../../UserControls/InputUser.ascx" %>
<%@ Control Language="c#" Inherits="RmsPM.Web.DesignChange.Controls.DesignListControl" CodeFile="DesignListControl.ascx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="RmsPM.BLL.ControlsLB" Assembly="RmsPM.BLL" %>
<%@ Register TagPrefix="uc1" TagName="InputStationUser" Src="../../UserControls/InputStationUser.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="Rms.ControlLb" Assembly="RmsPM.BLL" %>
<table class="search-area" cellSpacing="0" cellPadding="0" border="0" width="100%" id="TDSearch"
	runat="server">
	<tr>
	<td>
	<table>
	<tr>
		<td>����:
			<asp:TextBox id="TB_Name" runat="server" Width="100px" CssClass="input"></asp:TextBox></td>
		<td>���:
			<asp:TextBox id="TB_Code" runat="server" Width="100px" CssClass="input"></asp:TextBox></td>
		<td>
			<cc3:selectbox id=SelectBox1 runat="server" Url="../SelectBox/SelectContracts.aspx" Text="ѡ���ͬ" ProjectCode='<%=Request["ProjectCode"]%>' ImageUrl="../Images/ToolsItemSearch.Gif" Height="0px" BoxWith="120px" ButtonImage="../../Images/ToolsItemSearch.Gif" BoxCssClass="input" IsEditMode="true">
			</cc3:selectbox>
		</td>
		<td>������:
			<uc1:InputStationUser id="InputStationUser1" runat="server"></uc1:InputStationUser></td>
		<td><INPUT class="submit" id="btnSearch" type="button" value="��   ��" name="btnSearch" runat="server" onserverclick="btnSearch_ServerClick"></td>
	</tr>
	<tr id="trSearch" runat="server">
		<td>״̬:</td>
		<td colspan="2"><FONT face="����">
				<asp:CheckBox id="CB_ALL" runat="server" Text="����"></asp:CheckBox>&nbsp;
				<asp:CheckBox id="CB_Begin" runat="server" Text="����"></asp:CheckBox>&nbsp;
				<asp:CheckBox id="CB_Auditing" runat="server" Text="������"></asp:CheckBox>&nbsp;
				<asp:CheckBox id="CB_Pass" runat="server" Text="ͨ��"></asp:CheckBox>&nbsp;
				<asp:CheckBox id="CB_UnPass" runat="server" Text="δͨ��"></asp:CheckBox></FONT></td>
	</tr>
	</td>
	</tr>
	</table>
</table>
<table width="100%" border="0">
	<tr>
		<td><asp:DataGrid id="DataGrid1" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
				PageSize="15" CssClass="list">
				<FooterStyle CssClass="list-title"></FooterStyle>
				<HeaderStyle CssClass="list-title"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn HeaderText="����">
						<ItemTemplate>
							<a href=# onclick='OpenChange(<%# DataBinder.Eval(Container, "DataItem.DesignCode") %>)'>
								<%# DataBinder.Eval(Container, "DataItem.DesignName") %>
							</a>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DesignName") %>'>
							</asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="DesignID" HeaderText="���"></asp:BoundColumn>
					<asp:BoundColumn DataField="DesignReason" HeaderText="���ԭ��"></asp:BoundColumn>
					<asp:BoundColumn DataField="DesignJionTime" HeaderText="����ʱ��"></asp:BoundColumn>
					<asp:BoundColumn DataField="DesignLastTime" HeaderText="��������"></asp:BoundColumn>
					<asp:BoundColumn DataField="DesignPerson" HeaderText="������"></asp:BoundColumn>
					<asp:TemplateColumn HeaderText="״̬">
						<ItemTemplate>
							<asp:Label runat="server" ID="Label1">
								<%# RmsPM.BLL.Design_MessageSystem.ShowStateMessage(DataBinder.Eval(Container, "DataItem.DesignState").ToString()) %>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle Visible="False"></PagerStyle>
			</asp:DataGrid>
			<cc2:GridPagination id="gpControl" runat="server" DataGridId="DataGrid1" ControlSourceUrl="../Images/GridPaginationSource/" onpageindexchange="gpControl_PageIndexChange"></cc2:GridPagination></td>
	</tr>
</table>
<script>
	function OpenChange(code,state) 
	{ 
		OpenLargeWindow('DesignChangeModify.aspx?ApplicationCode='+code+'&ProjectCode=<%=Request["ProjectCode"]%>&State=View','��Ʊ��')
	}
</script>

