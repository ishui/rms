<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocumentFileList.aspx.cs"
    Inherits="RmsDM_DocumentFileList" %>

<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>文件列表</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet">

    <script type="text/javascript" language="javascript" src="../Rms.js"></script>

    <script type="text/javascript" language="javascript">

		function OpenRequisition()
        {
		    OpenLargeWindow('DocumentFileModify.aspx?DirectorCode=<%= Request["NodeValue"] %>','DocumentFileModify');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table id="Table1" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="tools-area" valign="top">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="table" valign="top">
                        <table class="table" id="tableToolBar" width="100%">
                            <tr>
                                <td class="tools-area" width="16">
                                    <img src="../images/btn_li.gif" align="absMiddle"></td>
                                <td class="tools-area">
                                    <input name="btnNew" id="NewDocumentFile" type="button" value=" 新增 " class="button"
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
                                                主题</td>
                                            <td>
                                                <asp:TextBox ID="SubjectTextBox" runat="server" CssClass="input"></asp:TextBox></td>
                                            <td>
                                                文号</td>
                                            <td>
                                                <asp:TextBox ID="FileCodeTextBox" runat="server" CssClass="input"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                标识序列号</td>
                                            <td>
                                                <asp:TextBox ID="DoucmentMarkingSNTextBox" runat="server" CssClass="input"></asp:TextBox></td>
                                            <td>
                                                归档日期</td>
                                            <td>
                                                <cc3:Calendar ID="AarchiveDateStart" runat="server" CalendarResource="../Images/CalendarResource/"
                                                    ReadOnly="False" CalendarMode="Date" Display="True" Value="">
                                                </cc3:Calendar>
                                                至<cc3:Calendar ID="ArchiveDateEnd" runat="server" CalendarResource="../Images/CalendarResource/"
                                                    ReadOnly="False" CalendarMode="Date" Display="True" Value="">
                                                </cc3:Calendar>
                                            </td>
                                            <td rowspan="4" align="center">
                                                <input type="button" name="button" value="搜索" class="button" runat="server" id="ButtonSearch" onserverclick="ButtonSearch_ServerClick"
                                                    ></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" height="100%">
                            <tr height="100%">
                                <td valign="top" align="left" style="height: 100%">
                                    <div style="overflow: auto; position: absolute; width: 100%; height: 100%">
                                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetDocumentFileList"
                                            TypeName="RmsDM.BFL.DocumentFileBFL" SortParameterName="SortColumns" StartRowIndexParameterName="StartRecord"
                                            MaximumRowsParameterName="MaxRecords">
                                            <SelectParameters>
                                                <asp:Parameter Name="SortColumns" Type="String" />
                                                <asp:Parameter DefaultValue="0" Name="StartRecord" Type="Int32" />
                                                <asp:Parameter DefaultValue="-1" Name="MaxRecords" Type="Int32" />
                                                <asp:QueryStringParameter Name="NodeCode" QueryStringField="NodeValue" Type="String" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1"
                                            AllowPaging="True" Width="100%" CssClass="list">
                                            <HeaderStyle CssClass="list-title"></HeaderStyle>
                                            <FooterStyle CssClass="list-title"></FooterStyle>
                                            <Columns>
                                                <asp:TemplateField HeaderText="主题" SortExpression="Subject">
                                                    <ItemTemplate>
                                                        <a href="#" onclick="OpenLargeWindow('DocumentFileModify.aspx?DocumentFileCode=<%# Eval("Code") %>','DocumentFileModify')">
                                                            <%# Eval("Subject") %>
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="FileCode" HeaderText="文号" SortExpression="FileCode" />
                                                <asp:BoundField DataField="DoucmentMarkingSN" HeaderText="标识序列号" SortExpression="DoucmentMarkingSN" />
                                                <asp:BoundField DataField="OperationType" HeaderText="业务类别" SortExpression="OperationType" />
                                                <asp:TemplateField HeaderText="申请人" SortExpression="ApplyUserCode">
                                                    <ItemTemplate>
                                                        <%# WebFunctionRule.GetUserNameByCode((Eval("ApplyUserCode")).ToString())%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="申请部门" SortExpression="ApplyDepartmentCode">
                                                    <ItemTemplate>
                                                        <%# RmsPM.BLL.SystemRule.GetUnitName((Eval("ApplyDepartmentCode")).ToString())%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ArchiveState" HeaderText="归档状态" SortExpression="ArchiveState" />
                                                <asp:BoundField DataField="ArchiveType" HeaderText="归档类型" SortExpression="ArchiveType" />
                                                <asp:BoundField DataField="ArchiveDatetime" HeaderText="归档日期" SortExpression="ArchiveDatetime" />
                                                <asp:BoundField DataField="AuditingState" HeaderText="审批状态" SortExpression="AuditingState" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td valign="bottom" class="note" style="height: 21px">
                                    <asp:Label ID="lbTitle" runat="server" CssClass="TitleText"></asp:Label><asp:Label
                                        ID="lblDocumentTypeName" runat="server" CssClass="Label"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" EnablePaging="True" MaximumRowsParameterName="MaxRecords"
                                        SelectMethod="GetDocumentFileListByCondition" SortParameterName="SortColumns"
                                        StartRowIndexParameterName="StartRecord" TypeName="RmsDM.BFL.DocumentFileBFL">
                                        <SelectParameters>
                                            <asp:Parameter Name="sortColumns" Type="String" />
                                            <asp:Parameter Name="startRecord" Type="Int32" />
                                            <asp:Parameter Name="maxRecords" Type="Int32" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
