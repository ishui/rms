<%@ Page language="c#" Inherits="RmsPM.Web.PicGroup.PicUpload" CodeFile="PicUpload.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ͼƬ�޸�</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<SCRIPT language="javascript">
<!--

function CheckSubmit(obj){
    //����ʱ����ѡ���ϴ�ͼƬ
    if (document.all.lblFileUploadHint.style.display == "")
    {
	    if( ""==obj.FileUpload.value )
	    {
		    alert("��ѡ��Ҫ�ϴ���ͼƬ��");
		    obj.FileUpload.focus();
		    return false;
	    }
    	
	}

    if ( ""!=obj.FileUpload.value )
    {
        showPicFun( obj.FileUpload.value );
        Form1.HidePicWidth.value = Form1.ShowPicFile.width;
        Form1.HidePicHeight.value = Form1.ShowPicFile.height;
    }

	return true;
}

function showPicFun( picSrc ){
	Form1.ShowPicFile.src = picSrc;
}

//-->
		</SCRIPT>
	</HEAD>
	<body scroll="no">
		<form id="Form1" onsubmit="return CheckSubmit(this);" method="post" runat="server">
			<table height="100%" width="100%" border="0">
				<tr>
					<td vAlign="top" align="center">
						<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<td align="left" background="../images/topic_bg.gif" colSpan="2"><STRONG>��������޸�һ��ͼƬ</STRONG></td>
							</TR>
							<TR>
								<TD class="form-item" align="center">ͼƬ·����</TD>
								<TD><INPUT id="FileUpload" onpropertychange="showPicFun(this.value);" type="file" name="FileUpload"
										runat="server" class="input" size="50"><span id="lblFileUploadHint" style="color:red" runat="server">*</span></TD>
							</TR>
							<TR>
								<TD class="form-item" align="center"><FONT face="����">ͼƬ���⣺</FONT></TD>
								<TD><INPUT type="text" class="input" id="TxtPicTitle" name="TxtPicTitle" runat="server" size="50"></TD>
							</TR>
							<TR>
								<TD class="form-item" align="center"><FONT face="����">��ע��</FONT></TD>
								<TD><TEXTAREA rows="3" cols="40" id="TxtPicRemark" name="TxtPicRemark" runat="server"></TEXTAREA></TD>
							</TR>
						</table>
						<INPUT class="submit" id="BtnSubmit" type="submit" value="ȷ ��" name="BtnSubmit" runat="server" onserverclick="BtnSubmit_ServerClick">&nbsp;
						<INPUT class="submit" id="btnDel" type="button" value="ɾ ��" name="btnDel" onclick="if (!confirm('ȷʵҪɾ����')) return false;" runat="server" onserverclick="btnDel_ServerClick">&nbsp;
						<INPUT class="submit" type="button" value="ȡ ��" onclick="window.close();">
					</td>
				</tr>
				<tr height="100%">
				    <td>
						<div style="position:absolute; OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%"><img id="ShowPicFile" runat="server"></div>
				    </td>
				</tr>
			</table>
			<INPUT id="HideMasterType" type="hidden" name="HideMasterType" runat="server"><INPUT id="HideMasterCode" type="hidden" name="HideMasterCode" runat="server"><INPUT id="HidePBSPicCode" type="hidden" name="HidePBSPicCode" runat="server"><INPUT id="HidePicWidth" type="hidden" name="HidePicWidth" runat="server"><INPUT id="HidePicHeight" type="hidden" name="HidePicHeight" runat="server">
		</form>
	</body>
</HTML>
