<%@ Page Language="C#" AutoEventWireup="true" CodeFile="selectindesignchange.aspx.cs" Inherits="SelectBox_selectindesignchange" %>



<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc2" TagName="InputUnit" Src="../UserControls/InputUnit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputSupplier" Src="../UserControls/InputSupplier.ascx" %>
<%@ Register TagPrefix="cc4" Namespace="Rms.ControlLb" Assembly="RmsPM.BLL" %>
<%@ Register Src="../UserControls/inputcontract.ascx" TagName="inputcontract" TagPrefix="uc3" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>选择设计变更</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../Rms.js"></script>
    <script>
	function doViewSupplierInfo(code)
    {
        OpenFullWindow('../Supplier/SupplierInfo.aspx?SupplierCode=' + code   ,"供应商信息");
    }
    function SelectDesignChange(code, type, name)
    {
       var flag = '<%=Request.QueryString["Flag"]%>';
       //alert(<%=ViewState["ReturnFunc"]%>);
       window.opener.<%=ViewState["ReturnFunc"]%>(code, type, name,flag);
       window.close();
    }
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td bgcolor="#e4eff6" height="6">
                </td>
            </tr>
            <tr>
                <td height="25">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">选择关联的内部设计变更</td>
				</tr>
                    </table>
                    <table class="search-area" cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            编 &nbsp; &nbsp; 号：</td>
                                        <td>
                                            <asp:TextBox ID="txtNumber" runat="server" CssClass="input"></asp:TextBox></td>
                                        <td>
                                            工程名称：</td>
                                        <td>
                                            <asp:TextBox ID="txtSolution" runat="server" CssClass="input"></asp:TextBox></td>
                                        <td>
                                            经办人：</td>
                                        <td>
                                            <uc1:InputUser ID="txtPerson" runat="server"></uc1:InputUser>
                                        </td>
                                        <td rowspan="5">
                                            &nbsp;<asp:Button ID="btnQuery" runat="server" Text="搜 索" CssClass="submit" OnClick="btnQuery_Click" />&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            承 包 商：</td>
                                        <td>
                                            <uc1:InputSupplier ID="txtSupplier" runat="server"></uc1:InputSupplier>
                                        </td>
                                        <td>
                                            合同相关：</td>
                                        <td colspan="3">
                                            <uc3:inputcontract ID="txtContract" runat="server" ImagePath="../Images/" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            部 &nbsp; &nbsp; 门：</td>
                                        <td>
                                            <uc2:InputUnit ID="txtUnit" runat="server"></uc2:InputUnit>
                                        </td>
                                        <td>
                                            变更日期：</td>
                                        <td colspan="3">
                                            <cc3:Calendar ID="DateStartTextBox" runat="server" CalendarResource="../Images/CalendarResource/"
                                                ReadOnly="False" Display="True" Value="">
                                            </cc3:Calendar>
                                            --&gt;
                                            <cc3:Calendar ID="DateEndTextBox" runat="server" CalendarResource="../Images/CalendarResource/"
                                                ReadOnly="False" Display="True" Value="">
                                            </cc3:Calendar>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            状 &nbsp; &nbsp; 态：</td>
                                        <td colspan="5">
                                            <asp:CheckBoxList ID="StatusCheckBoxList" runat="server" RepeatColumns="4">
                                                <asp:ListItem Value="0">待审</asp:ListItem>
                                                <asp:ListItem Value="1">审核中</asp:ListItem>
                                                <asp:ListItem Value="2">已审</asp:ListItem>
                                                <asp:ListItem Value="3">作废</asp:ListItem>
                                            </asp:CheckBoxList></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr height="100%">
                <td class="table" valign="top">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                        AllowSorting="True" CssClass="list" Width="100%" DataSourceID="ObjectDataSource1" OnDataBinding="GridView1_DataBinding">
                        <Columns>
                            <asp:TemplateField HeaderText="编号" SortExpression="Number">
                                <ItemTemplate>
                                <a href="#" onclick="javascript:SelectDesignChange('<%# Eval("Code") %>','<%# Eval("Type") %>','<%# Eval("SolutionName") %>');">
                                        <%# Eval("Number") %>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SolutionName" HeaderText="工程" SortExpression="SolutionName" />
                            <asp:TemplateField HeaderText="经办人" SortExpression="Person">
                                <ItemTemplate>
                                    <%# RmsPM.BLL.SystemRule.GetUserName((string)Eval("Person")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="经办部门" SortExpression="Unit">
                                <ItemTemplate>
                                    <%# RmsPM.BLL.SystemRule.GetUnitFullName((string)Eval("Unit")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Date" DataFormatString="{0:d}" HeaderText="变更日期" HtmlEncode="False"
                                SortExpression="Date" />
                            <asp:TemplateField HeaderText="承包商" SortExpression="Supplier">
                                <ItemTemplate>
                                    <a href="#" onclick="javascript:doViewSupplierInfo('<%# Eval("Supplier") %>');return false;">
                                            <%# RmsPM.BLL.ProjectRule.GetSupplierName((string)Eval("Supplier")) %>
                                        </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="状态" SortExpression="State">
                                <ItemTemplate>
                                    <%# RmsPM.BFL.DesignChangeBFL.GetStateNameByCode(Eval("State").ToString()) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="类型" SortExpression="Type">
                                <ItemTemplate>
                                    <%# (Eval("Type").ToString()=="0")?"外部":"内部" %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="list-title" />
                        <HeaderStyle CssClass="list-title" />
                        <EmptyDataTemplate>
                            无匹配数据

                        </EmptyDataTemplate>
                        <RowStyle HorizontalAlign="Center" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetDesignChangeList"
                        TypeName="RmsPM.BFL.DesignChangeBFL" EnablePaging="True" MaximumRowsParameterName="MaxRecords"
                        SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord" OldValuesParameterFormatString="original_{0}">
                        <SelectParameters>
                            <asp:Parameter Name="SortColumns" Type="String" />
                            <asp:Parameter Name="StartRecord" Type="Int32" />
                            <asp:Parameter Name="MaxRecords" Type="Int32" />
                            <asp:ControlParameter ControlID="txtSolution" Name="SolutionNameLike" PropertyName="Text"
                                Type="String" />
                            <asp:QueryStringParameter Name="ProjectCodeEqual" QueryStringField="ProjectCode"
                                Type="String" />
                            <asp:ControlParameter ControlID="txtNumber" Name="NumberEqual" PropertyName="Text"
                                Type="String" />
                            <asp:ControlParameter ControlID="txtPerson" Name="PersonEqual" PropertyName="Value"
                                Type="String" />
                            <asp:ControlParameter ControlID="txtUnit" Name="UnitEqual" PropertyName="Value" Type="String" />
                            <asp:ControlParameter ControlID="txtSupplier" Name="SupplierEqual" PropertyName="Value"
                                Type="String" />
                            <asp:Parameter Name="DateStart" Type="DateTime" />
                            <asp:Parameter Name="DateEnd" Type="DateTime" />
                            <asp:Parameter DefaultValue="1" Name="TypeEqual" Type="String" />
                            <asp:Parameter Name="StateInStr" Type="String" />
                            <asp:Parameter Name="ChangeType" Type="String" />
                            <asp:Parameter Name="ReferCode" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
            <tr>
                <td height="12">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td background="../images/corl_bg.gif">
                                <img height="12" src="../images/corl.gif" width="12"></td>
                            <td width="12">
                                <img height="12" src="../images/corr.gif" width="12"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td bgcolor="#e4eff6" height="6">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
