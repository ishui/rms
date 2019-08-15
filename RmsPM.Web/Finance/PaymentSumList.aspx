<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentSumList.aspx.cs" Inherits="Finance_PaymentSumList" %>

<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>付款汇总审批流程</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript" src="../Rms.js"></script>

    <script type="text/javascript">
		function SelectUnit()
		{
			OpenMiddleWindow("../SelectBox/SelectUnit.aspx?UnitCode=000000");
		}
		function SelectUnitReturn(code, name)
		{
			window.document.all.txtUnitName.value = name;
			window.document.all.txtUnit.value = code;
		}	
		
		function OpenRequisition()
        {
		    OpenFullWindow('<%= RmsPM.BLL.WorkFlowRule.GetProcedureURLByName("付款汇总审批流程")%>?Status=new&ProjectCode=<%= Request["ProjectCode"] + ""%>','付款汇总审批流程');
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
                            <td class="topic" background="../images/topic_bg.gif">
                                <img height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span
                                    id="spanTitle">付款汇总审批流程</span></td>
                            <td width="9">
                                <img height="25" src="../images/topic_corr.gif" width="9"></td>
                        </tr>
                    </table>
                    <table class="table" id="tableToolBar" width="100%">
                        <tr>
                            <td class="tools-area" width="16">
                                <img src="../images/btn_li.gif" align="absMiddle"></td>
                            <td class="tools-area">
                                <input name="btnRequisition" id="btnRequisition" type="button" value="汇总审批单发起" class="button"
                                    runat="server" onclick="javascript:OpenRequisition();return false;">
                            </td>
                        </tr>
                    </table>
                    <table class="search-area" cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            汇总审批单号</td>
                                        <td>
                                            <asp:TextBox ID="CollectBillCodeTextBox" CssClass="input" runat="server"></asp:TextBox></td>
                                        <td>
                                            审核日期</td>
                                        <td>
                                            <cc3:Calendar ID="DateStart" runat="server" CalendarResource="../Images/CalendarResource/"
                                                ReadOnly="False" CalendarMode="Date" Display="True" Value="">
                                            </cc3:Calendar>
                                            至<cc3:Calendar ID="DateEnd" runat="server" CalendarResource="../Images/CalendarResource/"
                                                ReadOnly="False" CalendarMode="Date" Display="True" Value="">
                                            </cc3:Calendar>
                                        </td>
                                        <td rowspan="4" align="center">
                                            <input type="button" name="button" value="搜索" class="button" runat="server" id="Button1"
                                                onserverclick="Button1_ServerClick"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            送件公司</td>
                                        <td>
                                            <asp:TextBox ID="SendUnitCodeTextBox" CssClass="input" runat="server"></asp:TextBox></td>
                                        <td>
                                            状态：</td>
                                        <td>
                                            <asp:CheckBoxList ID="cblStatus" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Selected="True">申请</asp:ListItem>
                                                <asp:ListItem Value="2">审核中</asp:ListItem>
                                                <asp:ListItem Value="3">已审</asp:ListItem>
                                                <asp:ListItem Value="4">未通过</asp:ListItem>
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
                        AllowSorting="True" CssClass="list" Width="100%" DataSourceID="ObjectDataSource1">
                        <Columns>
                            <asp:TemplateField HeaderText="汇总审批单号">
                                <ItemTemplate>
                                    
                                    </a><a href="#" onclick="javascript:OpenMiddleWindow('../WorkFlowPage/TC_PaymentSum.aspx?ProcedureCode=100004&frameType=List&CaseCode=<%# RmsPM.BLL.WorkFlowRule.GetCaseCodeByProcedureNameAndApplicationCode("付款汇总审批流程",Eval("Code").ToString()) %>&ApplicationCode=<%# Eval("Code")%>','PaymentSum');return false;">
                                        <%# Eval("CollectBillCode")%>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="送件公司">
                                <ItemTemplate>
                                    <%# Eval("SendUnitCode").ToString()%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="发起日期">
                                <ItemTemplate>
                                    <%# Eval("SumDateTime").ToString()%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="终审日期">
                                <ItemTemplate>
                                    <%# Eval("AuditDateTime").ToString() == "0001-1-1 0:00:00" ? "" : Eval("AuditDateTime").ToString()%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="状态" SortExpression="Status">
                                <ItemTemplate>
                                    <%# GetStatusName((string)Eval("Status")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="list-title" />
                        <HeaderStyle CssClass="list-title" />
                        <EmptyDataTemplate>
                            无匹配数据

                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" MaximumRowsParameterName="MaxRecords"
                        SelectMethod="GetTC_PaymentSumList" SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord"
                        TypeName="RmsPM.BLL.CommomWorkFlowBLL.TC_PaymentSumBFL">
                        <SelectParameters>
                            <asp:Parameter Name="sortColumns" Type="String" />
                            <asp:Parameter Name="startRecord" Type="Int32" />
                            <asp:Parameter Name="maxRecords" Type="Int32" />
                            <asp:ControlParameter ControlID="CollectBillCodeTextBox" Name="CollectBillCodeEqual"
                                PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="SendUnitCodeTextBox" Name="SendUnitCodeEqual" PropertyName="Text"
                                Type="String" />
                            <asp:ControlParameter ControlID="DateStart" Name="AuditDateTimeStartEqual" PropertyName="Value"
                                Type="DateTime" />
                            <asp:ControlParameter ControlID="DateEnd" Name="AuditDateTimeEndEqual" PropertyName="Value"
                                Type="DateTime" />
                            <asp:Parameter Name="StatusEqual" Type="String" />
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
