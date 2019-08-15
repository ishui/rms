<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BiddingLogList.aspx.cs" Inherits="BiddingManage_BiddingLogList" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>招投标日志</title>
        <LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script>
	        function OpenModify(code,state,projectcode)
	        {
		        OpenFullWindow('BiddingModify.aspx?ApplicationCode='+code+'&State='+state+'&ProjectCode='+projectcode,'招标计划维护');
	        }
        </script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle"><span id="spanTitle">
									</span></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
						<table class="table" id="tableToolBar" width="100%">
							<tr>
								<td class="tools-area" width="16"><IMG src="../images/btn_li.gif" align="absMiddle"></td>
								<td class="tools-area">
								</td>
							</tr>
						</table>
					</td>
				</tr>
				
				<tr height="100%">
					<td class="table" vAlign="top"><FONT face="宋体">
							<!--%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %-->
<!--<asp:TemplateColumn HeaderText="类别">
			<ItemTemplate>
				<%# RmsPM.BLL.SystemGroupRule.GetSystemGroupName((string)(DataBinder.Eval(Container, "DataItem.Type"))) %>
			</ItemTemplate>
		</asp:TemplateColumn>-->
<asp:datagrid id="dgList" CssClass="list" CellPadding="0" GridLines="Horizontal" PageSize="15"
	Width="100%" AllowPaging="True" runat="server" AutoGenerateColumns="False">
	<FooterStyle CssClass="list-title"></FooterStyle>
	<HeaderStyle CssClass="list-title"></HeaderStyle>
	<Columns>
		
		<asp:TemplateColumn HeaderText="修改人">
			<ItemTemplate>
				<a href="#" onclick='OpenUser(code);return false;' code='<%# DataBinder.Eval(Container.DataItem, "UserCode") %>'>
					<%# RmsPM.BLL.SystemRule.GetUserName((string)DataBinder.Eval(Container, "DataItem.UserCode"))%>
				</a>
			</ItemTemplate>
		</asp:TemplateColumn>
		
		<asp:BoundColumn DataField="UpdateTime" HeaderText="创建日期"></asp:BoundColumn>
		<asp:BoundColumn DataField="FormerMoney" HeaderText="原金额"></asp:BoundColumn>
		<asp:BoundColumn DataField="TeamMoney" HeaderText="修改后金额"></asp:BoundColumn>
        <asp:TemplateColumn>
            <ItemTemplate>
				<a href="#" onclick="javascript:OpenLargeWindow('BiddingLogModif.aspx?BiddingLogCode=<%#(string)DataBinder.Eval(Container, "DataItem.BiddingLogCode") %>','招投标日志');">
				    查看
				</a>
			</ItemTemplate>
        
        </asp:TemplateColumn>
		
		
	</Columns>
	<PagerStyle Visible="False" NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
		CssClass="ListHeadTr"></PagerStyle>
</asp:datagrid>
<cc1:GridPagination id="gpControl" runat="server" DataGridId="dgList" ControlSourceUrl="../Images/GridPaginationSource/" onpageindexchange="gpControl_PageIndexChange"></cc1:GridPagination>
</FONT>
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
	
	
	        function OpenLogInfo(code)
	        {
        	    OpenLargeWindow('BiddingLogModif.aspx?BiddingLogCode='+code,'招标计划日志');
	        }
	        function OpenUser( userCode )
            {	
	            OpenMiddleWindow('../Systems/UserInfo.aspx?UserCode='+userCode , '用户信息');
            }
		</script>
	</body>
</HTML>
