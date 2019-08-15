<%@ Page language="c#" Inherits="RmsPM.Web.Project.PBSModify" CodeFile="PBSModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PBSModify</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/Style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="10" topMargin="10">
		<form id="Form1" method="post" runat="server">
			<FONT face="����" size="3">
				<TABLE id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="bottom" height="40">
							<asp:Label id="LabelTitle" runat="server" CssClass="TitleText"></asp:Label>
							<TABLE id="Table4" height="1" cellSpacing="0" cellPadding="0" width="100%" bgColor="#00309c"
								border="0">
								<TR>
									<TD><IMG src="../Images/spacer.gif"></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center">
							<TABLE id="Table2" borderColor="#f7f3f7" cellSpacing="0" cellPadding="3" rules="rows" width="100%"
								border="1">
								<TR>
									<TD align="left" width="100" bgColor="#f7f3f7">�ֽ������ƣ�</TD>
									<TD colSpan="3">
										<asp:TextBox id="TextBoxPBSName" runat="server" Width="250px"></asp:TextBox>
										<asp:RequiredFieldValidator id="RequiredFieldValidatorPBSName" runat="server" DESIGNTIMEDRAGDROP="217" ErrorMessage="*"
											Display="Dynamic" ControlToValidate="TextBoxPBSName"></asp:RequiredFieldValidator></TD>
								</TR>
								<TR>
									<TD align="left" bgColor="#f7f3f7">ռ�������</TD>
									<TD style="HEIGHT: 21px">
										<asp:TextBox id="TextBoxFloorSpace" runat="server" Width="100px"></asp:TextBox>M2
										<asp:RequiredFieldValidator id="RequiredFieldValidatorFloorSpace" runat="server" ErrorMessage="*" Display="Dynamic"
											ControlToValidate="TextBoxPBSName"></asp:RequiredFieldValidator>
										<asp:RegularExpressionValidator id="RegularExpressionValidatorFloorSpace" runat="server" ErrorMessage="*" Display="Dynamic"
											ControlToValidate="TextBoxFloorSpace" ValidationExpression="\d{1,}(\.\d*)?"></asp:RegularExpressionValidator></TD>
									<TD width="100" bgColor="#f7f3f7">�ݻ��ʣ�</TD>
									<TD style="HEIGHT: 21px">
										<asp:TextBox id="TextBoxVolumeRate" runat="server" Width="100px"></asp:TextBox>%
										<asp:RequiredFieldValidator id="RequiredFieldValidatorVolumeRate" runat="server" ErrorMessage="*" Display="Dynamic"
											ControlToValidate="TextBoxVolumeRate"></asp:RequiredFieldValidator>
										<asp:RegularExpressionValidator id="RegularExpressionValidatorVolumeRate" runat="server" ErrorMessage="*" Display="Dynamic"
											ControlToValidate="TextBoxVolumeRate" ValidationExpression="\d{1,}(\.\d*)?"></asp:RegularExpressionValidator></TD>
								</TR>
								<TR>
									<TD align="left" bgColor="#f7f3f7">���������</TD>
									<TD>
										<asp:TextBox id="TextBoxBuildingArea" runat="server" Width="100px"></asp:TextBox>M2
										<asp:RequiredFieldValidator id="RequiredFieldValidatorBuildingArea" runat="server" ErrorMessage="*" Display="Dynamic"
											ControlToValidate="TextBoxBuildingArea"></asp:RequiredFieldValidator>
										<asp:RegularExpressionValidator id="RegularExpressionValidatorBuildingArea" runat="server" ErrorMessage="*" Display="Dynamic"
											ControlToValidate="TextBoxVolumeRate" ValidationExpression="\d{1,}(\.\d*)?"></asp:RegularExpressionValidator></TD>
									<TD bgColor="#f7f3f7">�����ʣ�</TD>
									<TD>
										<asp:TextBox id="TextBoxRateForSale" runat="server" Width="100px"></asp:TextBox>%
										<asp:RequiredFieldValidator id="RequiredFieldValidatorRateForSale" runat="server" ErrorMessage="*" Display="Dynamic"
											ControlToValidate="TextBoxRateForSale"></asp:RequiredFieldValidator>
										<asp:RegularExpressionValidator id="RegularExpressionValidatorRateForSale" runat="server" ErrorMessage="*" Display="Dynamic"
											ControlToValidate="TextBoxRateForSale" ValidationExpression="\d{1,}(\.\d*)?"></asp:RegularExpressionValidator></TD>
								</TR>
								<TR>
									<TD align="left" bgColor="#f7f3f7">���������</TD>
									<TD>
										<asp:TextBox id="TextBoxAreaForSale" runat="server" Width="100px"></asp:TextBox>M2
										<asp:RequiredFieldValidator id="RequiredFieldValidatorAreaForSale" runat="server" ErrorMessage="*" Display="Dynamic"
											ControlToValidate="TextBoxAreaForSale"></asp:RequiredFieldValidator>
										<asp:RegularExpressionValidator id="RegularExpressionValidatorAreaForSale" runat="server" ErrorMessage="*" Display="Dynamic"
											ControlToValidate="TextBoxAreaForSale" ValidationExpression="\d{1,}(\.\d*)?"></asp:RegularExpressionValidator></TD>
									<TD bgColor="#f7f3f7">��Ʒ���ʣ�</TD>
									<TD>
										<asp:TextBox id="TextBoxProductRate" runat="server" Width="100px"></asp:TextBox>%
										<asp:RequiredFieldValidator id="RequiredFieldValidatorProductRate" runat="server" ErrorMessage="*" Display="Dynamic"
											ControlToValidate="TextBoxProductRate"></asp:RequiredFieldValidator>
										<asp:RegularExpressionValidator id="RegularExpressionValidatorProductRate" runat="server" ErrorMessage="*" Display="Dynamic"
											ControlToValidate="TextBoxProductRate" ValidationExpression="\d{1,}(\.\d*)?"></asp:RegularExpressionValidator></TD>
								</TR>
								<TR>
									<TD align="left" bgColor="#f7f3f7">ƽ��ÿ�������</TD>
									<TD>
										<asp:TextBox id="TextBoxAreaPerHouse" runat="server" Width="100px"></asp:TextBox>M2
										<asp:RequiredFieldValidator id="RequiredFieldValidatorAreaPerHouse" runat="server" ErrorMessage="*" Display="Dynamic"
											ControlToValidate="TextBoxAreaPerHouse"></asp:RequiredFieldValidator>
										<asp:RegularExpressionValidator id="RegularExpressionValidatorAreaPerHouse" runat="server" ErrorMessage="*" Display="Dynamic"
											ControlToValidate="TextBoxAreaPerHouse" ValidationExpression="\d{1,}(\.\d*)?"></asp:RegularExpressionValidator></TD>
									<TD bgColor="#f7f3f7">�ܻ�����</TD>
									<TD>
										<asp:TextBox id="TextBoxTotalHouseCount" runat="server" Width="100px"></asp:TextBox>��
										<asp:RequiredFieldValidator id="RequiredFieldValidatorTotalHouseCount" runat="server" ErrorMessage="*" Display="Dynamic"
											ControlToValidate="TextBoxTotalHouseCount"></asp:RequiredFieldValidator>
										<asp:RegularExpressionValidator id="RegularExpressionValidatorTotalHouseCount" runat="server" ErrorMessage="*" Display="Dynamic"
											ControlToValidate="TextBoxTotalHouseCount" ValidationExpression="\d{1,}(\.\d*)?"></asp:RegularExpressionValidator></TD>
								</TR>
								<TR>
									<TD align="left" bgColor="#f7f3f7">��ע��</TD>
									<TD colSpan="3">
										<asp:TextBox id="TextBoxDescription" runat="server" Width="100%" Height="70px" TextMode="MultiLine"></asp:TextBox></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD height="40" align="center">
							<asp:Button id="ButtonSave" runat="server" Text="����"></asp:Button>&nbsp;<INPUT type="button" value="ȡ��" onclick="window.close();"></TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
