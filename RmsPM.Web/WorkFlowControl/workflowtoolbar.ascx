<%@ Reference Page="~/workflowcontrol/workflowbackex.aspx" %>
<%@ Reference Page="~/workflowcontrol/workflowback.aspx" %>
<%@ Control Language="c#" Inherits="RmsPM.Web.WorkFlowControl.Migrated_WorkFlowToolbar"
    CodeFile="WorkFlowToolbar.ascx.cs" %>
<table cellpadding="0" cellspacing="0" border="0"><tr><td>
<!-- 页面显示元素 -->

<input class="button" id="btnSignIn" type="button" value=" 签 收 " name="btnSignIn"
    runat="server" onserverclick="btnSignIn_ServerClick" visible="false">
<input class="button" id="btnSave" type="button" value=" 保 存 " name="btnSave" runat="server"
    onserverclick="btnSave_ServerClick" visible="false">
<input class="button" id="btnSend" onclick="GetObjectInControl(this.ClientID,'btnHiddenForwardOpinion').onclick();selectRouterControl('Send','选择路由')" type="button"
    value=" 发 送 " name="btnSend" runat="server" visible="false">
<input class="button" id="btnBack" onclick="GetObjectInControl(this.ClientID,'btnHiddenForwardOpinion').onclick();selectBackControl('back','选择退回上一步')"
    type="button" value="退回上一步" name="btnBack" runat="server" visible="false">
<input class="button" id="btnBackTop" onclick="GetObjectInControl(this.ClientID,'btnHiddenForwardOpinion').onclick();selectBackControl('backTop','选择退回经办人')"
    type="button" value="退起草者" name="btnBackTop" runat="server" visible="false">
<input class="button" id="btnBackEx" onclick="GetObjectInControl(this.ClientID,'btnHiddenForwardOpinion').onclick();selectBackControl('backEx','选择退回')"
    type="button" value="退回．．" name="btnBackEx" runat="server" visible="false">
<input class="button" id="btnOpinion" onclick="GetObjectInControl(this.ClientID,'btnHiddenForwardOpinion').onclick();selectOpinionControl('Opinion','填写意见')"
    type="button" value="意见起草" name="btnOpinion" runat="server" visible="false">
<input class="button" id="btnMakeCopy" onclick="GetObjectInControl(this.ClientID,'btnHiddenForwardOpinion').onclick();selectRouterControl('MakeCopy','选择抄送')"
    type="button" value=" 抄 送 " name="btnMakeCopy" runat="server" visible="false">
<input class="button" id="btnTaskFinish" type="button" value=" 完 成 " name="btnTaskFinish"
    runat="server" onserverclick="btnTaskFinish_ServerClick" visible="false">
<input class="button" id="btnFinish" type="button" value=" 结 束 " name="btnFinish"
    runat="server" onserverclick="btnFinish_ServerClick" visible="false">
<input class="button" id="btnBlankOut" type="button" value=" 作 废 " name="" runat="server"
    onserverclick="btnBlankOut_ServerClick" visible="false">
<input class="button" id="btnDelete" type="button" value=" 删 除 " name="btnDelete"
    runat="server" onserverclick="btnDelete_ServerClick">
<input class="button" id="btnReturn" type="button" value=" 收 回 " name="btnReturn"
    runat="server" onserverclick="btnReturn_ServerClick" visible="false">
<input class="button" id="btnClose" onclick="javascript:window.close();" type="button"
    value="关闭窗口" runat="server">
<input class="button" id="btnPrint" onclick="javascript:DoPrint();return false;"
    type="button" value="打印" runat="server" visible="false">
<input class="button" id="btnPrintForm" type="button" onclick="javascript:DoPrintForm();return false;"
    value="审批表打印" runat="server" visible="false">
<!-- 非页面显示元素 -->
<span id="spanScript" runat="server"></span>
<input id="btnHiddenSend" style="display: none" type="button" value="发送提交" name="btnHiddenSend"
    runat="server" onserverclick="btnHiddenSend_ServerClick">
<input id="btnHiddenBack" style="display: none" type="button" value="退回提交" name="btnHiddenBack"
    runat="server" onserverclick="btnHiddenBack_ServerClick">
<input id="btnHiddenMakeCopy" style="display: none" type="button" value="抄送提交" name="btnHiddenMakeCopy"
    runat="server" onserverclick="btnHiddenMakeCopy_ServerClick">
<input id="btnHiddenOpinion" style="display: none" type="button" value="意见保存" name="btnHiddenOpinion"
    runat="server" onserverclick="btnHiddenOpinion_ServerClick">
<input id="btnHiddenForwardOpinion" style="display: none" type="button" value="意见保存" name="btnHiddenForwardOpinion"
    runat="server" onserverclick="btnHiddenForwardOpinion_ServerClick">
<input id="btnHiddenFinish" style="display: none" type="button" value=" 结 束 " name="btnFinish"
    runat="server" onserverclick="btnFinish_ServerClick" >
<!-- 操作使用隐藏域 -->
<input id="HiddenSelectRouterCode" type="hidden" name="HiddenSelectRouterCode" runat="server">
<input id="HiddenSelectUserCodes" type="hidden" name="HiddenSelectUserCodes" runat="server">
<input id="HiddenCopyUsers" type="hidden" name="HiddenCopyUser" runat="server">
<input id="HiddenWaitForFlag" type="hidden" name="HiddenWaitForFlag" runat="server">
<input id="HiddenCaseCode" type="hidden" name="HiddenCaseCode" runat="server">
<input id="HiddenFlowOpinion" type="hidden" name="HiddenFlowOpinion" runat="server">
<input id="HiddenChkShow" type="hidden" name="HiddenChkShow" runat="server">
<input id="HiddenRouterMessage" type="hidden" name="HiddenRouterMessage" runat="server">
<input id="HiddenChkMail" type="hidden" name="HiddenChkMail" runat="server">
<input id="HiddenNewFlow" type="hidden" name="HiddenNewFlow" runat="server">
<input id="HiddenIsAudit" type="hidden" name="HiddenIsAudit" runat="server" value="0">
<input id="HiddenAuditValue" type="hidden" name="HiddenAuditValue" runat="server">
<input id="HiddenRateValue" type="hidden" name="HiddenRateValue" runat="server">
<!-- 流程步骤说明以及状态选择 -->
<asp:Label ID="Label1" runat="server"></asp:Label>
</td>
<td><table border="0" cellpadding="0" cellspacing="0"><tr><td>
&nbsp; <span id="msgbutton" name="msgbutton" runat="server"><a href="#" onclick="workflowmsgfunction();">
    消息查看</a>
</span></td></tr></table>
</td><td><table border="0" cellpadding="0" cellspacing="0" runat="server" id="HandmadeTable">
        <tr>
            <td style="color: midnightblue">
                &nbsp; 手送资料：</td>
            <td style="color: midnightblue">
                <asp:RadioButtonList ID="RadioHandmade" runat="server" RepeatColumns="2">
                    <asp:ListItem>有</asp:ListItem>
                    <asp:ListItem>无</asp:ListItem>
                </asp:RadioButtonList>
                <asp:Label ID="HandMadeLabel" runat="server" Text=""></asp:Label></td>
        </tr>
    </table>
    </td><td>
<span id="FormNoSpan" runat="server"></span>
</td></tr></table>
<div id="msgdiv" style="display:none;" name="msgdiv" runat="server">
</div>
<span runat="server" id="scriptspan"></span>

<script language="javascript">
	function DoPrint()
	{
		OpenPrintWindow("../Report/PrintList.aspx?FromControlID=td_Print$wfcCaseState_divList", "打印");
	}
</script>

