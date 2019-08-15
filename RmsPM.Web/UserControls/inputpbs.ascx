<%@ Control Language="c#" Inherits="RmsPM.Web.UserControls.InputPBS" CodeFile="InputPBS.ascx.cs" %>
<SCRIPT language="javascript" src="../images/XmlCom.js"></SCRIPT>
<SCRIPT language="javascript" src="../UserControls/InputPBS.js"></SCRIPT>
<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
<span style="display:none"><input class="input" id="txtInput" type="text" size="12" name="txtInput" onfocus="InputPBS_CodeFocus(this, this.ClientID);"
	onblur="InputPBS_CodeBlur(this, this.ClientID);" onkeydown="if(event.keyCode==13) InputPBS_CodeBlur(this, this.ClientID);"
	runat="server" title="输入单位工程名称后按回车"> </span><span id="divName" runat="server"></span>
	<A href="#" onclick="InputPBS_SelectPBS('<%=ClientID%>');return false;"><IMG src="../images/ToolsItemSearch.gif" border="0"></A>&nbsp;<span id="divHint" style="COLOR:red" runat="server"></span>
<input id="txtOldValue" type="hidden" name="txtOldValue" runat="server"> <input id="txtHint" type="hidden" name="txtHint" runat="server">
<input id="txtCode" type="hidden" name="txtCode" runat="server"> <input id="txtName" type="hidden" name="txtName" runat="server">
<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server">
<input id="txtPBSType" type="hidden" name="txtPBSType" runat="server">

<script language="javascript">
<!--

function InputPBS_OnChange(ClientID)
{
	<%=m_LoadOnChange%>;
}

//-->
</script>
