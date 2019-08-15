<%@ Reference Page="~/workflowcontrol/workflowbackex.aspx" %>
<%@ Reference Page="~/workflowcontrol/workflowback.aspx" %>
<%@ Control Language="c#" Inherits="RmsPM.Web.WorkFlowControl.Migrated_WorkFlowToolbar"
    CodeFile="WorkFlowToolbar.ascx.cs" %>
<table cellpadding="0" cellspacing="0" border="0"><tr><td>
<!-- ҳ����ʾԪ�� -->

<input class="button" id="btnSignIn" type="button" value=" ǩ �� " name="btnSignIn"
    runat="server" onserverclick="btnSignIn_ServerClick" visible="false">
<input class="button" id="btnSave" type="button" value=" �� �� " name="btnSave" runat="server"
    onserverclick="btnSave_ServerClick" visible="false">
<input class="button" id="btnSend" onclick="GetObjectInControl(this.ClientID,'btnHiddenForwardOpinion').onclick();selectRouterControl('Send','ѡ��·��')" type="button"
    value=" �� �� " name="btnSend" runat="server" visible="false">
<input class="button" id="btnBack" onclick="GetObjectInControl(this.ClientID,'btnHiddenForwardOpinion').onclick();selectBackControl('back','ѡ���˻���һ��')"
    type="button" value="�˻���һ��" name="btnBack" runat="server" visible="false">
<input class="button" id="btnBackTop" onclick="GetObjectInControl(this.ClientID,'btnHiddenForwardOpinion').onclick();selectBackControl('backTop','ѡ���˻ؾ�����')"
    type="button" value="�������" name="btnBackTop" runat="server" visible="false">
<input class="button" id="btnBackEx" onclick="GetObjectInControl(this.ClientID,'btnHiddenForwardOpinion').onclick();selectBackControl('backEx','ѡ���˻�')"
    type="button" value="�˻أ���" name="btnBackEx" runat="server" visible="false">
<input class="button" id="btnOpinion" onclick="GetObjectInControl(this.ClientID,'btnHiddenForwardOpinion').onclick();selectOpinionControl('Opinion','��д���')"
    type="button" value="������" name="btnOpinion" runat="server" visible="false">
<input class="button" id="btnMakeCopy" onclick="GetObjectInControl(this.ClientID,'btnHiddenForwardOpinion').onclick();selectRouterControl('MakeCopy','ѡ����')"
    type="button" value=" �� �� " name="btnMakeCopy" runat="server" visible="false">
<input class="button" id="btnTaskFinish" type="button" value=" �� �� " name="btnTaskFinish"
    runat="server" onserverclick="btnTaskFinish_ServerClick" visible="false">
<input class="button" id="btnFinish" type="button" value=" �� �� " name="btnFinish"
    runat="server" onserverclick="btnFinish_ServerClick" visible="false">
<input class="button" id="btnBlankOut" type="button" value=" �� �� " name="" runat="server"
    onserverclick="btnBlankOut_ServerClick" visible="false">
<input class="button" id="btnDelete" type="button" value=" ɾ �� " name="btnDelete"
    runat="server" onserverclick="btnDelete_ServerClick">
<input class="button" id="btnReturn" type="button" value=" �� �� " name="btnReturn"
    runat="server" onserverclick="btnReturn_ServerClick" visible="false">
<input class="button" id="btnClose" onclick="javascript:window.close();" type="button"
    value="�رմ���" runat="server">
<input class="button" id="btnPrint" onclick="javascript:DoPrint();return false;"
    type="button" value="��ӡ" runat="server" visible="false">
<input class="button" id="btnPrintForm" type="button" onclick="javascript:DoPrintForm();return false;"
    value="�������ӡ" runat="server" visible="false">
<!-- ��ҳ����ʾԪ�� -->
<span id="spanScript" runat="server"></span>
<input id="btnHiddenSend" style="display: none" type="button" value="�����ύ" name="btnHiddenSend"
    runat="server" onserverclick="btnHiddenSend_ServerClick">
<input id="btnHiddenBack" style="display: none" type="button" value="�˻��ύ" name="btnHiddenBack"
    runat="server" onserverclick="btnHiddenBack_ServerClick">
<input id="btnHiddenMakeCopy" style="display: none" type="button" value="�����ύ" name="btnHiddenMakeCopy"
    runat="server" onserverclick="btnHiddenMakeCopy_ServerClick">
<input id="btnHiddenOpinion" style="display: none" type="button" value="�������" name="btnHiddenOpinion"
    runat="server" onserverclick="btnHiddenOpinion_ServerClick">
<input id="btnHiddenForwardOpinion" style="display: none" type="button" value="�������" name="btnHiddenForwardOpinion"
    runat="server" onserverclick="btnHiddenForwardOpinion_ServerClick">
<input id="btnHiddenFinish" style="display: none" type="button" value=" �� �� " name="btnFinish"
    runat="server" onserverclick="btnFinish_ServerClick" >
<!-- ����ʹ�������� -->
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
<!-- ���̲���˵���Լ�״̬ѡ�� -->
<asp:Label ID="Label1" runat="server"></asp:Label>
</td>
<td><table border="0" cellpadding="0" cellspacing="0"><tr><td>
&nbsp; <span id="msgbutton" name="msgbutton" runat="server"><a href="#" onclick="workflowmsgfunction();">
    ��Ϣ�鿴</a>
</span></td></tr></table>
</td><td><table border="0" cellpadding="0" cellspacing="0" runat="server" id="HandmadeTable">
        <tr>
            <td style="color: midnightblue">
                &nbsp; �������ϣ�</td>
            <td style="color: midnightblue">
                <asp:RadioButtonList ID="RadioHandmade" runat="server" RepeatColumns="2">
                    <asp:ListItem>��</asp:ListItem>
                    <asp:ListItem>��</asp:ListItem>
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
		OpenPrintWindow("../Report/PrintList.aspx?FromControlID=td_Print$wfcCaseState_divList", "��ӡ");
	}
</script>

