<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GK_OA_OutFileSignEdit.aspx.cs"
    Inherits="RmsOA_GK_OA_OutFileSignEdit" %>

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
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>文件签发</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript" src="../Rms.js"></script>

    <link href="../Images/infra.css" type="text/css" rel="stylesheet">

    <script type="text/javascript" language="javascript">
        function SelectUnit(Flag)
		{
		    document.all.Flag.value= Flag;
			OpenSmallWindow("../SelectBox/SelectUnit.aspx?UnitCode=000000");
		}
		function SelectUnitReturn(code, name)
		{
		   if(document.all.Flag.value=="1")
		   {
			    window.document.all.FormView1_txtUnitName.value = name;
			    window.document.all.FormView1_txtUnit.value = code;
			}
			else
			{
			    window.document.all.FormView1_txtUnitName1.value = name;
			    window.document.all.FormView1_txtUnit1.value = code;
			}
		}	
		
		function OpenRequisition()
        {
		    OpenFullWindow('<%= RmsPM.BLL.WorkFlowRule.GetProcedureURLByName("文件签发单")%>?OutFileSignCode=<%= FormView1.DataKey.Value %>&ProjectCode=<%= Request["ProjectCode"] + ""%>','文件签发单审批');
        }
    </script>

    <link href="/../Images/index.css" rel="stylesheet" type="text/css" />
    <link href="/../Images/index.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td height="25">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                                文件签发</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%"
                        OnItemInserted="FormView1_ItemInserted" OnItemDeleted="FormView1_ItemDeleted"
                        OnItemUpdated="FormView1_ItemUpdated" DataKeyNames="Code" OnItemInserting="FormView1_ItemInserting"
                        OnDataBound="FormView1_DataBound" OnItemUpdating="FormView1_ItemUpdating">
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
                                        质量记录分号：</td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text='GKFC-JL-CX-420103'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        标识序号：</td>
                                    <td>
                                        <asp:TextBox ID="FileCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("FileCode") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        文件标题：</td>
                                    <td>
                                        <asp:TextBox ID="FileTitleTextBox" runat="server" CssClass="input" Text='<%# Bind("FileTitle") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        文件编号：</td>
                                    <td>
                                        <asp:TextBox ID="OutFileTextBox" runat="server" CssClass="input" Text='<%# Bind("OutFileCode") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        机密程度：</td>
                                    <td>
                                        <asp:TextBox ID="SecretTextBox" runat="server" CssClass="input" Text='<%# Bind("Secret") %>'
                                            Visible="false"></asp:TextBox>
                                        <asp:CheckBoxList ID="CheckBoxListSecret" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem>机密</asp:ListItem>
                                            <asp:ListItem>秘密</asp:ListItem>
                                        </asp:CheckBoxList></td>
                                    <td class="form-item" style="width: 20%">
                                        紧急程度：</td>
                                    <td>
                                        <asp:CheckBoxList ID="CheckBoxLisUrgent" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem>缓</asp:ListItem>
                                            <asp:ListItem>急</asp:ListItem>
                                        </asp:CheckBoxList>
                                        <asp:TextBox ID="UrgentTextBox" runat="server" CssClass="input" Text='<%# Bind("Urgent") %>'
                                            Visible="false"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        拟稿部门：</td>
                                    <td>
                                        <input id="txtUnit" runat="server" class="input" name="txtUnit" size="8" style="width: 72px;
                                            height: 18px" type="hidden" value='<%# Bind("NB_UnitCode") %>' /><input id="txtUnitName"
                                                runat="server" class="input" name="txtUnit" style="width: 121px; height: 18px"
                                                type="text" /><img onclick="SelectUnit(1);return false;" src="../images/ToolsItemSearch.gif"
                                                    style="cursor: hand" />
                                    </td>
                                    <td class="form-item" style="width: 20%">
                                        拟稿人：</td>
                                    <td>
                                        <asp:TextBox ID="NB_UserCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("NB_UserCode") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        经办部门：</td>
                                    <td>
                                        <input id="txtUnit1" runat="server" class="input" name="txtUnit" size="8" style="width: 72px;
                                            height: 18px" type="hidden" value='<%# Bind("JB_UnitCode") %>' /><input id="txtUnitName1"
                                                runat="server" class="input" name="txtUnit" style="width: 121px; height: 18px"
                                                type="text" /><img onclick="SelectUnit(2);return false;" src="../images/ToolsItemSearch.gif"
                                                    style="cursor: hand" />
                                    </td>
                                    <td class="form-item" style="width: 20%">
                                        经办人：</td>
                                    <td>
                                        <asp:TextBox ID="JB_UserCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("JB_UserCode") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        核稿：</td>
                                    <td>
                                        <asp:TextBox ID="HG_UserCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("HG_UserCode") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        登记日期：</td>
                                    <td>
                                        <cc3:Calendar ID="RegisterDateCalendar" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("RegisterDate") %>'>
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                                <td class="form-item" style="width: 20%">
                                    份数：</td>
                                <td>
                                    <igtxt:WebNumericEdit ID="NumberTextBox" runat="server" CssClass="infra-input-nember"
                                        ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
                                        JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
                                        Width="100" ValueText='<%# Bind("Number") %>'>
                                    </igtxt:WebNumericEdit>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="NumberTextBox"
                                            ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                </td>
                                <tr>
                                    <td class="form-item">
                                        附件
                                    </td>
                                    <td colspan="3" class="blackbordertdpaddingcontent">
                                        <uc1:AttachMentAdd ID="Attachmentadd1" runat="server" CtrlPath="../UserControls/"
                                            AttachMentType="OutFileSign" MasterCode='<%# Eval("Code") %>'></uc1:AttachMentAdd>
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
                                        质量记录分号：</td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text='GKFC-JL-CX-420103'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        标识序号：</td>
                                    <td>
                                        <asp:TextBox ID="FileCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("FileCode") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        文件标题：</td>
                                    <td>
                                        <asp:TextBox ID="FileTitleTextBox" runat="server" CssClass="input" Text='<%# Bind("FileTitle") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        文件编号：</td>
                                    <td>
                                        <asp:TextBox ID="OutFileTextBox" runat="server" CssClass="input" Text='<%# Bind("OutFileCode") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        机密程度：</td>
                                    <td>
                                        <asp:TextBox ID="SecretTextBox" runat="server" CssClass="input" Visible="false" Text='<%# Bind("Secret") %>'></asp:TextBox>
                                        <asp:CheckBoxList ID="CheckBoxListSecret" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem>机密</asp:ListItem>
                                            <asp:ListItem>秘密</asp:ListItem>
                                        </asp:CheckBoxList></td>
                                    <td class="form-item" style="width: 20%">
                                        紧急程度：</td>
                                    <td>
                                        <asp:TextBox ID="UrgentTextBox" runat="server" CssClass="input" Visible="false" Text='<%# Bind("Urgent") %> '></asp:TextBox>
                                        <asp:CheckBoxList ID="CheckBoxLisUrgent" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem>缓</asp:ListItem>
                                            <asp:ListItem>急</asp:ListItem>
                                        </asp:CheckBoxList></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        拟稿部门：</td>
                                    <td>
                                        <input id="txtUnit" runat="server" class="input" name="txtUnit" size="8" style="width: 72px;
                                            height: 18px" type="hidden" value='<%# Bind("NB_UnitCode") %>' /><input id="txtUnitName"
                                                runat="server" class="input" name="txtUnit" style="width: 121px; height: 18px"
                                                type="text" /><img onclick="SelectUnit(1);return false;" src="../images/ToolsItemSearch.gif"
                                                    style="cursor: hand" />
                                    </td>
                                    <td class="form-item" style="width: 20%">
                                        拟稿人：</td>
                                    <td>
                                        <asp:TextBox ID="NB_UserCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("NB_UserCode") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        经办部门：</td>
                                    <td>
                                        <input id="txtUnit1" runat="server" class="input" name="txtUnit" size="8" style="width: 72px;
                                            height: 18px" type="hidden" value='<%# Bind("JB_UnitCode") %>' /><input id="txtUnitName1"
                                                runat="server" class="input" name="txtUnit" style="width: 121px; height: 18px"
                                                type="text" /><img onclick="SelectUnit(2);return false;" src="../images/ToolsItemSearch.gif"
                                                    style="cursor: hand" />
                                    </td>
                                    <td class="form-item" style="width: 20%">
                                        经办人：</td>
                                    <td>
                                        <asp:TextBox ID="JB_UserCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("JB_UserCode") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        核稿：</td>
                                    <td>
                                        <asp:TextBox ID="HG_UserCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("HG_UserCode") %>'></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        登记日期：</td>
                                    <td>
                                        <cc3:Calendar ID="RegisterDateCalendar" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("RegisterDate") %>'>
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                                <td class="form-item" style="width: 20%">
                                    份数：</td>
                                <td>
                                    <igtxt:WebNumericEdit ID="NumberTextBox" runat="server" CssClass="infra-input-nember"
                                        ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
                                        JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
                                        Width="100" ValueText='<%# Bind("Number") %>'>
                                    </igtxt:WebNumericEdit>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="NumberTextBox"
                                            ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                </td>
                                <td class="form-item" style="width: 20%">
                                </td>
                                <td>
                                </td>
                                <tr>
                                    <td class="form-item">
                                        附件
                                    </td>
                                    <td colspan="3" class="blackbordertdpaddingcontent">
                                        <uc1:AttachMentAdd ID="Attachmentadd1" runat="server" CtrlPath="../UserControls/"
                                            AttachMentType="OutFileSign" MasterCode='<%# Eval("Code") %>'></uc1:AttachMentAdd>
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
                                        <input name="btnRequisition" id="btnRequisition" type="button" value=" 提交 " class="button"
                                            runat="server" onclick="javascript:OpenRequisition();return false;">
                                        <asp:Button ID="btnBankOut" runat="server" CssClass="button" Text=" 作废 " OnClick="btnBankOut_Click" />
                                        <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                            type="button" value=" 关闭 " />
                                    </td>
                                </tr>
                            </table>
                            <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        质量记录分号：</td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text='GKFC-JL-CX-420103'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        标识序号：</td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("FileCode") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        文件标题：</td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("FileTitle") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        文件编号：</td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("OutFileCode") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        机密程度：</td>
                                    <td>
                                        <asp:Label ID="SecretLabel" runat="server" Text='<%# Bind("Secret") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        紧急程度：</td>
                                    <td>
                                        <asp:Label ID="UrgentLabel" runat="server" Text='<%# Bind("Urgent") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        拟稿部门：</td>
                                    <td>
                                        <asp:Label ID="UnitLabel" runat="server" Text='<%# Bind("NB_UnitCode") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        拟稿人：</td>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("NB_UserCode") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        经办部门：</td>
                                    <td>
                                        <asp:Label ID="UnitLabel1" runat="server" Text='<%# Bind("JB_UnitCode") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        经办人：</td>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("JB_UserCode") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        核稿：</td>
                                    <td>
                                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("HG_UserCode") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        登记日期：</td>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("RegisterDate") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        份数：</td>
                                    <td>
                                        <asp:Label ID="LabelNumber" runat="server" Text='<%# Bind("Number") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        附件</td>
                                    <td colspan="3" class="blackbordertdpaddingcontent">
                                        &nbsp;
                                        <uc1:AttachMentList ID="Attachmentlist1" runat="server" CtrlPath="../UserControls/"
                                            AttachMentType="OutFileSign" MasterCode='<%# Eval("Code") %>'></uc1:AttachMentList>
                                    </td>
                                </tr>
                            </table>
                            <table cellspacing="0" cellpadding="0" width="470" border="0">
                                <tr id="webtabs">
                                    <td width="20">
                                    </td>
                                    <td class="TabDisplay" id="workflowmsg" runat="server" width="185">
                                        相关流程</td>
                                </tr>
                            </table>
                            <table cellspacing="0" cellpadding="0" width="100%" border="0" id="tabdiv">
                                <tr>
                                    <td>
                                        <uc4:WorkFlowList ID="WorkFlowList1" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsOA.MODEL.GK_OA_OutFileSignModel"
                        DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetGK_OA_OutFileSignListOne"
                        TypeName="RmsOA.BFL.GK_OA_OutFileSignBFL" UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <input id="Flag" type="hidden" name="Flag" runat="server">
            </tr>
        </table>
    </form>
</body>
</html>
