<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LocaleViseSelect.aspx.cs"
    Inherits="LocaleVise_LocaleViseSelect" %>

<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc2" TagName="InputUnit" Src="../UserControls/InputUnit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputSupplier" Src="../UserControls/InputSupplier.ascx" %>
<%@ Register TagPrefix="cc4" Namespace="Rms.ControlLb" Assembly="RmsPM.BLL" %>
<%@ Register Src="../UserControls/inputcontract.ascx" TagName="inputcontract" TagPrefix="uc3" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>签证选择</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td height="25">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                                签证选择</td>
                        </tr>
                    </table>
                    <table class="search-area" cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            签证编号：</td>
                                        <td>
                                            <asp:TextBox ID="ViseIdTextBox" runat="server" CssClass="input"></asp:TextBox></td>
                                        <td>
                                            签证名称：</td>
                                        <td>
                                            <asp:TextBox ID="ViseNameTextBox" runat="server" CssClass="input"></asp:TextBox></td>
                                        <td>
                                            经办人：</td>
                                        <td>
                                            <uc1:InputUser ID="VisePersonTextBox" runat="server"></uc1:InputUser>
                                        </td>
                                        <td rowspan="4">
                                            &nbsp;<asp:Button ID="Button1" runat="server" Text="搜 索" CssClass="submit" OnClick="btnQuery_Click" />&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            承 包 商：</td>
                                        <td colspan="3">
                                            <uc1:InputSupplier ID="ViseSupplierTextBox" runat="server"></uc1:InputSupplier>
                                        </td>
                                        <td>
                                            部 门：</td>
                                        <td>
                                            <uc2:InputUnit ID="ViseUnitTextBox" runat="server"></uc2:InputUnit>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            签证日期：</td>
                                        <td colspan="5">
                                            <cc3:Calendar ID="ViseDateStartTextBox" runat="server" CalendarResource="../Images/CalendarResource/"
                                                ReadOnly="False" Display="True" Value="">
                                            </cc3:Calendar>
                                            --&gt;
                                            <cc3:Calendar ID="ViseDateEndTextBox" runat="server" CalendarResource="../Images/CalendarResource/"
                                                ReadOnly="False" Display="True" Value="">
                                            </cc3:Calendar>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            签证期限：</td>
                                        <td colspan="5">
                                            <cc3:Calendar ID="ViseEndDateStartTextBox" runat="server" CalendarResource="../Images/CalendarResource/"
                                                ReadOnly="False" Display="True" Value="">
                                            </cc3:Calendar>
                                            --&gt;
                                            <cc3:Calendar ID="ViseEndDateEndTextBox" runat="server" CalendarResource="../Images/CalendarResource/"
                                                ReadOnly="False" Display="True" Value="">
                                            </cc3:Calendar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                        AllowSorting="True" DataKeyNames="ViseCode" CssClass="list" Width="100%" DataSourceID="ObjectDataSource2"
                        OnRowDataBound="GridView2_RowDataBound" PageSize="30">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <input type="checkbox" id="CheckBoxSelect" runat="server" value='<%# Eval("ViseCode") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ViseId" HeaderText="编号" SortExpression="ViseId" />
                            <asp:TemplateField HeaderText="名称" SortExpression="ViseName">
                                <ItemTemplate>
                                    <a href="#" onclick="javascript:OpenModify('<%# Eval("ViseCode") %>');">
                                        <%# Eval("ViseName") %>
                                    </a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="经办人" SortExpression="VisePerson">
                                <ItemTemplate>
                                    <%# RmsPM.BLL.SystemRule.GetUserName((string)Eval("VisePerson")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ViseEndDate" HeaderText="办理期限" SortExpression="ViseEndDate"
                                DataFormatString="{0:d}" HtmlEncode="False" />
                            <asp:BoundField DataField="ViseDate" HeaderText="签证日期" SortExpression="ViseDate"
                                DataFormatString="{0:d}" HtmlEncode="False" />
                        </Columns>
                        <PagerStyle CssClass="list-title" />
                        <HeaderStyle CssClass="list-title" />
                        <EmptyDataTemplate>
                            无匹配数据

                        </EmptyDataTemplate>
                        <RowStyle HorizontalAlign="Center" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetLocalVises"
                        TypeName="RmsPM.BFL.LocaleViseBFL" EnablePaging="True" MaximumRowsParameterName="MaxRecords"
                        SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord">
                        <SelectParameters>
                            <asp:Parameter Name="SortColumns" Type="String" />
                            <asp:Parameter Name="StartRecord" Type="Int32" />
                            <asp:Parameter Name="MaxRecords" Type="Int32" />
                            <asp:ControlParameter ControlID="ViseIdTextBox" Name="ViseId" PropertyName="Text"
                                Type="String" />
                            <asp:ControlParameter ControlID="ViseNameTextBox" Name="ViseName" PropertyName="Text"
                                Type="String" />
                            <asp:ControlParameter ControlID="VisePersonTextBox" Name="VisePerson" PropertyName="Value"
                                Type="String" />
                            <asp:ControlParameter ControlID="ViseSupplierTextBox" Name="ViseSupplier" PropertyName="Value"
                                Type="String" />
                            <asp:QueryStringParameter Name="ViseContractCode" QueryStringField="ContractCode"
                                Type="String" />
                            <asp:ControlParameter ControlID="ViseUnitTextBox" Name="ViseUnit" PropertyName="Value"
                                Type="String" />
                            <asp:ControlParameter ControlID="ViseDateStartTextBox" Name="ViseDateStart" PropertyName="Value"
                                Type="DateTime" />
                            <asp:ControlParameter ControlID="ViseDateEndTextBox" Name="ViseDateEnd" PropertyName="Value"
                                Type="DateTime" />
                            <asp:ControlParameter ControlID="ViseEndDateStartTextBox" Name="ViseEndDateStart"
                                PropertyName="Value" Type="DateTime" />
                            <asp:ControlParameter ControlID="ViseEndDateEndTextBox" Name="ViseEndDateEnd" PropertyName="Value"
                                Type="DateTime" />
                            <asp:QueryStringParameter Name="ViseProject" QueryStringField="ProjectCode" Type="String" />
                            <asp:Parameter Name="ViseType" Type="String" />
                            <asp:Parameter Name="ViseBalanceStatusInStr" Type="String" DefaultValue="1,2" />
                            <asp:Parameter Name="ViseStatusInStr" Type="String" DefaultValue="3" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:HiddenField ID="HiddenFieldCheckBoxCodes" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <input onclick="SelectCheckBox()" name="btnSave" type="button" id="btnSave" class="submit"
                        value="确 定" /> 
                    <input class="submit" id="btnClear" type="button" value="清 空" onclick="ClearCheckBox();"> 
                    <input class="submit" id="btnCancel" type="button" value="取 消" onclick="CancelCheckBox();"></td>
            </tr>
        </table>
    </form>
</body>

<script language="javascript">
    function OpenModify(Code)
	{
		OpenFullWindow('LocaleViseInfo.aspx?ProjectCode=<%= Request["ProjectCode"]+"" %>&ViseCode='+Code,'现场签证新增');
	}
	function SelectCheckBox()
	{
	    var SelectedViseCodes = "";
        var ViseCodes = document.all("HiddenFieldCheckBoxCodes").value.substr(0,document.all("HiddenFieldCheckBoxCodes").value.length-1).split(',');
		for ( var i=0; i<ViseCodes.length;i++)
		{
			if(ViseCodes[i]!="")
			{
			    if(document.all(ViseCodes[i]).checked)
			    {
			        SelectedViseCodes += document.all(ViseCodes[i]).value+",";
			    }
			}
		}
		returnValue = SelectedViseCodes.substr(0,SelectedViseCodes.length-1);
		close();
	}
	function CancelCheckBox()
	{
	    returnValue = "false";
	    close();
	}
	function ClearCheckBox()
	{
	    returnValue = "";
	    close();
	}
</script>

</html>
