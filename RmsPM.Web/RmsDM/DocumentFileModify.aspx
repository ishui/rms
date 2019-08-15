<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocumentFileModify.aspx.cs"
    Inherits="RmsDM_DocumentFileModify" %>

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
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>文件管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Images/index.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>

    <script language="javascript" type="text/javascript">
		function SelectUnit()
		{
			OpenMiddleWindow("../SelectBox/SelectUnit.aspx?UnitCode=000000");
		}
		function SelectUnitReturn(code, name)
		{
			
			window.document.all("FormView1_txtUnitName").value = name;
			window.document.all("FormView1_txtUnit").value = code;
	
		}
		function SelectTemplate()
		{
			OpenMiddleWindow("SelectDocumentTemplate.aspx?ReturnFunction=ReturnTemplate");
		}	
		function ReturnTemplate(Code,Name)
		{
		   window.document.all("FormView1_txtTemplateName").value = Name;
			window.document.all("FormView1_txtTemplateCode").value = Code;
		}
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0" height="100%"
                bgcolor="#ffffff">
                <tr>
                    <td class="topic" align="center" background="../images/topic_bg.gif" style="height: 25px">
                        文件信息</td>
                </tr>
                <tr>
                    <td class="topic" valign="top" align="center">
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetDocumentFileListOne"
                            TypeName="RmsDM.BFL.DocumentFileBFL" DataObjectTypeName="RmsDM.MODEL.DocumentFileModel"
                            DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted">
                            <SelectParameters>
                                <asp:QueryStringParameter DefaultValue="" Name="Code" QueryStringField="DocumentFileCode"
                                    Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%"
                            DataKeyNames="Code" OnDataBound="FormView1_DataBound" OnItemInserting="FormView1_ItemInserting"
                            OnItemDeleted="FormView1_ItemDeleted" OnItemInserted="FormView1_ItemInserted"
                            OnItemUpdated="FormView1_ItemUpdated">
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
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
                                    <tr>
                                        <td width="120" class="form-item">
                                            主题：
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="SubjectLabel" CssClass="input" runat="server" Text='<%# Bind("Subject") %>'
                                                Width="100%" Height="20"></asp:TextBox>&nbsp;</td>
                                                
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            份数(份)：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCounts" CssClass="input" runat="server" Text='<%# Bind("Counts") %>'></asp:TextBox>&nbsp;</td>
                                        <td class="form-item">
                                            页数(页)：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtLeaves" CssClass="input" runat="server" Text='<%# Bind("Leaves") %>'></asp:TextBox>&nbsp;</td>
                                                
                                    </tr>
                                    <tr>
                                        <td width="120" class="form-item">
                                            质量记录分类号：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="SortCodeLabel" CssClass="input" runat="server" Text='<%# Bind("SortCode") %>'></asp:TextBox>&nbsp;</td>
                                        <td class="form-item">
                                            文件标识序号：</td>
                                        <td>
                                            <asp:TextBox ID="DoucmentMarkingSNLabel" CssClass="input" runat="server" Text='<%# Bind("DoucmentMarkingSN") %>'></asp:TextBox>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            文号：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="FileCodeLabel" CssClass="input" runat="server" Text='<%# Bind("FileCode") %>'></asp:TextBox>&nbsp;
                                        </td>
                                        <td class="form-item">
                                            申请人：</td>
                                        <td>
                                            <uc1:InputUser ID="ucAccountant" runat="server" Value='<%# Bind("ApplyUserCode") %>'
                                                Visible="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            申请部门：</td>
                                        <td>
                                            <input id="txtUnit" runat="server" class="input" name="txtUnit" size="8" style="width: 72px;
                                                height: 18px" type="hidden" value='<%# Bind("ApplyDepartmentCode") %>' /><input id="txtUnitName"
                                                    runat="server" class="input" name="txtUnit" style="width: 121px; height: 18px"
                                                    type="text" /><img onclick="SelectUnit();return false;" src="../images/ToolsItemSearch.gif"
                                                        style="cursor: hand" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUnitName"
                                                ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td class="form-item">
                                            申请日期：</td>
                                        <td>
                                            <asp:TextBox ID="ApplyDateTimeLabel" CssClass="input" runat="server" Text='<%# Bind("ApplyDateTime") %>'>
                                            </asp:TextBox><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            内容：</td>
                                        <td colspan="3">
                                            <asp:TextBox ID="ContentLabel" runat="server" Text='<%# Bind("Content") %>' TextMode="MultiLine"
                                                Width="100%" Height="80px"></asp:TextBox><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            附件
                                        </td>
                                        <td colspan="3" class="blackbordertdpaddingcontent">
                                            <uc1:AttachMentAdd ID="Attachmentadd1" runat="server" CtrlPath="../UserControls/"
                                                AttachMentType="DocumentFile" MasterCode='<%# Eval("Code") %>'></uc1:AttachMentAdd>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            业务类别：</td>
                                        <td>
                                            <asp:TextBox ID="OperationTypeLabel" CssClass="input" runat="server" Text='<%# Bind("OperationType") %>'>
                                            </asp:TextBox>
                                        </td>
                                        <td class="form-item">
                                            版本号：</td>
                                        <td>
                                            <asp:TextBox ID="VersionNumberLabel" CssClass="input" runat="server" Text='<%# Bind("VersionNumber") %>'>
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            使用模板：</td>
                                        <td>
                                            <input id="txtTemplateCode" runat="server" class="input" name="txtTemplateCode" size="8"
                                                style="width: 72px; height: 18px" type="hidden" value='<%#Bind("FileTemplateCode") %>' />
                                            <input id="txtTemplateName" runat="server" class="input" name="txtTemplateName" style="width: 121px;
                                                height: 18px" type="text" />
                                            <img onclick="SelectTemplate();return false;" src="../images/ToolsItemSearch.gif"
                                                style="cursor: hand" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTemplateName"
                                                ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">取消模板</asp:LinkButton></td>
                                        <br />
                                        <td class="form-item">
                                            归档状态：</td>
                                        <td>
                                            <asp:TextBox ID="ArchiveStateLabel" CssClass="input" runat="server" Text='<%# Bind("ArchiveState") %>'>
                                            </asp:TextBox><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            归档类型：</td>
                                        <td>
                                            <asp:TextBox ID="ArchiveTypeLabel" CssClass="input" runat="server" Text='<%# Bind("ArchiveType") %>'>
                                            </asp:TextBox><br />
                                        </td>
                                        <td class="form-item">
                                            归档日期：</td>
                                        <td>
                                            <asp:TextBox ID="ArchiveDatetimeLabel" CssClass="input" runat="server" Text='<%# Bind("ArchiveDatetime") %>'>
                                            </asp:TextBox><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            审批日期：</td>
                                        <td>
                                            <asp:TextBox ID="AuditingDatetimeLabel" CssClass="input" runat="server" Text='<%# Bind("AuditingDatetime") %>'>
                                            </asp:TextBox><br />
                                        </td>
                                        <td class="form-item">
                                            审批状态：</td>
                                        <td>
                                            <asp:TextBox ID="AuditingStateLabel" CssClass="input" runat="server" Text='<%# Bind("AuditingState") %>'>
                                            </asp:TextBox><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            最后修改人：</td>
                                        <td>
                                            <uc1:InputUser ID="ucAccountantLast" runat="server" Value='<%# Bind("LastModifyByUserCode") %>'
                                                Visible="true" />
                                        </td>
                                        <td class="form-item">
                                            最后修改日期：</td>
                                        <td>
                                            <asp:TextBox ID="LastModifyDatetimeLabel" CssClass="input" runat="server" Text='<%# Bind("LastModifyDatetime") %>'>
                                            </asp:TextBox><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            创建人：</td>
                                        <td>
                                            <asp:TextBox ID="CreateUserCodeLabel" CssClass="input" runat="server" Text='<%# Bind("CreateUserCode") %>'>
                                            </asp:TextBox>
                                        </td>
                                        <td class="form-item">
                                            创建日期：</td>
                                        <td>
                                            <asp:TextBox ID="CreateDateLabel" runat="server" Text='<%# Bind("CreateDate") %>'>
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            备注：</td>
                                        <td colspan="3">
                                            <asp:TextBox ID="RemarkLabel" runat="server" Text='<%# Bind("Remark") %>' TextMode="MultiLine"
                                                Width="100%" Height="80px"></asp:TextBox><br />
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
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
                                    <tr>
                                        <td width="120" class="form-item">
                                            主题：
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="SubjectLabel" CssClass="input" runat="server" Text='<%# Bind("Subject") %>'
                                                Width="100%" Height="20"></asp:TextBox>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            份数：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCounts" CssClass="input" runat="server" Text='<%# Bind("Counts") %>'></asp:TextBox>&nbsp;</td>
                                        <td class="form-item">
                                            页数：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtLeaves" CssClass="input" runat="server" Text='<%# Bind("Leaves") %>'></asp:TextBox>&nbsp;</td>
                                                
                                    </tr>
                                    <tr>
                                        <td width="120" class="form-item">
                                            质量记录分类号：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="SortCodeLabel" CssClass="input" runat="server" Text='<%# Bind("SortCode") %>'></asp:TextBox>&nbsp;</td>
                                        <td class="form-item">
                                            文件标识序号：</td>
                                        <td>
                                            <asp:TextBox ID="DoucmentMarkingSNLabel" CssClass="input" runat="server" Text='<%# Bind("DoucmentMarkingSN") %>'></asp:TextBox>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            文号：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="FileCodeLabel" CssClass="input" runat="server" Text='<%# Bind("FileCode") %>'></asp:TextBox>&nbsp;
                                        </td>
                                        <td class="form-item">
                                            申请人：</td>
                                        <td>
                                            <uc1:InputUser ID="ucAccountant" runat="server" Value='<%# Bind("ApplyUserCode") %>'
                                                Visible="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            申请部门：</td>
                                        <td>
                                            <asp:Label ID="ApplyDepartmentCodeLabel" runat="server">
                                            </asp:Label>
                                        </td>
                                        <td class="form-item">
                                            申请日期：</td>
                                        <td>
                                            <cc3:Calendar ID="ApplyDateTime" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                                Display="True" ReadOnly="False" Value='<%# Bind("ApplyDateTime") %>'>
                                            </cc3:Calendar>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            内容：</td>
                                        <td colspan="3">
                                            <asp:TextBox ID="ContentLabel" runat="server" Text='<%# Bind("Content") %>' TextMode="MultiLine"
                                                Width="100%" Height="80px"></asp:TextBox><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            附件
                                        </td>
                                        <td colspan="3" class="blackbordertdpaddingcontent">
                                            <uc1:AttachMentAdd ID="Attachmentadd1" runat="server" CtrlPath="../UserControls/"
                                                AttachMentType="DocumentFile" MasterCode='<%# Eval("Code") %>'></uc1:AttachMentAdd>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            业务类别：</td>
                                        <td>
                                            <asp:TextBox ID="OperationTypeLabel" CssClass="input" runat="server" Text='<%# Bind("OperationType") %>'>
                                            </asp:TextBox>
                                        </td>
                                        <td class="form-item">
                                            版本号：</td>
                                        <td>
                                            <asp:TextBox ID="VersionNumberLabel" CssClass="input" runat="server" Text='<%# Bind("VersionNumber") %>'>
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            使用模板：</td>
                                        <td>
                                            <asp:Label ID="FileTemplateCodeLabel" runat="server" />
                                        </td>
                                        <td class="form-item">
                                            归档状态：</td>
                                        <td>
                                            <asp:TextBox ID="ArchiveStateLabel" CssClass="input" runat="server" Text='<%# Bind("ArchiveState") %>'>
                                            </asp:TextBox><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            归档类型：</td>
                                        <td>
                                            <asp:TextBox ID="ArchiveTypeLabel" CssClass="input" runat="server" Text='<%# Bind("ArchiveType") %>'>
                                            </asp:TextBox><br />
                                        </td>
                                        <td class="form-item">
                                            归档日期：</td>
                                        <td>
                                            <cc3:Calendar ID="ArchiveDatetime" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                                Display="True" ReadOnly="False" Value='<%# Bind("ArchiveDatetime") %>'>
                                            </cc3:Calendar>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            审批日期：</td>
                                        <td>
                                            <cc3:Calendar ID="AuditingDatetime" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                                Display="True" ReadOnly="False" Value='<%# Bind("AuditingDatetime") %>'>
                                            </cc3:Calendar>
                                        </td>
                                        <td class="form-item">
                                            审批状态：</td>
                                        <td>
                                            <asp:TextBox ID="AuditingStateLabel" CssClass="input" runat="server" Text='<%# Bind("AuditingState") %>'>
                                            </asp:TextBox><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            最后修改人：</td>
                                        <td>
                                            <uc1:InputUser ID="InputUser1" runat="server" Value='<%# Bind("LastModifyByUserCode") %>'
                                                Visible="true" />
                                        </td>
                                        <td class="form-item">
                                            最后修改日期：</td>
                                        <td>
                                            <cc3:Calendar ID="LastModifyDatetime" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                                Display="True" ReadOnly="False" Value='<%# Bind("LastModifyDatetime") %>'>
                                            </cc3:Calendar>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            创建人：</td>
                                        <td>
                                            <asp:TextBox ID="CreateUserCodeLabel" CssClass="input" runat="server" Text='<%# Bind("CreateUserCode") %>'>
                                            </asp:TextBox>
                                        </td>
                                        <td class="form-item">
                                            创建日期：</td>
                                        <td>
                                            <cc3:Calendar ID="CreateDate" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                                Display="True" ReadOnly="False" Value='<%# Bind("CreateDate") %>'>
                                            </cc3:Calendar>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            备注：</td>
                                        <td colspan="3">
                                            <asp:TextBox ID="RemarkLabel" runat="server" Text='<%# Bind("Remark") %>' TextMode="MultiLine"
                                                Width="100%" Height="80px"></asp:TextBox><br />
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
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
                                    <tr>
                                        <td width="120" class="form-item">
                                            主题：
                                        </td>
                                        <td colspan="3">
                                            <asp:Label ID="SubjectLabel" runat="server" Text='<%# Bind("Subject") %>'></asp:Label>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            份数：
                                        </td>
                                        <td>
                                            <asp:label ID="lblCounts"  runat="server" Text='<%# Bind("Counts") %>'></asp:label>&nbsp;</td>
                                        <td class="form-item">
                                            页数：
                                        </td>
                                        <td>
                                            <asp:label ID="lblLeaves" runat="server" Text='<%# Bind("Leaves") %>'></asp:label>&nbsp;</td>
                                                
                                    </tr>
                                    <tr>
                                        <td width="120" class="form-item">
                                            质量记录分类号：
                                        </td>
                                        <td>
                                            <asp:Label ID="SortCodeLabel" runat="server" Text='<%# Bind("SortCode") %>'></asp:Label>&nbsp;</td>
                                        <td class="form-item">
                                            文件标识序号：</td>
                                        <td>
                                            <asp:Label ID="DoucmentMarkingSNLabel" runat="server" Text='<%# Bind("DoucmentMarkingSN") %>'></asp:Label>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            文号：
                                        </td>
                                        <td>
                                            <asp:Label ID="FileCodeLabel" runat="server" Text='<%# Bind("FileCode") %>'></asp:Label>&nbsp;
                                        </td>
                                        <td class="form-item">
                                            申请人：</td>
                                        <td>
                                            <asp:Label ID="ApplyUserCodeLabel" runat="server" Text='<%# Bind("ApplyUserCode") %>'>
                                            </asp:Label>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            申请部门：</td>
                                        <td>
                                            <asp:Label ID="ApplyDepartmentCodeLabel" runat="server" Text='<%# Bind("ApplyDepartmentCode") %>'>
                                            </asp:Label><br />
                                        </td>
                                        <td class="form-item">
                                            申请日期：</td>
                                        <td>
                                            <asp:Label ID="ApplyDateTimeLabel" runat="server" Text='<%# Bind("ApplyDateTime") %>'>
                                            </asp:Label><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            内容：</td>
                                        <td colspan="3">
                                            <asp:Label ID="ContentLabel" runat="server" Text='<%# Bind("Content") %>'></asp:Label><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            附件</td>
                                        <td colspan="3" class="blackbordertdpaddingcontent">
                                            &nbsp;
                                            <uc1:AttachMentList ID="Attachmentlist1" runat="server" CtrlPath="../UserControls/"
                                                AttachMentType="DocumentFile" MasterCode='<%# Eval("Code") %>'></uc1:AttachMentList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            业务类别：</td>
                                        <td>
                                            <asp:Label ID="OperationTypeLabel" runat="server" Text='<%# Bind("OperationType") %>'>
                                            </asp:Label>
                                        </td>
                                        <td class="form-item">
                                            版本号：</td>
                                        <td>
                                            <asp:Label ID="VersionNumberLabel" runat="server" Text='<%# Bind("VersionNumber") %>'>
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            使用模板：</td>
                                        <td>
                                            <asp:Label ID="FileTemplateCodeLabel" runat="server" Text='<%# RmsDM.BFL.FileTemplateBFL.GetTemplateName((Eval("FileTemplateCode")).ToString()) %>'>
                                            </asp:Label><br />
                                        </td>
                                        <td class="form-item">
                                            归档状态：</td>
                                        <td>
                                            <asp:Label ID="ArchiveStateLabel" runat="server" Text='<%# Bind("ArchiveState") %>'>
                                            </asp:Label><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            归档类型：</td>
                                        <td>
                                            <asp:Label ID="ArchiveTypeLabel" runat="server" Text='<%# Bind("ArchiveType") %>'>
                                            </asp:Label><br />
                                        </td>
                                        <td class="form-item">
                                            归档日期：</td>
                                        <td>
                                            <asp:Label ID="ArchiveDatetimeLabel" runat="server" Text='<%# Bind("ArchiveDatetime") %>'>
                                            </asp:Label><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            审批日期：</td>
                                        <td>
                                            <asp:Label ID="AuditingDatetimeLabel" runat="server" Text='<%# Bind("AuditingDatetime") %>'>
                                            </asp:Label><br />
                                        </td>
                                        <td class="form-item">
                                            审批状态：</td>
                                        <td>
                                            <asp:Label ID="AuditingStateLabel" runat="server" Text='<%# Bind("AuditingState") %>'>
                                            </asp:Label><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            最后修改人：</td>
                                        <td>
                                            <asp:Label ID="LastModifyByUserCodeLabel" runat="server" Text='<%# Bind("LastModifyByUserCode") %>'>
                                            </asp:Label><br />
                                        </td>
                                        <td class="form-item">
                                            最后修改日期：</td>
                                        <td>
                                            <asp:Label ID="LastModifyDatetimeLabel" runat="server" Text='<%# Bind("LastModifyDatetime") %>'>
                                            </asp:Label><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            创建人：</td>
                                        <td>
                                            <asp:Label ID="CreateUserCodeLabel" runat="server" Text='<%# Bind("CreateUserCode") %>'>
                                            </asp:Label>
                                        </td>
                                        <td class="form-item">
                                            创建日期：</td>
                                        <td>
                                            <asp:Label ID="CreateDateLabel" runat="server" Text='<%# Bind("CreateDate") %>'>
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            备注：</td>
                                        <td colspan="3">
                                            <asp:Label ID="RemarkLabel" runat="server" Text='<%# Bind("Remark") %>'></asp:Label><br />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:FormView>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
