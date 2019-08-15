<%@ Page language="c#" Inherits="RmsPM.Web.SelectBox.SelectSUMain" CodeFile="SelectSUMain.aspx.cs" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>ѡ���û�</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet" />
    <link href="../Images/TreeView.css" type="text/css" rel="stylesheet" />

    <script language="javascript" type="text/javascript" src="../Rms.js"></script>

    <style type="text/css">
     .default
    {
	    border: solid 0px #90B7D4;
	    border-bottom-color:#90B7D4;
	    border-bottom-width:0px;
	    background-color:#EAEAEA;
	    font-size:9pt;
	    font-family:����;
	    font-weight:bolder;
	    color:Gray;
    }
    .over
    {
	    margin-left:1px;
	    margin-right:1px;
	    background:#90B7D4;
	    border:solid 1px #90B7D4;
	    border-bottom-width:0px;
	    vertical-align:bottom;
	    color:White;
	    cursor:hand;
	    filter:progid:DXImageTransform.Microsoft.Gradient(startColorStr='#A4C9E4', endColorStr='#85ADCB', gradientType='0');
	    vertical-align:middle;	    
    }
    </style>
</head>
<body bottommargin="0" leftmargin="0" topmargin="0" scroll="no" rightmargin="0" onload="winload();">
    <form id="Form1" method="post" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0" bgcolor="#ffffff">
            <tr>
                <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                    ѡ����Ա</td>
            </tr>
            <tr>
                <td height="100%">
                    <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="1">
                        <tr>
                            <td style="vertical-align: text-top; width: 34%;">
                                <div visible="false" runat="server" id="titlehead" class="default" style="text-align: center; width: 100%; vertical-align: top;"
                                    nowrap="true">
                                    <span class="over" id="stationtitle" onmouseover="titleover('stationtitle');" onmouseout="titleout('stationtitle');"
                                        onclick="titleclick('stationtitle');" style="text-align: center; width: 50%;">��λ��Ա</span><span
                                            id="grouptitle" style="text-align: center; width: 50%;" onmouseover="titleover('grouptitle');"
                                            onmouseout="titleout('grouptitle')" onclick="titleclick('grouptitle');">������Ա</span>
                                </div>
                                <table class="table" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td>
                                            <iframe id="frameTree" src='../Cost/LoadingPrepare.htm' width="100%" height="100%"
                                                frameborder="0" scrolling="auto"></iframe>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="33%" id="tdFrameS">
                                <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td class="note">
                                                        ѡ���λ</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" height="60%">
                                            <iframe id="frameStation" src="" width="100%" height="100%" frameborder="1"></iframe>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="10" width="100%" border="0">
                                                <tr>
                                                    <td align="center">
                                                        <img style="cursor: hand" onclick="appendSelectedS();" src="../images/member_add.gif"
                                                            title="���" border="0">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <img style="cursor: hand" onclick="removeSelectedS();" src="../images/member_del.gif"
                                                            title="�Ƴ�" border="0">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr height="100%">
                                        <td valign="top">
                                            <div style="overflow: auto; width: 100%; height: 100%">
                                                <table class="list" id="tableSelectedStation" cellspacing="0" cellpadding="0" border="0">
                                                    <tr class="list-title">
                                                        <td width="0">
                                                        </td>
                                                        <td width="0">
                                                            <input type="checkbox" id="chkAll" onclick="selectAllCheck(Form1.chkAll, Form1.chkShow);"
                                                                title="ȫѡ">
                                                        </td>
                                                        <td nowrap width="50%">
                                                            ��λ</td>
                                                        <td nowrap width="50%">
                                                            ����</td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="100%" id="tdFrameU">
                                <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td class="note">
                                                        ѡ����Ա</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" height="60%">
                                            <iframe id="frameUser" name="frameUser" src="" width="100%" height="100%" frameborder="1">
                                            </iframe>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="10" width="100%" border="0">
                                                <tr>
                                                    <td align="center">
                                                        <img style="cursor: hand" onclick="appendSelectedU();" src="../images/member_add.gif"
                                                            title="���" border="0" alt="">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <img style="cursor: hand" onclick="removeSelectedU();" src="../images/member_del.gif"
                                                            title="�Ƴ�" border="0" alt="">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr height="100%">
                                        <td valign="top">
                                            <div style="overflow: auto; width: 100%; height: 100%">
                                                <table class="list" id="tableSelectedUser" cellspacing="0" cellpadding="0" width="100%"
                                                    border="0">
                                                    <tr class="list-title">
                                                        <td width="40">
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" id="chkAllUser" onclick="selectAllCheck(Form1.chkAllUser, Form1.chkShowUser);"
                                                                title="ȫѡ">
                                                        </td>
                                                        <td width="100%">
                                                            ��Ա</td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" cellpadding="10">
                        <tr>
                            <td rowspan="2" align="center">
                                <input class="submit" id="btnSave" type="button" value="ȷ ��" onclick="returnStationUser();return false;" />
                                <input class="submit" id="btnCancel" onclick="window.close();" type="button" value="ȡ ��" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <input type="hidden" id="txtSelectedSU" runat="server" />
        <input type="hidden" id="txtSelectS" runat="server" name="txtSelectS" /><input type="hidden"
            id="txtSelectU" runat="server" name="txtSelectU" />
        <input type="hidden" id="txtUserCodes" runat="server" name="txtUserCodes" /><input
            type="hidden" id="txtStationCodes" runat="server" name="txtStationCodes" />
        <input type="hidden" id="txtType" runat="server" name="txtType" />
    </form>

    <script language="javascript" type="text/javascript">

	arsStation = new Array(); //����ѡ�еĸ�λ
	arsUser = new Array(); //����ѡ�е���Ա

	splitAccessRangeText(arsStation, Form1.txtSelectS);   // �������ķַ���Χ����
	splitAccessRangeText(arsUser, Form1.txtSelectU);   // �������ķַ���Χ����

	showSelectARStation();
	showSelectARUser();
	var tempID = 'stationtitle';
	
	function titleover(eleid)
	{
	    if(eleid != tempID)
	    {
	        document.getElementById(eleid).className='over';
	        document.getElementById(tempID).className='default';
	    }
	}
	
	function titleout(eleid)
	{
	    if(eleid != tempID)
	    {
	        document.getElementById(eleid).className='default';
	        document.getElementById(tempID).className='over';
	    }
	}
	
	function titleclick(eleid)
	{
	    if(eleid != tempID)
	    {
	        if(eleid == 'grouptitle')
	        {
	            document.all.frameTree.src ='SelectDefineGroup.aspx';
	            document.all.frameUser.src ='';
	        }
	        else
	        {
	             document.all.frameTree.src = 'SelectStationPersonIncludeS.aspx?ProjectCode=<%=Request["ProjectCode"]%>';
	             document.all.frameUser.src ='';
	        }
	        tempID = eleid;
	    }
	
	}
	function winload()
	{
		if (Form1.txtType.value.toUpperCase() == "U")
		{
			//ֻѡ�û�����ѡ��λ
			document.all.tdFrameS.style.display = "none";
			//���ṹ��ʾ����λ
			document.all.frameTree.src = 'SelectStationPersonIncludeS.aspx?ProjectCode=<%=Request["ProjectCode"]%>';
		}
		else
		{
			//��ѡ��λʱ�����ṹ��ʾ���Ӳ���
			document.all.frameTree.src = 'SelectStationPerson.aspx?ProjectCode=<%=Request["ProjectCode"]%>';
            document.all.titlehead.style.display = "none"
		}
	}
	
	// ��������SU
	function addSelectedSU( inputArray, ars)
	{
		for ( var i=0;i<inputArray.length; i++)
		{
			var relationCodeTemp = inputArray[i].relationCode;
			var accessRangeTypeTemp = inputArray[i].accessRangeType;
			var nameTemp = inputArray[i].name;
			var unitFullName = inputArray[i].unitFullName;
			var isFounded = false;
			for ( var j=0;j<ars.length;j++)
			{
				if ( relationCodeTemp == ars[j].relationCode && accessRangeTypeTemp == ars[j].accessRangeType )
				{
					isFounded = true;
					break;
				}
			}
			if ( ! isFounded )
			{
				ars.push( new AccessRange(relationCodeTemp,accessRangeTypeTemp,nameTemp,unitFullName));
			}
		}
	}

	// �Ƴ�SU
	function removeSU( relationCode, accessRangeType, ars)
	{
		var index = targetIndex ( relationCode, accessRangeType, ars);
		if ( index > -1 )
		{
			var o = ars.splice(index,1);
		}
	}

	//�����ڼ����е�����
	function targetIndex( relationCode, accessRangeType, ars )
	{
		var iCount = ars.length;
		var index = -1;
		
		for ( var i=0; i<iCount;i++)
		{
			if ( ars[i].relationCode == relationCode && ars[i].accessRangeType==accessRangeType )
			{
				index = i;
				break;
			}
		}
		return index;
	}

	//��Ӹ�λ
	function appendSelectedS()
	{
		if (document.all.frameStation.src == "")
		{
			alert("����ѡ��Ҫ��ӵĸ�λ");
			return;
		}
			
		var inputAR = frameStation.getSelectSU();
		
		if ((!inputAR) || (inputAR.length == 0))
		{
			alert("����ѡ��Ҫ��ӵĸ�λ");
			return;
		}
		
		addSelectedSU(inputAR, arsStation);
		showSelectARStation();
	}
	
	//����û�
	function appendSelectedU()
	{
		if (document.all.frameUser.src == "")
		{
			alert("����ѡ��Ҫ��ӵ���Ա");
			return;
		}
			
		var inputAR = frameUser.getSelectSU();
		
		if ((!inputAR) || (inputAR.length == 0))
		{
			alert("����ѡ��Ҫ��ӵ���Ա");
			return;
		}
		
		addSelectedSU(inputAR, arsUser);
		showSelectARUser();
	}
	
	//�Ƴ���λ
	function removeSelectedS()
	{
		var obj = document.all("chkShow");
		var SelectCount = 0;
		
		if (!obj)
		{
			alert("����ѡ��Ҫ�Ƴ��ĸ�λ");
			return;
		}
		
		if ( obj[0])
		{
			for ( var i=0;i<obj.length;i++)
			{
				if ( obj[i].checked)
				{
					SelectCount++;
					removeSU(obj[i].relationCode,obj[i].accessRangeType, arsStation);
				}
			}
		}
		else if ( obj )
		{
			if ( obj.checked )
			{
				SelectCount++;
				removeSU(obj.relationCode,obj.accessRangeType, arsStation);
			}
		}

		if (SelectCount == 0)
		{
			alert("����ѡ��Ҫ�Ƴ��ĸ�λ");
			return;
		}
				
		showSelectARStation();
	}

	//�Ƴ��û�
	function removeSelectedU()
	{
		var obj = document.all("chkShowUser");
		var SelectCount = 0;
		
		if (!obj)
		{
			alert("����ѡ��Ҫ�Ƴ�����Ա");
			return;
		}
		
		if ( obj[0])
		{
			for ( var i=0;i<obj.length;i++)
			{
				if ( obj[i].checked)
				{
					SelectCount++;
					removeSU(obj[i].relationCode,obj[i].accessRangeType, arsUser);
				}
			}
		}
		else if ( obj )
		{
			if ( obj.checked )
			{
				SelectCount++;
				removeSU(obj.relationCode,obj.accessRangeType, arsUser);
			}
		}

		if (SelectCount == 0)
		{
			alert("����ѡ��Ҫ�Ƴ�����Ա");
			return;
		}
				
		showSelectARUser();
	}


	function splitAccessRangeText(ars, txt)
	{
		var arStrings = txt.value;
		
		var us = arStrings.split(";");
		var iCount = us.length;
		for ( var i= 0; i<iCount ; i++)
		{
			if ( us[i] != "" )
			{
				var temp = us[i].split(",");
				var tempobject = new AccessRange(temp[0],temp[1],temp[2],temp[3]);
				ars.push(tempobject);
			}
		}
	}

	//��ʾ��ѡ�ĸ�λ
	function showSelectARStation()
	{
		var ars = arsStation;
		var tab = document.all("tableSelectedStation");
		clearTableExceptTitle(tab);
		var iCount = ars.length;
		for ( var i=0;i<iCount;i++)
		{
			var row1 = tab.insertRow();
			var td1 = row1.insertCell();
			var td2 = row1.insertCell();
			var td3 = row1.insertCell();
			var td4 = row1.insertCell();
			if ( ars[i].accessRangeType == 0 )
			{
				var img = document.createElement("<img border=0 width=15 heitht=15 src='../Images/user.gif'>")
				td1.appendChild(img);
			}
			else if ( ars[i].accessRangeType == 1 )
			{
				var img = document.createElement("<img border=0 width=15 heitht=15 src='../Images/group.gif'>")
				td1.appendChild(img);
			}
			
			var chk = document.createElement("<INPUT TYPE='checkbox' nameTemp='"+ars[i].name+"' relationCode='" + ars[i].relationCode + "' accessRangeType='" + ars[i].accessRangeType + "' id=chkShow VALUE=''   >")
			td2.appendChild(chk);
			
			td3.innerText = ars[i].name;
			td4.innerText = ars[i].unitFullName
		}
	}

	//��ʾ��ѡ���û�
	function showSelectARUser()
	{
		var ars = arsUser;
		var tab = document.all("tableSelectedUser");
		clearTableExceptTitle(tab);
		var iCount = ars.length;
		for ( var i=0;i<iCount;i++)
		{
			var row1 = tab.insertRow();
			var td1 = row1.insertCell();
			var td2 = row1.insertCell();
			var td3 = row1.insertCell();
			if ( ars[i].accessRangeType == 0 )
			{
				var img = document.createElement("<img border=0 width=15 heitht=15 src='../Images/user.gif'>")
				td1.appendChild(img);
			}
			else if ( ars[i].accessRangeType == 1 )
			{
				var img = document.createElement("<img border=0 width=15 heitht=15 src='../Images/group.gif'>")
				td1.appendChild(img);
			}
			
			var chk = document.createElement("<INPUT TYPE='checkbox' nameTemp='"+ars[i].name+"' relationCode='" + ars[i].relationCode + "' accessRangeType='" + ars[i].accessRangeType + "' id=chkShowUser VALUE=''   >")
			td2.appendChild(chk);
			
			td3.innerText = ars[i].name;
		}
	}


	function returnSelectedSU(inputAR)
	{
		addSelectedSU(inputAR);
		showSelectAR();
	}

	//��ձ�
	function clearTableExceptTitle( tab )
	{
		var iCount =tab.rows.length;
		if ( iCount< 1 ) 
			return;
		
		for ( var i=iCount-1; i>=1 ; i--)
		{
			tab.deleteRow(i);
		}

	}

	//ȫѡ
	function selectAllCheck(chkAll, obj)
	{
		if ( obj[0])
		{
			for ( var i=0;i<obj.length;i++)
			{
				obj[i].checked = chkAll.checked;
			}
		}
		else
		{
			obj.checked = chkAll.checked;
		}
	}

	function returnStationUser()
	{
	  
		var userCodes="";
		var stationCodes ="";
		var userNames="";
		var stationNames="";
		var iCount;
		
		iCount = arsStation.length;
		for( var i=0;i<iCount;i++)
		{
			if(stationCodes!='')
			{
				stationCodes+=',';
				stationNames+=',';
			}
			stationCodes+=arsStation[i].relationCode;
			stationNames+=arsStation[i].name;
		}

		iCount = arsUser.length;
		for( var i=0;i<iCount;i++)
		{
			if(userCodes!='')
			{
				userCodes+=',';					
				userNames+=',';					
			}
			userCodes+=arsUser[i].relationCode;
			userNames+=arsUser[i].name;
		}

/*
		var iCount = ars.length;
		for( var i=0;i<iCount;i++)
		{
			if ( ars[i].accessRangeType == 0  )
			{
				if(userCodes!='')
				{
					userCodes+=',';					
					userNames+=',';					
				}
				userCodes+=ars[i].relationCode;
				userNames+=ars[i].name;
			}
			else if ( ars[i].accessRangeType == 1  )
			{
				if(stationCodes!='')
				{
					stationCodes+=',';
					stationNames+=',';
				}
				stationCodes+=ars[i].relationCode;
				stationNames+=ars[i].name;
			}
		}
*/
		var flag = '<%=Request["Flag"]%>';
		
		//alert(userCodes + "\n" + userNames + "\n" + stationCodes + "\n" + stationNames);
		window.opener.<%=ViewState["ReturnFunc"]%>( userCodes,userNames,stationCodes,stationNames,flag);
		window.close(); 
	}

    </script>

</body>
</html>
