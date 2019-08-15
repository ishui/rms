<%@ Page language="c#" Inherits="RmsPM.Web.Cost.BatchCostEstimate" CodeFile="BatchCostEstimate.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>费用估算</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body onkeydown="if(event.keyCode==13) event.keyCode=9" onload="IniBody();">
		<form id="Form1" method="post" runat="server">
			<table height="97%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="white" id=tableFull>
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">
						<asp:Label id="lblProjectName" runat="server">Label</asp:Label>&nbsp;费用估算</td>
				</tr>
				<TR>
					<td class="topic" vAlign="top" align="center">
						<table class="list" cellSpacing="0" cellPadding="0" width="98%" align="center" border="0"
							id="tableMain" style="DISPLAY:none">
							<tr align="center" class="list-title">
								<td noWrap>费用项名称</td>
								<td noWrap>单 价（元）</td>
								<td noWrap>计量单位</td>
								<td noWrap>数量</td>
								<td noWrap>估算金额（万元）</td>
								<td noWrap>工作预算参考（万元）</td>
							</tr>
							<asp:repeater id="repeat1" runat="server">
								<ItemTemplate>
									<tr id='<%# "tr" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' pCode ='<%# DataBinder.Eval(Container.DataItem, "ParentCode").ToString() %>' ChildCount='<%# DataBinder.Eval(Container.DataItem, "ChildCount").ToString() %>'
									fCode = '<%# "@"  + DataBinder.Eval(Container.DataItem, "FullCode").ToString() %>'
									code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' 
									AccountPoint='<%# DataBinder.Eval(Container.DataItem, "AccountPoint") %>' 
									isEnd=""
									 >
										<td nowrap class='<%# "list-" +  DataBinder.Eval(Container.DataItem, "Deep").ToString() %>'>
											<font color="GrayText">
												<%# DataBinder.Eval(Container.DataItem, "SortID") %>
											</font>&nbsp;&nbsp; <a id='<%# "aDown" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' href="##" onclick="doExpand(this.code,'0')" code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'  >
												<img src="../images/Plus.gif" border="0"></a> <a id='<%# "aUp" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  href="##" onclick="doExpand(this.code,'1')" code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
												<img src="../images/Minus.gif" border="0"></a> &nbsp;&nbsp;
											<%# DataBinder.Eval(Container.DataItem, "CostName") %>
											<input type=hidden  id=txtCostCode value='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' NAME="txtCostCode" runat=server>
											<input type=hidden id='<%# "txtCostName" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  value='<%# DataBinder.Eval(Container.DataItem, "CostName") %>' NAME="txtCostName">
										</td>
										<td nowrap align="right">
											<input  type="text" id='<%# "txtUnitPrice" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' size=9  class=input-nember   value='<%# RmsPM.BLL.StringRule.BuildGeneralNumberString(DataBinder.Eval(Container, "DataItem.UnitPrice")) %>' code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
											<div id='<%# "divUnitPrice" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' ><%# RmsPM.BLL.StringRule.BuildGeneralNumberString(DataBinder.Eval(Container, "DataItem.UnitPrice")) %></div>
										</td>
										<td nowrap align="right">
											<input  type="text" size=4 id='<%# "txtMeasurementUnit" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  class=input   value='<%# DataBinder.Eval(Container, "DataItem.MeasurementUnit") %>' code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
											<div id='<%# "divMeasurementUnit" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' ><%# DataBinder.Eval(Container, "DataItem.MeasurementUnit") %></div>
										</td>
										<td nowrap align="right">
											<input id='<%# "txtProjectQuantity" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' type="text" size=10  class=input-nember   value='<%# RmsPM.BLL.StringRule.BuildGeneralNumberString(DataBinder.Eval(Container, "DataItem.ProjectQuantity")) %>' code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
											<div id='<%# "divProjectQuantity" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' ><%# RmsPM.BLL.StringRule.BuildGeneralNumberString(DataBinder.Eval(Container, "DataItem.ProjectQuantity")) %>
											</div>
											<input type=button class="button-small" id='<%# "btnS" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  value="计算" onclick="doSSingle(this.code);" code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
										</td>
										<td nowrap align="right">
											<input id='<%# "txtTotalMoney" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>' type="text" size=10   class=input-nember   value='<%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container, "DataItem.TotalMoney") ) %>' code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>'>
											<div id='<%# "divTotalMoney" +  DataBinder.Eval(Container.DataItem, "CostCode").ToString() %>'  code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' >
												<%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString( DataBinder.Eval(Container, "DataItem.TotalMoney") ) %>
											</div>
										</td>
										<td nowrap>
										<a href=## code='<%# DataBinder.Eval(Container.DataItem, "CostCode") %>' onclick='showTaskBudget(code);'
										><%# RmsPM.BLL.StringRule.BuildMoneyWanFormatString(  RmsPM.BLL.CBSRule.SumTaskBudgetByCostEx ( DataBinder.Eval(Container, "DataItem.CostCode").ToString() ) ) %></a>
										</td>
									</tr>
								</ItemTemplate>
							</asp:repeater></table>
						<table width="100%" cellspacing="10">
							<TR>
								<TD align="center"><INPUT class="submit" id="btnSave" onclick=" if ( !doGetResult() ) return false;  " type="button"
										value="确 定" runat="server" NAME="btnSave" onserverclick="btnSave_ServerClick">&nbsp;&nbsp; <INPUT class="submit" onclick="window.close();" type="button" value="取 消">
								</TD>
							</TR>
						</table>
					</td>
				</TR>
			</table>
<iframe id="iframeSave" style="DISPLAY: none" src="../Cost/SavingWating.htm" frameBorder="no"
				width="100%" scrolling="auto" height="70%"></iframe>


			<input id="txtAllCode" type="hidden" runat="server"> <input type="hidden" runat="server" id="txtResult" NAME="txtResult">
		</form>
		<script language="javascript">
<!--

	var ERR_NUMBER = "非法的数值！";



	function doSave()
	{
		document.all("iframeSave").style.display = "";
		document.all("tableFull").style.display = "none";
		return true;
	}

	function undoHidden()
	{
		document.all("iframeSave").style.display = "none";
		document.all("tableFull").style.display = "";
	}


	function IniBody()
	{
		var allCodesString = Form1.txtAllCode.value;
		var codes = allCodesString.split(',');
		var iCount = codes.length;
		for ( var i=0;i<iCount;i++)
		{
			var codetemp = codes[i];
			if ( codetemp != "" )
			{
				var trObj = document.all( "tr"+codetemp );
				

				if ( parseInt(trObj.ChildCount) > 0   )
				{
					trObj.isEnd = "0";
					
					document.all( "btnS"+codetemp  ).style.display="none"  ;
					document.all( "aDown"+codetemp  ).style.display="none"  ;
					document.all( "txtUnitPrice"+codetemp  ).style.display="none"  ;
					document.all( "txtMeasurementUnit"+codetemp  ).style.display="none"  ;
					document.all( "txtProjectQuantity"+codetemp  ).style.display="none"  ;
					document.all( "txtTotalMoney"+codetemp  ).style.display="none"  ;
				}
				else
				{
					trObj.isEnd = "1";
					
					document.all( "aDown"+codetemp  ).style.display="none"  ;
					document.all( "aUp"+codetemp  ).style.display="none"  ;
					document.all( "divUnitPrice"+codetemp  ).style.display="none"  ;
					document.all( "divMeasurementUnit"+codetemp  ).style.display="none"  ;
					document.all( "divProjectQuantity"+codetemp  ).style.display="none"  ;
					document.all( "divTotalMoney"+codetemp  ).style.display="none"  ;
				}
			}
		}
		
		IniExpandAccountPoint();
		undoHidden();
		document.all("tableMain").style.display = "";
	
	}
	

	function IniExpandAccountPoint()
	{
		var allCodesString = Form1.txtAllCode.value;
		var codes = allCodesString.split(',');
		var iCount = codes.length;
		for ( var i=0;i<iCount;i++)
		{
			var codetemp = codes[i];
			if ( codetemp != "" )
			{
				var trObj = document.all( "tr"+codetemp );
				var ap = parseInt(trObj.AccountPoint);
				if ( ap == 1 )
					doExpand(codetemp,1);
			}
		}
	}

	function doSSingle(code)
	{
		var upString = document.all( "txtUnitPrice"+code ).value;
		var pqString = document.all( "txtProjectQuantity"+code ).value;

		if ( upString == "" )
		{
			alert ( "单价： 请填写数值"  );
			return;
		}
		
		if ( pqString == "" )
		{
			alert ( "数量： 请填写数值"  );
			return;
		}
		
		if (!checknumber( upString  ,"单价")){
			return ;
		}
		
		if (!checknumber( pqString ,"数量")){
			return ;
		}
	
		var up = parseFloat(upString);
		var pq = parseFloat(pqString);
		document.all( "txtTotalMoney"+code ).value = up*pq/10000;

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
			
			document.all( "btnS"+code  ).style.display=""  ;
			document.all( "txtUnitPrice"+code  ).style.display=""  ;
			document.all( "txtMeasurementUnit"+code  ).style.display=""  ;
			document.all( "txtProjectQuantity"+code  ).style.display=""  ;
			document.all( "txtTotalMoney"+code  ).style.display=""  ;
			
			document.all( "divUnitPrice"+code  ).style.display="none"  ;
			document.all( "divMeasurementUnit"+code  ).style.display="none"  ;
			document.all( "divProjectQuantity"+code  ).style.display="none"  ;
			document.all( "divTotalMoney"+code  ).style.display="none"  ;
			
		}
		else if ( flag == "0" )
		{

			if ( parseInt(tr.ChildCount) > 0 )
			{
				document.all( "aUp"+code  ).style.display=""  ;
				document.all( "aDown"+code  ).style.display="none"  ;
			}
			
			document.all( "btnS"+code  ).style.display="none"  ;
			document.all( "txtUnitPrice"+code  ).style.display="none"  ;
			document.all( "txtMeasurementUnit"+code  ).style.display="none"  ;
			document.all( "txtProjectQuantity"+code  ).style.display="none"  ;
			document.all( "txtTotalMoney"+code  ).style.display="none"  ;
			
			document.all( "divUnitPrice"+code  ).style.display=""  ;
			document.all( "divMeasurementUnit"+code  ).style.display=""  ;
			document.all( "divProjectQuantity"+code  ).style.display=""  ;
			document.all( "divTotalMoney"+code  ).style.display=""  ;
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

	function doGetResult()
	{
		var allCodesString = Form1.txtAllCode.value;
		var codes = allCodesString.split(',');
		var iCount = codes.length;
		var re = "";
		
		for ( var i=0;i<iCount;i++)
		{
			var codetemp = codes[i];
			if ( codetemp != "" )
			{
			
				var trObj = document.all( "tr"+codetemp );
				if ( trObj.style.display == "" && trObj.isEnd == "1" )
				{
			
					var costName = document.all( "txtCostName" + codetemp ).value  ;
					if ( ! checknumber( document.all( "txtUnitPrice" + codetemp ).value  , costName + "- 单价" ) )
					{
						return false;
					}

					if ( ! checknumber( document.all( "txtProjectQuantity" + codetemp ).value , costName + "- 数量" ) )
					{
						return false;
					}
					
					if ( ! checknumber( document.all( "txtTotalMoney" + codetemp ).value , costName + "- 估算金额" ) )
					{
						return false;
					}
				}
			}
		}
		
		
		for ( var i=0;i<iCount;i++)
		{
			var codetemp = codes[i];
			if ( codetemp != "" )
			{
				var trObj = document.all( "tr"+codetemp );
				if ( trObj.style.display == "" && trObj.isEnd == "1"  )
				{
					re+=trObj.code + ",";
					re+="T" +",";
					re+=document.all( "txtUnitPrice"+codetemp ).value +",";
					re+=document.all( "txtMeasurementUnit"+codetemp ).value +",";
					re+=document.all( "txtProjectQuantity"+codetemp ).value +",";
					re+=document.all( "txtTotalMoney"+codetemp ).value +";";
				}
				else
				{
					re+=trObj.code + ",F,,,,;";
				}
			}
		}
		Form1.txtResult.value = re;
		doSave();
		return true;
	}

	function showTaskBudget( costCode )
	{
		OpenLargeWindow ( '../Cost/RelationTaskBudget.aspx?CostCode=' + costCode ,'相关工作预算' );
	}
	
//-->
		</script>
	</body>
</HTML>
