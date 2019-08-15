<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GK_OA_MaterialTransferEdit.aspx.cs" Inherits="RmsOA_GK_OA_MaterialTransferEdit" %>


<%@ Register Assembly="AspWebControl" Namespace="AspWebControl" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link href="../Images/index.css" rel="stylesheet" type="text/css" />
    <title>�ʲ�ת���嵥</title>
</head>
<body>

    <script language="javascript" type="text/javascript">		
		function OpenRequisition()
        {
		    OpenFullWindow('<%= RmsPM.BLL.WorkFlowRule.GetProcedureURLByName("�ļ���������")%>?FileChangeCode=<%= AccountFormView.DataKey.Value %>&ProjectCode=<%= Request["ProjectCode"] + ""%>','�̶��ʲ�ת��');
        }
    </script>
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td background="../images/topic_bg.gif" class="topic" style="height: 25px; text-align: center;">
                        �̶��ʲ�/�ͺ�Ʒת��</td>
                </tr>
            </table>
        </div>
        <asp:FormView ID="AccountFormView" runat="server" DataSourceID="FormViewObjectDataSource"
            Width="100%" DataKeyNames="Code" OnItemDeleted="AccountFormView_ItemDeleted" OnItemInserted="AccountFormView_ItemInserted" OnItemInserting="AccountFormView_ItemInserting" OnItemUpdated="AccountFormView_ItemUpdated">
            <EditItemTemplate>
                <table id="Table2" class="table" width="100%">
                    <tr>
                        <td class="tools-area" width="16">
                            <img align="absMiddle" src="../images/btn_li.gif" /></td>
                        <td class="tools-area">
                            <asp:Button ID="btnSave" runat="server" CommandName="Update" CssClass="button" Text=" ���� " />&nbsp;
                            <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                type="button" value=" �ر� " />
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ���ƣ�
                        </td>
                        <td>
                            <asp:TextBox ID="NameTextBox" runat="server" CssClass="input" Text='<%# Bind("Name") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ���
                        </td>
                        <td>
                            <asp:DropDownList ID="TypeDropDownList" runat="server" DataSourceID="DropDownListObjectDataSource">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ��ţ�
                        </td>
                        <td>
                            <asp:TextBox ID="NumberTextBox" runat="server" CssClass="input" Text='<%# Bind("Number") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ����/��λ ��
                        </td>
                        <td>
                            <asp:TextBox ID="NumUnitTextBox" runat="server" CssClass="input" Text='<%# Bind("NumUnit") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ԭʼ���ۣ�
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="OriginalPriceTextBox" runat="server" CssClass="input" Text='<%# Bind("OriginalPrice") %>'></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="OriginalPriceTextBox"
                                ErrorMessage="�����ʽ����" ValidationExpression="^[0-9]\d*[.]?\d*$"></asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ���ԭ��
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="ReasonTextBox" runat="server" CssClass="input" Height="50px" Text='<%# Bind("Reason") %>'
                                TextMode="MultiLine" Width="80%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ǰʹ���ˣ�
                        </td>
                        <td>
                            <asp:TextBox ID="PreUserTextBox" runat="server" CssClass="input" Text='<%# Bind("PreUser") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ��ʹ���ˣ�
                        </td>
                        <td>
                            <asp:TextBox ID="LaterUserTextBox" runat="server" CssClass="input" Text='<%# Bind("LaterUser") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ת�����ţ�
                        </td>
                        <td>
                            <asp:TextBox ID="PreDeptTextBox" runat="server" CssClass="input" Text='<%# Bind("PreDept") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ���ղ��ţ�
                        </td>
                        <td>
                            <asp:TextBox ID="LaterDeptTextBox" runat="server" CssClass="input" Text='<%# Bind("LaterDept") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ת�����Ÿ����ˣ�
                        </td>
                        <td>
                            <asp:TextBox ID="TransferMasterTextBox" runat="server" CssClass="input" Text='<%# Bind("TransferMaster") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ���ղ��Ÿ����ˣ�
                        </td>
                        <td>
                            <asp:TextBox ID="ReciveMasterTextBox" runat="server" CssClass="input" Text='<%# Bind("ReciveMaster") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ת�������ˣ�
                        </td>
                        <td>
                            <asp:TextBox ID="TransferHanderTextBox" runat="server" CssClass="input" Text='<%# Bind("TransferHander") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ���վ����ˣ�
                        </td>
                        <td>
                            <asp:TextBox ID="ReciveHanderTextBox" runat="server" CssClass="input" Text='<%# Bind("ReciveHander") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ת�����ڣ�
                        </td>
                        <td>
                            <cc1:Calendar ID="TransferDate" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                Display="True" ReadOnly="False" Value='<%# Bind("TransferDate") %>'>
                            </cc1:Calendar>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            �������ڣ�
                        </td>
                        <td>
                            <cc1:Calendar ID="ReciveDate" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                Display="True" ReadOnly="False" Value='<%# Bind("ReciveDate") %>'>
                            </cc1:Calendar>
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
                            ���ƣ�
                        </td>
                        <td>
                            <asp:TextBox ID="NameTextBox" runat="server" CssClass="input" Text='<%# Bind("Name") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ���
                        </td>
                        <td>
                            <asp:DropDownList ID="TypeDropDownList" runat="server" DataSourceID="DropDownListObjectDataSource">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ��ţ�
                        </td>
                        <td>
                            <asp:TextBox ID="NumberTextBox" runat="server" CssClass="input" Text='<%# Bind("Number") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ����/��λ ��
                        </td>
                        <td>
                            <asp:TextBox ID="NumUnitTextBox" runat="server" CssClass="input" Text='<%# Bind("NumUnit") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ԭʼ���ۣ�
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="OriginalPriceTextBox" runat="server" CssClass="input" Text='<%# Bind("OriginalPrice") %>'></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="OriginalPriceTextBox"
                                ErrorMessage="�����ʽ����" ValidationExpression="^[0-9]\d*[.]?\d*$"></asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ���ԭ��
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="ReasonTextBox" runat="server" CssClass="input" Height="50px" Text='<%# Bind("Reason") %>'
                                TextMode="MultiLine" Width="80%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ǰʹ���ˣ�
                        </td>
                        <td>
                            <asp:TextBox ID="PreUserTextBox" runat="server" CssClass="input" Text='<%# Bind("PreUser") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ��ʹ���ˣ�
                        </td>
                        <td>
                            <asp:TextBox ID="LaterUserTextBox" runat="server" CssClass="input" Text='<%# Bind("LaterUser") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ת�����ţ�
                        </td>
                        <td>
                            <asp:TextBox ID="PreDeptTextBox" runat="server" CssClass="input" Text='<%# Bind("PreDept") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ���ղ��ţ�
                        </td>
                        <td>
                            <asp:TextBox ID="LaterDeptTextBox" runat="server" CssClass="input" Text='<%# Bind("LaterDept") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ת�����Ÿ����ˣ�
                        </td>
                        <td>
                            <asp:TextBox ID="TransferMasterTextBox" runat="server" CssClass="input" Text='<%# Bind("TransferMaster") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ���ղ��Ÿ����ˣ�
                        </td>
                        <td>
                            <asp:TextBox ID="ReciveMasterTextBox" runat="server" CssClass="input" Text='<%# Bind("ReciveMaster") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ת�������ˣ�
                        </td>
                        <td>
                            <asp:TextBox ID="TransferHanderTextBox" runat="server" CssClass="input" Text='<%# Bind("TransferHander") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ���վ����ˣ�
                        </td>
                        <td>
                            <asp:TextBox ID="ReciveHanderTextBox" runat="server" CssClass="input" Text='<%# Bind("ReciveHander") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ת�����ڣ�
                        </td>
                        <td>
                            <cc1:Calendar ID="TransferDate" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                Display="True" ReadOnly="False" Value='<%# Bind("TransferDate") %>'>
                            </cc1:Calendar>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            �������ڣ�
                        </td>
                        <td>
                            <cc1:Calendar ID="ReciveDate" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                Display="True" ReadOnly="False" Value='<%# Bind("ReciveDate") %>'>
                            </cc1:Calendar>
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
                            <asp:Button ID="Button1" runat="server" CommandName="Edit" CssClass="button" Text=" �޸� " />
                            <asp:Button ID="Button2" runat="server" CommandName="Delete" CssClass="button" Text=" ɾ�� " />
                            <input id="btnRequisition" runat="server" class="button" name="btnRequisition" onclick="javascript:OpenRequisition();return false;"
                                type="button" value=" �ύ ">
                            <asp:Button ID="btnBankOut" runat="server" CssClass="button" OnClick="btnBankOut_Click"
                                Text=" ���� " />
                            <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                type="button" value=" �ر� " />
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ���ƣ�
                        </td>
                        <td>
                            <asp:Label ID="NameLabel" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ���
                        </td>
                        <td>
                            <asp:Label ID="TypeLabel" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ��ţ�
                        </td>
                        <td>
                            <asp:Label ID="NumberLabel" runat="server" Text='<%# Bind("Number") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ����/��λ ��
                        </td>
                        <td>
                            <asp:Label ID="NumUnitLabel" runat="server" Text='<%# Bind("NumUnit") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ԭʼ���ۣ�
                        </td>
                        <td colspan="3">
                            <asp:Label ID="OriginalPriceLabel" runat="server" Text='<%# Bind("OriginalPrice") %>'>
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ���ԭ��
                        </td>
                        <td colspan="3">
                            <asp:Label ID="ReasonLabel" runat="server" Text='<%# Bind("Reason") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ǰʹ���ˣ�
                        </td>
                        <td>
                            <asp:Label ID="PreUserLabel" runat="server" Text='<%# Bind("PreUser") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ��ʹ���ˣ�
                        </td>
                        <td>
                            <asp:Label ID="LaterUserLabel" runat="server" Text='<%# Bind("LaterUser") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ת�����ţ�
                        </td>
                        <td>
                            <asp:Label ID="PreDeptLabel" runat="server" Text='<%# Bind("PreDept") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ���ղ��ţ�
                        </td>
                        <td>
                            <asp:Label ID="LaterDeptLabel" runat="server" Text='<%# Bind("LaterDept") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ת�����Ÿ����ˣ�
                        </td>
                        <td>
                            <asp:Label ID="TransferMasterLabel" runat="server" Text='<%# Bind("TransferMaster") %>'>
                            </asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ���ղ��Ÿ����ˣ�
                        </td>
                        <td>
                            <asp:Label ID="ReciveMasterLabel" runat="server" Text='<%# Bind("ReciveMaster") %>'>
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ת�������ˣ�
                        </td>
                        <td>
                            <asp:Label ID="TransferHanderLabel" runat="server" Text='<%# Bind("TransferHander") %>'>
                            </asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ���վ����ˣ�
                        </td>
                        <td>
                            <asp:Label ID="ReciveHanderLabel" runat="server" Text='<%# Bind("ReciveHander") %>'>
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ת�����ڣ�
                        </td>
                        <td>
                            <asp:Label ID="TransferDateLabel" runat="server" Text='<%# Bind("TransferDate") %>'>
                            </asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            �������ڣ�
                        </td>
                        <td>
                            <asp:Label ID="ReciveDateLabel" runat="server" Text='<%# Bind("ReciveDate") %>'>
                            </asp:Label>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:FormView>
        <asp:ObjectDataSource ID="FormViewObjectDataSource" runat="server" DataObjectTypeName="RmsOA.MODEL.GK_OA_MaterialTransferModel"
            DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetGK_OA_MaterialTransferListOne"
            TypeName="RmsOA.BFL.GK_OA_MaterialTransferBFL" UpdateMethod="Update" OnInserted="FormViewObjectDataSource_Inserted">
            <SelectParameters>
                <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="DropDownListObjectDataSource" runat="server" SelectMethod="GetMaterialType" TypeName="RmsOA.BFL.GK_OA_MaterialTransferBFL"></asp:ObjectDataSource>
    </form>
</body>
</html>
