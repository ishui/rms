<%@ Page language="c#" Inherits="RmsPM.Web.Finance.PayoutDetailApportion" CodeFile="PayoutDetailApportion.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>�����̯</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/convert.js"></SCRIPT>
		<SCRIPT language="javascript" src="../images/checkbox.js"></SCRIPT>
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" scroll="no" onload="winload();">
		<form id="Form1" method="post" runat="server">
			<iframe src="../Cost/SavingWating.htm" style="DISPLAY:none" id="divHintSave" frameBorder="no"
				width="100%" scrolling="auto" height="100%"></iframe>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">�����̯</td>
				</tr>
				<tr>
					<td class="topic" vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<TD class="form-item" width="30%" id="tdPayout" runat="server">���/������</TD>
								<TD id="tdPayoutCodes" runat="server"></TD>
							</tr>
							<tr>
								<td class="form-item">�ܽ��(Ԫ)��</td>
								<TD><asp:label id="lblTotalMoney" Runat="Server"></asp:label></TD>
							</tr>
							<tr>
								<td class="form-item">��̯��ʽ��
								</td>
								<td><asp:Label Runat="server" ID="lblAllo"></asp:Label>
									<input class="button-small" id="btnSelectBuilding" onclick="javascript:SelectBuilding();return false; "
										type="button" value="ѡ���̯��ʽ" name="btnSelectBuilding" runat="server">
								</td>
							</tr>
							<tr>
								<td class="form-item">
								</td>
								<td><input id="chkManual" onclick="apportionManual();" type="checkbox" runat="server" NAME="chkManual">
									�ֶ���̯</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td vAlign="top">
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td class="intopic" width="200">��̯��ϸ</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
								<TR>
									<TD vAlign="top"><asp:datagrid id="dgList" runat="server" CssClass="List" Width="100%" ShowFooter="True" PageSize="15"
											AutoGenerateColumns="False" AllowSorting="True" CellPadding="0">
											<HeaderStyle CssClass="list-title"></HeaderStyle>
											<FooterStyle CssClass="list-title"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="���" FooterText="�ϼ�">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<ItemTemplate>
														<%# Container.ItemIndex + 1 %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="¥��/��λ��������">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<input type="hidden" id="txtBuildingCode" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.BuildingCode") %>' >
														<input type="hidden" id="txtBuildingName" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.BuildingName") %>' >
														<%# DataBinder.Eval(Container, "DataItem.BuildingName") %>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="��̯���(Ԫ)">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<ItemTemplate>
														<input type="text" onkeydown="if (event.keyCode==13) event.keyCode=9;" runat="server" id="txtMoney" size="30" class="input-nember" myid="txtMoney" onfocus="InputFocus(this);" onblur="InputBlur(this);" value='<%# DataBinder.Eval(Container, "DataItem.Money", "{0:N}") %>'>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblSumMoney"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle NextPageText="��ҳ&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;��ҳ" HorizontalAlign="Center"
												CssClass="ListHeadTr"></PagerStyle>
										</asp:datagrid></TD>
								</TR>
							</TABLE>
						</div>
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%" cellspacing="10" id="tableButton">
							<tr>
								<td align="center">
									<input id="btnSave" name="btnSave" type="button" class="submit" value="ȷ ��" runat="server"
										onclick="document.all.divHintSave.style.display = '';" onserverclick="btnSave_ServerClick"> <input id="btnCancel" name="btnCancel" type="button" class="submit" value="ȡ ��" onclick="javascript:self.close()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtProjectCode" type="hidden" runat="server"> <input id="txtAlloType" type="hidden" runat="server">
			<input id="txtTotalMoney" type="hidden" runat="server"> <input id="txtSelectCodes" type="hidden" runat="server">
			<input id="btnSelectBuildingReturn" type="button" value="ѡ��¥������" runat="server" onserverclick="btnSelectBuildingReturn_ServerClick">
			<input id="txtDetailCount" name="txDetailCount" runat="server"> <input id="txtSumMoney" name="txtSumMoney" runat="server">
		</form>
		<script language="javascript">
			//ѡ���̯¥��
			function SelectBuilding()
			{
				OpenCustomWindow("../SelectBox/SelectAlloBuilding.aspx?ProjectCode=" + Form1.txtProjectCode.value + "&ReturnFunc=SelectBuildingReturn","ѡ��¥��", 400, 540);
			}

			//ѡ���̯¥������
			function SelectBuildingReturn(AlloType, code, name)
			{
				Form1.txtAlloType.value = AlloType;
				Form1.txtSelectCodes.value = code;
				
				Form1.btnSelectBuildingReturn.click();
			}

			//�ֶ���̯
			function apportionManual()
			{
				
				var checked = document.all("chkManual").checked;
				var iCount = document.all.length;
				for ( var i=0;i<iCount;i++)
				{
					var obj = document.all(i);
					
					if (obj.myid == "txtMoney")
					{
						if ( checked )
							obj.style.display = "";
						else
							obj.style.display = "none";
					}
				}
			}
			
			function winload()
			{
				apportionManual();
			}			
			
var oldvalue;

function InputFocus(obj)
{
	oldvalue = obj.value;
}

function InputBlur(obj)
{
	if (obj.value != oldvalue)
	  CalcSum();
}

//����ϼ�
function CalcSum()
{
	var c = parseInt(Form1.txtDetailCount.value);
	var tempVal = 0;
	var sum = 0;
	
	for(i=0;i<c;i++)
	{
		tempVal = ConvertFloat(document.all("dgList__ctl" + (i + 2) + "_txtMoney").value);
		sum = sum + tempVal;
	}

	//�������뵽2λС��
	sum = Math.round(sum * 100) / 100;
	
	//��ʽ��
	sum = FormatNumber(sum, 2);
	
	document.all.txtSumMoney.value = sum;

	document.all("dgList__ctl" + (c + 2) + "_lblSumMoney").innerText = sum;
}

		</script>
	</body>
</HTML>
