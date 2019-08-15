<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaterialInfo.aspx.cs" Inherits="Material_MaterialInfo" %>

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
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>����ά��</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>
	<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
    <link href="/../Images/index.css" rel="stylesheet" type="text/css" />
    <link href="../Images/GridPaginationSource/PaginationControlStyle.css" rel="stylesheet"
        type="text/css" />
</head>
<body scroll="no">
    <form id="form1" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0"  width="100%" border="0">
            <tr>
                <td height="25">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                                ����ά��</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" OnItemInserted="FormView1_ItemInserted"
                        OnItemDeleted="FormView1_ItemDeleted" OnItemUpdated="FormView1_ItemUpdated" DataKeyNames="MaterialCode"
                        Width="100%" OnItemInserting="FormView1_ItemInserting" OnItemCommand="FormView1_ItemCommand"
                        OnDataBound="FormView1_DataBound" OnItemUpdating="FormView1_ItemUpdating">
                        <EditItemTemplate>
                            <table class="table" id="tableToolBar" width="100%">
                                <tr>
                                    <td class="tools-area" width="16">
                                        <img src="../images/btn_li.gif" align="absMiddle"></td>
                                    <td class="tools-area">
                                        <asp:Button ID="btnSave" Text=" ���� " CausesValidation="true" CssClass="button" runat="server"
                                            CommandName="Update" />
                                        <asp:Button ID="btnCancel" Text=" ȡ�� " CssClass="button" runat="server" CommandName="Cancel" />
                                        <input name="btnClose" id="btnClose" type="button" value=" �ر� " class="button" runat="server"
                                            onclick="javascript:window.close();">
                                    </td>
                                </tr>
                            </table>
                            <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="form-item" nowrap width="80">
                                        �������ƣ�</td>
                                    <td>
                                        <asp:TextBox ID="MaterialNameTextBox" runat="server" Text='<%# Bind("MaterialName") %>' CssClass="input" Width="200"></asp:TextBox>&nbsp;<font
                                            color="red">*</font>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="MaterialNameTextBox"
                                            ErrorMessage="RequiredFieldValidator">����</asp:RequiredFieldValidator></td>

                                    <td class="form-item" nowrap>
                                        ���</td>
                                    <td>
                                    <asp:TextBox ID="Spec" runat="server" width= "100" Text='<%# Bind("Spec") %>' CssClass="input"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        ��λ��</td>
                                    <td >
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Unit") %>' CssClass="input"></asp:TextBox>
                                    </td>
                                    <td class="form-item">
                                        �ο��ۣ�</td>
                                    <TD><igtxt:webnumericedit id="txtStandardPrice" runat="server" CssClass="infra-input-nember" Width="100px"
											JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none" ValueDecimal='<%# Bind("StandardPrice") %>'></igtxt:webnumericedit>Ԫ
									</TD>
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap>
                                        �������ͣ�</td>
                                    <td colspan = "3">
                                        <uc1:InputSystemGroup id="inputSystemGroup" ClassCode="1501" Value='<%# Bind("groupcode") %>'  runat="server" SelectAllLeaf="True"></uc1:InputSystemGroup>
                                        <font color="red">*</font><span runat="server" id="GroupSpan"></span>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="form-item">
                                        ��ע��</td>
                                    <td colspan = "3">
                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Remark") %>' CssClass="input" TextMode="MultiLine" Width="100%" Height="120px" ></asp:TextBox>
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
                                        <asp:Button ID="btnSave" Text=" ���� " CausesValidation="true" CssClass="button" runat="server"
                                            CommandName="Insert" />
                                        <input name="btnClose" id="btnClose" type="button" value=" �ر� " class="button" runat="server"
                                            onclick="javascript:window.close();">
                                    </td>
                                </tr>
                            </table>
                            <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="form-item" width="80" nowrap>
                                        �������ƣ�</td>
                                    <td nowrap>
                                        <asp:TextBox ID="MaterialNameTextBox" runat="server" Text='<%# Bind("MaterialName") %>' width= "200" CssClass="input"></asp:TextBox>&nbsp;<font
                                            color="red">*</font>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="MaterialNameTextBox"
                                            ErrorMessage="RequiredFieldValidator">����</asp:RequiredFieldValidator>
                                    </td>

                                    <td class="form-item" nowrap>
                                        ���</td>
                                    <td nowrap width="80">
                                        <asp:TextBox ID="TextBox4" runat="server" width= "100"  Text='<%# Bind("Spec") %>' CssClass="input"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        ��λ��</td>
                                    <td >
                                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("Unit") %>' CssClass="input"></asp:TextBox><span runat="server" id="SystemGroupSpan"></span>
                                    </td>
                                    <td class="form-item">
                                        �ο��ۣ�</td>
                                    <TD><igtxt:webnumericedit id="txtStandardPrice" runat="server" CssClass="infra-input-nember" Width="100px"
											JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none" ValueDecimal='<%# Bind("StandardPrice") %>'></igtxt:webnumericedit>Ԫ
									</TD>
                                    
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap>
                                        �������ͣ�</td>
                                    <td colspan = "3">
                                        <uc1:InputSystemGroup id="inputSystemGroup" ClassCode="1501" Value='<%# Bind("groupcode") %>'  runat="server" SelectAllLeaf="True"></uc1:InputSystemGroup>
                                        <font color="red">*</font><span runat="server" id="GroupSpan"></span>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="form-item">
                                        ��ע��</td>
                                    <td colspan = "3">
                                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("Remark") %>' CssClass="input" TextMode="MultiLine" Width="100%" Height="120px" ></asp:TextBox>
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
                                        <asp:Button ID="btnModify" Text=" �޸� " CssClass="button" runat="server" CommandName="Edit" />
                                        <asp:Button ID="btnDelete" Text=" ɾ�� " CssClass="button" runat="server" CommandName="Delete" />
                                        <input name="btnClose" id="btnClose" type="button" value=" �ر� " class="button" runat="server"
                                            onclick="javascript:window.close();">
                                    </td>
                                </tr>
                            </table>
                            <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="form-item" width="80px" nowrap>
                                        �������ƣ�</td>
                                    <td witdh="16%">
                                        <%# Eval("MaterialName") %>
                                    </td>
                                    <td class="form-item" width="80px" nowrap>
                                        ���</td>
                                    <td width="30%"">
                                       <%# Eval("Spec") %>
                                    </td>
                                 </tr>   
                                <tr>
                                    <td class="form-item">
                                        ��λ��</td>
                                    <td>
                                        <%# (Eval("Unit"))%>
                                    </td>
                                    <td class="form-item">
                                        �ο��ۣ�</td>
                                    <td>
                                         <%# RmsPM.BLL.MathRule.GetDecimalShowString(Eval("StandardPrice"))%>Ԫ
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td class="form-item" width="10%" nowrap>
                                        �������ͣ�</td>
                                    <td colspan="3">
                                         <%# RmsPM.BLL.SystemGroupRule.GetSystemGroupFullName((string)Eval("groupcode"))%>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="form-item">
                                        ��ע��</td>
                                    <td colspan="3">
                                         <%# RmsPM.BLL.ConvertRule.ToString(Eval("Remark")).Replace("\n", "<br>") %>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetMaterial" 
                    TypeName="RmsPM.BFL.MaterialBFL" DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted" DataObjectTypeName="TiannuoPM.MODEL.MaterialModel">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="Code" QueryStringField="MaterialCode" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Style="display: none">ˢ��</asp:LinkButton>
                </td>
            </tr>
        </table>
    </form>
</body>

</html>
