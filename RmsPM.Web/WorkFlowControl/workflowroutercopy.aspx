<%@ Page Language="c#" Inherits="RmsPM.Web.WorkFlowControl.WorkFlowRouterCopy"
    CodeFile="WorkFlowRouterCopy.aspx.cs" %>

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
    <script language="javascript" src="../Images/jdfunc.js" charset="gb2312"></script>    
    <link href="/../Images/index.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                    选择流向</td>
            </tr>
            <tr>
                <td valign="top" colspan="1" rowspan="1">
                <div id="parentdiv" style="overflow: auto; height: 420px">
                    <table cellspacing="7" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td style="border-right: #ededed 3px dotted; padding-right: 7px" valign="top" width="60%">
                                <div runat="server" id="pwdcheckdiv" align="center">
                                    <asp:Button ID="Button2" TabIndex="0" runat="server" Width="0" Height="0" Text=""
                                        OnClick="Button1_Click" />
                                    <br />
                                    <font color="red">请输入验证密码：</font>
                                    <input class="input" type="password" runat="server" id="txtpwd" onkeydown="if(event.keyCode==13){document.all('Button2').click();return false;}" /><br />
                                    <span runat="server" id="msgspan"></span>
                                    <br />
                                    <asp:Button ID="Button1" CssClass="submit" runat="server" Text=" 确 定 " OnClick="Button1_Click" />
                                    <input type="button" onclick="window.close();" class="submit" value=" 取 消 " />
                                </div>
                                <table id="SendTitle" cellspacing="0" cellpadding="0" border="0" runat="server" name="SendTitle">
                                    <tr>
                                        <td class="intopic">
                                            选择流向</td>
                                        <td>
                                            <input id="ChkMail" type="checkbox" name="ChkMail" runat="server"><span runat="server"
                                                id="EmailSpan">Email提醒</span></td>
                                                
                                         <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                          <td>
                                            <input id="chkRate" type="checkbox" name="chkRate" runat="server"/><span runat="server"
                                                id="spanRate">紧急</span>
                                        </td>
                                       
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
                                <table cellspacing="0" cellpadding="0" border="0" class="input" width="100%"><tr><td runat="server" id="SendCopyTd"></td></tr></table>
                                <br>
                                <table id="CopyTitle" cellspacing="0" cellpadding="0" border="0" runat="server">
                                    <tr>
                                        <td class="intopic" id="CopyName" runat="server">
                                        </td>
                                        <td>
                                            <input class="button-small" id="btnAddCopyUser" onclick="addCopyUser(); return false;"
                                                runat="server" type="button" value="选择人员" name="btnAddCopyUser"></td>
                                        <td>
                                            <input id="ChkMailCopy" type="checkbox" name="ChkMailCopy" runat="server"><span runat="server"
                                                id="EmailSpanCopy">Email提醒</span></td>
                                                
                                         <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                         <td>
                                            <input id="chkCopyRate" type="checkbox" name="chkCopyRate" runat="server"><span runat="server"
                                                id="span1">紧急</span>
                                        </td>
                                        
                                    </tr>
                                </table>
                                <table class="input" id="CopyTable" width="100%" runat="server" name="CopyTable">
                                    <tr>
                                        <td id="tdSelectCopyTaskActors" align="left" runat="server">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="tdSelectCopyTaskActorsSelect" align="left" runat="server">
                                            <textarea id="SelectCopyUserNames" style="width: 100%" disabled name="RouterMessage"
                                                rows="5" cols="118" runat="server"></textarea><input id="SelectCopyUserCodes" type="hidden"
                                                    name="SelectCopyUserCodes" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#e4eff6" style="border-top: darkgray 1px solid">
                                            <input id="WaitForFlag" type="checkbox" name="WaitForFlag" runat="server">
                                            等待完成</td>
                                    </tr>
                                </table>
                                <br>
                                <table id="OpinionTitle" cellspacing="0" cellpadding="0" border="0" runat="server">
                                    <tr>
                                        <td class="intopic">
                                            处理意见</td>
                                        <td>
                                            <input id="ChkShow" style="display: none" type="checkbox" checked name="ChkShow"
                                                runat="server"></td>
                                                <td>
                                                
                                                <select id="sltTemplateOpinion" runat="server" visible="false">
                    </select>
                                                </td>
                                                <td><asp:RadioButtonList ID="rdoCheck" runat="server" RepeatColumns="3">
                                                        <asp:ListItem Value="Approve" Text="同意"></asp:ListItem>
                                                        <asp:ListItem Value="Reject" Text="否决"></asp:ListItem>
                                                        <asp:ListItem Value="" Text="待选择" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList></td>
                                    </tr>
                                </table>
                                <table class="input" id="OpinionTable" width="100%" runat="server" name="OpinionTable">
                                    <tr>
                                        <td align="left">
                                            <textarea id="FlowOpinion" style="width: 100%" rows="5" cols="118" runat="server"></textarea>
                                            <uc1:AttachMentAdd ID="AttachMentAdd1" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentAdd>
                                        </td>
                                    </tr>
                                </table>
                                <table id="MessageTitle" cellspacing="0" cellpadding="0" border="0" runat="server">
                                    <tr>
                                        <td class="intopic">
                                            消息</td>
                                        
                                    </tr>
                                </table>
                                <table class="input" id="MessageTable" width="100%" runat="server">
                                    <tr>
                                        <td align="left">
                                            <textarea id="RouterMessage" style="width: 100%" name="RouterMessage" rows="5" cols="118"
                                                runat="server"></textarea></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    </div>
                    <table width="100%" runat="server" id="ButtonTable">
                    
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
                    </asp:DataGrid><asp:DataGrid ID="DataGrid2" runat="server">
                    </asp:DataGrid></td>
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
        <input id="txtTaskCopyActorIDs" type="hidden" name="txtTaskCopyActorIDs" runat="server">
        <input id="txtTaskType" type="hidden" name="txtTaskType" runat="server">
        <input class="input" id="txtUserCode" type="hidden" name="txtUserCode">
        <input class="input" id="txtCopyTaskActorCode" type="hidden" name="txtCopyTaskActorCode"
            runat="server">
        <input class="input" id="txtCopyTaskActorName" type="hidden" name="txtCopyTaskActorName"
            runat="server">
            <input class="input" id="txtSendCopyUserCodes" type="hidden" name="txtSendCopyUserCodes" runat="server" />
    </form>

    <script>
		
	var sss0 = "";
	var nnn0 = "";
	var sssCopy0 = "";
	var nnnCopy0 = "";
	var AuditValue = "";
	


	function getSelectTaskActor()
	{
	    if("<%=this.up_sPMNameLower%>"=="tianyangoa")
	    {
	        if(jsTrim(Form1.FlowOpinion.value)=="")
	        {
	            alert('请填写意见！ ');
				return false;
	        }
	    }
	    if(document.all("rdoCheck_0"))
	    {
	        if(document.all("rdoCheck_0").checked)
	            AuditValue = document.all("rdoCheck_0").value;
	        if(document.all("rdoCheck_1").checked)
	            AuditValue = document.all("rdoCheck_1").value;
	        if(document.all("rdoCheck_2").checked)
	            AuditValue = document.all("rdoCheck_2").value;
	    }
	
	    if('<%=Request["Work"]%>' == "Send")
	    {
		
			if ( Form1.txtSelectRouterCode.value == "" )
			{
				alert('请选择流向！ ');
				return false;
			}
		
			sss0 = "";
			nnn0 = "";
			sssCopy0 = "";
			nnnCopy0 = "";

			var taskActorIDs = Form1.txtTaskActorIDs.value.split(',');
			for ( var j=0; j<taskActorIDs.length;j++)
			{
				getST("sta" + taskActorIDs[j],"0");
			}
			
			var taskCopyActorIDs = Form1.txtTaskCopyActorIDs.value.substr(0,Form1.txtTaskCopyActorIDs.value.length-1).split(',');
			for ( var k=0; k<taskCopyActorIDs.length;k++)
			{
    			if(taskCopyActorIDs[k]!="")
    				getST("sta" + taskCopyActorIDs[k],"1");
			}
			

			if ( sss0 == "" )
			{
				alert ('不能传送到任何用户，请选择用户 ！');
				return false;
			}

//    	    alert(sss0);
	        //alert(sssCopy0);
	        //alert(nnn0);
	        //return false;
	        sssCopy0 = "";
	        nnnCopy0 = "";
	        if(Form1.txtSendCopyUserCodes.value != "")
	            sssCopy0 = Form1.txtSendCopyUserCodes.value;
	        
	        if(document.Form1.WaitForFlag == null)	    
			    window.opener.returnSelectRouterControl( Form1.txtSelectRouterCode.value , Form1.txtSelectRouterName.value , sss0,nnn0, sssCopy0,'<%=Request["Work"]%>',false,Form1.FlowOpinion.value,document.Form1.ChkShow.checked,Form1.RouterMessage.value,Form1.ChkMail.checked,AuditValue,Form1.chkRate.checked);
			else
			    window.opener.returnSelectRouterControl( Form1.txtSelectRouterCode.value , Form1.txtSelectRouterName.value , sss0,nnn0, sssCopy0,'<%=Request["Work"]%>',document.Form1.WaitForFlag.checked,Form1.FlowOpinion.value,document.Form1.ChkShow.checked,Form1.RouterMessage.value,Form1.ChkMail.checked,AuditValue,Form1.chkRate.checked);
	    }
	    else
	    {
	        sssCopy0 = "";
			nnnCopy0 = "";

			//alert(Form1.txtTaskCopyActorIDs.value);
	        var taskCopyActorIDs = Form1.txtTaskCopyActorIDs.value.substr(0,Form1.txtTaskCopyActorIDs.value.length-1).split(',');
	        
			for ( var j=0; j<taskCopyActorIDs.length;j++)
			{
				getST("sta" + taskCopyActorIDs[j],"1");
			}
			
			if(Form1.SelectCopyUserNames)
			{
				var copyCodes = Form1.SelectCopyUserCodes.value.split(',');
				var copyNames = Form1.SelectCopyUserNames.value.split(',');
				for ( var k=0; k<copyCodes.length;k++)
				{
    				if(copyCodes[k]!="")
    					getSTCopy(copyCodes[k],copyNames[k],"1");
				}
			}
	        
	        //alert(sssCopy0);
	        //return false;
	        if(document.Form1.WaitForFlag == null)	    
	            window.opener.returnSelectRouterControl( "" , "" , "","", sssCopy0,'<%=Request["Work"]%>',false,Form1.FlowOpinion.value,document.Form1.ChkShow.checked,Form1.RouterMessage.value,Form1.ChkMailCopy.checked,AuditValue,Form1.chkCopyRate.checked);
	        else
	            window.opener.returnSelectRouterControl( "" , "" , "","", sssCopy0,'<%=Request["Work"]%>',document.Form1.WaitForFlag.checked,Form1.FlowOpinion.value,document.Form1.ChkShow.checked,Form1.RouterMessage.value,Form1.ChkMailCopy.checked,AuditValue,Form1.chkCopyRate.checked);
	    }
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
			//alert();
			for ( var i=0;i<iCount;i++)
			{
				if ( objs[i].checked )
				{
					if(flag == 0)
					{
						sss += objs[i].value + ',' + objs[i].taskActorID + ',' + objs[i].userName + ',' + objs[i].taskActorName + ';' ;
						nnn += objs[i].userName + ',' ;
					}
					else
					{
					
						//sss += objs[i].value + ',';
						sss += objs[i].value + ',' + objs[i].taskActorID + ',' + objs[i].userName + ',' + objs[i].taskActorName + ';' ;
						nnn += objs[i].userName;
					}
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
					sss += objs.value + ','  + objs.taskActorID + ',' + objs.userName + ',' + objs.taskActorName + ';' ;
					nnn += objs.userName;
			    }
			    else
			    {
			        //sss += objs.value + ',';
			        sss += objs.value + ','  + objs.taskActorID + ',' + objs.userName + ',' + objs.taskActorName + ';' ;
			        nnn += objs.userName;
			        
			    }
			}
		}
		if(flag == "0")
		{
			sss0+=sss;
			nnn0+=nnn;
		}
		else
		{
		    sssCopy0+=sss;
			nnnCopy0+=nnn;
		}
	}
	
	
	function getSTCopy( code , name , flag)
	{
		sssCopy0 += code + ',' + Form1.txtCopyTaskActorCode.value + ',' + name + ',' + Form1.txtCopyTaskActorName.value + ';' ;
		nnnCopy0 += name + ',' ;
	}
	
	function addCopyUser()
	{
		OpenLargeWindow("../SelectBox/SelectSUMain.aspx?Type=U&UserCodes=" + Form1.SelectCopyUserCodes.value , "选择用户");	
	}
	
	function getReturnStationUser( userCodes,userNames,stationCodes,stationNames,flag)
	{
		Form1.SelectCopyUserCodes.value = userCodes;
		Form1.SelectCopyUserNames.value = userNames;		
	}
	
	function NodeClick(Trid)
    {
        //var objTable = document.all("TreeTable");
        var objTr = document.all(Trid);
        var objTable = objTr.parentElement;
        var displayStr = "";
        
        if(objTr.status == "block")
        {
            objTr.status = "none";
            displayStr = "none";
        }
        else
        {
            objTr.status = "block";
            displayStr = "block";
        }
        
        for(var i=0;i<objTable.rows.length;i++)
        {
            var objTableTr = objTable.rows[i];
            var objTrLeftLength = objTr.id.length;
            if(Trid != objTableTr.id && objTableTr.id.substring(0,objTrLeftLength) == Trid)
            {
                objTableTr.style.display = displayStr;
            }
        }
    }

    </script>

</body>
</html>
