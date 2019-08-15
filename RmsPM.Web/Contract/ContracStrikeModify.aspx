<%@ Reference Control="~/usercontrols/inputcostbudgetdtl.ascx" %>
<%@ Reference Control="~/usercontrols/exchangeratecontrol.ascx" %>


<%@ Register TagPrefix="uc1" TagName="InputCostBudgetDtl" Src="../UserControls/InputCostBudgetDtl.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExchangeRate" Src="../UserControls/ExchangeRateControl.ascx" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContracStrikeModify.aspx.cs"
    Inherits="RmsPM.Web.Contract.Contract_ContracStrikeModify" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>合同结算</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">

    <script language="javascript" src="../images/convert.js"></script>

    <link href="../Images/index.css" type="text/css" rel="stylesheet">
    <link href="../Images/infra.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>

    <script language="javascript">
<!--


function AddMessage(msg,Message)
{
    if(msg!='') 
        msg+=',';
    msg+=Message;
    return msg
}

function  isNumeric(strNumber)  {  
    var  newPar=/^(-  ¦\+)?\d+(\.\d+)?$/  
    return  newPar.test(strNumber);  
}  
                         
function Checklogic(){
    var msg='';
    
    var a= document.getElementById('txtSupplierChangeMoney').value;
    var b= document.getElementById('txtConsultantAuditMoney').value;
    var c= document.getElementById('txtProjectAuditMoney').value;
    
    if(isNumeric(a)&&isNumeric(b)) 
        if (ConvertFloat(a)<ConvertFloat(b))
            msg=AddMessage(msg,'[供应商变更申请金额]应大于等于[估算师审核金额]');
                
    if(isNumeric(b)&&isNumeric(c)) 
        if (ConvertFloat(b)<ConvertFloat(c))
            msg=AddMessage(msg,'[估算师审核金额]应大于等于[合约部审核金额]');
           
    if(isNumeric(a)&&!isNumeric(b)&&isNumeric(c)) 
        if (ConvertFloat(a)<ConvertFloat(c))
            msg=AddMessage(msg,'[供应商变更申请金额]应大于等于[合约部审核金额]');
    if (msg!='')
        return( confirm(msg+'\n是否确定变更？'));
    else
        return true;
}


function InfraMoneyValueChange(oEdit, oldValue, oEvent)
{
	InfraCalcSum();
}

//计算合计
function InfraCalcSum()
{
	var c = parseInt(document.all.dgCostList.rows.length) - 2;
	var CostOriginalMoney = 0;
	var CostTotalChangeMoney = 0;
	var CostChangeMoney = 0;
	var CostNewMoney = 0;
	var sumCostChangeMoney = 0;
	var sumCostNewMoney = 0;

	for(i=0;i<c;i++)
	{

//		CostOriginalMoney = ConvertFloat(GetObjectInDataGrid("dgCostList", (i + 2), "txtCostOriginalMoney").value) ;
//		CostTotalChangeMoney = ConvertFloat(GetObjectInDataGrid("dgCostList", (i + 2), "txtCostTotalChangeMoney").value);

		CostChangeMoney = ConvertFloat(GetObjectInDataGrid("dgCostList", (i + 2), "txtCostChangeCash").value);

//		CostNewMoney = CostOriginalMoney + CostTotalChangeMoney + CostChangeMoney;

//		GetObjectInDataGrid("dgCostList", (i + 2), "txtCostNewCash").value = formatNumber(CostNewMoney, "#,###.00");

		sumCostChangeMoney = sumCostChangeMoney + CostChangeMoney;
		sumCostNewMoney = sumCostNewMoney + CostNewMoney;
	}
	
	//显示合同金额
	ContractMoneyChange(sumCostChangeMoney,sumCostNewMoney);

	//格式化
	sumCostChangeMoney = formatNumber(sumCostChangeMoney, "#,###.00");
	sumCostNewMoney = formatNumber(sumCostNewMoney, "#,###.00");

	GetObjectInDataGrid("dgCostList", (c + 2), "lblSumCostChangeMoney").innerText = sumCostChangeMoney;
//	GetObjectInDataGrid("dgCostList", (c + 2), "lblSumCostNewMoney").innerText = sumCostNewMoney;
}

function ContractMoneyChange( ChangeMoney,NewTotalMoney )
{
	
	ChangeMoney = formatNumber(ChangeMoney, "#,###.00");
	NewTotalMoney = formatNumber(NewTotalMoney, "#,###.00");

	document.all.txtChangeMoney.value = ChangeMoney;
	document.all.txtNewTotalMoney.value = NewTotalMoney;
	igedit_getById("txtProjectAuditMoney").setValue(ChangeMoney);
}

function undoHidden()
{
	document.all("iframeSave").style.display = "none";
	document.all("tableMain").style.display = "";
}

      function SelectNexus()
      {
         var ud_sNexusCodes = document.all("hidNexusCodes").value;
         var ud_sContractCode = document.all("txtContractCode").value;
         var ud_sProjectCode = '<% =Request["ProjectCode"]+"" %>';
         var ud_sUrl = "../Contract/ContractNexusSelectFrme.aspx?ContractCode=" + ud_sContractCode + "&ProjectCode=" + ud_sProjectCode + "&NexusCodes=" + ud_sNexusCodes;
         
//         alert (ud_sUrl);
         
         var tmp = OpenCustomDialog(ud_sUrl,"","800px","600px");
         //参数中NexusCodes格式为([签证代码,签证类型];[设计变更代码,设计变更类型];...)(签证类型描述为 "Vise")
         //返回格式为 ([签证代码,签证类型];[设计变更代码,设计变更类型];...) ，清空时返回数据为 ("")。
         //取消操作时返回为 ("undefined")
         if ( tmp != undefined )
         {
            document.all("hidNexusCodes").value = tmp;
         }
         
//         alert(tmp);
         
         __doPostBack('btnLoadNexus','');
      }
//-->
    </script>

</head>
<body scroll="no">
    <form id="Form1" method="post" runat="server">
        <table id="tableMain" height="100%" cellspacing="0" cellpadding="0" width="100%"
            bgcolor="#ffffff" border="0">
            <tr>
                <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                    <asp:Label ID="lblTitle" runat="server" BackColor="Transparent">合同结算变更</asp:Label></td>
            </tr>
            <tr height="100%">
                <td valign="top">
                    <div style="overflow: auto; width: 100%; height: 100%">
                        <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td class="form-item" width="150">
                                    项目名称：</td>
                                <td>
                                    <asp:Label ID="lblProjectName" runat="server"></asp:Label></td>
                                <td class="form-item" width="150">
                                    合同编号：</td>
                                <td>
                                    <asp:Label ID="lblContractID" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="form-item">
                                    合同名称：</td>
                                <td>
                                    <asp:Label ID="lblContractName" runat="server"></asp:Label></td>
                                <td class="form-item">
                                    变更依据：</td>
                                <td>
                                    <input class="input" id="txtVoucher" type="text" size="32" name="txtVoucher" runat="server"></td>
                            </tr>
                            <tr>
                                <td class="form-item">
                                    供 应 商：</td>
                                <td>
                                    <asp:Label ID="lblSupplierName" runat="server"></asp:Label></td>
                                <td class="form-item">
                                    审批表编号：</td>
                                <td>
                                    <input class="input" id="txtChangeId" type="text" size="32" name="txtChangeId" runat="server"></td>
                            </tr>
                            <tr>
                                <td class="form-item" valign="top">
                                    变更原因及摘要：</td>
                                <td colspan="3">
                                    <textarea id="txtChangeReason" style="width: 100%" name="txtChangeReason" rows="4"
                                        runat="server"></textarea>
                                    <uc1:AttachMentAdd ID="myAttachMentAdd" runat="server"></uc1:AttachMentAdd>
                                </td>
                            </tr>
                            <tr>
                                <td class="form-item">
                                    原合同金额：</td>
                                <td>
                                    <input class="infra-input-nember" id="txtBudgetMoney" style="float: left; width: 120px;
                                        text-align: right" readonly type="text" name="txtBudgetMoney" runat="server">&nbsp;
                                </td>
                                <td class="form-item">
                                    供应商本次变更申请金额：</td>
                                <td>
                                    <igtxt:WebNumericEdit ID="txtSupplierChangeMoney" runat="server" Width="100" MinDecimalPlaces="Two"
                                        CssClass="infra-input-nember" ImageDirectory="../images/infragistics/images/"
                                        JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js">
                                    </igtxt:WebNumericEdit>
                                </td>
                            </tr>
                            <tr>
                                <td class="form-item">
                                    暂定金额/指定金额：</td>
                                <td>
                                    <input class="infra-input-nember" id="txtAdjustMoney" style="float: left; width: 120px;
                                        text-align: right" readonly type="text" name="txtAdjustMoney" runat="server">&nbsp;
                                </td>
                                <td class="form-item">
                                    顾问估算师审核金额：</td>
                                <td>
                                    <igtxt:WebNumericEdit ID="txtConsultantAuditMoney" runat="server" Width="100" MinDecimalPlaces="Two"
                                        CssClass="infra-input-nember" ImageDirectory="../images/infragistics/images/"
                                        JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js">
                                    </igtxt:WebNumericEdit>
                                </td>
                            </tr>
                            <tr>
                                <td class="form-item">
                                    实际金额：</td>
                                <td>
                                    <input id="hidOriginalMoney" type="hidden" name="hidOriginalMoney" runat="server">
                                    <input class="infra-input-nember" id="txtOriginalMoney" style="float: left; width: 120px;
                                        text-align: right" readonly type="text" name="txtOriginalMoney" runat="server">
                                </td>
                                <td class="form-item">
                                    项目合约部审核金额：</td>
                                <td>
                                    <igtxt:WebNumericEdit ID="txtProjectAuditMoney" ReadOnly="true" runat="server" Width="100" MinDecimalPlaces="Two"
                                        CssClass="infra-input-nember" ImageDirectory="../images/infragistics/images/"
                                        JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js">
                                    </igtxt:WebNumericEdit>
                                </td>
                            </tr>
                            <tr>
                                <td class="form-item">
                                    累计已批变更：</td>
                                <td>
                                    <input id="hidTotalChangeMoney" type="hidden" name="hidTotalChangeMoney" runat="server">
                                    <input class="infra-input-nember" id="txtTotalChangeMoney" style="float: left; width: 120px;
                                        text-align: right" readonly type="text" name="txtTotalChangeMoney" runat="server">
                                </td>
                                <td class="form-item">
                                    变更类型</td>
                                <td>
                                    <asp:DropDownList ID="ddlChangeType" runat="server">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td class="form-item">
                                    本次上报变更金额：</td>
                                <td>
                                    <input class="infra-input-nember" id="txtChangeMoney" style="width: 120px; text-align: right"
                                        readonly type="text" name="txtChangeMoney" runat="server">
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="form-item">
                                    合同总额预计：</td>
                                <td>
                                    <input class="infra-input-nember" id="txtNewTotalMoney" style="float: left; width: 120px;
                                        text-align: right" readonly type="text" name="txtNewTotalMoney" runat="server">
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                        <br>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td class="intopic" width="200">
                                    相关单据</td>
                                <td>
                                    <input class="submit" id="btnInputNexus" type="button" value="选择相关单据" name="btnInputNexus"
                                        runat="server" onclick="javascript:SelectNexus();return false;">
                                </td>
                            </tr>
                        </table>
                        <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvNexusList" runat="server" AutoGenerateColumns="false" CssClass="list"
                                        ShowFooter="true" ShowHeader="true" Width="100%">
                                        <RowStyle CssClass="list-i" />
                                        <HeaderStyle CssClass="list-title" Wrap="false" HorizontalAlign="center" />
                                        <FooterStyle CssClass="list-title" Wrap="false" HorizontalAlign="right" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="序号">
                                                <ItemTemplate>
                                                    <%# Container.DisplayIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ContractCode" HeaderText="合同系统编号" Visible="false" />
                                            <asp:BoundField DataField="ContractChangeCode" HeaderText="合同变更系统编号" Visible="false" />
                                            <asp:BoundField DataField="Code" HeaderText="系统编号" Visible="false" />
                                            <asp:BoundField DataField="Type" HeaderText="单据类型" Visible="false" />
                                            <asp:TemplateField HeaderText="单据类型">
                                                <ItemStyle HorizontalAlign="center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNexusType" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="标题">
                                                <ItemStyle HorizontalAlign="center" />
                                                <ItemTemplate>
                                                    <a href="#" onclick="javascript:OpenFullWindow(' <%# DataBinder.Eval(Container, "DataItem.Path" )  %>','');">
                                                        <%# DataBinder.Eval(Container, "DataItem.Name" )  %>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ID" HeaderText="编号" ItemStyle-HorizontalAlign="center" />
                                            <asp:TemplateField HeaderText="金额">
                                                <ItemStyle HorizontalAlign="right" />
                                                <FooterStyle HorizontalAlign="right" />
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container, "DataItem.Money","{0:N}" )%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    合计：&nbsp;&nbsp;<asp:Label ID="lblSumMoney" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="经办人">
                                                <ItemStyle HorizontalAlign="center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPersonName" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Date" HeaderText="日期" ItemStyle-HorizontalAlign="center" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        <br>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td class="intopic" width="200">
                                    合同款项明细</td>
                                <td>
                                    <input class="submit" id="btnNewCostItem" type="button" value="新增款项明细" name="btnNewCostItem"
                                        runat="server" onserverclick="btnNewCostItem_ServerClick">
                                </td>
                            </tr>
                        </table>
                        <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                            <tr>
                                <td>
                                    <asp:DataGrid ID="dgCostList" onkeydown="if(event.keyCode==13) event.keyCode=9" runat="server"
                                        Width="100%" CssClass="list" PageSize="15" AutoGenerateColumns="False" AllowSorting="True"
                                        ShowFooter="True">
                                        <AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
                                        <HeaderStyle CssClass="list-title"></HeaderStyle>
                                        <FooterStyle CssClass="list-title"></FooterStyle>
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="ContractCostChangeCode"></asp:BoundColumn>
                                            <asp:BoundColumn Visible="False" DataField="ContractCostCode"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="序号" ItemStyle-Width="40">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# Container.ItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="费用项&lt;font color=red&gt;*&lt;/font&gt;" FooterText="合计（RMB）："
                                                ItemStyle-Width="250" FooterStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemTemplate>
                                                    <uc1:InputCostBudgetDtl ID="ucCostBudgetDtl" runat="server" CostBudgetSetCode='<%# DataBinder.Eval(Container, "DataItem.CostBudgetSetCode") %>'
                                                        CostCode='<%# DataBinder.Eval(Container, "DataItem.CostCode") %>'></uc1:InputCostBudgetDtl>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="原始金额" ItemStyle-Width="200">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                <ItemTemplate>
                                                    <uc1:ExchangeRate ID="ucExchangeRate" runat="server"></uc1:ExchangeRate>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblSumCostOriginalMoney" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="原始金额" ItemStyle-Width="100" Visible="False">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                <ItemTemplate>
                                                    <igtxt:WebNumericEdit Width="100" ID="txtCostOriginalMoney" runat="server" MinDecimalPlaces="Two"
                                                        CssClass="infra-input-nember" Value='<%# DataBinder.Eval(Container, "DataItem.OriginalMoney") %>'
                                                        ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
                                                        JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js">
                                                    </igtxt:WebNumericEdit>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="累计已批变更" ItemStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                <ItemTemplate>
                                                    <igtxt:WebNumericEdit Width="100" ID="txtCostTotalChangeCash" runat="server" ReadOnly
                                                        MinDecimalPlaces="Two" CssClass="infra-input-nember" Value='<%# DataBinder.Eval(Container, "DataItem.TotalChangeCash") %>'
                                                        ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
                                                        JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js">
                                                    </igtxt:WebNumericEdit>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblSumCostTotalChangeMoney" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="本次变更金额" ItemStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                <ItemTemplate>
                                                    <igtxt:WebNumericEdit Width="100" ID="txtCostChangeCash" runat="server" MinDecimalPlaces="Two"
                                                        CssClass="infra-input-nember" Value='<%# DataBinder.Eval(Container, "DataItem.ChangeCash") %>'
                                                        ImageDirectory="../images/infragistics/images/" JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js"
                                                        JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js">
                                                        <ClientSideEvents ValueChange="InfraMoneyValueChange"></ClientSideEvents>
                                                    </igtxt:WebNumericEdit>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblSumCostChangeMoney" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="预计金额" ItemStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                <ItemTemplate>
                                                    <input class="infra-input-nember" id="txtCostNewCash" style="width: 100px; text-align: right"
                                                        readonly type="text" name="txtCostNewCash" runat="server" value='<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.NewCash")) %>'>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblSumCostNewMoney" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="预计金额（RMB）" ItemStyle-Width="100" Visible="False">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                                <ItemTemplate>
                                                    <input class="infra-input-nember" id="txtCostNewMoney" style="width: 100px; text-align: right"
                                                        readonly type="text" name="txtCostNewMoney" runat="server" value='<%# RmsPM.BLL.MathRule.GetDecimalShowString(DataBinder.Eval(Container, "DataItem.NewMoney")) %>'>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="说明">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemTemplate>
                                                    <input id="txtDescription" runat="server" class="input" value='<%# DataBinder.Eval(Container, "DataItem.Description") %>'
                                                        name="txtPayConditionText">
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:ButtonColumn Text="&lt;img src=../images/del.gif width=16 height=16 border=0&gt;"
                                                HeaderText="删除" CommandName="Delete" ItemStyle-Width="40"></asp:ButtonColumn>
                                        </Columns>
                                    </asp:DataGrid></td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="9" width="100%">
                        <tr>
                            <td align="center">
                                <input class="submit" id="btnSave" type="button" value="确 定" onclick="if(!Checklogic())return false;"
                                    name="btnSave" runat="server" onserverclick="btnSave_ServerClick">
                                &nbsp;
                                <input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button"
                                    value="取 消" name="btnCancel" runat="server">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <iframe id="iframeSave" style="display: none" src="../Cost/SavingWating.htm" frameborder="no"
            width="100%" scrolling="auto" height="70%"></iframe>
        <input id="txtContractCode" type="hidden" name="txtContractCode" runat="server">
        <input id="txtContractChangeCode" type="hidden" name="txtContractChangeCode" runat="server" />
        <input id="hidNexusCodes" runat="server" type="hidden" name="hidNexusCodes" />
        <input id="btnLoadNexus" runat="server" type="button" onserverclick="btnLoadNexus_ServerClick"
            visible="false" />
    </form>

    <script language="javascript">
<!--

undoHidden();

//-->
    </script>

</body>
</html>
