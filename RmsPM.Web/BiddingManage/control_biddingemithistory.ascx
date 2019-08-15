<%@ Control Language="c#" Inherits="RmsPM.Web.BiddingManage.Control_BiddingEmitHistory" CodeFile="Control_BiddingEmitHistory.ascx.cs" %>
<FONT face="宋体">
	<asp:DataGrid id="DataGrid1" runat="server" AutoGenerateColumns="False" Width="100%" GridLines="Horizontal"
		CellPadding="0" CssClass="list">
		<FooterStyle CssClass="list-title"></FooterStyle>
		<HeaderStyle CssClass="list-title"></HeaderStyle>
		<Columns>
			<asp:TemplateColumn HeaderText="序号">
				<ItemTemplate>
					<asp:Label runat="server"> 
						<%# Container.ItemIndex + 1%>
					</asp:Label>
				</ItemTemplate>
				<EditItemTemplate>
					<asp:TextBox runat="server"></asp:TextBox>
				</EditItemTemplate>
			</asp:TemplateColumn>
			<asp:BoundColumn DataField="EmitDate" HeaderText="发标时间" DataFormatString="{0:yyyy-MM-dd  HH:mm}"></asp:BoundColumn>
			<asp:BoundColumn DataField="PrejudicationDate" HeaderText="开标时间" DataFormatString="{0:yyyy-MM-dd HH:mm}"></asp:BoundColumn>
			<asp:BoundColumn DataField="EndDate" HeaderText="截标时间" DataFormatString="{0:yyyy-MM-dd HH:mm}"></asp:BoundColumn>
			<asp:TemplateColumn HeaderText="发标明细">
				<ItemTemplate>
					<A href='#' onclick='javascript:ReadBiddingEmit(<%# DataBinder.Eval(Container, "DataItem.BiddingEmitCode")%>,<%# DataBinder.Eval(Container, "DataItem.CreatUser").ToString()=="1"? "5":"2"%>)'>
						<%# DataBinder.Eval(Container, "DataItem.CreatUser").ToString()=="1"? "压价详情":"发标详情"%></A>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="回标明细">
				<ItemTemplate>
					<A href='#' onclick='javascript:ReadReturnModify(<%# DataBinder.Eval(Container, "DataItem.BiddingEmitCode") %>,<%# DataBinder.Eval(Container, "DataItem.CreatUser").ToString()=="1"? "6":"2"%>)'>
						<%# DataBinder.Eval(Container, "DataItem.CreatUser").ToString()=="1"? "回价详情":"回标详情"%></A>
				</ItemTemplate>
			</asp:TemplateColumn>
		</Columns>
		<PagerStyle CssClass="ListHeadTr"></PagerStyle>
	</asp:DataGrid></FONT>
<script>
	function ReadReturnModify(code,NowState)
	{
		OpenLargeWindow('BiddingReturnModifyPage.aspx?BiddingEmitCode='+code+'&State=view&NowState='+NowState,'显示回标信息');
	}
	function ReadBiddingEmit(code,NowState)
	{
		OpenLargeWindow('BiddingEmitManage.aspx?BiddingEmitCode='+code+'&State=view&NowState='+NowState,'显示发标信息');
	}
</script>
