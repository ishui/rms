<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BiddingTypeModify.aspx.cs" Inherits="BiddingManage_BiddingTypeModify" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>招投标类型</title>
     <link href="../Images/index.css" type="text/css" rel="stylesheet"/>
    <link href="../Images/infra.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="../Rms.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">招投标类型</td>
				</tr>
				<tr height="100%">
					<td vAlign="top" align="center">
				
						    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
							    <TR>
								    <td align="center">
                                        <select id="selBiddingType" runat=server>
                                            
                                        </select>&nbsp;&nbsp;<font color="red">*</font>
								    </td>
							    </TR>
							    <tr>
							        <td align="center">
							            
							        </td>
							    
							    </tr>
							    
						    </table>
					
					</td>
				</tr>
				<tr>
					<td align="center">
						<input id="btnSave" name="btnSave" type="button" class="submit" onclick="javascript:if(!window.confirm('确实要审核吗 ？')) return false;" value="确 定" runat="server" onserverclick="btnSave_ServerClick">
					 <input id="btnCancel" name="btnCancel" type="button" class="submit" value="取 消" onclick="javascript:self.close()">
					</td>		
				</tr>
			</table>
    </div>
    </form>
</body>
</html>
