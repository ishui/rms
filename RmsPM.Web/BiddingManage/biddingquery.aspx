<%@ Register TagPrefix="uc1" TagName="InputCostBudgetDtl" Src="../UserControls/InputCostBudgetDtl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BiddingList" Src="BiddingList.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register Src="../UserControls/inputunit.ascx" TagName="inputunit" TagPrefix="uc2" %>
<%@ Page language="c#" Inherits="RmsPM.Web.BiddingManage.BiddingQuery" CodeFile="BiddingQuery.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BiddingQuery</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
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
										项目管理>招标管理>招标计划</span></td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
						<table class="table" id="tableToolBar" width="100%">
							<tr>
								<td class="tools-area" width="16"><IMG src="../images/btn_li.gif" align="absMiddle"></td>
								<td class="tools-area"><input name="btnNew" id="btnNew" type="button" value=" 新增 " class="button" runat="server"
										onclick="javascript:OpenNew();">
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					    <td class="table" vAlign="top">
						        <table class="search-area" cellSpacing="0" cellPadding="0" width="100%" border="0">
							        <tr>
								        <td>
									        <table>
										        <tr>
											        <td noWrap><FONT face="宋体">拟定标段：</FONT><FONT face="宋体"><INPUT class="input" id="txtTitle" style="WIDTH: 176px;" type="text" size="24"
														        name="txtTitle" runat="server"/></FONT>
													
                                                            所属部门：
                                                            <uc2:inputunit ID="Inputunit1" runat="server" />
                                                            <span style="color: #ff0000"></span>
                                                    
											            <input  class="submit" id="btnSearch" type="button" value="搜 索" name="btnSearch" runat="server" onclick="document.all.divHintLoad.style.display = '';" onserverclick="btnSearch_ServerClick"/>&nbsp;<img src="../images/search_more.gif" title="高级查询" style="CURSOR:hand" id="imgAdvSearch"
																        onclick="ShowAdvSearch();"/>
													</td>
										        </tr>
										        <tr>
										            <td>
										                <table style="DISPLAY:none" id="divAdvSearch">
										                <tr>
        													<TD nowrap>预审日期：</TD>
														    <TD nowrap><cc3:calendar id="TxtPrejudicationDateS" runat="server" Value="" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar></TD>
														    <TD>&nbsp;到&nbsp;：</TD>
														    <TD nowrap><cc3:calendar id="TxtPrejudicationDateE" runat="server" Value="" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar></TD>
														    <TD>发标日期：</TD>
														    <TD nowrap><cc3:calendar id="TxtEmitDateS" runat="server" Value="" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar></TD>
														    <TD>&nbsp;到&nbsp;：</TD>
													    	<TD nowrap><cc3:calendar id="TxtEmitDateE" runat="server" Value="" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar></TD>
													    	
														</tr>
														<tr>
														    <TD nowrap>回标日期：</TD>
														    <TD nowrap><cc3:calendar id="TxtReturnDateS" runat="server" Value="" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar></TD>
														    <TD>&nbsp;到&nbsp;：</TD>
														    <TD nowrap><cc3:calendar id="TxtReturnDateE" runat="server" Value="" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar></TD>
														    <TD nowrap>定标日期：</TD>
														    <TD nowrap><cc3:calendar id="TxtConfirmDateS" runat="server" Value="" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar></TD>
														    <TD>&nbsp;到&nbsp;：</TD>
														    <TD nowrap><cc3:calendar id="TxtConfirmDateE" runat="server" Value="" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar></TD>
														</tr>
												        </table>
											        </td>
										        </tr>
									        </table>
									        </td>
								        </tr>
							        </table>
								</td>
							</tr>
					
				<tr height="100%">
					<td class="table" vAlign="top"><FONT face="宋体">
							<uc1:BiddingList id="BiddingList1" runat="server"></uc1:BiddingList></FONT>
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
			<div id="divHintLoad" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px; BACKGROUND-COLOR: transparent">
				<TABLE id="tableHintLoad" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameLoad" src="../Cost/LoadingPrepare.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<input id="txtAdvSearch" type="hidden" value="none" name="txtAdvSearch" runat="server">
		</form>
		<script>
	        function OpenNew()
	        {
		        OpenFullWindow('BiddingModify.aspx?State=edit&ProjectCode=<%= Request["ProjectCode"]+"" %>','招标计划新增');
	        }
	
	
            //高级查询
            function ShowAdvSearch()
            {
               
	            var display = Form1.txtAdvSearch.value;
            	
	            if ( display == "none" )
	            {
		            display = "block";
	            }
	            else
	            {
		            display = "none";
	            }
            	
	            Form1.txtAdvSearch.value = display;
            	
	            SetAdvSearch();
            }

            function SetAdvSearch()
            {
	            document.all("divAdvSearch").style.display = Form1.txtAdvSearch.value;

	            if ( Form1.txtAdvSearch.value == "none" )
	            {
            //		Form1.imgAdvSearch.src = "../images/ArrowDown.gif";
		            Form1.imgAdvSearch.title = "高级查询";
	            }
	            else
	            {
            //		Form1.imgAdvSearch.src = "../images/ArrowUp.gif";
		            Form1.imgAdvSearch.title = "隐藏高级查询";
	            }
            }

            SetAdvSearch();
            
		</script>
	</body>
</HTML>
