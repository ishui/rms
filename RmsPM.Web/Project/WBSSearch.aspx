<%@ Page language="c#" CodeFile="WBSSearch.aspx.cs" AutoEventWireup="false" Inherits="RmsPM.Web.Project.WBSSearch" codePage="936"%>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>WBSSearch</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		function SubmitCondition()
		{
			var m_Condition = "";
			
			m_Condition = "?TaskName=" + escape(document.all.txtTaskName.value);
			m_Condition += "&Master=" + escape(document.all.txtMaster.value);
			m_Condition += "&Status=" + document.all.lstStatus.value;
			m_Condition += "&ImportantLevel=" + document.all.lstImportantLevel.value;
			m_Condition += "&Exceed=" + document.all.lstExceed.value;
			m_Condition += "&StartDate=" + document.all.dtbStartDate.value;
			m_Condition += "&EndDate=" + document.all.dtbEndDate.value;
			
			window.parent.SelectTask(m_Condition);
			
		}
		
		function Body_Load()
		{
			var i = 0 ;
			var obj = null;
			for (i = 0 ; i < document.all.length;i++)
			{
				obj = document.all[i];
				if (obj.tagName.toUpperCase() == "INPUT" || obj.tagName.toUpperCase() == "SELECT" ) 
				{
					obj.value = (obj.name == "btnSearch")?obj.value:"";
				}
			}
		}
		
		</script>
</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
										<table cellSpacing="0" cellPadding="0" width="100%" border="0">
											<tr>
												<td class="search-area">
				
					<table height=60  cellSpacing="10" cellPadding="0" width="100%" border="0">
						<TR width="100%">
							<TD >工作名称/编码:</TD>
							<TD><input style="WIDTH: 120px" type="text" name="txtTaskName">
							</TD>
							<TD >负责人:</TD>
							<TD><input style="WIDTH: 120px" type="text" name="txtMaster">
							</TD>
							<TD >工作状态:</TD>
							<TD><select style="WIDTH: 100px" name="lstStatus">
									<option value="" selected>--请选择--</option>
									<option value="0">未开始</option>
									<option value="1">进行中</option>
									<option value="2">暂停</option>
									<option value="3">取消</option>
									<option value="4">已完成</option>
								</select>
							</TD>
							<TD >重要程度:</TD>
							<TD><select style="WIDTH: 100px" name="lstImportantLevel">
									<OPTION value="" selected>--请选择--</OPTION>
									<OPTION value="0">一般</OPTION>
									<option value="1">重要</option>
								</select></TD>
						</TR>
						<TR>
							<TD >是否超期:</TD>
							<TD><select style="WIDTH: 100px" name="lstExceed">
									<OPTION value="" selected>--请选择--</OPTION>
									<OPTION value="0">是</OPTION>
									<option value="1">否</option>
								</select></TD>
							<TD >开始时间：</TD>
							<TD><cc3:calendar id="dtbStartDate" runat="server" CalendarResource="../Images/CalendarResource/"
									ReadOnly="False" Display="True"></cc3:calendar></TD>
							<TD >结束时间：</TD>
							<TD><cc3:calendar id="dtbEndDate" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False"
									Display="True"></cc3:calendar></TD>
							<td align="center" colSpan="2"><input class="submit" id="btnSearch" onclick="SubmitCondition();return false;" type="button"
									value="搜 索" name="btnSearch">
							</td>
						</TR>
					</table>
				</td>
			</tr>
			<tr height="100%"><td>&nbsp;</td></tr>
			</table>
		</form>
	</body>
</HTML>
