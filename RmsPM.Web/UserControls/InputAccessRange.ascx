<%@ Register TagPrefix="uc1" TagName="InputStationUser" Src="../UserControls/InputStationUser.ascx" %>
<%@ Control Language="c#" Inherits="RmsPM.Web.UserControls.InputAccessRange" CodeFile="InputAccessRange.ascx.cs" %>
<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
<SCRIPT language="javascript">
<!--

var InputAccessRange_ClientID = "<%=ClientID%>";

//-->
</SCRIPT>
<SCRIPT language="javascript" src="../UserControls/InputAccessRange.js"></SCRIPT>
<uc1:inputstationuser id="ucPerson" runat="server" OnClientPost="InputAccessRange_ClientPost()">
</uc1:inputstationuser>
<input id="txtClassCode" type="hidden" name="txtClassCode" runat="server">
<input id="txtOperationCodes" type="hidden" name="txtOperationCodes" runat="server">
<input id="txtRelationCode" type="hidden" name="txtRelationCode" runat="server">
<input type="checkbox" runat="server" id="chkReadonly" visible="false" />
<input id="txtHint" type="hidden" name="txtHint" runat="server">
<div id="divSave" name="divSave" runat="server">
    <input type="button" runat="server" id="btnSave" name="btnSave" value="±£ ´æ" class="submit" onserverclick="btnSave_ServerClick" />&nbsp;&nbsp;
    <input type="button" runat="server" id="btnCancel" name="btnCancel" value="È¡ Ïû" class="submit" onserverclick="btnCancel_ServerClick" />
</div>