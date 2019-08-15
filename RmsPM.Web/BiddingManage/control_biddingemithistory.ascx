<%@ Control Language="c#" Inherits="RmsPM.Web.BiddingManage.Control_BiddingEmitHistory" CodeFile="Control_BiddingEmitHistory.ascx.cs" %>
<FONT face="����">
	<asp:DataGrid id="DataGrid1" runat="server" AutoGenerateColumns="False" Width="100%" GridLines="Horizontal"
		CellPadding="0" CssClass="list">
		<FooterStyle CssClass="list-title"></FooterStyle>
		<HeaderStyle CssClass="list-title"></HeaderStyle>
		<Columns>
			<asp:TemplateColumn HeaderText="���">
				<ItemTemplate>
					<asp:Label runat="server"> 
						<%# Container.ItemIndex + 1%>
					</asp:Label>
				</ItemTemplate>
				<EditItemTemplate>
					<asp:TextBox runat="server"></asp:TextBox>
				</EditItemTemplate>
			</asp:TemplateColumn>
			<asp:BoundColumn DataField="EmitDate" HeaderText="����ʱ��" DataFormatString="{0:yyyy-MM-dd  HH:mm}"></asp:BoundColumn>
			<asp:BoundColumn DataField="PrejudicationDate" HeaderText="����ʱ��" DataFormatString="{0:yyyy-MM-dd HH:mm}"></asp:BoundColumn>
			<asp:BoundColumn DataField="EndDate" HeaderText="�ر�ʱ��" DataFormatString="{0:yyyy-MM-dd HH:mm}"></asp:BoundColumn>
			<asp:TemplateColumn HeaderText="������ϸ">
				<ItemTemplate>
					<A href='#' onclick='javascript:ReadBiddingEmit(<%# DataBinder.Eval(Container, "DataItem.BiddingEmitCode")%>,<%# DataBinder.Eval(Container, "DataItem.CreatUser").ToString()=="1"? "5":"2"%>)'>
						<%# DataBinder.Eval(Container, "DataItem.CreatUser").ToString()=="1"? "ѹ������":"��������"%></A>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="�ر���ϸ">
				<ItemTemplate>
					<A href='#' onclick='javascript:ReadReturnModify(<%# DataBinder.Eval(Container, "DataItem.BiddingEmitCode") %>,<%# DataBinder.Eval(Container, "DataItem.CreatUser").ToString()=="1"? "6":"2"%>)'>
						<%# DataBinder.Eval(Container, "DataItem.CreatUser").ToString()=="1"? "�ؼ�����":"�ر�����"%></A>
				</ItemTemplate>
			</asp:TemplateColumn>
		</Columns>
		<PagerStyle CssClass="ListHeadTr"></PagerStyle>
	</asp:DataGrid></FONT>
<script>
	function ReadReturnModify(code,NowState)
	{
		OpenLargeWindow('BiddingReturnModifyPage.aspx?BiddingEmitCode='+code+'&State=view&NowState='+NowState,'��ʾ�ر���Ϣ');
	}
	function ReadBiddingEmit(code,NowState)
	{
		OpenLargeWindow('BiddingEmitManage.aspx?BiddingEmitCode='+code+'&State=view&NowState='+NowState,'��ʾ������Ϣ');
	}
</script>
