<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Control Language="c#" Inherits="RmsPM.Web.BiddingManage.BiddingMessageModify"
    CodeFile="BiddingMessageModify.ascx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="AspWebControl" Assembly="AspWebControl" %>
<link href="/../Images/index.css" rel="stylesheet" type="text/css" />
<div id="OperableDiv" runat="server">
    <table cellspacing="0" cellpadding="0" width="100%" align="center" border="1">
        <tr>
            <td style="width: 15%" class="blackbordertdcontent">
                ��Ŀ����</td>
            <td style="width: 35%" class="blackbordertd" runat="server" id="txtProjectCode">
            </td>
            <td style="width: 15%" class="blackbordertdcontent">
                ��ͬ���</td>
            <td style="width: 35%" class="blackbordertd">
                &nbsp;&nbsp;&nbsp;&nbsp;<input id="txtContractNember" type="text" runat="server"
                    class="input" name="txtContractNember"></td>
        </tr>
        <tr>
            <td class="blackbordertdcontent">
                ��ͬ����</td>
            <td class="blackbordertd">
                &nbsp;&nbsp;&nbsp;&nbsp;<input id="txtContractName" type="text" runat="server" class="input"
                    name="txtContractName"><font face="����" color="#cc0066">*</font></td>
            <td class="blackbordertdcontent">
                ��ͬ���</td>
            <td class="blackbordertd" runat="server" id="txtContractType">
                <font face="����"></font>
            </td>
        </tr>
        <tr>
            <td class="blackbordertdcontent">
                ǩԼ��λ</td>
            <td class="blackbordertd" runat="server" id="txtSupplier" colspan="3">
            <br />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="DropSupplier" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropSupplier_SelectedIndexChanged">
                </asp:DropDownList><br /><br /></td>
        </tr>
        <tr>
            <td class="blackbordertdcontent">
                �б���</td>
            <td class="blackbordertd" colspan="3">
                <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                </asp:CheckBoxList></td>
        </tr>
        <tr>
            <td style="height: 35px" class="blackbordertdcontent">
                Ԥ��ǩԼ����</td>
            <td class="blackbordertd" colspan="3">
                &nbsp;&nbsp;&nbsp;&nbsp;<cc1:Calendar ID="txtContractDate" runat="server" CalendarResource="../Images/CalendarResource/"
                    CalendarMode="Date">
                </cc1:Calendar>
            </td>
        </tr>
        <tr>
            <td class="blackbordertdcontent">
                ����</td>
            <td colspan="3" class="blackbordertd">
                <uc1:AttachMentAdd ID="AttachMentAdd1" runat="server"></uc1:AttachMentAdd>
                <font face="����">&nbsp;</font></td>
        </tr>
        <tr>
            <td class="blackbordertdcontent" nowrap>
                &nbsp;&nbsp;��ͬ����&nbsp;&nbsp;</td>
            <td class="blackbordertd" colspan="3">
                <textarea id="txtRemark" style="width: 100%; height: 56px" name="Reason" rows="3"
                    runat="server"></textarea></td>
        </tr>
    </table>
</div>
<div id="EyeableDiv" runat="server">
    <table cellspacing="0" cellpadding="0" width="100%" align="center" border="1">
        <tr>
            <td class="blackbordertdcontent">
                ��Ŀ����</td>
            <td class="blackbordertd" runat="server" id="tdProjectCode">
            </td>
            <td class="blackbordertdcontent">
                ��ͬ���</td>
            <td class="blackbordertd" runat="server" id="tdContractNember">
            </td>
        </tr>
        <tr>
            <td class="blackbordertdcontent">
                ��ͬ����</td>
            <td class="blackbordertd" runat="server" id="tdContractName">
            </td>
            <td class="blackbordertdcontent">
                ��ͬ���</td>
            <td class="blackbordertd" runat="server" id="tdContractType">
            </td>
        </tr>
        <tr>
            <td class="blackbordertdcontent">
                ǩԼ��λ</td>
            <td class="blackbordertd" runat="server" id="tdSupplier" colspan="3">
            </td>
            </tr>
            <tr>
            <td class="blackbordertdcontent">
                �б���</td>
            <td class="blackbordertd" runat="server" id="tdBiddingDtl" colspan="3">
            </td>
            </tr>
            
            <tr>
            <td class="blackbordertdcontent">
                Ԥ��ǩԼ����</td>
            <td class="blackbordertd" runat="server" id="tdContractDate" colspan="3">
            </td>
        </tr>
        <tr>
            <td class="blackbordertdcontent">
                ����</td>
            <td colspan="3" class="blackbordertd">
                <uc1:AttachMentList ID="AttachMentList1" runat="server"></uc1:AttachMentList>
                <font face="����">&nbsp;</font></td>
            <tr>
                <td class="blackbordertdcontent">
                    ��ͬ����</td>
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
			alert("��ͬ�������������");
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
	//	alert('��ͬ���Ʊ�����д');
      //  return false;
	//}
	return true;
}
</script>

