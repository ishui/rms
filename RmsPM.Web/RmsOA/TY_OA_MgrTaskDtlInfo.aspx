<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TY_OA_MgrTaskDtlInfo.aspx.cs"
    Inherits="RmsOA_TY_OA_MgrTaskDtlInfo" %>

<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputUsers" Src="../UserControls/InputUsers.ascx" %>
<%@ Register Src="../WorkFlowControl/WorkFlowList.ascx" TagName="WorkFlowList" TagPrefix="uc4" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>总经理交办事宜</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet">
    <link href="../Images/infra.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>

    <script language="javascript">
function OpenRequisition()
{
   OpenFullWindow('<%=ViewState["_AuditingURL"]%>?MgrDtlCode=<%= Request["MgrDtlCode"] %>','交办事宜审核');
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
                                交办具体事宜</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%"
                        OnItemDeleted="FormView1_ItemDeleted" OnItemUpdated="FormView1_ItemUpdated" DataKeyNames="MgrDtlCode"
                        OnDataBound="FormView1_DataBound">
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
                                    <td class="form-item" style="width: 15%">
                                        责任人：</td>
                                    <td>
                                        <uc1:InputUser ID="ResponsePerson" runat="server" Value='<%# Bind("ResponsePerson") %>'>
                                        </uc1:InputUser>
                                    </td>
                                    <td class="form-item">
                                        要求完成日期：</td>
                                    <td colspan="3">
                                        <cc3:Calendar ID="Calendar6" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("DeadLine") %>'>
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        协办人：</td>
                                    <td>
                                        <uc1:InputUsers ID="TxtTaskName" runat="server" Value='<%# Bind("Assistpersons") %>'
                                            MustInput="false"  ButtonName="选择协办人"></uc1:InputUsers>
                                        <td class="form-item">
                                            审核状态：</td>
                                    <td>
                                        <%#RmsOA.BLL.TY_OA_MgrTaskBLL.GetStatusName(Eval("State").ToString())%>
                                    </td>
                                    <td class="form-item">
                                        完成状态：</td>
                                    <td>
                                        <%# RmsOA.BLL.TY_OA_MgrTaskDtlBLL.GetIsfinishName(Eval("Isfinish").ToString())%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap>
                                        交办具体工作事项：</td>
                                    <td colspan="5">
                                        <textarea id="TxtTaskTail" style="width: 100%" name="TxtTaskTail" rows="7" value='<%# Bind("MgrDtlInfo") %>'
                                            runat="server"></textarea>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        办理回复：</td>
                                    <td colspan="5">
                                        <textarea id="TrancRevertID" style="width: 100%" name="TxtTaskTail" rows="7" readonly="readonly"
                                            value='<%# Bind("TrancRevert") %>' runat="server"></textarea>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        总经理回复：</td>
                                    <td colspan="5">
                                        <textarea id="ManagerRevertID" style="width: 100%" name="TxtTaskTail" rows="7" readonly="readonly"
                                            value='<%# Bind("ManagerRevert") %>' runat="server"></textarea>
                                    </td>
                                </tr>
                            </table>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <table id="Table3" class="table" width="100%">
                                <tr>
                                    <td class="tools-area" width="16">
                                        <img align="absMiddle" src="../images/btn_li.gif" /></td>
                                    <td class="tools-area">
                                        <asp:Button ID="btnEdit" runat="server" CommandName="Edit" CssClass="button" Text=" 修改 " />
                                        <asp:Button ID="btnDelete" runat="server" CommandName="Delete" CssClass="button"
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
                                    <td class="form-item" style="width: 15%">
                                        责任人：</td>
                                    <td width="30%">
                                        <%# RmsPM.BLL.SystemRule.GetUserName(RmsPM.BLL.ConvertRule.ToString(Eval("ResponsePerson")))%>
                                    </td>
                                    <td class="form-item" style="width: 15%">
                                        要求完成日期：</td>
                                    <td colspan="3">
                                        <%#RmsPM.BLL.StringRule.ShowDate(DataBinder.Eval(Container.DataItem, "DeadLine"))%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 15%">
                                        协办人：</td>
                                    <td>
                                        <asp:Label ID="Eyeteamer" Width="100%" runat="server" Text=' <%# Eval("Assistpersons")%>'></asp:Label>
                                    </td>
                                    <td class="form-item" style="width: 15%">
                                        审核状态：</td>
                                    <td>
                                        <%#RmsOA.BLL.TY_OA_MgrTaskBLL.GetStatusName(Eval("State").ToString())%>
                                    </td>
                                    <td class="form-item" style="width: 15%">
                                        完成状态：</td>
                                    <td>
                                        <%# RmsOA.BLL.TY_OA_MgrTaskDtlBLL.GetIsfinishName(Eval("Isfinish").ToString())%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 15%" nowrap>
                                        交办具体工作事项：</td>
                                    <td colspan="5">
                                        <%#(DataBinder.Eval(Container.DataItem, "MgrDtlInfo")).ToString().Replace("\n","<br>")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 15%">
                                        办理回复：</td>
                                    <td colspan="5">
                                        <%# Eval("TrancRevert").ToString().Replace("\n", "<br>")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 15%">
                                        总经理回复：</td>
                                    <td colspan="5">
                                        <%# Eval("ManagerRevert").ToString().Replace("\n", "<br>")%>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" TypeName="RmsOA.BFL.TY_OA_MgrTaskDtlBFL"
                        runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetTY_OA_MgrTaskDtl"
                        DataObjectTypeName="RmsOA.MODEL.TY_OA_MgrTaskDtlModel" DeleteMethod="Delete"
                        InsertMethod="Insert" UpdateMethod="Update">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="Code" QueryStringField="MgrDtlCode" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="intopic" width="200">
                                相关流程</td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <uc4:WorkFlowList ID="WorkFlowList1" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
