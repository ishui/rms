<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BiddingConditionFileInfo.ascx.cs" Inherits="RmsPM.Web.BiddingManage.BiddingConditionFileInfo" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>

<table class="form" id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center"
        border="0">
        
        <tr align=right>
            <td class="tools-area" colspan=4 align=right>
             <table  class="table" id="tableToolBar">
               <tr>
                        <td >
                                <input name="btnEdit" id="btnEdit" type="button" value=" 编辑 " class="button"
                                    runat="server" >
                                 <input name="btnWorkflow" id="btnWorkflow" type="button" value=" 提交审核 " class="button"
                                    runat="server" >  
                                <input name="btnModify" id="Btn" type="button" value=" 审核 " visible="false" class="button"
                                    runat="server">  
                       </td>
              </tr>
             </table>
            </td>
        </tr>
        
        
          <tr>
            <td class="form-item">
                招标技术条件名称：</td>
            <td style="width:80%" colspan="3" runat=server id="TdBiddingConditionFileName">
              &nbsp;
            </td>
            
            
        </tr>
        <tr>
             <td class="form-item" >
                招标技术条件编号：</td>
            <td style="width:80%" colspan="3"  runat=server  id="TdNumber">
                &nbsp;
            </td>
        </tr>
        
        
       <tr>
			<td class="blackbordertd" colSpan="4">
			    <table width="100%" cellpadding="0" cellspacing="0" border="0">
			        <tr>
			            <td colspan="2"><br >1.招标范围：</td>
			        </tr>
			        <tr>
			            <td colspan="2" height="80" valign="top">
			                <asp:Label ID="lblZBFW" runat="server"></asp:Label>
			            </td>
			        </tr>
			        <tr>
			            <td width="40">附件：</td>
			            <td align="left">
			                 <uc1:AttachMentList id="AttachMentList1" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentList>
			            </td>
    		        </tr>
			        <tr>
			            <td colspan="2" style="height: 117px"><br >2.技术要求及指标：</td>
			        </tr>
			        <tr>
			            <td colspan="2"  height="80" valign="top">
			                <asp:Label ID="lblJSYQ" runat="server"></asp:Label>
			            </td>
			        </tr>
			        <tr>
			            <td width="40">附件：</td>
			            <td align="left">
			                <uc1:AttachMentList id="AttachMentList2" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentList>
			            </td>
    		        </tr>
    		        
    		        <tr>
			            <td colspan="2"><br >3.质量标准：</td>
			        </tr>
			        <tr>
			            <td colspan="2"  height="80" valign="top">
			                <asp:Label ID="lblZLBZ" runat="server"></asp:Label>
			            </td>
			        </tr>
			        <tr>
			            <td width="40">附件：</td>
			            <td align="left">
			                <uc1:AttachMentList id="AttachMentList3" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentList>
			            </td>
    		        </tr>
    		        
    		        <tr>
			            <td colspan="2"><br >4.工期：</td>
			        </tr>
			        <tr>
			            <td colspan="2"  height="80" valign="top">
			                  <asp:Label ID="lblGQ" runat="server"></asp:Label>
			            </td>
			        </tr>
			        <tr>
			            <td width="40">附件：</td>
			            <td align="left">
			               <uc1:AttachMentList id="AttachMentList4" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentList>
			            </td>
    		        </tr>
    		         <tr>
			            <td colspan="2"><br >5.入场条件及总包管理方式：</td>
			        </tr>
			        <tr>
			            <td colspan="2"  height="80" valign="top">
			               <asp:Label ID="lblRCTJ" runat="server"></asp:Label>
			            </td>
			        </tr>
			        <tr>
			            <td width="40">附件：</td>
			            <td align="left">
			               <uc1:AttachMentList id="AttachMentList5" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentList>
			            </td>
    		        </tr>
    		        
    		        <tr>
			            <td colspan="2"><br >6.保修及售后服务：</td>
			        </tr>
			        <tr>
			            <td colspan="2"  height="80" valign="top">
			                  <asp:Label ID="lblSHFW" runat="server"></asp:Label>	
			            </td>
			        </tr>
			        <tr>
			            <td width="40">附件：</td>
			            <td align="left">
			              <uc1:AttachMentList id="AttachMentList6" runat="server" CtrlPath="../UserControls/"></uc1:AttachMentList>
			            </td>
    		        </tr>
			    </table>
			    
			    
              
				
			</td>
			
			
		</tr>
             <tr>
              
                <td class="form-item" >审核状态：</td>
                <td runat="server" id="tdBiddingFileState" style="width:80%" ></td>
             </tr>
             <tr></tr>
             </table>
          