<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GK_OA_InFileRegisterEdit.aspx.cs" Inherits="RmsOA_GK_OA_InFileRegisterEdit" %>

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
		
		function OpenRequisition()
        {
		    OpenFullWindow('<%= RmsPM.BLL.WorkFlowRule.GetProcedureURLByName("文件承办单审批")%>?InFileRegisterAuditingCode=New&InFileRegisterCode=<%= FormView1.DataKey.Value %>&ProjectCode=<%= Request["ProjectCode"] + ""%>','文件承办单审批');
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
                                        收文编号：</td>
                                    <td>
                                        <asp:TextBox ID="FileCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("InFileCode") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        原文字号：</td>
                                    <td>
                                        <asp:TextBox ID="OriginalFileCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("OriginalFileCode") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        文件类别：</td>
                                    <td>
                                        <asp:TextBox ID="FileTypeTextBox" runat="server" CssClass="input" Text='<%# Bind("FileType") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        登记时间：</td>
                                    <td>
                                        <cc3:Calendar ID="Calendar6" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("InFileDate") %>'>
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        份数：</td>
                                    <td>
                                        <asp:TextBox ID="FileNumberTextBox" runat="server" CssClass="input" Text='<%# Bind("FileNumber") %>'></asp:TextBox></td>
                                    <td  style="width: 20%">
                                        </td>
                                    <td>
                                        </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        备注：</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="RemarkTextBox" runat="server" Text='<%# Bind("Remark") %>'
                                            TextMode="MultiLine" Width="100%"></asp:TextBox></td>
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
                                        收文编号：</td>
                                    <td>
                                        <asp:TextBox ID="FileCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("InFileCode") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        原文字号：</td>
                                    <td>
                                        <asp:TextBox ID="OriginalFileCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("OriginalFileCode") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        文件类别：</td>
                                    <td>
                                        <asp:TextBox ID="FileTypeTextBox" runat="server" CssClass="input" Text='<%# Bind("FileType") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        登记时间：</td>
                                    <td>
                                        <cc3:Calendar ID="Calendar6" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("InFileDate") %>'>
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        份数：</td>
                                    <td>
                                        <asp:TextBox ID="FileNumberTextBox" runat="server" CssClass="input" Text='<%# Bind("FileNumber") %>'></asp:TextBox></td>
                                    <td  style="width: 20%">
                                        </td>
                                    <td>
                                        </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        备注：</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="RemarkTextBox" runat="server" Text='<%# Bind("Remark") %>'
                                            TextMode="MultiLine" Width="100%"></asp:TextBox></td>
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
                                        <input name="btnRequisition" id="btnRequisition" type="button" value=" 提交 " class="button"
                                            runat="server" onclick="javascript:OpenRequisition();return false;">
                                        
                                        <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                            type="button" value=" 关闭 " />
                                    </td>
                                </tr>
                            </table>
                            <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        收文编号：</td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("InFileCode") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        原文字号：</td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("OriginalFileCode") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        文件类别：</td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("FileType") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        登记时间：</td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("InFileDate") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        份数：</td>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("FileNumber") %>'></asp:Label></td>
                                    <td  style="width: 20%">
                                        </td>
                                    <td>
                                        
                                     </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        备注：</td>
                                    <td colspan="3">
                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("Remark") %>'></asp:Label></td>
                                </tr>
                            </table>
                            <table cellspacing="0" cellpadding="0" width="470" border="0">
                                <tr id="webtabs">
                                    <td width="20">
                                    </td>
                                    <td class="TabDisplay" id="workflowmsg" runat="server" width="185">
                                        相关流程</td>
                                </tr>
                            </table>
                            <table cellspacing="0" cellpadding="0" width="100%" border="0" id="tabdiv">
                                <tr>
                                    <td>
                                        <uc4:WorkFlowList ID="WorkFlowList1" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsOA.MODEL.GK_OA_InFileRegisterModel"
                        DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetGK_OA_InFileRegisterListOne"
                        TypeName="RmsOA.BFL.GK_OA_InFileRegisterBFL" UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted">
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