<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>

<%@ Page Language="c#" Inherits="RmsPM.Web.Supplier.SupplierModify" CodeFile="SupplierModify.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="InputDictItem" Src="../UserControls/InputDictItem.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>厂商信息</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Images/index.css" type="text/css" rel="stylesheet">
</head>
<body scroll="no">
    <form id="Form1" method="post" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" bgcolor="white"
            border="0">
            <tr>
                <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                    厂商信息</td>
            </tr>
            <tr height="100%">
                <td class="topic" valign="top" style="height: 100%">
                    <div style="overflow: auto; width: 100%; position: absolute; height: 100%">
                        <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td class="form-item">
                                        名称：</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtSupplierName" runat="server" Width="352px" CssClass="input"></asp:TextBox><font
                                            color="red">*</font>
                                    </td>
                                    <td class="form-item" nowrap>
                                        简称：</td>
                                    <td nowrap width="20">
                                        <asp:TextBox ID="txtAbbreviation" runat="server" CssClass="input"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        所属类型：</td>
                                    <td>
                                        <uc1:InputSystemGroup ID="inputSystemGroup" runat="server"></uc1:InputSystemGroup>
                                        <font color="red">*</font></td>
                                    <td class="form-item" nowrap>
                                        法人代表：</td>
                                    <td nowrap width="20%">
                                        <asp:TextBox ID="txtArtificialPerson" runat="server" CssClass="input"></asp:TextBox></td>
                                    <td class="form-item">
                                        地区：</td>
                                    <td>
                                        <asp:TextBox ID="txtAreaCode" runat="server" CssClass="input"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap>
                                        注册地址：</td>
                                    <td nowrap colspan="3">
                                        <asp:TextBox ID="txtRegisteredAddress" runat="server" Width="352px" CssClass="input"></asp:TextBox></td>
                                    <td class="form-item">
                                        注册资金：</td>
                                    <td>
                                        <asp:TextBox ID="txtRegisteredCapital" runat="server" CssClass="input"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap width="13%">
                                        行业性质 ：</td>
                                    <td nowrap>
                                        <asp:TextBox ID="txtIndustryType" runat="server" CssClass="input"></asp:TextBox></td>
                                    <td class="form-item" nowrap width="13%">
                                        行业排名：</td>
                                    <td nowrap>
                                        <asp:TextBox ID="txtIndustrySort" runat="server" CssClass="input"></asp:TextBox></td>
                                    <td class="form-item" nowrap width="13%">
                                        税籍户管：</td>
                                    <td nowrap>
                                        <asp:TextBox ID="txtSJHG" runat="server" CssClass="input"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap>
                                        工商执照号 ：</td>
                                    <td nowrap>
                                        <asp:TextBox ID="txtLicenseID" runat="server" CssClass="input"></asp:TextBox></td>
                                    <td class="form-item" nowrap>
                                        税务执照号 ：</td>
                                    <td nowrap>
                                        <asp:TextBox ID="txtTaxID" runat="server" CssClass="input"></asp:TextBox></td>
                                    <td class="form-item" nowrap>
                                        税务代码：</td>
                                    <td nowrap>
                                        <asp:TextBox ID="txtTaxNo" runat="server" CssClass="input"></asp:TextBox></td>
                                </tr>
                                
                                <tr>
                                    <td class="form-item" nowrap>
                                        经营期限：</td>
                                    <td nowrap>
                                        <asp:TextBox ID="txtWorkTimeLimit" runat="server" CssClass="input"></asp:TextBox></td>
                                    <td class="form-item" nowrap>
                                        经营地址：</td>
                                    <td nowrap colspan="3">
                                        <asp:TextBox ID="txtWorkAddress" runat="server" Width="370px" CssClass="input"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap>
                                        企业性质：</td>
                                    <td nowrap>
                                        <asp:TextBox ID="txtStructure" runat="server" CssClass="input"></asp:TextBox></td>
                                    <td class="form-item" nowrap>
                                        经营范围：</td>
                                    <td nowrap colspan="3">
                                        <asp:TextBox ID="txtWorkScope" runat="server" Width="370px" CssClass="input"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap style="height: 33px">
                                        销售形式：</td>
                                    <td nowrap style="height: 33px">
                                        <select id="selSaleType" name="selSaleType" runat="server">
                                            <option value="" selected>--------请选择--------</option>
                                        </select>
                                    </td>
                                    <td class="form-item" nowrap style="height: 33px">
                                        品质类别：</td>
                                    <td nowrap colspan="3" style="height: 33px">
                                        <select id="selCharacterType" name="selCharacterType" runat="server">
                                            <option value="" selected>--------请选择--------</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap>
                                        CCC认证：</td>
                                    <td nowrap>
                                        <select id="selCCC" name="selCCC" runat="server">
                                            <option value="" selected>--------请选择--------</option>
                                            <option value="1">有</option>
                                            <option value="0">无</option>
                                        </select>
                                    </td>
                                    <td class="form-item" nowrap>
                                        ISO认证：</td>
                                    <td nowrap colspan="3">
                                        <select id="selISO" name="selISO" runat="server">
                                            <option value="" selected>--------请选择--------</option>
                                            <option value="1">有</option>
                                            <option value="0">无</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap>
                                        备注：</td>
                                    <td nowrap colspan="5">
                                        <asp:TextBox ID="txtRemark" runat="server" Width="650px" CssClass="input" Height="60px"
                                            Rows="2" TextMode="MultiLine"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                    <td class="form-item" nowrap>
                                        开户银行：</td>
                                    <td nowrap>
                                    <asp:TextBox runat="server" ID="tbxOpenBank" CssClass="input"></asp:TextBox>
                                    </td>
                                    <td class="form-item" nowrap>
                                        银行帐号：</td>
                                    <td  nowrap>
                                    <asp:TextBox runat="server" ID="tbxAccount" CssClass="input"></asp:TextBox>
                                    </td>
                                    <td class="form-item" nowrap>
                                        受款人：</td>
                                    <td  nowrap>
                                    <asp:TextBox runat="server" ID="tbxReciver" CssClass="input"></asp:TextBox>
                                    </td>
                                </tr>
                                            
                                    <tr>
                                        <td class="form-item" nowrap>
                                            联 系 人：</td>
                                        <td nowrap>
                                            <asp:TextBox ID="txtContractPerson" runat="server" CssClass="input"></asp:TextBox><span
                                                runat="server" id="SpanPerson"><font color="red">*</font></span></td>
                                        <td class="form-item" nowrap>
                                            联系电话：</td>
                                        <td>
                                            <asp:TextBox ID="txtOfficePhone" runat="server" CssClass="input"></asp:TextBox><span
                                                runat="server" id="SpanPhone"><font color="red">*</font></span></td>
                                        <td class="form-item" nowrap>
                                            邮政编码：</td>
                                        <td nowrap>
                                            <asp:TextBox ID="txtPostCode" runat="server" CssClass="input"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item" nowrap>
                                            手机：</td>
                                        <td nowrap>
                                            <asp:TextBox ID="txtMobile" runat="server" CssClass="input"></asp:TextBox></td>
                                        <td class="form-item" nowrap>
                                            传真：</td>
                                        <td>
                                            <asp:TextBox ID="txtFax" runat="server" CssClass="input"></asp:TextBox></td>
                                        <td class="form-item" nowrap>
                                            EMail：</td>
                                        <td nowrap>
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="input"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item" nowrap>
                                            网址：</td>
                                        <td nowrap>
                                            <asp:TextBox ID="txtWebAddress" runat="server" Width="300px" CssClass="input"></asp:TextBox></td>
                                        <td class="form-item" nowrap>
                                            信用等级：</td>
                                        <td nowrap>
                                            <uc1:InputDictItem ID="txtCreditLevel" runat="server" DictName="信用等级"></uc1:InputDictItem>
                                        </td>
                                        <td class="form-item" nowrap>
                                            资质等级：</td>
                                        <td nowrap runat="server" id="tdQualityGrade">
                                            <select id="selQualityGrade" name="selQualityGrade" runat="server">
                                                <option value="" selected>--------请选择--------</option>
                                            </select>
                                            &nbsp;级
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            资质：</td>
                                        <td nowrap colspan="5">
                                            <asp:TextBox ID="txtQuality" runat="server" Width="650px" CssClass="input" Height="60px"
                                                Rows="2" TextMode="MultiLine"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            业绩：</td>
                                        <td nowrap colspan="5">
                                            <asp:TextBox ID="txtAchievement" runat="server" Width="650px" CssClass="input" Height="60px"
                                                Rows="2" TextMode="MultiLine"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            产品服务：</td>
                                        <td colspan="5">
                                            <asp:TextBox ID="txtProduct" runat="server" Width="650px" CssClass="input" Height="60px"
                                                Rows="2" TextMode="MultiLine"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            评价意见：</td>
                                        <td nowrap colspan="5">
                                            <asp:TextBox ID="txtCheckOpinion" runat="server" Width="650px" CssClass="input" Height="60px"
                                                Rows="2" TextMode="MultiLine"></asp:TextBox></td>
                                    </tr>
                            </tbody>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellspacing="10" width="100%">
                        <tr>
                            <td align="center">
                                <input id="btnSave" name="btnSave" type="button" class="submit" value="确 定" runat="server"
                                    onserverclick="btnSave_ServerClick">&nbsp;
                                <input id="btnCancel" name="btnCancel" type="button" class="submit" value="取 消" onclick="javascript:self.close()">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>

    <script language="javascript" src="../Rms.js"></script>

    <script language="javascript">
<!--
	function OpenSupplierOpinion(SupplierOpinionCode,SupplierCode)
	{	
		OpenMiddleWindow('SupplierOpinionModify.aspx?act=view&SupplierCode=&SupplierOpinionCode='+SupplierOpinionCode , 'SupplierOpinionModify');
	}
	
	function doSelectSupplierType()
	{
		var code = Form1.txtSupplierTypeCode.value;
		OpenMiddleWindow('../Supplier/SelSupplierType.aspx?SupplierTypeCode='+ code  ,'选择厂商类型');
	}
	
	function getReturnSupplierType( supplierTypeCode,supplierTypeName)
	{
		Form1.txtSupplierTypeCode.value = supplierTypeCode;
		Form1.txtTypeName.value = supplierTypeName;
	}
//-->
    </script>

</body>
</html>
