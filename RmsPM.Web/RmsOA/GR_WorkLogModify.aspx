<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GR_WorkLogModify.aspx.cs"
    Inherits="RmsOA_GR_WorkLogModify" ValidateRequest="false" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>   
<%@ Register Assembly="AspWebControl" Namespace="AspWebControl" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<style type="text/css">
 body
 {
 	margin: 0px;
	scrollbar-face-color: #E4EFF6;
	scrollbar-arrow-color: #266290;
	scrollbar-shadow-color: #A1A7AB;
	scrollbar-highlight-color: #E4EFF6;
	scrollbar-3dlight-color: #B0C4DE;
	scrollbar-darkshadow-color: #B6D2E7;
	scrollbar-track-color: #D7E9F8;
	background: url(../Images/bg.jpg) no-repeat right bottom;
 }
 td {
	font-size: 12px;
	line-height: 150%;
	font-family: "Tahoma","宋体"
}
 .button {
	font-size: 12px;
	font-weight: bold;
	color: #FFFFFF;
	background-image: url(../Images/bn.gif);
	cursor: hand;
	border: 1px solid #333333;
	padding-top: 3px;
	height: 21px;
}
.form {
	border-top: 3px solid #D2B48C;
	border-bottom: 1px solid #D2B48C;
	background-color: #FFFFFF;
	border-collapse: collapse;
	font-size:9pt;
}
.form-item {
	background-color: #FFEBCD;
	text-align: right;
	border-top: 1px solid #93ACDB;
}
.tools-area {
	background-color: #E5F1FA;
	border-bottom: 1px solid #4C5F6E;
	padding: 7px 0 5px 10px;
	height:35px
}
.topic {
	font-weight: bold;
	color: #FFFFFF;
	font-size:9pt;
}
</style>
    <title>工作日记</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td background="../images/topic_bg.gif" class="topic" style="height: 25px; text-align: center;">
                        工作日志</td>
                </tr>
            </table>
            <asp:FormView ID="FormView1" runat="server" Width="100%" DataSourceID="ObjectDataSource1"
                OnDataBound="FormView1_DataBound">
                <EditItemTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="tools-area" style="width: 100%; height: 25px;" valign="top">
                                <img align="absMiddle" alt="" src="../images/btn_li.gif">
                                <asp:Button ID="ButtonUpdate" runat="server" Text="更新" CssClass="button" OnClick="btnUpdate_Click" />
                                <asp:Button ID="ButtonCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="取消" CssClass="button" />
                                <input type="button" class="button" value="关闭" onclick="self.close()" />
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                        <tr style="background-color: #FFEBCD;">
                            <td style="width: 80px;">
                                &nbsp;写作日期：
                            </td>
                            <td>
                                <cc1:Calendar ID="DayWrited" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                    Value='<%# Bind("DayWrited") %>'>
                                </cc1:Calendar>
                            </td>
                            <td>
                                天气：
                            </td>
                            <td >
                                <asp:DropDownList ID="WeatherDropDownList" runat="server" DataSourceID="ObjectDataSource2"
                                    Font-Size="9pt" SelectedValue='<%# Bind("Weather") %>'>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 80px;">
                                心情：
                            </td>
                            <td>
                                <asp:DropDownList ID="MoodDropDownList" runat="server" DataSourceID="ObjectDataSource3"
                                    Font-Size="9pt" SelectedValue='<%# Bind("Mood") %>'>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="height: 100%;">
                            <td colspan="6">
                                <FTB:FreeTextBox ID="ContextTextBox" runat="server" ButtonPath="../images/ftb/office2003/"
                                    HelperFilesPath="../HelperScripts" Text='<%# Bind("Context") %>' Width="100%">
                                </FTB:FreeTextBox>
                            </td>
                        </tr>
                    </table>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <table border="0" width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="tools-area" style="width: 100%; height: 25px;" valign="top">
                                <img align="absMiddle" alt="" src="../images/btn_li.gif">
                                <asp:Button ID="ButtonAdd" runat="server" Text="添加" CssClass="button" OnClick="btAdd_Click" />
                                <input type="reset" class="button" value="重置" />
                                <input type="button" class="button" value="关闭" onclick="self.close()" />
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                        <tr style="background-color: #FFEBCD;">
                            <td style="width: 80px;">
                                写作日期：
                            </td>
                            <td>
                                <cc1:Calendar ID="DayWrited" runat="server" CalendarMode="date" CalendarResource="../Images/CalendarResource/"
                                    Value="">
                                </cc1:Calendar>
                            </td>
                            <td style="width: 40px;">
                                天气：
                            </td>
                            <td style="width: 80px;">
                                <asp:DropDownList ID="WeatherDropDownList" runat="server" DataSourceID="ObjectDataSource2">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 80px;">
                                心情：
                            </td>
                            <td>
                                <asp:DropDownList ID="MoodDropDownList" runat="server" DataSourceID="ObjectDataSource3">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="height:100%;">
                            <td colspan="6">
                                <FTB:FreeTextBox ID="ContextTextBox" runat="server" ButtonPath="../images/ftb/office2003/"
                                    HelperFilesPath="../HelperScripts" Width="100%">
                                </FTB:FreeTextBox>
                            </td>
                        </tr>
                    </table>
                </InsertItemTemplate>
                <ItemTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="tools-area" style="width: 100%; height: 25px;" valign="top">
                                <img align="absMiddle" alt="" src="../images/btn_li.gif">
                                <asp:Button ID="ButtonDelete" runat="server" Text="删除" CssClass="button" OnClick="ButtonDelete_Click" />
                                <asp:Button ID="ButtonEdit" runat="server" Text="编辑" CssClass="button" OnClick="ButtonEdit_Click" />
                                <input type="button" value="关闭" class="button" onclick="self.close()" />
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                        <tr style="background-color: #FFEBCD;">
                            <td style="width: 230px;">
                                写作日期：
                                <asp:Label ID="DayWritedLabel" runat="server" Text='<%# Bind("DayWrited") %>'></asp:Label>
                            </td>
                            <td style="width: 100px;">
                                天气：
                                <asp:Label ID="WeatherLabel" runat="server" Text='<%# Bind("Weather") %>'></asp:Label>
                            </td>
                            <td style="width: 100px;">
                                心情：
                                <asp:Label ID="MoodLabel" runat="server" Text='<%# Bind("Mood") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                            <p>
                                <asp:Label ID="ContextLabel" runat="server" Text='<%# Bind("Context") %>' Width="100%"></asp:Label>
                            </p>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsOA.MODEL.GK_OA_WorkLogModel"
                DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetGK_OA_WorkLogListOne" TypeName="RmsOA.BFL.GK_OA_WorkLogBFL"
                UpdateMethod="Update">
                <SelectParameters>
                    <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetWeatherType"
                TypeName="RmsOA.BFL.GK_OA_WorkLogBFL"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="GetMoodType"
                TypeName="RmsOA.BFL.GK_OA_WorkLogBFL"></asp:ObjectDataSource>
        </div>
    </form>
</body>
</html>
