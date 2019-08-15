<%@ Register TagPrefix="uc1" TagName="InputCostBudgetDtl" Src="../UserControls/InputCostBudgetDtl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExchangeRateControl" Src="../UserControls/ExchangeRateControl.ascx" %>
<%@ Control Language="c#" Inherits="RmsPM.Web.BiddingManage.Bidding" CodeFile="Bidding.ascx.cs" %>


<%@ Register Src="BiddingFileInfo.ascx" TagName="BiddingFileInfo" TagPrefix="uc3" %>
<%@ Register Src="../UserControls/inputunit.ascx" TagName="inputunit" TagPrefix="uc2" %>
<%@ Register TagPrefix="cc2" Namespace="Rms.ControlLb" Assembly="RmsPM.BLL" %>
<%@ Register TagPrefix="cc3" Namespace="RmsPM.BLL.ControlsLB" Assembly="RmsPM.BLL" %>
<%@ Register TagPrefix="uc1" TagName="UCSelectProject" Src="../UserControls/UCSelectProject.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<link href="../Images/infra.css" type="text/css" rel="stylesheet">

<script>
</script>

<div id="OperableDiv" runat="server">
    <table class="form" id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center"
        border="1">
        <tr>
            <td class="form-item">
                �ⶨ��Σ�</td>
            <td>
                <input class="input" id="txtTitle" style="width: 136px; height: 22px" type="text"
                    size="17" name="txtTitle" runat="server"><font color="red">*
            </td>
            <td class="form-item">
                ���</td>
            <td colspan="4">
                <font color="red">
                    <uc1:InputSystemGroup ID="inputSystemGroup" runat="server"></uc1:InputSystemGroup>
                    &nbsp;<font color="red">*</td>
        </tr>
        <tr>
            <td class="form-item">
                �������ţ�</td>
            <td>
                <uc2:inputunit ID="Inputunit1" runat="server" />
                <span style="color: #ff0000"></span>
            </td>
            <td class="form-item">
                ������Ŀ��</td>
            <td id="txtProject" style="width: 170px" colspan="3" runat="server">
            </td>
        </tr>
        <tr runat="server" id="TrBiddingTypeAndMoneyEdit">
            <td class="form-item">
                 �б����ͣ�
            </td>
            <td>
                <select id="selBiddingType" runat=server>
                 </select>&nbsp;&nbsp;
               
            </td>
            <td class="form-item">
                Ԥ�����ã�</td>
            <td style="height: 30px; border-right-style: none;" colspan="3" align="left" runat="server"
                id="txtMoney1">
                <asp:Label ID="txtMoney" runat="server" Text=""></asp:Label><input name="btnLastUpdate" id="btnLastUpdate" type="button" value="����޸�" class="button"
                                    runat="server">
            </td>
        </tr>
        <tr>
         <td class="form-item">
                 �б�ص㣺
            </td>
            <td  colspan="6">
                 <input class="input" id="TxtAddress" style="width: 220px;" type="text"
                    name="TxtAddress" runat="server">
            </td>
         
        </tr>
        <tr>
            <td class="form-item">
                ����ժҪ��</td>
            <td colspan="6">
                <textarea id="txtContent" style="width: 100%; height: 56px" name="Reason" rows="3"
                    cols="96" runat="server"></textarea></td>
        </tr>
        <tr>
            <td class="form-item">
                ��ע��</td>
            <td colspan="6">
                <textarea id="txtRemark" style="width: 100%; height: 56px" name="Reason" rows="3"
                    cols="96" runat="server"></textarea></td>
        </tr>
        <tr>
            <td class="form-item">
                ��Ҫ��Σ�</td>
            <td colspan="6">
                <input id="chkaccssory" type="checkbox" name="chkaccssory" runat="server">��Ҫ��Σ�׮�����ܰ������磬��װ�ޣ����磬����/Ļǽ����ǽ��װ�Σ�������ߣ�������Ҫ�豸�����ȡ���</td>
        </tr>
        <tr>
        <td colspan=7 style="width:100%;">
            <uc3:BiddingFileInfo ID="BiddingFileInfo1" runat="server" />
        </td>
        </tr>
        
        <tr runat="server" id="ArrangedDateTr">
            <td class="form-item">
                ���ͼֽ��</td>
            <td>
                <cc1:Calendar ID="txtArrangedDate" runat="server" CalendarResource="../Images/CalendarResource/"
                    CalendarMode="All">
                </cc1:Calendar>
            </td>
            <td class="form-item">
                �淶���ڣ�</td>
            <td>
                <cc1:Calendar ID="txtStandardDate" runat="server" CalendarResource="../Images/CalendarResource/"
                    CalendarMode="All">
                </cc1:Calendar>
            </td>
        
            <td class="form-item">
                Ԥ�����ڣ�</td>
            <td>
                <cc1:Calendar ID="txtPrejudicationDate" runat="server" CalendarResource="../Images/CalendarResource/"
                    CalendarMode="All">
                </cc1:Calendar>
            </td>
       
        </tr>
        <tr>
            <td class="form-item">
                �������ڣ�</td>
            <td >
                <cc1:Calendar ID="txtEmitDate" runat="server" CalendarResource="../Images/CalendarResource/"
                    CalendarMode="All">
                </cc1:Calendar>
            </td>
            <td class="form-item">
                �ر����ڣ�</td>
            <td >
                <cc1:Calendar ID="txtReturnDate" runat="server" CalendarResource="../Images/CalendarResource/"
                    CalendarMode="All">
                </cc1:Calendar>
            </td>
      
            <td class="form-item">
                �������ڣ�</td>
            <td >
                <cc1:Calendar ID="txtConfirmDate" runat="server" CalendarResource="../Images/CalendarResource/"
                    CalendarMode="All">
                </cc1:Calendar>
            </td>
        </tr>
    </table>
</div>
<div id="EyeableDiv" runat="server">
    <table class="form" id="Table2" cellspacing="0" cellpadding="0" width="100%" align="center"
        border="1">
        <tr>
            <td class="form-item" >
                �ⶩ��Σ�</td>
            <td id="tdTitle" runat="server">
            </td>
            <td class="form-item" >
                ���</td>
            <td id="tdType" runat="server">
            </td>
            <td class="form-item" >
                ������Ŀ��</td>
            <td id="tdProjectName" runat="server">
            </td>
        </tr>
        <tr>
            <td class="form-item" >
                ��Ҫ��Σ�</td>
            <td id="tdCostBudget" runat="server">
            </td>
           <td class="form-item">
                �������ţ�</td>
            <td colspan="3">
                <asp:Label ID="lblUnit" runat="server" Text=""></asp:Label>
            </td>
           
        </tr>
       
        <tr runat="server" id="TrBiddingTypeAndMoneyView">
            <td class="form-item" >
                 �б����ͣ�
            </td>
            <td>
                <asp:Label ID="tdBiddingType" runat="server" Text=""></asp:Label>
            </td>
            
            <td class="form-item" >
                Ԥ�����ã�
            </td>
            <td id="tdMoney1" colspan="3" runat="server">
             <asp:Label ID="tdMoney" runat="server" Text=""></asp:Label><input name="btnLastUpdate" id="btnlastUpdate2" type="button" value="����޸�" class="button"
                                    runat="server">
            </td>
        </tr>
         <tr>
         <td class="form-item" >
                 �б�ص㣺
            </td>
            <td  colspan="6">
               <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label>
            </td>
         
        </tr>
        <tr>
            <td class="form-item" >
                ����ժҪ��</td>
            <td id="tdContent" colspan="5" runat="server">
            </td>
        </tr>
        <tr>
            <td class="form-item" >
                ��ע��</td>
            <td id="tdRemark" colspan="5" runat="server">
            </td>
        </tr>
        <tr>
        <td colspan=7  style="width:100%;">
            <uc3:BiddingFileInfo ID="EyeaBiddingFileInfo" runat="server" />
        </td>
        </tr>
        <tr>
            <td class="form-item" >
                ���ͼֽ��</td>
            <td id="tdArrangedDate" style="height: 32px" runat="server">
            </td>
            <td class="form-item" >
                �淶���ڣ�</td>
            <td id="tdStandardDate" style="height: 32px" runat="server">
            </td>
            <td class="form-item">
                Ԥ�����ڣ�</td>
            <td id="tdPrejudicationDate"  runat="server">
            </td>
       
        </tr>
        <tr>
            <td class="form-item">
                �������ڣ�</td>
            <td id="tdEmitDate"  runat="server">
            </td>
            <td class="form-item">
                �ر����ڣ�</td>
            <td id="tdReturnDate"  runat="server">
            </td>
        
            <td class="form-item">
                �������ڣ�</td>
            <td id="tdConfirmDate" colspan=3 runat="server">
            </td>
        </tr>
    </table>
</div>



        
       
<script language="javascript">
function BiddingCheckSubmit()
{
	//var id = 
	if(document.all("<%=ClientID%>_txtTitle").value == "")
	{
		alert('�ⶩ��α�����д');
        return false;
	}
	return true;
}
function BiddingCheckMoney(obj)
{
	if(obj.value.length>0)
	{
		if(obj.value.match("^[0-9]+(\.[0-9]+)?$")==null)
		{
			obj.select();
			obj.focus();
			alert("Ԥ����������������");
			obj.select();
			return false;
		}
	}
	return true;				
}

function OpenCostBudget(code)
{
	OpenLargeWindow('../CostBudget/CostBudgetInfo.aspx?CostBudgetSetCode='+code,'�б�ƻ�ά��');
}
</script>

<div runat="server" id="gaoyuan">
</div>
