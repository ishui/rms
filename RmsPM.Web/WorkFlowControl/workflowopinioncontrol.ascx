<%@ Control Language="c#" Inherits="RmsPM.Web.WorkFlowControl.WorkFlowOpinionControl" CodeFile="WorkFlowOpinionControl.ascx.cs" %>
<span id="titlespan" runat="server"></span><span id="contentspan" runat="server"></span>
<input id="txtSelectRouterCode" type="hidden" name="txtSelectRouterCode" runat="server">
<input id="txtSelectUserCodes" type="hidden" name="txtSelectUserCodes" runat="server">
<input id="HiddenOption" type="hidden" name="HiddenOption" runat="server"> <input id="hiddenOpinionText" type="hidden" name="hiddenOpinionText" runat="server">
<input id="btnSubmitOpinion" style="DISPLAY: none" type="button" value="提交会签意见" name="btnSubmitOpinion"
	runat="server" onserverclick="btnSubmitOpinion_ServerClick"> <input id="btnSendOpinion" style="DISPLAY: none" type="button" value="发送按钮隐藏事件" name="btnSendOpinion"
	runat="server" onserverclick="btnSendOpinion_ServerClick"><INPUT id="btnEndOpinion" style="DISPLAY: none" type="button" value="完成按钮隐藏事件" name="btnEndOpinion"
	runat="server" onserverclick="btnEndOpinion_ServerClick"><input id="HiddenCopyUsers" type="hidden" name="HiddenCopyUser" runat="server"> 
