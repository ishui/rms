<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StampDutyInfo.aspx.cs" Inherits="RmsPM.Web.Systems.Systems_StampDutyInfo" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>印花税</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript" src="../Rms.js"></script>

    <script language="javascript" type="text/javascript">
	
	function doModifyRole()
	{
		window.navigate('RoleModify.aspx?RoleCode=<%=Request["RoleCode"]%>','编辑角色');
	}
	
	function OpenStation( code )
	{
		OpenMiddleWindow( 'StationInfo.aspx?StationCode=' + code ,'岗位信息' );
	}
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <input id="txtRefreshScript" type="hidden" name="txtRefreshScript" runat="server" />

        <script type="text/javascript">
	function configRoleRight()
	{
		window.open( 'ConfigRoleRight.aspx?RoleCode=<%=Request["RoleCode"]%>' );
	}

        </script>

        <asp:FormView ID="Model" runat="server" DataSourceID="ModelData" DataKeyNames="StampDutyID"
            Width="100%" OnItemDeleted="Model_ItemDeleted" OnItemInserted="Model_ItemInserted"
            OnItemUpdated="Model_ItemUpdated">
            <EditItemTemplate>
                <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0" bgcolor="#ffffff">
                    <tr>
                        <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                            印花税</td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("StampDutyID")  %>' Visible="false" />
                            <table class="form" cellspacing="0" cellpadding="0" width="100%">
                                <tr>
                                    <td class="form-item" width="20%" style="height: 49px">
                                        税目：</td>
                                    <td width="20%" style="height: 49px">
                                        <asp:TextBox CssClass="input" ID="TaxItemsTextBox" runat="server" Text='<%# Bind("TaxItems") %>'>
                                        </asp:TextBox><font color="red">*<asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                            runat="server" ControlToValidate="TaxItemsTextBox" Display="None" ErrorMessage="请填写税目"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator></font></td>
                                    <td class="form-item" width="20%" style="height: 49px">
                                        税率：</td>
                                    <td width="20%" style="height: 49px">
                                        <asp:TextBox CssClass="input-nember" ID="TaxRateTextBox" runat="server" Style="behavior: url('../Images/RmsControls/javaScripts/Money.htc')"
                                            Text='<%# Bind("TaxRate") %>'>
                                        </asp:TextBox><font color="red">*<asp:RangeValidator ID="RangeValidator2" runat="server"
                                            ControlToValidate="TaxRateTextBox" ErrorMessage="税率填写不正确(税率必须大于0且小于1)" MaximumValue="1"
                                            MinimumValue="0" SetFocusOnError="True" Type="Double" Display="None"></asp:RangeValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="请填写税率"
                                                ControlToValidate="TaxRateTextBox" Display="None"></asp:RequiredFieldValidator></font></td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        纳 税 人：</td>
                                    <td>
                                        <asp:TextBox CssClass="input" ID="TaxPayerTextBox" runat="server" Text='<%# Bind("TaxPayer") %>'>
                                        </asp:TextBox></td>
                                    <td class="form-item">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        范围：</td>
                                    <td class="tdBlankp" colspan="3">
                                        <asp:TextBox CssClass="input" ID="RangeTextBox" Style="width: 424px;" runat="server"
                                            Text='<%# Bind("Range") %>'>
                                        </asp:TextBox><font color="red">*<asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                            runat="server" ControlToValidate="RangeTextBox" ErrorMessage="请填写范围" Display="None"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator></font></td>
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap>
                                        说明：</td>
                                    <td nowrap colspan="3" id="tdStationName" runat="server">
                                        <asp:TextBox Style="width: 424px;" CssClass="input" ID="RemarksTextBox" runat="server"
                                            Text='<%# Bind("Remarks") %>'>
                                        </asp:TextBox></td>
                                </tr>
                                <tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="trSave">
                        <td>
                            <table cellspacing="10" width="100%">
                                <tr>
                                    <td align="center">
                                        <asp:Button CssClass="submit" ID="btnSave" Text="确 定" name="btnSave" runat="server"
                                            CausesValidation="True" CommandName="Update" />&nbsp;
                                        <input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button"
                                            value="取 消" name="btnCancel">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </EditItemTemplate>
            <InsertItemTemplate>
                <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0" bgcolor="#ffffff">
                    <tr>
                        <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                            印花税</td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <table class="form" cellspacing="0" cellpadding="0" width="100%">
                                <tr>
                                    <td class="form-item" width="20%">
                                        税目：</td>
                                    <td width="20%">
                                        <asp:TextBox CssClass="input" ID="TaxItemsTextBox" runat="server" Text='<%# Bind("TaxItems") %>'>
                                        </asp:TextBox><font color="red">*<asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                            runat="server" ControlToValidate="TaxItemsTextBox" Display="None" ErrorMessage="请填写税目"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator></font></td>
                                    <td class="form-item" width="20%">
                                        税率：</td>
                                    <td width="20%">
                                        <asp:TextBox CssClass="input-nember" ID="TaxRateTextBox" runat="server" Style="behavior: url('../Images/RmsControls/javaScripts/Money.htc')"
                                            Text='<%# Bind("TaxRate") %>'>
                                        </asp:TextBox><font color="red">*<asp:RangeValidator ID="RangeValidator2" runat="server"
                                            ControlToValidate="TaxRateTextBox" ErrorMessage="税率填写不正确(税率必须大于0且小于1)" MaximumValue="1"
                                            MinimumValue="0" SetFocusOnError="True" Type="Double" Display="None"></asp:RangeValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TaxRateTextBox"
                                                Display="None" ErrorMessage="请填写税率"></asp:RequiredFieldValidator></font></td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        纳 税 人：</td>
                                    <td>
                                        <asp:TextBox CssClass="input" ID="TaxPayerTextBox" runat="server" Text='<%# Bind("TaxPayer") %>'>
                                        </asp:TextBox></td>
                                    <td class="form-item">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        范围：</td>
                                    <td class="tdBlankp" colspan="3">
                                        <asp:TextBox CssClass="input" ID="RangeTextBox" Style="width: 424px;" runat="server"
                                            Text='<%# Bind("Range") %>'>
                                        </asp:TextBox><font color="red">*<asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                            runat="server" ControlToValidate="RangeTextBox" ErrorMessage="请填写范围" Display="None"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator></font></td>
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap>
                                        说明：</td>
                                    <td nowrap colspan="3" id="tdStationName" runat="server">
                                        <asp:TextBox Style="width: 424px;" CssClass="input" ID="RemarksTextBox" runat="server"
                                            Text='<%# Bind("Remarks") %>'>
                                        </asp:TextBox></td>
                                </tr>
                                <tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="trSave">
                        <td>
                            <table cellspacing="10" width="100%">
                                <tr>
                                    <td align="center">
                                        <asp:Button CssClass="submit" CausesValidation="True" CommandName="Insert" ID="btnSave"
                                            type="button" Text="确 定" name="btnSave" runat="server" />&nbsp;
                                        <input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button"
                                            value="取 消" name="btnCancel">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </InsertItemTemplate>
            <ItemTemplate>
                <table height="100%" cellspacing="0" cellpadding="0" width="100%" bgcolor="white"
                    border="0">
                    <tr>
                        <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                            用户信息</td>
                    </tr>
                    <tr>
                        <td class="tools-area" valign="top">
                            <img src="../images/btn_li.gif" align="absMiddle">
                            <asp:Button CssClass="button" ID="btnModify" CausesValidation="False" CommandName="Edit"
                                Text="修 改" name="btnModify" runat="server" />
                            <asp:Button class="button" ID="btnDelete" OnClientClick="javascript:if(!window.confirm('确实要删除这条记录吗？')) return false;"
                                CausesValidation="False" CommandName="Delete" Text="删 除" name="btnDelete" runat="server" />
                            <input class="button" id="btnClose" onclick="window.close();" type="button" value="关 闭"
                                name="btnClose" runat="server" />
                            <asp:Button CssClass="button" ID="btnAdd" Text="继续新增" CommandName="New" runat="server" />
                        </td>
                    </tr>
                    <tr height="100%">
                        <td valign="top">
                            <asp:Label ID="StampDutyIDLabel" runat="server" Visible="false" Text='<%# Bind("StampDutyID") %>'>
                            </asp:Label>
                            <table class="form" cellspacing="0" cellpadding="0" width="100%">
                                <tr>
                                    <td class="form-item" width="20%">
                                        税目：</td>
                                    <td width="20%">
                                        <asp:Label ID="TaxItemsLabel" runat="server" Text='<%# Bind("TaxItems") %>'></asp:Label></td>
                                    <td class="form-item" width="20%">
                                        税率：</td>
                                    <td width="20%">
                                        <asp:Label ID="TaxRateLabel" runat="server" Text='<%# Bind("TaxRate") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        纳 税 人：</td>
                                    <td>
                                        <asp:Label ID="TaxPayerLabel" runat="server" Text='<%# Bind("TaxPayer") %>'></asp:Label></td>
                                    <td class="form-item">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        范围：</td>
                                    <td class="tdBlankp" colspan="3">
                                        <asp:Label ID="RangeLabel" runat="server" Text='<%# Bind("Range") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap>
                                        说明：</td>
                                    <td nowrap colspan="3" id="tdStationName" runat="server">
                                        <asp:Label ID="RemarksLabel" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label></td>
                                </tr>
                                <tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:FormView>
        <asp:ObjectDataSource ID="ModelData" runat="server" DataObjectTypeName="RmsPM.BLL.StampDuty"
            DeleteMethod="Delete" InsertMethod="Add" SelectMethod="GetModel" TypeName="RmsPM.BLL.StampDuty"
            UpdateMethod="Update" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:Parameter Name="StampDutyID" Type="Int32" />
            </SelectParameters>
            <InsertParameters>
                <asp:Parameter Name="TaxItems" Type="String" />
                <asp:Parameter Name="Range" Type="String" />
                <asp:Parameter Name="TaxRate" Type="Decimal" />
                <asp:Parameter Name="TaxPayer" Type="String" />
                <asp:Parameter Name="Remarks" Type="String" />
            </InsertParameters>
            <DeleteParameters>
                <asp:Parameter Name="StampDutyID" Type="Int32" />
            </DeleteParameters>
        </asp:ObjectDataSource>
        <input id="txtMainModuleCode" type="hidden" name="Hidden1" runat="server" />
        <input id="txtInputModuleCode" type="hidden" name="Hidden2" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" />
    </form>
</body>
</html>
