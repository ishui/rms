<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LocaleViseInfo.aspx.cs" Inherits="LocaleVise_LocaleViseInfo" %>

<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>
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
<%@ Register TagPrefix="uc1" TagName="InputInDesignChange" Src="../UserControls/InputInDesignChange.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>签证维护</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>

    <link href="/../Images/index.css" rel="stylesheet" type="text/css" />
    <link href="../Images/GridPaginationSource/PaginationControlStyle.css" rel="stylesheet"
        type="text/css" />
    <link href="../Images/GridPaginationSource/PaginationControlStyle.css" rel="stylesheet"
        type="text/css" />
    <link href="/../Images/index.css" rel="stylesheet" type="text/css" />
    <link href="/../Images/index.css" rel="stylesheet" type="text/css" />
    <link href="../Images/GridPaginationSource/PaginationControlStyle.css" rel="stylesheet"
        type="text/css" />

    <script language="javascript">   
   

   
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
                                签证维护</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" OnItemInserted="FormView1_ItemInserted"
                        OnItemDeleted="FormView1_ItemDeleted" OnItemUpdated="FormView1_ItemUpdated" DataKeyNames="ViseCode"
                        Width="100%" OnItemInserting="FormView1_ItemInserting" OnItemCommand="FormView1_ItemCommand"
                        OnDataBound="FormView1_DataBound" OnItemUpdating="FormView1_ItemUpdating">
                        <EditItemTemplate>
                            <table class="table" id="tableToolBar" width="100%">
                                <tr>
                                    <td class="tools-area" width="16">
                                        <img src="../images/btn_li.gif" align="absMiddle"></td>
                                    <td class="tools-area">
                                        <asp:Button ID="btnSave" Text=" 保存 " CausesValidation="true" CssClass="button" runat="server"
                                            CommandName="Update" />
                                        <asp:Button ID="btnCancel" Text=" 取消 " CssClass="button" runat="server" CommandName="Cancel" />
                                        <input name="btnClose" id="btnClose" type="button" value=" 关闭 " class="button" runat="server"
                                            onclick="javascript:window.close();">
                                    </td>
                                </tr>
                            </table>
                            <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="form-item">
                                        签证名称：</td>
                                    <td>
                                        <asp:TextBox ID="ViseNameTextBox" runat="server" Text='<%# Bind("ViseName") %>' CssClass="input"></asp:TextBox>&nbsp;<font
                                            color="red">*</font>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ViseNameTextBox"
                                            ErrorMessage="RequiredFieldValidator">必填</asp:RequiredFieldValidator></td>
                                    <td class="form-item">
                                        合同选择：</td>
                                    <td>
                                        <uc3:inputcontract ID="ViseContractCodeTextBox" runat="server" ImagePath="../Images/"
                                            Value='<%# Bind("ViseContractCode") %>' ProjectCode='<%# _projectCode+"" %>' />
                                        <%if (this.up_sPMNameLower == "tianyangoa")
                                          { %>
                                        <a href="#" onclick='javascript:GetViseID();'>获取签证自动编号</a><%} %>
                                    </td>
                                    <td class="form-item">
                                        签证时间：</td>
                                    <td>
                                        <cc3:Calendar ID="ViseDateTextBox" runat="server" CalendarResource="../Images/CalendarResource/"
                                            ReadOnly="False" Display="True" Value='<%# Bind("ViseDate") %>'>
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        签证编号：</td>
                                    <td>
                                        <asp:TextBox ID="ViseIdTextBox" runat="server" Text='<%# Bind("ViseId") %>' CssClass="input"></asp:TextBox>&nbsp;<font
                                            color="red">*</font> <a href="#" id="openViseIdSelectBox" onclick="openwin();" style='display: <%if (this.up_sPMNameLower != "yefengpm" ){%>"none"<%}else{%>""<%}%>'>
                                                选择</a>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ViseIdTextBox"
                                            ErrorMessage="RequiredFieldValidator">必填</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="form-item">
                                        施工单位编号：</td>
                                    <td>
                                        <asp:TextBox ID="ViseId2TextBox" runat="server" Text='<%# Bind("ViseId2") %>' CssClass="input"></asp:TextBox>
                                    </td>
                                    <%if (this.up_sPMNameLower == "yefengpm")
                                      { %>
                                    <td class="form-item">
                                        事件缘由：</td>
                                    <td colspan="4" id="CaseReason" runat="server">
                                        <uc1:InputInDesignChange Value='<%# Bind("ViseReferCode") %>' ID="InputInDesignChange"
                                            runat="server"></uc1:InputInDesignChange>
                                    </td>
                                    <%} %>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        签证类型：</td>
                                    <td>
                                        <uc1:InputSystemGroup ID="inputSystemGroup" ClassCode="22" Value='<%# Bind("ViseType") %>'
                                            runat="server" SelectAllLeaf="True" AutoPostBack="true" OnChange="inputSystemGroup_OnChange">
                                        </uc1:InputSystemGroup>
                                        &nbsp;<font color="red">*</font>
                                    </td>
                                    <%if ("yefengpm" != this.up_sPMNameLower)
                                      {%>
                                    <td class="form-item">
                                        原因选择：</td>
                                    <td colspan="3">
                                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" DataSourceID="XmlDataSource1"
                                            DataTextField="text" DataValueField="value" RepeatDirection="Horizontal" OnDataBound="CheckBoxList1_DataBound">
                                        </asp:CheckBoxList><span runat="server" id="SystemGroupSpan"></span></td>
                                    <%} %>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        经办部门：</td>
                                    <td>
                                        <uc2:InputUnit ID="ViseUnitTextBox" runat="server" Value='<%# Bind("ViseUnit") %>'></uc2:InputUnit>
                                        &nbsp;<font color="red">*</font> <span runat="server" id="UnitMsgSpan"></span>
                                    </td>
                                    <td class="form-item">
                                        经办人：</td>
                                    <td>
                                        <uc1:InputUser ID="VisePersonTextBox" runat="server" Value='<%# Bind("VisePerson") %>'>
                                        </uc1:InputUser>
                                        &nbsp;<font color="red">*</font> <span runat="server" id="PersonMsgSpan"></span>
                                    </td>
                                    <td class="form-item">
                                        承包商：</td>
                                    <td>
                                        <uc1:InputSupplier ID="ViseSupplierTextBox" runat="server" Value='<%# Bind("ViseSupplier") %>'>
                                        </uc1:InputSupplier>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        办理期限：</td>
                                    <td colspan="5">
                                        <cc3:Calendar ID="ViseEndDateTextBox" runat="server" CalendarResource="../Images/CalendarResource/"
                                            ReadOnly="False" Display="True" Value='<%# Bind("ViseEndDate") %>'>
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" width="15%">
                                        <%if ("yefengpm" == this.up_sPMNameLower)
                                                                         { %>
                                        <font color="red">*</font><%} %>
                                        签证原因：</td>
                                    <td colspan="5">
                                        <asp:TextBox ID="ViseReasonTextBox" runat="server" Text='<%# Bind("ViseReason") %>'
                                            Height="80px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                      
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4"  runat="server" ControlToValidate="ViseReasonTextBox"
                                            ErrorMessage="RequiredFieldValidator" Enabled="false">必填</asp:RequiredFieldValidator>
                                        <uc1:AttachMentAdd ID="AttachMentAdd1" runat="server" MasterCode='<%# Eval("ViseCode") %>'
                                            AttachMentType="ViseReason" Visible="false"></uc1:AttachMentAdd>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" width="15%">
                                        附&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;件：</td>
                                    <td colspan="5">
                                        <uc1:AttachMentAdd ID="AttachMentAdd3" runat="server" MasterCode='<%# Eval("ViseCode") %>'
                                            AttachMentType="ViseRemark"></uc1:AttachMentAdd>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" width="15%">
                                        备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：</td>
                                    <td colspan="5">
                                        <asp:TextBox ID="ViseRemarkTextBox" runat="server" Text='<%# Bind("ViseRemark") %>'
                                            Height="60px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <%if (this.up_sPMNameLower != "yefengpm")
                                   { %>
                                <tr>
                                    <td class="form-item" width="15%">
                                        监理签证：</td>
                                    <td colspan="5">
                                        <asp:TextBox ID="ViseScrutinyTextBox" runat="server" Text='<%# Bind("ViseScrutiny") %>'
                                            Height="80px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                        <uc1:AttachMentAdd ID="AttachMentAdd2" runat="server" MasterCode='<%# Eval("ViseCode") %>'
                                            AttachMentType="ViseScrutiny" Visible="false"></uc1:AttachMentAdd>
                                    </td>
                                </tr>
                                <%} %>
                            </table>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <table class="table" id="tableToolBar" width="100%">
                                <tr>
                                    <td class="tools-area" width="16">
                                        <img src="../images/btn_li.gif" align="absMiddle"></td>
                                    <td class="tools-area">
                                        <asp:Button ID="btnSave" Text=" 保存 " CausesValidation="true" CssClass="button" runat="server"
                                            CommandName="Insert" />
                                        <input name="btnClose" id="btnClose" type="button" value=" 关闭 " class="button" runat="server"
                                            onclick="javascript:window.close();">
                                    </td>
                                </tr>
                            </table>
                            <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="form-item">
                                        签证名称：</td>
                                    <td>
                                        <asp:TextBox ID="ViseNameTextBox" runat="server" Text='<%# Bind("ViseName") %>' CssClass="input"></asp:TextBox>&nbsp;<font
                                            color="red">*</font>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ViseNameTextBox"
                                            ErrorMessage="RequiredFieldValidator">必填</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="form-item">
                                        合同选择：</td>
                                    <td>
                                        <uc3:inputcontract ID="ViseContractCodeTextBox" runat="server" ImagePath="../Images/"
                                            Value='<%# Bind("ViseContractCode") %>' ProjectCode='<%# _projectCode+"" %>' />
                                        <%if (this.up_sPMNameLower == "tianyangoa")
                                              { %>
                                        <a href="#" onclick='javascript:GetViseID();'>获取签证自动编号</a><%} %>
                                    </td>
                                    <td class="form-item">
                                        签证时间：</td>
                                    <td>
                                        <cc3:Calendar ID="ViseDateTextBox" runat="server" CalendarResource="../Images/CalendarResource/"
                                            ReadOnly="False" Display="True" Value='<%# Bind("ViseDate") %>'>
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        签证编号：</td>
                                    <td>
                                        <asp:TextBox ID="ViseIdTextBox" runat="server" Text='<%# Bind("ViseId") %>' CssClass="input"></asp:TextBox>&nbsp;<font
                                            color="red">*</font> <a href="#" id="openViseIdSelectBox" onclick="openwin();" style='display: <%if (this.up_sPMNameLower != "yefengpm" ){%>"none"<%}else{%>""<%}%>'>
                                                选择</a>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ViseIdTextBox"
                                            ErrorMessage="RequiredFieldValidator">必填</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="form-item">
                                        施工单位编号：</td>
                                    <td>
                                        <asp:TextBox ID="ViseId2TextBox" runat="server" Text='<%# Bind("ViseId2") %>' CssClass="input"></asp:TextBox>
                                    </td>
                                    <%if (this.up_sPMNameLower == "yefengpm")
                                     {%>
                                    <td class="form-item">
                                        事件缘由：</td>
                                    <td colspan="4" id="CaseReason" runat="server">
                                        <uc1:InputInDesignChange Value='<%# Bind("ViseReferCode") %>' ID="InputInDesignChange"
                                            runat="server"></uc1:InputInDesignChange>
                                    </td>
                                    <%} %>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        签证类型：</td>
                                    <td>
                                        <uc1:InputSystemGroup ID="inputSystemGroup" ClassCode="22" Value='<%# Bind("ViseType") %>'
                                            runat="server" SelectAllLeaf="True" AutoPostBack="true" OnChange="inputSystemGroup_OnChange">
                                        </uc1:InputSystemGroup>
                                        &nbsp;<font color="red">*</font> <span runat="server" id="SystemGroupSpan"></span>
                                    </td>
                                    <%if ("yefengpm" != this.up_sPMNameLower)
                                      {%>
                                    <td class="form-item">
                                        原因选择：</td>
                                    <td colspan="3">
                                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" DataSourceID="XmlDataSource1"
                                            DataTextField="text" DataValueField="value" RepeatDirection="Horizontal">
                                        </asp:CheckBoxList><span runat="server" id="Span1"></span></td>
                                    <%} %>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        经办部门：</td>
                                    <td>
                                        <uc2:InputUnit ID="ViseUnitTextBox" runat="server" Value='<%# Bind("ViseUnit") %>'></uc2:InputUnit>
                                        &nbsp;<font color="red">*</font> <span runat="server" id="UnitMsgSpan"></span>
                                    </td>
                                    <td class="form-item">
                                        经办人：</td>
                                    <td>
                                        <uc1:InputUser ID="VisePersonTextBox" runat="server" Value='<%# Bind("VisePerson") %>'>
                                        </uc1:InputUser>
                                        &nbsp;<font color="red">*</font> <span runat="server" id="PersonMsgSpan"></span>
                                    </td>
                                    <td class="form-item">
                                        承包商：</td>
                                    <td>
                                        <uc1:InputSupplier ID="ViseSupplierTextBox" runat="server" Value='<%# Bind("ViseSupplier") %>'>
                                        </uc1:InputSupplier>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        办理期限：</td>
                                    <td colspan="5">
                                        <cc3:Calendar ID="ViseEndDateTextBox" runat="server" CalendarResource="../Images/CalendarResource/"
                                            ReadOnly="False" Display="True" Value='<%# Bind("ViseEndDate") %>'>
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" width="15%">
                                        <%if ("yefengpm" == this.up_sPMNameLower)
                                      { %>
                                        <font color="red">*</font><%} %>
                                        签证原因：</td>
                                    <td colspan="5">
                                        <asp:TextBox ID="ViseReasonTextBox" runat="server" Text='<%# Bind("ViseReason") %>'
                                            Height="80px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                        
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"  ControlToValidate="ViseReasonTextBox"
                                            ErrorMessage="RequiredFieldValidator" Enabled="false">必填</asp:RequiredFieldValidator>
                                        <uc1:AttachMentAdd ID="AttachMentAdd1" runat="server" MasterCode='<%# Eval("ViseCode") %>'
                                            AttachMentType="ViseReason" Visible="false"></uc1:AttachMentAdd>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" width="15%">
                                        附&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;件：</td>
                                    <td colspan="5">
                                        <uc1:AttachMentAdd ID="AttachMentAdd3" runat="server" MasterCode='<%# Eval("ViseCode") %>'
                                            AttachMentType="ViseRemark"></uc1:AttachMentAdd>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" width="15%">
                                        备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：</td>
                                    <td colspan="5">
                                        <asp:TextBox ID="ViseRemarkTextBox" runat="server" Text='<%# Bind("ViseRemark") %>'
                                            Height="60px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <%if (this.up_sPMNameLower != "yefengpm")
                                   { %>
                                <tr>
                                    <td class="form-item" width="15%">
                                        监理签证：</td>
                                    <td colspan="5">
                                        <asp:TextBox ID="ViseScrutinyTextBox" runat="server" Text='<%# Bind("ViseScrutiny") %>'
                                            Height="80px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                        <uc1:AttachMentAdd ID="AttachMentAdd2" runat="server" MasterCode='<%# Eval("ViseCode") %>'
                                            AttachMentType="ViseScrutiny" Visible="false"></uc1:AttachMentAdd>
                                    </td>
                                </tr>
                                <%} %>
                            </table>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <table class="table" id="tableToolBar" width="100%">
                                <tr>
                                    <td class="tools-area" width="16">
                                        <img src="../images/btn_li.gif" align="absMiddle"></td>
                                    <td class="tools-area">
                                        <asp:Button ID="btnModify" Text=" 修改 " CssClass="button" runat="server" CommandName="Edit" />
                                        <asp:Button ID="btnDelete" Text=" 删除 " CssClass="button" runat="server" CommandName="Delete" />
                                        <input name="btnRequisition" id="btnRequisition" type="button" value=" 提交申请 " class="button"
                                            runat="server" onclick="javascript:OpenRequisition();return false;">
                                        <input name="btnAudit" id="btnAudit" type="button" value=" 审核 " class="button" runat="server"
                                            onclick="javascript:OpenAudit();return false;">
                                        <input name="btnPrint" id="btnPrint" type="button" value=" 审批表打印 " class="button"
                                            runat="server" onclick="javascript:OpenPrint();return false;">
                                        <asp:Button ID="btnBalance" Text=" 结算 " CssClass="button" runat="server" CommandName="Balance" />
                                        <input name="btnClose" id="btnClose" type="button" value=" 关闭 " class="button" runat="server"
                                            onclick="javascript:window.close();">
                                    </td>
                                </tr>
                            </table>
                            <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="form-item">
                                        签证名称：</td>
                                    <td>
                                        <%# Eval("ViseName") %>
                                    </td>
                                    <td class="form-item">
                                        合同名称：</td>
                                    <td>
                                        <a href="#" onclick="javascript:doViewContractInfo('<%# Eval("ViseContractCode") %>');return false;">
                                            <%# RmsPM.BLL.ContractRule.GetContractName((string)Eval("ViseContractCode")) %>
                                        </a>
                                    </td>
                                    <td class="form-item">
                                        合同编号：</td>
                                    <td>
                                        <a href="#" onclick="javascript:doViewContractInfo('<%# Eval("ViseContractCode") %>');return false;">
                                            <%# RmsPM.BLL.ContractRule.GetContractID((string)Eval("ViseContractCode")) %>
                                        </a>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        签证编号：</td>
                                    <td>
                                        <%# Eval("ViseId") %>
                                        <a href="#" id="openViseIdSelectBox" onclick="openwin();" style="display: none">选择</a> &nbsp;&nbsp;<font
                                            color="red"><%# RmsPM.BFL.LocaleViseBFL.GetViseStatusName((int)Eval("ViseStatus")) %></font>
                                    </td>
                                    <td class="form-item">
                                        施工单位编号：</td>
                                    <td>
                                        <%# Eval("ViseId2") %>
                                    </td>
                                    <td class="form-item">
                                        签证时间：</td>
                                    <td>
                                        <%# ((DateTime)Eval("ViseDate")).ToShortDateString() %>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        签证类型：</td>
                                    <td>
                                        <%# RmsPM.BLL.SystemGroupRule.GetSystemGroupFullName((string)Eval("ViseType"))%>
                                    </td>
                                    <%if ("yefengpm" != this.up_sPMNameLower)
                                      {%>
                                    <td class="form-item">
                                        原因选择：</td>
                                    <td colspan="3">
                                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" DataSourceID="XmlDataSource1"
                                            DataTextField="text" DataValueField="value" RepeatDirection="Horizontal" OnDataBound="CheckBoxList1_DataBound">
                                        </asp:CheckBoxList><span runat="server" id="SystemGroupSpan"></span></td>
                                    <%} %>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        经办部门：</td>
                                    <td>
                                        <%# RmsPM.BLL.SystemRule.GetUnitFullName((string)Eval("ViseUnit")) %>
                                    </td>
                                    <td class="form-item">
                                        经办人：</td>
                                    <td>
                                        <%# RmsPM.BLL.SystemRule.GetUserName((string)Eval("VisePerson")) %>
                                    </td>
                                    <td class="form-item">
                                        承包商：</td>
                                    <td>
                                        <a href="#" onclick="javascript:doViewSupplierInfo('<%# Eval("ViseSupplier") %>');return false;">
                                            <%# RmsPM.BLL.ProjectRule.GetSupplierName((string)Eval("ViseSupplier")) %>
                                        </a>
                                    </td>
                                </tr>
                                <%if (this.up_sPMNameLower == "yefengpm")
                                  {%>
                                <tr>
                                    <td class="form-item">
                                        事件缘由：</td>
                                    <td colspan="5" id="CaseReason" runat="server">
                                        <a href="#" onclick="javascript:OpenDesignChange('<%# Eval("ViseReferCode") %>');">
                                            <%# RmsPM.BLL.ConvertRule.ToInt(Eval("ViseReferCode")) == 0 ? "" : Eval("ViseReferCode") %>
                                        </a>
                                    </td>
                                </tr>
                                <%} %>
                                <tr>
                                    <td class="form-item">
                                        办理期限：</td>
                                    <td colspan="5">
                                        <%# ((DateTime)Eval("ViseEndDate")).ToShortDateString()%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" width="15%">
                                        签证原因：</td>
                                    <td colspan="5">
                                        <%# Eval("ViseReason").ToString().Replace("\n","<br>") %>
                                        <uc1:AttachMentList ID="AttachMentList1" runat="server" AttachMentType="ViseReason"
                                            MasterCode='<%# Eval("ViseCode") %>' Visible="false"></uc1:AttachMentList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" width="15%">
                                        附&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;件：</td>
                                    <td colspan="5">
                                        <uc1:AttachMentList ID="AttachMentList3" runat="server" AttachMentType="ViseRemark"
                                            MasterCode='<%# Eval("ViseCode") %>'></uc1:AttachMentList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" width="15%">
                                        备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：</td>
                                    <td colspan="5">
                                        <%# Eval("ViseRemark").ToString().Replace("\n", "<br>")%>
                                    </td>
                                </tr>
                                <%if (this.up_sPMNameLower != "yefengpm")
                                   { %>
                                <tr>
                                    <td class="form-item" width="15%">
                                        监理签证：</td>
                                    <td colspan="5">
                                        <%# Eval("ViseScrutiny").ToString().Replace("\n", "<br>")%>
                                        <uc1:AttachMentList ID="AttachMentList2" runat="server" AttachMentType="ViseScrutiny"
                                            MasterCode='<%# Eval("ViseCode") %>' Visible="false"></uc1:AttachMentList>
                                    </td>
                                </tr>
                                <%} %>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="TiannuoPM.MODEL.LocaleViseModel"
                        DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetLocalVise" TypeName="RmsPM.BFL.LocaleViseBFL"
                        UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="Code" QueryStringField="ViseCode" Type="Int32" />
                        </SelectParameters>
                        <InsertParameters>
                            <asp:Parameter Direction="ReturnValue" Name="Code" Type="Int32" DefaultValue="0" />
                        </InsertParameters>
                    </asp:ObjectDataSource>
                    <table cellspacing="0" cellpadding="0" width="470" border="0">
                        <tr id="webtabs">
                            <td width="20">
                            </td>
                            <td class="TabDisplay" id="workflowmsg" runat="server" width="185" nowrap onclick="EventClickTab(0);">
                                相关流程</td>
                            <td class="TabShow" width="285" nowrap onclick="EventClickTab(1);">
                                签证费用&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnAddDtl" CssClass="button-small" Text="新 增" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="0" cellpadding="0" width="100%" border="0" id="tabdiv">
                        <tr style="display: none;">
                            <td>
                                <uc4:WorkFlowList ID="WorkFlowList1" runat="server" />
                            </td>
                        </tr>
                        <tr style="display: block;">
                            <td>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource2"
                                    CssClass="List" Width="100%" ShowFooter="True">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <a href="#" onclick="javascript:AddDtl('<%# Eval("ViseCostCode") %>');return false;">
                                                    选择</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="费用项" SortExpression="CostCode">
                                            <ItemTemplate>
                                                <%# RmsPM.BLL.CostBudgetRule.GetCostBudgetSetName((string)Eval("CostBudgetSetCode")) %>
                                                &nbsp;
                                                <%# RmsPM.BLL.CBSRule.GetCostFullName((string)Eval("CostCode"))%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="金额(元)" SortExpression="Money">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <%# RmsPM.BLL.MathRule.GetDecimalShowString(Eval("Money")) %>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <FooterTemplate>
                                                合计：<%# RmsPM.BLL.MathRule.GetDecimalShowString(RmsPM.BFL.LocaleViseBFL.GetViseSumMoney((int)FormView1.DataKey.Value)) %>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="审批金额(元)">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <%# RmsPM.BLL.MathRule.GetDecimalShowString(Eval("CheckMoney")) %>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <FooterTemplate>
                                                合计：<%# RmsPM.BLL.MathRule.GetDecimalShowString(RmsPM.BFL.LocaleViseBFL.GetViseSumCheckMoney((int)FormView1.DataKey.Value)) %>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="备注" SortExpression="Remark">
                                            <ItemTemplate>
                                                <%# Eval("Remark").ToString().Replace("\n","<br>") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="list-title" />
                                    <HeaderStyle CssClass="list-title" />
                                    <FooterStyle CssClass="list-title" />
                                    <EmptyDataTemplate>
                                        无
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetLocalViseCosts"
                        TypeName="RmsPM.BFL.LocaleViseBFL">
                        <SelectParameters>
                            <asp:ControlParameter Type="int32" ControlID="FormView1" Name="Code" PropertyName="DataKey.Value" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/LocaleVise/ViseReasonConfig.xml"
                        XPath="Reason/Type[@Name='']/Item"></asp:XmlDataSource>
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Style="display: none">刷新</asp:LinkButton>
                </td>
            </tr>
        </table>
        <radA:RadAjaxServiceManager ID="RadAjaxServiceManager1" runat="server">
            <WebServices>
                <radA:WebServiceReference Path="~/WebSevice/RmsPMWS.asmx" />
            </WebServices>
        </radA:RadAjaxServiceManager>
        <radW:RadWindowManager ID="RadWindowManager2" runat="server" SingleNonMinimizedWindow="true"
            ClientCallBackFunction="CallBackFunction" OnClientShow="OnClientShow" OnClientClose="OnClientClose">
            <Windows>
                <radW:RadWindow ID="dialogwindow" runat="server" Left="" Width="600" Height="500"
                    SkinsPath="~/RadControls/Window/Skins"
                    Title="" Top="" Modal="true" />
            </Windows>
        </radW:RadWindowManager>
    </form>
</body>

<script language="javascript">
function GetViseID()
   {
    var contractcodeinput=document.getElementById("FormView1_ViseContractCodeTextBox_txtCode");
    if(contractcodeinput!=null)
    {
        SetViseID('<#=Request["Project_code"]#>',contractcodeinput.value);
        return false;
    }
   }
   function SetViseID(projectcode,contractcode)
    {
        RmsPM.Web.RmsPMWS.AutoRunViseID(projectcode,contractcode,onGetViseID);
        return false;
    }
     function onGetViseID(result)
    {
        var idinput=document.getElementById("FormView1_ViseIdTextBox");
        if(idinput!=null){
            idinput.value=result;
        }
    }
    function GetSupplier()//not use
    {
        var contractcodeinput=document.getElementById("FormView1_ViseContractCodeTextBox_txtCode");
        if(contractcodeinput!=null)
         {
        SetSupplier(contractcodeinput.value);
        return false;
        }
    }
    function SetSupplier(contractcode)
    {
        RmsPMWS.GetSupplierByContract(contractcode,ServiceCompleteCallback, ServiceErrorCallback);
        return false;
    }
     function ServiceCompleteCallback(ResponseObject, ResponseAsXml, ResponseAsText)
    {
        DoSelectSupplierReturn ( ResponseObject.Code,ResponseObject.Name )       
    }
    function ServiceErrorCallback(args){}
    function AddDtl(Code)
    {
        OpenCustomWindow('LocaleViseDtl.aspx?ViseCostCode='+Code+'&ViseCode=<%= (FormView1.DataKey.Value != null)?FormView1.DataKey.Value.ToString():"" %>&ProjectCode=<%= _projectCode+"" %>','签证费用', 500, 230);
    }
    function OpenAudit()
    {
        OpenCustomWindow('LocaleViseAudit.aspx?ViseCode=<%= (FormView1.DataKey.Value != null)?FormView1.DataKey.Value.ToString():"" %>','签证审核', 600, 400);
    }
    function WinReload()
    {
        __doPostBack('LinkButton1','');
    }
    function OpenPrint()
    {
        var ApplicationCode = '<%= (FormView1.DataKey.Value != null)?FormView1.DataKey.Value.ToString():"" %>';
        var CaseCode = '<%= RmsPM.BLL.WorkFlowRule.GetCaseCodeByProcedureNameAndApplicationCode("签证审核",(FormView1.DataKey.Value != null)?FormView1.DataKey.Value.ToString():"")%>';
		var PrintUrl = '<%= RmsPM.BLL.WorkFlowRule.GetProcedureSourceURLByName("签证审核")%>?frameType=List&ApplicationCode=' + ApplicationCode + '&CaseCode=' + CaseCode;
		OpenFullWindow( PrintUrl ,'签证打印预览');
    }
    function OpenRequisition()
    {
		OpenFullWindow('<%= RmsPM.BLL.WorkFlowRule.GetProcedureURLByName("签证审核")%>?ViseCode=<%= FormView1.DataKey.Value %>&ProjectCode=<%= _projectCode + ""%>','签证审核');
    }
    function doViewContractInfo( code )
	{
		OpenFullWindow('../Contract/ContractInfo.aspx?ProjectCode=<%=_projectCode%>&ContractCode=' + code,'合同信息');
	}
	function doViewSupplierInfo(code)
    {
        OpenFullWindow('../Supplier/SupplierInfo.aspx?SupplierCode=' + code   ,"供应商信息");
    }
    
    function OpenDesignChange(code)
    {
       OpenFullWindow('../DesignChange/DesignChangeDetails.aspx?ProjectCode=<%= Request["ProjectCode"]+"" %>&Type=1&DesignChangeCode='+code,'设计变更修改');
    }
      
    function EventClickTab(tabindex)
    {
        var objTable = document.all("tabdiv");
        var TabTr = document.all("webtabs");
        for(var i=0;i<objTable.rows.length;i++)
        {
            var objTableTr = objTable.rows[i];
            if(i==tabindex)
            {
                objTableTr.style.display = "block";
                TabTr.cells[i+1].className = "TabShow";
            }
            else
            {
                objTableTr.style.display = "none";
                TabTr.cells[i+1].className = "TabDisplay";
            }
     }        
}
/*--------------------------------*/
if(typeof(InputContract_DoOthers)!="undefined"){
    InputContract_DoOthers.prototype.DoSomething=function()
    {
       SetSupplier(this.contractCode);
    }
}     
        function openwin()
        {                     
            var oWin=radopen("<%=Request.ApplicationPath%>/selectbox/selectviseid.aspx?projectCode=<%=_projectCode %>&type=<%=HttpUtility.UrlEncode("签证") %>","dialogwindow");
        } 
        function OnClientShow(radWindow)
        {    
            var oName = document.getElementById("FormView1_ViseIdTextBox");
            var viseid;
            if(oName!=null){viseid=oName.value;}else{viseid="";}
            var viseidstring=viseid.split("-");
            var arg = new Object(); 
            arg.projectCode='<%=_projectCode%>'
            if(viseidstring.length>4)
            {
                arg.p1=viseidstring[0];
                arg.p2=viseidstring[1];
                arg.p3=viseidstring[2];
                var re1=/.*(?=\()/;
                if(viseidstring[3].match(re1)){               
                  arg.p4=viseidstring[3].match(re1).toString();
                }else{arg.p4="";}
                var re2=/\(.*\)/;
                if(viseidstring[3].match(re2)){ 
                    var viseidstring3=viseidstring[3].match(re2).toString();                    
                    arg.p5=viseidstring3.substring(1,viseidstring3.length-1);                    
                }else{arg.p5="";}                
                arg.p6=viseidstring[4];               
            }else
            {
                arg.p1="";arg.p2="";arg.p3="";arg.p4="";arg.p5="";
            }
            radWindow.Argument = arg;            
            
            if(radWindow.IsLoaded)
            {
                document.frames(radWindow.Iframe.id).ConfigureDialog();
            }
            else{
                radWindow.IsLoaded=true;
            }
        }
                                                                                
        function CallBackFunction(radWindow, returnValue)
        {
            var visetext = document.getElementById("FormView1_ViseIdTextBox");
            var viseid=returnValue.p1+"-"+returnValue.p2+"-"+returnValue.p3;
            if(returnValue.p4){viseid=viseid+"-"+returnValue.p4;}
            viseid=viseid+"(";
            if(returnValue.p5){viseid=viseid+returnValue.p5;}
            viseid=viseid+")";
            viseid=viseid+"-####";
            visetext.value=viseid;
        }
        function OnClientClose(radWindow)
        {                    
            
        }                                                                          
</script>

</html>
