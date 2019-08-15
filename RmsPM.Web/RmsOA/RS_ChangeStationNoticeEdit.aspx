﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RS_ChangeStationNoticeEdit.aspx.cs"
    Inherits="RmsOA_RS_ChangeStationNoticeEdit" %>

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
    <title>员工转岗通知</title>
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
		    OpenFullWindow('<%= RmsPM.BLL.WorkFlowRule.GetProcedureURLByName("员工转岗通知审批")%>?ChangeStationNoticeCode=<%= FormView1.DataKey.Value %>&ProjectCode=<%= Request["ProjectCode"] + ""%>','员工转岗通知审批');
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
                                员工转岗通知</td>
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
                                        姓名：</td>
                                    <td>
                                        <asp:TextBox ID="PersonNameTextBox" runat="server" CssClass="input" Text='<%# Bind("PersonName") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        性别：</td>
                                    <td>
                                        <asp:TextBox ID="SexTextBox" runat="server" CssClass="input" Text='<%# Bind("Sex") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        入职时间：</td>
                                    <td>
                                        <cc3:Calendar ID="Calendar1" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("InComDate") %>'>
                                        </cc3:Calendar>
                                    <td class="form-item" style="width: 20%">
                                        日期：</td>
                                    <td>
                                        <cc3:Calendar ID="Calendar6" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("ChangeStationDate") %>'>
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        原因：</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="ReasonTextBox" runat="server" Text='<%# Bind("Reason") %>' TextMode="MultiLine"
                                            Width="100%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 20%">
                                    </td>
                                    <td>
                                        由</td>
                                    <td style="width: 20%">
                                        去</td>
                                    <td>
                                        备注</td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        部门：</td>
                                    <td>
                                        <asp:TextBox ID="OldUnitCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("OldUnitCode") %>'></asp:TextBox></td>
                                    <td  style="width: 20%">
                                        <asp:TextBox ID="NewUnitCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("NewUnitCode") %>'></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="Remark1TextBox" runat="server" Text='<%# Bind("Remark1") %>' TextMode="MultiLine"  Width ="100%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        岗位：</td>
                                    <td>
                                        <asp:TextBox ID="OldStationTextBox" runat="server" CssClass="input" Text='<%# Bind("OldStation") %>'></asp:TextBox></td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="NewStationTextBox" runat="server" CssClass="input" Text='<%# Bind("NewStation") %>'></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="Remark2TextBox" runat="server" Text='<%# Bind("Remark2") %>'  TextMode="MultiLine" Width ="100%" ></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        岗位级别：</td>
                                    <td>
                                        <asp:TextBox ID="OldStationLeavelTextBox" runat="server" CssClass="input" Text='<%# Bind("OldStationLeavel") %>'></asp:TextBox></td>
                                    <td  style="width: 20%">
                                        <asp:TextBox ID="NewStationLeavelTextBox" runat="server" CssClass="input" Text='<%# Bind("NewStationLeavel") %>'></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="Remark3TextBox" runat="server" Text='<%# Bind("Remark3") %>'  TextMode="MultiLine" Width ="100%"></asp:TextBox></td>
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
                                        姓名：</td>
                                    <td>
                                        <asp:TextBox ID="PersonNameTextBox" runat="server" CssClass="input" Text='<%# Bind("PersonName") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        性别：</td>
                                    <td>
                                        <asp:TextBox ID="SexTextBox" runat="server" CssClass="input" Text='<%# Bind("Sex") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        入职时间：</td>
                                    <td>
                                        <cc3:Calendar ID="Calendar2" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("InComDate") %>'>
                                        </cc3:Calendar>
                                    <td class="form-item" style="width: 20%">
                                        日期：</td>
                                    <td>
                                        <cc3:Calendar ID="Calendar6" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("ChangeStationDate") %>'>
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        原因：</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="ReasonTextBox" runat="server" Text='<%# Bind("Reason") %>' TextMode="MultiLine"
                                            Width="100%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 20%">
                                    </td>
                                    <td>&nbsp;
                                        由</td>
                                    <td style="width: 20%">&nbsp;
                                        去</td>
                                    <td>&nbsp;
                                        备注</td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        部门：</td>
                                    <td>
                                        <asp:TextBox ID="OldUnitCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("OldUnitCode") %>'></asp:TextBox></td>
                                    <td >
                                        <asp:TextBox ID="NewUnitCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("NewUnitCode") %>'></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="Remark1TextBox" runat="server" Text='<%# Bind("Remark1") %>' TextMode="MultiLine" Width ="100%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        岗位：</td>
                                    <td>
                                        <asp:TextBox ID="OldStationTextBox" runat="server" CssClass="input" Text='<%# Bind("OldStation") %>'></asp:TextBox></td>
                                    <td >
                                        <asp:TextBox ID="NewStationTextBox" runat="server" CssClass="input" Text='<%# Bind("NewStation") %>'></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="Remark2TextBox" runat="server" Text='<%# Bind("Remark2") %>' TextMode="MultiLine" Width ="100%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        岗位级别：</td>
                                    <td>
                                        <asp:TextBox ID="OldStationLeavelTextBox" runat="server" CssClass="input" Text='<%# Bind("OldStationLeavel") %>'></asp:TextBox></td>
                                    <td >
                                        <asp:TextBox ID="NewStationLeavelTextBox" runat="server" CssClass="input" Text='<%# Bind("NewStationLeavel") %>'></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="Remark3TextBox" runat="server" Text='<%# Bind("Remark3") %>' TextMode="MultiLine"  Width ="100%"></asp:TextBox></td>
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
                                        <asp:Button ID="btnBankOut" runat="server" CssClass="button" Text=" 作废 " OnClick="btnBankOut_Click" />
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
                                        姓名：</td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("PersonName") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        性别：</td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Sex") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        入职时间：</td>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("InComDate") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        日期：</td>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("ChangeStationDate") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        原因：</td>
                                    <td colspan="3">
                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("Reason") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 20%">
                                    </td>
                                    <td>
                                        由</td>
                                    <td style="width: 20%">
                                        去</td>
                                    <td>
                                        备注</td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        部门：</td>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("OldUnitCode") %>'></asp:Label></td>
                                    <td >
                                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("NewUnitCode") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("Remark1") %>' Width ="100%"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        岗位：</td>
                                    <td>
                                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("OldStation") %>'></asp:Label></td>
                                    <td >
                                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("NewStation") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("Remark2") %>'   Width ="100%"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        岗位级别：</td>
                                    <td>
                                        <asp:Label ID="Label14" runat="server" Text='<%# Bind("OldStationLeavel") %>'></asp:Label></td>
                                    <td >
                                        <asp:Label ID="Label15" runat="server" Text='<%# Bind("NewStationLeavel") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="Label16" runat="server" Text='<%# Bind("Remark3") %>'  Width ="100%"></asp:Label></td>
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
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsOA.MODEL.GK_OA_ChangeStationNoticeModel"
                        DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetGK_OA_ChangeStationNoticeListOne"
                        TypeName="RmsOA.BFL.GK_OA_ChangeStationNoticeBFL" UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted">
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
