<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CL_OilEdit.aspx.cs" Inherits="RmsOA_CL_OilEdit" %>

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

<html>
<head id="Head1" runat="server">
    <title>��������</title>
   
        <META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>

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
		
		function OpenRequisition()
        {
		    OpenFullWindow('<%= RmsPM.BLL.WorkFlowRule.GetProcedureURLByName("������������")%>?ManPowerNeedCode=<%= FormView1.DataKey.Value %>&ProjectCode=<%= Request["ProjectCode"] + ""%>','������������');
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
                                ��������</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%"
                        OnItemInserted="FormView1_ItemInserted" OnItemDeleted="FormView1_ItemDeleted"
                        OnItemUpdated="FormView1_ItemUpdated" OnItemInserting="FormView1_ItemInserting"
                        DataKeyNames="Code" OnDataBound="FormView1_DataBound" OnItemUpdating="FormView1_ItemUpdating">
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
                                    <td class="form-item" style="width: 20%">
                                        ������¼�ţ�</td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text='GKFC-JL-ZY-630201'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        ��ʶ��ţ�</td>
                                    <td>
                                        <asp:TextBox ID="IndexNum" runat="server" CssClass="input" Text='<%# Bind("IndexNum") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        ���ţ�</td>
                                    <td>
                                        <asp:TextBox ID="Car_id" runat="server" CssClass="input" Text='<%# Bind("Car_id") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        �������ڣ�</td>
                                    <td>
                                        <cc3:Calendar ID="GetDate" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("GetDate") %>'>
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        �����ˣ�</td>
                                    <td>
                                        <asp:TextBox ID="GetMan" runat="server" CssClass="input" Text='<%# Bind("GetMan") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        ����������</td>
                                    <td>
                                        <igtxt:WebNumericEdit ID="MoneyTextBox" runat="server" CssClass="infra-input-nember"
                                            ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
                                            JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
                                            Width="100" ValueText='<%# Bind("GetNum") %>' MinDecimalPlaces="Two">
                                        </igtxt:WebNumericEdit>������
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        �ϴι�������</td>
                                    <td>
                                        <igtxt:WebNumericEdit ID="WebnumericeditFirstMil" runat="server" CssClass="infra-input-nember"
                                            ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
                                            JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
                                            Width="100" ValueText='<%# Bind("FirstMil") %>'>
                                        </igtxt:WebNumericEdit>
                                    </td>
                                    <td class="form-item" style="width: 20%">
                                        ���ι�������</td>
                                    <td>
                                        <igtxt:WebNumericEdit ID="WebnumericeditThisMil" runat="server" CssClass="infra-input-nember"
                                            ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
                                            JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
                                            Width="100" ValueText='<%# Bind("ThisMil") %>'>
                                        </igtxt:WebNumericEdit>
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
                                    <td class="form-item" style="width: 20%">
                                        ������¼�ţ�</td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text='GKFC-JL-ZY-630201'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        ��ʶ��ţ�</td>
                                    <td>
                                        <asp:TextBox ID="IndexNum" runat="server" CssClass="input" Text='<%# Bind("IndexNum") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        ���ţ�</td>
                                    <td>
                                        <asp:TextBox ID="Car_id" runat="server" CssClass="input" Text='<%# Bind("Car_id") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        �������ڣ�</td>
                                    <td>
                                        <cc3:Calendar ID="GetDate" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("GetDate") %>'>
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        �����ˣ�</td>
                                    <td>
                                        <asp:TextBox ID="GetMan" runat="server" CssClass="input" Text='<%# Bind("GetMan") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        ����������</td>
                                    <td>
                                        <igtxt:WebNumericEdit ID="MoneyTextBox" runat="server" CssClass="infra-input-nember"
                                            ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
                                            JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
                                            Width="100" ValueText='<%# Bind("GetNum") %>' MinDecimalPlaces="Two" >
                                        </igtxt:WebNumericEdit>������
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        �ϴι�������</td>
                                    <td>
                                        <igtxt:WebNumericEdit ID="WebnumericeditFirstMil" runat="server" CssClass="infra-input-nember"
                                            ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
                                            JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
                                            Width="100" ValueText='<%# Bind("FirstMil") %>' MinDecimalPlaces="Two">
                                        </igtxt:WebNumericEdit>
                                    </td>
                                    <td class="form-item" style="width: 20%">
                                        ���ι�������</td>
                                    <td>
                                        <igtxt:WebNumericEdit ID="WebnumericeditThisMil" runat="server" CssClass="infra-input-nember"
                                            ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
                                            JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
                                            Width="100" ValueText='<%# Bind("ThisMil") %>' MinDecimalPlaces="Two">
                                        </igtxt:WebNumericEdit>
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
                                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" CssClass="button" Text=" �޸� " />
                                        <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" CssClass="button"
                                            Text=" ɾ�� " />
                                        <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                            type="button" value=" �ر� " />
                                    </td>
                                </tr>
                            </table>
                            <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        ������¼�ţ�</td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text='GKFC-JL-ZY-630201'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        ��ʶ��ţ�</td>
                                    <td>
                                        <asp:Label ID="IndexNum" runat="server" Text='<%# Bind("IndexNum") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        ���ţ�</td>
                                    <td>
                                        <asp:Label ID="Car_id" runat="server" Text='<%# Bind("Car_id") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        �������ڣ�</td>
                                    <td>
                                        <asp:Label ID="GetDate" runat="server" Text='<%# Bind("GetDate") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        �����ˣ�</td>
                                    <td>
                                        <asp:Label ID="GetMan" runat="server" Text='<%# Bind("GetMan") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        ����������</td>
                                    <td>
                                        <asp:Label ID="GetNum" runat="server" Text='<%# Bind("GetNum") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        �ϴι�������</td>
                                    <td>
                                        <asp:Label ID="FactMil" runat="server" Text='<%# Bind("FirstMil") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        ���ι�������</td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("ThisMil") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        ʵ�ʹ�������</td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("FactMil") %>'></asp:Label></td>
                                </tr>
                            </table>
                            <table cellspacing="0" cellpadding="0" width="470" border="0">
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsOA.MODEL.GK_OA_OilModel"
                        DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetGK_OA_OilListOne"
                        TypeName="RmsOA.BFL.GK_OA_OilBFL" UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted">
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
