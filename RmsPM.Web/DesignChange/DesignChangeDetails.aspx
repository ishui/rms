<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DesignChangeDetails.aspx.cs"
    Inherits="DesignChange_DesignChangeDetails" %>

<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>

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
<%@ Register TagPrefix="uc1" TagName="InputDictItem" Src="../UserControls/InputDictItem.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputInDesignChange" Src="../UserControls/InputInDesignChange.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>设计变更</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>

    <link href="../Images/GridPaginationSource/PaginationControlStyle.css" rel="stylesheet"
        type="text/css" />
 
    

</head>
<body>
    <form id="form1" runat="server">
        
        <radW:RadWindowManager ID="RadWindowManager2" runat="server"
                    ClientCallBackFunction="CallBackFunction"
                    OnClientShow = "OnClientShow"
                    OnClientClose = "OnClientClose"
        >
            <Windows>
                <radW:RadWindow ID="dialogwindow" runat="server" 
                    Left=""  Width=650 Height=550 
                    SkinsPath="~/RadControls/Window/Skins"
                     Title="" 
                     Top="" 
                     Modal = "true"
                 />
            </Windows>
        </radW:RadWindowManager>
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td height="25">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                                设计变更</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" OnItemInserted="FormView1_ItemInserted"
                        OnItemDeleted="FormView1_ItemDeleted" OnItemUpdated="FormView1_ItemUpdated" DataKeyNames="Code"
                        Width="100%" OnItemInserting="FormView1_ItemInserting" 
                        OnDataBound="FormView1_DataBound" OnItemUpdating="FormView1_ItemUpdating" OnItemCreated="FormView1_ItemCreated"
                         >
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
                                        项目名称：</td>
                                    <td>
                                        <%# RmsPM.BLL.ProjectRule.GetProjectName((string)Eval("ProjectName")) %>
                                    </td>
                                    <td class="form-item">
                                        工程名称：</td>
                                    <td>
                                        <asp:TextBox ID="txtSolutionName" runat="server" Text='<%# Bind("SolutionName") %>'
                                            CssClass="input"></asp:TextBox>&nbsp;<font color="red">*</font>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSolutionName"
                                            ErrorMessage="RequiredFieldValidator">必填</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="form-item">
                                        设计变更编号：</td>
                                    <td>
                                        <asp:TextBox ID="txtNumber" runat="server" Text='<%# Bind("Number") %>' CssClass="input"></asp:TextBox>&nbsp;<font
                                            color="red">*</font>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNumber"
                                            ErrorMessage="RequiredFieldValidator">必填</asp:RequiredFieldValidator>                                            
                                        <a href=# id="openViseIdSelectBox" onclick="openwin();" style='display:<%if (this.up_sPMNameLower != "yefengpm" ){%>"none"<%}else{%>""<%}%>'>选择</a>
                                        
                                          
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        经办人：</td>
                                    <td>
                                        <uc1:InputUser ID="txtPerson" runat="server" Value='<%# Bind("Person") %>'></uc1:InputUser>
                                        &nbsp;<font color="red">*</font> <span runat="server" id="PersonMsgSpan"></span>
                                    </td>
                                    <td class="form-item">
                                        经办部门：</td>
                                    <td>
                                        <uc2:InputUnit ID="txtUnit" runat="server" Value='<%# Bind("Unit") %>'></uc2:InputUnit>
                                        &nbsp;<font color="red">*</font> <span runat="server" id="UnitMsgSpan"></span>
                                    </td>
                                    <td class="form-item">
                                        承包商：</td>
                                    <td>
                                        <uc1:InputSupplier ID="txtSupplierTextBox" runat="server" Value='<%# Bind("Supplier") %>'>
                                        </uc1:InputSupplier>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        合同选择：</td>
                                    <td colspan="3">
                                        <uc3:inputcontract ID="txtContract" runat="server" ImagePath="../Images/" Value='<%# Bind("Contract") %>'
                                            ProjectCode='<%# Request["ProjectCode"]+"" %>' />
                                    </td>
                                    <td class="form-item">
                                        变更日期：</td>
                                    <td>
                                        <cc3:Calendar ID="Calendar1" runat="server" CalendarResource="../Images/CalendarResource/"
                                            ReadOnly="False" Display="True" Value='<%# Bind("Date") %>' CalendarMode="Date">
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                                <tr  id="NouseinyfIndesign"  style="display:<%if(this.up_sPMNameLower == "yefengpm"&&Request.QueryString["Type"].ToString() == "1"){%>none<%}else{%>''<%} %>">
                                    <td class="form-item">
                                        设计院单位：</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="input" Text='<%# Bind("Designer") %>'
                                            Width="279px"></asp:TextBox></td>
                                    <td class="form-item">
                                        设计院联系单编号：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox3" runat="server" CssClass="input" Text='<%# Bind("RelationNumber") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        收文人：</td>
                                    <td>
                                        &nbsp;<asp:TextBox ID="TextBox4" runat="server" CssClass="input" Text='<%# Bind("TerminatingPerson") %>'></asp:TextBox></td>
                                    <td class="form-item">
                                        收文日期：</td>
                                    <td>
                                        <cc3:Calendar ID="Calendar2" runat="server" CalendarResource="../Images/CalendarResource/"
                                            ReadOnly="False" Display="True" Value='<%# Bind("TerminatingDate") %>' CalendarMode="Date">
                                        </cc3:Calendar>
                                    </td>
                                    <td class="form-item">
                                        专业：</td>
                                    <td>
                                        &nbsp;<uc1:InputDictItem ID="TextBox5" runat="server" DictName="设计专业" Text='<%# Bind("Specialty") %>'>
                                        </uc1:InputDictItem>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" width="15%">
                                        变更类型：</td>
                                    <td>
                                        <uc1:InputSystemGroup Value='<%# Bind("ChangeType") %>' ID="ChangeType" runat="server">
                                        </uc1:InputSystemGroup>
                                        <font color="red">*</font><span runat="server" id="Span1"></span>
                                    </td>
                                    <td class="form-item" width="15%" id="CaseReasonTitle" runat="server">
                                        事件缘由：</td>
                                    <td colspan="3" id="CaseReason" runat="server">
                                        <uc1:InputInDesignChange Value='<%# Bind("ReferCode") %>' ID="InputInDesignChange"
                                            runat="server"></uc1:InputInDesignChange>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" width="15%"><%if ("yefengpm" == this.up_sPMNameLower)
                                                                        { %>
                                     <font color="red">*</font>  <%} %> 变更事由：</td>
                                    <td colspan="5">
                                        <asp:TextBox ID="ViseReasonTextBox" runat="server" Text='<%# Bind("Reason") %>' Height="80px"
                                            TextMode="MultiLine" Width="100%"></asp:TextBox> 
                                           
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ViseReasonTextBox" Enabled="false"
                                            ErrorMessage="RequiredFieldValidator">必填</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" width="15%">
                                        附&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;件：</td>
                                    <td colspan="5">
                                        <uc1:AttachMentAdd ID="AttachMentAdd3" runat="server" MasterCode='<%# Eval("Code") %>'
                                            AttachMentType="DesignChange"></uc1:AttachMentAdd>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" width="15%">
                                        备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：</td>
                                    <td colspan="5">
                                        <asp:TextBox ID="ViseRemarkTextBox" runat="server" Text='<%# Bind("Remark") %>' Height="80px"
                                            TextMode="MultiLine" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
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
                                        项目名称：</td>
                                    <td>
                                        <%# RmsPM.BLL.ProjectRule.GetProjectName((string)Request["ProjectCode"]) %>
                                    </td>
                                    <td class="form-item">
                                        工程名称：</td>
                                    <td>
                                        <asp:TextBox ID="txtSolutionName" runat="server" Text='<%# Bind("SolutionName") %>'
                                            CssClass="input"></asp:TextBox>&nbsp;<font color="red">*</font>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSolutionName"
                                            ErrorMessage="RequiredFieldValidator">必填</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="form-item">
                                        设计变更编号：</td>
                                    <td>
                                        <asp:TextBox ID="txtNumber" runat="server" Text='<%# Bind("Number") %>' CssClass="input"></asp:TextBox>&nbsp;<font
                                            color="red">*</font>
                                        <a href=# id="openViseIdSelectBox" onclick="openwin();" style='display:<%if (this.up_sPMNameLower != "yefengpm" ){%>"none"<%}else{%>""<%}%>'>选择</a>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNumber"
                                            ErrorMessage="RequiredFieldValidator">必填</asp:RequiredFieldValidator>
                                           
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        经办人：</td>
                                    <td>
                                        <uc1:InputUser ID="txtPerson" runat="server" Value='<%# Bind("Person") %>'></uc1:InputUser>
                                        &nbsp;<font color="red">*</font> <span runat="server" id="PersonMsgSpan"></span>
                                    </td>
                                    <td class="form-item">
                                        经办部门：</td>
                                    <td>
                                        <uc2:InputUnit ID="txtUnit" runat="server" Value='<%# Bind("Unit") %>'></uc2:InputUnit>
                                        &nbsp;<font color="red">*</font> <span runat="server" id="UnitMsgSpan"></span>
                                    </td>
                                    <td class="form-item">
                                        承包商：</td>
                                    <td>
                                        <uc1:InputSupplier ID="txtSupplierTextBox" runat="server" Value='<%# Bind("Supplier") %>'>
                                        </uc1:InputSupplier>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        合同选择：</td>
                                    <td colspan="3">
                                        <uc3:inputcontract ID="txtContract" runat="server" ImagePath="../Images/" Value='<%# Bind("Contract") %>'
                                            ProjectCode='<%# Request["ProjectCode"]+"" %>' ClassCode="22" />
                                    </td>
                                    <td class="form-item">
                                        变更日期：</td>
                                    <td>
                                        <cc3:Calendar ID="Calendar1" runat="server" CalendarResource="../Images/CalendarResource/"
                                            ReadOnly="False" Display="True" Value='<%# Bind("Date") %>' CalendarMode="Date">
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                                <tr style="display:<%if(this.up_sPMNameLower == "yefengpm"&&Request.QueryString["Type"].ToString() == "1"){%>none<%}else{%>''<%} %>">
                                    <td class="form-item">
                                        设计院单位：</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="input" Text='<%# Bind("Designer") %>'
                                            Width="279px"></asp:TextBox></td>
                                    <td class="form-item">
                                        设计院联系单编号：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox3" runat="server" CssClass="input" Text='<%# Bind("RelationNumber") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        收文人：</td>
                                    <td>
                                        &nbsp;<asp:TextBox ID="TextBox4" runat="server" CssClass="input" Text='<%# Bind("TerminatingPerson") %>'></asp:TextBox></td>
                                    <td class="form-item">
                                        收文日期：</td>
                                    <td>
                                        <cc3:Calendar ID="Calendar2" runat="server" CalendarResource="../Images/CalendarResource/"
                                            ReadOnly="False" Display="True" Value='<%# Bind("TerminatingDate") %>' CalendarMode="Date">
                                        </cc3:Calendar>
                                    </td>
                                    <td class="form-item">
                                        专业：</td>
                                    <td>
                                        &nbsp;<uc1:InputDictItem ID="InputDictItem1" runat="server" DictName="设计专业" Text='<%# Bind("Specialty") %>'>
                                        </uc1:InputDictItem>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" width="15%">
                                        变更类型：</td>
                                    <td>
                                        <uc1:InputSystemGroup Value='<%# Bind("ChangeType") %>' ID="ChangeType" runat="server">
                                        </uc1:InputSystemGroup>
                                        <font color="red">*</font><span runat="server" id="Span1"></span>
                                    </td>
                                    <td class="form-item" width="15%" id="CaseReasonTitle" runat="server">
                                        事件缘由：</td>
                                    <td colspan="3"  id="CaseReason" runat="server">
                                        <uc1:InputInDesignChange Value='<%# Bind("ReferCode") %>' ID="InputInDesignChange"
                                            runat="server"></uc1:InputInDesignChange>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" width="15%"><%if ("yefengpm" == this.up_sPMNameLower)
                                                                        { %>
                                      <font color="red">*</font> <%} %> 变更事由：</td>
                                    <td colspan="5">
                                        <asp:TextBox ID="ViseReasonTextBox" runat="server" Text='<%# Bind("Reason") %>' Height="80px"
                                            TextMode="MultiLine" Width="100%"></asp:TextBox>
                                           
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ViseReasonTextBox" Enabled="false"
                                            ErrorMessage="RequiredFieldValidator">必填</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" width="15%">
                                        附&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;件：</td>
                                    <td colspan="5">
                                        <uc1:AttachMentAdd ID="AttachMentAdd3" runat="server" MasterCode='<%# Eval("Code") %>'
                                            AttachMentType="DesignChange"></uc1:AttachMentAdd>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" width="15%">
                                        备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：</td>
                                    <td colspan="5">
                                        <asp:TextBox ID="ViseRemarkTextBox" runat="server" Text='<%# Bind("Remark") %>' Height="80px"
                                            TextMode="MultiLine" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
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
                                            runat="server">
                                        <input name="btnAudit" id="btnAudit" type="button" value=" 审核 " class="button" runat="server"
                                            onclick="javascript:OpenAudit();return false;">
                                        <input name="btnClose" id="btnClose" type="button" value=" 关闭 " class="button" runat="server"
                                            onclick="javascript:window.close();">
                                    </td>
                                </tr>
                            </table>
                            <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="form-item">
                                        项目名称：</td>
                                    <td>
                                        <%# RmsPM.BLL.ProjectRule.GetProjectName((string)Eval("ProjectName")) %>
                                    </td>
                                    <td class="form-item">
                                        工程名称：</td>
                                    <td>
                                        <%# Eval("SolutionName") %>
                                    </td>
                                    <td class="form-item">
                                        设计变更编号：</td>
                                    <td>
                                        <%# Eval("Number") %>
                                        <a href=# id="openViseIdSelectBox" onclick="openwin();" style="display:none">选择</a>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        经办人：</td>
                                    <td>
                                        <%# RmsPM.BLL.SystemRule.GetUserName((string)Eval("Person")) %>
                                    </td>
                                    <td class="form-item">
                                        经办部门：</td>
                                    <td>
                                        <%# RmsPM.BLL.SystemRule.GetUnitFullName((string)Eval("Unit")) %>
                                    </td>
                                    <td class="form-item">
                                        承包商：</td>
                                    <td>
                                        <a href="#" onclick="javascript:doViewSupplierInfo('<%# Eval("Supplier") %>');return false;">
                                            <%# RmsPM.BLL.ProjectRule.GetSupplierName((string)Eval("Supplier")) %>
                                        </a>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        合同名称：</td>
                                    <td>
                                        <a href="#" onclick="javascript:doViewContractInfo('<%# Eval("Contract") %>');return false;">
                                            <%# RmsPM.BLL.ContractRule.GetContractName((string)Eval("Contract")) %>
                                        </a>
                                    </td>
                                    <td class="form-item">
                                        合同编号：</td>
                                    <td>
                                        <a href="#" onclick="javascript:doViewContractInfo('<%# Eval("Contract") %>');return false;">
                                            <%# RmsPM.BLL.ContractRule.GetContractID((string)Eval("Contract")) %>
                                        </a>
                                    </td>
                                    <td class="form-item">
                                        变更日期：</td>
                                    <td>
                                        <%#  ((DateTime)Eval("Date")).ToShortDateString()%>
                                    </td>
                                </tr>
                                <tr style="display:<%if(this.up_sPMNameLower == "yefengpm"&&Request.QueryString["Type"].ToString() == "1"){%>none<%}else{%>''<%} %>">
                                    <td class="form-item">
                                        设计院单位：</td>
                                    <td colspan="3">
                                        <%# Eval("Designer") %>
                                    </td>
                                    <td class="form-item">
                                        设计院联系单编号：</td>
                                    <td>
                                        <%# Eval("RelationNumber") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        收文人：</td>
                                    <td>
                                        <%# Eval("TerminatingPerson") %>
                                    </td>
                                    <td class="form-item">
                                        收文日期：</td>
                                    <td>
                                        <%# ((DateTime)Eval("TerminatingDate")).ToShortDateString()%>
                                    </td>
                                    <td class="form-item">
                                        专业：</td>
                                    <td>
                                        <%# Eval("Specialty") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" width="15%">
                                        变更类型：</td>
                                    <td>
                                        <asp:Label Text='<%# Bind("ChangeType") %>' ID="ChangeType" runat="server" />
                                    </td>
                                    <td class="form-item" width="15%" id="CaseReasonTitle" runat="server">
                                        事件缘由(编号)：</td>
                                    <td colspan="3" id="CaseReason" runat="server">
                                        <a href="#" onclick="javascript:OpenModify('<%# Eval("ReferCode") %>');">
                                            <%#RmsPM.BLL.ConvertRule.ToInt(Eval("ReferCode")) == 0 ? "" : Eval("ReferCode")%>
                                        </a>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" width="15%">
                                        变更事由：</td>
                                    <td colspan="5">
                                        <%# Eval("Reason").ToString().Replace("\n", "<br/>")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" width="15%">
                                        附&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;件：</td>
                                    <td colspan="5">
                                        <uc1:AttachMentList ID="AttachMentList3" runat="server" AttachMentType="DesignChange"
                                            MasterCode='<%# Eval("Code") %>'></uc1:AttachMentList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" width="15%">
                                        备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：</td>
                                    <td colspan="5">
                                        <%# Eval("Remark").ToString().Replace("\n", "<br/>")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" width="15%">
                                        变更费用：</td>
                                    <td colspan="5">
                                        <%# Eval("ChangeMoney").ToString().Replace("\n", "<br/>")%>
                                        <br />
                                        合计（估计结算金额）：<%# Eval("TotalMoney") %>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="TiannuoPM.MODEL.DesignChangeModel"
                        DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetDesignChange" TypeName="RmsPM.BFL.DesignChangeBFL"
                        UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted" OldValuesParameterFormatString="original_{0}" OnInserting="ObjectDataSource1_Inserting" OnUpdating="ObjectDataSource1_Updating">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="Code" QueryStringField="DesignChangeCode" Type="Int32" />
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
                    &nbsp;
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Style="display: none">刷新</asp:LinkButton>
                </td>
            </tr>
        </table>
         
    </form>
    <script language="javascript" type="text/javascript">
    function WinReload()
    {
        __doPostBack('LinkButton1','');
    }
    function doViewContractInfo( code )
	{
		OpenFullWindow('../Contract/ContractInfo.aspx?ProjectCode=<%=Request["ProjectCode"]%>&ContractCode=' + code,'合同信息');
	}
	function doViewSupplierInfo(code)
    {
        OpenFullWindow('../Supplier/SupplierInfo.aspx?SupplierCode=' + code   ,"供应商信息");
    }
    function OpenAudit()
    {
        OpenCustomWindow('DesignChangeAudit.aspx?projectCode=<%=projectCode%>&DesignChangeCode=<%= (FormView1.DataKey.Value != null)?FormView1.DataKey.Value.ToString():"" %>','设计变更审核', 400, 220);
    }
    function OpenRequisition()
    {
		OpenFullWindow('<%= RmsPM.BLL.WorkFlowRule.GetProcedureURLByName("设计变更")%>?DesignChangeCode=<%= FormView1.DataKey.Value %>&ProjectCode=<%= Request["ProjectCode"] + ""%>','设计变更');
    }
    function OpenRequisitionInternal()
    {
		OpenFullWindow('<%= RmsPM.BLL.WorkFlowRule.GetProcedureURLByName("设计变更[内部]")%>?DesignChangeCode=<%= FormView1.DataKey.Value %>&ProjectCode=<%= Request["ProjectCode"] + ""%>','设计变更内部');
    }
    
	function OpenModify(Code)
	{
	    // alert("<%= Request["ProjectCode"]+"" %>");
		OpenFullWindow('DesignChangeDetails.aspx?ProjectCode=<%= Request["ProjectCode"]+"" %>&Type=1&DesignChangeCode='+Code,'设计变更修改');
	}
    
    </script>
    <script type="text/javascript">   
        function openwin()
        {                     
            var oWin=radopen("<%=Request.ApplicationPath%>/selectbox/selectviseid.aspx?projectCode=<%=projectCode %>&type=<%=HttpUtility.UrlEncode("设计变更"+inoutType) %>","dialogwindow");
        }     
        function OnClientShow(radWindow)
        {    
            var oName = document.getElementById("FormView1_txtNumber");
            var viseid;
            if(oName!=null){viseid=oName.value;}else{viseid="";}
            var viseidstring=viseid.split("-");
            var arg = new Object();
            arg.type='<%=inoutType %>'
            arg.projectCode='<%=projectCode%>'
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
            var visetext = document.getElementById("FormView1_txtNumber");
            var viseid=returnValue.p1+"-"+returnValue.p2+"-"+returnValue.p3+"-"
            if(returnValue.p4){viseid=viseid+returnValue.p4;}
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
</body>
</html>
