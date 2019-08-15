<%@ Reference Control="~/workflowcontrol/workflowtoolbar.ascx" %>
<%@ Reference Control="~/usercontrols/inputcost.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.ContractFlow.ContractModify" CodeFile="ContractModify.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc3" TagName="InputCost" Src="../UserControls/InputCost.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>合同修改</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY: none"><input id="btnPayConditionReturn" type="button" value="btnPayConditionReturn" name="btnPayConditionReturn"
					runat="server" onserverclick="btnPayConditionReturn_ServerClick"> <input id="btnAddTaskReturn" type="button" value="btnAddTaskReturn" name="btnAddTaskReturn"
					runat="server" onserverclick="btnAddTaskReturn_ServerClick">
			</div>
			<table id="tableMain" height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff"
				border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">合同信息
						<asp:label id="lblTitle" runat="server" BackColor="Transparent"></asp:label></td>
				</tr>
				<tr>
					<td vAlign="top">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="form-item" width="10%">合同名称：</TD>
								<TD width="23%"><INPUT class="input" id="txtContractName" type="text" size="32" name="txtContractName"
										runat="server"><font color="red">*</font></TD>
								<TD class="form-item" width="10%">合同编号：</TD>
								<TD width="23%"><INPUT class="input" id="txtContractID" type="text" name="txtContractID" runat="server"></TD>
								<TD class="form-item" width="10%">部门：</TD>
								<TD width="23%"><input class="input" id="txtUnitName" readOnly type="text" name="txtUnit" runat="server">
									<input class="input" id="txthUnit" type="hidden" name="txthUnit" runat="server">
									<IMG style="CURSOR: hand" onclick="SelectUnit();return false;" src="../images/ToolsItemSearch.gif"><font color="red">*</font>
								</TD>
							</TR>
							<tr>
								<TD class="form-item">供 应 商：</TD>
								<TD><INPUT class="input" id="txtSupplierName" readOnly type="text" size="32" name="txtSupplierName"
										runat="server"><FONT color="#ff0000">*</FONT> <INPUT id="txtSupplierCode" style="WIDTH: 18px; HEIGHT: 22px" type="hidden" name="txtSupplierCode"
										runat="server"> <A onclick="doSelectSupplier();return false;" href="##"><IMG src="../images/ToolsItemSearch.gif" border="0"></A></TD>
								<TD class="form-item">第 三 方：</TD>
								<TD><INPUT class="input" id="txtThirdParty" type="text" size="32" name="Text1" runat="server"></TD>
								<TD class="form-item">合同类型：</TD>
								<TD><uc1:inputsystemgroup id="inputSystemGroup" runat="server"></uc1:inputsystemgroup></TD>
							</tr>
							<TR>
								<TD class="form-item">合同标的：</TD>
								<TD colSpan="5"><TEXTAREA id="txtContractObject" style="WIDTH: 90%" name="txtContractObject" rows="2" runat="server"></TEXTAREA></TD>
							</TR>
							<TR>
								<TD class="form-item">对应工作：</TD>
								<TD colSpan="3"><A href="#" onclick="OpenTask();return false;"><span id="spanTaskName" runat="server"></span></A><input id="txtTaskName" type="hidden" name="txtTaskName" runat="server">
									<input id="txtWBSCode" type="hidden" name="txtWBSCode" runat="server"> <A href="#" onclick="SelectTask();return false;">
										<IMG src="../images/ToolsItemSearch.gif" border="0"></A> <font color="red">*</font>
								</TD>
								<td id="lblOldMoney" runat="server"></td>
								<td><INPUT class="input" id="oldMoney" onblur="FormatInput(this);" type="text" size="32" name="oldMoney"
										runat="server" Visible="false"></td>
							</TR>
							<TR>
								<TD class="form-item">经 办 人：</TD>
								<TD><input class="input-readonly" id="txtContractPersonName" readOnly type="text" runat="server">
									<input id="txtContractPerson" readOnly type="hidden" runat="server"> <A href="#" onclick="SelectContractPerson();return false;">
										<IMG src="../images/ToolsItemSearch.gif" border="0"></A>
								</TD>
								<TD class="form-item">附件文档：</TD>
								<TD colSpan="3"><uc1:attachmentadd id="myAttachMentAdd" runat="server"></uc1:attachmentadd></TD>
							</TR>
							<TR>
								<TD class="form-item">备注：</TD>
								<TD colSpan="5"><TEXTAREA id="txtRemark" style="WIDTH: 90%" name="Textarea1" rows="2" runat="server"></TEXTAREA></TD>
							</TR>
							<TR>
								<TD class="form-item">采 购 单：</TD>
								<TD colSpan="5"><select runat="server" id="PurchaseSelect"></select></TD>
							</TR>
							<tr height="5">
								<td colSpan="6"></td>
							</tr>
							<TR id="trChange1" runat="server">
								<TD class="form-item">变更原因：</TD>
								<TD colSpan="5"><TEXTAREA id="txtChangeReason" style="WIDTH: 90%" name="txtChangeReason" rows="2" runat="server"></TEXTAREA></TD>
							</TR>
							<TR id="trChange2" runat="server">
								<TD class="form-item">变更内容：</TD>
								<TD colSpan="5"><TEXTAREA id="txtChangeRemark" style="WIDTH: 90%" name="txtChangeRemark" rows="2" runat="server"></TEXTAREA></TD>
							</TR>
						</table>
						<br>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="intopic" width="200">付款计划</td>
								<td><input class="submit" id="btnNewItem" type="button" value="新增款项" name="btnNewItem" runat="server" onserverclick="btnNewItem_ServerClick"></td>
							</tr>
						</table>
						<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<TR>
								<TD><asp:datagrid id="dgCostList" runat="server" ShowFooter="True" CssClass="list" AllowSorting="True"
										AutoGenerateColumns="False" PageSize="15" Width="100%">
										<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
										<HeaderStyle CssClass="list-title"></HeaderStyle>
										<FooterStyle CssClass="list-title"></FooterStyle>
										<Columns>
											<asp:BoundColumn Visible="False" DataField="AllocateCode"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="序号">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<%# Container.ItemIndex + 1 %>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="款项名称&lt;font color=red&gt;*&lt;/font&gt;">
												<ItemTemplate>
													<input size="30" type="text" runat="server" id="txtItemName" value='<%#  DataBinder.Eval(Container.DataItem, "ItemName")  %>' class="input" NAME="txtItemName">
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="计划付款时间&lt;font color=red&gt;*&lt;/font&gt;">
												<ItemTemplate>
													<cc3:calendar id="dtPlanningPayDate" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/" value='<%#  DataBinder.Eval(Container.DataItem, "PlanningPayDate")  %>'>
													</cc3:calendar>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="付款条件">
												<ItemTemplate>
													<span id="spanPayConditionHtml"><input size="30" type="text" runat="server" id="txtPayConditionText" value='<%#  DataBinder.Eval(Container.DataItem, "PayConditionText")  %>' class="input" NAME="txtItemName">
														<br>
														<%#  DataBinder.Eval(Container.DataItem, "PayConditionHtml") %>
													</span>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="费用项&lt;font color=red&gt;*&lt;/font&gt;">
												<ItemTemplate>
													<uc3:InputCost id="ucCost" runat="server" Value='<%#DataBinder.Eval(Container, "DataItem.CostCode")%>' >
													</uc3:InputCost>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="金额（元）&lt;font color=red&gt;*&lt;/font&gt;">
												<ItemTemplate>
													<INPUT class="input-nember" id="txtMoney" type="text" value='<%# RmsPM.BLL.StringRule.BuildShowNumberString( DataBinder.Eval(Container.DataItem, "Money")) %> ' name="txtMoney" runat="server" onblur="FormatInput(this);">
												</ItemTemplate>
												<FooterTemplate>
													<FONT face="宋体">
														<asp:Label id="lblTotalMoney" runat="server"></asp:Label></FONT>
												</FooterTemplate>
											</asp:TemplateColumn>
											<asp:ButtonColumn Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;" HeaderText="删除" 
 CommandName="Delete"></asp:ButtonColumn>
										</Columns>
										<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
											CssClass="ListHeadTr"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
						</TABLE>
						<br>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="intopic" width="200">分摊</td>
							</tr>
						</table>
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="form-item" width="15%">分摊方式：<A href="#" onclick="SelectBuilding();return false;"><IMG src="../images/ToolsItemSearch.gif" border="0"></A></TD>
								<TD><input class="input-readonly" id="txtAllocateName" readOnly type="text" runat="server">
								</TD>
								<TD><input id="txtAllocateCodes" type="hidden" runat="server"><input id="txtAlloType" type="hidden" runat="server">
								</TD>
							</TR>
						</table>
						<br>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="intopic" width="200">相关工作</td>
								<td><input class="submit" id="btnAddTask" onclick="SelectDetailTask();" type="button" value="添加相关工作"
										name="btnAddTask" runat="server"></td>
							</tr>
						</table>
						<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<TR>
								<TD colSpan="2"><asp:datagrid id="dgTaskList" runat="server" CssClass="list" AllowSorting="True" AutoGenerateColumns="False"
										PageSize="15" Width="100%" CellPadding="2" GridLines="Horizontal" AllowPaging="False" DataKeyField="TaskContractCode">
										<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
										<HeaderStyle CssClass="list-title"></HeaderStyle>
										<FooterStyle CssClass="list-title"></FooterStyle>
										<Columns>
											<asp:BoundColumn DataField="WBSCode" Visible="False"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="序号">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<%# Container.ItemIndex + 1 %>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="工作名称">
												<ItemTemplate>
													<a style="cursor:hand" onclick="OpenTask(this.val)" val='<%#  DataBinder.Eval(Container.DataItem, "WBSCode")%>'>
														<%#  DataBinder.Eval(Container.DataItem, "TaskName")%>
													</a>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="当前进度">
												<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
												<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<%#  RmsPM.BLL.StringRule.AddUnit(DataBinder.Eval(Container.DataItem, "CompletePercent"), "%")%>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="负责人">
												<ItemTemplate>
													<%#  DataBinder.Eval(Container.DataItem, "UserNames")%>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:ButtonColumn Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;" HeaderText="删除" 
 CommandName="Delete"></asp:ButtonColumn>
										</Columns>
										<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
											CssClass="ListHeadTr"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
						</TABLE>
						<table style="DISPLAY: none" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="intopic" width="200">执行计划</td>
								<td><input class="submit" id="btnNewPlan" type="button" value="新增执行计划" name="btnNew" runat="server" onserverclick="btnNewPlan_ServerClick"></td>
							</tr>
						</table>
						<TABLE style="DISPLAY: none" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="0">
							<TR>
								<TD colSpan="2"><asp:datagrid id="dgExecuteList" runat="server" CssClass="list" AllowSorting="True" AutoGenerateColumns="False"
										PageSize="15" Width="100%" CellPadding="2" GridLines="Horizontal" AllowPaging="False">
										<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
										<HeaderStyle CssClass="list-title"></HeaderStyle>
										<FooterStyle CssClass="list-title"></FooterStyle>
										<Columns>
											<asp:BoundColumn DataField="ContractExecutePlanCode" HeaderText="编号" Visible="False"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="序号">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<%# Container.ItemIndex + 1 %>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="负责人" ItemStyle-Width="150">
												<ItemTemplate>
													<input readonly type="text" class="input-readonly" id="txtExecutePersonNameShow" runat="server" value ='<%#  DataBinder.Eval(Container.DataItem, "ExecutePersonName")  %>' >
													<input type="hidden" runat="server" id="txtExecutePersonCode" value='<%#  DataBinder.Eval(Container.DataItem, "Executor")%>'>
													<input type="hidden" runat="server" id="txtExecutePersonName" value='<%#  DataBinder.Eval(Container.DataItem, "ExecutePersonName")%>'>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn>
												<ItemTemplate>
													<a  href="javascript:SelectPlanPerson(<%#Container.ItemIndex + 2 %>);"><img src="../images/ToolsItemSearch.gif" border="0"></a>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="执行时间" ItemStyle-Width="150">
												<ItemTemplate>
													<cc3:calendar id="dtExecuteDate" runat="server" Display="True" value='<%#  DataBinder.Eval(Container.DataItem, "ExecuteDate")  %>' ReadOnly="False" CalendarResource="../Images/CalendarResource/">
													</cc3:calendar>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="工作">
												<ItemTemplate>
													<input type="text" id="txtExecuteDetail" runat="server" size="90" class="input" value='<%#  DataBinder.Eval(Container.DataItem, "ExecuteDetail")  %>'>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:ButtonColumn Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;" HeaderText="删除" 
 CommandName="Delete"></asp:ButtonColumn>
										</Columns>
										<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
											CssClass="ListHeadTr"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
						</TABLE>
						<table cellSpacing="0" cellPadding="0" width="100%">
							<tr>
								<td align="center"><input class="submit" id="btnSave" onclick="if(!doSave()) return false;" type="button"
										value="确 定" name="btnSave" runat="server" onserverclick="btnSave_ServerClick"> &nbsp; <input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="取 消"
										name="btnCancel" runat="server">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<iframe id="iframeSave" style="DISPLAY: none" src="../Cost/SavingWating.htm" frameBorder="no"
				width="100%" scrolling="auto" height="70%"></iframe><input id="txtSelectCostItemIndex" type="hidden" name="txtSelectCostItemIndex" runat="server">
			<input id="txtSelectExecuteItemIndex" type="hidden" name="txtSelectExecuteItemIndex" runat="server">
			<input id="txtContractCode" type="hidden" name="txtContractCode" runat="server">
			<input id="txtConditionAllocateCode" type="hidden" name="txtConditionAllocateCode" runat="server"><input id="txtConditionPayDate" type="hidden" name="txtConditionPayDate" runat="server">
			<input id="txtAddTaskCode" type="hidden" name="txtAddTaskCode" runat="server"> <input id="txtSelectTaskFlag" type="hidden" name="txtSelectTaskFlag" runat="server">
			<input id="hBeforeAccountTotalMoney" type="hidden" name="hBeforeAccountTotalMoney" runat="server"><input id="oldSupplier" type="hidden" name="oldSupplier" runat="server">
			<input type="button" style="DISPLAY:none" id="btnSelectProject" runat="server" NAME="btnSelectProject" onserverclick="btnSelectProject_ServerClick">
			<input type="hidden" id="txtProjectCode" runat="server" NAME="txtProjectCode"> <span id="SpanScript" runat="server">
			</span><input type="hidden" runat="server" id="hdProjectCode">
		</form>
		<script language="javascript">
<!--

	undoHidden();

	function SelectUnit()
	{
		OpenMiddleWindow("../SelectBox/SelectUnit.aspx?UnitCode="+window.document.all.txthUnit.value);
	}
	//选择单位返回
	function SelectUnitReturn(code, name)
	{
		window.document.all.txtUnitName.value = name;
		window.document.all.txthUnit.value = code;
	}
	
	function FormatInput(obj)
	{
		obj.value = FormatValue(obj.value);
		
		calSumPrice();
	}
	
	function calSumPrice()
	{
		var dgRows = document.all.dgCostList.rows.length;	//alert('rows:'+dgRows);
		var GrandTotal = 0.0;	
		for(j=1;j<dgRows-1;j++)
		{
			var iIndex = j+1;//alert(iIndex);
			var tmp = document.all("dgCostList__ctl"+iIndex+"_txtMoney").value;
			if(tmp!='')
			{
				for(m=0;m<tmp.length;m++)
				{
					if(tmp.indexOf(',')!=-1)
						tmp = tmp.replace(',','');
				}				
				GrandTotal = parseFloat(GrandTotal)+ parseFloat(tmp);//alert(tmp);
			}
			//alert(GrandTotal);
		}			
		//var index = GrandTotal.toString().indexOf('.');
		//if(index!=-1)
		//	GrandTotal = GrandTotal.toString().substring(0,GrandTotal.toString().indexOf('.')+2);
		
		//alert(GrandTotal);//
			
		document.all("dgCostList__ctl"+dgRows+"_lblTotalMoney").innerText = GrandTotal//FormatValue(GrandTotal);
	}
	
	function FormatValue(val)
	{
		// 先去处,号
		var tmp = val;
		//alert("1FormatValue:"+tmp);
		if(tmp!='')
		{
			for(m=0;m<tmp.length;m++)
			{
				if(tmp.indexOf(',')!=-1)
					tmp = tmp.replace(',','');
			}		
		}		
		//alert("2no ,:"+tmp);
		// 添加, 
		var dotIndex = tmp.indexOf('.');
		//alert("3dotIndex:"+dotIndex);
		var atmp = '';
		if(dotIndex!=-1)
		{   ///alert("adf");
			atmp = tmp.substring(dotIndex,tmp.length);
			tmp = tmp.substring(0,dotIndex);			
		}
		//alert("4dotIndex:"+atmp);
		var tlength = tmp.length;
		if(tmp.length>0)
		{
			var i=0;
			for(n=0;n<tlength-1;n++)
			{
				if(n%3==0)
				{
					i++;
					tmp = tmp.substring(0,tlength-n)+','+tmp.substring(tlength-n,tlength+i);
					//alert(tmp);
				}
			}	
		}
		//alert("5:"+tmp);
		if(dotIndex!=-1)
			tmp = tmp.substring(0,tmp.length)+atmp;
		else
			tmp = tmp.substring(0,tmp.length-1);
		
		//alert(tmp);
		return tmp;
	}

	function doSave()
	{
		var dgRows = document.all.dgCostList.rows.length;	//alert(dgRows);
		var GrandTotal = 0;	
		for(j=1;j<dgRows-2;j++)
		{
			var iIndex = j+1;//alert(iIndex);
			var tmp = document.all("dgCostList__ctl"+iIndex+"_txtMoney").value;
			if(tmp!='')
			{
				for(m=0;m<tmp.length;m++)
				{
					if(tmp.indexOf(',')!=-1)
						tmp = tmp.replace(',','');
				}				
				GrandTotal = parseFloat(GrandTotal)+ parseFloat(tmp);//alert(tmp);
			}
			//alert(GrandTotal);	
		}			
		var index = GrandTotal.toString().indexOf('.');
		if(index!=-1)
			GrandTotal = GrandTotal.toString().substring(0,GrandTotal.toString().indexOf('.')+2);
		
		
		var tol = '<%=totalMoney%>';
		//alert(GrandTotal); 
		//alert(tol); 
		if(tol==GrandTotal)
		{
			document.all("iframeSave").style.display = "";
			document.all("tableMain").style.display = "none";	
			return true;
		}
		else
		{
			var act = '<%=Request["Act"]%>';
			//alert(act);
			if(act=="Edit"&&act!="Change")
			{
				///alert('请检查,合同总额与付款计划总额不等!');
				//return false;
				return true;
			}
			else
			{
				document.all("iframeSave").style.display = "";
				document.all("tableMain").style.display = "none";	
				return true;
			}
			
		}
		
	}

	function undoHidden()
	{
		document.all("iframeSave").style.display = "none";
		document.all("tableMain").style.display = "";
	}

	function doSelectSupplier()
	{
		var supplierCode = Form1.txtSupplierCode.value;
		OpenLargeWindow('../SelectBox/SelectSupplier.aspx?SupplierCode=' + supplierCode ,'选择供应商');
	}

	function DoSelectSupplierReturn ( code,name )
	{
		Form1.txtSupplierCode.value = code;
		Form1.txtSupplierName.value = name;
	}

	function SelectPlanPerson(i)
	{
		document.all("txtSelectExecuteItemIndex").value=i;
		OpenMiddleWindow('../SelectBox/SelectPerson.aspx?Flag=1&Type=Single&ProjectCode='+document.Form1.hdProjectCode.value,'选择用户');
	}

	function DoSelectUser(userCode,userName,flag)
	{
		// 选合同执行计划的执行人
		if ( flag==1)
		{
			var i = Form1.txtSelectExecuteItemIndex.value;
			document.all("dgExecuteList__ctl" + i + "_txtExecutePersonCode").value = userCode;
			document.all("dgExecuteList__ctl" + i + "_txtExecutePersonNameShow").innerText = userName;
			document.all("dgExecuteList__ctl" + i + "_txtExecutePersonName").value = userName;
		}
		// 选合同经办人
		else ( flag == 0 )
		{
			Form1.txtContractPerson.value = userCode;
			Form1.txtContractPersonName.value = userName;
		}
	}

	function SelectContractPerson()
	{
		OpenMiddleWindow('../SelectBox/SelectPerson.aspx?Flag=0&Type=Single','选择用户');
	}

	function SelectBuilding()
	{
		var AlloType = Form1.txtAlloType.value;
		var code = Form1.txtAllocateCodes.value;
		OpenCustomWindow('../SelectBox/SelectAlloBuilding.aspx?ProjectCode='+document.Form1.hdProjectCode.value+'&AlloType=' + AlloType + '&SelectCode=' + escape(code) + '&ReturnFunc=SelectBuildingReturn','选择楼栋', 400, 540);
	}

	function SelectBuildingReturn(AlloType, code, name)
	{
		Form1.txtAllocateCodes.value = code;
		Form1.txtAlloType.value = AlloType;
		if ( AlloType == "P" )
			document.all("txtAllocateName").value = '项目';
		else
			document.all("txtAllocateName").value = name;
	}

	//修改付款条件
	function ModifyPayCondition(ConditionCode, AllocateCode)
	{
		Form1.txtConditionAllocateCode.value = AllocateCode;
		OpenCustomWindow("ContractPayConditionModify.aspx?ContractCode=" + Form1.txtContractCode.value + "&AllocateCode=" + AllocateCode + "&ConditionCode=" + ConditionCode, "修改付款条件", 400, 260);
	}
	
	//新增付款条件
	function AddPayCondition(AllocateCode)
	{
		Form1.txtConditionAllocateCode.value = AllocateCode;
		OpenCustomWindow("ContractPayConditionModify.aspx?ProjectCode='+document.Form1.hdProjectCode.value+'&ContractCode=" + Form1.txtContractCode.value + "&AllocateCode=" + AllocateCode, "修改付款条件", 400, 260);
	}
	
	//修改付款条件返回
	function PayConditionReturn(sPayDate)
	{
		Form1.txtConditionPayDate.value = sPayDate;
		Form1.btnPayConditionReturn.click();
	}
	
//显示工作信息
function OpenTask(WBSCode)
{
	OpenFullWindow("../Project/WBSInfo.aspx?WBSCode="+WBSCode,"");
}

//选择工作项
function SelectDetailTask()
{
	Form1.txtSelectTaskFlag.value = "1";
	OpenMiddleWindow("../SelectBox/SelectTask.aspx?Flag=1&WBSCode=");
}

//选择工作项
function SelectTaskReturn(code, name)
{
	if (Form1.txtSelectTaskFlag.value == "1")
	{
		Form1.txtAddTaskCode.value = code;
		Form1.btnAddTaskReturn.click();;
	}
	else
	{
		Form1.txtWBSCode.value = code;
		Form1.txtTaskName.value = name;
		document.all.spanTaskName.innerText = name;
	}
}

//选择工作项
function SelectTask()
{
	Form1.txtSelectTaskFlag.value = "";
	OpenMiddleWindow("../SelectBox/SelectTask.aspx?Flag=1&WBSCode=&ProjectCode="+document.Form1.hdProjectCode.value);
}
//选择项目
function DoSelectProject(projectCode,projectName)
    {
		Form1.txtProjectCode.value = projectCode;
		Form1.btnSelectProject.click();
    }

//-->
		</script>
	</body>
</HTML>
