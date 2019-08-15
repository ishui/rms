<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SM_Grade.ascx.cs" Inherits="WorkFlowOperation_SM_Grade" %>
<%@ Register Src="sm_SupplierGradeOpinion.ascx" TagName="sm_SupplierGradeOpinion"
    TagPrefix="uc2" %>
<%@ Register Src="sm_SupplierGrade.ascx" TagName="sm_SupplierGrade" TagPrefix="uc1" %>
<div runat="server" id="GradeOpinionDiv">
<uc2:sm_SupplierGradeOpinion ID="ucGradeOpinionControl" runat="server" />
    
 
</div>
<div runat="server" id="GradeDiv">
   <uc1:sm_SupplierGrade ID="ucGradeControl" runat="server" />
</div>

