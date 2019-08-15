<%@ Control Language="c#" Inherits="RmsPM.Web.WorkFlowControl.Migrated_WorkFlowFormOpinion"
    CodeFile="WorkFlowFormOpinion.ascx.cs" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ImageSign" Src="../WorkFlowControl/WorkFlowFormSign.ascx" %>
&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="OpinionTitle" runat="server"></asp:Label>
<input class="input" id="OpinionText" type="text" size="15" style="width: 100%" name="OpinionText"
    runat="server">
<asp:Label ID="OpinionLabel" runat="server"></asp:Label>
<igtxt:WebNumericEdit ID="OpinionNum" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember"
    ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
    JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
    Width="120px">
</igtxt:WebNumericEdit>
<div runat="server" id="OpinionDiv">
    <textarea id="OpinionTextArea" style="width: 100%; height: 100%" class="textareaNoneBorder"
        name="OpinionTextArea" runat="server" rows="4"></textarea>
    <div runat="server" style="margin-left: 6px; width: 100%; height: 35px" id="OpinionTextAreaDiv">
    </div>
    <div align="right" runat="server" id="OpinionUserAndDate">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td valign="bottom">
                    <select id="sltTemplateOpinion" runat="server" visible="false">
                    </select>
                </td>
                <td valign="bottom">
                    <asp:RadioButtonList ID="rdoCheck" runat="server" RepeatColumns="3">
                        <asp:ListItem Value="Approve" Text="同意"></asp:ListItem>
                        <asp:ListItem Value="Reject" Text="否决"></asp:ListItem>
                        <asp:ListItem Value="" Text="待选择" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    <span id="CheckSpan" runat="server"></span>&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td>
                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td>
                                <uc1:ImageSign ID="wfsImageSign" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                签字：<span runat="server" id="OpinionUser"></span>&nbsp;&nbsp;&nbsp;&nbsp;日期：<span
                                    runat="server" id="OpinionDate"></span>&nbsp;&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</div>
