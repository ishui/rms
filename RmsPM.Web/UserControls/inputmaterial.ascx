<%@ Control Language="C#" AutoEventWireup="true" CodeFile="inputmaterial.ascx.cs" Inherits="RmsPM.Web.UserControls.InputMaterial" %>
<SCRIPT language="javascript" src="<%=imagePath%>XmlCom.js"></SCRIPT>
<SCRIPT language="javascript" src="<%=imagePath%>../UserControls/inputmaterial.js"></SCRIPT>

<span style="display:none"><input class="input" id="txtInput" type="text" size="12" name="txtInput" onfocus="InputMaterial_CodeFocus(this, this.ClientID);"
	onblur="InputMaterial_CodeBlur(this, this.ClientID);" onkeydown="if(event.keyCode==13) InputMaterial_CodeBlur(this, this.ClientID);"
	runat="server" title="ÊäÈë²ÄÁÏ"> </span><span id="divName" runat="server"></span>
	<A href="#" onclick="InputMaterialType_SelectMaterial('<%=ClientID%>','<%=imagePath%>');return false;">
	<IMG src="<%=imagePath%>ToolsItemSearch.gif" border="0"></A>&nbsp;<span id="divHint" style="COLOR:red" runat="server"></span>
<input id="txtOldValue" type="hidden" name="txtOldValue" runat="server"> <input id="txtHint" type="hidden" name="txtHint" runat="server">
<input id="txtCode" type="hidden" name="txtCode" runat="server">
<input id="txtName" type="hidden" name="txtName" runat="server"><input id="txtunit" type="hidden" name="txtUnit" runat="server">
<input id="txtspec" type="hidden" name="txtspec" runat="server"><input id="txtOutQty" type="hidden" name="txtOutQty" runat="server">
 <input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server">