<%@ Control Language="c#" Inherits="RmsPM.Web.WorkFlowOperation.Migrated_CheckControl" CodeFile="CheckControl.ascx.cs" %>
<div id="OperableDiv" runat="server" onkeydown="if(event.keyCode==13) event.keyCode=9">
	<asp:RadioButtonList ID="rdoContrachCheck" Runat="server" RepeatColumns="2">
		<asp:ListItem Value="Approve" Text="同意"></asp:ListItem>
		<asp:ListItem Value="Reject" Text="否决"></asp:ListItem>
	</asp:RadioButtonList>
</div>
<div id="EyeableDiv" runat="server">
	审批结果：<asp:Label ID="lblResult" Runat="server"></asp:Label>
</div>
