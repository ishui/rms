<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContractPaymentList.aspx.cs" Inherits="Contract_ContractPaymentList" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>厂商请款记录表</title>
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
                    厂商请款记录表</td>
            </tr>
            <tr height="100%">
                <td valign="top">
                    <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="form-item" width="10%">项目名称：</td>
                            <td><asp:Label ID="lblProjectName" runat="server"></asp:Label></td>
                            <td class="form-item" width="10%">签约日期：</td>
                            <td><asp:Label ID="lblContractDate" runat="server"></asp:Label></td>
                            <td class="form-item" colspan="2" style="text-align:center">合同付款条件及款项比例</td>
                            <td class="form-item" width="10%">原合同价款：</td>
                            <td><asp:Label ID="lblOriginalMoney" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="form-item">工程名称：</td>
                            <td><asp:Label ID="lblContractName" runat="server"></asp:Label></td>
                            <td class="form-item">开工日期：</td>
                            <td><asp:Label ID="lblWorkStartDate" runat="server"></asp:Label></td>
                            <td class="form-item">预 付 款：</td>
                            <td><asp:Label ID="lblPerCash1" runat="server"></asp:Label></td>
                            <td class="form-item">第一次增补：</td>
                            <td><asp:Label ID="lblChangeMoney1" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="form-item">合 同 编 号：</td>
                            <td><asp:Label ID="lblContractID" runat="server"></asp:Label></td>
                            <td class="form-item">竣工日期：</td>
                            <td><asp:Label ID="lblWorkEndDate" runat="server"></asp:Label></td>
                            <td class="form-item">验 收 款：</td>
                            <td><asp:Label ID="lblPerCash2" runat="server"></asp:Label></td>
                            <td class="form-item">第二次增补：</td>
                            <td><asp:Label ID="lblChangeMoney2" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="form-item">施 工 厂 商：</td>
                            <td><asp:Label ID="lblSupplierName" runat="server"></asp:Label></td>
                            <td class="form-item">完成比例：</td>
                            <td><asp:Label ID="lblFinishedPer" runat="server"></asp:Label></td>
                            <td class="form-item" width="10%">保 修 款：</td>
                            <td><asp:Label ID="lblPerCash3" runat="server"></asp:Label></td>
                            <td class="form-item">合同总价款：</td>
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
			                <asp:TemplateField HeaderText="估验期次" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
				                <ItemTemplate>
					                <%# Container.DataItemIndex + 1 %>
				                </ItemTemplate>
			                </asp:TemplateField>
			                <asp:BoundField HeaderText="估验日期" DataField="ApplyDate" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
			                <asp:BoundField HeaderText="工程项目及内容" FooterText="合计" DataField="Summary" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
			                <asp:BoundField HeaderText="请款金额" DataField="ItemCash0" DataFormatString="{0:N}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
			                <asp:BoundField HeaderText="预付款 %" DataField="ItemCash1" DataFormatString="{0:N}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
			                <asp:BoundField HeaderText="验收款 %" DataField="ItemCash2" DataFormatString="{0:N}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
			                <asp:BoundField HeaderText="保修款 %" DataField="ItemCash3" DataFormatString="{0:N}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
			                <asp:BoundField HeaderText="其他扣款" DataField="ItemCash9" DataFormatString="{0:N}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
			                <asp:BoundField HeaderText="实际请款金额" DataField="ItemCash" DataFormatString="{0:N}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                            <asp:TemplateField HeaderText="比例" ItemStyle-Wrap="false">
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
