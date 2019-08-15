<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RS_ScoreForManagerEdit.aspx.cs" Inherits="RmsOA_RS_ScoreForManagerEdit" %>

<%@ Register Src="../WorkFlowControl/workflowtoolbar.ascx" TagName="workflowtoolbar"
    TagPrefix="uc5" %>
<%@ Register Src="../WorkFlowControl/WorkFlowList.ascx" TagName="WorkFlowList" TagPrefix="uc4" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../Images/index.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../Rms.js" type="text/javascript"></script>

    <title>��ֹ���/��Ա�����</title>
</head>
<body>

    <script language="javascript" type="text/javascript">		
		function OpenRequisition()
        {
		    OpenFullWindow('<%= RmsPM.BLL.WorkFlowRule.GetProcedureURLByName("���ž���������")%>?FKCode=<%= Request["FKCode"] %>&UserCode=<%= Request["UserCode"] %>','Ա����ֹ���');
        }
    </script>

    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td background="../images/topic_bg.gif" class="topic" style="height: 25px; text-align: center;">
                        ��ֹ���/�����ž�����</td>
                </tr>
            </table>
        </div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="40">
                    <img alt="" height="35" src="../Images/12-7-1.gif" width="40" /></td>
                <td background="../Images/12-7-2.gif">
                    <span><span style="color: #FAA210; font-weight: bolder;">
                        <% GetYear(); %>
                    </span>��<span style="color: #FAA210; font-weight: bolder;"><% GetMonth(); %></span>��
                        �߿Ƽ��� ���ž�����</span></td>
                <td width="15px;">
                    <img height="35" src="../Images/12-7-3.gif" width="15" /></td>
            </tr>
        </table>
        <table id="tableToolBar" class="table" width="100%">
            <tr>
                <td class="tools-area" width="16">
                    <img align="absMiddle" src="../images/btn_li.gif"></td>
                <td class="tools-area">
                    <input id="btnRequisition" runat="server" class="button" onclick="Javascript:OpenRequisition();return false;"
                        type="button" value="�ύ" />
                    <input class="button" onclick="self.close();" type="button" value="�ر�" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="list"
            DataSourceID="ObjectDataSource1" OnRowEditing="GridView1_RowEditing"
            Width="100%">
            <HeaderStyle CssClass="list-title" HorizontalAlign="Center" />
            <RowStyle HorizontalAlign="Center" />
            <Columns>
                <asp:TemplateField HeaderText="���" SortExpression="Index">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Index") %>'></asp:Label>
                        <input id="HidCode" runat="server" type="hidden" value='<%# Bind("Code") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Index") %>'></asp:Label>
                        <input id="HidCode" runat="server" type="hidden" value='<%# Bind("Code") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="����" SortExpression="UserName">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                        <input id="HidName" runat="server" type="hidden" value='<%# Bind("UserCode") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="���˷���" SortExpression="Score">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="input" Text='<%# Bind("Score") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Score") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="�༭">
                    <EditItemTemplate>
                        <asp:Button ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                            CssClass="button" Text="����" />
                        <asp:Button ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                            CssClass="button" Text="ȡ��" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Button ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit"
                            CssClass="button" Text="�༭" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <table border="0" cellpadding="0" cellspacing="0" width="470">
            <tr id="webtabs">
                <td width="20">
                </td>
                <td id="workflowmsg" runat="server" class="TabDisplay" width="185">
                    �������</td>
            </tr>
        </table>
        <table id="tabdiv" border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <uc4:WorkFlowList ID="WorkFlowList1" runat="server" />
                </td>
            </tr>
        </table>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsOA.MODEL.EmployViewModel"
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetManageScoresByFKCode"
            TypeName="RmsOA.BFL.RS_ScoreManageBFL" UpdateMethod="UpDateScores">
            <SelectParameters>
                <asp:QueryStringParameter Name="FKCode" QueryStringField="FKCode" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
