<%@ Page language="c#" Inherits="RmsPM.Web.CashFlow.RptConstructProgList" CodeFile="RptConstructProgList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RptConstructProgList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<style>
.list-t1 { FONT-WEIGHT: bold; BACKGROUND-COLOR: #f0fff0; TEXT-ALIGN: left }
		</style>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white">
				<tr style="">
					<td>
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td width="100%" height="25" valign="bottom" class="note">工程进度分析表——<span runat="server" id="lblYm"></span></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td>
						<div id="divMain" style="OVERFLOW: auto; WIDTH: 100%; POSITION: absolute; HEIGHT: 100%">
							<table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
								<tr>
									<td nowrap class="form-item">项目规划建筑面积</td>
									<td nowrap width="80"></td>
									<td nowrap class="form-item">住宅规划建筑面积</td>
									<td nowrap width="80"></td>
									<td nowrap class="form-item">商业规划建筑面积</td>
									<td nowrap width="80"></td>
									<td nowrap class="form-item">其他规建面积</td>
									<td nowrap width="80"></td>
									<td nowrap class="form-item">项目销售周期</td>
									<td nowrap width="80"></td>
								</tr>
								<tr>
									<td nowrap class="form-item">项目调整建筑面积</td>
									<td nowrap width="80"></td>
									<td nowrap class="form-item">住宅调整建筑面积</td>
									<td nowrap width="80"></td>
									<td nowrap class="form-item">商业调整建筑面积</td>
									<td nowrap width="80"></td>
									<td nowrap class="form-item">其他调整建面</td>
									<td nowrap width="80"></td>
									<td nowrap class="form-item">项目销售启动</td>
									<td nowrap width="80"></td>
								</tr>
							</table>
							<br>
							<table cellSpacing="0" cellPadding="0" border="0" width="100%">
								<tr>
									<td>
										<table class="list" cellSpacing="0" cellPadding="0" border="0" width="100%">
											<tr class="list-title" align="center">
												<td noWrap rowSpan="2" colspan="2"></td>
												<td noWrap colSpan="3">本月</td>
												<td noWrap colSpan="2">后期预测</td>
												<td noWrap colSpan="3">当年累计</td>
												<td noWrap colSpan="3">项目累计</td>
											</tr>
											<tr class="list-title" align="center">
												<td nowrap>实际发生</td>
												<td nowrap>预算/测算</td>
												<td nowrap>对比%</td>
												<td nowrap>1个月内</td>
												<td nowrap>3个月内</td>
												<td nowrap>实际发生</td>
												<td nowrap>预算/测算</td>
												<td nowrap>对比%</td>
												<td nowrap>实际发生</td>
												<td nowrap>预算/测算</td>
												<td nowrap>对比%</td>
											</tr>
											<tr>
												<td nowrap rowspan="12" class="list-c">工<br>程<br>量</td>
												<td nowrap class="sum-item">工程开工量(平米)</td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
											</tr>
											<tr>
												<td nowrap class="list-c">&nbsp;&nbsp;&nbsp;住宅</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
											<tr>
												<td nowrap class="list-c">&nbsp;&nbsp;&nbsp;商业</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
											<tr>
												<td nowrap class="list-c">&nbsp;&nbsp;&nbsp;其他</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
											<tr>
												<td nowrap class="sum-item">工程在建量(平米)</td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
											</tr>
											<tr>
												<td nowrap class="list-c">&nbsp;&nbsp;&nbsp;住宅</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
											<tr>
												<td nowrap class="list-c">&nbsp;&nbsp;&nbsp;商业</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
											<tr>
												<td nowrap class="list-c">&nbsp;&nbsp;&nbsp;其他</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
											<tr>
												<td nowrap class="sum-item">工程竣工量(平米)</td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
												<td nowrap class="sum"></td>
											</tr>
											<tr>
												<td nowrap class="list-c">&nbsp;&nbsp;&nbsp;住宅</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
											<tr>
												<td nowrap class="list-c">&nbsp;&nbsp;&nbsp;商业</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
											<tr>
												<td nowrap class="list-c">&nbsp;&nbsp;&nbsp;其他</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td>
										<table class="list" cellSpacing="0" cellPadding="0" border="0" width="100%">
											<tr class="list-title" align="center">
												<td nowrap rowspan="8" class="list-c">进<br>度</td>
												<td noWrap>项目开发主要结点进度</td>
												<td nowrap>计划开始时间</td>
												<td nowrap>计划耗时</td>
												<td nowrap>实际开始时间</td>
												<td nowrap>截止本月耗时</td>
												<td nowrap>截止本月完成率</td>
												<td nowrap>完成总开发周期</td>
												<td nowrap>备注说明</td>
											</tr>
											<tr>
												<td nowrap class="list-c">前期四证取得</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
											<tr>
												<td nowrap class="list-c">项目动拆迁及土地平整</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
											<tr>
												<td nowrap class="list-c">桩基及基础施工</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
											<tr>
												<td nowrap class="list-c">项目《预售许可证》取得</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
											<tr>
												<td nowrap class="list-c">项目开盘</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
											<tr>
												<td nowrap class="list-c">项目主体施工（达到预售条件）</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
											<tr>
												<td nowrap class="list-c">项目《住宅交付使用许可证》取得</td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
												<td nowrap></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
			</table>
			<div id="divHintLoad" style="DISPLAY: none; LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 50px">
				<TABLE id="tableHintLoad" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="center"><iframe id="frameLoad" src="../Cost/LoadingPrepare.htm" frameBorder="no" width="100%" scrolling="auto"
								height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</div>
			<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server"> <input id="txtYear" type="hidden" name="txtYear" runat="server"><input id="txtMonth" type="hidden" name="txtMonth" runat="server">
		</form>
	</body>
</HTML>
