<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GK_OA_SubmitAccountDtlEdit.aspx.cs"
    Inherits="RmsOA_GK_OA_SubmitAccountDtlEdit" %>

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


<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="InputCostBudgetDtl" Src="../UserControls/InputCostBudgetDtl.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>报销明细</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet" />
    <link href="../Images/infra.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../Rms.js"></script>

    <script type="text/javascript" language="javascript">
        function SelectUnit()
		{
			OpenSmallWindow("../SelectBox/SelectUnit.aspx?UnitCode=000000");
		}
		function SelectUnitReturn(code, name)
		{
			window.document.all.FormView1_txtUnitName.value = name;
			window.document.all.FormView1_txtUnit.value = code;
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
                                报销明细</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%"
                        OnItemInserted="FormView1_ItemInserted" OnItemDeleted="FormView1_ItemDeleted"
                        OnItemUpdated="FormView1_ItemUpdated" DataKeyNames="Code" OnItemInserting="FormView1_ItemInserting">
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
                                    <td class="form-item" style="width: 20%">
                                        月份：</td>
                                    <td>
                                        <asp:TextBox ID="MonthTextBox" runat="server" CssClass="input" Text='<%# Bind("Month") %>'></asp:TextBox></td>
                                   <td class="form-item" style="width: 20%">
                                        标准费用(元)：</td>
                                    <td>
                                        <igtxt:webnumericedit id="WebNumericEdit1" runat="server" cssclass="infra-input-nember"
                                            imagedirectory="../images/infragistics/images/" javascriptfilename="../images/infragistics/20051/scripts/ig_edit.js"
                                            javascriptfilenamecommon="../images/infragistics/20051/scripts/ig_shared.js"
                                            mindecimalplaces="Two" width="100" valuetext='<%# Bind("StandardCost") %>'>
                                        </igtxt:webnumericedit>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        实际费用(元)：</td>
                                    <td>
                                        <igtxt:webnumericedit id="WebNumericEdit2" runat="server" cssclass="infra-input-nember"
                                            imagedirectory="../images/infragistics/images/" javascriptfilename="../images/infragistics/20051/scripts/ig_edit.js"
                                            javascriptfilenamecommon="../images/infragistics/20051/scripts/ig_shared.js"
                                            mindecimalplaces="Two" width="100" valuetext='<%# Bind("RealityCost") %>'>
                                        </igtxt:webnumericedit>
                                    </td>
                                    <td class="form-item" style="width: 20%">
                                        所剩余额(元)：</td>
                                    <td>
                                        <igtxt:webnumericedit id="WebNumericEdit3" runat="server" cssclass="infra-input-nember"
                                            imagedirectory="../images/infragistics/images/" javascriptfilename="../images/infragistics/20051/scripts/ig_edit.js"
                                            javascriptfilenamecommon="../images/infragistics/20051/scripts/ig_shared.js"
                                            mindecimalplaces="Two" width="100" valuetext='<%# Bind("RemainCost") %>'>
                                        </igtxt:webnumericedit>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        汇总余额(元)：</td>
                                    <td>
                                        <igtxt:webnumericedit id="WebNumericEdit4" runat="server" cssclass="infra-input-nember"
                                            imagedirectory="../images/infragistics/images/" javascriptfilename="../images/infragistics/20051/scripts/ig_edit.js"
                                            javascriptfilenamecommon="../images/infragistics/20051/scripts/ig_shared.js"
                                            mindecimalplaces="Two" width="100" valuetext='<%# Bind("SumCost") %>'>
                                        </igtxt:webnumericedit>
                                    </td>
                                    <td style="width: 20%">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        备注：</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="RemarkTextBox" runat="server" TextMode="MultiLine" Width="100%"
                                            Text='<%# Bind("Remark") %>'></asp:TextBox></td>
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
                                    <td class="form-item" style="width: 20%">
                                        月份：</td>
                                    <td>
                                        <asp:TextBox ID="MonthTextBox" runat="server" CssClass="input" Text='<%# Bind("Month") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        标准费用(元)：</td>
                                    <td>
                                        <igtxt:webnumericedit id="WebNumericEdit1" runat="server" cssclass="infra-input-nember"
                                            imagedirectory="../images/infragistics/images/" javascriptfilename="../images/infragistics/20051/scripts/ig_edit.js"
                                            javascriptfilenamecommon="../images/infragistics/20051/scripts/ig_shared.js"
                                            mindecimalplaces="Two" width="100" valuetext='<%# Bind("StandardCost") %>'>
                                        </igtxt:webnumericedit>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        实际费用(元)：</td>
                                    <td>
                                        <igtxt:webnumericedit id="WebNumericEdit2" runat="server" cssclass="infra-input-nember"
                                            imagedirectory="../images/infragistics/images/" javascriptfilename="../images/infragistics/20051/scripts/ig_edit.js"
                                            javascriptfilenamecommon="../images/infragistics/20051/scripts/ig_shared.js"
                                            mindecimalplaces="Two" width="100" valuetext='<%# Bind("RealityCost") %>'>
                                        </igtxt:webnumericedit>
                                    </td>
                                    <td class="form-item" style="width: 20%">
                                        所剩余额(元)：</td>
                                    <td>
                                        <igtxt:webnumericedit id="WebNumericEdit3" runat="server" cssclass="infra-input-nember"
                                            imagedirectory="../images/infragistics/images/" javascriptfilename="../images/infragistics/20051/scripts/ig_edit.js"
                                            javascriptfilenamecommon="../images/infragistics/20051/scripts/ig_shared.js"
                                            mindecimalplaces="Two" width="100" valuetext='<%# Bind("RemainCost") %>'>
                                        </igtxt:webnumericedit>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        汇总余额(元)：</td>
                                    <td>
                                        <igtxt:webnumericedit id="WebNumericEdit4" runat="server" cssclass="infra-input-nember"
                                            imagedirectory="../images/infragistics/images/" javascriptfilename="../images/infragistics/20051/scripts/ig_edit.js"
                                            javascriptfilenamecommon="../images/infragistics/20051/scripts/ig_shared.js"
                                            mindecimalplaces="Two" width="100" valuetext='<%# Bind("SumCost") %>'>
                                        </igtxt:webnumericedit>
                                    </td>
                                    <td style="width: 20%">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        备注：</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="RemarkTextBox" runat="server" TextMode="MultiLine" Width="100%"
                                            Text='<%# Bind("Remark") %>'></asp:TextBox></td>
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
                            <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        月份：</td>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("Month") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        标准费用(元)：</td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("StandardCost") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        实际费用(元)：</td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("RealityCost") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        所剩余额(元)：</td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("RemainCost") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        汇总余额(元)：</td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("SumCost") %>'></asp:Label></td>
                                    <td style="width: 20%">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        备注：</td>
                                    <td colspan="3">
                                        <asp:Label ID="RemarkLabel" runat="server" Text='<%# Bind("Remark") %>'></asp:Label></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsOA.MODEL.GK_OA_SubmitAccountDtlModel"
                        DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetGK_OA_SubmitAccountDtlListOne"
                        TypeName="RmsOA.BFL.GK_OA_SubmitAccountDtlBFL" UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
