<%@ Page Language="c#" Inherits="RmsPM.Web.WorkFlowControl.WorkFlowBack" CodeFile="WorkFlowBack.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title></title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Images/index.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>

</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                    选择退回流向</td>
            </tr>
            <tr>
                <td valign="top" colspan="1" rowspan="1">
                    <div id="parentdiv" style="overflow: auto; height: 420px">
                        <table cellspacing="7" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td style="border-right: #ededed 3px dotted; padding-right: 7px" valign="top" width="60%">
                                    <table id="SendTitle" cellspacing="0" cellpadding="0" border="0" runat="server" name="SendTitle">
                                        <tr>
                                            <td class="intopic">
                                                选择退回流向</td>
                                            <td>
                                                <input id="ChkMail" type="checkbox" name="ChkMail" runat="server">Email提醒</td>
                                        </tr>
                                    </table>
                                    <table class="input" id="SendTable" cellspacing="0" cellpadding="0" width="100%"
                                        border="0" runat="server" name="SendTable">
                                        <tr>
                                            <td align="center" width="40%">
                                                <asp:RadioButtonList ID="rblSelectRouter" runat="server" Width="80%" Height="45px"
                                                    RepeatColumns="1" RepeatDirection="Horizontal" RepeatLayout="Table" AutoPostBack="True"
                                                    OnSelectedIndexChanged="rblSelectRouter_SelectedIndexChanged">
                                                </asp:RadioButtonList></td>
                                            <td id="tdSelectTaskActors" align="center" runat="server">
                                            </td>
                                        </tr>
                                    </table>
                                    <br>
                                    <table id="OpinionTitle" cellspacing="0" cellpadding="0" border="0" runat="server">
                                        <tr>
                                            <td class="intopic">
                                                处理意见</td>
                                            <td>
                                                <input id="ChkShow" type="checkbox" name="ChkShow" runat="server" style="display: none"
                                                    checked></td>
                                        </tr>
                                    </table>
                                    <table class="input" id="OpinionTable" width="100%" runat="server" name="OpinionTable">
                                        <tr>
                                            <td align="left">
                                                <textarea rows="5" runat="server" id="FlowOpinion" style="width: 100%" cols="118"
                                                    name="FlowOpinion"></textarea>
                                                <uc1:AttachMentAdd ID="AttachMentAdd1" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentAdd>
                                            </td>
                                        </tr>
                                    </table>
                                    <br>
                                    <table id="Table1" cellspacing="0" cellpadding="0" border="0" runat="server">
                                        <tr>
                                            <td class="intopic">
                                                消息</td>
                                        </tr>
                                    </table>
                                    <table class="input" id="Table2" width="100%">
                                        <tr>
                                            <td align="left">
                                                <textarea id="RouterMessage" style="width: 100%" rows="5" cols="118" runat="server"
                                                    name="RouterMessage"></textarea></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <input class="submit" id="btnSend" onclick="getSelectTaskActor(); " type="button"
                                    value="确 定">
                                <input class="submit" id="btnCancel" onclick="window.close();" type="button" value="取 消"
                                    name="btnCancel" runat="server">
                            </td>
                        </tr>
                    </table>
                    <asp:DataGrid ID="DataGrid1" runat="server">
                    </asp:DataGrid>
                    <asp:DataGrid ID="DataGrid2" runat="server">
                    </asp:DataGrid>
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
        <input id="txtTaskType" type="hidden" name="txtTaskType" runat="server">
        <input class="input" id="txtUserCode" type="hidden" name="txtUserCode">
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
		//alert(taskActorIDs);
		for ( var j=0; j<taskActorIDs.length;j++)
		{
			getST("sta" + taskActorIDs[j],"0");
		}
		
		if ( sss0 == "" )
		{
			alert ('不能传送到任何用户，请选择用户 ！');
			return false;
		}
		//alert(sss0);
		//return false;
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
    </script>

</body>
</html>
