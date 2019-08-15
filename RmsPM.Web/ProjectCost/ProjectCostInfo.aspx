<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectCostInfo.aspx.cs" Inherits="ProjectCost_ProjectCostInfo" %>
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
    <title>项目造价维护</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet">
    <SCRIPT language="javascript" src="../images/convert.js"></SCRIPT>
    <script language="javascript" src="../Rms.js"></script>
	<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
    <link href="/../Images/index.css" rel="stylesheet" type="text/css" />
    <link href="../Images/GridPaginationSource/PaginationControlStyle.css" rel="stylesheet"
        type="text/css" />
        
    <script language="javascript">
function PriceValueChange(oEdit, oldValue, oEvent)
{
	var Price = 0;
	var Area = 0;
	var money = 0;
    Price = ConvertFloat(GetObjectInControl("FormView1", "txtPrice").value);
    Area = ConvertFloat(GetObjectInControl("FormView1", "Area").value);
   
    money = formatNumber(Price*Area, "#,###.00");
   
    igedit_getById("FormView1_Money").setValue(money);
}
function MoneyValueChange(oEdit, oldValue, oEvent)
{
	var Price = 0;
	var Area = 0;
	var money = 0;
    money = ConvertFloat(GetObjectInControl("FormView1", "Money").value);
    Area = ConvertFloat(GetObjectInControl("FormView1", "Area").value);
   
    Price = formatNumber(money/Area, "#,###.00");
   
    igedit_getById("FormView1_txtPrice").setValue(Price);
}
</script>
</head>
<body scroll="no">
    <form id="form1" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0"  width="100%" border="0">
            <tr>
                <td height="25">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                                项目造价维护</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" OnItemInserted="FormView1_ItemInserted"
                        OnItemDeleted="FormView1_ItemDeleted" OnItemUpdated="FormView1_ItemUpdated" DataKeyNames="ProjectCostCode"
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
                                    <td class="form-item" nowrap>
                                        项目名称：</td>
                                    <td nowrap>
                                        <asp:TextBox ID="ProjectNameTextBox" runat="server" Text='<%# Bind("ProjectName") %>' CssClass="input"></asp:TextBox>&nbsp;<font
                                            color="red">*</font>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ProjectNameTextBox"
                                            ErrorMessage="RequiredFieldValidator">必填</asp:RequiredFieldValidator>
                                    </td>

                                    <td class="form-item" nowrap>
                                        面 积：</td>
                                    <td nowrap>
                                        <igtxt:webnumericedit id="Area" runat="server" CssClass="infra-input-nember" Width="100px"
											JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none" ValueDecimal='<%# Bind("Area") %>'>
											<ClientSideEvents ValueChange="PriceValueChange"></ClientSideEvents>
											</igtxt:webnumericedit>平米
                                    </td>
                                </tr>
                              <tr>
                                    <td class="form-item" nowrap>
                                        费用项：</td>
                                    <td colspan="3">
                                        <uc1:InputSystemGroup id="inputSystemGroup" ClassCode="1521" Value='<%# Bind("groupcode") %>'  runat="server" SelectAllLeaf="True"></uc1:InputSystemGroup>
                                        <font color="red">*</font><span runat="server" id="GroupSpan"></span>
                                    </td>
                              </tr>
                                <tr>

                                    <td class="form-item">
                                        单方造价：</td>
                                    <TD><igtxt:webnumericedit id="txtPrice" runat="server" CssClass="infra-input-nember" Width="100px"
											JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none" ValueDecimal='<%# Bind("Price") %>'>
											<ClientSideEvents ValueChange="PriceValueChange"></ClientSideEvents>
											</igtxt:webnumericedit>元
									</TD>
                                    <td class="form-item">
                                        总 价：</td>
                                    <TD><igtxt:webnumericedit id="Money" runat="server" CssClass="infra-input-nember" Width="100px"
											JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none" ValueDecimal='<%# Bind("Money") %>'>
											<ClientSideEvents ValueChange="MoneyValueChange"></ClientSideEvents>
											</igtxt:webnumericedit>元
									</TD>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        数 量：</td>
                                    <td><igtxt:webnumericedit id="Qty" runat="server" CssClass="infra-input-nember" Width="100px"
											JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none" ValueDecimal='<%# Bind("Qty") %>'></igtxt:webnumericedit>
                                    </td>
                                    <td class="form-item">
                                        单 位：</td>
                                    <td >
                                        <asp:TextBox ID="Unit" runat="server" Text='<%# Bind("Unit") %>' CssClass="input"></asp:TextBox><span runat="server" id="SystemGroupSpan"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        说 明：</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="Remark" runat="server" Text='<%# Bind("Remark") %>' CssClass="input" TextMode="MultiLine" Width="100%" Height="120px" ></asp:TextBox>
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
                                    <td class="form-item" nowrap>
                                        项目名称：</td>
                                    <td nowrap>
                                        <asp:TextBox ID="ProjectNameTextBox" runat="server" Text='<%# Bind("ProjectName") %>' CssClass="input"></asp:TextBox>&nbsp;<font
                                            color="red">*</font>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ProjectNameTextBox"
                                            ErrorMessage="RequiredFieldValidator">必填</asp:RequiredFieldValidator>
                                    </td>

                                    <td class="form-item" nowrap>
                                        面 积：</td>
                                    <td nowrap>
                                        <igtxt:webnumericedit id="Area" runat="server" CssClass="infra-input-nember" Width="100px"
											JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none" ValueDecimal='<%# Bind("Area") %>'><ClientSideEvents ValueChange="PriceValueChange"></ClientSideEvents></igtxt:webnumericedit>平米
											
											
                                    </td>
                                </tr>
                              <tr>
                                    <td class="form-item" nowrap>
                                        费用项：</td>
                                    <td colspan="3">
                                        <uc1:InputSystemGroup id="inputSystemGroup" ClassCode="1521" Value='<%# Bind("groupcode") %>'  runat="server" SelectAllLeaf="True"></uc1:InputSystemGroup>
                                        <font color="red">*</font><span runat="server" id="GroupSpan"></span>
                                    </td>
                              </tr>
                                <tr>

                                    <td class="form-item">
                                        单方造价：</td>
                                    <TD><igtxt:webnumericedit id="txtPrice" runat="server" CssClass="infra-input-nember" Width="100px"
											JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none" ValueDecimal='<%# Bind("Price") %>'>
											<ClientSideEvents ValueChange="PriceValueChange"></ClientSideEvents>
											</igtxt:webnumericedit>元
									</TD>
                                    <td class="form-item">
                                        总 价：</td>
                                    <TD><igtxt:webnumericedit id="Money" runat="server" CssClass="infra-input-nember" Width="100px"
											JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none" ValueDecimal='<%# Bind("Money") %>'>
											<ClientSideEvents ValueChange="MoneyValueChange"></ClientSideEvents>
											</igtxt:webnumericedit>元
									</TD>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        数 量：</td>
                                    <td><igtxt:webnumericedit id="Qty" runat="server" CssClass="infra-input-nember" Width="100px"
											JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
											ImageDirectory="../images/infragistics/images/" MinDecimalPlaces="none" ValueDecimal='<%# Bind("Qty") %>'></igtxt:webnumericedit>
                                    </td>
                                    <td class="form-item">
                                        单 位：</td>
                                    <td >
                                        <asp:TextBox ID="Unit" runat="server" Text='<%# Bind("Unit") %>' CssClass="input"></asp:TextBox><span runat="server" id="SystemGroupSpan"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        说 明：</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="Remark" runat="server" Text='<%# Bind("Remark") %>' CssClass="input" TextMode="MultiLine" Width="100%" Height="120px" ></asp:TextBox>
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
                                        <input name="btnClose" id="btnClose" type="button" value=" 关闭 " class="button" runat="server"
                                            onclick="javascript:window.close();">
                                    </td>
                                </tr>
                            </table>
                            <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="form-item" width="20%" nowrap>
                                        项目名称：</td>
                                    <td width="30%">
                                        <%# Eval("ProjectName") %>
                                    </td>
                                    <td class="form-item" width="20%" nowrap>
                                        面 积：</td>
                                    <td width="30%">
                                       <%#  RmsPM.BLL.MathRule.GetDecimalShowString(Eval("Area"))%>平米
                                    </td>
                                 </tr>   
                                <tr>
                                    <td class="form-item" nowrap>
                                        费用项：</td>
                                    <td  colspan="3">
                                         <%# RmsPM.BLL.SystemGroupRule.GetSystemGroupFullName((string)Eval("groupcode"))%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        单方造价：</td>
                                    <td>
                                         <%#  RmsPM.BLL.MathRule.GetDecimalShowString(Eval("Price"))%>元
                                    </td>
                                    <td class="form-item">
                                        总 价：</td>
                                    <td>
                                     <%#  RmsPM.BLL.MathRule.GetDecimalShowString(Eval("Money"))%>元
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        数 量：</td>
                                    <td>
                                        <%# (Eval("Qty"))%>
                                    </td>
                                    <td class="form-item">
                                        单 位：</td>
                                    <td>
                                        <%# (Eval("Unit"))%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap>
                                        录入人：</td>
                                    <td>
                                     <%# RmsPM.BLL.SystemRule.GetUserName(RmsPM.BLL.ConvertRule.ToString(Eval("InputPerson")))%>
                                    </td>
                                    <td class="form-item">
                                        录入日期：</td>
                                    <td>
                                        <%# RmsPM.BLL.StringRule.ShowDate(Eval("InputDate"))%>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="form-item">
                                        说 明：</td>
                                    <td colspan="3">
                                         <%# RmsPM.BLL.ConvertRule.ToString(Eval("Remark")).Replace("\n", "<br>") %>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetProjectCost" 
                    TypeName="RmsPM.BFL.ProjectCostBFL" DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted" DataObjectTypeName="TiannuoPM.MODEL.ProjectCostModel" OldValuesParameterFormatString="original_{0}">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="Code" QueryStringField="ProjectCostCode" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Style="display: none">刷新</asp:LinkButton>
                </td>
            </tr>
        </table>
    </form>
</body>

</html>
