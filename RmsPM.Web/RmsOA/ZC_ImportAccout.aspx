<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZC_ImportAccout.aspx.cs"
    Inherits="RmsOA_ZC_ImportAccout" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>固定资产台帐导入</title>
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
                                    文件：</td>
                                <td>
                                    <asp:FileUpload runat="server" Width="100%" ID="importFile" CssClass="input" /></td>
                            </tr>
                        </table>
                    </div>
                    <div style="font-size: 9pt;">
                        文件格式说明：<br />
                        1.文件类型必须是csv（逗号分隔）<br />
                        2.文件的第1行为标题行，以后为数据行。<br />
                        3.数据行依次为：<br />
                        &nbsp;&nbsp;<asp:Label runat="server" ID="lblFieldDesc" ForeColor="blue"></asp:Label><br />
                        <asp:Literal runat="server" ID="lblOtherMessage"></asp:Literal><br />
                    </div>
                    <div style="vertical-align: text-bottom; height: 100%; text-align: center">
                        <asp:Button CssClass="submit" runat="server" ID="Import" Text="导 入" OnClick="Import_Click" />
                        <span style="width: 10px;"></span>
                        <input type="button" value="关 闭" class="submit" onclick="opener = null; window.close();" />
                    </div>
                </asp:View>
                <asp:View ID="ResultView" runat="server">
                    <table cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td class="intopic" width="200">
                                预导入资产台帐列表</td>
                        </tr>
                    </table>
                    <asp:GridView runat="server" ID="successGridView" AutoGenerateColumns="false" Width="100%"
                        CssClass="list" ShowFooter="false">
                        <HeaderStyle CssClass="list-title" HorizontalAlign="center" Wrap="false" />
                        <RowStyle HorizontalAlign="center" />
                        <Columns>
                            <asp:BoundField DataField="Index" HeaderText="序号" SortExpression="Index" />
                            <asp:BoundField DataField="NoIndex" HeaderText="行号" SortExpression="NoIndex" />
                            <asp:BoundField DataField="Name" HeaderText="名称" SortExpression="Name" />
                            <asp:BoundField DataField="Type" HeaderText="类别" SortExpression="Type" />
                            <asp:BoundField DataField="Number" HeaderText="编号" SortExpression="Number" />
                            <asp:BoundField DataField="Buydate" HeaderText="购置日期" SortExpression="Buydate" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" />
                            <asp:BoundField DataField="BuyCount" HeaderText="数量" SortExpression="BuyCount" />
                            <asp:BoundField DataField="DeptName" HeaderText="单位" SortExpression="Unit" />
                            <asp:BoundField DataField="Price" HeaderText="金额" SortExpression="Price" />
                            <asp:BoundField DataField="Dept" HeaderText="使用部门" SortExpression="Dept" />
                            <asp:BoundField DataField="UserName" HeaderText="使用人" SortExpression="UserName" />
                            <asp:BoundField DataField="Place" HeaderText="存放地点" SortExpression="Place" />
                            <asp:BoundField DataField="TransferRecord" HeaderText="变更情况" SortExpression="TransferRecord"
                                ItemStyle-Wrap="true" />
                            <asp:BoundField DataField="Remark" HeaderText="备注" SortExpression="Remark" ItemStyle-Wrap="true" />
                        </Columns>
                    </asp:GridView>
                    <table cellspacing="0" cellpadding="0" border="0" runat="server" id="WrongTable">
                        <tr>
                            <td class="intopic" width="200">
                                错误信息列表</td>
                        </tr>
                    </table>
                    <asp:GridView runat="server" AutoGenerateColumns="false" ID="failureGridView" CssClass="list"
                        Width="100%" ShowFooter="false">
                        <HeaderStyle CssClass="list-title" HorizontalAlign="center" Wrap="false" />
                        <FooterStyle CssClass="list-title" HorizontalAlign="center" Wrap="false" />
                        <RowStyle HorizontalAlign="center" />
                        <Columns>
                            <asp:BoundField DataField="Index" HeaderText="序号" SortExpression="Index" />
                            <asp:BoundField DataField="NoIndex" HeaderText="行号" SortExpression="NoIndex" />
                            <asp:BoundField ItemStyle-ForeColor="Red" DataField="Message" HeaderText="出错原因" SortExpression="Message" />
                        </Columns>
                        <EmptyDataRowStyle Font-Size="9pt" HorizontalAlign="center" Font-Bold="true" />
                        <EmptyDataTemplate>
                        导入全部成功
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <div style="color: Blue; text-align: center; width: 100%; font-size: 9pt;">
                        数据导入完成，共导入&nbsp;<asp:Label Font-Size="9pt" ForeColor="red" Font-Bold="true" runat="server"
                            ID="lblTotalMessage"></asp:Label>&nbsp;条， 成功&nbsp;<asp:Label Font-Size="9pt" ForeColor="red"
                                Font-Bold="true" runat="server" ID="lblrightMessage"></asp:Label>&nbsp;条，失败&nbsp;<asp:Label
                                    Font-Size="9pt" ForeColor="red" Font-Bold="true" runat="server" ID="lblWrongMessage"></asp:Label>&nbsp;条。
                    </div>
                    <div style="height: 100%; vertical-align: bottom; text-align: center;">
                        <asp:Button CssClass="submit" runat="server" ID="PreViewButton" CommandName="SwitchViewByID"
                            CommandArgument="FirstView" Text="上一步" />
                        <span style="width: 10px;"></span>
                        <asp:Button runat="server" ID="SaveButton" CssClass="submit" Text="保 存" OnClientClick="return window.confirm('确认要保存有效数据吗?');"
                            OnClick="SaveButton_Click" />
                        <span style="width: 10px;"></span>
                        <input type="button" value="关 闭" class="submit" onclick="opener = null; window.close();" />
                    </div>
                </asp:View>
            </asp:MultiView>
        </div>
    </form>
</body>
</html>
