<%@ Control Language="c#" Inherits="RmsPM.Web.PBS.UCBuildingFunctionList" CodeFile="UCBuildingFunctionList.ascx.cs" %>
<asp:datagrid id="dgList" runat="server" ShowFooter="True" AutoGenerateColumns="False" CellPadding="0"
	CssClass="list" Width="100%">
	<FooterStyle CssClass="list-title"></FooterStyle>
	<HeaderStyle CssClass="list-title"></HeaderStyle>
	<Columns>
		<asp:BoundColumn Visible="False" DataField="BuildingFunctionCode" ReadOnly="True" HeaderText="BuildingFunctionCode"></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="����" FooterText="�ϼ�">
			<ItemTemplate>
				<a href="#" onclick='doBFunction_<%=ClientID%>("<%# DataBinder.Eval(Container, "DataItem.BuildingFunctionCode") %>","SingleView");return false;'>
					<%# DataBinder.Eval(Container, "DataItem.FunctionName") %>
				</a>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="��Ԫ��">
			<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
			<ItemStyle HorizontalAlign="Right"></ItemStyle>
			<ItemTemplate>
				<%# DataBinder.Eval(Container, "DataItem.FunctionNum") %>
			</ItemTemplate>
			<FooterStyle HorizontalAlign="Right"></FooterStyle>
			<FooterTemplate>
				<asp:Label runat="server" ID="ftTotalFunctionNum"></asp:Label>
			</FooterTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="���(ƽ��)">
			<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
			<ItemStyle HorizontalAlign="Right"></ItemStyle>
			<ItemTemplate>
				<%# DataBinder.Eval(Container, "DataItem.FunctionArea") %>
			</ItemTemplate>
			<FooterStyle HorizontalAlign="Right"></FooterStyle>
			<FooterTemplate>
				<asp:Label runat="server" ID="ftTotalFunctionArea"></asp:Label>
			</FooterTemplate>
		</asp:TemplateColumn>
	</Columns>
</asp:datagrid>
<Script language="JavaScript" type="text/javascript">
<!--

function doBFunction_<%=ClientID%>(code,dotype){
	var NowTime = new Date();
	var strBuildingCode = Form1.txtBuildingCode.value;
	
	var strURL = './BuildingFunctionModify.aspx?BuildingCode=' + strBuildingCode;
	
	strURL += '&CellCode=' + code;
	
	strURL += '&DoType=' + dotype;
	
	strURL += '&ct_'+ NowTime.getFullYear().toString() + '_' + NowTime.getMonth().toString() + '_' + NowTime.getDay().toString() + '_' + NowTime.getHours().toString() + '_' + NowTime.getMinutes().toString() + '_' + NowTime.getSeconds().toString() + '_' + NowTime.getMilliseconds().toString();
	
	var theWin = OpenMiddleWindow(strURL,"BuildingFunctionModify");
	theWin.focus();
}

//-->
</Script>