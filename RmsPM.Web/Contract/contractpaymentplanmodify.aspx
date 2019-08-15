<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc3" TagName="InputCost" Src="../UserControls/InputCost.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.Contract.ContractPaymentPlanModify" CodeFile="ContractPaymentPlanModify.aspx.cs" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="InputCostBudgetDtl" Src="../UserControls/InputCostBudgetDtl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>��ͬ����ƻ�</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY: none"><input id="btnPayConditionReturn" type="button" value="btnPayConditionReturn" name="btnPayConditionReturn"
					runat="server" onserverclick="btnPayConditionReturn_ServerClick"> <input id="btnAddTaskReturn" type="button" value="btnAddTaskReturn" name="btnAddTaskReturn"
					runat="server">
			</div>
			<table id="tableMain" height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff"
				border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">��ͬ����ƻ�
					</td>
				</tr>
				<tr>
					<td vAlign="top">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="form-item" width="10%">��ͬ���ƣ�</TD>
								<TD width="23%"><asp:label id="lblContractName" runat="server" BackColor="Transparent"></asp:label></TD>
								<TD class="form-item" width="10%">��ͬ��ţ�</TD>
								<TD width="23%"><asp:label id="lblContractID" runat="server" BackColor="Transparent"></asp:label></TD>
							</TR>
							<TR>
								<TD class="form-item">���ţ�</TD>
								<TD><asp:label id="lblUnitName" runat="server"></asp:label></TD>
								<TD class="form-item">���ͣ�</TD>
								<TD><asp:label id="LabelType" runat="server"></asp:label></TD>
							</TR>
							<tr>
								<td class="form-item">�ƻ���ʼʱ��</td>
								<td><cc3:calendar id="ccStartDay" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar></td>
								<td class="form-item">�ƻ�����ʱ��</td>
								<td><cc3:calendar id="ccEndDay" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/"></cc3:calendar></td>
							</tr>
						</table>
						<br>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="intopic" width="200">����ƻ�</td>
								<td><input class="submit" id="btnNewItem" type="button" value="��������" name="btnNewItem" runat="server" onserverclick="btnNewItem_ServerClick"></td>
								<td>
									<asp:dropdownlist id="ddlCost" Runat="server"></asp:dropdownlist>&nbsp;
									�ƻ�ģ�壺<asp:dropdownlist id="ddlClaimsExpressions" Runat="server"></asp:dropdownlist>&nbsp;
									<asp:button id="btnBuildPlan" Runat="server" Text="���ɼƻ�" CssClass="submit" onclick="btnBuildPlan_Click"></asp:button></td>
							</tr>
						</table>
						<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<TR>
								<TD><asp:datagrid id="dgCostList" runat="server" CssClass="list" Width="100%" PageSize="15" AutoGenerateColumns="False"
										AllowSorting="True" ShowFooter="True">
										<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
										<HeaderStyle CssClass="list-title"></HeaderStyle>
										<FooterStyle CssClass="list-title"></FooterStyle>
										<Columns>
											<asp:BoundColumn Visible="False" DataField="ContractCostCode"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="ContractCostPlanCode"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="���">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<%# Container.ItemIndex + 1 %>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="�ƻ�����ʱ��<font color=red>*</font>">
												<ItemStyle></ItemStyle>
												<ItemTemplate>
													<cc3:calendar id="dtPlanningPayDate" runat="server" Display="True" ReadOnly="False" CalendarResource="../Images/CalendarResource/" value='<%#  DataBinder.Eval(Container.DataItem, "PlanningPayDate")  %>'>
													</cc3:calendar>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="��Ԫ��&lt;font color=blue&gt;*&lt;/font&gt;" ItemStyle-Width="100">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<FooterStyle HorizontalAlign="Right"></FooterStyle>
												<ItemTemplate>
													<igtxt:webnumericedit Width="100" id="txtMoney" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember" Value='<%# DataBinder.Eval(Container, "DataItem.Money") %>' ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js">
													</igtxt:webnumericedit>
												</ItemTemplate>
												<FooterTemplate>
													<asp:Label id="lblSumPlanMoney" runat="server"></asp:Label>
												</FooterTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="��������">
												<ItemStyle></ItemStyle>
												<ItemTemplate>
													<span id="spanPayConditionHtml"><input size=30 type=text runat=server id="txtPayConditionText" value='<%#  DataBinder.Eval(Container.DataItem, "PayConditionText")  %>' class=input NAME="txtPayConditionText">
													</span>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:ButtonColumn Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;" HeaderText="ɾ��" 
 CommandName="Delete"></asp:ButtonColumn>
										</Columns>
										<PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Right"
											CssClass="ListHeadTr"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
						</TABLE>
						<br>
						<table cellSpacing="0" cellPadding="0" width="100%">
							<tr>
								<td align="center"><input class="submit" id="btnSave" onclick="doSave();" type="button" value="ȷ ��" name="btnSave"
										runat="server" onserverclick="btnSave_ServerClick"> &nbsp; <input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="ȡ ��"
										name="btnCancel" runat="server">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<iframe id="iframeSave" style="DISPLAY: none" src="../Cost/SavingWating.htm" frameBorder="no"
				width="100%" scrolling="auto" height="70%"></iframe><input id="txtSelectCostItemIndex" type="hidden" name="txtSelectCostItemIndex" runat="server">
			<input id="txtContractCode" type="hidden" name="txtContractCode" runat="server">
			<input id="txtConditionAllocateCode" type="hidden" name="txtConditionAllocateCode" runat="server">
			<input id="txtConditionPayDate" type="hidden" name="txtConditionPayDate" runat="server">
			<uc1:inputcostbudgetdtl id="ucCostBudgetDtl" runat="server"></uc1:inputcostbudgetdtl></form>
		<script language="javascript">
<!--

	undoHidden();

	function doSave()
	{
		document.all("iframeSave").style.display = "";
		document.all("tableMain").style.display = "none";
	}

	function undoHidden()
	{
		document.all("iframeSave").style.display = "none";
		document.all("tableMain").style.display = "";
	}


	function SelectPlanPerson(i)
	{
		document.all("txtSelectExecuteItemIndex").value=i;
		OpenMiddleWindow('../SelectBox/SelectPerson.aspx?Flag=1&Type=Single&ProjectCode=<%=Request["ProjectCode"]%>','ѡ���û�');
	}

	//�޸ĸ�������
	function ModifyPayCondition(ConditionCode, AllocateCode)
	{
		Form1.txtConditionAllocateCode.value = AllocateCode;
		OpenCustomWindow("ContractPayConditionModify.aspx?ContractCode=" + Form1.txtContractCode.value + "&AllocateCode=" + AllocateCode + "&ConditionCode=" + ConditionCode, "�޸ĸ�������", 400, 260);
	}
	
	//������������
	function AddPayCondition(AllocateCode)
	{
		Form1.txtConditionAllocateCode.value = AllocateCode;
		OpenCustomWindow("ContractPayConditionModify.aspx?ProjectCode=<%=Request["ProjectCode"]%>&ContractCode=" + Form1.txtContractCode.value + "&AllocateCode=" + AllocateCode, "�޸ĸ�������", 400, 260);
	}
	
	//�޸ĸ�����������
	function PayConditionReturn(sPayDate)
	{
		Form1.txtConditionPayDate.value = sPayDate;
		Form1.btnPayConditionReturn.click();
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
		// ��ȥ��,��
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
		// ���, 
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

//-->
		</script>
	</body>
</HTML>
