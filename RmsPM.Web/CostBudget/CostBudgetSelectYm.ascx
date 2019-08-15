<%@ Control Language="c#" Inherits="RmsPM.Web.CostBudget.CostBudgetSelectYm" CodeFile="CostBudgetSelectYm.ascx.cs" %>
<SCRIPT language="javascript" src="../CostBudget/CostBudgetSelectYm.js" charset="gb2312"></SCRIPT>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
<input type="hidden" name="txtMaxYearsBetween" id="txtMaxYearsBetween" runat="server">
<span id="spanShowMonth" runat="server"><input type="checkbox" id="chkShowMonth" name="chkShowMonth" onclick="CostBudgetSelectYm_ShowMonthClick(this.ClientID, this);"
		runat="server"><span id="spanShowMonthTitle" runat="server"></span></span><span id="spanMonth" style="DISPLAY:none" runat="server">
	<igtxt:webdatetimeedit id="dtMonthStart" CssClass="infra-input-year" PromptChar=" " JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
		JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
		Width="70px" EditModeFormat="yyyy-MM" DisplayModeFormat="yyyy-MM" runat="server">
		<SPINBUTTONS EnsureFocus="True" Display="OnRight"></SPINBUTTONS>
	</igtxt:webdatetimeedit>жа
	<igtxt:webdatetimeedit id="dtMonthEnd" CssClass="infra-input-year" PromptChar=" " JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
		JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
		Width="70px" EditModeFormat="yyyy-MM" DisplayModeFormat="yyyy-MM" runat="server">
		<SPINBUTTONS EnsureFocus="True" Display="OnRight"></SPINBUTTONS>
	</igtxt:webdatetimeedit>
	&nbsp;&nbsp;<input type="button" value="от й╬" runat="server" id="btnGotoMonth" name="btnGotoMonth" class="button-small"
		onclick="if (!CostBudgetSelectYm_GotoMonth(this.ClientID)) return false;" onserverclick="btnGotoMonth_ServerClick"> </span>
<SCRIPT language="javascript">
<!--

function CostBudgetSelectYm_MyOnClientPost()
{
	if (!<%=MyOnClientPost%>) return false;
	
	return true;
}

CostBudgetSelectYm_ShowMonthClick('<%=ClientID%>', null);
//-->
</SCRIPT>
