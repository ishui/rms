<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YF_AssetMainRecord.aspx.cs"
    Inherits="RmsOA_YF_AssetMainRecord" %>

<%@ Register Src="../UserControls/inputunit.ascx" TagName="inputunit" TagPrefix="uc1" %>
<%@ Register Assembly="AspWebControl" Namespace="AspWebControl" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../Images/index.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../Rms.js" type="text/javascript"></script>

    <title>�豸ά�޼�¼</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td background="../images/topic_bg.gif" class="topic" style="height: 25px; text-align: center;">
                            �̶��ʲ�/�豸ά�޼�¼</td>
                    </tr>
                </table>
            </div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="intopic" style="width: 250px;">
                        ά�������
                    </td>
                </tr>
            </table>
            <asp:FormView ID="FormView2" runat="server" DataSourceID="ObjectDataSource2" Width="100%" OnDataBound="FormView2_DataBound">
                <EditItemTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="tools-area" style="width: 100%; height: 25px;" valign="top">
                                <img align="absMiddle" alt="" src="../images/btn_li.gif">
                                <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="UpDate"
                                    CssClass="button" Text="����" />
                                <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                    CssClass="button" Text="ȡ��" />
                                <input class="button" onclick="self.close()" type="button" value="�ر�" />
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                ��˾����
                            </td>
                            <td>
                                <uc1:inputunit id="DeptInputunit" runat="server" value='<%# Bind("Dept") %>'>
</uc1:inputunit>
                            </td>
                            <td class="form-item" style="width: 100px;">
                                ������
                            </td>
                            <td>
                                <asp:Label ID="UserCodeLabel" runat="server" Text='<%# Bind("UserCode") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                �豸���
                            </td>
                            <td>
                                <asp:Label ID="CodeNOLabel" runat="server"></asp:Label>
                            </td>
                            <td class="form-item" style="width: 100px;">
                                ��������
                            </td>
                            <td>
                                <cc1:Calendar ID="ApplyDate" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                    Value='<%# Bind("ApplyDate") %>'>
                                </cc1:Calendar>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                ��������
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="ReasonTextBox" runat="server" CssClass="input" Text='<%# Bind("Reason") %>'
                                    Width="80%">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                ��������
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="RemarkTextBox" runat="server" Height="60px" Text='<%# Bind("Remark") %>'
                                    TextMode="multiLine" Width="90%">
                                </asp:TextBox>
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
                                <asp:Button ID="btnSave" runat="server" CommandName="Insert" CssClass="button" Text=" ���� " />
                                <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                    type="button" value=" �ر� " />
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                ��˾����
                            </td>
                            <td>
                                <uc1:inputunit id="DeptInputunit" runat="server" value='<%# Bind("Dept") %>'>
</uc1:inputunit>
                            </td>
                            <td class="form-item" style="width: 100px;">
                                ������
                            </td>
                            <td>
                                <asp:Label ID="UserCodeLabel" runat="server" Text='<%# Bind("UserCode") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                �豸���
                            </td>
                            <td>
                                <asp:Label ID="CodeNOLabel" runat="server"></asp:Label>
                            </td>
                            <td class="form-item" style="width: 100px;">
                                ��������
                            </td>
                            <td>
                                <cc1:Calendar ID="ApplyDate" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                    Value='<%# Bind("ApplyDate") %>'>
                                </cc1:Calendar>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                ��������
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="ReasonTextBox" runat="server" CssClass="input" Text='<%# Bind("Reason") %>'
                                    Width="80%">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                ��������
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="RemarkTextBox" runat="server" Height="60px" Text='<%# Bind("Remark") %>'
                                    TextMode="multiLine" Width="90%">
                                </asp:TextBox>
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
                                <asp:Button ID="EditButton" runat="server" CommandArgument="Code" CommandName="Edit"
                                    CssClass="button" Text=" �޸� " />
                                <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" CssClass="button"
                                    Text=" ɾ�� " />
                                <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                    type="button" value=" �ر� " />
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                ��˾����
                            </td>
                            <td>
                                <%# RmsPM.BLL.SystemRule.GetUnitName((string)Eval("Dept"))%>
                            </td>
                            <td class="form-item" style="width: 100px;">
                                ������
                            </td>
                            <td>
                                <asp:Label ID="UserCodeLabel" runat="server" Text='<%# Bind("UserCode") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                �豸���
                            </td>
                            <td>
                                <asp:Label ID="CodeNOLabel" runat="server"></asp:Label>
                            </td>
                            <td class="form-item" style="width: 100px;">
                                ��������
                            </td>
                            <td>
                                <asp:Label ID="ApplyDateLabel" runat="server" Text='<%# Bind("ApplyDate") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                ��������
                            </td>
                            <td colspan="3">
                                <asp:Label ID="ReasonLabel" runat="server" Text='<%# Bind("Reason") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                ��������
                            </td>
                            <td colspan="3">
                                <asp:Label ID="RemarkLabel" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:FormView>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="intopic" style="width: 250px;">
                        ά�޼�¼��
                    </td>
                </tr>
            </table>
            <asp:FormView ID="FormView1" DataKeyNames="Code" runat="server" DataSourceID="ObjectDataSource1" Width="100%" OnDataBound="FormView1_DataBound">
                <EditItemTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="tools-area" style="width: 100%; height: 25px;" valign="top">
                                <img align="absMiddle" alt="" src="../images/btn_li.gif">
                                <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="UpDate"
                                    CssClass="button" Text="����" />
                                <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                    CssClass="button" Text="ȡ��" />
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                ά������
                            </td>
                            <td colspan="3">
                                <asp:TextBox TextMode="multiLine" Height="60px" Width="90%" ID="ContentTextBox" runat="server" Text='<%# Bind("Content") %>'>
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                ����ʱ��
                            </td>
                            <td>
                                <asp:TextBox ID="CostTimeTextBox" runat="server" CssClass="input" Text='<%# Bind("CostTime") %>'>
                                </asp:TextBox>
                                ����
                            </td>
                            <td class="form-item" style="width: 100px;">
                                ���ѷ���
                            </td>
                            <td>
                                <asp:TextBox ID="CostMoneyTextBox" runat="server" Text='<%# Bind("CostMoney") %>' CssClass="input"></asp:TextBox>Ԫ
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                ����
                            </td>
                            <td colspan="3">
                                <asp:TextBox CssClass="input" TextMode="multiLine" Width="90%" Height="60px" ID="ChangeDetailTextBox" runat="server" Text='<%# Bind("ChangeDetail") %>'>
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                �û�ǩ��
                            </td>
                            <td>
                                <asp:TextBox ID="UserSignTextBox" CssClass="input" runat="server" Text='<%# Bind("UserSign") %>'>
                                </asp:TextBox>
                            </td>
                            <td class="form-item" style="width: 100px;">
                                ά����Աǩ��
                            </td>
                            <td>
                                <asp:TextBox ID="MainUserTextBox" runat="server" Text='<%# Bind("MainUser") %>' CssClass="input"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                                ��������</td>
                            <td>
                                <cc1:Calendar ID="TransTime" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                    Value='<%# Bind("MainTime") %>'>
                                </cc1:Calendar>
                             </td>   
                            <td class="form-item" style="width: 100px;">
                                ά�����</td>
                            <td>
                                <asp:TextBox ID="ResultTextBox" runat="server" Text='<%# Bind("Result") %>' CssClass="input"></asp:TextBox>
                        </tr>
                    </table>
                </EditItemTemplate>
                <ItemTemplate>
                
                    <table id="Table3" class="table" width="100%">
                        <tr>
                            <td class="tools-area" width="16">
                                <img align="absMiddle" src="../images/btn_li.gif" /></td>
                            <td class="tools-area">
                                <asp:Button ID="EditButton" runat="server" CommandName="Edit"
                                    CssClass="button" Text=" �޸� " />
                            </td>
                        </tr>
                    </table>
                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form">
                        <tr>
                            <td style="width: 100px;" class="form-item">ά������
                            </td>
                            <td colspan="3">
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Content") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">����ʱ��
                            </td>
                            <td>
                                <asp:Label ID="CostTimeLabel" runat="server" Text='<%# Bind("CostTime") %>'></asp:Label>
                                ����
                                </td>
                            <td class="form-item" style="width: 100px;">
                            ���ѷ���
                            </td>
                            <td>
                                <asp:Label ID="CostMoneyLabel" runat="server" Text='<%# Bind("CostMoney") %>'></asp:Label>Ԫ
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                            ����
                            </td>
                            <td colspan="3">
                                <asp:Label ID="ChangeDetailLabel" runat="server" Text='<%# Bind("ChangeDetail") %>'>
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="width: 100px;">
                            �û�ǩ��
                            </td>
                            <td>
                                <asp:Label ID="UserSignLabel" runat="server" Text='<%# Bind("UserSign") %>'></asp:Label>
                            </td>
                            <td class="form-item" style="width: 100px;"> ά����Աǩ��
                            </td>
                            <td>
                                <asp:Label ID="MainUserLabel" runat="server" Text='<%# Bind("MainUser") %>'></asp:Label>
                            </td>
                            
                        </tr>
                        <tr>
                        <td class="form-item" style="width:100px;">��������</td><td>
                            <asp:Label ID="MainTimeLabel" runat="server" Text='<%# Bind("MainTime") %>'></asp:Label></td>
                        <td class="form-item" style="width:100px;">ά�����</td><td>
                            <asp:Label ID="ResultLabel" runat="server" Text='<%# Bind("Result") %>'></asp:Label></td>
                        </tr>
                    </table>              
                </ItemTemplate>
            </asp:FormView>
        </div>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="RmsOA.BFL.YF_AssetMainRecordBFL"
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetYF_AssetMainRecord" DataObjectTypeName="RmsOA.MODEL.YF_AssetMainRecordModel" UpdateMethod="Update">
            <SelectParameters>
                <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetYF_AssetMainApplyListOne" TypeName="RmsOA.BFL.YF_AssetMainApplyBFL" DataObjectTypeName="RmsOA.MODEL.YF_AssetMainApplyModel" DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update">
            <SelectParameters>
                <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
