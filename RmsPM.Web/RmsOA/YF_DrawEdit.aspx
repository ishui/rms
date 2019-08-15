<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YF_DrawEdit.aspx.cs" Inherits="RmsOA_YF_DrawEdit" %>

<%@ Register Src="../UserControls/inputunit.ascx" TagName="inputunit" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/inputuser.ascx" TagName="inputuser" TagPrefix="uc2" %>
<%@ Register Assembly="AspWebControl" Namespace="AspWebControl" TagPrefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../Images/index.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../Rms.js" type="text/javascript"></script>

    <title>资产领用申请</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td background="../images/topic_bg.gif" class="topic" style="height: 25px; text-align: center;">
                            固定资产/领用申请</td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:FormView Width="100%" DataKeyNames="Code" ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" OnDataBound="FormView1_DataBound" OnItemDeleted="FormView1_ItemDeleted" OnItemInserted="FormView1_ItemInserted" OnItemUpdated="FormView1_ItemUpdated" OnItemInserting="FormView1_ItemInserting">
            <EditItemTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="tools-area" style="width: 100%; height: 25px;" valign="top">
                            <img align="absMiddle" alt="" src="../images/btn_li.gif">
                            <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="UpDate"
                                CssClass="button" Text="更新" />
                            <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                CssClass="button" Text="取消" />
                            <input class="button" onclick="self.close()" type="button" value="关闭" />
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            设备名称</td>
                        <td>
                            <asp:Label ID="NameLabel" runat="server"></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            申请部门
                        </td>
                        <td>
                            <uc1:inputunit ID="DrawUnitInputunit" runat="server" Value='<%# Bind("DrawUnit") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            设备编号
                        </td>
                        <td>
                            <asp:Label ID="SortNOLabel" runat="server"></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            领用日期
                        </td>
                        <td>
                            <cc1:Calendar ID="DrawDate" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                Value='<%# Bind("DrawDate") %>'>
                            </cc1:Calendar>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            归还日期
                        </td>
                        <td>
                            <cc1:Calendar ID="BackTime" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                Value='<%# Bind("BackTime") %>'>
                            </cc1:Calendar>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            领用人
                        </td>
                        <td>
                            <uc2:inputuser ID="DrawPersonInputuser" runat="server" Value='<%# Bind("DrawPerson") %>' />
                        </td>
                        <td class="form-item" style="width: 100px;">
                            使用人
                        </td>
                        <td>
                            <uc2:inputuser ID="UserCodeInputuser" runat="server" Value='<%# Bind("UserCode") %>' />
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
                        <td class="form-item" style="width: 100px;">
                            设备名称</td>
                        <td>
                            <asp:Label ID="NameLabel" runat="server"></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            申请部门
                        </td>
                        <td>
                            <uc1:inputunit ID="DrawUnitInputunit" runat="server" Value='<%# Bind("DrawUnit") %>' />
                            <span style="color:Red;">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            设备编号
                        </td>
                        <td>
                            <asp:Label ID="SortNOLabel" runat="server"></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            领用日期
                        </td>
                        <td>
                            <cc1:Calendar ID="DrawDate" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                Value='<%# Bind("DrawDate") %>'>
                            </cc1:Calendar>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            归还日期
                        </td>
                        <td>
                            <cc1:Calendar ID="BackTime" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                Value='<%# Bind("BackTime") %>'>
                            </cc1:Calendar>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            领用人
                        </td>
                        <td>
                            <uc2:inputuser ID="DrawPersonInputuser" runat="server" Value='<%# Bind("DrawPerson") %>' />
                        </td>
                        <td class="form-item" style="width: 100px;">
                            使用人
                        </td>
                        <td>
                            <uc2:inputuser ID="UserCodeInputuser" runat="server" Value='<%# Bind("UserCode") %>' />
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
            <table width="100%" class="form" cellpadding="0" cellspacing="0" border="0">
            <tr>
            <td style="width:100px;" class="form-item">
                设备名称</td>
            <td>
                <asp:Label runat="server" ID="NameLabel"></asp:Label>
                </td>
                <td class="form-item" style="width: 100px;">
                申请部门
                </td>
                <td>
                    <%# RmsPM.BLL.SystemRule.GetUnitName((string)Eval("DrawUnit"))%>
                </td>
              
            </tr>
                <tr>
                    <td class="form-item" style="width: 100px;">
                    设备编号
                    </td>
                    <td>
                    <asp:Label runat="server" ID="SortNOLabel"></asp:Label>
                    </td>
                    <td class="form-item" style="width: 100px;">
                    </td>
                    <td>
                    </td>
                </tr>
               
                <tr>
                    <td class="form-item" style="width: 100px;">
                    领用日期
                    </td>
                    <td>
                        <asp:Label ID="DrawDateLabel" runat="server" Text='<%# Bind("DrawDate") %>'></asp:Label>
                    </td>
                    <td class="form-item" style="width: 100px;">
                    归还日期
                    </td>
                    <td>
                        <asp:Label ID="BackTimeLabel" runat="server" Text='<%# Bind("BackTime") %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="form-item" style="width: 100px;">
                    领用人
                    </td>
                    <td>
                    <%# RmsPM.BLL.SystemRule.GetUserName((string)Eval("DrawPerson")) %>
                    </td>
                    <td class="form-item" style="width: 100px;">
                    使用人
                    </td>
                    <td>
                        <%# RmsPM.BLL.SystemRule.GetUserName((string)Eval("UserCode"))%>
                    </td>
                </tr>
                </table>
            </ItemTemplate>
        </asp:FormView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="RmsOA.BFL.YF_AssetDrawBFL" OldValuesParameterFormatString="original_{0}" SelectMethod="GetYF_AssetDrawListOne" DataObjectTypeName="RmsOA.MODEL.YF_AssetDrawModel" DeleteMethod="Delete" InsertMethod="Insert" OnInserted="ObjectDataSource1_Inserted" UpdateMethod="Update">
            <SelectParameters>
                <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
