<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<%@ Control Language="c#" Inherits="RmsPM.Web.BiddingManage.BiddingReturnList" CodeFile="BiddingReturnList.ascx.cs" %>
<asp:datagrid id="dgList" CssClass="list" CellPadding="0" GridLines="Horizontal" PageSize="15"
	Width="100%" AllowPaging="True" runat="server" AutoGenerateColumns="False">
	<HeaderStyle CssClass="list-title"></HeaderStyle>
	<FooterStyle CssClass="list-title"></FooterStyle>
	<Columns>
		<asp:BoundColumn Visible="False" DataField="BiddingReturnCode" HeaderText="BiddingReturnCode"></asp:BoundColumn>
		<asp:BoundColumn DataField="SupplierCode" HeaderText="投标单位"></asp:BoundColumn>
		<asp:BoundColumn DataField="Money" HeaderText="金额"></asp:BoundColumn>
		<asp:BoundColumn DataField="Remark" HeaderText="备注"></asp:BoundColumn>
		<asp:BoundColumn DataField="OrderCode" HeaderText="序号"></asp:BoundColumn>
		<asp:BoundColumn DataField="ReturnDate" HeaderText="回标日期" DataFormatString="{0:d}"></asp:BoundColumn>
	</Columns>
	<PagerStyle Visible="False" NextPageText="下一�?gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一�? HorizontalAlign="Right"
		CssClass="ListHeadTr"></PagerStyle>
</asp:datagrid>
<cc1:GridPagination id="gpControl" runat="server" DataGridId="dgList" ControlSourceUrl="../Images/GridPaginationSource/" onpageindexchange="gpControl_PageIndexChange"></cc1:GridPagination>
