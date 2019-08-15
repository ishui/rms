<%@ Control Language="C#" AutoEventWireup="true" CodeFile="manycurrencycostInfo.ascx.cs" Inherits="UserControls_manycurrencycostInfo" %>
<%@ Reference Control="~/usercontrols/exchangeratecontrol.ascx" %>
<%@ Reference Control="~/usercontrols/inputcostbudgetdtl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="inputCost" Src="inputCost.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="RmsPM.BLL.ControlsLB" Assembly="RmsPM.BLL" %>
<%@ Register TagPrefix="cc2" Namespace="Rms.ControlLb" Assembly="RmsPM.BLL" %>
<%@ Register TagPrefix="uc1" TagName="ExchangeRateControl" Src="ExchangeRateControl.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<script language="javascript" src="../images/convert.js"></script>
<script type="text/javascript" language="javascript" src="../UserControls/manycurrencycost.js"></script>
<asp:Label ID="lblCashDetail" runat="server" ></asp:Label>
