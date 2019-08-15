<%@ Page language="c#" Inherits="RmsPM.Web.WorkFlowContral.WorkFlowInBox" CodeFile="WorkFlowInBox.aspx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>在办事宜</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body style="BORDER-RIGHT: 0px" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span id="spanTitle" runat="server">
										流程&nbsp;在办事宜</span></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="1">
					<td vAlign="top">
						<table class="search-area" cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td>
									<table>
                                    <tr>
                                        <td>
                                            流程名称：</td>
                                        <td>
                                            <select id="sltProcedure" runat="server" name="sltProcedure">
                                                <option selected value="">---所有流程---</option>
                                            </select>
                                        </td>
                                        <td>
                                            项 &nbsp; &nbsp;&nbsp; 目：</td>
                                        <td>
                                            <asp:DropDownList ID="DropDownProject" runat="server">
                                            </asp:DropDownList></td>
                                        <td></td>
                                        <td></td>
                                        <td rowspan="3"> &nbsp;<input class="submit" id="btnSearch" type="button" value="搜 索" name="btnSearch" runat="server"
                                                onserverclick="btnSearch_ServerClick">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                    <td>
                                        流&nbsp; 水 号：</td>
                                        <td>
                                            <input class="input" type="text" runat="server" id="txtCaseCode" /></td>
                                        <td>
                                            任 &nbsp; &nbsp;&nbsp; 务：</td>
                                        <td>
                                            <input class="input" type="text" runat="server" id="txtTaskName" /></td>
                                        <td>
                                            主 &nbsp; &nbsp;&nbsp; 题：</td>
                                        <td>
                                            <input type="text" class="input" runat="server" id="txtTitle" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            发 &nbsp;件 人：</td>
                                        <td><uc1:InputUser id="ucPerson" runat="server"></uc1:InputUser>
                                        </td>
                                        <td>
                                            发件日期：</td>
                                        <td><cc3:calendar id="DateStart" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False" Display="True" Value=""></cc3:calendar></td>
                                        <td align="center">--></td>
                                        <td><cc3:calendar id="DateEnd" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False" Display="True" Value=""></cc3:calendar>
                                        </td>
                                    </tr>
                                </table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="table" vAlign="top">
						<div style="OVERFLOW:auto;WIDTH:100%;HEIGHT:100%">
							<asp:datagrid id="dgList" runat="server" Width="100%" PageSize="15" AutoGenerateColumns="False"
								GridLines="Horizontal" CellPadding="0" CssClass="list" AllowPaging="True" AllowSorting="True" OnSortCommand="dgList_SortCommand">
								<HeaderStyle CssClass="list-title"></HeaderStyle>
								<FooterStyle CssClass="list-title"></FooterStyle>
								<Columns>
								    <asp:TemplateColumn HeaderText="流水号">
										<ItemTemplate>
										    <%# (RmsPM.BLL.WorkFlowRule.GetWorkFlowRate(DataBinder.Eval(Container, "DataItem.CaseCode").ToString()) == "true") ? "<font color='red'>！</font>" : "<font color='blue'></font>"%>
											<%# RmsPM.BLL.WorkFlowRule.GetWorkFlowNumber(DataBinder.Eval(Container, "DataItem.CaseCode").ToString()) %>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="项目简称">
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# RmsPM.BLL.WorkFlowRule.GetWorkFlowCaseProjectName(DataBinder.Eval(Container, "DataItem.CaseCode").ToString()) %>' ID="Label1">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ProcedureName" HeaderText="流程名称">
										<HeaderStyle Wrap="False"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="任 务">
										<HeaderStyle Wrap="False"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "ToTaskName") %>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="主题">
										<ItemTemplate>
										<a href="##" onclick='gotoDirect(this.caseCode,this.actCode); return false;'
											actCode='<%# DataBinder.Eval(Container.DataItem, "ActCode") %>'
											caseCode='<%# DataBinder.Eval(Container.DataItem, "CaseCode") %>'
											>
											<%# RmsPM.BLL.WorkFlowRule.GetWorkFlowCaseTitle(DataBinder.Eval(Container, "DataItem.CaseCode").ToString()) %>
											</a>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="发件人" SortExpression="FromUserCode">
										<HeaderStyle Wrap="False"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
										<ItemTemplate>
											<%# RmsPM.BLL.SystemRule.GetUserName( DataBinder.Eval(Container.DataItem, "FromUserCode").ToString()) %>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="发件日期" SortExpression="FromDate">
										<HeaderStyle Wrap="False"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
										<ItemTemplate>
											<%# RmsPM.BLL.WorkFlowRule.GetFormatExcedableDate((DateTime)DataBinder.Eval(Container.DataItem, "FromDate")) %>
										</ItemTemplate>
									</asp:TemplateColumn>
								    <asp:TemplateColumn HeaderText="完成日期">
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                        <ItemTemplate>
                                           <%# RmsPM.BLL.WorkFlowRule.GetWorkFlowCaseTime(DataBinder.Eval(Container, "DataItem.CaseCode").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="计划办理日期">
										<HeaderStyle Wrap="False"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "PlanDate","{0:d}") %>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="手送资料">
                                    <ItemTemplate>
                                        <%# RmsPM.BLL.WorkFlowRule.GetWorkFlowHandmade(DataBinder.Eval(Container, "DataItem.CaseCode").ToString()) %>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
								</Columns>
								<PagerStyle Visible="False" NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
									CssClass="ListHeadTr"></PagerStyle>
							</asp:datagrid>
							<cc1:GridPagination id="gpControl" runat="server" DataGridId="dgList" ControlSourceUrl="../Images/GridPaginationSource/" onpageindexchange="gpControl_PageIndexChange"></cc1:GridPagination>
						</div>
					</td>
				</tr>
				<tr>
					<td height="12">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td background="../images/corl_bg.gif"><IMG height="12" src="../images/corl.gif" width="12"></td>
								<td width="12"><IMG height="12" src="../images/corr.gif" width="12"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
			</table>
		</form>
		<script>
	function gotoDirect ( caseCode, actCode )
	{
		OpenFullWindow(   'WorkFlowDirect.aspx?action=DealWith&caseCode='+caseCode + '&actCode=' + actCode  ,'aaaa流程处理');
	}
		</script>
	</body>
</HTML>
