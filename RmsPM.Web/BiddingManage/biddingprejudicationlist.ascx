<%@ Control Language="c#" Inherits="RmsPM.Web.BiddingManage.BiddingPrejudicationList" CodeFile="BiddingPrejudicationList.ascx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<asp:datagrid id="dgList" CssClass="list" CellPadding="0" GridLines="Horizontal" PageSize="15"
	Width="100%" AllowPaging="True" runat="server" AutoGenerateColumns="False">
	<HeaderStyle CssClass="list-title"></HeaderStyle>
	<FooterStyle CssClass="list-title"></FooterStyle>
	<Columns>
		<asp:BoundColumn DataField="BiddingPrejudicationCode" HeaderText="BiddingPrejudicationCode"></asp:BoundColumn>
		<asp:BoundColumn DataField="BiddingCode" HeaderText="BiddingCode"></asp:BoundColumn>
		<asp:BoundColumn DataField="WorkConfine" HeaderText="WorkConfine"></asp:BoundColumn>
		<asp:BoundColumn DataField="Number" HeaderText="Number"></asp:BoundColumn>
		<asp:BoundColumn DataField="Remark" HeaderText="Remark"></asp:BoundColumn>
		<asp:BoundColumn DataField="UserCode" HeaderText="UserCode"></asp:BoundColumn>
		<asp:BoundColumn DataField="CreateDate" HeaderText="CreateDate"></asp:BoundColumn>
		<asp:BoundColumn DataField="State" HeaderText="State"></asp:BoundColumn>
		<asp:BoundColumn DataField="Flag" HeaderText="Flag"></asp:BoundColumn>
	</Columns>
	<PagerStyle Visible="False" NextPageText="下一�?gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一�? HorizontalAlign="Right"
		CssClass="ListHeadTr"></PagerStyle>
</asp:datagrid>
<cc1:GridPagination id="gpControl" runat="server" DataGridId="dgList" ControlSourceUrl="../Images/GridPaginationSource/" onpageindexchange="gpControl_PageIndexChange"></cc1:GridPagination>
