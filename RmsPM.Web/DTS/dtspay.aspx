<%@ Page language="c#" Inherits="RmsPM.Web.DTS.DtsPay" CodeFile="DtsPay.aspx.cs" %>
<%@ Register TagPrefix="cc1" TagName="DtsProgress" Src="../Dts/DtsProgress.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DtsPay</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body onload="winload();" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
				<tr>
					<td bgColor="#e4eff6" height="6"></td>
				</tr>
				<tr>
					<td height="25">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" background="../images/topic_bg.gif"><IMG height="25" src="../images/topic_li.jpg" width="35" align="absMiddle">财务管理 
									- 销售导入
								</td>
								<td width="9"><IMG height="25" src="../images/topic_corr.gif" width="9"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="table" align="center">
						<br>
						<table cellpadding="5" cellspacing="0" width="400" height="100" bgcolor="#ffffff" style="BORDER-RIGHT:#87b3d0 1px solid; BORDER-TOP:#87b3d0 1px solid; BORDER-LEFT:#87b3d0 1px solid; BORDER-BOTTOM:#87b3d0 1px solid">
							<tr bgcolor="#dbeaf5" height="30">
								<td>执行选项</td>
							</tr>
							<tr>
								<td>
									<table cellpadding="0" cellspacing="0" border="0" width="100%" height="100%">
										<tr>
										<!--
											<td rowspan="2">
												<asp:RadioButtonList id="rdoOption" runat="server" Height="18px">
													<asp:ListItem Value="0" Selected="True">项目</asp:ListItem>
													<asp:ListItem Value="1">客户</asp:ListItem>
												</asp:RadioButtonList>
											</td>
											-->
											<td>项目：</td>
											<td>
												<SELECT id="sltProject" name="sltProject" runat="server">
													<OPTION selected value=" ">全部</OPTION>
												</SELECT>
											</td>
										</tr>
										<tr>
											<td>客户：</td>
											<td><input type="text" runat="server" id="txtClientName" name="txtClientName"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table cellpadding="5" cellspacing="0" width="400" height="100" bgcolor="#ffffff" style="BORDER-RIGHT:#87b3d0 1px solid; BORDER-TOP:#87b3d0 1px solid; BORDER-LEFT:#87b3d0 1px solid; BORDER-BOTTOM:#87b3d0 1px solid">
							<tr bgcolor="#dbeaf5" height="30">
								<td>执行状态</td>
							</tr>
							<tr height="30">
								<td><asp:label id="lbHint" runat="server">Label</asp:label></td>
							</tr>
							<tr valign="top">
								<td>
									<table id="tbProgress" cellSpacing="0" cellPadding="0" border="0" runat="server" width="100%">
										<tr>
											<td colspan="6">
												<table cellpadding="0" width="90%" border="0" cellspacing="0" style="BORDER-RIGHT:#104a7b 1px solid; PADDING-RIGHT:0px; BORDER-TOP:#104a7b 1px solid; PADDING-LEFT:0px; PADDING-BOTTOM:2px; BORDER-LEFT:#104a7b 1px solid; PADDING-TOP:2px; BORDER-BOTTOM:#104a7b 1px solid; BACKGROUND-COLOR:white">
													<tr>
														<td width="100%">
															<div id="divProgress" style="FONT-SIZE:1px; WIDTH:0%; HEIGHT:5px; BACKGROUND-COLOR:#76d769"></div>
														</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td id="tdExecCount"></td>
											<td>&nbsp;of&nbsp;</td>
											<td id="tdCount" runat="server"></td>
											<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;出错：</td>
											<td align="right" id="tdErrCount"></td>
											<td align="right">条</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td><iframe id="frameExec" src="DtsPayExec.aspx" frameBorder="0" width="100%" height="50"></iframe>
								</td>
							</tr>
						</table>
						<br>
					</td>
				</tr>
				<tr id="trErr" runat="server" height="100%">
					<td class="table" align="center">
						<table cellpadding="5" cellspacing="0" width="400" height="100%" bgcolor="#ffffff" style="BORDER-RIGHT:#87b3d0 1px solid; BORDER-TOP:#87b3d0 1px solid; BORDER-LEFT:#87b3d0 1px solid; BORDER-BOTTOM:#87b3d0 1px solid">
							<tr bgcolor="#dbeaf5" height="30">
								<td>出错信息</td>
							</tr>
							<tr>
								<td>
									<TEXTAREA id="txtErr" name="txtErr" style="BORDER-RIGHT:0px;BORDER-TOP:0px;BORDER-LEFT:0px;WIDTH:100%;BORDER-BOTTOM:0px;HEIGHT:100%"
										runat="server"></TEXTAREA></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="table" align="center">
						<table cellSpacing="0" cellPadding="10" border="0" runat="server">
							<tr align="center">
								<td><input class="submit" id="btnDtc" onclick="if (!DtsStart()) return false;" type="button"
										value="导 入" name="btnDtc" runat="server" onserverclick="btnDtc_ServerClick"></td>
								<td>&nbsp;<input class="submit" id="btnClear" onclick="if (!Clear()) return false;" type="button"
										value="清 空" name="btnClear" runat="server" onserverclick="btnClear_ServerClick"></td>
								<td>&nbsp;<input class="submit" id="btnStop" onclick="return Stop();" type="button" value="终 止" name="btnStop"
										runat="server"></td>
							</tr>
						</table>
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
				<div style="DISPLAY: none"><input id="btnDtsContinue" title="" type="button" name="btnDtsContinue" runat="server" onserverclick="btnDtsContinue_ServerClick">
					<input id="btnDtsFinish" title="" type="button" name="btnDtsFinish" runat="server" onserverclick="btnDtsFinish_ServerClick">
				</div>
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
			<div id="divHintSave" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 200px">
				<TABLE id="tableHintSave" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameSave" src="../Cost/SavingWating.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<input id="txtServer" type="hidden" name="txtServer" runat="server"> <input id="txtDatabase" type="hidden" name="txtDatabase" runat="server">
			<input id="txtIsContinue" type="hidden" name="txtIsContinue" runat="server"> <input id="txtCount" type="hidden" name="txtCount" runat="server">
			<div style="DISPLAY:none">
				<!--
						<TABLE height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD vAlign="top" align="left"><cc1:dtsprogress id="DtsProgress" runat="server"></cc1:dtsprogress></TD>
							</TR>
						</TABLE>
				--></div>
		</form>
		<script language="javascript">
	function winload()
	{
		if (Form1.txtIsContinue.value == "1")
//		if (isDtsContinue)
		{
			DtsExecReturn("", "0", -1, 0);
//			document.all.btnDtsContinue.click();
		}
	}
	
	function DtsStart()
	{
		return true;
	}
	
	function Clear()
	{
		var i = Form1.sltProject.selectedIndex;
		var ProjectName = Form1.sltProject.options[i].text;
		
		if(!window.confirm('确实要清空 ' + ProjectName + ' 已导入的销售数据吗？'))
			return false;
			
		document.all.divHintSave.style.display = "";
			
		return true;
	}
	
	function DtsExecReturn(ErrMess, IsEof, CurrentIndex, ErrCount)
	{
		document.all.tdExecCount.innerText = CurrentIndex + 1;
		document.all.tdErrCount.innerText = ErrCount;
		
		var count = parseInt(Form1.txtCount.value);
		var pos = (CurrentIndex + 1) / count * 100;
		
		document.all.divProgress.style.width = pos + "%";
		
		if (ErrMess != "")
		{
			Form1.txtErr.value = Form1.txtErr.value + ErrMess + "\n";
		}
		
		if (IsEof == "0")
		{
			document.all.frameExec.src = "DtsPayExec.aspx?act=1&server=" + Form1.txtServer.value + "&database=" + Form1.txtDatabase.value;
		}
		else
		{
			Form1.txtIsContinue.value = "0";
			Form1.btnDtsFinish.click();
		}
	}
	
	function Stop()
	{
		if(!window.confirm('确实要终止吗？')) return false;
		
		Form1.txtIsContinue.value = "0";
		Form1.btnDtsFinish.click();
		
		return false;
	}
		</script>
	</body>
</HTML>
