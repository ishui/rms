<%@ Control Language="C#" AutoEventWireup="true" CodeFile="inputmaterialin.ascx.cs" Inherits="RmsPM.Web.UserControls.InputMaterialin" %>
<SCRIPT language="javascript" src="<%=imagePath%>XmlCom.js"></SCRIPT>
<SCRIPT language="javascript" src="<%=imagePath%>../UserControls/inputmaterialin.js"></SCRIPT>
<span style="display:none"><input class="input" id="txtInput" type="text" size="12" name="txtInput" onfocus="inputmaterialin_CodeFocus(this, this.ClientID);"
	onblur="inputmaterialin_CodeBlur(this, this.ClientID);" onkeydown="if(event.keyCode==13) inputmaterialin_CodeBlur(this, this.ClientID);"
	runat="server" title="ÊäÈë²ÄÁÏ"> </span><span id="divName" runat="server"></span>
	<A href="#" onclick="inputmaterialinType_selectmaterialin('<%=ClientID%>','<%=imagePath%>',<%=Request["ProjectCode"]%>);return false;">
	<IMG src="<%=imagePath%>ToolsItemSearch.gif" border="0"></A>&nbsp;<span id="divHint" style="COLOR:red" runat="server"></span>
<input id="txtOldValue" type="hidden" name="txtOldValue" runat="server"> <input id="txtHint" type="hidden" name="txtHint" runat="server">
<input id="txtCode" type="hidden" name="txtCode" title="" runat="server">
<input id="txtName" type="hidden" name="txtName" runat="server"><input id="txtMaterialCode" type="hidden" name="txtMaterialCode" runat="server">
 <input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"><input id="txtOutPrice" type="hidden" name="txtOutPrice" runat="server">
