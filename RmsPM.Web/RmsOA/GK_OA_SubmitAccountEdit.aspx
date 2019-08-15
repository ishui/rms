<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GK_OA_SubmitAccountEdit.aspx.cs"
    Inherits="RmsOA_GK_OA_SubmitAccountEdit" %>

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
    <title>车改人员报销</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript" src="../Rms.js"></script>

    <link href="../Images/infra.css" type="text/css" rel="stylesheet">

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
        
        function OpenSubmitAccountDtl()
		{
			OpenSmallWindow("./GK_OA_SubmitAccountDtlEdit.aspx?SubmitAccountMainCode=<%= FormView1.DataKey.Value %>&ProjectCode=<%= Request["ProjectCode"] + ""%>','SubmitAccountDtlEdit'");
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
                                车改人员报销</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%"
                        OnItemInserted="FormView1_ItemInserted" OnItemDeleted="FormView1_ItemDeleted"
                        OnItemUpdated="FormView1_ItemUpdated" DataKeyNames="Code" OnDataBound="FormView1_DataBound"
                        OnItemInserting="FormView1_ItemInserting">
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
                                        文件编号：</td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="GKFC-ZY-630202"></asp:Label>
                                    </td>
                                    <td class="form-item" style="width: 20%">
                                        标识序号：</td>
                                    <td>
                                        <asp:TextBox ID="FileCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("FileCode") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        姓名：</td>
                                    <td>
                                        <asp:TextBox ID="NameTextBox" runat="server" CssClass="input" Text='<%# Bind("Name") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="NameTextBox"
                                            ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                    <td class="form-item" style="width: 20%">
                                        职务：</td>
                                    <td>
                                        <asp:TextBox ID="DutiesTextBox" runat="server" CssClass="input" Text='<%# Bind("Duties") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        登记日期：</td>
                                    <td>
                                        <cc3:Calendar ID="SubmitDateCalendar" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("RegiesterDate") %>'>
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                            </table>
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td class="intopic" width="200">
                                        报销明细</td>
                                    <td>
                                        <input class="button-small" id="btnAddDtl" type="button" value="新 增" name="btnAddDtl"
                                            runat="server" onserverclick="btnAddDtl_ServerClick"></td>
                                </tr>
                            </table>
                            </td></tr>
                            <tr height="100%">
                                <td>
                                    <asp:DataGrid ID="dgDtl" onkeydown="if(event.keyCode==13) event.keyCode=9" runat="server"
                                        DataKeyField="Code" CellPadding="0" AutoGenerateColumns="False" GridLines="Horizontal"
                                        Width="100%" CssClass="list" OnDeleteCommand="dgDtl_DeleteCommand">
                                        <HeaderStyle CssClass="list-title" />
                                        <FooterStyle CssClass="list-title" />
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="序号">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemTemplate>
                                                    <%# Container.ItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderTemplate>
                                                    月份<font color="red">*</font></HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                                <ItemTemplate>
                                                    <igtxt:WebDateTimeEdit ID="dtMonth" CssClass="infra-input-year" PromptChar=" " JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
                                                        JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
                                                        Width="80px" EditModeFormat="yyyy-MM" DisplayModeFormat="yyyy-MM" Value='<%# DataBinder.Eval(Container, "DataItem.Month","{0:yyyy-MM}") %>'
                                                        runat="server">
                                                        <SpinButtons EnsureFocus="True" Display="OnRight"></SpinButtons>
                                                    </igtxt:WebDateTimeEdit>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderTemplate>
                                                    标准费用(元)<font color="red">*</font></HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                                <ItemTemplate>
                                                    <igtxt:WebNumericEdit ID="txtStandardCost" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.StandardCost") %>'
                                                        CssClass="infra-input-nember" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
                                                        JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
                                                        MinDecimalPlaces="Two">
                                                    </igtxt:WebNumericEdit>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderTemplate>
                                                    实际费用(元)<font color="red">*</font></HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                                <ItemTemplate>
                                                    <igtxt:WebNumericEdit ID="txtRealityCost" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.RealityCost") %>'
                                                        CssClass="infra-input-nember" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
                                                        JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
                                                        MinDecimalPlaces="Two">
                                                    </igtxt:WebNumericEdit>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderTemplate>
                                                    备注<font color="red">*</font></HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRemark" CssClass="input" Text='<%# Bind("Remark")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="删除">
                                                <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnDelete" runat="server" Text="<img src=../images/del.gif width=16 height=16 border=0>"
                                                        CausesValidation="false" CommandName="Delete"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
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
                                        质量记录分类号：</td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="GKFC-ZY-630202"></asp:Label>
                                    </td>
                                    <td class="form-item" style="width: 20%">
                                        标识序号：</td>
                                    <td>
                                        <asp:TextBox ID="FileCodeTextBox" runat="server" CssClass="input" Text='<%# Bind("FileCode") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        姓名：</td>
                                    <td>
                                        <asp:TextBox ID="NameTextBox" runat="server" CssClass="input" Text='<%# Bind("Name") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="NameTextBox"
                                            ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                    <td class="form-item" style="width: 20%">
                                        职务：</td>
                                    <td>
                                        <asp:TextBox ID="DutiesTextBox" runat="server" CssClass="input" Text='<%# Bind("Duties") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        登记日期：</td>
                                    <td>
                                        <cc3:Calendar ID="SubmitDateCalendar" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("RegiesterDate") %>'>
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                            </table>
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td class="intopic" width="200">
                                        报销明细</td>
                                    <td>
                                        <input class="button-small" id="btnAddDtl" type="button" value="新 增" name="btnAddDtl"
                                            runat="server" onserverclick="btnAddDtl_ServerClick"></td>
                                </tr>
                            </table>
                            </td></tr>
                            <tr height="100%">
                                <td>
                                    <asp:DataGrid ID="dgDtl" onkeydown="if(event.keyCode==13) event.keyCode=9" runat="server"
                                        DataKeyField="Code" CellPadding="0" AutoGenerateColumns="False" GridLines="Horizontal"
                                        Width="100%" CssClass="list" OnDeleteCommand="dgDtl_DeleteCommand">
                                        <HeaderStyle CssClass="list-title" />
                                        <FooterStyle CssClass="list-title" />
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="序号">
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemTemplate>
                                                    <%# Container.ItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderTemplate>
                                                    月份<font color="red">*</font></HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                                <ItemTemplate>
                                                    <igtxt:WebDateTimeEdit ID="dtMonth" CssClass="infra-input-year" PromptChar=" " JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
                                                        JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
                                                        Width="80px" EditModeFormat="yyyy-MM" DisplayModeFormat="yyyy-MM" Value='<%# DataBinder.Eval(Container, "DataItem.Month","{0:yyyy-MM}") %>'
                                                        runat="server">
                                                        <SpinButtons EnsureFocus="True" Display="OnRight"></SpinButtons>
                                                    </igtxt:WebDateTimeEdit>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderTemplate>
                                                    标准费用(元)<font color="red">*</font></HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                                <ItemTemplate>
                                                    <igtxt:WebNumericEdit ID="txtStandardCost" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.StandardCost") %>'
                                                        CssClass="infra-input-nember" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
                                                        JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
                                                        MinDecimalPlaces="Two">
                                                    </igtxt:WebNumericEdit>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderTemplate>
                                                    实际费用(元)<font color="red">*</font></HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                                <ItemTemplate>
                                                    <igtxt:WebNumericEdit ID="txtRealityCost" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.RealityCost") %>'
                                                        CssClass="infra-input-nember" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
                                                        JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
                                                        MinDecimalPlaces="Two">
                                                    </igtxt:WebNumericEdit>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderTemplate>
                                                    备注<font color="red">*</font></HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRemark" CssClass="input" Text='<%# Bind("Remark")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="删除">
                                                <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                                <HeaderStyle Wrap="False" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnDelete" runat="server" Text="<img src=../images/del.gif width=16 height=16 border=0>"
                                                        CausesValidation="false" CommandName="Delete"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
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
                                        质量记录分类号：</td>
                                    <td>
                                        <asp:Label ID="SystemCodeLabel" runat="server" Text='<%# Bind("SystemCode") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        标识序号：</td>
                                    <td>
                                        <asp:Label ID="FileCodeLabel" runat="server" Text='<%# Bind("FileCode") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        姓名：</td>
                                    <td>
                                        <asp:Label ID="NameLabel" runat="server" Text='<%# Bind("Name") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        职务：</td>
                                    <td>
                                        <asp:Label ID="DutiesLabel" runat="server" Text='<%# Bind("Duties") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        登记日期：</td>
                                    <td>
                                        <asp:Label ID="RegiesterLabel" runat="server" Text='<%# Bind("RegiesterDate") %>'></asp:Label></td>
                                </tr>
                                <table cellspacing="0" cellpadding="0" width="100%" border="0" id="tabdiv">
                                    <tr>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource2"
                                                CssClass="List" Width="100%" ShowFooter="True" DataKeyNames="Code">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="月份">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <%# Eval("Month").ToString().Substring(0,6)%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="标准费用(元)" SortExpression="StandardCost">
                                                        <ItemTemplate>
                                                            <%# Eval("StandardCost","") %>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="实际费用(元)" SortExpression="RealityCost">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <%# Eval("RealityCost","") %>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="所剩余额(元)">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <%# Eval("RemainCost","") %>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="累计余额(元)">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <%# Eval("SumCost","") %>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="备注" SortExpression="Remark">
                                                        <ItemTemplate>
                                                            <%# Eval("Remark").ToString().Replace("\n","<br>") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="list-title" />
                                                <HeaderStyle CssClass="list-title" />
                                                <FooterStyle CssClass="list-title" />
                                                <EmptyDataTemplate>
                                                    无报销明细数据
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                        </ItemTemplate>
                    </asp:FormView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsOA.MODEL.GK_OA_SubmitAccountMainModel"
                        DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetGK_OA_SubmitAccountMainListOne"
                        TypeName="RmsOA.BFL.GK_OA_SubmitAccountMainBFL" UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSource2" TypeName="RmsOA.BFL.GK_OA_SubmitAccountDtlBFL"
                        runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetGK_OA_SubmitAccountDtlList"
                        OnSelected="ObjectDataSource2_Selected">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="MastCodeEqual" QueryStringField="Code" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
