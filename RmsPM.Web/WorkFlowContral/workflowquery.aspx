<%@ Register TagPrefix="cc2" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Page Language="c#" Inherits="RmsPM.Web.WorkFlowContral.WorkFlowQuery" CodeFile="WorkFlowQuery.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>完成事宜</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../Images/index.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>

</head>
<body style="BORDER-RIGHT: 0px" scroll="no">
    <form id="Form1" method="post" runat="server">
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
                                    id="spanTitle" runat="server"> 流程&nbsp;完成事宜</span></td>
                            <td width="9">
                                <img height="25" src="../images/topic_corr.gif" width="9"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr height="1">
                <td valign="top">
                    <table class="search-area" cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            流程名称：</td>
                                        <td>
                                            <select id="sltProcedure" runat="server" name="sltProcedure">
                                                <option selected value="">---所有流程---</option>
                                            </select>
                                        </td>
                                        <td>
                                            项 &nbsp; &nbsp;&nbsp; 目：</td>
                                        <td>
                                            <asp:DropDownList ID="DropDownProject" runat="server">
                                            </asp:DropDownList></td>
                                        <td></td>
                                        <td></td>
                                        <td rowspan="3"> &nbsp;<input class="submit" id="btnSearch" type="button" value="搜 索" name="btnSearch" runat="server"
                                                onserverclick="btnSearch_ServerClick">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                    <td>
                                        流&nbsp; 水 号：</td>
                                        <td>
                                            <input class="input" type="text" runat="server" id="txtCaseCode" /></td>
                                        <td>
                                            主 &nbsp; &nbsp;&nbsp; 题：</td>
                                        <td>
                                            <input type="text" class="input" runat="server" id="txtTitle" /></td>
                                        <td>
                                            发 &nbsp;件 人：</td>
                                        <td><uc1:InputUser id="ucPerson" runat="server"></uc1:InputUser>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            发件日期：</td>
                                        <td><cc3:calendar id="DateStart" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False" Display="True" Value=""></cc3:calendar></td>
                                        <td align="center">--></td>
                                        <td><cc3:calendar id="DateEnd" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False" Display="True" Value=""></cc3:calendar>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            结束日期：</td>
                                        <td><cc3:calendar id="CalendarStart" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False" Display="True" Value=""></cc3:calendar></td>
                                        <td align="center">--></td>
                                        <td><cc3:calendar id="CalendarEnd" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False" Display="True" Value=""></cc3:calendar>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr height="100%">
                <td class="table" valign="top">
                    <asp:DataGrid ID="dgList" runat="server" CssClass="list" CellPadding="0" GridLines="Horizontal"
                        AutoGenerateColumns="False" PageSize="18" Width="100%" AllowPaging="True" OnSortCommand="dgList_SortCommand" AllowSorting="True">
                        <HeaderStyle CssClass="list-title"></HeaderStyle>
                        <FooterStyle CssClass="list-title"></FooterStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="流水号">
                                <ItemTemplate>
                                    <%# RmsPM.BLL.WorkFlowRule.GetWorkFlowNumber(DataBinder.Eval(Container, "DataItem.CaseCode").ToString()) %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="项目简称">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# RmsPM.BLL.WorkFlowRule.GetWorkFlowCaseProjectName(DataBinder.Eval(Container, "DataItem.CaseCode").ToString()) %>'
                                        ID="Label1">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="审批类别">
                                <HeaderStyle Wrap="False"></HeaderStyle>
                                <ItemStyle Wrap="False"></ItemStyle>
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "Description") %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="主题">
                                <ItemTemplate>
                                    <a href="##" onclick='gotoDirect("<%# DataBinder.Eval(Container.DataItem, "ApplicationPath") %>","<%# DataBinder.Eval(Container.DataItem, "CaseCode") %>","<%# DataBinder.Eval(Container.DataItem, "ApplicationCode") %>"); return false;'>
                                        <%# RmsPM.BLL.WorkFlowRule.GetWorkFlowCaseTitle(DataBinder.Eval(Container, "DataItem.CaseCode").ToString()) %>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="发件人" SortExpression="SourceUserCode">
                                <HeaderStyle Wrap="False"></HeaderStyle>
                                <ItemStyle Wrap="False"></ItemStyle>
                                <ItemTemplate>
                                    <%# RmsPM.BLL.SystemRule.GetUserName( DataBinder.Eval(Container.DataItem, "SourceUserCode").ToString()) %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="发件日期" SortExpression="CreateDate">
                                <HeaderStyle Wrap="False"></HeaderStyle>
                                <ItemStyle Wrap="False"></ItemStyle>
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "CreateDate", "{0:yyyy-MM-dd HH:mm}")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="结束日期" SortExpression="FinishDate">
                                <HeaderStyle Wrap="False"></HeaderStyle>
                                <ItemStyle Wrap="False"></ItemStyle>
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "FinishDate", "{0:yyyy-MM-dd HH:mm}")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="完成日期">
                                <HeaderStyle Wrap="False"></HeaderStyle>
                                <ItemStyle Wrap="False"></ItemStyle>
                                <ItemTemplate>
                                   <%# RmsPM.BLL.WorkFlowRule.GetWorkFlowCaseTime(DataBinder.Eval(Container, "DataItem.CaseCode").ToString())%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="手送资料">
                                    <ItemTemplate>
                                        <%# RmsPM.BLL.WorkFlowRule.GetWorkFlowHandmade(DataBinder.Eval(Container, "DataItem.CaseCode").ToString()) %>
                                    </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="版本">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# RmsPM.BLL.WorkFlowRule.GetProcedureVersionByCode((string)DataBinder.Eval(Container, "DataItem.ProcedureCode")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="版本说明">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# RmsPM.BLL.WorkFlowRule.GetProcedureVersionDescriptionByCode((string)DataBinder.Eval(Container, "DataItem.ProcedureCode")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle Visible="False" NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页"
                            HorizontalAlign="Right" CssClass="ListHeadTr" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                    <cc2:GridPagination ID="GridPagination1" runat="server" DataGridId="dgList" ControlSourceUrl="../Images/GridPaginationSource/"
                        OnPageIndexChange="GridPagination1_PageIndexChange"></cc2:GridPagination>
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

    <script>
	function gotoDirect ( path , CaseCode,ApplicationCode)
	{
		OpenFullWindow(  path + ((path.indexOf("?")>0)?"&":"?")+"CaseCode=" + CaseCode +"&ApplicationCode="+ApplicationCode+"&frameType=List",'流程处理');
	}
    </script>

</body>
</html>
