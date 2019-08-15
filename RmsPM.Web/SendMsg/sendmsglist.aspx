<%@ Page language="c#" AutoEventWireup="true" Inherits="RmsPM.Web.SendMsg.SendMsgList" CodeFile="SendMsgList.aspx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="Tiannuo.Web.Controls" Assembly="DataGridWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SendMsgList</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script type="text/javascript">
		    function MyLoad()
		    {   
		        window.opener.location.reload();
		    }
		    
		    function  MyDelete()
		    {
		       if(window.confirm('确认删除短消息?'))
		       {   
		           if(window.confirm('确认删除选中的短消息?'))
		           {
                        return  true;
                   }
                   else
                   {
                        return false;
                   }   
		       }
		       else
		       {
		        return false;
		       }
		    }
		
		</script>

        <link href="../Images/GridPaginationSource/PaginationControlStyle.css" rel="stylesheet"
            type="text/css" />
	</HEAD>
	<body onload="MyLoad()" >
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" colSpan="2" height="25">消息列表</td>
				</tr>
				<tr>
					<td class="tools-area" style="WIDTH: 0px" width="0"><IMG src="../images/btn_li.gif" align="absMiddle"></td>
					<td class="tools-area" width="100%"><input class="submit" id="Button1" type="button" value="发送新消息" name="btnSend" onclick="javascript:goediturl();">
						<input class="submit" id="Button2" onclick="window.close();" type="button" value="关 闭"
							name="btnCancel" runat="server">
						<input class="submit" id="Button3" type="button" value="删 除" name="delSend" runat="server" onserverclick="Button3_ServerClick"  onclick="if(!MyDelete()){return false;}"/>
						</td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="2">
						<table cellSpacing="7" cellPadding="0" width="100%" border="0">
							<tr>
								<td style="BORDER-RIGHT: #ededed 3px dotted; PADDING-RIGHT: 7px" vAlign="top" width="60%">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td class="intopic">已发消息</td>
										</tr>
									</table>
									<asp:datagrid id="dgList" runat="server" Width="100%" PageSize="15" AutoGenerateColumns="False"
										GridLines="Horizontal" CellPadding="0" CssClass="list" AllowPaging="True">
										<HeaderStyle CssClass="list-title"></HeaderStyle>
										<FooterStyle CssClass="list-title"></FooterStyle>
										<Columns>
											<asp:BoundColumn Visible="False" DataField="SendMsgCode" HeaderText="代码"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="接收人">
												<ItemTemplate>
													<a href="#" onclick="javascript:gourl('<%# DataBinder.Eval(Container, "DataItem.SendMsgCode") %>','');return false;"><%# RmsPM.BLL.SystemRule.GetUserName((string)DataBinder.Eval(Container, "DataItem.ToUserCode")) %></a>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="SendTime" HeaderText="时间" DataFormatString="{0:d}"></asp:BoundColumn>
											<asp:BoundColumn DataField="msg" HeaderText="内容"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="状态">
												<ItemTemplate>
													<%# RmsPM.Web.SendMsg.SendMsgList.GetMsgState((string)DataBinder.Eval(Container, "DataItem.State")) %>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="选项">
											    <ItemTemplate>
											        <asp:CheckBox runat="server" ID="ck1" />
											    </ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle Visible="False" NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
											CssClass="ListHeadTr"></PagerStyle>
									</asp:datagrid>
									<cc1:GridPagination id="GridPagination1" runat="server" DataGridId="dgList" ControlSourceUrl="../Images/GridPaginationSource/" onpageindexchange="GridPagination1_PageIndexChange"></cc1:GridPagination>
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td class="intopic">接收消息</td>
										</tr>
									</table>
									<asp:datagrid id="Datagrid1" runat="server" Width="100%" PageSize="15" AutoGenerateColumns="False"
										GridLines="Horizontal" CellPadding="0" CssClass="list" AllowPaging="True">
										<HeaderStyle CssClass="list-title"></HeaderStyle>
										<FooterStyle CssClass="list-title"></FooterStyle>
										<Columns>
											<asp:BoundColumn Visible="False" DataField="SendMsgCode" HeaderText="代码"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="发送人">
												<ItemTemplate>
													<a href="#" onclick="javascript:gourl('<%# DataBinder.Eval(Container, "DataItem.SendMsgCode") %>','true');return false;"><%# RmsPM.BLL.SystemRule.GetUserName((string)DataBinder.Eval(Container, "DataItem.SendUserCode")) %></a>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="SendTime" HeaderText="时间" DataFormatString="{0:d}"></asp:BoundColumn>
											<asp:BoundColumn DataField="msg" HeaderText="内容"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="状态">
												<ItemTemplate>
													<%# RmsPM.Web.SendMsg.SendMsgList.GetMsgState((string)DataBinder.Eval(Container, "DataItem.State")) %>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="选项">
											    <ItemTemplate>
											        <asp:CheckBox runat="server" ID="ck2" />
											    </ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle Visible="False" NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
											CssClass="ListHeadTr"></PagerStyle>
									</asp:datagrid>
									<cc1:GridPagination id="GridPagination2" runat="server" DataGridId="Datagrid1" ControlSourceUrl="../Images/GridPaginationSource/" onpageindexchange="GridPagination2_PageIndexChange"></cc1:GridPagination>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
		<script>
	function gourl ( MsgCode ,Re)
	{
		OpenLargeWindow('../SendMsg/SendMsgView.aspx?MsgCode='+MsgCode+'&Re='+Re,'发送查看');
		
	}
	function goediturl()
	{
	    OpenLargeWindow('../SendMsg/SendMsgModify.aspx','新建消息');
	}

		</script>
	</body>
</HTML>
