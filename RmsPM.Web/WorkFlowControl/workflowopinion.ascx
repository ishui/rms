<%@ Control Language="c#" Inherits="RmsPM.Web.WorkFlowControl.Migrated_WorkFlowOpinion" CodeFile="WorkFlowOpinion.ascx.cs" %>
&nbsp;<asp:Label id="lblOpinionNameClass" runat="server"></asp:Label>
<asp:Label id="lblOpinionName" runat="server"></asp:Label><input class="input" id="txtOpinion" type="text" size="15" name="txtOpinion" runat="server">
<asp:label id="lblOpinion" runat="server"></asp:label>
<div id="MatterDiv" runat="server">
	<TEXTAREA id="textareaOpinion" style="WIDTH: 100%; HEIGHT: 100%" class="textareaNoneBorder"
		name="textareaOpinion" rows="4" runat="server"></TEXTAREA>
	<div runat="server" style="MARGIN-LEFT: 6px; WIDTH: 100%; HEIGHT: 70px" id="divOpinion"></div>
	<div align="right" runat="server" id="OpinionUserAndDate">Ç©×Ö£º<Span runat="server" id="OpinionUser"></Span>&nbsp;&nbsp;&nbsp;&nbsp;ÈÕÆÚ£º<span runat="server" id="OpinionDate"></span>&nbsp;&nbsp;</div>
</div>
