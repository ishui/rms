<%@ Page language="c#" Inherits="RmsPM.Web.Remind.NoticeList" CodeFile="NoticeList.aspx.cs" %>

<%@ Register Src="../UserControls/inputuser.ascx" TagName="inputuser" TagPrefix="uc1" %>

<%@ Register Assembly="RmsPM.BLL" Namespace="Rms.ControlLb" TagPrefix="cc2" %>
<%@ Register TagPrefix="cc1" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>NoticeList</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script language="javascript">
			function NoticeAction(Code)
			{
					OpenMiddleWindow('../Remind/Notify.aspx?&Action=Modify&Code=' + Code+"&ProjectCode=<%=Request["ProjectCode"]%>",'');// 修改
			}
			
			function AddNewNotice()
			{
				OpenMiddleWindow('../Remind/Notify.aspx?Action=Insert&ProjectCode=<%=Request["ProjectCode"]%>&DocType=<%=Request["DocType"]%>','');
			}
			
			function refresh()
			{
				window.document.Form1.submit();
			}
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="topic" background="../images/topic_bg.gif" width="100%"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">
						<span id="lblTitle" runat="server"></span>
					</td>
					<td><IMG height="25" src="../images/topic_corr.gif"></td>
				</tr>
			</table>
			
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="tools-area" id="tdNewNotice" runat="server">
						<IMG src="../images/btn_li.gif" align="absMiddle">
						&nbsp;&nbsp; <input class="button" onclick="AddNewNotice();return false;" type="button" value="新增通知" id="btnAddNew" runat="server">
					</td>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
			    <TR>
					<td class="search-area" vAlign="top" nowrap>
						    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						    标题:&nbsp;
							<asp:textbox id="TB_NoticeTitle" runat="server" Height="20px" Width="100px" CssClass="input"></asp:textbox>
							&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							发布人:&nbsp;<uc1:inputuser ID="SP_Notice" runat="server" />
							<br><br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							
						　　发布时间:&nbsp;
						    <cc1:Calendar id="dtNoticeDateBegin" runat="server" CalendarResource="../Images/CalendarResource/"
											Value=""></cc1:Calendar>&nbsp;——
										<cc1:Calendar id="dtNoticeDateEnd" runat="server" CalendarResource="../Images/CalendarResource/"
											Value=""></cc1:Calendar>
							&nbsp;&nbsp;&nbsp;&nbsp;
						   <asp:Label ID="lblNoticeClass" runat="server">发布类型:</asp:Label>&nbsp;
                           <SELECT id="DDLNoticeClass" size="1" name="sltNotice" runat="server">
										    <OPTION value="" selected>------请选择------</OPTION>
									        </SELECT>
                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                           <INPUT class="submit" id="btnSearch" type="button" value="搜   索" name="btnSearch" runat="server"
								style="WIDTH: 80px; height: 20px" onserverclick="btnSearch_ServerClick" >
					</td>
				</TR> 
				<tr vAlign="top" height="100%" width="100%">
					<td class="table" vAlign="top">
						<asp:datagrid id="dgNoticeList" runat="server" Width="100%" CssClass="list" AutoGenerateColumns="False">
							<HeaderStyle CssClass="list-title"></HeaderStyle>
							<Columns>
								<asp:BoundColumn HeaderText="状态" Visible=False></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="标题">
									<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
									<ItemTemplate>
										<a href="#" onclick="NoticeAction(this.Code);return false;" Code='<%# DataBinder.Eval(Container, "DataItem.NoticeCode") %>'><%# DataBinder.Eval(Container, "DataItem.NoticeClassTitle")%></a>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="SubmitPerson" HeaderText="发布人"></asp:BoundColumn>
								<asp:BoundColumn DataField="SubmitDate" HeaderText="发出时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
								<asp:BoundColumn DataField="UpdateDate" HeaderText="最后修改日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="状态">
									<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
									<ItemTemplate>
										<%# (UserHasReadThisNotice(DataBinder.Eval(Container.DataItem, "NoticeCode").ToString())) ? "已读" : "未读"%>
									</ItemTemplate>
								</asp:TemplateColumn>
								
							</Columns>
						</asp:datagrid></td>
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
					<td bgColor="#e4eff6" height="6"><FONT face="宋体">&nbsp;</FONT></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
