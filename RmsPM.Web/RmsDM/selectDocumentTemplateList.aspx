<%@ Page Language="C#" AutoEventWireup="true" CodeFile="selectDocumentTemplateList.aspx.cs"
    Inherits="SelectBox_selectDocumentTemplateList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>文件模板列表</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet">

    <script language="javascript" type="text/javascript">
        function ChooseTemplate(Code,TemplateName)
        {            
            window.parent.opener.<%=ViewState["ReturnFunc"]%>(Code,TemplateName);
			window.parent.close();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table id="Table1" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="table" valign="top">
                        <table width="100%" height="100%">
                            <tr height="100%">
                                <td valign="top" align="left" style="height: 100%">
                                    
                                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetFileTemplateList"
                                            TypeName="RmsDM.BFL.FileTemplateBFL" SortParameterName="SortColumns" StartRowIndexParameterName="StarRecord"
                                            MaximumRowsParameterName="MaxRecords">
                                            <SelectParameters>
                                                <asp:Parameter Name="SortColumns" Type="String" />
                                                <asp:Parameter Name="StartRecord" Type="Int32" DefaultValue="0" />
                                                <asp:Parameter Name="MaxRecords" Type="Int32" DefaultValue="-1" />
                                                <asp:QueryStringParameter Name="NodeCode" QueryStringField="NodeValue" Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" Width="100%" CssClass="list" PageSize="15" AllowPaging="True">
                                            <HeaderStyle CssClass="list-title"></HeaderStyle>
                                            <FooterStyle CssClass="list-title"></FooterStyle>
                                            <Columns>
                                                <asp:TemplateField Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCode" runat="server" Text='<%# Eval("Code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFileTemplateName" runat="server" Text='<%# Eval("FileTemplateName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="模板名称" SortExpression="FileTemplateName">
                                                    <ItemTemplate>
                                                        <a href="#" onclick="ChooseTemplate('<%#Eval("Code") %>','<%#Eval("FileTemplateName") %>')">
                                                            <%# Eval("FileTemplateName") %>
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="操作" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="SelectCheckBox" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%" id="Table3" class="table">
                            <tr>
                                <td align="center">
                                    <asp:Button ID="SelectButton" runat="server" CssClass="button" Text="确定" OnClick="SelectButton_Click" />
                                    <input id="CloseButton" type="button" value="关闭" onclick="javascript:window.parent.close();"
                                        runat="server" class="button" />
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
