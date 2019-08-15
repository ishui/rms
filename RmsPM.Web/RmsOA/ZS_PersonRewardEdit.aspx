<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZS_PersonRewardEdit.aspx.cs" Inherits="PersonalManage_ZS_PersonRewardEdit" %>

<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>奖惩情况</title>
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
                                奖惩情况</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%"
                        OnItemInserted="FormView1_ItemInserted" OnItemDeleted="FormView1_ItemDeleted"
                        OnItemUpdated="FormView1_ItemUpdated" OnItemInserting="FormView1_ItemInserting" DataKeyNames="Code">
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
                                    <td class="form-item"  style="width:20%">
                                        日期：</td>
                                    <td>
                                        <cc3:Calendar ID="Calendar6" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("dj_date") %>'>
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width:20%">
                                        奖惩内容：</td>
                                    <td>
                                        <asp:TextBox ID="SCHOOL_NAMETextBox" runat="server" Text='<%# Bind("content") %>'
                                            TextMode="MultiLine" Width="100%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width:20%">
                                        原因：</td>
                                    <td>
                                        <asp:TextBox ID="DEGREETextBox" runat="server" Text='<%# Bind("cause") %>' TextMode="MultiLine"
                                            Width="100%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width:20%">
                                        备注：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("remark") %>' TextMode="MultiLine"
                                            Width="100%"></asp:TextBox></td>
                                </tr>
                            </table>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <table id="Table1" class="table" width="100%" >
                                <tr>
                                    <td class="tools-area" width="16">
                                        <img align="absMiddle" src="../images/btn_li.gif" /></td>
                                    <td class="tools-area">
                                        <asp:Button ID="btnSave" runat="server" CommandName="Insert" CssClass="button" Text=" 保存 " />
                                        <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                            type="button" value=" 关闭 " />
                                    </td>
                                </tr>
                            </table><table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                                <tr>
                                    <td class="form-item" style="width:20%">
                                        日期：</td>
                                    <td>
                                        <cc3:Calendar ID="Calendar6" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("dj_date") %>'>
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width:20%">
                                        奖惩内容：</td>
                                    <td>
                                        <asp:TextBox ID="SCHOOL_NAMETextBox" runat="server" Text='<%# Bind("content") %>'
                                            TextMode="MultiLine" Width="100%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width:20%">
                                        原因：</td>
                                    <td>
                                        <asp:TextBox ID="DEGREETextBox" runat="server" Text='<%# Bind("cause") %>' TextMode="MultiLine"
                                            Width="100%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width:20%">
                                        备注：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("remark") %>' TextMode="MultiLine"
                                            Width="100%"></asp:TextBox></td>
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
                                        <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                            type="button" value=" 关闭 " />
                                    </td>
                                </tr>
                            </table>
                            <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                                <tr>
                                    <td class="form-item" style="width:20%">
                                        日期：</td>
                                    <td>
                                        <asp:Label ID="BEGIN_DATELabel" runat="server" Text='<%# Bind("dj_date") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width:20%">
                                        奖惩内容：</td>
                                    <td>
                                        <asp:Label ID="SCHOOL_NAMELabel" runat="server" Text='<%# Bind("content") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width:20%">
                                        原因：</td>
                                    <td>
                                        <asp:Label ID="DEGREELabel" runat="server" Text='<%# Bind("cause") %>'></asp:Label></td>
                                </tr>
                                 <tr>
                                    <td class="form-item"style="width:20%">
                                        备注：</td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("remark") %>'></asp:Label></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsOA.MODEL.OAPersonRewardModel"
                        DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetOAPersonRewardListOne"
                        TypeName="RmsOA.BFL.OAPersonRewardBFL" UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted">
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

