<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SupplierGradeDpsModif.aspx.cs" Inherits="SupplierGrade_SupplierGradeDpsModif" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>评分部门权重管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Images/index.css" type="text/css" rel="stylesheet">
    <link href="../Images/infra.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>

</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">  
          
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><asp:Label ID="lblTitlename" runat="server" >
				                                        </asp:Label>
									</td>
								<td width="9"><img src="../images/topic_corr.gif" width="9" height="25"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
                    <td>
                        <table class="search-area">
                            <tr>
                                <td style="width:10%;">评分表类型：</td>
								    <td style="width:35%;"><asp:DropDownList ID="ddlWorkFlowTypeView" runat="server">
                                        <asp:ListItem Value="100001" Selected>承包商评分部门</asp:ListItem>
                                        <asp:ListItem Value="100002">供应商评分部门</asp:ListItem>
                                    </asp:DropDownList></td>
                                
                                  <td> 
                                    <input class="submit" id="btnSearch" type="button" value="搜 索" name="btnSearch" runat="server" onserverclick="btnSearch_ServerClick">
                                    
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
				<tr height="100%">
                    <td class="table" valign="top">
                        <table width="100%" height="100%">
				            
				            <tr>
				                <td valign="top">
				                    <asp:datagrid id="dgList" runat="server" PageSize="8" AutoGenerateColumns="False" AllowSorting="True"
										CellPadding="0" CssClass="list" Width="100%">
										<HeaderStyle CssClass="list-title"></HeaderStyle>
										<FooterStyle CssClass="list-title"></FooterStyle>
										<Columns>
										    <asp:TemplateColumn HeaderText="序号">
		                                        <HeaderStyle CssClass="blackbordertdcontent"></HeaderStyle>
		                                        <ItemStyle CssClass="blackbordertdcontent"></ItemStyle>
		                                        <ItemTemplate>
			                                        <asp:Label ID="lblIndex" runat="server" Text='<%#Container.ItemIndex+1%>'>
			                                        </asp:Label>
		                                        </ItemTemplate>
	                                        </asp:TemplateColumn>
	                                        <asp:TemplateColumn Visible=false >
		                                        <HeaderStyle CssClass="blackbordertdcontent"></HeaderStyle>
		                                        <ItemStyle CssClass="blackbordertdcontent"></ItemStyle>
		                                        <ItemTemplate >
			                                        <asp:Label ID="lblDepartmentCode" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.DepartmentDefineCode")%>'>
			                                        </asp:Label>
		                                        </ItemTemplate>
	                                        </asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="部门名称">
		                                        <HeaderStyle CssClass="blackbordertdcontent"></HeaderStyle>
		                                        <ItemStyle CssClass="blackbordertdcontent"></ItemStyle>
		                                        <ItemTemplate>
			                                        <asp:Label ID="lblDepartment" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.DepartmentName")%>'>
			                                        </asp:Label>
		                                        </ItemTemplate>
	                                        </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="权重">
                                                <ItemTemplate>
                                                    &nbsp;<asp:TextBox class="infra-input-nember" style=" WIDTH: 120px; TEXT-ALIGN: right" ID="TxtPercentage" Text='<%#DataBinder.Eval(Container, "DataItem.Percentage")%>'  runat="server"></asp:TextBox>%
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
											
										</Columns>
										<PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Center"
											CssClass="ListHeadTr"></PagerStyle>
									</asp:datagrid>
					            </td>
				            </tr>
				            <tr>
					            <td align="center">
						            <input id="btnSave" name="btnSave" type="button" class="submit" value="确 定" runat="server" onserverclick="btnSave_ServerClick">
						            <input id="btnCancel" name="btnCancel" type="button" class="submit" value="取 消" onclick="javascript:self.close()">
					            </td>
				            </tr>
				        </table>
				    </td>
				</tr>
        </table>       
    </form>


</body>
</html>