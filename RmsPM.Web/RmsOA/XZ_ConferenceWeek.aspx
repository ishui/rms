<%@ Page Language="C#" MasterPageFile="~/RmsOA/XZ_ConferenceMasterPage.master"
    AutoEventWireup="true" CodeFile="XZ_ConferenceWeek.aspx.cs" Inherits="RmsOA_XZ_ConferenceWeek"
    Title="本周会议" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <script src="../Rms.js" type="text/javascript"></script>
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td background="../images/topic_bg.gif" class="topic" style="height: 25px; text-align: center;">
                        会议管理>本周会议列表</td>
                </tr>
            </table>
        </div>
        <div>
            <div style="text-align: center; font-size: 9pt;">
                <span style="color: #FAA210; font-weight: bolder;">
                    <% WirteYear(); %>
                </span>年第<span style="color: #FAA210; font-weight: bolder;"><% WriteWeekIndex(); %></span>周会议安排（<span
                    style="color: #FAA210; font-weight: bolder;"><%WriteWeekTime(); %></span>）
            </div>
            <table class="table" width="100%">
                <tr>
                    <td class="tools-area" style="width: 16px;">
                        <img align="absMiddle" src="../images/btn_li.gif" /></td>
                    <td class="tools-area">
                        <asp:Button ID="WeekButton" runat="server" CssClass="button" Text="本周" OnClick="WeekButton_Click" />
                        <asp:Button ID="PreButton" runat="server" CssClass="button" Text="上周" OnClick="PreButton_Click"/>
                        <asp:Button ID="NextWeekButtom" runat="server" CssClass="button" Text="下周" OnClick="NextWeekButtom_Click" />
                    </td>
                </tr>
            </table>
            <table width="100%" class="list">
                <thead>
                    <tr class="list-title">
                        <td>
                            日期</td>
                        <td>
                            会议主题</td>
                        <td>
                            主办单位</td>
                        <td>
                            会议类型</td>
                        <td>
                            会议地点</td>
                        <td>
                            会议时间</td>
                    </tr>
                </thead>
                <tbody>
                    <% WriteMeetContent(); %>
                </tbody>
            </table>
        </div>
</asp:Content>
