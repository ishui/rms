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
							<TD >��������/����:</TD>
							<TD><input style="WIDTH: 120px" type="text" name="txtTaskName">
							</TD>
							<TD >������:</TD>
							<TD><input style="WIDTH: 120px" type="text" name="txtMaster">
							</TD>
							<TD >����״̬:</TD>
							<TD><select style="WIDTH: 100px" name="lstStatus">
									<option value="" selected>--��ѡ��--</option>
									<option value="0">δ��ʼ</option>
									<option value="1">������</option>
									<option value="2">��ͣ</option>
									<option value="3">ȡ��</option>
									<option value="4">�����</option>
								</select>
							</TD>
							<TD >��Ҫ�̶�:</TD>
							<TD><select style="WIDTH: 100px" name="lstImportantLevel">
									<OPTION value="" selected>--��ѡ��--</OPTION>
									<OPTION value="0">һ��</OPTION>
									<option value="1">��Ҫ</option>
								</select></TD>
						</TR>
						<TR>
							<TD >�Ƿ���:</TD>
							<TD><select style="WIDTH: 100px" name="lstExceed">
									<OPTION value="" selected>--��ѡ��--</OPTION>
									<OPTION value="0">��</OPTION>
									<option value="1">��</option>
								</select></TD>
							<TD >��ʼʱ�䣺</TD>
							<TD><cc3:calendar id="dtbStartDate" runat="server" CalendarResource="../Images/CalendarResource/"
									ReadOnly="False" Display="True"></cc3:calendar></TD>
							<TD >����ʱ�䣺</TD>
							<TD><cc3:calendar id="dtbEndDate" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False"
									Display="True"></cc3:calendar></TD>
							<td align="center" colSpan="2"><input class="submit" id="btnSearch" onclick="SubmitCondition();return false;" type="button"
									value="�� ��" name="btnSearch">
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
