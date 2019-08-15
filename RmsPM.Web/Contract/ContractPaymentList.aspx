<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContractPaymentList.aspx.cs" Inherits="Contract_ContractPaymentList" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>��������¼��</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Images/index.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>

    <script language="javascript" src="../images/JoyBox.js"></script>

</head>
<body>
  <form id="Form1" method="post" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" bgcolor="#ffffff"
            border="0">
            <tr>
                <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                    ��������¼��</td>
            </tr>
            <tr height="100%">
                <td valign="top">
                    <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="form-item" width="10%">��Ŀ���ƣ�</td>
                            <td><asp:Label ID="lblProjectName" runat="server"></asp:Label></td>
                            <td class="form-item" width="10%">ǩԼ���ڣ�</td>
                            <td><asp:Label ID="lblContractDate" runat="server"></asp:Label></td>
                            <td class="form-item" colspan="2" style="text-align:center">��ͬ�����������������</td>
                            <td class="form-item" width="10%">ԭ��ͬ�ۿ</td>
                            <td><asp:Label ID="lblOriginalMoney" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="form-item">�������ƣ�</td>
                            <td><asp:Label ID="lblContractName" runat="server"></asp:Label></td>
                            <td class="form-item">�������ڣ�</td>
                            <td><asp:Label ID="lblWorkStartDate" runat="server"></asp:Label></td>
                            <td class="form-item">Ԥ �� �</td>
                            <td><asp:Label ID="lblPerCash1" runat="server"></asp:Label></td>
                            <td class="form-item">��һ��������</td>
                            <td><asp:Label ID="lblChangeMoney1" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="form-item">�� ͬ �� �ţ�</td>
                            <td><asp:Label ID="lblContractID" runat="server"></asp:Label></td>
                            <td class="form-item">�������ڣ�</td>
                            <td><asp:Label ID="lblWorkEndDate" runat="server"></asp:Label></td>
                            <td class="form-item">�� �� �</td>
                            <td><asp:Label ID="lblPerCash2" runat="server"></asp:Label></td>
                            <td class="form-item">�ڶ���������</td>
                            <td><asp:Label ID="lblChangeMoney2" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="form-item">ʩ �� �� �̣�</td>
                            <td><asp:Label ID="lblSupplierName" runat="server"></asp:Label></td>
                            <td class="form-item">��ɱ�����</td>
                            <td><asp:Label ID="lblFinishedPer" runat="server"></asp:Label></td>
                            <td class="form-item" width="10%">�� �� �</td>
                            <td><asp:Label ID="lblPerCash3" runat="server"></asp:Label></td>
                            <td class="form-item">��ͬ�ܼۿ</td>
                            <td><asp:Label ID="lblTotalMoney" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                    <br />
                    <asp:GridView ID="gvPaymentItem" runat="server" DataKeyNames="PaymentItemCode" CellPadding="0" AllowSorting="True"
		                GridLines="Both" AutoGenerateColumns="False" PageSize="15" ShowFooter="True" Width="100%" CssClass="list"
		                >
		                <HeaderStyle Wrap="False" CssClass="list-title" HorizontalAlign="Center" />
		                <RowStyle  Wrap="False" HorizontalAlign="Center" />
		                <FooterStyle Wrap="False" CssClass="list-title" HorizontalAlign="Center"/>
		                <Columns>
			                <asp:TemplateField HeaderText="�����ڴ�" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
				                <ItemTemplate>
					                <%# Container.DataItemIndex + 1 %>
				                </ItemTemplate>
			                </asp:TemplateField>
			                <asp:BoundField HeaderText="��������" DataField="ApplyDate" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
			                <asp:BoundField HeaderText="������Ŀ������" FooterText="�ϼ�" DataField="Summary" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
			                <asp:BoundField HeaderText="�����" DataField="ItemCash0" DataFormatString="{0:N}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
			                <asp:BoundField HeaderText="Ԥ���� %" DataField="ItemCash1" DataFormatString="{0:N}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
			                <asp:BoundField HeaderText="���տ� %" DataField="ItemCash2" DataFormatString="{0:N}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
			                <asp:BoundField HeaderText="���޿� %" DataField="ItemCash3" DataFormatString="{0:N}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
			                <asp:BoundField HeaderText="�����ۿ�" DataField="ItemCash9" DataFormatString="{0:N}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
			                <asp:BoundField HeaderText="ʵ�������" DataField="ItemCash" DataFormatString="{0:N}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                            <asp:TemplateField HeaderText="����" ItemStyle-Wrap="false">
				                <ItemTemplate>
				                    <%# ((decimal)DataBinder.Eval( Container.DataItem,"ItemCash") / decimal.Parse(ViewState["TotalMoney"].ToString())).ToString("P") %>
                                </ItemTemplate>
			                </asp:TemplateField>
		                </Columns>
                    </asp:GridView>
                    
                </td>
             </tr>
        </table>
    </form>
</body>
</html>
