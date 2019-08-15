<%@ Page language="c#" Inherits="RmsPM.Web.WorkFlowControl.WorkFlowBackTop" CodeFile="WorkFlowBackTop.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title></title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">选择退回流向</td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="1" rowSpan="1">
						<table cellSpacing="7" cellPadding="0" width="100%" border="0">
							<tr>
								<td style="BORDER-RIGHT: #ededed 3px dotted; PADDING-RIGHT: 7px" vAlign="top" width="60%">
									<table id="SendTitle" cellSpacing="0" cellPadding="0" border="0" runat="server" name="SendTitle">
										<tr>
											<td class="intopic">退回 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span align=right><img src="../Images/arrow_down1.gif"  id="img2" onclick="EventClickTab();"/></span></td>
											
											<td><input id="ChkMail" type="checkbox" name="ChkMail" checked=checked runat="server">Email提醒</td>
										</tr>
									</table>
									<table class="input" id="SendTable" cellSpacing="0" cellPadding="0" width="100%" border="0"
										runat="server" name="SendTable">
										<tr id="sendtr1" style="display:block">
											<td align="center" width="40%"><asp:radiobuttonlist id="rblSelectRouter" runat="server" Width="80%" Height="45px" RepeatColumns="1"
													RepeatDirection="Horizontal" RepeatLayout="Table" AutoPostBack="True"></asp:radiobuttonlist></td>
											<td id="tdSelectTaskActors" align="center" runat="server"></td>
										</tr>
									</table>
									<br>
									<table id="OpinionTitle" cellSpacing="0" cellPadding="0" border="0" runat="server">
										<tr>
											<td class="intopic">公开意见</td>
											<td><input id="ChkShow" type="checkbox" name="ChkShow" runat="server" style="DISPLAY: none"
													checked></td>
										</tr>
									</table>
									<table class="input" id="OpinionTable" width="100%" runat="server" name="OpinionTable">
										<tr>
											<td align="left"><TEXTAREA rows="5" runat="server" id="FlowOpinion" style="WIDTH: 100%" cols="118" NAME="FlowOpinion"></TEXTAREA>
												<uc1:attachmentadd id="AttachMentAdd1" runat="server" CtrlPath="../UserControls/"></uc1:attachmentadd></td>
										</tr>
									</table>
									<br>
									<table id="Table1" cellSpacing="0" cellPadding="0" border="0" runat="server">
										<tr>
											<td class="intopic">隐藏意见</td>
										</tr>
									</table>
									<table class="input" id="Table2" width="100%">
										<tr>
											<td align="left"><TEXTAREA id="RouterMessage" style="WIDTH: 100%" rows="5" cols="118" runat="server" NAME="RouterMessage"></TEXTAREA></td>
										</tr>
									</table>
									<table width="100%">
										<tr>
											<td align="center"><input class="submit" id="btnSend" onclick="getSelectTaskActor(); " type="button" value="发　送">
												<!--<input class="submit" id="btnCancel" onclick="window.close();" type="button" value="取 消"
													name="btnCancel" runat="server">-->
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtProcedureCode" type="hidden" name="txtProcedureCode" runat="server">
			<input id="txtCurrentTaskCode" type="hidden" name="txtCurrentTaskCode" runat="server">
			<input id="txtCurrentActCode" type="hidden" name="txtCurrentActCode" runat="server">
			<input id="txtSelectRouterCode" type="hidden" name="txtSelectRouterCode" runat="server">
			<input id="txtSelectRouterName" type="hidden" name="txtSelectRouterName" runat="server">
			<input id="txtSelectUserCodes" type="hidden" name="txtSelectUserCodes" runat="server">
			<input id="txtSelectUserNames" type="hidden" name="txtSelectUserNames" runat="server">
			<input id="txtTaskActorIDs" type="hidden" name="txtTaskActorIDs" runat="server">
			<input id="txtTaskType" type="hidden" name="txtTaskType" runat="server"> <input class="input" id="txtUserCode" type="hidden" name="txtUserCode">
		</form>
		
		<script>
		
	var sss0 = "";
	var nnn0 = "";
	var sssCopy0 = "";
	var nnnCopy0 = "";

	function getSelectTaskActor()
	{
		if ( Form1.txtSelectRouterCode.value == "" )
		{
			alert('请选择退回流向！ ');
			return false;
		}
	
		sss0 = "";
		nnn0 = "";

		var taskActorIDs = Form1.txtTaskActorIDs.value.split(',');
		for ( var j=0; j<taskActorIDs.length;j++)
		{
			getST("sta" + taskActorIDs[j],"0");
		}
		
		if ( sss0 == "" )
		{
			alert ('不能传送到任何用户，请选择用户 ！');
			return false;
		}
		window.opener.returnSelectRouterControl( Form1.txtSelectRouterCode.value , Form1.txtSelectRouterName.value , sss0,nnn0, '','Back',false,Form1.FlowOpinion.value,document.Form1.ChkShow.checked,Form1.RouterMessage.value,Form1.ChkMail.checked);

		window.close();

	}
	
	
	function getST( id ,flag)
	{

		var sss = "";
		var nnn = "";
		
		var objs = document.all( id );

		if ( objs && objs[0] )
		{

			var iCount = objs.length;
			for ( var i=0;i<iCount;i++)
			{
				if ( objs[i].checked )
				{
					sss += objs[i].value + ',' + objs[i].taskActorID + ',' + objs[i].userName + ',' + objs[i].taskActorName + ';' ;
					nnn += objs[i].userName + ',' ;
				}
			}
		}
		else if ( objs )
		{
		    if(objs.checked)
		    {
		        if(flag == "0")
		        {
					//sss = objs.value + ',' + objs.userName + ','  + objs.taskActorID + ',' + objs.taskActorName + ';' ;
					sss = objs.value + ','  + objs.taskActorID + ',' + objs.userName + ',' + objs.taskActorName + ';' ;
					nnn = objs.userName;
			    }
			    else
			    {
			        sss = objs.value + ',';
			        nnn = objs.userName;
			        
			    }
			}
		}
		if(flag == "0")
		{
			sss0+=sss;
			nnn0+=nnn;
		}
	}
	
	function EventClickTab()
    {
         var objTable = document.all("SendTable");
         
         var objTableTr=objTable.rows["sendtr1"];
    
        
        if( objTableTr.style.display=="none")
        {
            objTableTr.style.display = "block";
           
        }
        else
        {
            objTableTr.style.display = "none";
          
        }
    }
    


		</script>
	</body>
</HTML>
