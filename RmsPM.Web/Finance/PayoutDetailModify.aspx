<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayoutDetailModify.aspx.cs" Inherits="Finance_PayoutDetailModify" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="InputSubject" Src="../UserControls/InputSubject.ascx" %>
<%@ Register Assembly="Infragistics.WebUI.WebDataInput.vT1" Namespace="Infragistics.WebUI.WebDataInputT1" TagPrefix="igtxt" %>
<%@ Register TagPrefix="uc1" TagName="ExchangeRate" Src="../UserControls/inputExchangeRate.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>���</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
		<meta content="C#" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />		
		<script language="javascript" src="../Rms.js"></script>
		<script language="javascript" src="../images/convert.js"></script>
		<script language="javascript" src="../images/checkbox.js"></script>
		<script language="javascript" src="../images/infra.js"></script>
		<link href="../images/index.css" type="text/css" rel="stylesheet" />
		<link href="../images/infra.css" type="text/css" rel="stylesheet" />
		<style>
			.list-un { background-color: #d4d0c8 }
		</style>
</head>
<script language="javascript">

//��ʼ����ϸ��
function InfraDtlExchangeRate()
{
	
    //������ؼ�
    var txtPayoutExchangeRateName = GetObjectNameInControl("ucExchangeRate","fvExchangeRate_txtExchangeRate");
    var ddlPayoutMoneyType =  GetObjectInControl("ucExchangeRate","fvExchangeRate_ddlMoneyType");

	//֧������
	var PayoutMoneyType = "";

	for ( i=0;i<ddlPayoutMoneyType.options.length;i++)
	{
	    if ( ddlPayoutMoneyType.options[i].value == ddlPayoutMoneyType.value)
	    {
	        PayoutMoneyType = ddlPayoutMoneyType.options[i].text;
	        break;
	    }
	}
	
	var PayoutExchangeRate = igedit_getById(txtPayoutExchangeRateName).getValue()
	
	var GridViewID = "gvPayoutItem";
 	var c = parseInt(document.all(GridViewID).rows.length) - 2;
	var PaymentMoneyType;

	for(i=0;i<c;i++)
	{
        //�����ؿؼ�
        var txtPayoutItemExchangeRateName = GetObjectNameInDataGrid(GridViewID,i + 2,"txtPayoutExchangeRate");
        
        var txtPaymentMoneyType = GetObjectInDataGrid(GridViewID,i + 2,"txtPaymentMoneyType");
        
        if ( txtPaymentMoneyType.value == PayoutMoneyType )
        {
            igedit_getById(txtPayoutItemExchangeRateName).setValue(PayoutExchangeRate);
        }
	}
	
	InfraPayoutCashMoneyChange();

}		

//��ʼ����ϸ��
function IniDtlCheck()
{
	var objCheck = document.all.chkSelect;
	var ischecked;

	if (!objCheck) return;
	
		if(objCheck[0])
		{
			l = objCheck.length;
		
			for(var i=0;i<l;i++)
			{
				ischecked = (objCheck[i].getAttribute("chk") == "1");
				
				if (objCheck[i].checked != ischecked)
				{
					objCheck[i].click();
				}
			}
		}
		else
		{
			if(objCheck)
			{
				ischecked = (objCheck.getAttribute("chk") == "1");

				if (objCheck.checked != ischecked)
				{
					objCheck.click();
				}
			}
		}
}

//��ϸ��
function MyChkSelectRow(index, objCheck, objTable, selectedClass, unselectedClass)
{
	var i = parseInt(index);
	
	if (!objTable) return;
	if (!objTable.rows(i)) return;

	if (objCheck.checked)
	{
		objTable.rows(i).className = selectedClass;
	}
	else
	{
		objTable.rows(i).className = unselectedClass;
	}
	
	ApportDtlSingle(index, objCheck);
}


function ApportDtlSingle(index, objCheck)
{
    var GridViewID = "gvPayoutItem";
	var i = parseInt(index);
	var c = parseInt(document.all(GridViewID).rows.length) - 2;
	var RemainItemCash,PayoutCash,PayoutMoney,PayoutExchangeRate;
	
    //������ؼ�
    var txtPayoutCashName = GetObjectNameInControl("ucExchangeRate","fvExchangeRate_txtCash");
    var txtPayoutExchangeRateName = GetObjectNameInControl("ucExchangeRate","fvExchangeRate_txtExchangeRate");
    var ddlPayoutMoneyType =  GetObjectInControl("ucExchangeRate","fvExchangeRate_ddlMoneyType");

	//���ؿؼ�
	var txtRemainItemCash = GetObjectInDataGrid( GridViewID ,i + 1,"txtRemainItemCash");
	var txtPaymentMoneyType = GetObjectInDataGrid(GridViewID,i + 1,"txtPaymentMoneyType");
	var txtPaymentExchangeRate =  GetObjectInDataGrid(GridViewID,i + 1,"txtPaymentExchangeRate");
	
	//�����ؿؼ�
	var txtPayoutItemCashName = GetObjectNameInDataGrid(GridViewID,i + 1,"txtPayoutCash");
	var txtPayoutItemExchangeRateName = GetObjectNameInDataGrid(GridViewID,i + 1,"txtPayoutExchangeRate");
	var lblPayoutItemMoney = GetObjectInDataGrid(GridViewID,i + 1,"lblPayoutItemMoney");
	var txtSelect = GetObjectInDataGrid(GridViewID,i + 1,"txtSelect");
	
	
	//֧������
	var PayoutMoneyType = "";

	for ( i=0;i<ddlPayoutMoneyType.options.length;i++)
	{
	    if ( ddlPayoutMoneyType.options[i].value == ddlPayoutMoneyType.value)
	    {
	        PayoutMoneyType = ddlPayoutMoneyType.options[i].text;
	        break;
	    }
	}
	
	//֧�����ֻ���
	var PayoutExchangeRate = igedit_getById(txtPayoutExchangeRateName).getValue();
	
	//������
	var PaymentExchangeRate = ConvertFloat(txtPaymentExchangeRate.value);
	PaymentExchangeRate = formatNumber(PaymentExchangeRate, "#,###.00");
	
	if (objCheck.checked)
	{
		//����ʱ���Զ���̯���
		txtSelect.value = 1;

		RemainItemCash =  ConvertFloat(txtRemainItemCash.value);


		if (RemainItemCash == "")
		{
			return;
		}
			
		if (isNaN(RemainItemCash))
		{
			alert("δ��������Ч����ֵ");
			return;

		}

        //��ֵ
		igedit_getById(txtPayoutItemCashName).setValue(RemainItemCash);
		
		var PayoutItemMoney;
		
        if ( txtPaymentMoneyType.value == PayoutMoneyType )
        {
		    //�����������������ͬʱ�����븶����ʣ��Ҳ��ɸ���
    	    igedit_getById(txtPayoutItemExchangeRateName).setValue(PayoutExchangeRate);
    	    igedit_getById(txtPayoutItemExchangeRateName).setEnabled(false);
    	    PayoutItemMoney = RemainItemCash * PayoutExchangeRate;
    	}
    	else
    	{
		    //��������������ֲ���ͬʱ�����������ʣ����޸�
    	    igedit_getById(txtPayoutItemExchangeRateName).setValue(PaymentExchangeRate);
    	    igedit_getById(txtPayoutItemExchangeRateName).setEnabled(true);
    	    PayoutItemMoney = RemainItemCash * PaymentExchangeRate;
    	}
    	
    	PayoutItemMoney = formatNumber(RemainItemCash, "#,###.00");
    	
        lblPayoutItemMoney.innerText = PayoutItemMoney;
    		    
	    //�����ؼ���д״̬
    	igedit_getById(txtPayoutItemCashName).setEnabled(true);

	}
	else
	{
		//δ����ʱ��������
		txtSelect.value = 0;

 
        //��ֵ
		igedit_getById(txtPayoutItemCashName).setValue(0);
        lblPayoutItemMoney.innerText = formatNumber(0, "#,###.00");		
		
	    //�رտؼ���д״̬
    	igedit_getById(txtPayoutItemCashName).setEnabled(false);
    	igedit_getById(txtPayoutItemExchangeRateName).setEnabled(false);
	}
	
    InfraPayoutCashMoneyChange();
}

function InfraPayoutCashMoneyChange()
{
    var GridViewID = "gvPayoutItem";
 	var c = parseInt(document.all(GridViewID).rows.length) - 2;
	var PayoutCash,PaymentExchangeRate,PayoutMoney,PayoutExchangeRate,PayoutCashMoney;
	var SumPayoutMoney = 0;
	var SumPayoutCash = 0;

	for(i=0;i<c;i++)
	{
        //�����ؿؼ�
	    var txtPayoutItemCashName = GetObjectNameInDataGrid(GridViewID,i + 2,"txtPayoutCash");
        var txtPayoutItemExchangeRateName = GetObjectNameInDataGrid(GridViewID,i + 2,"txtPayoutExchangeRate");
        
        var txtPaymentExchangeRate = GetObjectInDataGrid(GridViewID,i + 2,"txtPaymentExchangeRate");
        var lblPayoutItemMoney = GetObjectInDataGrid(GridViewID,i + 2,"lblPayoutItemMoney");

        PayoutCash = ConvertFloat(igedit_getById(txtPayoutItemCashName).getValue());
        PayoutExchangeRate = ConvertFloat(igedit_getById(txtPayoutItemExchangeRateName).getValue());
        
        PaymentExchangeRate = ConvertFloat(txtPaymentExchangeRate.value);
        
        PayoutItemMoney = PayoutCash * PayoutExchangeRate;
        
        SumPayoutMoney = SumPayoutMoney + PayoutItemMoney;
        SumPayoutCash = SumPayoutCash + PayoutCash;
        
        lblPayoutItemMoney.innerText = formatNumber(PayoutItemMoney, "#,###.00");
	}

	//��ʽ��
	SumPayoutMoney = formatNumber(SumPayoutMoney, "#,###.00");
	SumPayoutCash = formatNumber(SumPayoutCash, "#,###.00");
	
	GetObjectInDataGrid(GridViewID,c + 2,"lblSumPayoutMoney").innerText = SumPayoutMoney;
	GetObjectInDataGrid(GridViewID,c + 2,"lblSumPayoutCash").innerText = SumPayoutCash;
}
   
function InfraPayoutMoneyTypeChange(i)
{

    var GridViewID = "gvPayoutItem";
    var PayoutCash,PayoutCashMoney,PayoutExchangeRate;
    
    i = parseInt(i);

    //���ؿؼ�
    var txtPaymentMoneyType = GetObjectInDataGrid(GridViewID,i + 2,"txtPaymentMoneyType");
	
    //�����ؿؼ�
	var txtPayoutCash = GetObjectNameInDataGrid(GridViewID,i + 2,"txtPayoutCash");
    var txtPayoutExchangeRate = GetObjectNameInDataGrid(GridViewID,i + 2,"txtPayoutExchangeRate");

    var lblPayoutCashMoney = GetObjectInDataGrid(GridViewID,i + 2,"lblPayoutCashMoney");
/*    
    if ( txtPaymentMoneyType.value == ddlPayoutMoneyType.value )
    {
	    //�����������������ͬʱ������Ϊ1.00���Ҳ��ɸ���
	    igedit_getById(txtPayoutExchangeRate).setValue(1.00);
	    igedit_getById(txtPayoutExchangeRate).setEnabled(false);
    }
    else
    {
        //��������������ֲ���ͬʱ�����ʿ����޸�
    	igedit_getById(txtPayoutExchangeRate).setEnabled(true);
    }
*/  
    PayoutExchangeRate = igedit_getById(txtPayoutExchangeRate).getValue();
    PayoutCash = igedit_getById(txtPayoutCash).getValue();
    
    PayoutCash = ConvertFloat(PayoutCash);
    PayoutExchangeRate = ConvertFloat(PayoutExchangeRate);
    
    PayoutCashMoney = PayoutCash * PayoutExchangeRate;
    
    lblPayoutCashMoney.innerText = formatNumber(PayoutCashMoney, "#,###.00");
    
}


//��������Ϣ
function ViewCostCode(code)
{
	OpenFullWindow('../Cost/DynamicCostInfo.aspx?ProjectCode=' + Form1.txtProjectCode.value + '&CostCode=' + code ,'��̬��������Ϣ');
}

//�鿴��
function GotoPayment(PaymentCode)
{
	OpenCustomWindow("../Finance/PaymentInfo.aspx?PaymentCode=" + PaymentCode + "&Open=1", "��", 760, 540);
}

</script>
<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0"  onkeydown="if(event.keyCode==13) event.keyCode=9" onload="IniDtlCheck();">
    <form id="Form1" runat="server">
	    <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0" bgcolor="#ffffff">
		    <tr>
			    <td class="topic" align="center" background="../images/topic_bg.gif" height="25">���</td>
		    </tr>
		    <tr>
			    <td class="topic" valign="top" align="center">
				    <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
					    <tr>
						    <td class="form-item">����ţ�</td>
						    <td><input class="input" id="txtPayoutID" style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px"
								    readonly type="text" name="txtPayoutID" runat="server"></td>
						    <td class="form-item">�������ͣ�</td>
						    <td><select class="select" id="sltPaymentType" name="sltPaymentType" runat="server">
								    <option value="" selected>--��ѡ��--</option>
							    </select>
						    </td>
						    <td></td>
						    <td>
						        <asp:Label ID="lblMoney" runat="server" Visible="false"></asp:Label>&nbsp;
						    </td>								
					    </tr>
					    <tr>
						    <td class="form-item">�ܿλ��</td>
						    <td><asp:label id="lblSupplyName" Runat="server"></asp:label></td>
						    <td class="form-item">�� �� �ˣ�</td>
						    <td><input class="input" id="txtPayer" type="text" name="txtPayer" runat="server"></td>
						    <td class="form-item">�������ڣ�</td>
						    <td><cc3:calendar id="dtPayoutDate" runat="server" CalendarResource="../Images/CalendarResource/"
								    Display="True" Value="" ReadOnly="False"></cc3:calendar><font color="red">*</font></td>
					    </tr>
					    <tr>
						    <td class="form-item">ϵͳ���ͣ�</td>
						    <td>
							    <uc1:InputSystemGroup id="inputSystemGroup" runat="server"></uc1:InputSystemGroup><font color="red">*</font>
						    </td>
						    <td class="form-item">������Ŀ��</td>
						    <td colspan="3"><uc1:InputSubject id="ucInputSubject" runat="server"></uc1:InputSubject><asp:Label runat="server" ID="lblSubjectCodeHint" ForeColor="red"></asp:Label><font color="red">*</font>
						    </td>
					    </tr>
					    <tr>
						    <td class="form-item">֧ Ʊ �ţ�</td>
						    <td><input class="input" id="txtBillNo" type="text" runat="server" /></td>
						    <td class="form-item">Ʊ �� �ţ�</td>
						    <td><input class="input" id="txtInvoNo" type="text" runat="server" /></td>
						    <td class="form-item">����������</td>
						    <td><input class="input-nember" size="4" id="txtReceiptCount" type="text" runat="server" /></td>
					    </tr>
					    <tr>
					        <td class="form-item">֧����</td>
					        <td colspan="5"><uc1:ExchangeRate ID="ucExchangeRate" runat="server" ValueChange="InfraDtlExchangeRate();"/></td>
					    </tr>
					    <tr>
					        <td class="form-item">����ϵͳƾ֤�ţ�</td>
						    <td><input class="input" id="txtVoucherNo" type="text" runat="server" /></td>
					    </tr>
				    </table>
			    </td>
		    </tr>
		    <tr>
			    <td valign="top">
				    <table cellspacing="0" cellpadding="0" border="0">
					    <tr>
						    <td class="intopic" width="200">������ϸ</td>
					    </tr>
				    </table>
			    </td>
		    </tr>
			<tr height="100%">
				<td valign="top">
					<div style="overflow: auto; width: 100%; position: absolute; height: 100%">
					    <table height="100%" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
								<tr>
									<td valign="top">
					                    <asp:GridView ID="gvPayoutItem" runat="server" DataKeyNames="PayoutItemCode" CellPadding="0" AllowSorting="True"
							                GridLines="Both" AutoGenerateColumns="False" PageSize="15" ShowFooter="True" Width="100%" CssClass="list"
							                OnRowDataBound="gvPayoutItem_RowDataBound" OnRowCreated="gvPayoutItem_RowCreated">
							                <HeaderStyle Wrap="False" CssClass="list-title" HorizontalAlign="Center" />
							                <RowStyle  Wrap="False" HorizontalAlign="Center" />
							                <FooterStyle Wrap="False" CssClass="list-title" HorizontalAlign="Right"/>
							                <Columns>
								                <asp:TemplateField Visible="True" HeaderText="&lt;input type='checkbox' name='chkAll' onclick='ChkSelectAll(document.all.chkSelect, document.all.chkAll.checked);' title='ȫѡ��ȫ��ѡ'&gt;">
									                <ItemTemplate>
										                <input type="checkbox" name="chkSelect" class="list-checkbox" onclick="MyChkSelectRow('<%# Container.DataItemIndex + 1%>', this, document.all.gvPayoutItem, '', 'list-un');" value='<%#DataBinder.Eval(Container, "DataItem.PayoutItemCode")%>' chk='<%#DataBinder.Eval(Container, "DataItem.Checked")%>' checked />
										                <input type="hidden" id="txtSelect" runat="server" value='<%#DataBinder.Eval(Container, "DataItem.Checked")%>' />
									                </ItemTemplate>
								                </asp:TemplateField>
    							                <asp:TemplateField HeaderText="���" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
									                <ItemTemplate>
										                <%# Container.DataItemIndex + 1 %>
									                </ItemTemplate>
								                </asp:TemplateField>
												<asp:TemplateField HeaderText="��λ����">
													<HeaderStyle HorizontalAlign="Left" Wrap="false"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<%# RmsPM.BLL.CostBudgetRule.GetPBSName(RmsPM.BLL.ConvertRule.ToString(DataBinder.Eval(Container, "DataItem.CostBudgetSetCode"))) %>
													</ItemTemplate>
												</asp:TemplateField>
	                                            <asp:TemplateField HeaderText="����" ItemStyle-Wrap="false" FooterText="�ϼ�(RMB)��" FooterStyle-Wrap="false">
									                <ItemTemplate>
										                <%# DataBinder.Eval(Container, "DataItem.Summary") %>
										                <input type="hidden" id="txtPayoutItemCode" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.PayoutItemCode") %>' />
										                <input type="hidden" id="txtPaymentItemCode" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.PaymentItemCode") %>' />
										                <input type="hidden" id="txtSummary" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.Summary") %>' />
										                <input type="hidden" id="txtTotalPayoutCash" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.TotalPayoutCash") %>' />
										                <input type="hidden" id="txtRemainItemCash" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.RemainItemCash") %>' />
                                                        <input type="hidden" id="txtItemMoney" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.ItemMoney") %>' />
                                                        <input type="hidden" id="txtPaymentMoneyType" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.PaymentMoneyType") %>' />
                                                        <input type="hidden" id="txtPaymentExchangeRate" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.PaymentExchangeRate") %>' />
									                </ItemTemplate>
								                </asp:TemplateField>
								                <asp:TemplateField HeaderText="�����" ItemStyle-Wrap="false">
								                    <ItemTemplate>
								                        <asp:Label ID="lblPaymentMoneyType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PaymentMoneyType") %>'></asp:Label>��&nbsp;&nbsp;
								                        <asp:Label ID="lblPaymentCash" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ItemCash","{0:N}") %>'></asp:Label>
								                    </ItemTemplate>
								                </asp:TemplateField>
								                <asp:BoundField HeaderText="�Ѹ����<br />��ԭ���֣�" DataField="TotalPayoutCash" DataFormatString="{0:N}"  ItemStyle-Wrap="false"  HtmlEncode="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="right"/>
								                <asp:BoundField HeaderText="δ�����<br />��ԭ���֣�" DataField="RemainItemCash" DataFormatString="{0:N}" ItemStyle-Wrap="false"  HtmlEncode="false" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="right"/>
								                <asp:TemplateField HeaderText="���θ�����<br />��ԭ���֣�" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" >
								                    <ItemTemplate>
									                    <igtxt:webnumericedit Width="80" id="txtPayoutCash" runat="server" MinDecimalPlaces="Two" CssClass="infra-input-nember" Value='<%# DataBinder.Eval(Container, "DataItem.PayoutCash") %>' ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js">
										                    <ClientSideEvents ValueChange="InfraPayoutCashMoneyChange" />
									                    </igtxt:webnumericedit>
								                    </ItemTemplate>
								                    <FooterTemplate>
								                        <asp:Label ID="lblSumPayoutCash" runat="server"></asp:Label>
								                    </FooterTemplate>	
								                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="����"  ItemStyle-Wrap="false">
								                    <ItemTemplate>
							                                <igtxt:webnumericedit Width="75" id="txtPayoutExchangeRate" runat="server" MinDecimalPlaces="8" CssClass="infra-input-nember" Value='<%# DataBinder.Eval(Container, "DataItem.ExchangeRate") %>' ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js">
							                                     <ClientSideEvents ValueChange="InfraPayoutCashMoneyChange" />
							                                </igtxt:webnumericedit>								        
					        	                    </ItemTemplate>
								                </asp:TemplateField>
								                <asp:TemplateField HeaderText="����֧�����<br>��RMB��"  ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="right">
								                    <ItemTemplate>
								                        <asp:Label ID="lblPayoutItemMoney" runat="server"></asp:Label>
								                    </ItemTemplate>
								                    <FooterTemplate>
								                        <asp:Label ID="lblSumPayoutMoney" runat="server"></asp:Label>
								                    </FooterTemplate>								                    
								                </asp:TemplateField>
								                <asp:TemplateField HeaderText="��Ŀ"  ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="left">
									                <ItemTemplate>
										                <uc1:InputSubject id="ucPayoutInputSubject" runat="server" Value='<%#DataBinder.Eval(Container, "DataItem.SubjectCode")%>' />
								                    </ItemTemplate>
								                </asp:TemplateField>
								                <asp:TemplateField HeaderText="������"  ItemStyle-Wrap="false">
									                <ItemTemplate>
										                <span id="divCostName" runat="server">
										                    <a href="#" onclick="ViewCostCode(this.code);return false;" code='<%#  DataBinder.Eval(Container.DataItem, "CostCode") %>'>
												                <%# DataBinder.Eval(Container, "DataItem.CostName") %>
											                </a>
										                </span>
										                <input type="hidden" runat="server" id="txtCostCode" value='<%#DataBinder.Eval(Container, "DataItem.CostCode")%>' />
										                <input type="hidden" runat="server" id="txtCostName" value='<%#DataBinder.Eval(Container, "DataItem.CostName")%>' />
									                </ItemTemplate>
								                </asp:TemplateField>
								                <asp:TemplateField HeaderText="����"  ItemStyle-Wrap="false">
									                <HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
									                <ItemStyle Wrap="False"></ItemStyle>
									                <ItemTemplate>
                                                        <a style="cursor:hand" onclick="javascript:GotoPayment(this.code);" code='<%#  DataBinder.Eval(Container.DataItem, "PaymentCode") %>'>
															<%# DataBinder.Eval(Container, "DataItem.PaymentID") %>
														</a>									                
										                <input type="hidden" id="txtPaymentCode" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.PaymentCode") %>' />
										                <input type="hidden" id="txtPaymentID" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.PaymentID") %>' />
									                </ItemTemplate>
								                </asp:TemplateField>
							                </Columns>
					                    </asp:GridView>
									</td>
								</tr>
							</table>					    
					</div>
				</td>
			</tr>
		    <tr>
			    <td>
				    <table cellspacing="10" width="100%">
					    <tr>
						    <td align="center">
						        <input class="submit" id="btnSave" type="button" value="ȷ ��" runat="server" onserverclick="btnSave_ServerClick" />
							    <input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="ȡ ��" />
						    </td>
					    </tr>
				    </table>
			    </td>
		    </tr>						
		</table>
		<input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server" />
		<input id="txtIsNew" type="hidden" name="txtIsNew" runat="server" />
		<input id="txtPaymentCode" type="hidden" name="txtPaymentCode" runat="server" />
		<input id="txtPayoutCode" type="hidden" name="txtPayoutCode" runat="server" />
		<input id="txtSupplyCode" type="hidden" name="txtSupplyCode" runat="server" />
		<input id="txtSelectDetailItemIndex" type="hidden" name="txtSelectDetailItemIndex" runat="server" />
		<input id="txtSumPayoutMoney" type="hidden" name="txtSumPayoutMoney" runat="server" />
		<input id="txtStatus" type="hidden" name="txtStatus" runat="server" />
		<input id="txtSubjectSetCode" type="hidden" name="txtSubjectSetCode" runat="server" />
		<input id="txtDetailSno" type="hidden" name="txtDetailSno" runat="server" />

    </form>
</body>
</html>
