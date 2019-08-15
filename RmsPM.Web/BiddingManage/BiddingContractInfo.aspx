<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BiddingContractInfo.aspx.cs" Inherits="BiddingManage_BiddingContractInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>BiddingContractInfo</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../Rms.js"></script>
    <link href="../Images/infra.css" rel="stylesheet" type="text/css" />
</head>
<script language="javascript">
    <!--
	
	function DoPrint()
	{
		OpenPrintWindow("../Report/PrintList.aspx?FromControlID=tdPrint", "打印");
	}	

    //-->
	</script>	

<body>
    <form id="form1" runat="server">
    <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0" style="background-color:White">
			    <tr>
			        <td height="25">
			            <table class="table" id="table1" width="100%">
				            <tr>
					            <td class="tools-area" width="16">
					                <img src="../images/btn_li.gif" align="absMiddle" />
					            </td>
								<td class="tools-area">
									<input class="button" onclick="DoPrint();return false;" type="button" value="打印" name="btnPrint" />
									<input class="button" type="button" value="关闭" name="" onclick="window.close();">
								</td>
				            </tr>
			            </table>
			        </td>
			    </tr>
	          		
				<tr>
					<td class="table" valign="top" id="tdPrint" runat="server">
   <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td  align=center>
                                    <asp:Label ID="lblProjectName" runat="server" Text="Label"></asp:Label>  开发项目</td>
                               
                            </tr>
                            <tr>
                                <td  align=center>
                                    <asp:Label ID="lblBiddingName" runat="server" Text="Label"></asp:Label> 工程中标通知书</td>
                               
                            </tr>
                            <tr><td style="height: 19px"> <asp:Label ID="lblReturnCompany" runat="server" Text="Label"></asp:Label> 公司：</td></tr>
                            
                            <tr>
                                <td style="height: 38px">
                                     &nbsp; &nbsp;我司 <asp:Label ID="lblDtlBiddingName" runat="server" ></asp:Label> 工程,经工程招标及评审。现确定由贵司中标。

                                     接到本通知后，务于3日内前来我司接洽，商谈后续事宜。

                                </td>
                            </tr>
                            <tr><td> &nbsp; &nbsp;</td></tr>
                            <tr>
                                <td> &nbsp; &nbsp;特此通知<br><br><br></td>
                            
                            </tr>
                            
                            <tr><td align="center">公司 <asp:Label ID="lblCompanyName" runat="server" Text="Label"></asp:Label></td></tr>
                              <tr><td align="center">年 <asp:Label ID="lblYear" runat="server" Text="Label"></asp:Label> 月 <asp:Label ID="lblMonth" runat="server" Text="Label"></asp:Label> 日 <asp:Label ID="lblDay" runat="server" Text="Label"></asp:Label></td></tr>
                            
     </table>
     </td>
     </tr>
     </table>
    </form>
</body>
</html>
