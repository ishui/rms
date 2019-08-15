<%@ Control Language="c#" Inherits="RmsPM.Web.UserControls.InputCostBudgetDtl" CodeFile="InputCostBudgetDtl.ascx.cs" %>
<SCRIPT language="javascript" src="../images/XmlCom.js"></SCRIPT>
<SCRIPT language="javascript" src="../UserControls/InputCostBudgetDtl.js" charset="gb2312"></SCRIPT>
<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
<input class="input" id="txtInput" onkeydown="if(event.keyCode==13) InputCostBudgetDtl_CodeBlur(this, this.ClientID);"
	onblur="InputCostBudgetDtl_CodeBlur(this, this.ClientID);" title="输入费用项编号后按回车" style="DISPLAY: none"
	onfocus="InputCostBudgetDtl_CodeFocus(this, this.ClientID);" type="text" size="12"
	name="txtInput" runat="server"><span id="divName" runat="server"></span> <span id="div_SearchButton" runat="server">
	<A onclick="InputCostBudgetDtl_SelectCost('<%=ClientID%>');return false;" href="#" >
		<IMG src="../images/ToolsItemSearch.gif" border="0"></A></span>&nbsp; <span id="divHint" style="COLOR: red" runat="server">
</span><span id="divDesc" runat="server"></span><input id="txtOldValue" type="hidden" name="txtOldValue" runat="server">
<input id="txtHint" type="hidden" name="txtHint" runat="server"> <input id="txtCode" type="hidden" name="txtCode" runat="server">
<input id="txtName" type="hidden" name="txtName" runat="server"> <input id="txtFullName" type="hidden" name="txtName" runat="server">
<input id="txtSortID" type="hidden" name="txtSortID" runat="server"> <input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server">
<input id="txtSelectAllLeaf" type="hidden" name="txtSelectAllLeaf" runat="server">
<input id="txtCostBudgetSetCode" type="hidden" name="txtCostBudgetSetCode" runat="server">
<input id="txtPBSType" type="hidden" name="txtPBSType" runat="server"> <input id="txtPBSCode" type="hidden" name="txtPBSCode" runat="server">
<input id="txtPBSName" type="hidden" name="txtPBSName" runat="server"> <input id="txtDesc" type="hidden" name="txtDesc" runat="server">
<asp:CheckBox ID="hid_Enable" Runat="server" Visible="False" Checked="True"></asp:CheckBox>
