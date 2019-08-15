<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZS_PersonWorkEdit.aspx.cs"
    Inherits="PersonalManage_ZS_PersonWorkEdit" %>

<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>工作经历情况</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript" src="../Rms.js"></script>

    <script type="text/javascript" language="javascript">
        function SelectUnit()
		{
			OpenSmallWindow("../SelectBox/SelectUnit.aspx?UnitCode=000000");
		}
		function SelectUnitReturn(code, name)
		{
			window.document.all.FormView1_txtUnitName.value = name;
			window.document.all.FormView1_txtUnit.value = code;
		}	
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td height="25">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                                工作经历情况</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%"
                        OnItemInserted="FormView1_ItemInserted" OnItemDeleted="FormView1_ItemDeleted"
                        OnItemUpdated="FormView1_ItemUpdated" OnItemInserting="FormView1_ItemInserting"
                        DataKeyNames="Code">
                        <EditItemTemplate>
                            <table id="Table2" class="table" width="100%">
                                <tr>
                                    <td class="tools-area" width="16">
                                        <img align="absMiddle" src="../images/btn_li.gif" /></td>
                                    <td class="tools-area">
                                        <asp:Button ID="btnSave" runat="server" CommandName="Update" CssClass="button" Text=" 保存 " />&nbsp;
                                        <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                            type="button" value=" 关闭 " />
                                    </td>
                                </tr>
                            </table>
                            <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                                <tr>
                                    <td class="form-item">
                                        开始时间：</td>
                                    <td>
                                        <cc3:Calendar ID="Calendar6" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("BeginDate") %>'>
                                        </cc3:Calendar>
                                    </td>
                                    <td class="form-item">
                                        结束时间：</td>
                                    <td>
                                        <cc3:Calendar ID="Calendar1" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("EndDate") %>'>
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        就职单位：</td>
                                    <td>
                                        <asp:TextBox ID="WorkUnitTextBox" runat="server" Text='<%# Bind("WorkUnit") %>' CssClass="input"></asp:TextBox></td>
                                    <td class="form-item">
                                        职务：</td>
                                    <td>
                                        <asp:TextBox ID="JobPostTextBox" runat="server" Text='<%# Bind("JobPost") %>' CssClass="input"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        职责：</td>
                                    <td>
                                        <asp:TextBox ID="DutyTextBox" runat="server" Text='<%# Bind("Duty") %>' CssClass="input"></asp:TextBox></td>
                                    <td class="form-item">
                                        证明人：</td>
                                    <td>
                                        <asp:TextBox ID="TextBoxCertifier" runat="server" Text='<%# Bind("Certifier") %>'
                                            CssClass="input"></asp:TextBox></td>
                                </tr>
                            </table>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <table id="Table1" class="table" width="100%">
                                <tr>
                                    <td class="tools-area" width="16">
                                        <img align="absMiddle" src="../images/btn_li.gif" /></td>
                                    <td class="tools-area">
                                        <asp:Button ID="btnSave" runat="server" CommandName="Insert" CssClass="button" Text=" 保存 " />
                                        <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                            type="button" value=" 关闭 " />
                                    </td>
                                </tr>
                            </table>
                            <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                                <tr>
                                    <td class="form-item">
                                        开始时间：</td>
                                    <td>
                                        <cc3:Calendar ID="Calendar6" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("BeginDate") %>'>
                                        </cc3:Calendar>
                                    </td>
                                    <td class="form-item">
                                        结束时间：</td>
                                    <td>
                                        <cc3:Calendar ID="Calendar1" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("EndDate") %>'>
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        就职单位：</td>
                                    <td>
                                        <asp:TextBox ID="WorkUnitTextBox" runat="server" Text='<%# Bind("WorkUnit") %>' CssClass="input"></asp:TextBox></td>
                                    <td class="form-item">
                                        职务：</td>
                                    <td>
                                        <asp:TextBox ID="JobPostTextBox" runat="server" Text='<%# Bind("JobPost") %>' CssClass="input"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        职责：</td>
                                    <td>
                                        <asp:TextBox ID="DutyTextBox" runat="server" Text='<%# Bind("Duty") %>' CssClass="input"></asp:TextBox></td>
                                    <td class="form-item">
                                        证明人：</td>
                                    <td>
                                        <asp:TextBox ID="TextBoxCertifier" runat="server" Text='<%# Bind("Certifier") %>'
                                            CssClass="input"></asp:TextBox></td>
                                </tr>
                            </table>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <table id="Table3" class="table" width="100%">
                                <tr>
                                    <td class="tools-area" width="16">
                                        <img align="absMiddle" src="../images/btn_li.gif" /></td>
                                    <td class="tools-area">
                                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" CssClass="button" Text=" 修改 " />
                                        <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" CssClass="button"
                                            Text=" 删除 " />
                                        &nbsp;
                                        <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                            type="button" value=" 关闭 " />
                                    </td>
                                </tr>
                            </table>
                            <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                                <tr>
                                    <td class="form-item">
                                        开始时间：</td>
                                    <td>
                                        <asp:Label ID="BeginDateLabel" runat="server" Text='<%# Bind("BeginDate") %>'></asp:Label></td>
                                    <td class="form-item">
                                        结束时间：</td>
                                    <td>
                                        <asp:Label ID="EndDateLabel" runat="server" Text='<%# Bind("EndDate") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        就职单位：</td>
                                    <td>
                                        <asp:Label ID="WorkUnitLabel" runat="server" Text='<%# Bind("WorkUnit") %>'></asp:Label></td>
                                    <td>
                                        <td class="form-item">
                                            职务：</td>
                                        <td>
                                            <asp:Label ID="JobPostLabel" runat="server" Text='<%# Bind("JobPost") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        职责：</td>
                                    <td>
                                        <asp:Label ID="DutyLabel" runat="server" Text='<%# Bind("Duty") %>'></asp:Label></td>
                                    <td class="form-item">
                                        证明人：</td>
                                    <td>
                                        <asp:Label ID="LabelCertifier" runat="server" Text='<%# Bind("Certifier") %>'></asp:Label></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsOA.MODEL.GK_OAPersonWorkModel"
                        DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetGK_OAPersonWorkListOne"
                        TypeName="RmsOA.BFL.GK_OAPersonWorkBFL" UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
