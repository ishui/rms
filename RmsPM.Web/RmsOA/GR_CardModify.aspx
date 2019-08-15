<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GR_CardModify.aspx.cs" Inherits="RmsOA_GR_CardModify" %>

<%@ Register Assembly="AspWebControl" Namespace="AspWebControl" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Images/index.css" type="text/css" rel="stylesheet" />
    <title>名片夹</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td background="../images/topic_bg.gif" style="height: 25px; text-align: center;"
                        class="topic">
                        名片管理</td>
                </tr>
            </table>
        </div>
        <asp:ObjectDataSource ID="CardObjectDataSource" runat="server" SelectMethod="GetGK_OA_CardsFolderListOne" TypeName="RmsOA.BFL.GK_OA_CardsFolderBFL">
            <SelectParameters>
                <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:FormView ID="CardFormView" runat="server" DataSourceID="CardObjectDataSource" Width="100%" OnDataBound="CardFormView_DataBound">
            <EditItemTemplate>
               <table width="100%" border="0" cellpadding="0" cellspacing="0">
               <tr>
                   <td class="tools-area" style="width: 100%; height: 25px;" valign="top">
                       <img align="absMiddle" alt="" src="../images/btn_li.gif">
                   <asp:Button ID="UpdateButton" runat="server" CausesValidation="True"
                       Text="更新" CssClass="button" OnClick="UpdateButton_Click" />
                   <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                       Text="取消" CssClass="button" />
                   <input type="button" class="button" value="关闭" onclick="self.close()"/>
               </td></tr>
               </table>
                <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            姓名
                        </td>
                        <td>
                            <asp:TextBox ID="UserNameTextBox" runat="server" Text='<%# Bind("UserName") %>' CssClass="input" Font-Size="9pt"></asp:TextBox>
                            <span style="color:Red;">*</span>
                            <asp:RequiredFieldValidator ID="NameRequiredFieldValidator" runat="server" ControlToValidate="UserNameTextBox"
                                ErrorMessage="提示：姓名不能为空"></asp:RequiredFieldValidator></td>
                        <td class="form-item" style="width: 60px;">
                            性别</td>
                        <td>
                            <asp:DropDownList ID="SexDropDownList" runat="server" SelectedValue='<%# Bind("Sex") %>'>
                                <asp:ListItem> </asp:ListItem>
                                <asp:ListItem>男</asp:ListItem>
                                <asp:ListItem>女</asp:ListItem>
                            </asp:DropDownList></td>
                        <td class="form-item" style="width: 60px;">
                            年龄</td>
                        <td>
                            <asp:TextBox ID="AgeTextBox" runat="server" Text='<%# Bind("Age") %>' CssClass="input" Width="50%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            单位名称</td>
                        <td colspan="5">
                            <asp:TextBox ID="CompanyNameTextBox" runat="server" Text='<%# Bind("CompanyName") %>' CssClass="input" Width="65%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            单位地址
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="CompanyAddressTextBox" runat="server" Text='<%# Bind("CompanyAddress") %>' CssClass="input" Width="65%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            部门
                        </td>
                        <td>
                            <asp:TextBox ID="DeptTextBox" runat="server" Text='<%# Bind("Dept") %>' CssClass="input"></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 60px;">
                            职务
                        </td>
                        <td>
                            <asp:TextBox ID="HeadshipTextBox" runat="server" Text='<%# Bind("Headship") %>' CssClass="input"></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 60px;">
                            邮编
                        </td>
                        <td>
                            <asp:TextBox ID="PostalcodeTextBox" runat="server" Text='<%# Bind("Postalcode") %>' CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            电话
                        </td>
                        <td>
                            <asp:TextBox ID="PhoneTextBox" runat="server" Text='<%# Bind("Phone") %>' CssClass="input"></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 60px;">
                            传真
                        </td>
                        <td>
                            <asp:TextBox ID="FaxTextBox" runat="server" Text='<%# Bind("Fax") %>' CssClass="input"></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 60px;">
                            手机
                        </td>
                        <td>
                            <asp:TextBox ID="MobileTextBox" runat="server" Text='<%# Bind("Mobile") %>' CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            宅电
                        </td>
                        <td>
                            <asp:TextBox ID="HomePhoneTextBox" runat="server" Text='<%# Bind("HomePhone") %>' CssClass="input"></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 60px;">
                            E_MAIL
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="EmailTextBox" runat="server" Text='<%# Bind("Email") %>' CssClass="input" Width="65%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            网址
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="NetAddressTextBox" runat="server" Text='<%# Bind("NetAddress") %>' CssClass="input" Width="65%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            爱好
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="HobbyTextBox" runat="server" Text='<%# Bind("Hobby") %>' CssClass="input" Height="40px" TextMode="MultiLine" Width="65%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            家庭地址
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="HomeAddressTextBox" runat="server" Text='<%# Bind("HomeAddress") %>' CssClass="input" Width="65%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            籍贯
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="NativePlaceTextBox" runat="server" Text='<%# Bind("NativePlace") %>' CssClass="input" Width="65%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            婚姻状况
                        </td>
                        <td>
                            &nbsp;<asp:DropDownList ID="WedLockDropDownList" runat="server" DataSourceID="WedLockObjectDataSource"
                                SelectedValue='<%# Bind("WedLock") %>'>
                            </asp:DropDownList></td>
                        <td class="form-item" style="width: 60px;">
                            名片类型
                        </td>
                        <td>
                            <asp:DropDownList ID="CardTypeDropDownList" runat="server" DataSourceID="CardTypeObjectDataSource"
                                SelectedValue='<%# Bind("CardType") %>'>
                            </asp:DropDownList></td>
                        <td class="form-item" style="width: 60px;">
                            生日
                        </td>
                        <td>
                            <cc1:Calendar ID="Birthday" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                Value="">
                            </cc1:Calendar>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            备注
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="RemarkTextBox" runat="server" Text='<%# Bind("Remark") %>' CssClass="input" Height="60px" TextMode="MultiLine" Width="65%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            首次联系时间</td>
                        <td>
                            <cc1:Calendar ID="ContactTime" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                Value="">
                            </cc1:Calendar>
                           </td>
                           <td class="form-item" style="width:80px;">公开程度                           
                           </td>
                           <td colspan="3">
                               <asp:RadioButtonList ID="PublicSatuesRadioButtonList" runat="server" RepeatDirection="Horizontal">
                                   <asp:ListItem Text="公开" Selected="True" Value="0"></asp:ListItem>
                                   <asp:ListItem Text="保密" Value="1"></asp:ListItem>
                               </asp:RadioButtonList></td>
                    </tr>
                </table>
            </EditItemTemplate>
            <InsertItemTemplate>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="tools-area" style="width: 100%; height: 25px;" valign="top">
                    <img align="absMiddle" alt="" src="../images/btn_li.gif">
                <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CssClass="button"
                    Text="添加" OnClick="InsertButton_Click" />
                <input type="reset" class="button" value="重置" />
                <input type="button" class="button" value="关闭" onclick="self.close()" />
            </td></tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            姓名
                        </td>
                        <td>
                            <asp:TextBox ID="UserNameTextBox" runat="server" CssClass="input" Font-Size="9pt"
                                Text='<%# Bind("UserName") %>'></asp:TextBox><span style="color:Red">*</span>
                            <asp:RequiredFieldValidator ID="NameRequiredFieldValidator" runat="server" ControlToValidate="UserNameTextBox"
                                ErrorMessage="提示：姓名不能为空"></asp:RequiredFieldValidator></td>
                        <td class="form-item" style="width: 60px;">
                            性别</td>
                        <td>
                            <asp:DropDownList ID="SexDropDownList" runat="server">
                                <asp:ListItem>|--请选择--|</asp:ListItem>
                                <asp:ListItem>男</asp:ListItem>
                                <asp:ListItem>女</asp:ListItem>
                            </asp:DropDownList></td>
                        <td class="form-item" style="width: 60px;">
                            年龄</td>
                        <td>
                            <asp:TextBox ID="AgeTextBox" runat="server" CssClass="input" Text='<%# Bind("Age") %>'
                                Width="50%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            单位名称</td>
                        <td colspan="5">
                            <asp:TextBox ID="CompanyNameTextBox" runat="server" CssClass="input" Text='<%# Bind("CompanyName") %>'
                                Width="65%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            单位地址
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="CompanyAddressTextBox" runat="server" CssClass="input" Text='<%# Bind("CompanyAddress") %>'
                                Width="65%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            部门
                        </td>
                        <td>
                            <asp:TextBox ID="DeptTextBox" runat="server" CssClass="input" Text='<%# Bind("Dept") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 60px;">
                            职务
                        </td>
                        <td>
                            <asp:TextBox ID="HeadshipTextBox" runat="server" CssClass="input" Text='<%# Bind("Headship") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 60px;">
                            邮编
                        </td>
                        <td>
                            <asp:TextBox ID="PostalcodeTextBox" runat="server" CssClass="input" Text='<%# Bind("Postalcode") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            电话
                        </td>
                        <td>
                            <asp:TextBox ID="PhoneTextBox" runat="server" CssClass="input" Text='<%# Bind("Phone") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 60px;">
                            传真
                        </td>
                        <td>
                            <asp:TextBox ID="FaxTextBox" runat="server" CssClass="input" Text='<%# Bind("Fax") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 60px;">
                            手机
                        </td>
                        <td>
                            <asp:TextBox ID="MobileTextBox" runat="server" CssClass="input" Text='<%# Bind("Mobile") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            宅电
                        </td>
                        <td>
                            <asp:TextBox ID="HomePhoneTextBox" runat="server" CssClass="input" Text='<%# Bind("HomePhone") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 60px;">
                            E_MAIL
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="EmailTextBox" runat="server" CssClass="input" Text='<%# Bind("Email") %>'
                                Width="65%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            网址
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="NetAddressTextBox" runat="server" CssClass="input" Text='<%# Bind("NetAddress") %>'
                                Width="65%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            爱好
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="HobbyTextBox" runat="server" CssClass="input" Height="40px" Text='<%# Bind("Hobby") %>'
                                TextMode="MultiLine" Width="65%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            家庭地址
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="HomeAddressTextBox" runat="server" CssClass="input" Text='<%# Bind("HomeAddress") %>'
                                Width="65%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            籍贯
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="NativePlaceTextBox" runat="server" CssClass="input" Text='<%# Bind("NativePlace") %>'
                                Width="65%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            婚姻状况
                        </td>
                        <td>
                            &nbsp;<asp:DropDownList ID="WedLockDropDownList" runat="server" DataSourceID="WedLockObjectDataSource">
                            </asp:DropDownList></td>
                        <td class="form-item" style="width: 60px;">
                            名片类型
                        </td>
                        <td>
                            &nbsp;<asp:DropDownList ID="CardTypeDropDownList" runat="server" DataSourceID="CardTypeObjectDataSource">
                            </asp:DropDownList></td>
                        <td class="form-item" style="width: 60px;">
                            生日
                        </td>
                        <td>
                            <cc1:Calendar ID="Birthday" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                Value="">
                            </cc1:Calendar>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            备注
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="RemarkTextBox" runat="server" CssClass="input" Height="60px" Text='<%# Bind("Remark") %>'
                                TextMode="MultiLine" Width="65%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            首次联系时间</td>
                        <td>
                            <cc1:Calendar ID="ContactTime" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                Value="">
                            </cc1:Calendar>
                        </td>
                        <td class="form-item" style="width: 80px;">
                            公开程度
                        </td>
                        <td colspan="3">
                            <asp:RadioButtonList ID="PublicSatuesRadioButtonList" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="公开" Selected="True" Value="0"></asp:ListItem>
                            <asp:ListItem Text="保密" Value="1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </InsertItemTemplate>
            <ItemTemplate>
            <table border="0" cellspacing="0" width="100%">
            <tr>
                <td class="tools-area" style="width: 100%; height: 25px;" valign="top">
                    <img align="absMiddle" src="../images/btn_li.gif" alt="">
            <asp:Button ID="EditButton" runat="server" CssClass="button" Text="编辑" OnClick="EditButton_Click"/>
            <asp:Button runat="server" ID="DeleteButton" CssClass="button" Text="删除" OnClick="DeleteButton_Click" />
            <input type="button" class="button" onclick="self.close()" value="关闭"/>
            </td></tr>
            </table>
                <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            姓名
                        </td>
                        <td>
                            <asp:Label ID="UserNameLabel" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 60px;">
                            性别</td>
                        <td>
                            <asp:Label ID="SexLabel" runat="server" Text='<%# Bind("Sex") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 60px;">
                            年龄</td>
                        <td>
                            <asp:Label ID="AgeLabel" runat="server" Text='<%# Bind("Age") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            单位名称</td>
                        <td colspan="5">
                            <asp:Label ID="CompanyNameLabel" runat="server" Text='<%# Bind("CompanyName") %>'>
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            单位地址
                        </td>
                        <td colspan="5">
                            <asp:Label ID="CompanyAddressLabel" runat="server" Text='<%# Bind("CompanyAddress") %>'>
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            部门
                        </td>
                        <td>
                            <asp:Label ID="DeptLabel" runat="server" Text='<%# Bind("Dept") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 60px;">
                            职务
                        </td>
                        <td>
                            <asp:Label ID="HeadshipLabel" runat="server" Text='<%# Bind("Headship") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 60px;">
                            邮编
                        </td>
                        <td>
                            <asp:Label ID="PostalcodeLabel" runat="server" Text='<%# Bind("Postalcode") %>'>
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            电话
                        </td>
                        <td>
                            <asp:Label ID="PhoneLabel" runat="server" Text='<%# Bind("Phone") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 60px;">
                            传真
                        </td>
                        <td>
                            <asp:Label ID="FaxLabel" runat="server" Text='<%# Bind("Fax") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 60px;">
                            手机
                        </td>
                        <td>
                            <asp:Label ID="MobileLabel" runat="server" Text='<%# Bind("Mobile") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            宅电
                        </td>
                        <td>
                            <asp:Label ID="HomePhoneLabel" runat="server" Text='<%# Bind("HomePhone") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 60px;">
                            E_MAIL
                        </td>
                        <td colspan="3">
                            <asp:Label ID="EmailLabel" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            网址
                        </td>
                        <td colspan="5">
                            <asp:Label ID="NetAddressLabel" runat="server" Text='<%# Bind("NetAddress") %>'>
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            爱好
                        </td>
                        <td colspan="5">
                            <asp:Label ID="HobbyLabel" runat="server" Text='<%# Bind("Hobby") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            家庭地址
                        </td>
                        <td colspan="5">
                            <asp:Label ID="HomeAddressLabel" runat="server" Text='<%# Bind("HomeAddress") %>'>
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            籍贯
                        </td>
                        <td colspan="5">
                            <asp:Label ID="NativePlaceLabel" runat="server" Text='<%# Bind("NativePlace") %>'>
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            婚姻状况
                        </td>
                        <td>
                            <asp:Label ID="WedlockLabel" runat="server" Text='<%# Bind("Wedlock") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 60px;">
                            名片类型
                        </td>
                        <td>
                            <asp:Label ID="CardTypeLabel" runat="server" Text='<%# Bind("CardType") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 60px;">
                            生日
                        </td>
                        <td >
                            <asp:Label ID="BirthdayLabel" runat="server" Text='<%# Bind("Birthday") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            备注
                        </td>
                        <td colspan="5">
                            <asp:Label ID="RemarkLabel" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 80px;">
                            首次联系时间</td>
                        <td>
                            <asp:Label ID="ContactTimeLabel" runat="server" Text='<%# Bind("ContactTime") %>'>
                            </asp:Label>
                        </td>
                        <td class="form-item" style="width:80px;">公开程度</td>
                        <td colspan="3">
                        <asp:Label ID="PublicStatusLabel" runat="server" Text='<%# Bind("PublicStatus") %>'></asp:Label>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:FormView>
        <asp:ObjectDataSource ID="WedLockObjectDataSource" runat="server" SelectMethod="GetWedLockType"
            TypeName="RmsOA.BFL.GK_OA_CardsFolderBFL"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="CardTypeObjectDataSource" runat="server" SelectMethod="GetCardType"
            TypeName="RmsOA.BFL.GK_OA_CardsFolderBFL"></asp:ObjectDataSource>
    </form>
</body>
</html>
