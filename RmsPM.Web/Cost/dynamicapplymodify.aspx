<%@ Page language="c#" Inherits="RmsPM.Web.Cost.DynamicApplyModify" CodeFile="DynamicApplyModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>��̬���������ƶ�</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body onkeydown="if(event.keyCode==13) event.keyCode=9" onload='IniBody();'>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0"
				id="tableFull">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">��̬��������</td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center" width="100%">
						<table width="100%">
							<tr>
								<td class="note">��λ����Ԫ</td>
							</tr>
						</table>
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="form-item">����ԭ��</TD>
								<TD colSpan="5"><TEXTAREA id="txtReason" style="WIDTH: 80%" name="Textarea1" rows="2" cols="20" runat="server"></TEXTAREA><font color="red">*</font></TD>
							</TR>
							<tr height="3">
								<td colSpan="6"></td>
							</tr>
							<TR>
								<TD class="form-item" width="13%">�� �� �ˣ�</TD>
								<TD width="20%"><asp:label id="lblApplyPersonName" runat="server"></asp:label></TD>
								<TD class="form-item" width="13%">����ʱ�䣺</TD>
								<TD width="20%"><asp:label id="lblApplyDate" runat="server"></asp:label></TD>
								<TD class="form-item" width="13%"></TD>
								<TD width="20%"></TD>
							</TR>
						</table>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="intopic" width="200">���õ���</td>
								<td>&nbsp;<asp:Label ID="lblCostName" Runat="server">&nbsp;</asp:Label></td>
								<td><input class="button-small" id="btnContinue" onclick="doSelectCost(); return false;" type="button"
										value="��������" name="btnSelectCost" runat="server" onserverclick="btnContinue_ServerClick"> <input type="button" id="btnDeleteItem" runat="server" value="ɾ ��" class="button-small" onserverclick="btnDeleteItem_ServerClick">
									<input onclick="if ( !doSubmit()) return false;" class="button-small" id="btnSave" type="button"
										value="�� ��" name="btnSave" runat="server" onserverclick="btnSave_ServerClick">&nbsp; <input class="button-small" id="btnCancel" onclick="javascript:self.close()" type="button"
										value="ȡ ��" name="btnCancel">
								</td>
								<td></td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" border="0" width="100%" id="tableItemMain" runat="server">
							<tr>
								<td>
									<table class="list" cellSpacing="0" cellPadding="0" align="left" border="0">
										<TR>
											<td rowSpan="3"></td>
											<td rowSpan="3">��ǰ�ܼ�</td>
											<td id="tdCurrentPeriod" noWrap align="center" colSpan="12">����Ԥ��</td>
											<td noWrap align="center" colSpan="10">����Ԥ��</td>
										</TR>
										<tr>
											<td id="tdCurrentHappenMonth" align="center" colSpan="3" runat="server">�ѷ���</td>
											<td id="tdAfterHappenMonth" align="center" colSpan="6" runat="server">������</td>
											<td colSpan="10"></td>
										</tr>
										<tr>
											<td id="tdMonthTitle1" noWrap runat="server">1</td>
											<td id="tdMonthTitle2" noWrap runat="server">2</td>
											<td id="tdMonthTitle3" noWrap runat="server">3</td>
											<td id="tdMonthTitle4" noWrap runat="server">4</td>
											<td id="tdMonthTitle5" noWrap runat="server">5</td>
											<td id="tdMonthTitle6" noWrap runat="server">6</td>
											<td id="tdMonthTitle7" noWrap runat="server">7</td>
											<td id="tdMonthTitle8" noWrap runat="server">8</td>
											<td id="tdMonthTitle9" noWrap runat="server">9</td>
											<td id="tdMonthTitle10" noWrap runat="server">10</td>
											<td id="tdMonthTitle11" noWrap runat="server">11</td>
											<td id="tdMonthTitle12" noWrap runat="server">12</td>
											<td id="tdYearTitle1" noWrap runat="server">����1��</td>
											<td id="tdYearTitle2" noWrap runat="server">����2��</td>
											<td id="tdYearTitle3" noWrap runat="server">����3��</td>
											<td id="tdYearTitle4" noWrap runat="server">����4��</td>
											<td id="tdYearTitle5" noWrap runat="server">����5��</td>
											<td id="tdYearTitle6" noWrap runat="server">����6��</td>
											<td id="tdYearTitle7" noWrap runat="server">����7��</td>
											<td id="tdYearTitle8" noWrap runat="server">����8��</td>
											<td id="tdYearTitle9" noWrap runat="server">����9��</td>
											<td id="tdYearTitle10" noWrap runat="server">����10��</td>
										</tr>
										<tr id="trOldBudget" runat="server">
											<td nowrap>ԭԤ��</td>
											<td id="tdPreOldBudget" runat="server"></td>
										</tr>
										<tr id="trAH" runat="server">
											<td nowrap>�ѷ���</td>
											<td id="tdPreAH" runat="server"></td>
										</tr>
										<tr id="trUse" runat="server">
											<td nowrap>��ͬռ��</td>
											<td id="tdPreUse" runat="server"></td>
										</tr>
										<tr id="trApply" runat="server">
											<td nowrap>��ͬ����</td>
											<td id="tdPreApply" runat="server"></td>
										</tr>
										<tr id="trBalance" runat="server">
											<td nowrap>�� ��</td>
											<td id="tdPreSurplus" runat="server"></td>
										</tr>
										<tr id="trNewBudget">
											<td noWrap>������</td>
											<td><input class="input-nember" id="txtPreSurplus" size="8" runat="server" NAME="txtPreSurplus"></td>
											<td id="tdMonthNewBudget1" noWrap align="right"><input class="input-nember" id="txtMonthNewBudget1" size="8" runat="server" NAME="txtMonthNewBudget1"></td>
											<td id="tdMonthNewBudget2" noWrap align="right"><input class="input-nember" id="txtMonthNewBudget2" size="8" runat="server" NAME="txtMonthNewBudget2"></td>
											<td id="tdMonthNewBudget3" noWrap align="right"><input class="input-nember" id="txtMonthNewBudget3" size="8" runat="server" NAME="txtMonthNewBudget3"></td>
											<td id="tdMonthNewBudget4" noWrap align="right"><input class="input-nember" id="txtMonthNewBudget4" size="8" runat="server" NAME="txtMonthNewBudget4"></td>
											<td id="tdMonthNewBudget5" noWrap align="right"><input class="input-nember" id="txtMonthNewBudget5" size="8" runat="server" NAME="txtMonthNewBudget5"></td>
											<td id="tdMonthNewBudget6" noWrap align="right"><input class="input-nember" id="txtMonthNewBudget6" size="8" runat="server" NAME="txtMonthNewBudget6"></td>
											<td id="tdMonthNewBudget7" noWrap align="right"><input class="input-nember" id="txtMonthNewBudget7" size="8" runat="server" NAME="txtMonthNewBudget7"></td>
											<td id="tdMonthNewBudget8" noWrap align="right"><input class="input-nember" id="txtMonthNewBudget8" size="8" runat="server" NAME="txtMonthNewBudget8"></td>
											<td id="tdMonthNewBudget9" noWrap align="right"><input class="input-nember" id="txtMonthNewBudget9" size="8" runat="server" NAME="txtMonthNewBudget9"></td>
											<td id="tdMonthNewBudget10" noWrap align="right"><input class="input-nember" id="txtMonthNewBudget10" size="8" runat="server" NAME="txtMonthNewBudget10"></td>
											<td id="tdMonthNewBudget11" noWrap align="right"><input class="input-nember" id="txtMonthNewBudget11" size="8" runat="server" NAME="txtMonthNewBudget11"></td>
											<td id="tdMonthNewBudget12" noWrap align="right"><input class="input-nember" id="txtMonthNewBudget12" size="8" runat="server" NAME="txtMonthNewBudget12"></td>
											<td id="tdYearNewBudget1" noWrap align="right"><input class="input-nember" id="txtYearNewBudget1" size="8" runat="server" NAME="txtYearNewBudget1"></td>
											<td id="tdYearNewBudget2" noWrap align="right"><input class="input-nember" id="txtYearNewBudget2" size="8" runat="server" NAME="txtYearNewBudget2"></td>
											<td id="tdYearNewBudget3" noWrap align="right"><input class="input-nember" id="txtYearNewBudget3" size="8" runat="server" NAME="txtYearNewBudget3"></td>
											<td id="tdYearNewBudget4" noWrap align="right"><input class="input-nember" id="txtYearNewBudget4" size="8" runat="server" NAME="txtYearNewBudget4"></td>
											<td id="tdYearNewBudget5" noWrap align="right"><input class="input-nember" id="txtYearNewBudget5" size="8" runat="server" NAME="txtYearNewBudget5"></td>
											<td id="tdYearNewBudget6" noWrap align="right"><input class="input-nember" id="txtYearNewBudget6" size="8" runat="server" NAME="txtYearNewBudget6"></td>
											<td id="tdYearNewBudget7" noWrap align="right"><input class="input-nember" id="txtYearNewBudget7" size="8" runat="server" NAME="txtYearNewBudget7"></td>
											<td id="tdYearNewBudget8" noWrap align="right"><input class="input-nember" id="txtYearNewBudget8" size="8" runat="server" NAME="txtYearNewBudget8"></td>
											<td id="tdYearNewBudget9" noWrap align="right"><input class="input-nember" id="txtYearNewBudget9" size="8" runat="server" NAME="txtYearNewBudget9"></td>
											<td id="tdYearNewBudget10" noWrap align="right"><input class="input-nember" id="txtYearNewBudget10" size="8" runat="server" NAME="txtYearNewBudget10"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="intopic" width="200">���õ�����ϸ</td>
								<td>
								</td>
							</tr>
						</table>
						<table class="list" cellSpacing="0" cellPadding="0" width="98%" align="center" border="0">
							<TR>
								<td noWrap width="20%">��������</td>
								<td noWrap>������ϸ</td>
							</TR>
							<asp:repeater id="repeatList" runat="server">
								<ItemTemplate>
									<tr>
										<td>
											<a code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' href="##" onclick="doModifyItem(code); return false;">
												<%# DataBinder.Eval(Container.DataItem, "CostName") %>
											</a>
										</td>
										<td><%# DataBinder.Eval(Container.DataItem, "AdjustString") %></td>
									</tr>
								</ItemTemplate>
							</asp:repeater></table>
					</td>
				</tr>
			</table>
			<iframe id="iframeSave" style="DISPLAY: none" src="../Cost/SavingWating.htm" frameBorder="no"
				width="100%" scrolling="auto" height="70%"></iframe><input id="txtAllCode" type="hidden" name="txtAllCode" runat="server">
			<input id="btnRefreshItem" style="DISPLAY: none" type="button" runat="server" onserverclick="btnRefreshItem_ServerClick"> <input type="hidden" id="txtInputCode" runat="server">
			<input id="txtYear" type="hidden" name="txtYear" runat="server"> <input id="txtMonth" type="hidden" name="txtMonth" runat="server">
			<input id="txtAfterPeriod" type="hidden" name="txtAfterPeriod" runat="server"> <input id="txtPeriodMonth" type="hidden" name="txtPeriodMonth" runat="server">
			<input id="txtDynamicStartMonth" type="hidden" name="txtDynamicStartMonth" runat="server">
			<input id="btnReturnSelectCodes" runat="server" type="button" style="DISPLAY:none" value="ѡ������������ˢ��" onserverclick="btnReturnSelectCodes_ServerClick">
			<input id="txtSelectReturnCodes" runat="server" type="hidden">
			<input id="txtOldSurplusCost" runat="server" type="hidden" NAME="txtOldSurplusCost">
		</form>
		<script language="javascript">
<!--

	function doSave()
	{
		if ( ! checkInput())
			return false;
		document.all("iframeSave").style.display = "";
		document.all("tableFull").style.display = "none";
		return true;
	}

	function undoHidden()
	{
		document.all("iframeSave").style.display = "none";
		document.all("tableFull").style.display = "";
	}



	var ERR_NUMBER = "�Ƿ�����ֵ��";

	var IMaxMonth = 12;
	var IMaxPeriod = 10;

	function IniBody()
	{
		var iYear = parseInt( Form1.txtYear.value);
		var iMonth = parseInt( Form1.txtMonth.value);
		var periodMonth = parseInt ( Form1.txtPeriodMonth.value);
		var afterPeriod = parseInt ( Form1.txtAfterPeriod.value);
		var dynamicStartMonth = parseInt(Form1.txtDynamicStartMonth.value);
		

		// �����ı���
		for ( var j=1;j<dynamicStartMonth;j++)
		{
			document.all( "txtMonthNewBudget" + j ).style.display = "none";
		}

		for ( var i=1;i<=IMaxMonth;i++)
		{
		

			// ����
			if ( i> periodMonth)
				document.all( "tdMonthTitle" + i ).style.display = "none";
			document.all("tdMonthTitle" + i).innerText = getNextMonth(iYear,iMonth,i) ;
		

			// ��Ԥ��
			if ( i> periodMonth)
				document.all( "tdMonthNewBudget" + i ).style.display = "none";

			// ԭԤ��
			if ( i> periodMonth)
				document.all( "tdMonthOldBudget" + i ).style.display = "none";

			// �ѷ���
			if ( i> periodMonth)
				document.all( "tdMonthAH" + i ).style.display = "none";
			
			// ռ��	
			if ( i> periodMonth)
				document.all( "tdMonthUse" + i ).style.display = "none";
			
			

			// ����
			if ( i> periodMonth)
				document.all( "tdMonthApply" + i ).style.display = "none";

			// ���
			if ( i> periodMonth)
				document.all( "tdMonthBlance" + i ).style.display = "none";


		}
		
		
		for ( var i=1;i<=IMaxPeriod;i++)
		{
		
			// ����
			if ( i> afterPeriod)
				document.all( "tdYearTitle" + i ).style.display = "none";
			
			// ��Ԥ��
			if ( i> afterPeriod)
				document.all( "tdYearNewBudget" + i ).style.display = "none";

			// ԭԤ��
			if ( i> afterPeriod)
				document.all( "tdYearOldBudget" + i ).style.display = "none";

			// �ѷ���
			if ( i> afterPeriod)
				document.all( "tdYearAH" + i ).style.display = "none";
			
			// ռ��	
			if ( i> afterPeriod)
				document.all( "tdYearUse" + i ).style.display = "none";

			// ����
			if ( i> afterPeriod)
				document.all( "tdYearApply" + i ).style.display = "none";

			// ���
			if ( i> afterPeriod)
				document.all( "tdYearBalance" + i ).style.display = "none";
				
		}
		
		doDisplayPass();
		undoHidden();
		document.all("tableFull").style.display = "";

	}


	function doDisplayPass()
	{
	
		var iYear = parseInt( Form1.txtYear.value);
		var iMonth = parseInt( Form1.txtMonth.value);
		var periodMonth = parseInt ( Form1.txtPeriodMonth.value);
		var afterPeriod = parseInt ( Form1.txtAfterPeriod.value);
		var dynamicStartMonth = parseInt(Form1.txtDynamicStartMonth.value);
	
		if ( dynamicStartMonth == 1 )
			return;

		var dsp = document.all( "tdCurrentHappenMonth" ).style.display;
		if ( dsp == "" )
		{
			dsp = "none";
			document.all("tdCurrentPeriod").colSpan = periodMonth - dynamicStartMonth +1  ;
		}
		else
		{
			dsp = "";
			document.all("tdCurrentPeriod").colSpan = periodMonth;
		}

		document.all("tdCurrentHappenMonth").style.display = dsp;


		for ( var j=1;j<dynamicStartMonth;j++)
		{
			// ����
			document.all( "tdMonthTitle" + j ).style.display = dsp;

			// ��Ԥ��
			document.all( "tdMonthNewBudget" + j ).style.display = dsp;

			// ԭԤ��
			document.all( "tdMonthOldBudget" + j ).style.display = dsp;

			// �ѷ���
			document.all( "tdMonthAH" + j ).style.display = dsp;
			
			// ռ��	
			document.all( "tdMonthUse" + j ).style.display = dsp;

			// ����
			document.all( "tdMonthApply" + j ).style.display = dsp;

			
			// ���
			document.all( "tdMonthBalance" + j ).style.display = dsp;
		}
		
	}


	function getNextMonth( iYear, iMonth, iPlusMonth )
	{
		var iMonth = iPlusMonth + iMonth - 1 ;
		if ( iMonth > IMaxMonth )
		{
			iYear = iYear +1;
			iMonth = iMonthTemp -IMaxMonth;
		}
		return iYear + "-" + iMonth;
	}

	function checknumber(data,lbl){
		var tmp ;
		if (data == "")
		{
			return true;
		}
		var re = /^[\-\+]?([1-9]\d*|0|[1-9]\d{0,2}(,\d{3})*)(\.\d+)?([Ee][\-\+]?\d+)?$/;
		if (re.test(data)){
			gar = data + '.';
			tmp = gar.split('.');
			if (tmp[0].length > 15) {
					alert(lbl+":"+ERR_NUMBER);
			}
			return true;
		}
		alert(lbl+":"+ERR_NUMBER);
		return false;
	}


	function doSubmit()
	{
		if ( document.all("txtReason").value == "" )
		{
			alert('����д����ԭ�� ��');
			return false;
		}
		return doSave();
	}

	function checkInput ()
	{
		var iYear = parseInt( Form1.txtYear.value);
		var iMonth = parseInt( Form1.txtMonth.value);
		var periodMonth = parseInt ( Form1.txtPeriodMonth.value);
		var afterPeriod = parseInt ( Form1.txtAfterPeriod.value);
		var dynamicStartMonth = parseInt(Form1.txtDynamicStartMonth.value);


		// ���Ӯ��͵�����		
		var sp = document.all("txtPreSurplus").value;
		if ( ! checknumber( sp,"Ӯ��������� " ))
		{
			return false;
		}
		var sspp = 0;
		if ( sp != "" )
			sspp=parseFloat(sp);
		if ( sspp < 0 )
		{
			alert ( 'Ӯ���������Ϊ��������������Ҳ��ܳ���ԭӮ������ ��Ӯ��Ϊ������ʱ��Ӯ����������Ϊ0 �� ' );
			return false;
		}
		var oldsp = Form1.txtOldSurplusCost.value;
		var oldsspp = 0;
		if ( oldsp != "" )
		{
			oldsspp = parseFloat(oldsp);
		}

		if ( oldsspp <= 0 && sspp != 0 )
		{
			alert ( 'Ӯ���������Ϊ��������������Ҳ��ܳ���ԭӮ������ ��Ӯ��Ϊ������ʱ��Ӯ����������Ϊ0 �� ' );
			return false;
		}
		
		if ( oldsspp > 0 && oldsspp < sspp )
		{
			alert ( 'Ӯ���������Ϊ��������������Ҳ��ܳ���ԭӮ������ ��Ӯ��Ϊ������ʱ��Ӯ����������Ϊ0 �� ' );
			return false;
		}
	
		// ���Ԥ�����
		for ( var j=dynamicStartMonth; j<=periodMonth ; j++)
		{
			var vv = document.all( "txtMonthNewBudget" + j ).value;
			if ( ! checknumber( vv,"�¶ȷ��� " ))
			{
				return false;
			}
		}
		
		for ( var j=1; j<=afterPeriod ; j++)
		{
			var vv = document.all( "txtYearNewBudget" + j ).value;
			if ( ! checknumber( vv,"��ȷ��� " ))
			{
				return false;
			}
		}

		return true;
	}
	

	function doModifyItem( costCode )
	{
		Form1.txtSelectReturnCodes.value = costCode;
		Form1.btnReturnSelectCodes.click();
	}

		
	function doSelectCost()
	{
		if ( ! checkInput())
			return false;
		OpenLargeWindow('../SelectBox/SelectCost.aspx?AccountPoint=AccountPoint&Type=Single&ProjectCode=<%=Request["ProjectCode"]%>','ѡ�������');
	}

	function GetReturnSingleCostCode( codes , names )
	{

		if ( HasString( Form1.txtAllCode.value,codes ) )
		{
			alert ( names + '�Ѿ�����' );
			return;
		}

		Form1.txtSelectReturnCodes.value = codes;
		doSave();
		Form1.btnReturnSelectCodes.click();
	
	}


		
//-->
		</script>
	</body>
</HTML>
