<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SM_BiddingMessageManage.ascx.cs" Inherits="WorkFlowOperation_SM_BiddingMessageManage" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="AspWebControl" Assembly="AspWebControl" %>
<link href="/../Images/index.css" rel="stylesheet" type="text/css" />
<div id="OperableDiv" runat="server">
    <table cellspacing="0" cellpadding="0" width="100%" align="center" border="1">
        <tr>
            <td style="width: 15%" class="blackbordertdcontent">
                项目名称</td>
            <td style="width: 35%" class="blackbordertd" runat="server" id="txtProjectCode">
            </td>
            <td style="width: 15%" class="blackbordertdcontent">
                合同编号</td>
            <td style="width: 35%" class="blackbordertd">
                &nbsp;&nbsp;&nbsp;&nbsp;<input id="txtContractNember" type="text" runat="server"
                    class="input" name="txtContractNember"></td>
        </tr>
        <tr>
            <td class="blackbordertdcontent">
                合同名称</td>
            <td class="blackbordertd">
                &nbsp;&nbsp;&nbsp;&nbsp;<input id="txtContractName" type="text" runat="server" class="input"
                    name="txtContractName"><font face="宋体" color="#cc0066">*</font></td>
            <td class="blackbordertdcontent">
                合同类别</td>
            <td class="blackbordertd" runat="server" id="txtContractType">
                <font face="宋体"></font>
            </td>
        </tr>
        <tr>
            <td class="blackbordertdcontent">
                签约单位</td>
            <td class="blackbordertd" runat="server" id="txtSupplier" colspan="3">
            <br />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="DropSupplier" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropSupplier_SelectedIndexChanged">
                </asp:DropDownList><br /><br /></td>
        </tr>
        <tr>
            <td class="blackbordertdcontent">
                中标标段</td>
            <td class="blackbordertd" colspan="3">
                <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                </asp:CheckBoxList>&nbsp;</td>
        </tr>
        <tr>
            <td  class="blackbordertdcontent">
                预计签约日期</td>
            <td class="blackbordertd" colspan="3">
                &nbsp;&nbsp;&nbsp;&nbsp;<cc1:Calendar ID="txtContractDate" runat="server" CalendarResource="../Images/CalendarResource/"
                    CalendarMode="Date">
                </cc1:Calendar>
            </td>
        </tr>
        <tr>
            <td class="blackbordertdcontent">
                附件</td>
            <td class="blackbordertd" colspan="3" >
                <uc1:AttachMentAdd ID="AttachMentAdd1" runat="server"></uc1:AttachMentAdd>&nbsp;
            </td>
        </tr>
        <tr>
            <td class="blackbordertdcontent" nowrap>
                &nbsp;&nbsp;合同概述&nbsp;&nbsp;</td>
            <td class="blackbordertd" colspan="3" align="center">
                <textarea id="txtRemark" style="width: 98%; height: 56px" name="Reason" rows="3"
                    runat="server"></textarea></td>
        </tr>
    </table>
</div>
<div id="EyeableDiv" runat="server">
    <table cellspacing="0" cellpadding="0" width="100%" align="center" border="1">
        <tr>
            <td class="blackbordertdcontent">
                项目名称</td>
            <td class="blackbordertd" runat="server" id="tdProjectCode">
            </td>
            <td class="blackbordertdcontent">
                合同编号</td>
            <td class="blackbordertd" runat="server" id="tdContractNember">
            </td>
        </tr>
        <tr>
            <td class="blackbordertdcontent">
                合同名称</td>
            <td class="blackbordertd" runat="server" id="tdContractName">
            </td>
            <td class="blackbordertdcontent">
                合同类别</td>
            <td class="blackbordertd" runat="server" id="tdContractType">
            </td>
        </tr>
        <tr>
            <td class="blackbordertdcontent">
                签约单位</td>
            <td class="blackbordertd" runat="server" id="tdSupplier" colspan="3">
            </td>
            </tr>
            <tr>
            <td class="blackbordertdcontent">
                中标标段</td>
            <td class="blackbordertd" runat="server" id="tdBiddingDtl" colspan="3">
            </td>
            </tr>
            
            <tr>
            <td class="blackbordertdcontent">
                预计签约日期</td>
            <td class="blackbordertd" runat="server" id="tdContractDate" colspan="3">
            </td>
        </tr>
        <tr>
            <td class="blackbordertdcontent">
                附件</td>
            <td colspan="3" class="blackbordertd">
                <uc1:AttachMentList ID="AttachMentList1" runat="server"></uc1:AttachMentList>
                <font face="宋体">&nbsp;</font></td>
            <tr>
                <td class="blackbordertdcontent">
                    合同概述</td>
                <td class="blackbordertd" colspan="3" runat="server" id="tdRemark">
                </td>
            </tr>
    </table>
</div>

<script language="javascript">
function BiddingMessageCheck(obj)
{
	if(obj.value.length>0)
	{
		if(obj.value.match("^[0-9]+(\.[0-9]+)?$")==null)
		{
			obj.select();
			obj.focus();
			alert("合同编号请输入数字");
			obj.select();
			return false;
		}
	}
	return true;				
}
function BiddingMessageSubmitCheck()
{
	//if(document.all("<%=ClientID%>_txtContractName").value == "")
	//{
	//	alert('合同名称必须填写');
      //  return false;
	//}
	return true;
}
</script>


