<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BiddingFileInfo.ascx.cs" Inherits="RmsPM.Web.BiddingManage.BiddingFileInfo" %>
<table class="form"  id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center"
        border="0">
        
        <tr align=right>
            
            <td class="tools-area" align=right  width="100%">
             <table  id="tableToolBar" width="100%">
               <tr>
                       
                        <td align="right">
                                <input name="btnEdit" id="btnEdit" type="button" value=" 编辑 " class="button"
                                    runat="server" >
                                 <input name="btnWorkflow" id="btnWorkflow" type="button" value=" 提交审核 " class="button"
                                    runat="server" >  
                                <input name="btnModify" id="Button2" type="button" value=" 审核 " visible="false" class="button"
                                    runat="server">  
                       </td>
              </tr>
             </table>
            </td>
        </tr>
        <tr>
            <td style="width:100%;">
             <table class="form"  id="table2" width="100%">
               <tr>
                   
                    <td class="form-item" style="width: 15%">招标文件名称：<asp:TextBox ID="TxtBiddingFileCode" Visible="false" runat="server"></asp:TextBox></td>
                    <td runat="server" id="tdBiddingFileLink" colspan=2>&nbsp;</td>
             </tr>
             <tr>
              
                <td class="form-item" style="width: 15%; height: 21px;">招标文件审核状态：</td>
                <td runat="server" id="tdBiddingFileState" style="height: 21px"></td>
             </tr>
             <tr></tr>
             </table>
            </td>
            
        </tr>
       
        
        
    </table>
    
    
