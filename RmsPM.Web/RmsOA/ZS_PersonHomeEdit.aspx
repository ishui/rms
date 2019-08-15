<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZS_PersonHomeEdit.aspx.cs"
    Inherits="PersonalManage_ZS_PersonHomeEdit" %>

<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>家庭情况</title>
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
                                家庭情况</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%"
                        OnItemInserted="FormView1_ItemInserted" OnItemDeleted="FormView1_ItemDeleted"
                        OnItemUpdated="FormView1_ItemUpdated" OnItemInserting="FormView1_ItemInserting"
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
                                        称谓：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("appname") %>' CssClass="input"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox7"
                                            ErrorMessage="*"></asp:RequiredFieldValidator></td>

                                    <td class="form-item" style="width: 20%">
                                        姓名：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("cname") %>' CssClass="input"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox8"
                                            ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        身份证号：</td>
                                    <td>
                                        <asp:TextBox ID="DEGREETextBox" runat="server" Text='<%# Bind("idcard") %>' CssClass="input"></asp:TextBox></td>

                                    <td class="form-item" style="width: 20%">
                                        政治面貌：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("polity") %>' CssClass="input"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        工作单位：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("workplace") %>' CssClass="input"></asp:TextBox></td>

                                    <td class="form-item" style="width: 20%">
                                        职务：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("duty") %>' CssClass="input"></asp:TextBox></td>
                                </tr>
                                
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        电话：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("phone") %>' CssClass="input"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox6"
                                            ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                    <td class="form-item" style="width: 20%">
                                        学历：</td>
                                    <td>
                                        <asp:TextBox ID="TextBoxeducational" runat="server" Text='<%# Bind("educational") %>' CssClass="input"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        是否供养：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("yesno") %>' CssClass="input"></asp:TextBox></td>

                                    <td class="form-item" style="width: 20%">
                                        月收入：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("monthearn") %>' CssClass="input"></asp:TextBox></td>
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
                                        称谓：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("appname") %>' CssClass="input"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox7"
                                            ErrorMessage="*"></asp:RequiredFieldValidator></td>

                                    <td class="form-item" style="width: 20%">
                                        姓名：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("cname") %>' CssClass="input"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox8"
                                            ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        身份证号：</td>
                                    <td>
                                        <asp:TextBox ID="DEGREETextBox" runat="server" Text='<%# Bind("idcard") %>' CssClass="input"></asp:TextBox></td>

                                    <td class="form-item" style="width: 20%">
                                        政治面貌：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("polity") %>' CssClass="input"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        工作单位：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("workplace") %>' CssClass="input"></asp:TextBox></td>

                                    <td class="form-item" style="width: 20%">
                                        职务：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("duty") %>' CssClass="input"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        电话：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("phone") %>' CssClass="input"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBox6"
                                            ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                     <td class="form-item" style="width: 20%">
                                        学历：</td>
                                    <td>
                                        <asp:TextBox ID="TextBoxeducational" runat="server" Text='<%# Bind("educational") %>' CssClass="input"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        是否供养：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("yesno") %>' CssClass="input"></asp:TextBox></td>

                                    <td class="form-item" style="width: 20%">
                                        月收入：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("monthearn") %>' CssClass="input"></asp:TextBox></td>
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
                                        称谓：</td>
                                    <td>
                                        <asp:Label ID="BEGIN_DATELabel" runat="server" Text='<%# Bind("appname") %>'></asp:Label></td>
                        
                                    <td class="form-item" style="width: 20%">
                                        姓名：</td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("cname") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        身份证号：</td>
                                    <td>
                                        <asp:Label ID="SCHOOL_NAMELabel" runat="server" Text='<%# Bind("idcard") %>'></asp:Label></td>

                                    <td class="form-item" style="width: 20%">
                                        政治面貌：</td>
                                    <td>
                                        <asp:Label ID="DEGREELabel" runat="server" Text='<%# Bind("polity") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        工作单位：</td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("workplace") %>'></asp:Label></td>

                                    <td class="form-item" style="width: 20%">
                                        职务：</td>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("duty") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        电话：</td>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("phone") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        学历：</td>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("educational") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        是否供养：</td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("yesno") %>'></asp:Label></td>

                                    <td class="form-item" style="width: 20%">
                                        月收入：</td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("monthearn") %>'></asp:Label></td>
                                </tr>
                                
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsOA.MODEL.OAPersonHomeModel"
                        DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetOAPersonHomeListOne"
                        TypeName="RmsOA.BFL.OAPersonHomeBFL" UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted">
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
