<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RS_ScoreForEmploy.aspx.cs" Inherits="RmsOA_RS_ScoreForEmploy" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../Images/index.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../Rms.js" type="text/javascript"></script>

    <title>��ֹ���/��Ա�����</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td background="../images/topic_bg.gif" class="topic" style="height: 25px; text-align: center;">
                        ��ֹ���/��Ա�����</td>
                </tr>
            </table>
        </div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="40">
                    <img alt="" height="35" src="../Images/12-7-1.gif" width="40" /></td>
                <td background="../Images/12-7-2.gif">
                    <span><span style="color: #FAA210; font-weight:bolder;">
                        <% GetYear(); %>
                    </span>��<span style="color: #FAA210; font-weight: bolder;"><% GetMonth(); %></span>�� �߿Ƽ��� <span
                        style="color: #FAA210; font-weight: bolder;">
                        <% GetDept(); %>
                    </span>����Ա�����</span></td>
                <td width="15px;">
                    <img height="35" src="../Images/12-7-3.gif" width="15" /></td>
            </tr>
        </table>
        <table id="tableToolBar" class="table" width="100%">
            <tr>
                <td class="tools-area" width="16">
                    <img align="absMiddle" src="../images/btn_li.gif"></td>
                <td class="tools-area">
                <asp:Button ID="SaveButton" runat="server" CssClass="button" OnClick="ButtonSubmit_Click"
                    Text="����" />
                    <input class="button" onclick="self.close();" type="button" value="�ر�" />
                </td>
            </tr>
        </table>
    <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="list" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound">
    <HeaderStyle CssClass="list-title" HorizontalAlign="Center"/>   
   <RowStyle HorizontalAlign="Center" />    
        <Columns>
        <asp:BoundField HeaderText="���" />
            <asp:TemplateField HeaderText="�û�����">
            <ItemTemplate>
            <%# Eval("UserName") %>
            <input type="hidden" id="HidUserCode" runat="server" value='<%# Eval("UserCode") %>'/>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="���˷���">
            <ItemTemplate>
            <asp:TextBox runat="server" ID="ScoreTextBox" CssClass="input">
            </asp:TextBox>
            </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </form>
</body>
</html>
