<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GK_OA_EvectionApplyEdit.aspx.cs"
    Inherits="RmsOA_GK_OA_EvectionApplyEdit" %>

<%@ Register Src="../SelectBox/SelectSessionUserUnit.ascx" TagName="SelectSessionUserUnit"
    TagPrefix="uc3" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register Src="../UserControls/inputunit.ascx" TagName="inputunit" TagPrefix="uc1" %>
<%@ Register Src="../WorkFlowControl/WorkFlowList.ascx" TagName="WorkFlowList" TagPrefix="uc4" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Images/index.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Rms.js"></script>

    <title>Ա�����������</title>
</head>
<body>
    <form id="form1" runat="server">

        <script language="javascript" type="text/javascript">
        function SelectUnit()
		{
			OpenSmallWindow("../SelectBox/SelectUnit.aspx?UnitCode=000000");
		}
		function SelectUnitReturn(code, name)
		{
			window.document.all.EvectionFormView_txtUnitName.value = name;
			window.document.all.EvectionFormView_txtUnit.value = code;
		}	
		
		function OpenRequisition()
        {
		    OpenLargeWindow('<%= RmsPM.BLL.WorkFlowRule.GetProcedureURLByName("Ա����������")%>?Code=<%= EvectionFormView.DataKey.Value %>&ProjectCode=<%= Request["FileChangeCode"] + ""%>','EvectionApply');
        }
        </script>

        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td background="../images/topic_bg.gif" class="topic" style="height: 25px; text-align: center;">
                        ��������</td>
                </tr>
            </table>
        </div>
        <asp:FormView ID="EvectionFormView" DataKeyNames="Code" runat="server" Width="100%"
            DataSourceID="FormViewObjectDataSource" OnItemDeleted="EvectionFormView_ItemDeleted"
            OnItemInserted="EvectionFormView_ItemInserted" OnItemInserting="EvectionFormView_ItemInserting"
            OnItemUpdated="EvectionFormView_ItemUpdated" OnItemUpdating="EvectionFormView_ItemUpdating"
            OnDataBound="EvectionFormView_DataBound">
            <ItemTemplate>
                <table id="Table3" class="table" width="100%">
                    <tr>
                        <td class="tools-area" width="16">
                            <img align="absMiddle" src="../images/btn_li.gif" /></td>
                        <td class="tools-area">
                            <asp:Button ID="EditButton" runat="server" CommandName="Edit" CssClass="button" Text=" �޸� " />
                            <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" CssClass="button"
                                Text=" ɾ�� " />
                            <input id="btnRequisition" runat="server" class="button" name="btnRequisition" onclick="OpenRequisition()"
                                type="button" value=" �ύ " />
                            <asp:Button ID="btnBankOut" runat="server" CssClass="button" OnClick="btnBankOut_Click"
                                Text=" ���� " />
                            <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                type="button" value=" �ر� " />
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                    <tr>
                        <td class="form-item" style="width: 100px">
                            ���벿�ţ�</td>
                        <td>
                            <asp:Label ID="DepartmentCodeLabel" runat="server"></asp:Label>
                            <asp:HiddenField ID="DepartmentHiddenField" runat="server" Value='<%# Bind("Dept") %>' />
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ����ʱ�䣺</td>
                        <td>
                            <asp:Label ID="UseSealDateLabel" runat="server" Text='<%# Bind("ApplyDate") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            �����ˣ�</td>
                        <td>
                            <asp:Label ID="UserCodeLabel" runat="server"></asp:Label>
                            <asp:HiddenField ID="UserHiddenField" runat="server" Value='<%# Bind("Applyer") %>' />
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ͬ����Ա��</td>
                        <td>
                            <asp:Label ID="UsersLabel" runat="server" Text='<%# Bind("Users") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            �������ɣ�</td>
                        <td colspan="3">
                            <asp:Label ID="ReasonLabel" runat="server" Text='<%# Bind("Reason") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ����·�ߣ�</td>
                        <td colspan="3">
                            <asp:Label ID="RountLabel" runat="server" Text='<%# Bind("Rount") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            �������ڣ�
                        </td>
                        <td colspan="3">
                            <asp:Label ID="StartDataLabel" runat="server" Text='<%# Bind("StartData") %>'></asp:Label>
                            ��
                            <asp:Label ID="EndDateLabel" runat="server" Text='<%# Bind("EndDate") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ��ͨ���ߣ�
                        </td>
                        <td>
                            <asp:Label ID="VehicleLabel" runat="server" Text='<%# Bind("Vehicle") %>'></asp:Label>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ס�ޱ�׼��
                        </td>
                        <td>
                            <asp:Label ID="LiveLevelLabel" runat="server" Text='<%# Bind("LiveLevel") %>'></asp:Label>�Ǳ���
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            Ԥ�Ʒ��ã�
                        </td>
                        <td>
                            <asp:Label ID="BudgetMoneyLabel" runat="server" Text='<%# Bind("BudgetMoney") %>'>
                            </asp:Label>
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="0" width="470">
                    <tr id="webtabs">
                        <td width="20">
                        </td>
                        <td id="workflowmsg" runat="server" class="TabDisplay" width="185">
                            �������</td>
                    </tr>
                </table>
                <table id="tabdiv" border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <uc4:WorkFlowList ID="WorkFlowList1" runat="server"></uc4:WorkFlowList>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
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
                        <td class="form-item" style="width: 100px">
                            ���벿�ţ�</td>
                        <td>
                            <asp:Label ID="DepartmentCodeLabel" runat="server"></asp:Label>
                            <asp:HiddenField ID="DepartmentHiddenField" runat="server" Value='<%# Bind("Dept") %>' />
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ����ʱ�䣺</td>
                        <td>
                            <asp:Label ID="ApplySealDateLabel" runat="server" Text='<%# Bind("ApplyDate") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            �����ˣ�</td>
                        <td>
                            <asp:Label ID="UserCodeLabel" runat="server"></asp:Label>
                            <asp:HiddenField ID="UserHiddenField" runat="server" Value='<%# Bind("Applyer") %>' />
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ͬ����Ա��</td>
                        <td>
                            <asp:TextBox ID="UsersTextBox" runat="server" Text='<%# Bind("Users") %>' CssClass="input"
                                Height="50px" TextMode="MultiLine" Width="90%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            �������ɣ�</td>
                        <td colspan="3">
                            <asp:TextBox ID="ReasonTextBox" runat="server" Text='<%# Bind("Reason") %>' CssClass="input"
                                TextMode="MultiLine" Width="95%" Height="50px" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ReasonTextBox"
                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ����·�ߣ�</td>
                        <td colspan="3">
                            <asp:TextBox ID="RountTextBox" runat="server" Text='<%# Bind("Rount") %>' CssClass="input"
                                Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            �������ڣ�
                        </td>
                        <td colspan="3">
                            <cc3:Calendar ID="StartDateCalendar" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                Display="True" ReadOnly="False" Value='<%# Bind("StartData") %>'>
                            </cc3:Calendar>
                            ��
                            <cc3:Calendar ID="EndDateCalendar" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                Display="True" ReadOnly="False" Value='<%# Bind("EndDate") %>'>
                            </cc3:Calendar>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ��ͨ���ߣ�
                        </td>
                        <td>
                            <asp:TextBox ID="VehicleTextBox" runat="server" Text='<%# Bind("Vehicle") %>' CssClass="input"></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ס�ޱ�׼��
                        </td>
                        <td>
                            <asp:TextBox ID="LiveLevelTextBox" runat="server" Text='<%# Bind("LiveLevel") %>'
                                CssClass="input" Width="50px"></asp:TextBox>
                            �Ǳ���<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="LiveLevelTextBox"
                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            Ԥ�Ʒ��ã�
                        </td>
                        <td>
                            <asp:TextBox ID="BudgetMoneyTextBox" runat="server" Text='<%# Bind("BudgetMoney") %>'
                                CssClass="input" Width="60px"></asp:TextBox>
                            Ԫ
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="BudgetMoneyTextBox"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                ControlToValidate="BudgetMoneyTextBox" ErrorMessage="�����ʽ����" ValidationExpression="^[0-9]\d*[.]?\d*$"></asp:RegularExpressionValidator></td>
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
                        <td class="form-item" style="width: 100px">
                            ���벿�ţ�</td>
                        <td>
                            <uc3:SelectSessionUserUnit ID="SelectSessionUserUnit" runat="server" SelectedValue='<%# Bind("Dept") %>'
                                UserCode="<%# UserCode %>" />
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ����ʱ�䣺</td>
                        <td>
                            <asp:Label ID="ApplySealDateLabel" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            �����ˣ�</td>
                        <td>
                            <asp:Label ID="UserCodeLabel" runat="server"></asp:Label>
                            <asp:HiddenField ID="UserHiddenField" runat="server" Value='<%# Bind("Applyer") %>' />
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ͬ����Ա��</td>
                        <td>
                            <asp:TextBox ID="UsersTextBox" runat="server" CssClass="input" Height="50px" Text='<%# Bind("Users") %>'
                                TextMode="MultiLine" Width="90%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            �������ɣ�</td>
                        <td colspan="3">
                            <asp:TextBox ID="ReasonTextBox" runat="server" CssClass="input" Text='<%# Bind("Reason") %>'
                                TextMode="MultiLine" Width="95%" Height="50px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ReasonTextBox"
                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ����·�ߣ�</td>
                        <td colspan="3">
                            <asp:TextBox ID="RountTextBox" runat="server" CssClass="input" Text='<%# Bind("Rount") %>'
                                Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            �������ڣ�
                        </td>
                        <td colspan="3">
                            <cc3:Calendar ID="StartDateCalendar" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                Display="True" ReadOnly="False" Value='<%# Bind("StartData") %>'>
                            </cc3:Calendar>
                            ��
                            <cc3:Calendar ID="EndDateCalendar" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                Display="True" ReadOnly="False" Value='<%# Bind("EndDate") %>'>
                            </cc3:Calendar>
                        </td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            ��ͨ���ߣ�
                        </td>
                        <td>
                            <asp:TextBox ID="VehicleTextBox" runat="server" CssClass="input" Text='<%# Bind("Vehicle") %>'></asp:TextBox>
                        </td>
                        <td class="form-item" style="width: 100px;">
                            ס�ޱ�׼��
                        </td>
                        <td>
                            <asp:TextBox ID="LiveLevelTextBox" runat="server" CssClass="input" Text='<%# Bind("LiveLevel") %>'
                                Width="50px"></asp:TextBox>
                            �Ǳ���<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="LiveLevelTextBox"
                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 100px;">
                            Ԥ�Ʒ��ã�
                        </td>
                        <td>
                            <asp:TextBox ID="BudgetMoneyTextBox" runat="server" CssClass="input" Text='<%# Bind("BudgetMoney") %>'
                                Width="60px"></asp:TextBox>
                            Ԫ
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="BudgetMoneyTextBox"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                ControlToValidate="BudgetMoneyTextBox" ErrorMessage="�����ʽ����" ValidationExpression="^[0-9]\d*[.]?\d*$"></asp:RegularExpressionValidator></td>
                    </tr>
                </table>
            </InsertItemTemplate>
        </asp:FormView>
        <span style="font-size: 12px;">ע��<br />
            &nbsp; &nbsp;&nbsp; 1�������ɻ��򳬳�ס�ޱ�׼�����Ⱦ��ܾ�����׼��
            <br />
            &nbsp; &nbsp;&nbsp; 2�����������Ʊ����Ʊ��ǰ����֪ͨ�������²���Ԥ����Ӧ�ķ��á� </span>
        <asp:ObjectDataSource ID="FormViewObjectDataSource" runat="server" SelectMethod="GetGK_OA_EvectionApplyListOne"
            TypeName="RmsOA.BFL.GK_OA_EvectionApplyBFL" OldValuesParameterFormatString="original_{0}"
            DataObjectTypeName="RmsOA.MODEL.GK_OA_EvectionApplyModel" DeleteMethod="Delete"
            InsertMethod="Insert" UpdateMethod="Update" OnInserted="FormViewObjectDataSource_Inserted">
            <SelectParameters>
                <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
