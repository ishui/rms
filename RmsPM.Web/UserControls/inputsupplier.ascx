<%@ Control Language="c#" Inherits="RmsPM.Web.UserControls.InputSupplier" CodeFile="InputSupplier.ascx.cs" %>
<SCRIPT language="javascript" src="<%=imagePath%>XmlCom.js"></SCRIPT>
<SCRIPT language="javascript" src="<%=imagePath%>../UserControls/InputSupplier.js"></SCRIPT>
<input class="input" id="txtInput" type="text" name="txtInput" onfocus="InputSupplier_CodeFocus(this, this.ClientID);"
	onblur="InputSupplier_CodeBlur(this, this.ClientID);" onkeydown="if(event.keyCode==13) {InputSupplier_CodeBlur(this, this.ClientID);event.keyCode=9;}"
	runat="server" title="输入编号按回车" style="width: 140px"> <A href="#" onclick="InputSupplier_Select('<%=ClientID%>','<%=imagePath%>');return false;">
	<IMG src="<%=imagePath%>ToolsItemSearch.gif" border="0"></A> &nbsp;<span id="divName" runat="server"></span>
&nbsp;<span id="divHint" style="COLOR:red" runat="server"></span> <input id="txtOldValue" type="hidden" name="txtOldValue" runat="server">
<input id="txtHint" type="hidden" name="txtHint" runat="server"> <input id="txtCode" type="hidden" name="txtCode" runat="server">
<input id="txtName" type="hidden" name="txtName" runat="server"> <input id="txtClassCode" type="hidden" name="txtClassCode" runat="server">
<input id="txtSortID" type="hidden" name="txtSortID" runat="server"> <input id="txtClientID" type="hidden" name="txtClientID" runat="server">
<script language="javascript">
function GetInputSupplierClientID ( )
{
	return '<%=ClientID%>';
}
</script>