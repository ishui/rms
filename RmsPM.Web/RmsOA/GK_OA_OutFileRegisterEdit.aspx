<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GK_OA_OutFileRegisterEdit.aspx.cs"
    Inherits="RmsOA_GK_OA_OutFileRegisterEdit" %>

<%@ Register Src="../WorkFlowControl/workflowtoolbar.ascx" TagName="workflowtoolbar"
    TagPrefix="uc5" %>
<%@ Register Src="../WorkFlowControl/WorkFlowList.ascx" TagName="WorkFlowList" TagPrefix="uc4" %>
<%@ Register Src="../UserControls/inputcontract.ascx" TagName="inputcontract" TagPrefix="uc3" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc2" TagName="InputUnit" Src="../UserControls/InputUnit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputSupplier" Src="../UserControls/InputSupplier.ascx" %>
<%@ Register TagPrefix="cc4" Namespace="Rms.ControlLb" Assembly="RmsPM.BLL" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>发文登记</title>
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
                                发文登记</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%"
                        OnItemInserted="FormView1_ItemInserted" OnItemDeleted="FormView1_ItemDeleted"
                        OnItemUpdated="FormView1_ItemUpdated" DataKeyNames="Code" OnDataBound="FormView1_DataBound">
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
                                    <td class="form-item" style="width: 20%">
                                        文件编号：</td>
                                    <td>
                                        <asp:TextBox ID="FileCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("FileCode") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        日期：</td>
                                    <td>
                                        <cc3:Calendar ID="SubmitDateCalendar" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("RegiesterDate") %>'>
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        部门：</td>
                                    <td>
                                        <input id="txtUnit" runat="server" class="input" name="txtUnit" size="8" style="width: 72px;
                                            height: 18px" type="hidden" value='<%# Bind("UnitCode") %>' /><input id="txtUnitName"
                                                runat="server" class="input" name="txtUnit" style="width: 121px; height: 18px"
                                                type="text" /><img onclick="SelectUnit();return false;" src="../images/ToolsItemSearch.gif"
                                                    style="cursor: hand" />
                                    </td>
                                    <td class="form-item" style="width: 20%">
                                        收文人：</td>
                                    <td>
                                        <asp:TextBox ID="UserCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("UserCode") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        份数：</td>
                                    <td>
                                        <asp:TextBox ID="FileNumberTextBox" runat="server" CssClass="input" Text='<%# Bind("FileNumeber") %>'></asp:TextBox></td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        摘要：</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="ReasionTextBox" runat="server" TextMode="MultiLine" Width="100%"
                                            Text='<%# Bind("Detail") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        备注：</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="OldContextTextBox" runat="server" TextMode="MultiLine" Width="100%"
                                            Text='<%# Bind("Remark") %>'></asp:TextBox></td>
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
                                    <td class="form-item" style="width: 20%">
                                        文件编号：</td>
                                    <td>
                                        <asp:TextBox ID="FileCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("FileCode") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        日期：</td>
                                    <td>
                                        <cc3:Calendar ID="SubmitDateCalendar" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("RegiesterDate") %>'>
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        部门：</td>
                                    <td>
                                        <input id="txtUnit" runat="server" class="input" name="txtUnit" size="8" style="width: 72px;
                                            height: 18px" type="hidden" value='<%# Bind("UnitCode") %>' /><input id="txtUnitName"
                                                runat="server" class="input" name="txtUnit" style="width: 121px; height: 18px"
                                                type="text" /><img onclick="SelectUnit();return false;" src="../images/ToolsItemSearch.gif"
                                                    style="cursor: hand" />
                                    </td>
                                    <td class="form-item" style="width: 20%">
                                        收文人：</td>
                                    <td>
                                        <asp:TextBox ID="UserCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("UserCode") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        份数：</td>
                                    <td>
                                        <asp:TextBox ID="FileNumberTextBox" runat="server" CssClass="input" Text='<%# Bind("FileNumeber") %>'></asp:TextBox></td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        摘要：</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="ReasionTextBox" runat="server" TextMode="MultiLine" Width="100%"
                                            Text='<%# Bind("Detail") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        备注：</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="OldContextTextBox" runat="server" TextMode="MultiLine" Width="100%"
                                            Text='<%# Bind("Remark") %>'></asp:TextBox></td>
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
                                    <td class="form-item" style="width: 20%">
                                        文件编号：</td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("FileCode") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        日期：</td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("RegiesterDate") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        部门：</td>
                                    <td>
                                        <asp:Label ID="UnitLabel" runat="server" Text='<%# Bind("UnitCode") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        收文人：</td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("UserCode") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        份数：</td>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("FileNumeber") %>'></asp:Label></td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        摘要：</td>
                                    <td colspan="3">
                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("Detail") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        备注：</td>
                                    <td colspan="3">
                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("Remark") %>'></asp:Label></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsOA.MODEL.GK_OA_OutFileRegiesterModel"
                        DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetGK_OA_OutFileRegiesterListOne"
                        TypeName="RmsOA.BFL.GK_OA_OutFileRegiesterBFL" UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted">
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
