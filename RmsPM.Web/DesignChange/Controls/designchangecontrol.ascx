<%@ Register TagPrefix="cc1" Namespace="Rms.ControlLb" Assembly="RmsPM.BLL" %>
<%@ Register TagPrefix="cc2" Namespace="RmsPM.BLL.ControlsLB" Assembly="RmsPM.BLL" %>
<%@ Control Language="c#" Inherits="RmsPM.Web.DesignChange.Controls.DesignChangeControl" CodeFile="DesignChangeControl.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../../UserControls/AttachMentAdd.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="uc2" TagName="InputSupplier" Src="../../UserControls/InputSupplier.ascx" %>
<table class="form" id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td class="form-item" width="100">ͼֽ�������</td>
		<td align="left"><cc1:textbox_lable id="TB_DesignName" CssClass="input" ReturnButton="Button1" IsEditMode="True" IsValidator="True"
				runat="server"></cc1:textbox_lable></td>
		<td class="form-item" width="100">ͼֽ����ţ�</td>
		<td align="left"><cc1:textbox_lable id="TB_DesignCode" CssClass="input" IsEditMode="True" IsValidator="True" runat="server"></cc1:textbox_lable></td>
	</tr>
	<tr>
		<td class="form-item">������ݣ�</td>
		<td align="left"><cc1:textbox_lable id="TB_DesignReason" CssClass="input" runat="server"></cc1:textbox_lable></td>
		<td class="form-item">�а��̣�</td>
		<td align="left"><uc2:inputsupplier id="InputSupplier1" runat="server"></uc2:inputsupplier>
			<DIV id="Lb_Supplier" style="DISPLAY: inline; HEIGHT: 15px" runat="server"></DIV>
		</td>
	</tr>
	<tr>
		<td class="form-item">ѡ���ͬ��</td>
		<td align="left"><FONT 
      face=����></FONT>
			<DIV id="Lb_Contract" style="DISPLAY: inline; HEIGHT: 15px" runat="server"></DIV>
			<cc2:selectbox id=SelectBox1 IsEditMode="true" runat="server" ProjectCode='<%=Request["ProjectCode"]%>' ImageUrl="../Images/ToolsItemSearch.Gif" Text="ѡ���ͬ" Url="../SelectBox/SelectContracts.aspx" Height="0px" BoxWith="120px" ButtonImage="../../Images/ToolsItemSearch.Gif" BoxCssClass="input">
			</cc2:selectbox><FONT face="����" color="#ff0000"></FONT><FONT face="����" color="#ff0000"></FONT><FONT face="����" color="#ff0000"></FONT></td>
		<td class="form-item">�������ޣ�</td>
		<td align="left"><cc1:calendar_lb id="Calendar_LB1" runat="server" CalendarResource="../Images/CalendarResource/"></cc1:calendar_lb></td>
	</tr>
	<tr>
		<td class="form-item" align="left">���ͼֽ��Ϣ��</td>
		<td class="form-item" align="left" colSpan="3"></td>
	</tr>
	<tr>
		<td colSpan="4"><uc1:attachmentadd id="AttachMentAdd1" runat="server"></uc1:attachmentadd><uc1:attachmentlist id="AttachMentList1" runat="server"></uc1:attachmentlist></td>
	</tr>
	<tr>
		<td class="form-item" align="left">���ԭ��</td>
		<td class="form-item" align="left" colSpan="3"></td>
	</tr>
	<tr>
		<td colSpan="4"><cc1:textbox_lable id="TB_DesignRemark" IsValidator="False" runat="server" MaxLength="20000" Height="59px"
				Width="100%" TextMode="MultiLine"></cc1:textbox_lable></td>
	</tr>
</table>
