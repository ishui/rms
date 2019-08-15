<%@ Page language="c#" Inherits="RmsPM.Web.Cost.BudgetInfo" CodeFile="BudgetInfo.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>费用预算信息</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body onload="IniBody(); return false;">
		<form id="Form1" method="post" runat="server">
			<table height="97%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="white">
				<TR>
					<TD class="topic" align="center" background="../images/topic_bg.gif" height="25" id="tdTitle"
						runat="server">费用预算表&nbsp;&nbsp;&nbsp;
					</TD>
				</TR>
				<tr height="25">
					<td class="tools-area" vAlign="top">
						<INPUT class="button" onclick="doModify('');return false;" type="button" value="制定预算" id="btnModify"
							name="Button1" runat="server"> <INPUT class="button" onclick="doModify('Detail');return false;" type="button" value="细化预算"
							id="btnModifyDetail" name="btnModifyDetail" runat="server"> <input class="button" id="btnClose" onclick="window.self.close();" type="button" value="关 闭"
							name="btnClose" runat="server">
					</td>
				</tr>
				<tr height="1">
					<td class="note">单位（万元）</td>
				</tr>
				<TR>
					<td class="topic" vAlign="top" align="center">
						<table style=" DISPLAY: none" cellSpacing="0" cellPadding="0" width="100%" class="list"
							align="center" border="0" id="tableMain">
							<tr align="center">
								<td noWrap rowSpan="2">费用名称</td>
								<td noWrap rowSpan="2">估算费用</td>
								<td noWrap rowSpan="2">预算费用</td>
								<td noWrap rowSpan="2">预算前累计发生</td>
								<td noWrap colSpan="13">本期预算</td>
								<td noWrap colSpan="5">后续预算</td>
							</tr>
							<tr>
								<td noWrap id="tdCurrentYear" runat="server">本期预算</td>
								<td noWrap width="60" id="tdMonthTitle1">1</td>
								<td noWrap width="60" id="tdMonthTitle2">2</td>
								<td noWrap width="60" id="tdMonthTitle3">3</td>
								<td noWrap width="60" id="tdMonthTitle4">4</td>
								<td noWrap width="60" id="tdMonthTitle5">5</td>
								<td noWrap width="60" id="tdMonthTitle6">6</td>
								<td noWrap width="60" id="tdMonthTitle7">7</td>
								<td noWrap width="60" id="tdMonthTitle8">8</td>
								<td noWrap width="60" id="tdMonthTitle9">9</td>
								<td noWrap width="60" id="tdMonthTitle10">10</td>
								<td noWrap width="60" id="tdMonthTitle11">11</td>
								<td noWrap width="60" id="tdMonthTitle12">12</td>
								<td noWrap id="tdYearTitle0" runat="server">后续总预算</td>
								<td noWrap id="tdYearTitle1" runat="server">后续1期</td>
								<td noWrap id="tdYearTitle2" runat="server">后续2期</td>
								<td noWrap id="tdYearTitle3" runat="server">后续3期</td>
								<td noWrap id="tdYearTitle4" runat="server">后续4期</td>
								<td noWrap id="tdYearTitle5" runat="server">后续5期</td>
								<td noWrap id="tdYearTitle6" runat="server">后续6期</td>
								<td noWrap id="tdYearTitle7" runat="server">后续7期</td>
								<td noWrap id="tdYearTitle8" runat="server">后续8期</td>
								<td noWrap id="tdYearTitle9" runat="server">后续9期</td>
								<td noWrap id="tdYearTitle10" runat="server">后续10期</td>
							</tr>
							<asp:repeater id="repeat1" runat="server">
								<ItemTemplate>
									<tr id='<%# "tr" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' pCode ='<%# DataBinder.Eval(Container.DataItem, "ParentCode").ToString() %>' ChildCount='<%# DataBinder.Eval(Container.DataItem, "ChildCount").ToString() %>'
									fCode = '<%# "@"  + DataBinder.Eval(Container.DataItem, "FullCode").ToString() %>'
									code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' 
									isEnd=""
									 >
										<td nowrap  class='<%# "list-" +  DataBinder.Eval(Container.DataItem, "Deep").ToString() %>'>
											<font color="GrayText">
												<%# DataBinder.Eval(Container.DataItem, "SortID") %>
											</font>&nbsp;&nbsp; <a id='<%# "aDown" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' href="##" onclick="doExpand(this.code,'0')" code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'  >
												<img src="../images/Plus.gif" border="0"></a> <a id='<%# "aUp" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  href="##" onclick="doExpand(this.code,'1')" code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
												<img src="../images/Minus.gif" border="0"></a> &nbsp;&nbsp;
											<a href=## onclick='ViewBudgetInfo(code);'
											code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' >
											<%# DataBinder.Eval(Container.DataItem, "CostName") %></a>
										</td>
										<td nowrap align="right"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "TotalMoney") ) %></td>
										<td nowrap align="right"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "BudgetCost") ) %></td>
										<td nowrap align="right"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "BeforeHappenCost") ) %></td>
										<td nowrap align="right"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "CurrentPlanCost") ) %></td>
										<td nowrap align="right" id="tdMonth1"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "CurrentPlanCost1") ) %></td>
										<td nowrap align="right" id="tdMonth2"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "CurrentPlanCost2") ) %></td>
										<td nowrap align="right" id="tdMonth3"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "CurrentPlanCost3") ) %></td>
										<td nowrap align="right" id="tdMonth4"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "CurrentPlanCost4") ) %></td>
										<td nowrap align="right" id="tdMonth5"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "CurrentPlanCost5") ) %></td>
										<td nowrap align="right" id="tdMonth6"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "CurrentPlanCost6") ) %></td>
										<td nowrap align="right" id="tdMonth7"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "CurrentPlanCost7") ) %></td>
										<td nowrap align="right" id="tdMonth8"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "CurrentPlanCost8") ) %></td>
										<td nowrap align="right" id="tdMonth9"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "CurrentPlanCost9") ) %></td>
										<td nowrap align="right" id="tdMonth10"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "CurrentPlanCost10") ) %></td>
										<td nowrap align="right" id="tdMonth11"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "CurrentPlanCost11") ) %></td>
										<td nowrap align="right" id="tdMonth12"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "CurrentPlanCost12") ) %></td>
										<td nowrap align="right" id="tdYear0"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "AfterPlanCost") ) %></td>
										<td nowrap align="right" id="tdYear1"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "AfterPlanCost1") ) %></td>
										<td nowrap align="right" id="tdYear2"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "AfterPlanCost2") ) %></td>
										<td nowrap align="right" id="tdYear3"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "AfterPlanCost3") ) %></td>
										<td nowrap align="right" id="tdYear4"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "AfterPlanCost4") ) %></td>
										<td nowrap align="right" id="tdYear5"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "AfterPlanCost5") ) %></td>
										<td nowrap align="right" id="tdYear6"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "AfterPlanCost6") ) %></td>
										<td nowrap align="right" id="tdYear7"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "AfterPlanCost7") ) %></td>
										<td nowrap align="right" id="tdYear8"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "AfterPlanCost8") ) %></td>
										<td nowrap align="right" id="tdYear9"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "AfterPlanCost9") ) %></td>
										<td nowrap align="right" id="tdYear10"><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container.DataItem, "AfterPlanCost10") ) %></td>
									</tr>
								</ItemTemplate>
							</asp:repeater>
						</table>
					</td>
				</TR>
			</table>
			<input type="hidden" runat="server" id="txtYear"> <input type="hidden" runat="server" id="txtMonth" NAME="txtMonth">
			<input type="hidden" runat="server" id="txtAfterPeriod" NAME="txtAfterPeriod"> <input type="hidden" runat="server" id="txtPeriodMonth" NAME="txtPeriodMonth">
			<input id="txtAllCode" type="hidden" runat="server" NAME="txtAllCode">
		</form>
		<script language="javascript">
<!--


	var IMaxMonth = 12;
	var IMaxPeriod = 10;

	function IniBody()
	{
		var iYear = parseInt( Form1.txtYear.value);
		var iMonth = parseInt( Form1.txtMonth.value);
		var periodMonth = parseInt ( Form1.txtPeriodMonth.value);
		var afterPeriod = parseInt ( Form1.txtAfterPeriod.value);
	

		var obj = document.all("tdMonth1");
		var iCount = obj.length;
		
		for ( var i=1;i<=IMaxMonth;i++)
		{
			var monthObj = document.all( "tdMonth" + i );
			if ( obj[0] )
			{
				for ( var j=0;j<iCount;j++)
				{
					if ( i> periodMonth)
						monthObj[j].style.display = "none";
				}
			}
			else
			{
				if ( i> periodMonth)
					monthObj.style.display = "none";
			}
			
			if ( i> periodMonth)
				document.all( "tdMonthTitle" + i ).style.display = "none";
			
			document.all("tdMonthTitle" + i).innerText = getNextMonth(iYear,iMonth,i) ;
			
		}


		for ( var i=1;i<=IMaxPeriod;i++)
		{
			var yearObj = document.all( "tdYear" + i );
			if ( obj[0] )
			{
				for ( var j=0;j<iCount;j++)
				{
					if ( i> afterPeriod)
						yearObj[j].style.display = "none";
				}
			}
			else
			{
				if ( i> afterPeriod)
					yearObj.style.display = "none";
			}
			
			if ( i> afterPeriod)
				document.all( "tdYearTitle" + i ).style.display = "none";
		}

		var allCodesString = Form1.txtAllCode.value;
		var codes = allCodesString.split(',');
		var iCount0 = codes.length;	

		for ( var i=0;i<iCount0;i++)
		{
			var codetemp = codes[i];
			if ( codetemp != "" )
			{
				var trObj = document.all( "tr"+codetemp );
				

				if ( parseInt(trObj.ChildCount) > 0 )
				{
					trObj.isEnd = "0";
					document.all( "aDown"+codetemp  ).style.display="none"  ;
				}
				else
				{
					trObj.isEnd = "1";
					
					document.all( "aDown"+codetemp  ).style.display="none"  ;
					document.all( "aUp"+codetemp  ).style.display="none"  ;
				}

			}
		}
		
		document.all("tableMain").style.display = "";
	
	}


	function doExpand( code, flag)
	{
		var tr = document.all("tr"+code );
		var fullCode = tr.fCode;
		tr.isEnd = flag ;

		if ( flag == "1" )
		{
	
			if ( parseInt(tr.ChildCount) > 0 )
			{
				document.all( "aUp"+code  ).style.display="none"  ;
				document.all( "aDown"+code  ).style.display=""  ;
			}
						
		}
		else if ( flag == "0" )
		{

			if ( parseInt(tr.ChildCount) > 0 )
			{
				document.all( "aUp"+code  ).style.display=""  ;
				document.all( "aDown"+code  ).style.display="none"  ;
			}

		}
		
		var allCodesString = Form1.txtAllCode.value;
		var codes = allCodesString.split(',');
		var iCount = codes.length;
		for ( var i=0;i<iCount;i++)
		{
			var codetemp = codes[i];
			if ( codetemp != "" )
			{
				var trObj = document.all( "tr"+codetemp );
				if ( flag == "1" )
				{
					if ( HasString( trObj.fCode,fullCode ) && trObj.code != code  )
					{
						trObj.style.display = "none";
					}
				}
				else if ( flag=="0" )
				{
					if ( trObj.pCode == code )
					{
						trObj.style.display = "";
						doExpand(trObj.code,trObj.isEnd);
					}
				}
			}
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
	
	function doModify(type)
	{
		window.navigate( '../Cost/BudgetModify.aspx?ProjectCode=<%=Request["ProjectCode"]%>&BudgetCode=<%=Request["BudgetCode"]%>&CostCode=<%=Request["CostCode"]%>&Type=' + type ,'制定预算'  );
	}

	function ViewBudgetInfo(costCode )
	{
		window.navigate('../Cost/BudgetInfo.aspx?ProjectCode=<%=Request["ProjectCode"]%>&BudgetCode=<%=Request["BudgetCode"]%>&CostCode=' + costCode );
	}
	

//-->
		</script>
	</body>
</HTML>
