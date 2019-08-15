<%@ Control Language="c#" Inherits="RmsPM.Web.PBS.UCBuildingStationList" CodeFile="UCBuildingStationList.ascx.cs" %>
<asp:datagrid id="dgList" runat="server" ShowFooter="True" AutoGenerateColumns="False" CellPadding="0"
	CssClass="list" Width="100%">
	<FooterStyle CssClass="list-title"></FooterStyle>
	<HeaderStyle CssClass="list-title"></HeaderStyle>
	<Columns>
		<asp:BoundColumn Visible="False" DataField="BuildingStationCode" ReadOnly="True" HeaderText="BuildingStationCode"></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="名称" FooterText="合计">
			<ItemTemplate>
				<a href="#" onclick='doBStation_<%=ClientID%>("<%# DataBinder.Eval(Container, "DataItem.BuildingStationCode") %>","SingleView");return false;'>
					<%# DataBinder.Eval(Container, "DataItem.StationName") %>
				</a>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="单元数">
			<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
			<ItemStyle HorizontalAlign="Right"></ItemStyle>
			<ItemTemplate>
				<%# DataBinder.Eval(Container, "DataItem.StationNum") %>
			</ItemTemplate>
			<FooterStyle HorizontalAlign="Right"></FooterStyle>
			<FooterTemplate>
				<asp:Label runat="server" ID="ftTotalStationNum"></asp:Label>
			</FooterTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="面积(平米)">
			<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
			<ItemStyle HorizontalAlign="Right"></ItemStyle>
			<ItemTemplate>
				<%# DataBinder.Eval(Container, "DataItem.StationArea") %>
			</ItemTemplate>
			<FooterStyle HorizontalAlign="Right"></FooterStyle>
			<FooterTemplate>
				<asp:Label runat="server" ID="ftTotalStationArea"></asp:Label>
			</FooterTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="可销售面积(平米)">
			<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
			<ItemStyle HorizontalAlign="Right"></ItemStyle>
			<ItemTemplate>
				<%# DataBinder.Eval(Container, "DataItem.AreaForVolumeRate") %>
			</ItemTemplate>
			<FooterStyle HorizontalAlign="Right"></FooterStyle>
			<FooterTemplate>
				<asp:Label runat="server" ID="ftTotalAreaForVolumeRate"></asp:Label>
			</FooterTemplate>
		</asp:TemplateColumn>
	</Columns>
</asp:datagrid>
<Script language="JavaScript" type="text/javascript">
<!--

function doBStation_<%=ClientID%>(code,dotype){
	var NowTime = new Date();
	var strBuildingCode = Form1.txtBuildingCode.value;
	
	var strURL = './BuildingStationModify.aspx?BuildingCode=' + strBuildingCode;
	
	strURL += '&CellCode=' + code;
	
	strURL += '&DoType=' + dotype;
	
	strURL += '&ct_'+ NowTime.getFullYear().toString() + '_' + NowTime.getMonth().toString() + '_' + NowTime.getDay().toString() + '_' + NowTime.getHours().toString() + '_' + NowTime.getMinutes().toString() + '_' + NowTime.getSeconds().toString() + '_' + NowTime.getMilliseconds().toString();
	
	var theWin = OpenMiddleWindow(strURL,"BuildingStationModify");
	theWin.focus();
}

//-->
</Script>
