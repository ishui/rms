<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZC_ImportAccout.aspx.cs"
    Inherits="RmsOA_ZC_ImportAccout" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�̶��ʲ�̨�ʵ���</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript" src="../Rms.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table cellspacing="0" cellpadding="0" width="100%" border="0" style="height: 25px;">
                <tr>
                    <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                        <asp:Label runat="server" ID="lblTitle" Font-Size="9pt" ForeColor="white" Font-Bold="true"></asp:Label></td>
                </tr>
            </table>
            <asp:MultiView runat="server" ActiveViewIndex="0" ID="multiView">
                <asp:View ID="FirstView" runat="server">
                    <div style="width: 100%; color: Blue; text-align: center; font-size: 9pt;">
                        <asp:Label runat="server" ID="lblHead" Font-Size="9pt" ForeColor="blue"></asp:Label>
                    </div>
                    <div>
                        <table class="form" width="100%" border="0" cellpadding="0">
                            <tr>
                                <td class="form-item" style="width: 20%;">
                                    �ļ���</td>
                                <td>
                                    <asp:FileUpload runat="server" Width="100%" ID="importFile" CssClass="input" /></td>
                            </tr>
                        </table>
                    </div>
                    <div style="font-size: 9pt;">
                        �ļ���ʽ˵����<br />
                        1.�ļ����ͱ�����csv�����ŷָ���<br />
                        2.�ļ��ĵ�1��Ϊ�����У��Ժ�Ϊ�����С�<br />
                        3.����������Ϊ��<br />
                        &nbsp;&nbsp;<asp:Label runat="server" ID="lblFieldDesc" ForeColor="blue"></asp:Label><br />
                        <asp:Literal runat="server" ID="lblOtherMessage"></asp:Literal><br />
                    </div>
                    <div style="vertical-align: text-bottom; height: 100%; text-align: center">
                        <asp:Button CssClass="submit" runat="server" ID="Import" Text="�� ��" OnClick="Import_Click" />
                        <span style="width: 10px;"></span>
                        <input type="button" value="�� ��" class="submit" onclick="opener = null; window.close();" />
                    </div>
                </asp:View>
                <asp:View ID="ResultView" runat="server">
                    <table cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td class="intopic" width="200">
                                Ԥ�����ʲ�̨���б�</td>
                        </tr>
                    </table>
                    <asp:GridView runat="server" ID="successGridView" AutoGenerateColumns="false" Width="100%"
                        CssClass="list" ShowFooter="false">
                        <HeaderStyle CssClass="list-title" HorizontalAlign="center" Wrap="false" />
                        <RowStyle HorizontalAlign="center" />
                        <Columns>
                            <asp:BoundField DataField="Index" HeaderText="���" SortExpression="Index" />
                            <asp:BoundField DataField="NoIndex" HeaderText="�к�" SortExpression="NoIndex" />
                            <asp:BoundField DataField="Name" HeaderText="����" SortExpression="Name" />
                            <asp:BoundField DataField="Type" HeaderText="���" SortExpression="Type" />
                            <asp:BoundField DataField="Number" HeaderText="���" SortExpression="Number" />
                            <asp:BoundField DataField="Buydate" HeaderText="��������" SortExpression="Buydate" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" />
                            <asp:BoundField DataField="BuyCount" HeaderText="����" SortExpression="BuyCount" />
                            <asp:BoundField DataField="DeptName" HeaderText="��λ" SortExpression="Unit" />
                            <asp:BoundField DataField="Price" HeaderText="���" SortExpression="Price" />
                            <asp:BoundField DataField="Dept" HeaderText="ʹ�ò���" SortExpression="Dept" />
                            <asp:BoundField DataField="UserName" HeaderText="ʹ����" SortExpression="UserName" />
                            <asp:BoundField DataField="Place" HeaderText="��ŵص�" SortExpression="Place" />
                            <asp:BoundField DataField="TransferRecord" HeaderText="������" SortExpression="TransferRecord"
                                ItemStyle-Wrap="true" />
                            <asp:BoundField DataField="Remark" HeaderText="��ע" SortExpression="Remark" ItemStyle-Wrap="true" />
                        </Columns>
                    </asp:GridView>
                    <table cellspacing="0" cellpadding="0" border="0" runat="server" id="WrongTable">
                        <tr>
                            <td class="intopic" width="200">
                                ������Ϣ�б�</td>
                        </tr>
                    </table>
                    <asp:GridView runat="server" AutoGenerateColumns="false" ID="failureGridView" CssClass="list"
                        Width="100%" ShowFooter="false">
                        <HeaderStyle CssClass="list-title" HorizontalAlign="center" Wrap="false" />
                        <FooterStyle CssClass="list-title" HorizontalAlign="center" Wrap="false" />
                        <RowStyle HorizontalAlign="center" />
                        <Columns>
                            <asp:BoundField DataField="Index" HeaderText="���" SortExpression="Index" />
                            <asp:BoundField DataField="NoIndex" HeaderText="�к�" SortExpression="NoIndex" />
                            <asp:BoundField ItemStyle-ForeColor="Red" DataField="Message" HeaderText="����ԭ��" SortExpression="Message" />
                        </Columns>
                        <EmptyDataRowStyle Font-Size="9pt" HorizontalAlign="center" Font-Bold="true" />
                        <EmptyDataTemplate>
                        ����ȫ���ɹ�
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <div style="color: Blue; text-align: center; width: 100%; font-size: 9pt;">
                        ���ݵ�����ɣ�������&nbsp;<asp:Label Font-Size="9pt" ForeColor="red" Font-Bold="true" runat="server"
                            ID="lblTotalMessage"></asp:Label>&nbsp;���� �ɹ�&nbsp;<asp:Label Font-Size="9pt" ForeColor="red"
                                Font-Bold="true" runat="server" ID="lblrightMessage"></asp:Label>&nbsp;����ʧ��&nbsp;<asp:Label
                                    Font-Size="9pt" ForeColor="red" Font-Bold="true" runat="server" ID="lblWrongMessage"></asp:Label>&nbsp;����
                    </div>
                    <div style="height: 100%; vertical-align: bottom; text-align: center;">
                        <asp:Button CssClass="submit" runat="server" ID="PreViewButton" CommandName="SwitchViewByID"
                            CommandArgument="FirstView" Text="��һ��" />
                        <span style="width: 10px;"></span>
                        <asp:Button runat="server" ID="SaveButton" CssClass="submit" Text="�� ��" OnClientClick="return window.confirm('ȷ��Ҫ������Ч������?');"
                            OnClick="SaveButton_Click" />
                        <span style="width: 10px;"></span>
                        <input type="button" value="�� ��" class="submit" onclick="opener = null; window.close();" />
                    </div>
                </asp:View>
            </asp:MultiView>
        </div>
    </form>
</body>
</html>
