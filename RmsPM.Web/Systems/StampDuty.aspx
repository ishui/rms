<%@ Page Language="C#" AutoEventWireup="true" codeFile="StampDuty.aspx.cs" Inherits="RmsPM.Web.Systems.Systems_StampDuty" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>印花税</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript" src="../Rms.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ObjectDataSource ID="ListData" runat="server" SelectMethod="GetList" TypeName="RmsPM.BLL.StampDuty">
        </asp:ObjectDataSource>
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td bgcolor="#e4eff6" height="6">
                </td>
            </tr>
            <tr>
                <td height="25">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" background="../images/topic_bg.gif">
                                <img height="25" src="../images/topic_li.jpg" width="35" alt="" align="absMiddle"><span
                                    id="spanTitle" runat="server"> 系统管理&nbsp;- 印花税税目税率</span></td>
                            <td width="9">
                                <img alt="" height="25" src="../images/topic_corr.gif" width="9"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tools-area" valign="top">
                    <img src="../images/btn_li.gif" align="absMiddle">
                    <input class="button" id="btnAdd" onclick="DoAddNew();return false;" type="button"
                        value="新 增" name="btnAdd">
                </td>
            </tr>
            <tr height="100%">
                <td class="table" valign="top">
                    <div style="overflow: auto; width: 100%; height: 100%">
                        <asp:DataGrid ID="dgList" runat="server" CssClass="list" CellPadding="0" GridLines="Horizontal"
                            AllowSorting="True" AutoGenerateColumns="False" PageSize="15" Width="100%" DataSourceID="ListData">
                            <HeaderStyle CssClass="list-title"></HeaderStyle>
                            <FooterStyle CssClass="list-title"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="税目">
                                    <HeaderStyle Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <a href="#" onclick="DoModify(this.code);return false;" code="<%#  DataBinder.Eval(Container.DataItem, "StampDutyID") %>">
                                            <%#  DataBinder.Eval(Container.DataItem, "TaxItems")%></a>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Range" HeaderText="范围">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TaxRate" HeaderText="税率" >
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Width="50px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TaxPayer" HeaderText="纳税人">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Width="70px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Remarks" HeaderText="说明">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                            </Columns>
                            <PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
                                CssClass="ListHeadTr"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td height="12">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td background="../images/corl_bg.gif">
                                <img alt="" height="12" src="../images/corl.gif" width="12"></td>
                            <td width="12">
                                <img alt="" height="12" src="../images/corr.gif" width="12"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td bgcolor="#e4eff6" height="6">
                </td>
            </tr>
        </table>
    </form>

    <script language="javascript">
<!--
		function DoAddNew()
		{
			OpenLargeWindow('StampDutyInfo.aspx?Projectcode=<%=Request["Projectcode"]%>','印花税');
		}
	
		function DoModify( Rolecode )
		{
			OpenLargeWindow('StampDutyInfo.aspx?Projectcode=<%=Request["Projectcode"]%>&StampDutyID=' + Rolecode ,'印花税');
		}
		
	
//-->
    </script>

</body>
</html>
