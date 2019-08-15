<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<%@ Control Language="c#" Inherits="RmsPM.Web.BiddingManage.BiddingEmitList" CodeFile="BiddingEmitList.ascx.cs" %>
<asp:datagrid id="dgList" CssClass="list" CellPadding="0" GridLines="Horizontal" PageSize="15"
	Width="100%" AllowPaging="True" runat="server" AutoGenerateColumns="False">
	<HeaderStyle CssClass="list-title"></HeaderStyle>
	<FooterStyle CssClass="list-title"></FooterStyle>
	<Columns>
		<asp:BoundColumn Visible="False" DataField="BiddingEmitCode" HeaderText="BiddingEmitCode"></asp:BoundColumn>
		<asp:BoundColumn Visible="False" DataField="BiddingCode" HeaderText="BiddingCode"></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="编号">
			<ItemTemplate>
				<a href="#" onclick='javascript:BiddingEmitListReturnModify("<%# (string)(DataBinder.Eval(Container, "DataItem.BiddingEmitCode")) %>");return false;'>
					<%# DataBinder.Eval(Container, "DataItem.EmitNumber") %>
				</a>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="EmitDate" HeaderText="发标日期" DataFormatString="{0:d}"></asp:BoundColumn>
		<asp:BoundColumn DataField="EndDate" HeaderText="截止日期" DataFormatString="{0:d}"></asp:BoundColumn>
		<asp:BoundColumn DataField="PrejudicationDate" HeaderText="开标日期" DataFormatString="{0:d}"></asp:BoundColumn>
		<asp:BoundColumn Visible="False" DataField="CreatUser" HeaderText="CreatUser"></asp:BoundColumn>
		<asp:BoundColumn Visible="False" DataField="CreatDate" HeaderText="CreatDate"></asp:BoundColumn>
	</Columns>
	<PagerStyle Visible="False" NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
		CssClass="ListHeadTr"></PagerStyle>
</asp:datagrid>
<cc1:GridPagination id="gpControl" runat="server" DataGridId="dgList" ControlSourceUrl="../Images/GridPaginationSource/"></cc1:GridPagination>
<script>
function BiddingEmitListReturnModify(code)
{
	OpenLargeWindow('BiddingReturnModifyPage.aspx?BiddingEmitCode='+code+'&State=edit','会标操作');
}
</script>
