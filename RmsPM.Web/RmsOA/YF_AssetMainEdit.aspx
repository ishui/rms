<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YF_AssetMainEdit.aspx.cs" Inherits="RmsOA_YF_AssetMainEdit" %>

<%@ Register Src="../UserControls/inputunit.ascx" TagName="inputunit" TagPrefix="uc1" %>
<%@ Register Assembly="AspWebControl" Namespace="AspWebControl" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../Images/index.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../Rms.js" type="text/javascript"></script>

    <title>设备维修申请</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td background="../images/topic_bg.gif" class="topic" style="height: 25px; text-align: center;">
                        固定资产/设备维修</td>
                </tr>
            </table>
        </div>
    </div>
        <asp:FormView ID="FormView1" DataKeyNames="Code" runat="server" DataSourceID="ObjectDataSource1" Width="100%" OnItemDeleted="FormView1_ItemDeleted" OnItemInserting="FormView1_ItemInserting" OnItemUpdated="FormView1_ItemUpdated" OnDataBound="FormView1_DataBound">
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
                            公司部门
                        </td>
                        <td>
                            <uc1:inputunit ID="DeptInputunit" runat="server" Value='<%# Bind("Dept") %>' />
                        </td>
                        <td class="form-item" style="width: 100px;">
                            申请人
                        </td>
                        <td>
                            <asp:Label ID="UserCodeLabel" runat="server" Text='<%# Bind("UserCode") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            设备编号
                        </td>
                        <td>
                            <asp:Label ID="CodeNOLabel" runat="server"></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            报修日期
                        </td>
                        <td>
                            <cc1:Calendar ID="ApplyDate" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                Value='<%# Bind("ApplyDate") %>'>
                            </cc1:Calendar>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            申请事宜
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="ReasonTextBox" runat="server" CssClass="input" Text='<%# Bind("Reason") %>'
                                Width="80%">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            故障描述
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="RemarkTextBox" Width="90%" runat="server" Height="60px" Text='<%# Bind("Remark") %>'
                                TextMode="multiLine">
                            </asp:TextBox>
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
                            公司部门
                        </td>
                        <td>
                            <uc1:inputunit ID="DeptInputunit" runat="server" Value='<%# Bind("Dept") %>' />
                            </td>
                        <td class="form-item" style="width: 100px;">
                            申请人
                        </td>
                        <td>
                            <asp:Label ID="UserCodeLabel" runat="server" Text='<%# Bind("UserCode") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            设备编号
                        </td>
                        <td>
                        <asp:Label runat="server" ID="CodeNOLabel"></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            报修日期
                        </td>
                        <td>
                            <cc1:Calendar ID="ApplyDate" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                Value='<%# Bind("ApplyDate") %>'>
                            </cc1:Calendar>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            申请事宜
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="ReasonTextBox" CssClass="input" Width="80%" runat="server" Text='<%# Bind("Reason") %>'>
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            故障描述
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="RemarkTextBox" TextMode="multiLine" Height="60px" runat="server" Width="90%" Text='<%# Bind("Remark") %>'>
                            </asp:TextBox>
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
                            <asp:Button ID="EditButton" runat="server" CssClass="button" CommandName="Edit" CommandArgument="Code"
                                Text=" 修改 " />
                            <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" CssClass="button"
                                Text=" 删除 " />
                            <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                type="button" value=" 关闭 " />
                        </td>
                    </tr>
                </table>
            <table class="form" width="100%" cellpadding="0" cellspacing="0" border="0">
            <tr>
             <td class="form-item" style="width:100px;">
             公司部门
             </td><td>
             
                 <%# RmsPM.BLL.SystemRule.GetUnitName((string)Eval("Dept"))%>
                 </td>
                <td class="form-item" style="width: 100px;">
                申请人
                </td>
                <td>
                    <asp:Label ID="UserCodeLabel" runat="server" Text='<%# Bind("UserCode") %>'></asp:Label>
                </td>
            </tr>
                <tr>
                    <td class="form-item" style="width: 100px;">
                    设备编号
                    </td>
                    <td>
                    <asp:Label ID="CodeNOLabel" runat="server"></asp:Label>
                    </td>
                    <td class="form-item" style="width: 100px;">
                    报修日期
                    </td>
                    <td>
                        <asp:Label ID="ApplyDateLabel" runat="server" Text='<%# Bind("ApplyDate") %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="form-item" style="width: 100px;">
                    申请事宜
                    </td>
                    <td colspan="3">
                        <asp:Label ID="ReasonLabel" runat="server" Text='<%# Bind("Reason") %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="form-item" style="width: 100px;"> 故障描述
                    </td>
                    <td colspan="3">
                        <asp:Label ID="RemarkLabel" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                    </td>
                </tr>
            </table>
            </ItemTemplate>
        </asp:FormView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="RmsOA.BFL.YF_AssetMainApplyBFL" OldValuesParameterFormatString="original_{0}" SelectMethod="GetYF_AssetMainApplyListOne" DataObjectTypeName="RmsOA.MODEL.YF_AssetMainApplyModel" DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted">
            <SelectParameters>
                <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        
        
    </form>
</body>
</html>
