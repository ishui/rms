<%@ Control Language="c#" Inherits="RmsPM.Web.Project.WBSAlertStatus" CodeFile="WBSAlertStatus.ascx.cs" %>
<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
<script language=javascript>
	function DoAction(action)
	{
		var status = '<%=strStatus%>';
		if(status=='2'&&action=='Start') action = 'Retart';
		OpenCustomWindow("WBSStatus.aspx?Status="+action+"&WBSCode=<%=strTaskCode%>&ProjectCode=<%=Request["ProjectCode"]%>","",500,300);
			
	}
</script>
<input class="button" type="button" id="btStart" value="开始工作" onclick="javascript:DoAction('Start'); return false;"
	runat="server" onserverclick="btStart_ServerClick">&nbsp; <input class="button" type="button" id="btPause" value="暂停工作" runat="server" onclick="javascript:DoAction('Pause'); return false;" onserverclick="btPause_ServerClick">
<input class="button" type="button" id="btCancel" value="取消工作" runat="server" onclick="javascript:DoAction('Cancel'); return false;" onserverclick="btCancel_ServerClick">
<input class="button" type="button" id="btFinish" value="完成工作" runat="server" onclick="DoAction('Finish'); return false;" onserverclick="btFinish_ServerClick"></asp:button>
