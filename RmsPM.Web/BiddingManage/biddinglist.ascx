<%@ Control Language="c#" Inherits="RmsPM.Web.BiddingManage.BiddingList" CodeFile="BiddingList.ascx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<!--<asp:TemplateColumn HeaderText="类别">
			<ItemTemplate>
				<%# RmsPM.BLL.SystemGroupRule.GetSystemGroupName((string)(DataBinder.Eval(Container, "DataItem.Type"))) %>
			</ItemTemplate>
		</asp:TemplateColumn>-->
<asp:datagrid id="dgList" CssClass="list" CellPadding="0" GridLines="Horizontal" PageSize="15"
	Width="100%" AllowPaging="True" runat="server" AutoGenerateColumns="False">
	<FooterStyle CssClass="list-title"></FooterStyle>
	<HeaderStyle CssClass="list-title"></HeaderStyle>
	<Columns>
		
		<asp:TemplateColumn HeaderText="拟定标段">
			<ItemTemplate>
				<a href="#" onclick='javascript:OpenModify("<%# DataBinder.Eval(Container, "DataItem.BiddingCode") %>","edit","<%=ProjectCode%>");'>
					<%# DataBinder.Eval(Container, "DataItem.Title") %>
				</a>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="承办部门">
			<ItemTemplate>
				<asp:Label ID="lblUnitName" runat="server">
					<%#  RmsPM.BLL.SystemRule.GetUnitFullName(DataBinder.Eval(Container, "DataItem.BiddingRemark1").ToString())%>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="ArrangedDate" HeaderText="议标图纸" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
		<asp:BoundColumn DataField="StandardDate" HeaderText="规范日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
		<asp:BoundColumn DataField="PrejudicationDate" HeaderText="资格预审日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
		<asp:BoundColumn DataField="EmitDate" HeaderText="发标日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
		<asp:BoundColumn DataField="ReturnDate" HeaderText="回标日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
		<asp:BoundColumn DataField="ConfirmDate" HeaderText="定标日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
		<asp:BoundColumn DataField="Remark" HeaderText="备注"></asp:BoundColumn>
		
		<asp:TemplateColumn HeaderText="当前状态">
			<ItemTemplate>
				<asp:Label runat="server">
					<%# RmsPM.BLL.BiddingSystem.GetStateMessage(DataBinder.Eval(Container, "DataItem.State").ToString()) %>
				</asp:Label>
			</ItemTemplate>
			<EditItemTemplate>
				<asp:TextBox runat="server"></asp:TextBox>
			</EditItemTemplate>
		</asp:TemplateColumn>
	</Columns>
	<PagerStyle Visible="False" NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
		CssClass="ListHeadTr"></PagerStyle>
</asp:datagrid>
<cc1:GridPagination id="gpControl" runat="server" DataGridId="dgList" ControlSourceUrl="../Images/GridPaginationSource/" onpageindexchange="gpControl_PageIndexChange"></cc1:GridPagination>
<script>
	function OpenModify(code,state,projectcode)
	{
		OpenFullWindow('BiddingModify.aspx?ApplicationCode='+code+'&State='+state+'&ProjectCode='+projectcode,'招标计划维护');
	}
</script>
