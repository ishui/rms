<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SM_PaymentAuditing.ascx.cs" Inherits="WorkFlowOperation_SM_PaymentAuditing" %>

<div id="OperableDiv" onkeydown="if(event.keyCode==13) event.keyCode=9" runat="server">
	<table cellSpacing="0" cellPadding="0" width="100%" border="0">
		<tr>
			<td class="blackbordertd" align="right" width="15%">��Ŀ���ƣ�&nbsp;</td>
			<td class="blackbordertdpaddingcontent" width="35%"><asp:label id="lblOperProjectName" Runat="server"></asp:label>&nbsp;
			</td>
			<td class="blackbordertd" align="right" width="15%">����������&nbsp;</td>
			<td class="blackbordertdpaddingcontent" width="35%">��&nbsp;<INPUT class="input" id="txtOperIssue" style="WIDTH: 50px; TEXT-ALIGN: right" type="text"
					name="txtOperIssue" runat="server"> &nbsp;��<font color="red">*</font>
			</td>
		</tr>
		<tr>
			<td class="blackbordertd" align="right">��ͬ���ƣ�&nbsp;</td>
			<td class="blackbordertdpaddingcontent"><asp:label id="lblOperContractName" Runat="server"></asp:label>&nbsp;
			</td>
			<td class="blackbordertd" align="right">��ͬ��ţ�&nbsp;</td>
			<td class="blackbordertdpaddingcontent"><asp:label id="lblOperContractID" Runat="server"></asp:label>&nbsp;
			<asp:HyperLink ID="HyperLink1" runat="server" Target="_blank"></asp:HyperLink>
			</td>
		</tr>
		<tr>
			<td class="blackbordertd" align="right">�а��ˣ��ְ�����&nbsp;</td>
			<td class="blackbordertdpaddingcontent" colSpan="3"><asp:label id="lblOperSupplierName" Runat="server"></asp:label>&nbsp;
			</td>
		</tr>
		<tr>
			<td class="blackbordertd" align="right">�а��ˣ��ܰ�����&nbsp;</td>
			<td class="blackbordertdpaddingcontent">
				<asp:label id="lblOperSupplier2Name" Runat="server"></asp:label>&nbsp;
			</td>
			<td class="blackbordertd" align="right">¥���ţ�&nbsp;</td>
			<td class="blackbordertdpaddingcontent">
				<asp:label id="lblOperBuilding" Runat="server"></asp:label>&nbsp;
			</td>
		</tr>
		<tr>
			<td vAlign="top" colSpan="2" height="100%">
				<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<tr>
						<td class="blackbordertdpaddingcontent">��ͬ��Լ���������Լ���ۺ�������<font color="red">*</font>
						</td>
					</tr>
					<tr>
						<td>
							<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr>
									<td class="blackbordertdpaddingcontent" style="HEIGHT: auto"><TEXTAREA id="txtOperCheckOpinion" style="WIDTH: 100%" name="txtOperCheckOpinion" rows="10"
											runat="server"></TEXTAREA>&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
			<td vAlign="top" colSpan="2" height="100%">
				<table id="tabOperMoney" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0"
					runat="server">
					<tr>
						<td class="blackbordertd" width="25%">&nbsp;</td>
						<td class="blackbordertdpaddingcontent" width="35%" align="center">RMB</td>
						<td class="blackbordertdpaddingcontent" width="40%" align="center"><asp:Label ID="lblOperMoneyType" runat="server"></asp:Label></td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">ԭ��ͬ��&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center" width="15">&nbsp;</td>
									<td><INPUT class="infra-input-nember" id="txtOperBudgetMoney" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperBudgetMoney" runat="server" visible="false">&nbsp;
									</td>
								</tr>
							</table>
						</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td><INPUT class="infra-input-nember" id="txtOperBudgetCash" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperBudgetCash" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>						
					</tr>
					<tr>
						<td class="blackbordertd" align="right">�ݶ����/ָ����&nbsp;</td>
						<td  class="blackbordertdpaddingcontent"> 
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center" width="15">-</td>
									<td><INPUT class="infra-input-nember" id="txtOperAdjustMoney" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperAdjustMoney" runat="server" visible="false">&nbsp;
									</td>
								</tr>
							</table>
						</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td><INPUT class="infra-input-nember" id="txtOperAdjustCash" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperAdjustCash" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">ʵ�ʽ�&nbsp;</td>
						<td   class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center" width="15">=</td>
									<td><INPUT class="infra-input-nember" id="txtOperOriginalMoney" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperOriginalMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td><INPUT class="infra-input-nember" id="txtOperOriginalCash" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperOriginalCash" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>						
					</tr>
					<tr>
						<td class="blackbordertd" align="right">�ۼ����������&nbsp;</td>
						<td   class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center" width="15">+</td>
									<td><INPUT class="infra-input-nember" id="txtOperTotalChangeMoney" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperTotalChangeMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td><INPUT class="infra-input-nember" id="txtOperTotalChangeCash" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperTotalChangeCash" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>						
					</tr>
					<tr>
						<td class="blackbordertd" align="right">��ͬ�ܶ�Ԥ�ƣ�&nbsp;</td>
						<td   class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center" width="15">=</td>
									<td><INPUT class="infra-input-nember" id="txtOperNewTotalMoney" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperNewTotalMoney" runat="server">
									</td>
								</tr>
							</table>
						</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td><INPUT class="infra-input-nember" id="txtOperNewTotalCash" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperNewTotalCash" runat="server">
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">�ۼ�Ӧ���&nbsp;</td>
						<td   class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center" width="15">&nbsp;</td>
									<td>
									    <INPUT class="infra-input-nember" id="txtOperTotalPayMoney" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperTotalPayMoney" runat="server">
									</td>
								</tr>
							</table>
						</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td>
									    <INPUT class="infra-input-nember" id="txtOperTotalPayCash" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperTotalPayCash" runat="server">&nbsp;
											<asp:Label ID="lblOperTotalPayMoneyPer" runat="server"></asp:Label>
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">���ۼ������&nbsp;</td>
						<td   class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center" width="15">+</td>
									<td><INPUT class="infra-input-nember" id="txtOperNegAHMoney" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperNegAHMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td><INPUT class="infra-input-nember" id="txtOperNegAHCash" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperNegAHCash" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>					
					<tr>
						<td style="border-top:gray solid" class="blackbordertd" align="right">����Ӧ���&nbsp;</td>
						<td style="border-top:gray solid" class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center" width="15">=</td>
									<td>
									    <INPUT class="infra-input-nember" id="txtOperTotalItemMoney" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperTotalItemMoney" runat="server">
									</td>
								</tr>
							</table>
						</td>
						<td  style="border-top:gray solid" class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td>
									    <INPUT class="infra-input-nember" id="txtOperTotalItemCash" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtOperTotalItemCash" runat="server">&nbsp;
											<asp:Label ID="lblOperTotalItemMoneyPer" runat="server"></asp:Label>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr id="trOperAttachment" runat="server">
			<td colSpan="4">
				<table cellSpacing="0" cellPadding="0" width="100%" border="0">
					<tr>
						<td class="blackbordertd" colSpan="2">�����嵥��</td>
					</tr>
					<tr>
						<td class="blackbordertd" width="40%"><asp:checkbox id="chkOperAttachMent1" Runat="server" Text="1.�а��̵ĸ������룺"></asp:checkbox></td>
						<td class="blackbordertdpaddingcontent"><uc1:attachmentadd id="myOperAttachMentAdd1" runat="server"></uc1:attachmentadd>&nbsp;
						</td>
					</tr>
					<tr>
						<td class="blackbordertd"><asp:checkbox id="chkOperAttachMent2" Runat="server" Text="2.����֤�飺"></asp:checkbox></td>
						<td class="blackbordertdpaddingcontent"><uc1:attachmentadd id="myOperAttachMentAdd2" runat="server"></uc1:attachmentadd>&nbsp;
						</td>
					</tr>
					<tr>
						<td class="blackbordertd"><asp:checkbox id="chkOperAttachMent3" Runat="server" Text="3.�����¼���ۼƱ���"></asp:checkbox></td>
						<td class="blackbordertdpaddingcontent"><uc1:attachmentadd id="myOperAttachMentAdd3" runat="server"></uc1:attachmentadd>&nbsp;
						</td>
					</tr>
					<tr>
						<td class="blackbordertd"><asp:checkbox id="chkOperAttachMent4" Runat="server" Text="4.���������"></asp:checkbox></td>
						<td class="blackbordertdpaddingcontent"><uc1:attachmentadd id="myOperAttachMentAdd4" runat="server"></uc1:attachmentadd>&nbsp;
						</td>
					</tr>
					<tr>
						<td class="blackbordertd"><asp:checkbox id="chkOperAttachMent5" Runat="server" Text="5.���̲�/����������֮רҵ�����"></asp:checkbox></td>
						<td class="blackbordertdpaddingcontent"><uc1:attachmentadd id="myOperAttachMentAdd5" runat="server"></uc1:attachmentadd>&nbsp;
						</td>
					</tr>
					<tr>
						<td class="blackbordertd"><asp:checkbox id="chkOperAttachMent6" Runat="server" Text="6.��Ʊ��"></asp:checkbox></td>
						<td class="blackbordertdpaddingcontent"><uc1:attachmentadd id="myOperAttachMentAdd6" runat="server"></uc1:attachmentadd>&nbsp;
						</td>
					</tr>
					<tr>
						<td class="blackbordertd"><asp:checkbox id="chkOperAttachMent7" Runat="server" Text="7.������"></asp:checkbox><asp:textbox id="txtOperOtherAttachMent" Runat="server" Width="250" CssClass="infra-input-nember"></asp:textbox></td>
						<td class="blackbordertdpaddingcontent"><uc1:attachmentadd id="myOperAttachMentAdd7" runat="server"></uc1:attachmentadd>&nbsp;
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td class="blackbordertdpaddingcontent" colSpan="4">
				<table cellSpacing="0" cellPadding="0" width="100%" border="0">
					<tr>
						<td width="120">���ʽ��</td>
						<td><asp:radiobuttonlist id="rdoOperPayType" Runat="server" RepeatColumns="4">
								<asp:ListItem Value="1" Selected>֧Ʊ</asp:ListItem>
								<asp:ListItem Value="2">����</asp:ListItem>
								<asp:ListItem Value="3">���</asp:ListItem>
								<asp:ListItem Value="4">����</asp:ListItem>
							</asp:radiobuttonlist></td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td colSpan="4">
				<table cellSpacing="0" cellPadding="0" width="100%" border="0">
					<tr>
						<td class="blackbordertd" align="right" width="15%">�տ��������ƣ�&nbsp;</td>
						<td class="blackbordertdpaddingcontent" width="18%"><INPUT class="infra-input-nember" id="txtOperBankName" style="WIDTH: 150px" type="text"
								name="txtOperBankName" runat="server">&nbsp;</td>
						<td class="blackbordertd" align="right" width="15%">�����������ڣ�&nbsp;</td>
						<td class="blackbordertdpaddingcontent" width="18%"><cc3:calendar id="dtOperInputDate" runat="server" ReadOnly="False" Display="True" CalendarResource="../Images/CalendarResource/"></cc3:calendar></td>
						<td class="blackbordertd" align="right" width="15%">���������ڣ�&nbsp;</td>
						<td class="blackbordertdpaddingcontent" width="19%"><cc3:calendar id="dtOperPayDate" runat="server" ReadOnly="False" Display="True" CalendarResource="../Images/CalendarResource/"></cc3:calendar><font color="red">*</font></td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">�տλ�����ʺţ�&nbsp;</td>
						<td class="blackbordertdpaddingcontent"><INPUT class="infra-input-nember" id="txtOperBankAccount" style="WIDTH: 150px" type="text"
								name="txtOperBankAccount" runat="server">&nbsp;</td>
						<td class="blackbordertd" align="right">�����ڼ��㷽ʽ��&nbsp;</td>
						<td class="blackbordertdpaddingcontent" colspan="3"><INPUT class="infra-input-nember" id="txtOperIssueMode" style="WIDTH: 400px" type="text"
								name="txtIssueMode" runat="server">&nbsp;</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</div>
<div id="EyeableDiv" runat="server">
	<table cellSpacing="0" cellPadding="0" width="100%" border="0">
		<tr>
			<td class="blackbordertd" align="right" width="20%">��Ŀ���ƣ�&nbsp;</td>
			<td class="blackbordertdpaddingcontent" width="40%"><asp:label id="lblEyeProjectName" Runat="server"></asp:label>&nbsp;
			</td>
			<td class="blackbordertd" align="right" width="15%">����������&nbsp;</td>
			<td class="blackbordertdpaddingcontent" width="25%">��&nbsp;<asp:label id="lblEyeIssue" runat="server"></asp:label>&nbsp;��
			</td>
		</tr>
		<tr>
			<td class="blackbordertd" align="right">��ͬ���ƣ�&nbsp;</td>
			<td class="blackbordertdpaddingcontent"><asp:label id="lblEyeContractName" Runat="server"></asp:label>&nbsp;
			</td>
			<td class="blackbordertd" align="right">��ͬ��ţ�&nbsp;</td>
			<td class="blackbordertdpaddingcontent"><asp:label id="lblEyeContractID" Runat="server"></asp:label>&nbsp;
			</td>
		</tr>
		<tr>
			<td class="blackbordertd" align="right">�а��ˣ��ְ�����&nbsp;</td>
			<td class="blackbordertdpaddingcontent" colSpan="3"><asp:label id="lblEyeSupplierName" Runat="server"></asp:label>&nbsp;
			</td>
		</tr>
		<tr>
			<td class="blackbordertd" align="right">�а��ˣ��ܰ�����&nbsp;</td>
			<td class="blackbordertdpaddingcontent">
				<asp:label id="lblEyeSupplier2Name" Runat="server"></asp:label>&nbsp;
			</td>
			<td class="blackbordertd" align="right">¥���ţ�&nbsp;</td>
			<td class="blackbordertdpaddingcontent">
				<asp:label id="lblEyeBuilding" Runat="server"></asp:label>&nbsp;
			</td>
		</tr>
		<tr>
			<td vAlign="top" colSpan="2" height="100%">
				<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<tr>
						<td class="blackbordertdpaddingcontent">��ͬ��Լ���������Լ���ۺ�������
						</td>
					</tr>
					<tr>
						<td>
							<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr>
									<td class="blackbordertdpaddingcontent" style="HEIGHT: auto"><asp:label id="lblEyeCheckOpinion" runat="server"></asp:label>&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
			<td vAlign="top" colSpan="2" height="100%">
				<table id="tabEyeMoney" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0"
					runat="server">
					<tr>
						<td class="blackbordertd" width="25%">&nbsp;</td>
						<td class="blackbordertdpaddingcontent" width="35%" align="center">RMB</td>
						<td class="blackbordertdpaddingcontent" width="40%" align="center"><asp:Label ID="lblEyeMoneyType" runat="server"></asp:Label></td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">ԭ��ͬ��&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center" width="15">&nbsp;</td>
									<td><INPUT class="infra-input-nember" id="txtEyeBudgetMoney" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeBudgetMoney" runat="server" visible="false">&nbsp;
									</td>
								</tr>
							</table>
						</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td><INPUT class="infra-input-nember" id="txtEyeBudgetCash" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeBudgetCash" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">�ݶ����/ָ����&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center" width="15">-</td>
									<td><INPUT class="infra-input-nember" id="txtEyeAdjustMoney" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeAdjustMoney" runat="server" visible="false">&nbsp;
									</td>
								</tr>
							</table>
						</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td><INPUT class="infra-input-nember" id="txtEyeAdjustCash" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeAdjustCash" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">ʵ�ʽ�&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center" width="15">=</td>
									<td><INPUT class="infra-input-nember" id="txtEyeOriginalMoney" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeOriginalMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td><INPUT class="infra-input-nember" id="txtEyeOriginalCash" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeOriginalCash" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">�ۼ����������&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
                                    <td align="center" width="15">+</td>								
									<td>
									    <INPUT class="infra-input-nember" id="txtEyeTotalChangeMoney" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeTotalChangeMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td>
									    <INPUT class="infra-input-nember" id="txtEyeTotalChangeCash" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeTotalChangeCash" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">��ͬ�ܶ�Ԥ�ƣ�&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td><INPUT class="infra-input-nember" id="txtEyeNewTotalMoney" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeNewTotalMoney" runat="server">
									</td>
								</tr>
							</table>
						</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td><INPUT class="infra-input-nember" id="txtEyeNewTotalCash" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeNewTotalCash" runat="server">
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">�ۼ�Ӧ���&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center" width="15">&nbsp;</td>
									<td>
										<INPUT class="infra-input-nember" id="txtEyeTotalPayMoney" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeTotalPayMoney" runat="server">
									</td>
								</tr>
							</table>
						</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td>
										<INPUT class="infra-input-nember" id="txtEyeTotalPayCash" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeTotalPayCash" runat="server">&nbsp;
											<asp:Label ID="lblEyeTotalPayMoneyPer" runat="server"></asp:Label>
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">���ۼ������&nbsp;</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center" width="15">+</td>
									<td><INPUT class="infra-input-nember" id="txtEyeNegAHMoney" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeNegAHMoney" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
						<td class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td><INPUT class="infra-input-nember" id="txtEyeNegAHCash" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeNegAHCash" runat="server">&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>					
					<tr>
						<td style="border-top:gray solid" class="blackbordertd" align="right">����Ӧ���&nbsp;</td>
						<td style="border-top:gray solid" class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td align="center" width="15">=</td>
									<td>
									    <INPUT class="infra-input-nember" id="txtEyeTotalItemMoney" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeTotalItemMoney" runat="server">
											
									</td>
								</tr>
							</table>
						</td>
						<td style="border-top:gray solid" class="blackbordertdpaddingcontent">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td>
									    <INPUT class="infra-input-nember" id="txtEyeTotalItemCash" style="FLOAT: left; WIDTH: 100px; TEXT-ALIGN: right"
											readOnly type="text" name="txtEyeTotalItemCash" runat="server">&nbsp;
											<asp:Label ID="lblEyeTotalItemMoneyPer" runat="server"></asp:Label>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr id="trEyeAttachment" runat="server">
			<td colSpan="4">
				<table cellSpacing="0" cellPadding="0" width="100%" border="0">
					<tr>
						<td class="blackbordertd" colSpan="2">�����嵥��</td>
					</tr>
					<tr>
						<td class="blackbordertd" width="40%"><asp:checkbox id="chkEyeAttachMent1" Runat="server" Text="1.�а��̵ĸ������룺" Enabled="False"></asp:checkbox></td>
						<td class="blackbordertdpaddingcontent"><uc1:attachmentlist id="myEyeAttachMentList1" runat="server"></uc1:attachmentlist>&nbsp;
						</td>
					</tr>
					<tr>
						<td class="blackbordertd"><asp:checkbox id="chkEyeAttachMent2" Runat="server" Text="2.����֤�飺" Enabled="False"></asp:checkbox></td>
						<td class="blackbordertdpaddingcontent"><uc1:attachmentlist id="myEyeAttachMentList2" runat="server"></uc1:attachmentlist>&nbsp;
						</td>
					</tr>
					<tr>
						<td class="blackbordertd"><asp:checkbox id="chkEyeAttachMent3" Runat="server" Text="3.�����¼���ۼƱ���" Enabled="False"></asp:checkbox></td>
						<td class="blackbordertdpaddingcontent"><uc1:attachmentlist id="myEyeAttachMentList3" runat="server"></uc1:attachmentlist>&nbsp;
						</td>
					</tr>
					<tr>
						<td class="blackbordertd"><asp:checkbox id="chkEyeAttachMent4" Runat="server" Text="4.���������" Enabled="False"></asp:checkbox></td>
						<td class="blackbordertdpaddingcontent"><uc1:attachmentlist id="myEyeAttachMentList4" runat="server"></uc1:attachmentlist>&nbsp;
						</td>
					</tr>
					<tr>
						<td class="blackbordertd"><asp:checkbox id="chkEyeAttachMent5" Runat="server" Text="5.���̲�/����������֮רҵ�����" Enabled="False"></asp:checkbox></td>
						<td class="blackbordertdpaddingcontent"><uc1:attachmentlist id="myEyeAttachMentList5" runat="server"></uc1:attachmentlist>&nbsp;
						</td>
					</tr>
					<tr>
						<td class="blackbordertd"><asp:checkbox id="chkEyeAttachMent6" Runat="server" Text="6.��Ʊ��" Enabled="False"></asp:checkbox></td>
						<td class="blackbordertdpaddingcontent"><uc1:attachmentlist id="myEyeAttachMentList6" runat="server"></uc1:attachmentlist>&nbsp;
						</td>
					</tr>
					<tr>
						<td class="blackbordertd"><asp:checkbox id="chkEyeAttachMent7" Runat="server" Text="7.������" Enabled="False"></asp:checkbox><asp:textbox id="txtEyeOtherAttachMent" Runat="server"  Width="300" CssClass="infra-input-nember"
								Enabled="False"></asp:textbox></td>
						<td class="blackbordertdpaddingcontent"><uc1:attachmentlist id="myEyeAttachMentList7" runat="server"></uc1:attachmentlist>&nbsp;
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td class="blackbordertdpaddingcontent" colSpan="4">
				<table cellSpacing="0" cellPadding="0" width="100%" border="0">
					<tr>
						<td width="120">���ʽ��</td>
						<td><asp:radiobuttonlist id="rdoEyePayType" Runat="server" RepeatColumns="4" Enabled="False">
								<asp:ListItem Value="1" Selected>֧Ʊ</asp:ListItem>
								<asp:ListItem Value="2">����</asp:ListItem>
								<asp:ListItem Value="3">���</asp:ListItem>
								<asp:ListItem Value="4">����</asp:ListItem>
							</asp:radiobuttonlist></td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td colSpan="4">
				<table cellSpacing="0" cellPadding="0" width="100%" border="0">
					<tr>
						<td class="blackbordertd" align="right" width="15%">�տ��������ƣ�&nbsp;</td>
						<td class="blackbordertdpaddingcontent" width="18%"><asp:label id="lblEyeBankName" runat="server"></asp:label>&nbsp;
						</td>
						<td class="blackbordertd" align="right" width="15%">�����������ڣ�&nbsp;</td>
						<td class="blackbordertdpaddingcontent" width="18%"><cc3:calendar id="dtEyeInputDate" runat="server" ReadOnly="true" Display="True" CalendarResource="../Images/CalendarResource/"></cc3:calendar></td>
						<td class="blackbordertd" align="right" width="15%">���������ڣ�&nbsp;</td>
						<td class="blackbordertdpaddingcontent" width="19%"><cc3:calendar id="dtEyePayDate" runat="server" ReadOnly="True" Display="True" CalendarResource="../Images/CalendarResource/"></cc3:calendar></td>
					</tr>
					<tr>
						<td class="blackbordertd" align="right">�տλ�����ʺţ�&nbsp;</td>
						<td class="blackbordertdpaddingcontent"><asp:label id="lblEyeBankAccount" runat="server"></asp:label>&nbsp;</td>
						<td class="blackbordertd" align="right">�����ڼ��㷽ʽ��&nbsp;</td>
						<td class="blackbordertdpaddingcontent" colspan="3"><asp:label id="lblEyeIssueMode" Runat="server"></asp:label>&nbsp;</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</div>
