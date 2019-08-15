<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileTemplateVersionList.aspx.cs"
    Inherits="RmsDM_FileTemplateVersionList" %>

<%@ Register Src="../UserControls/attachmentlist.ascx" TagName="attachmentlist" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>文档模板版本</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet"/>

    <script language="javascript" src="../Rms.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
    function AddDocument()
    {
	    OpenMiddleWindow("FileTemplateVersionModify.aspx?FormType=Add&Tempcode="+'<%=Request["code"]%>', "FilTemplateVersionModify");
    }
	
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td background="../images/topic_bg.gif" class="topic" style="height: 25px; text-align: center;">
                    版本列表</td>
            </tr>
        </table>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" DataObjectTypeName="RmsDM.MODEL.FileTemplateModel"
            DeleteMethod="Delete" SelectMethod="GetFileTemplateListOne" TypeName="RmsDM.BFL.FileTemplateBFL"
            UpdateMethod="Update">
            <SelectParameters>
                <asp:QueryStringParameter Name="Code" QueryStringField="code" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource2" OnDataBound="FormView1_DataBound"
            Width="100%">
            <EditItemTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td class="tools-area" valign="top">
                            <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                CssClass="button" OnClick="UpdateButton_Click" Text="更新" />
                            <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                CssClass="button" Text="取消" />
                        </td>
                    </tr>
                </table>
                <table class="form" width="100%">
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            文档类别名称：</td>
                        <td colspan="3">
                            <asp:Label ID="lblSortName" runat="server"></asp:Label>
                            <asp:DropDownList ID="SortDropDownList" runat="server" Font-Size="9pt">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td align="left" class="form-item" width="120">
                            模版名称：
                        </td>
                        <td>
                            <asp:TextBox ID="FileTemplateNameTextBox" runat="server" CssClass="input" Font-Size="9pt"
                                Text='<%# Bind("FileTemplateName") %>'></asp:TextBox>
                            <span style="color: Red;">*</span>
                        </td>
                        <td align="left" class="form-item" style="width: 120px;">
                            质量记录分类号：</td>
                        <td>
                            <asp:TextBox ID="SortCodeTextBox" runat="server" CssClass="input" Text='<%#Bind("SortCode") %>'></asp:TextBox><span
                                style="color: Red;">*</span>
                        </td>
                    </tr>
                </table>
            </EditItemTemplate>
            <ItemTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="tools-area" valign="top">
                            <input id="btnAdd" runat="server" class="button" name="btnAdd" onclick="AddDocument('')"
                                type="button" value="新增模版版本" />
                            <asp:Button ID="EditButton" runat="server" CssClass="button" OnClick="EditButton_Click"
                                Text="编辑" />
                            <input class="button" onclick="window.close()" type="button" value="关闭" />
                        </td>
                    </tr>
                </table>
                <table class="form" width="100%">
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            文档类别名称：</td>
                        <td colspan="3">
                            <asp:Label ID="lblSortName" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="form-item" style="width: 120px;">
                            模版名称:
                        </td>
                        <td>
                            <asp:Label ID="FileTemplateNameLabel" runat="server" Text='<%# Bind("FileTemplateName") %>'>
                            </asp:Label>
                        </td>
                        <td class="form-item" style="width: 120px;">
                            质量记录分类号：</td>
                        <td>
                            <asp:Label ID="SortCodeLabel" runat="server" Text='<%#Bind("SortCode") %>'>'></asp:Label></td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:FormView>
    
    </div>
        <div>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetFileTemplateVersionList"
                TypeName="RmsDM.BFL.FileTemplateVersionBFL" SortParameterName="SortColumns"
                StartRowIndexParameterName="StartRecord" MaximumRowsParameterName="MaxRecords">
                <SelectParameters>
                    <asp:Parameter Name="SortColumns" Type="String" />
                    <asp:Parameter DefaultValue="0" Name="StartRecord" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="MaxRecords" Type="Int32" />
                    <asp:Parameter DefaultValue="" Name="CodeEqual" Type="Int32" />
                    <asp:QueryStringParameter Name="FileTemplateCodeEqual" QueryStringField="code" Type="Int32" />
                    <asp:Parameter Name="WorkFlowProcedureNameEqual" Type="String" />
                    <asp:Parameter Name="VersionNumberEqual" Type="String" />
                    <asp:Parameter Name="IsPigeonholeEqual" Type="String" />
                    <asp:Parameter Name="PigeonholeTimeEqual" Type="String" />
                    <asp:Parameter Name="SaveTermEqual" Type="String" />
                    <asp:Parameter Name="RecordKindEqual" Type="String" />
                    <asp:Parameter Name="MarkingSNRuleEqual" Type="String" />
                    <asp:Parameter Name="IsAvailabilityEqual" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            </div>
        <table width="100%" class="form" border="0">
            <tr>
                <td align="left" style="background-color: #FFEBCD;">
                    模版版本列表</td>
            </tr>
        </table>
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1"
                Width="100%" CssClass="list">
                <HeaderStyle CssClass="list-title"></HeaderStyle>
                <FooterStyle CssClass="list-title"></FooterStyle>
                <Columns>
                    <asp:TemplateField HeaderText="版本名称" SortExpression="VersionNumber">
                        <ItemTemplate>
                            <a href="#" onclick="OpenMiddleWindow('FileTemplateVersionModify.aspx?FormType=Edit&Code=<%# Eval("Code") %>&TempCode=<%=Request["code"] %>','FileTemplateVersionModify')">
                                <%# Eval("VersionNumber")%>
                            </a>
                        </ItemTemplate>
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="MarkingSNRule" HeaderText="编码规则" SortExpression="MarkingSNRule">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="IsAvailability" HeaderText="有效状态" SortExpression="IsAvailability">
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="IsPigeonhole" HeaderText="是否归档" SortExpression="IsPigeonhole">
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PigeonholeTime" HeaderText="归档时间" SortExpression="PigeonholeTime">
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SaveTerm" HeaderText="保存时间" SortExpression="SaveTerm">
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="附件列表">
                        <ItemTemplate>
                            <uc1:attachmentlist ID="Attachmentlist1" runat="server" AttachMentType="FileTemplateVersion"
                                MasterCode='<%# Eval("Code") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="WorkFlowProcedureName" HeaderText="工作流程" SortExpression="WorkFlowProcedureName">
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
