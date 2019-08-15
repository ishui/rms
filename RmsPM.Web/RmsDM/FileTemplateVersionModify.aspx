<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileTemplateVersionModify.aspx.cs"
    Inherits="RmsDM_FileTemplateVersionModify" %>

<%@ Register Assembly="AspWebControl" Namespace="AspWebControl" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/attachmentlist.ascx" TagName="attachmentlist" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/attachmentadd.ascx" TagName="attachmentadd" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>文档模板更改</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="Form1" runat="Server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td background="../images/topic_bg.gif" class="topic" style="height: 25px; text-align: center;">
                        版本详细信息</td>
                </tr>
            </table>
        </div>
        <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%" OnItemInserting="FormView1_ItemInserting"
            OnItemUpdated="FormView1_ItemUpdated" OnItemUpdating="FormView1_ItemUpdating" OnDataBound="FormView1_DataBound">
            <EditItemTemplate>
                <table width="100%" class="form">
                    <tr>
                        <td class="form-item" width="120">
                            版本名称:
                        </td>
                        <td>
                            <asp:TextBox ID="VersionNumberTextBox" runat="server" Text='<%# Bind("VersionNumber") %>' CssClass="input">
                            </asp:TextBox><font color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="VersionNumberTextBox"
                                ErrorMessage="提醒：版本名称不能为空"></asp:RequiredFieldValidator>
                        </td>
                        <td class="form-item" width="120">
                            文档编号规则:
                        </td>
                        <td>
                            <asp:TextBox ID="MarkingSNRuleTextBox" runat="server" CssClass="input" Text='<%# Bind("MarkingSNRule") %>'>
                            </asp:TextBox><font color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="MarkingSNRuleTextBox"
                                ErrorMessage="提醒：文档编码规则不能为空"></asp:RequiredFieldValidator>
                           
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" width="120">
                          有效状态:  
                        </td>
                        <td>
                            <asp:DropDownList ID="IsAvailabilityDropDownList" runat="server" Font-Size="9pt">
                                <asp:ListItem>
                        请选择
                                </asp:ListItem>
                                <asp:ListItem Selected="true">有效</asp:ListItem>
                                <asp:ListItem>无效</asp:ListItem>
                            </asp:DropDownList>  
                        </td>
                        <td class="form-item" width="120">
                            是否归档:
                        </td>
                        <td>
                            <asp:DropDownList ID="IsPigeonholeDropDownList" runat="server" Font-Size="9pt" Width="60px">
                                <asp:ListItem Selected="true">是</asp:ListItem>
                                <asp:ListItem>否</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" width="120">
                            保存期限:
                        </td>
                        <td>
                            <asp:TextBox ID="SaveTermTextBox" runat="server" CssClass="input" Text='<%# Bind("SaveTerm") %>'>
                            </asp:TextBox>
                        </td>
                        <td class="form-item" width="120">
                            归档时间:
                        </td>
                        <td>
                            <cc1:Calendar ID="SaveDateTime" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                Value='<%# Bind("PigeonholeTime") %>'>
                            </cc1:Calendar>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" width="120">
                            记录性质:
                        </td>
                        <td>
                            <asp:TextBox ID="RecordKindTextBox" runat="server" CssClass="input" Text='<%# Bind("RecordKind") %>'>
                            </asp:TextBox>
                        </td>
                        <td class="form-item" width="120">
                            工作流程:
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="ObjectDataSource2"
                                DataTextField="ProcedureName" DataValueField="ProcedureName" Font-Size="9pt">
                                <asp:ListItem>请选择</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" width="120">
                            编辑附件:
                        </td>
                        <td colspan="3">
                            <uc1:attachmentadd ID="Attachmentadd1" runat="server" MasterCode='<%# Bind("Code") %>'
                                AttachMentType="FileTemplateVersion"></uc1:attachmentadd>
                        </td>
                    </tr>
                </table>
                <table width="100%" cellspacing="10">
                    <tr>
                        <td align="center">
                            <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                Text="更新" CssClass="button"></asp:Button>
                            <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text="取消" CssClass="button"></asp:Button></td>
                    </tr>
                </table>
            </EditItemTemplate>
            <InsertItemTemplate>
                <table width="100%" class="form">
                    <tr>
                        <td class="form-item" width="120">
                            版本名称:
                        </td>
                        <td>
                            <asp:TextBox ID="VersionNumberTextBox" runat="server" Text='<%# Bind("VersionNumber") %>' CssClass="input"></asp:TextBox><font color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="VersionNumberTextBox"
                                ErrorMessage="提醒：请输入版本号"></asp:RequiredFieldValidator>
                        </td>
                        <td class="form-item" width="120">
                             文档编号规则:
                        </td>
                        <td>
                            <asp:TextBox ID="MarkingSNRuleTextBox" runat="server" CssClass="input" Text='<%# Bind("MarkingSNRule") %>'>
                            </asp:TextBox><font color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="MarkingSNRuleTextBox"
                                ErrorMessage="提醒： 文档编号规则不能为空"></asp:RequiredFieldValidator>   
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" width="120">
                           有效状态: 
                        </td>
                        <td>
                            <asp:DropDownList ID="IsAvailabilityDropDownList" runat="server" Font-Size="9pt">
                                <asp:ListItem Selected="true">有效</asp:ListItem>
                                <asp:ListItem>无效</asp:ListItem>
                            </asp:DropDownList>
                            
                        </td>
                        <td class="form-item" width="120">
                            是否归档:
                        </td>
                        <td>
                            <asp:DropDownList ID="IsPigeonholeDropDownList" runat="server" Width="60px" Font-Size="9pt">
                                <asp:ListItem Selected="true"> 是</asp:ListItem>
                                <asp:ListItem>否</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" width="120">
                            保存期限:
                        </td>
                        <td>
                            <asp:TextBox ID="SaveTermTextBox" runat="server" Text='<%# Bind("SaveTerm") %>' CssClass="input"></asp:TextBox>
                        </td>
                        <td class="form-item" width="120">
                            归档时间:
                        </td>
                        <td> &nbsp;
                            <cc1:Calendar ID="SaveDateTime" runat="server" ReadOnly="false" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                Value='<%# Bind("PigeonholeTime") %>'>
                            </cc1:Calendar>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" width="120">
                            记录性质:
                        </td>
                        <td>
                            <asp:TextBox ID="RecordKindTextBox" runat="server" Text='<%# Bind("RecordKind") %>' CssClass="input"></asp:TextBox>
                        </td>
                        <td class="form-item" width="120">
                            工作流程:
                        </td>
                        <td style="width:160px;">
                            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="ObjectDataSource2"
                                DataTextField="ProcedureName" Font-Size="9pt" DataValueField="ProcedureName">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" width="120">
                            添加附件:
                        </td>
                        <td colspan="3">
                            <uc1:attachmentadd ID="Attachmentadd1" runat="server" MasterCode='<%# Bind("Code") %>'
                                AttachMentType="FileTemplateVersion"></uc1:attachmentadd>
                        </td>
                    </tr>
                </table>
                <table width="100%" cellspacing="10">
                    <tr>
                        <td align="center">
                            <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                Text="添加" CssClass="button"></asp:Button>
                                <input type="button" class="button" value="关闭" onclick="self.close()" />
                                </td>
                    </tr>
                </table>
            </InsertItemTemplate>
            <ItemTemplate>
                <table width="100%" class="form">
                    <tr>
                        <td class="form-item" width="120">
                            版本名称:
                        </td>
                        <td>
                            <asp:Label ID="VersionNumberTextBox" runat="server" Text='<%# Bind("VersionNumber") %>'>
                            </asp:Label>
                        </td>
                        <td class="form-item" width="120">
                            文档编号规则:
                        </td>
                        <td>
                            <asp:Label ID="MarkingSNRuleTextBox" runat="server" Text='<%# Bind("MarkingSNRule") %>'>
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" width="120">
                             有效状态:
                        </td>
                        <td>
                            <asp:Label ID="IsAvailabilityTextBox" runat="server" Text='<%# Bind("IsAvailability") %>'></asp:Label>
                        </td>
                        <td class="form-item" width="120">
                            是否归档:
                        </td>
                        <td>
                            <asp:Label ID="IsPigeonholeTextBox" runat="server" Text='<%# Bind("IsPigeonhole") %>'>
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" width="120">
                            保存期限:
                        </td>
                        <td>
                            <asp:Label ID="SaveTermTextBox" runat="server" Text='<%# Bind("SaveTerm") %>'>
                            </asp:Label>
                        </td>
                        <td class="form-item" width="120">
                            归档时间:
                        </td>
                        <td>
                            <asp:Label ID="PigeonholeTimeTextBox" runat="server" Text='<%# Bind("PigeonholeTime") %>'>
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" width="120">
                            记录性质:
                        </td>
                        <td>
                            <asp:Label ID="RecordKindTextBox" runat="server" Text='<%# Bind("RecordKind") %>'>
                            </asp:Label>
                        </td>
                        <td class="form-item" width="120">
                            工作流程:
                        </td>
                        <td>
                            <asp:Label ID="WorkFlowProcedureNameTextBox" runat="server" Text='<%# Bind("WorkFlowProcedureName") %>'>
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" width="120">
                            下载附件:
                        </td>
                        <td colspan="3">
                            <uc2:attachmentlist ID="Attachmentlist1" runat="server" MasterCode='<%# Bind("Code") %>'
                                AttachMentType="FileTemplateVersion" />
                        </td>
                    </tr>
                </table>
                <table width="100%" cellspacing="10">
                    <tr>
                        <td align="center">
                            <asp:Button ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                                Text="编辑" CssClass="button"></asp:Button>
                            <asp:Button ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="作废" CssClass="button" OnClick="DeleteButton_Click"></asp:Button>
                             <input type="button" class="button" value="关闭" onclick="self.close()"/>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:FormView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsDM.MODEL.FileTemplateVersionModel"
            DeleteMethod="Delete" InsertMethod="Insert" OnInserted="ObjectDataSource1_Inserted"
            SelectMethod="GetFileTemplateVersionListOne" TypeName="RmsDM.BFL.FileTemplateVersionBFL"
            UpdateMethod="Update">
            <SelectParameters>
                <asp:QueryStringParameter Name="Code" QueryStringField="code" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetWorkFlowProcedureList"
            TypeName="RmsDM.BFL.WorkFlowProcedureBFL" SortParameterName="SortColumns"
            StartRowIndexParameterName="StartRecord" MaximumRowsParameterName="MaxRecords">
            <SelectParameters>
                <asp:Parameter Name="SortColumns" Type="String" />
                <asp:Parameter Name="StartRecord" Type="Int32" DefaultValue="0" />
                <asp:Parameter Name="MaxRecords" Type="Int32" DefaultValue="-1" />
                <asp:Parameter Name="ProcedureCodeEqual" Type="String" />
                <asp:Parameter Name="ProcedureNameEqual" Type="String" />
                <asp:Parameter Name="DescriptionEqual" Type="String" />
                <asp:Parameter Name="ApplicationPathEqual" Type="String" />
                <asp:Parameter Name="ApplicationInfoPathEqual" Type="String" />
                <asp:Parameter Name="TypeEqual" Type="Int32" />
                <asp:Parameter Name="RemarkEqual" Type="String" />
                <asp:Parameter Name="SysTypeEqual" Type="String" />
                <asp:Parameter Name="CreatorEqual" Type="String" />
                <asp:Parameter Name="VersionNumberEqual" Type="Decimal" />
                <asp:Parameter Name="ProjectCodeEqual" Type="String" />
                <asp:Parameter Name="CreateUserEqual" Type="String" />
                <asp:Parameter Name="CreateDateEqual" Type="DateTime" />
                <asp:Parameter Name="ModifyUserEqual" Type="String" />
                <asp:Parameter Name="ModifyDateEqual" Type="DateTime" />
                <asp:Parameter DefaultValue="1" Name="ActivityEqual" Type="Int32" />
                <asp:Parameter Name="VersionDescriptionEqual" Type="String" />
                <asp:Parameter Name="ProcedureRemarkEqual" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
