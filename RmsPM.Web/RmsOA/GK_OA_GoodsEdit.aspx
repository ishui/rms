<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GK_OA_GoodsEdit.aspx.cs"
    Inherits="RmsOA_GK_OA_GoodsEdit" %>

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
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>低耗品登记</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet" />

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
                                低耗品登记</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%"
                        OnItemInserted="FormView1_ItemInserted" OnItemDeleted="FormView1_ItemDeleted"
                        OnItemUpdated="FormView1_ItemUpdated" 
                        DataKeyNames="Code">
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
                                        低耗品名称：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="input" Text='<%# Bind("GoodsName") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        低耗品编号：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="input" Text='<%# Bind("GoodsCode") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        类别：</td>
                                    <td>
                                        <asp:TextBox ID="GoodsTypeTextBox" runat="server" CssClass="input" Text='<%# Bind("GoodsType") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        规格：</td>
                                    <td>
                                        <asp:TextBox ID="GoodsPartTextBox" runat="server" CssClass="input" Text='<%# Bind("GoodsPart") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        数量：</td>
                                    <td>
                                        <asp:TextBox ID="GoodsNumberTextBox" runat="server" CssClass="input" Text='<%# Bind("GoodsNumber") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        单价：</td>
                                    <td>
                                        <asp:TextBox ID="GoodsPriceTextBox" runat="server" CssClass="input" Text='<%# Bind("GoodsPrice") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        单位：</td>
                                    <td>
                                        <asp:TextBox ID="UnitCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("UnitCode") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        部门：</td>
                                    <td>
                                        <asp:TextBox ID="DepartmentCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("DepartmentCode") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        使用人：</td>
                                    <td>
                                        <asp:TextBox ID="UsePersonCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("UsePersonCode") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        入库日期：</td>
                                    <td>
                                        <cc3:Calendar ID="Calendar6" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("InputDate") %>'>
                                        </cc3:Calendar>
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
                                    <td class="form-item" style="width: 20%">
                                        低耗品名称：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="input" Text='<%# Bind("GoodsName") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        低耗品编号：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="input" Text='<%# Bind("GoodsCode") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        类别：</td>
                                    <td>
                                        <asp:TextBox ID="GoodsTypeTextBox" runat="server" CssClass="input" Text='<%# Bind("GoodsType") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        规格：</td>
                                    <td>
                                        <asp:TextBox ID="GoodsPartTextBox" runat="server" CssClass="input" Text='<%# Bind("GoodsPart") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        数量：</td>
                                    <td>
                                        <asp:TextBox ID="GoodsNumberTextBox" runat="server" CssClass="input" Text='<%# Bind("GoodsNumber") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        单价：</td>
                                    <td>
                                        <asp:TextBox ID="GoodsPriceTextBox" runat="server" CssClass="input" Text='<%# Bind("GoodsPrice") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        单位：</td>
                                    <td>
                                        <asp:TextBox ID="UnitCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("UnitCode") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        部门：</td>
                                    <td>
                                        <asp:TextBox ID="DepartmentCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("DepartmentCode") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        使用人：</td>
                                    <td>
                                        <asp:TextBox ID="UsePersonCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("UsePersonCode") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        入库日期：</td>
                                    <td>
                                        <cc3:Calendar ID="Calendar6" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("InputDate") %>'>
                                        </cc3:Calendar>
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
                            <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        低耗品名称：</td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("GoodsName") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        低耗品编号：</td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("GoodsCode") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        类别：</td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("GoodsType") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        规格：</td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("GoodsPart") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        数量：</td>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("GoodsNumber") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        单价：</td>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("GoodsPrice") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        单位：</td>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("UnitCode") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        部门：</td>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("DepartmentCode") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        使用人：</td>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("UsePersonCode") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        入库日期：</td>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("InputDate") %>'></asp:Label></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsOA.MODEL.GK_OA_GoodsModel"
                        DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetGK_OA_GoodsListOne"
                        TypeName="RmsOA.BFL.GK_OA_GoodsBFL" UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted">
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
