<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="InputSubjectSet" Src="../UserControls/InputSubjectSet.ascx" %>
<%@ Page language="c#" Inherits="RmsPM.Web.PBS.PBSBuildModify" CodeFile="PBSBuildModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>¥���޸�</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
		<script>
		/*
	function iniBody()
	{
		var strName=document.all("txtIsArea").value;
		
		if (strName=="1")
		{
			document.all("trBuilding").style.display="none";
			document.all("trArea").style.display="block";
		}
		else
		{
			document.all("trBuilding").style.display="block";
			document.all("trArea").style.display="none";
		}
		
		
	}
	*/
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25"><span id="spanTitle" runat="server">¥��</span>�޸�</td>
				</tr>
				<tr id="trArea" style="DISPLAY: none" height="100%" runat="server">
					<td class="topic" vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="form-item" width="80">�������ƣ�</TD>
								<TD><input class="input" id="txtAreaName" type="text" size="30" name="txtAreaName" runat="server"><font color="red">*</font>
								</TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr id="trBuilding" height="100%" runat="server">
					<td class="topic" vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="form-item" noWrap width="100">���ƣ�</TD>
								<TD noWrap width="50%"><input class="input" id="txtBuildingName" type="text" size="30" name="txtBuildingName"
										runat="server"><font color="red">*</font></TD>
								<!--<TD class="form-item" noWrap width="100">¥����ƣ�</TD>
								<TD noWrap width="50%"><input class="input" id="txtBuildingShortName" type="text" size="30" name="txtBuildingShortName"
										runat="server"></TD>-->
							</TR>
							<TR>
								<TD class="form-item">��Ʒ���ͣ�</TD>
								<TD><SELECT id="sltPBSTypeCode" name="sltPBSTypeCode" runat="server">
										<OPTION value="" selected>------��ѡ��------</OPTION>
									</SELECT>
									<font color="red">*</font>
								</TD>
								<TD class="form-item">�� �� ����</TD>
								<TD><igtxt:webnumericedit id="txtFloorCount" runat="server" CssClass="infra-input-nember" Width="100px" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
										JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"></igtxt:webnumericedit><font color="red">*</font></TD>
							</TR>
							<TR>
								<TD class="form-item">Ͷ�����ʣ�</TD>
								<TD><SELECT id="sltInvestType" name="sltInvestType" runat="server">
										<OPTION value="" selected>------��ѡ��------</OPTION>
									</SELECT></TD>
								<TD class="form-item">ʹ�����ʣ�</TD>
								<TD><SELECT id="sltUseType" name="sltUseType" runat="server">
										<OPTION value="" selected>------��ѡ��------</OPTION>
									</SELECT></TD>
							</TR>
							<tr>
								<TD class="form-item">��������</TD>
								<TD><SELECT id="sltParentCode" name="sltParentCode" runat="server">
										<OPTION value="" selected>------��ѡ��------</OPTION>
									</SELECT></TD>
								<TD class="form-item">����</TD>
								<TD><SELECT id="sltDirection" size="1" name="sltDirection" runat="server">
										<OPTION value="" selected>------��ѡ��------</OPTION>
									</SELECT></TD>
							</tr>
							<tr>
								<TD class="form-item">�ƻ������</TD>
								<TD colSpan="3"><igtxt:webnumericedit id="txtHouseArea" runat="server" CssClass="infra-input-nember" Width="100px" JavaScriptFileNameCommon="../images/infragistics/20051/scripts/ig_shared.js"
										JavaScriptFileName="../images/infragistics/20051/scripts/ig_edit.js" ImageDirectory="../images/infragistics/images/"
										MinDecimalPlaces="Two"></igtxt:webnumericedit>ƽ��</TD>
								<TD class="form-item" style="DISPLAY: none">����ȥ��</TD>
								<TD style="DISPLAY: none"><SELECT id="sltWhither" name="sltWhither" runat="server">
										<OPTION value="" selected>------��ѡ��------</OPTION>
									</SELECT></TD>
							</tr>
							<TR>
								<TD class="form-item">��ע��</TD>
								<TD colSpan="3"><textarea id="txtRemark" style="WIDTH: 100%" name="txtRemark" rows="5" runat="server"></textarea></TD>
							</TR>
						</table>
						<!--
						<br>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="intopic" width="200">�������</td>
							</tr>
						</table>
						<uc1:InputSubjectSet id="ucInputSubjectSet" runat="server" TableName="BuildingSubjectSet" KeyFieldName="BuildingSubjectSetCode"
							CodeFieldName="BuildingCode"></uc1:InputSubjectSet>
						-->
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="10" width="100%">
							<tr>
								<td align="center"><input class="submit" id="btnSave" type="button" value="ȷ ��" name="btnSave" runat="server" onserverclick="btnSave_ServerClick">
									<input class="submit" id="btnDelete" style="DISPLAY: none" onclick="javascript:if(!window.confirm('ȷʵҪɾ��������¼��')) return false;"
										type="button" value="ɾ ��" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick"> <input class="submit" id="btnCancel" onclick="javascript:self.close()" type="button" value="ȡ ��"
										name="btnCancel">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="txtIsArea" type="hidden" name="txtIsArea" runat="server"> <input id="txtBuildingCode" type="hidden" name="txtBuildingCode" runat="server">
			<input id="txtParentCode" type="hidden" name="txtParentCode" runat="server"><input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server">
		</form>
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script language="javascript">
							<!--
							//-->
		</script>
	</body>
</HTML>
