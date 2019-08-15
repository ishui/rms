<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TY_OA_ManagerTaskList.aspx.cs"
    Inherits="RmsOA_TY_OA_ManagerTaskList" %>

<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>总经理交办事宜</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet">
    <link href="../Images/infra.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>

    <script language="javascript">
function OpenModify(Code)
{   
	OpenLargeWindow('TY_OA_MgrTaskInfo.aspx?MgrCode='+Code,'总经理交办事宜维护');
}
function OpenNew()
{    
    OpenLargeWindow('TY_OA_MgrTaskInfo.aspx','总经理交办事宜新增');
}
    </script>

</head>
<body bgcolor="#ffffff" style="border-right: 0px">
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
                                <img height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">
                                <span id="spanTitle">总经理审批列表</span></td>
                            <td width="9">
                                <img height="25" src="../images/topic_corr.gif" width="9"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tools-area" valign="top">
                    <img src="../images/btn_li.gif" align="absMiddle">
                    <input name="btnNew" id="btnNew" type="button" value=" 新增审批单 " class="button" runat="server"
                        onclick="javascript:OpenNew();">
                </td>
            </tr>
            <tr>
                <td class="table" valign="top">
                    <table height="100%" width="100%">
                        <tr>
                            <td>
                                <table width="100%" class="search-area" cellspacing="0" cellpadding="0" border="0"
                                    onkeydown="SearchKeyDown();">
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td nowrap>
                                                        任务主题：</td>
                                                    <td>
                                                        <asp:TextBox ID="TaskNameID" runat="server" CssClass="input"></asp:TextBox></td>
                                                    <td nowrap>
                                                        交办日期：</td>
                                                    <td>
                                                        <cc3:Calendar ID="CreateDateIDStart" runat="server" CalendarResource="../Images/CalendarResource/"
                                                            ReadOnly="False" Display="True" Value="">
                                                        </cc3:Calendar>
                                                        -->
                                                        <cc3:Calendar ID="CreateDateIDEnd" runat="server" CalendarResource="../Images/CalendarResource/"
                                                            ReadOnly="False" Display="True" Value="">
                                                        </cc3:Calendar>
                                                    </td>
                                                    <td nowrap>
                                                        <input class="submit" id="btnSearch" type="button" value="搜索" name="btnSearch" runat="server"
                                                            onserverclick="btnSearch_ServerClick">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td nowrap>
                                                        审核状态：</td>
                                                    <td>
                                                        <asp:CheckBoxList ID="StateID" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1" Text="待审"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="已审">
                                                            </asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap>
                                                        任务事由：</td>
                                                    <td colspan="5">
                                                        <asp:TextBox ID="TaskDetailID" runat="server" CssClass="input" Width="70%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr height="100%">
                            <td valign="top">
                                <div style="overflow: auto; width: 100%; position: absolute; height: 100%">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                        AllowSorting="True" CssClass="list" PageSize="12" Width="100%" DataSourceID="ObjectDataSource1"
                                        GridLines="Horizontal">
                                        <Columns>
                                            <asp:TemplateField HeaderText="任务主题" SortExpression="TaskName">
                                                <ItemTemplate>
                                                    <a href="#" onclick="javascript:OpenModify('<%# Eval("Code") %>');return false;">
                                                        <%# Eval("TaskName")%>
                                                    </a>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="交办日期" SortExpression="CreateDate">
                                                <ItemTemplate>
                                                    <%#RmsPM.BLL.StringRule.ShowDate(Eval("CreateDate"))%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="要求完成日期" SortExpression="DeadLine">
                                                <ItemTemplate>
                                                    <%#RmsPM.BLL.StringRule.ShowDate(Eval("DeadLine"))%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            
                                            <asp:BoundField DataField="TaskDetail" HeaderText="交办事由" SortExpression="TaskDetail"  ItemStyle-HorizontalAlign="Left"/>
                                            <asp:TemplateField HeaderText="审核状态" SortExpression="State">
                                                <ItemTemplate>
                                                    <%#RmsOA.BLL.TY_OA_MgrTaskBLL.GetStatusName(Eval("State").ToString())%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="TaskTail" HeaderText="TaskTail" SortExpression="TaskTail"
                                                Visible="False" />
                                            <asp:BoundField DataField="MgrTaskID" HeaderText="MgrTaskID" SortExpression="MgrTaskID"
                                                Visible="False" />
                                            <asp:BoundField DataField="ReferLink" HeaderText="ReferLink" SortExpression="ReferLink"
                                                Visible="False" />
                                            <asp:BoundField DataField="IsFinish" HeaderText="是否完成" SortExpression="IsFinish"
                                                Visible="False" />
                                            <asp:BoundField DataField="CreateMan" HeaderText="CreateMan" SortExpression="CreateMan"
                                                Visible="False" />
                                            <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" Visible="False" />
                                        </Columns>
                                        <PagerStyle CssClass="list-title" />
                                        <HeaderStyle CssClass="list-title" />
                                        <EmptyDataTemplate>
                                            无匹配数据
                                        </EmptyDataTemplate>
                                        <RowStyle HorizontalAlign="Center" />
                                    </asp:GridView>
                                    <asp:ObjectDataSource ID="ObjectDataSource1" TypeName="RmsOA.BFL.TY_OA_MgrTaskBFL"
                                        runat="server" DataObjectTypeName="RmsOA.MODEL.TY_OA_MgrTaskModel" OldValuesParameterFormatString="original_{0}"
                                        SelectMethod="GetTY_OA_MgrTaskList" UpdateMethod="Update" OnSelected="ObjectDataSource1_Selected"
                                        EnablePaging="True" MaximumRowsParameterName="MaxRecords" SortParameterName="SortColumns"
                                        StartRowIndexParameterName="StartRecord" InsertMethod="Insert" DeleteMethod="Delete">
                                        <SelectParameters>
                                            <asp:Parameter Name="SortColumns" Type="String" />
                                            <asp:Parameter Name="StartRecord" Type="Int32" />
                                            <asp:Parameter Name="MaxRecords" Type="Int32" />
                                            <asp:Parameter Name="CodeEqual" Type="Int32" />
                                            <asp:Parameter Name="MgrTaskIDEqual" Type="String" />
                                            <asp:ControlParameter ControlID="lbltblist" Name="StateEqual" PropertyName="Text"
                                                Type="String" />
                                            <asp:ControlParameter ControlID="TaskNameID" Name="TaskNameEqual" PropertyName="Text"
                                                Type="String" />
                                            <asp:ControlParameter ControlID="TaskDetailID" Name="TaskDetailEqual" PropertyName="Text"
                                                Type="String" />
                                            <asp:Parameter Name="IsFinishEqual" Type="String" />
                                            <asp:Parameter Name="TaskTailEqual" Type="String" />
                                            <asp:Parameter Name="CreateDateEqual" Type="DateTime" />
                                            <asp:ControlParameter ControlID="CreateDateIDStart" Name="CreateDateRange1" PropertyName="Value"
                                                Type="String" />
                                            <asp:ControlParameter ControlID="CreateDateIDEnd" Name="CreateDateRange2" PropertyName="Value"
                                                Type="String" />
                                            <asp:Parameter Name="CreateManEqual" Type="String" />
                                            <asp:Parameter Name="ReferLinkEqual" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                    <table width="100%" class="list">
                                        <tr class="list-title">
                                            <td style="height: 23px">
                                                共
                                                <asp:Label runat="server" ID="lblRecordCount">0</asp:Label>
                                                条
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td bgcolor="#e4eff6" height="6">
                </td>
            </tr>
            <asp:Label runat="server" ID="lbltblist" Visible="false"></asp:Label>
        </table>

        <script language="javascript">
function SearchKeyDown()
{
	if(event.keyCode==13)
	{
		event.keyCode = 9;
		form1.btnSearch.click();
	}
}
//SetAdvSearch();
        </script>

    </form>
</body>
</html>
