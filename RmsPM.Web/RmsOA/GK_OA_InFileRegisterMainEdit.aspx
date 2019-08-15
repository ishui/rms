﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GK_OA_InFileRegisterMainEdit.aspx.cs"
    Inherits="RmsOA_GK_OA_InFileRegisterMainEdit" %>

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
    <title>收文登记</title>
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
		function OpenInFileRegister()
		{
			OpenSmallWindow("./GK_OA_InFileRegisterEdit.aspx?&InFileRegisterMainCode=<%= FormView1.DataKey.Value %>");
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
                                收文登记</td>
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
                                    <td class="form-item" style="width: 20%">
                                        文件编号：</td>
                                    <td>
                                        <asp:TextBox ID="SystemnCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("SystemCode") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        标识序号：</td>
                                    <td>
                                        <asp:TextBox ID="FileCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("FileCode") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        登记时间：</td>
                                    <td>
                                        <cc3:Calendar ID="Calendar6" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("InFileDate") %>'>
                                        </cc3:Calendar>
                                    </td>
                                    <td style="width: 20%">
                                    </td>
                                    <td style="width: 20%">
                                    </td>
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
                                        <asp:TextBox ID="SystemnCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("SystemCode") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        标识序号：</td>
                                    <td>
                                        <asp:TextBox ID="FileCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("FileCode") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        登记时间：</td>
                                    <td>
                                        <cc3:Calendar ID="Calendar6" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("InFileDate") %>'>
                                        </cc3:Calendar>
                                    </td>
                                    <td style="width: 20%">
                                    </td>
                                    <td style="width: 20%">
                                    </td>
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
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("SystemCode") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        标识序号：</td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("FileCode") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        登记时间：</td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("InFileDate") %>'></asp:Label></td>
                                </tr>
                                <table cellspacing="0" cellpadding="0" width="470" border="0">
                                    <tr id="webtabs">
                                        <td width="20">
                                        </td>
                                        <td class="TabDisplay" id="workflowmsg" runat="server" width="185">
                                            相关明细</td>
                                         <td>
                                            <input name="btnInFileRegister" id="btnInFileRegister" type="button" value="新增" class="button"
                                            runat="server" onclick="javascript:OpenInFileRegister();return false;">
                                         </td>
                                    </tr>
                                </table>
                                <table cellspacing="0" cellpadding="0" width="100%" border="0" id="tabdiv">
                                    <tr height="100%">
                                        <td class="table" valign="top">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                                AllowSorting="True" CssClass="list" Width="100%" DataSourceID="ObjectDataSource2">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="收文编号">
                                                        <ItemTemplate>
                                                            <a href="#" onclick="javascript:OpenMiddleWindow('GK_OA_InFileRegisterEdit.aspx?Code=<%# Eval("Code")%>','RegisterDetail');return false;">
                                                                <%# Eval("InFileCode")%>
                                                            </a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="OriginalFileCode" HeaderText="原文字号" SortExpression="OriginalFileCode" />
                                                    <asp:BoundField DataField="FileType" HeaderText="文件类别" SortExpression="FileType" />
                                                    <asp:BoundField DataField="FileNumber" HeaderText="份数" SortExpression="FileNumber" />
                                                </Columns>
                                                <PagerStyle CssClass="list-title" />
                                                <HeaderStyle CssClass="list-title" />
                                                <EmptyDataTemplate>
                                                    无匹配数据

                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                        </ItemTemplate>
                    </asp:FormView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsOA.MODEL.GK_OA_InFileRegisterMainModel"
                        DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetGK_OA_InFileRegisterMainListOne"
                        TypeName="RmsOA.BFL.GK_OA_InFileRegisterMainBFL" UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    
                    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" EnablePaging="True" MaximumRowsParameterName="MaxRecords"
                        SelectMethod="GetGK_OA_InFileRegisterList" SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord"
                        TypeName="RmsOA.BFL.GK_OA_InFileRegisterBFL">
                        <SelectParameters>
                            <asp:Parameter Name="sortColumns" Type="String" />
                            <asp:Parameter Name="startRecord" Type="Int32" />
                            <asp:Parameter Name="maxRecords" Type="Int32" />
                            <asp:Parameter Name="RegisterMainCodeEqual" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
