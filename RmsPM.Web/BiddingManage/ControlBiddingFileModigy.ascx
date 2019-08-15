<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ControlBiddingFileModigy.ascx.cs" Inherits="RmsPM.Web.BiddingManage.ControlBiddingFileModigy" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>

<div id="OperableDiv" runat="server">
    <table class="form" id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center"
        border="1">
         <tr>  
             <td ForeColor="Red"  runat="server" id="tdBiddingFileState1" align=center colspan="4"></td>
        </tr>
         <TR>
		<TD class="form-item">项目名称：</TD>
		<TD  runat="server" id="txtProjectName" ><FONT face="宋体"></FONT></TD>
		
	
		<TD class="form-item">招标项目：</TD>
		
		<TD  runat="server" id="txtBiddingTitle"></TD>
	</TR>

        <tr>
            <td class="form-item">
                招标文件名称：</td>
            <td>
                <input class="input" id="TxtBiddingFileName"  type="text"
                    size="17" name="TxtBiddingFileName" runat="server"><font color="red">*
            </td>
      
       
             <td class="form-item">
                记录编号：</td>
            <td >
                <input class="input" id="TxtNumber"  type="text"
                    size="17" name="TxtNumber" runat="server"><font color="red">*
            </td>
        </tr>
        
		<tr>
			<td class="blackbordertdnobottom" colSpan="4">&nbsp;&nbsp;&nbsp;1.招标文件
				<uc1:AttachMentAdd id="AttachMentAdd1" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentAdd>
				
			</td>
			
		</tr>
       
    </table>
</div>
<div id="EyeableDiv" runat="server">
    <table class="form" id="Table2" cellspacing="0" cellpadding="0" width="100%" align="center"
        border="1">
         <tr>
            
                <td ForeColor="Red" align=center runat="server" id="tdBiddingFileState2"  colspan="4"></td>
           
           
        </tr>
         <TR>
		<TD class="form-item">项目名称：</TD>
		<TD runat="server" id="tdProjectName" ><FONT face="宋体"></FONT></TD>
		
	
		<TD class="form-item">招标项目：</TD>
		
		<TD runat="server" id="tdBiddingTitle"></TD>
	</TR>
       <tr>
            <td class="form-item">
                招标文件名称：</td>
            <td  runat="server" id="TdBiddingFileName">
              &nbsp;
            </td>
            
        
             <td class="form-item" >
                记录编号：</td>
            <td   runat="server"  id="TdNumber">
                &nbsp;
            </td>
        </tr>
        
			<TR>
				<TD class="blackbordertdnobottom" colSpan="4">1.招标文件<br>	
					<uc1:AttachMentList id="AttachMentList1" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentList><BR>
					
			</TR>
        
    </table>
</div>