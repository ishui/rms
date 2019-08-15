<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ZD_BiddingConditionFile.ascx.cs" Inherits="WorkFlowOperation_ZD_BiddingConditionFile" %>
<%@ Register TagPrefix="uc1" TagName="UCBiddingSupplierModify" Src="../BiddingManage/UCBiddingSupplierModify.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCBiddingSupplierList" Src="../BiddingManage/UCBiddingSupplierList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BiddingPrejudicationModify" Src="../BiddingManage/BiddingPrejudicationModify.ascx" %>
<%@ Reference Control="~/workflowcontrol/workflowtoolbar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>



<div id="OperableDiv" runat="server">
    <table class="form" id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center"
        border="1">
         <tr>  
             <td ForeColor="Red"  runat="server" id="tdBiddingConditionFileState1" align=center style="height: 21px" colspan="4"></td>
        </tr>
         <TR>
		<TD class="form-item">项目名称</TD>
		<TD class="blackbordertdcontent" runat="server" id="txtProjectName" colspan=3><FONT face="宋体"></FONT></TD>
		
	</TR>
	<TR>
		<TD class="form-item">招标内容/标段名称</TD>
		
		<TD class="blackbordertdcontent" colspan="3" runat="server" id="txtBiddingTitle"></TD>
	</TR>

        <tr>
            <td class="form-item">
                招标技术条件名称：</td>
            <td  style="width:80%" colspan=3>
                <input class="input" id="TxtBiddingConditionFileName" style="width: 136px; height: 22px" type="text"
                    size="17" name="TxtBiddingConditionFileName" runat="server"><font color="red">*
            </td>
      
        </tr>
        <tr>
             <td class="form-item">
                招标技术条件编号：</td>
            <td  style="width:80%" colspan=3>
                <input class="input" id="TxtNumber" style="width: 136px; height: 22px" type="text"
                    size="17" name="TxtNumber" runat="server"><font color="red">*
            </td>
        </tr>
        
		<tr>
			<td class="blackbordertd" colSpan="4">
			    <table width="100%" cellpadding="0" cellspacing="0" border="0">
			        <tr>
			            <td colspan="2"><br >1.招标范围：</td>
			        </tr>
			        <tr>
			            <td colspan="2">
			                <asp:TextBox ID="TxtZBFW" runat="server" TextMode="MultiLine" Width="100%" Rows="4"></asp:TextBox>
			            </td>
			        </tr>
			        <tr>
			            <td width="40">附件：</td>
			            <td align="left">
			                <uc1:AttachMentAdd id="AttachMentAdd1" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentAdd>
			            </td>
    		        </tr>
			        <tr>
			            <td colspan="2"><br >2.技术要求及指标：</td>
			        </tr>
			        <tr>
			            <td colspan="2">
			                <asp:TextBox ID="TxtJSYQ" runat="server" TextMode="MultiLine" Width="100%" Rows="4"></asp:TextBox>
			            </td>
			        </tr>
			        <tr>
			            <td width="40">附件：</td>
			            <td align="left">
			                <uc1:AttachMentAdd id="AttachMentAdd2" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentAdd>
			            </td>
    		        </tr>
    		        
    		        <tr>
			            <td colspan="2"><br >3.质量标准：</td>
			        </tr>
			        <tr>
			            <td colspan="2">
			                <asp:TextBox ID="TxtZLBZ" runat="server" TextMode="MultiLine" Width="100%" Rows="4"></asp:TextBox>
			            </td>
			        </tr>
			        <tr>
			            <td width="40">附件：</td>
			            <td align="left">
			                <uc1:AttachMentAdd id="AttachMentAdd3" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentAdd>
			            </td>
    		        </tr>
    		        
    		        <tr>
			            <td colspan="2"><br >4.工期：</td>
			        </tr>
			        <tr>
			            <td colspan="2">
			                <asp:TextBox ID="TxtGQ" runat="server" TextMode="MultiLine" Width="100%" Rows="4"></asp:TextBox>
			            </td>
			        </tr>
			        <tr>
			            <td width="40">附件：</td>
			            <td align="left">
			               <uc1:AttachMentAdd id="AttachMentAdd4" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentAdd>
			            </td>
    		        </tr>
    		         <tr>
			            <td colspan="2"><br >5.入场条件及总包管理方式：</td>
			        </tr>
			        <tr>
			            <td colspan="2">
			                <asp:TextBox ID="TxtRCTJ" runat="server" TextMode="MultiLine" Width="100%" Rows="4"></asp:TextBox>
			            </td>
			        </tr>
			        <tr>
			            <td width="40">附件：</td>
			            <td align="left">
			               <uc1:AttachMentAdd id="AttachMentAdd5" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentAdd>
			            </td>
    		        </tr>
    		        
    		        <tr>
			            <td colspan="2"><br >6.保修及售后服务：</td>
			        </tr>
			        <tr>
			            <td colspan="2">
			                <asp:TextBox ID="TxtSHFW" runat="server" TextMode="MultiLine" Width="100%" Rows="4"></asp:TextBox>
			            </td>
			        </tr>
			        <tr>
			            <td width="40">附件：</td>
			            <td align="left">
			              <uc1:AttachMentAdd id="AttachMentAdd6" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentAdd>
			            </td>
    		        </tr>
			    </table>
			    
			    
''              
				
			</td>
			
			
		</tr>
       
       
    </table>
</div>
<div id="EyeableDiv" runat="server">
    <table class="form" id="Table2" cellspacing="0" cellpadding="0" width="100%" align="center"
        border="1">
         <tr>
            
                <td ForeColor="Red" align=center runat="server" id="tdBiddingConditionFileState2" style="height: 21px" colspan="4"></td>
           
           
        </tr>
         <TR>
		<TD class="form-item">项目名称</TD>
		<TD class="blackbordertdcontent" colspan=3 runat="server" id="tdProjectName" ><FONT face="宋体"></FONT></TD>
		
	</TR>
	<TR>
		<TD class="form-item">招标内容/标段名称</TD>
		
		<TD class="blackbordertdcontent" colspan="3" runat="server" id="tdBiddingTitle"></TD>
	</TR>
       <tr>
            <td class="form-item">
                招标技术条件名称：</td>
            <td style="width:80%" colspan=3 runat=server id="TdBiddingConditionFileName">
              &nbsp;
            </td>
            
            
        </tr>
        <tr>
             <td class="form-item" >
                招标技术条件编号：</td>
            <td style="width:80%" colspan=3 runat=server  id="TdNumber">
                &nbsp;
            </td>
        </tr>
        
			<TR>
				<TD class="blackbordertd" colSpan="4">1.招标范围：
                 <uc1:AttachMentList id="AttachMentList1" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentList>
				 <br>
                招标范围备注：
                <asp:Label ID="lblZBFW" runat="server"></asp:Label>
				&nbsp;&nbsp;&nbsp;
               <br>2.技术要求及指标：
                <uc1:AttachMentList id="AttachMentList2" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentList>
				 <br>
                技术要求及指标备注：
                <asp:Label ID="lblJSYQ" runat="server"></asp:Label>
				&nbsp;&nbsp;&nbsp;<br>3.质量标准：
				<uc1:AttachMentList id="AttachMentList3" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentList>
				 <br>
                质量标准备注：
                <asp:Label ID="lblZLBZ" runat="server"></asp:Label>
				&nbsp;&nbsp;&nbsp;<br>4.工期：
				<uc1:AttachMentList id="AttachMentList4" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentList>
				 <br>
                工期备注：
                <asp:Label ID="lblGQ" runat="server"></asp:Label>
				&nbsp;&nbsp;&nbsp;<br>5.入场条件及总包管理方式：
				<uc1:AttachMentList id="AttachMentList5" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentList>
				 <br>
               入场条件及总包管理方式备注：
                <asp:Label ID="lblRCTJ" runat="server"></asp:Label>
				&nbsp;&nbsp;&nbsp;<br>6.保修及售后服务：
				<uc1:AttachMentList id="AttachMentList6" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentList>
				 <br>
                保修及售后服务备注：
                <asp:Label ID="lblSHFW" runat="server"></asp:Label>	
			</TR>
        
    </table>
</div>