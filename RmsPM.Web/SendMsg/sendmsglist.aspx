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
		       if(window.confirm('ȷ��ɾ������Ϣ?'))
		       {   
		           if(window.confirm('ȷ��ɾ��ѡ�еĶ���Ϣ?'))
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
					<td class="topic" align="center" background="../images/topic_bg.gif" colSpan="2" height="25">��Ϣ�б�</td>
				</tr>
				<tr>
					<td class="tools-area" style="WIDTH: 0px" width="0"><IMG src="../images/btn_li.gif" align="absMiddle"></td>
					<td class="tools-area" width="100%"><input class="submit" id="Button1" type="button" value="��������Ϣ" name="btnSend" onclick="javascript:goediturl();">
						<input class="submit" id="Button2" onclick="window.close();" type="button" value="�� ��"
							name="btnCancel" runat="server">
						<input class="submit" id="Button3" type="button" value="ɾ ��" name="delSend" runat="server" onserverclick="Button3_ServerClick"  onclick="if(!MyDelete()){return false;}"/>
						</td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="2">
						<table cellSpacing="7" cellPadding="0" width="100%" border="0">
							<tr>
								<td style="BORDER-RIGHT: #ededed 3px dotted; PADDING-RIGHT: 7px" vAlign="top" width="60%">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td class="intopic">�ѷ���Ϣ</td>
										</tr>
									</table>
									<asp:datagrid id="dgList" runat="server" Width="100%" PageSize="15" AutoGenerateColumns="False"
										GridLines="Horizontal" CellPadding="0" CssClass="list" AllowPaging="True">
										<HeaderStyle CssClass="list-title"></HeaderStyle>
										<FooterStyle CssClass="list-title"></FooterStyle>
										<Columns>
											<asp:BoundColumn Visible="False" DataField="SendMsgCode" HeaderText="����"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="������">
												<ItemTemplate>
													<a href="#" onclick="javascript:gourl('<%# DataBinder.Eval(Container, "DataItem.SendMsgCode") %>','');return false;"><%# RmsPM.BLL.SystemRule.GetUserName((string)DataBinder.Eval(Container, "DataItem.ToUserCode")) %></a>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="SendTime" HeaderText="ʱ��" DataFormatString="{0:d}"></asp:BoundColumn>
											<asp:BoundColumn DataField="msg" HeaderText="����"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="״̬">
												<ItemTemplate>
													<%# RmsPM.Web.SendMsg.SendMsgList.GetMsgState((string)DataBinder.Eval(Container, "DataItem.State")) %>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="ѡ��">
											    <ItemTemplate>
											        <asp:CheckBox runat="server" ID="ck1" />
											    </ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle Visible="False" NextPageText="��һҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��һҳ" HorizontalAlign="Right"
											CssClass="ListHeadTr"></PagerStyle>
									</asp:datagrid>
									<cc1:GridPagination id="GridPagination1" runat="server" DataGridId="dgList" ControlSourceUrl="../Images/GridPaginationSource/" onpageindexchange="GridPagination1_PageIndexChange"></cc1:GridPagination>
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td class="intopic">������Ϣ</td>
										</tr>
									</table>
									<asp:datagrid id="Datagrid1" runat="server" Width="100%" PageSize="15" AutoGenerateColumns="False"
										GridLines="Horizontal" CellPadding="0" CssClass="list" AllowPaging="True">
										<HeaderStyle CssClass="list-title"></HeaderStyle>
										<FooterStyle CssClass="list-title"></FooterStyle>
										<Columns>
											<asp:BoundColumn Visible="False" DataField="SendMsgCode" HeaderText="����"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="������">
												<ItemTemplate>
													<a href="#" onclick="javascript:gourl('<%# DataBinder.Eval(Container, "DataItem.SendMsgCode") %>','true');return false;"><%# RmsPM.BLL.SystemRule.GetUserName((string)DataBinder.Eval(Container, "DataItem.SendUserCode")) %></a>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="SendTime" HeaderText="ʱ��" DataFormatString="{0:d}"></asp:BoundColumn>
											<asp:BoundColumn DataField="msg" HeaderText="����"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="״̬">
												<ItemTemplate>
													<%# RmsPM.Web.SendMsg.SendMsgList.GetMsgState((string)DataBinder.Eval(Container, "DataItem.State")) %>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="ѡ��">
											    <ItemTemplate>
											        <asp:CheckBox runat="server" ID="ck2" />
											    </ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle Visible="False" NextPageText="��һҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��һҳ" HorizontalAlign="Right"
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
		OpenLargeWindow('../SendMsg/SendMsgView.aspx?MsgCode='+MsgCode+'&Re='+Re,'���Ͳ鿴');
		
	}
	function goediturl()
	{
	    OpenLargeWindow('../SendMsg/SendMsgModify.aspx','�½���Ϣ');
	}

		</script>
	</body>
</HTML>
