<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DocumentFile.ascx.cs"
    Inherits="WorkFlowOperation_DocumentFile" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register Src="SelectSessionUserUnit.ascx" TagName="SelectSessionUserUnit"
    TagPrefix="uc3" %>
<%@ Register Src="../UserControls/attachmentlist.ascx" TagName="attachmentlist" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/attachmentadd.ascx" TagName="attachmentadd" TagPrefix="uc2" %>
<link href="/../Images/index.css" rel="stylesheet" type="text/css" />
<asp:ObjectDataSource ID="DocumentFileObjectDataSource" runat="server" DataObjectTypeName="RmsDM.MODEL.DocumentFileModel"
    DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetDocumentFileListOne"
    TypeName="RmsDM.BFL.DocumentFileBFL" UpdateMethod="Update" OnInserted="DocumentFileObjectDataSource_Inserted" OldValuesParameterFormatString="original_{0}">
    <SelectParameters>
        <asp:QueryStringParameter Name="Code" QueryStringField="DocumentFileCode" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:FormView ID="DocumentFileFormView" runat="server" DataSourceID="DocumentFileObjectDataSource" Width="100%" OnDataBound="DocumentFileFormView_DataBound" OnItemInserting="DocumentFileFormView_ItemInserting" OnItemUpdating="DocumentFileFormView_ItemUpdating" DataKeyNames="Code">
    <EditItemTemplate>        
         <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="blackbordertd" align="right">
                    标&nbsp;&nbsp;&nbsp;&nbsp;题：&nbsp;</td>
                <td class="blackbordertdpaddingcontent" colspan="3">
                    <asp:TextBox ID="SubjectTextBox" runat="server" Text='<%# Bind("Subject") %>' CssClass="input" Width="50%"></asp:TextBox><font color="red">*</font>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="SubjectTextBox"
                                            ErrorMessage="RequiredFieldValidator">提示：请输入标题</asp:RequiredFieldValidator>&nbsp;
                </td>
            </tr>
            <tr>                
                <td class="blackbordertd" align="right" width="20%">
                   文&nbsp;&nbsp;&nbsp;&nbsp;号：&nbsp;</td>
                <td class="blackbordertdpaddingcontent" width="30%">
                     <asp:TextBox ID="FileCodeTextBox" runat="server" Text='<%# Bind("FileCode") %>' CssClass="input" Width="80%">
                    </asp:TextBox>&nbsp;
                </td>
                <td class="blackbordertd" align="right" width="20%">
                   申&nbsp;请&nbsp;日&nbsp;期：&nbsp;</td>
                <td class="blackbordertdpaddingcontent" width="30%">
                   <cc3:Calendar ID="ApplyDateTime" runat="server" CalendarResource="../Images/CalendarResource/"
                        ReadOnly="False" CalendarMode="Date" Display="True" Value='<%# Bind("ApplyDateTime") %>'>
                    </cc3:Calendar>&nbsp;
                </td>
            </tr>
            <tr>
                <td class="blackbordertd" align="right" width="20%">
                   质&nbsp;量&nbsp;记&nbsp;录&nbsp;分&nbsp;类&nbsp;号：&nbsp;</td>
                <td class="blackbordertdpaddingcontent" width="30%">
                    <asp:Label ID="SortCodeLabel" runat="server" Text='<%# Bind("SortCode") %>'></asp:Label>&nbsp;
                </td>
                <td class="blackbordertd" align="right" width="20%">
                    标&nbsp;识&nbsp;序&nbsp;号&nbsp;</td>
                <td class="blackbordertdpaddingcontent" width="30%">
                    <asp:TextBox ID="DoucmentMarkingSNLabel" runat="server" Text='<%# Bind("DoucmentMarkingSN") %>'
                        CssClass="input" Width="80%">
                    </asp:TextBox><font color="red">*</font>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DoucmentMarkingSNLabel"
                                            ErrorMessage="RequiredFieldValidator">提示：请输入标识序号</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="blackbordertd" align="right">
                    部&nbsp;&nbsp;&nbsp;&nbsp;门：&nbsp;</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="ApplyDepartmentCodeLabel" runat="server"></asp:Label>
                    <asp:HiddenField ID="ApplyDepartmentCodeHiddenField" runat="server" Value='<%# Bind("ApplyDepartmentCode") %>' />
                    &nbsp;
                </td>
                <td class="blackbordertd" align="right">
                    经&nbsp;办&nbsp;人：&nbsp;</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="ApplyUserCodeLabel" runat="server"></asp:Label>
                    <asp:HiddenField ID="ApplyUserCodeHiddenField" runat="server" Value='<%# Bind("ApplyUserCode") %>' />  &nbsp;
                </td>
            </tr>
            <tr>
                <td class="blackbordertd" align="right">
                    附件文档：&nbsp;</td>
                <td colspan="5" class="blackbordertdpaddingcontent">
                    <uc2:attachmentadd ID="Attachmentadd1" runat="server" AttachMentType="DocumentFile"
                        MasterCode='<%# Eval("Code") %>' />
                </td>
            </tr>
            <tr>
                <td class="blackbordertd" align="right">
                    主要内容：&nbsp;</td>
                <td colspan="5" class="blackbordertdpaddingcontent">
                    <asp:TextBox ID="ContentLabel" runat="server" Text='<%# Bind("Content") %>' TextMode="MultiLine" Width="100%" Height="150px"></asp:TextBox>&nbsp;</td>
            </tr>
            <tr>
                <td class="blackbordertd" align="right">
                    备注：&nbsp;</td>
                <td colspan="5" class="blackbordertdpaddingcontent">
                    <asp:TextBox ID="RemarkLabel" runat="server" Text='<%# Bind("Remark") %>' TextMode="MultiLine" Width="100%" Height="50px"></asp:TextBox>&nbsp;
                </td>
            </tr>
        </table>
    </EditItemTemplate>
    <InsertItemTemplate>
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="blackbordertd" align="right">
                    标&nbsp;&nbsp;&nbsp;&nbsp;题：&nbsp;</td>
                <td class="blackbordertdpaddingcontent" colspan="3">
                    <asp:TextBox ID="SubjectTextBox" runat="server" Text='<%# Bind("Subject") %>' CssClass="input" Width="50%"></asp:TextBox>
                    <font color="red">*</font>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="SubjectTextBox"
                                            ErrorMessage="RequiredFieldValidator">提示：请输入标题</asp:RequiredFieldValidator>&nbsp;
                </td>
            </tr>
            <tr>
                 <td class="blackbordertd" align="right" width="20%">
                   文&nbsp;&nbsp;&nbsp;&nbsp;号：&nbsp;</td>
                <td class="blackbordertdpaddingcontent" width="30%">
                     <asp:TextBox ID="FileCodeTextBox" runat="server" Text='<%# Bind("FileCode") %>' CssClass="input" Width="80%">
                    </asp:TextBox>&nbsp;
                </td>
                <td class="blackbordertd" align="right" width="20%">
                   申&nbsp;请&nbsp;日&nbsp;期：&nbsp;</td>
                <td class="blackbordertdpaddingcontent" width="30%">
                   <cc3:Calendar ID="ApplyDateTime" runat="server" CalendarResource="../Images/CalendarResource/"
                        ReadOnly="False" CalendarMode="Date" Display="True" Value='<%# Bind("ApplyDateTime") %>'>
                    </cc3:Calendar>&nbsp;
                </td>
            </tr>
            <tr>
                <td class="blackbordertd" align="right" width="20%">
                   质&nbsp;量&nbsp;记&nbsp;录&nbsp;分&nbsp;类&nbsp;号：&nbsp;</td>
                <td class="blackbordertdpaddingcontent" width="30%">
                    <asp:Label ID="SortCodeLabel" runat="server" Text='<%# Bind("SortCode") %>'></asp:Label>&nbsp;
                </td>
                <td class="blackbordertd" align="right" width="20%">
                    标&nbsp;识&nbsp;序&nbsp;号&nbsp;</td>
                <td class="blackbordertdpaddingcontent" width="30%">
                    <asp:TextBox ID="DoucmentMarkingSNLabel" runat="server" Text='<%# Bind("DoucmentMarkingSN") %>'
                        CssClass="input" Width="80%">
                    </asp:TextBox><font color="red">*</font>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DoucmentMarkingSNLabel"
                                            ErrorMessage="RequiredFieldValidator">提示：请输入标识序号</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="blackbordertd" align="right">
                    部&nbsp;&nbsp;&nbsp;&nbsp;门：&nbsp;</td>
                <td class="blackbordertdpaddingcontent">
                    <uc3:SelectSessionUserUnit ID="SelectSessionUserUnit" runat="server" UserCode='<%# UserCode %>'
                        SelectedValue='<%# Bind("ApplyDepartmentCode") %>'  OnSelectedIndexChanged="SelectSessionUserUnit_SelectedIndexChanged"/>
                    &nbsp;
                </td>
                <td class="blackbordertd" align="right">
                    经&nbsp;办&nbsp;人：&nbsp;</td>
                <td class="blackbordertdpaddingcontent">                   
                    <asp:Label ID="ApplyUserCodeLabel" runat="server"></asp:Label>
                    <asp:HiddenField ID="ApplyUserCodeHiddenField" runat="server" Value='<%# Bind("ApplyUserCode") %>' /> &nbsp;
                </td>
            </tr>
            <tr>
                <td class="blackbordertd" align="right">
                    附件文档：&nbsp;</td>
                <td colspan="5" class="blackbordertdpaddingcontent">
                    <uc2:attachmentadd ID="Attachmentadd1" runat="server" AttachMentType="DocumentFile"
                        MasterCode='<%# Eval("Code") %>' />
                </td>
            </tr>
            <tr>
                <td class="blackbordertd" align="right">
                    主要内容：&nbsp;</td>
                <td colspan="5" class="blackbordertdpaddingcontent">
                    <asp:TextBox ID="ContentLabel" runat="server" Text='<%# Bind("Content") %>' TextMode="MultiLine" Width="100%" Height="150px"></asp:TextBox>&nbsp;</td>
            </tr>
            <tr>
                <td class="blackbordertd" align="right">
                    备注：&nbsp;</td>
                <td colspan="5" class="blackbordertdpaddingcontent">
                    <asp:TextBox ID="RemarkLabel" runat="server" Text='<%# Bind("Remark") %>' TextMode="MultiLine" Width="100%" Height="50px"></asp:TextBox>&nbsp;
                </td>
            </tr>
        </table>
       
    </InsertItemTemplate>
    <ItemTemplate>
         <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="blackbordertd" align="right">
                    标&nbsp;&nbsp;&nbsp;&nbsp;题：&nbsp;</td>
                <td class="blackbordertdpaddingcontent" colspan="3">
                    <asp:Label ID="SubjectLabel" runat="server" Text='<%# Bind("Subject") %>'></asp:Label>&nbsp;
                </td>
            </tr>
            <tr> 
             <td class="blackbordertd" align="right" width="20%">
                   文&nbsp;&nbsp;&nbsp;&nbsp;号：&nbsp;</td>
                <td class="blackbordertdpaddingcontent" width="30%">
                      <asp:Label ID="FileCodeLabel" runat="server" Text='<%# Bind("FileCode") %>'>
                    </asp:Label>&nbsp;
                </td> 
                <td class="blackbordertd" align="right" width="20%">
                   申&nbsp;请&nbsp;日&nbsp;期：&nbsp;</td>
                <td class="blackbordertdpaddingcontent" width="30%">
                   <asp:Label ID="Label1" runat="server" Text='<%# Bind("ApplyDateTime") %>'>
                    </asp:Label>&nbsp;
                </td>
            </tr>
            <tr>
                <td class="blackbordertd" align="right" width="20%">
                  质&nbsp;量&nbsp;记&nbsp;录&nbsp;分&nbsp;类&nbsp;号：&nbsp;</td>
                <td class="blackbordertdpaddingcontent" width="30%">
                    <asp:Label ID="SortCodeLabel" runat="server" Text='<%# Bind("SortCode") %>'></asp:Label>&nbsp;
                </td>
                <td class="blackbordertd" align="right" width="20%">
                    标&nbsp;识&nbsp;序&nbsp;号&nbsp;</td>
                <td class="blackbordertdpaddingcontent" width="30%">
                    <asp:Label ID="DoucmentMarkingSNLabel" runat="server" Text='<%# Bind("DoucmentMarkingSN") %>'>
                    </asp:Label>&nbsp;
                </td>
            </tr>
            <tr>
                <td class="blackbordertd" align="right">
                    部&nbsp;&nbsp;&nbsp;&nbsp;门：&nbsp;</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="ApplyDepartmentCodeLabel" runat="server" Text='<%# RmsPM.BLL.SystemRule.GetUnitName((Eval("ApplyDepartmentCode")).ToString()) %>'>
                    </asp:Label>&nbsp;
                </td>
                <td class="blackbordertd" align="right">
                    经&nbsp;办&nbsp;人：&nbsp;</td>
                <td class="blackbordertdpaddingcontent">
                    <asp:Label ID="ApplyUserCodeLabel" runat="server" Text='<%# WebFunctionRule.GetUserNameByCode((Eval("ApplyUserCode")).ToString()) %>'>
                    </asp:Label>&nbsp;
                </td>
            </tr>
            <tr>
                <td class="blackbordertd" align="right">
                    附件文档：&nbsp;</td>
                <td colspan="5" class="blackbordertdpaddingcontent">
                   <uc1:attachmentlist ID="Attachmentlist1" runat="server" AttachMentType="DocumentFile"
                        MasterCode='<%# Eval("Code") %>' /> &nbsp;
                </td>
            </tr>
            <tr>
                <td class="blackbordertd" align="right">
                    主要内容：&nbsp;</td>
                <td colspan="5" class="blackbordertdpaddingcontent">
                    <asp:Label ID="ContentLabel" runat="server" Text='<%# Bind("Content") %>'></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="blackbordertd" align="right">
                    备注：&nbsp;</td>
                <td colspan="5" class="blackbordertdpaddingcontent">
                    <asp:Label ID="RemarkLabel" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>&nbsp;
                </td>
            </tr>
        </table>
        
    </ItemTemplate>
</asp:FormView>
