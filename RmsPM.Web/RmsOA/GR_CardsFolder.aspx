<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GR_CardsFolder.aspx.cs" Inherits="RmsOA_CardsFolder" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link type="text/css" href="../Images/index.css" rel="stylesheet" />
    <title>名片列表</title>

    <script language="javascript" type="text/javascript">
     function OpenLargeWindow(strUrl,strName)
     {
	    return window.open(strUrl,strName,"width=800,height=600,fullscreen=0,top="+(window.screen.height-600)/2+",left="+(window.screen.width-800)/2+",menubar=no,toolbar=no,scrollbars=yes,status=no,titlebar=no,resizable=yes,location=no");
     }
    </script>

</head>
<body style="BORDER-RIGHT: 0px" scroll="no">
    <form id="form1" runat="server">
        <div>
          <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td height="6" bgcolor="#e4eff6"></td>
            </tr>
          </table>
          <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
              <td width="986" background="../images/topic_bg.gif" class="topic"><img align="absMiddle" height="25" src="../images/topic_li.jpg" width="35">个人办公>个人名片夹 </td>
              <td width="9"><img height="25" src="../images/topic_corr.gif" width="9"></td>
            </tr>
            <tr>
              <td colspan="2" valign="top" class="tools-area" style="width: 100%;"><img align="absMiddle" src="../images/btn_li.gif">
                  <input name="button"
                            type="button" class="button" id="NewButton" onClick="OpenLargeWindow('GR_CardModify.aspx?Type=Add','Card_Modify')" value="新增" runat="server" /></td>
		    </tr>
          </table>
          <table border="0" cellpadding="0" cellspacing="0" class="search-area" width="100%">
                <tr>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    姓名
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxName" runat="server" CssClass="input" Width="60px"></asp:TextBox>
                                </td>
                                <td>
                                    单位名称
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxDept" runat="server" CssClass="input"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>电话号码
                                </td>
                                <td>
                                    <asp:TextBox ID="PhoneTextBox" runat="server" CssClass="input" Width="80px"></asp:TextBox>
                                </td>
                                <td>
                                    手机
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxMobile" runat="server" CssClass="input" Width="80px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    名片类型
                                </td>
                                <td>
                                <asp:DropDownList ID="ddlType" runat="server" DataSourceID="ObjectDataSource1" Font-Size="9pt">
                                  </asp:DropDownList>
                                </td>
                                <td>
                                    名片范围
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlScope" runat="server" Font-Size="9pt">
                                        <asp:ListItem Text="个人名片" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="公共名片" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td colspan="4">
                                    <input id="SearchButton" runat="server" class="submit" onserverclick="btSearch_Click"
                                        type="button" value="搜索" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
          </table>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" CssClass="list" PageSize="15"
                Width="100%" AutoGenerateColumns="False">
                <FooterStyle CssClass="list-title" />
                <HeaderStyle CssClass="list-title" />
                <Columns>
                    <asp:TemplateField HeaderText="姓名">
                        <ItemTemplate>
                            <a href="#" onClick="OpenLargeWindow('GR_CardModify.aspx?Type=Read&Code=<%#Eval("Code")%>','CardModify')">
                                <%# Eval("UserName")%>
                            </a>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle CssClass="list-title" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="CompanyName" HeaderText="单位名称" SortExpression="CompanyName">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Phone" HeaderText="电话" SortExpression="Phone">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Headship" HeaderText="职务" SortExpression="Headship">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Mobile" HeaderText="手机" SortExpression="Mobile">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
                <PagerSettings FirstPageText="首 页" PreviousPageText="上一页" NextPageText="下一页" LastPageText="尾 页"
                    Visible="true" Position="Bottom" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetCardType"
                TypeName="RmsOA.BFL.GK_OA_CardsFolderBFL"></asp:ObjectDataSource>
      </div>
    </form>
</body>
</html>
