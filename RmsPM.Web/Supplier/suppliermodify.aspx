<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>

<%@ Page Language="c#" Inherits="RmsPM.Web.Supplier.SupplierModify" CodeFile="SupplierModify.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="InputDictItem" Src="../UserControls/InputDictItem.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>������Ϣ</title>
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
                    ������Ϣ</td>
            </tr>
            <tr height="100%">
                <td class="topic" valign="top" style="height: 100%">
                    <div style="overflow: auto; width: 100%; position: absolute; height: 100%">
                        <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td class="form-item">
                                        ���ƣ�</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtSupplierName" runat="server" Width="352px" CssClass="input"></asp:TextBox><font
                                            color="red">*</font>
                                    </td>
                                    <td class="form-item" nowrap>
                                        ��ƣ�</td>
                                    <td nowrap width="20">
                                        <asp:TextBox ID="txtAbbreviation" runat="server" CssClass="input"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        �������ͣ�</td>
                                    <td>
                                        <uc1:InputSystemGroup ID="inputSystemGroup" runat="server"></uc1:InputSystemGroup>
                                        <font color="red">*</font></td>
                                    <td class="form-item" nowrap>
                                        ���˴���</td>
                                    <td nowrap width="20%">
                                        <asp:TextBox ID="txtArtificialPerson" runat="server" CssClass="input"></asp:TextBox></td>
                                    <td class="form-item">
                                        ������</td>
                                    <td>
                                        <asp:TextBox ID="txtAreaCode" runat="server" CssClass="input"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap>
                                        ע���ַ��</td>
                                    <td nowrap colspan="3">
                                        <asp:TextBox ID="txtRegisteredAddress" runat="server" Width="352px" CssClass="input"></asp:TextBox></td>
                                    <td class="form-item">
                                        ע���ʽ�</td>
                                    <td>
                                        <asp:TextBox ID="txtRegisteredCapital" runat="server" CssClass="input"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap width="13%">
                                        ��ҵ���� ��</td>
                                    <td nowrap>
                                        <asp:TextBox ID="txtIndustryType" runat="server" CssClass="input"></asp:TextBox></td>
                                    <td class="form-item" nowrap width="13%">
                                        ��ҵ������</td>
                                    <td nowrap>
                                        <asp:TextBox ID="txtIndustrySort" runat="server" CssClass="input"></asp:TextBox></td>
                                    <td class="form-item" nowrap width="13%">
                                        ˰�����ܣ�</td>
                                    <td nowrap>
                                        <asp:TextBox ID="txtSJHG" runat="server" CssClass="input"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap>
                                        ����ִ�պ� ��</td>
                                    <td nowrap>
                                        <asp:TextBox ID="txtLicenseID" runat="server" CssClass="input"></asp:TextBox></td>
                                    <td class="form-item" nowrap>
                                        ˰��ִ�պ� ��</td>
                                    <td nowrap>
                                        <asp:TextBox ID="txtTaxID" runat="server" CssClass="input"></asp:TextBox></td>
                                    <td class="form-item" nowrap>
                                        ˰����룺</td>
                                    <td nowrap>
                                        <asp:TextBox ID="txtTaxNo" runat="server" CssClass="input"></asp:TextBox></td>
                                </tr>
                                
                                <tr>
                                    <td class="form-item" nowrap>
                                        ��Ӫ���ޣ�</td>
                                    <td nowrap>
                                        <asp:TextBox ID="txtWorkTimeLimit" runat="server" CssClass="input"></asp:TextBox></td>
                                    <td class="form-item" nowrap>
                                        ��Ӫ��ַ��</td>
                                    <td nowrap colspan="3">
                                        <asp:TextBox ID="txtWorkAddress" runat="server" Width="370px" CssClass="input"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap>
                                        ��ҵ���ʣ�</td>
                                    <td nowrap>
                                        <asp:TextBox ID="txtStructure" runat="server" CssClass="input"></asp:TextBox></td>
                                    <td class="form-item" nowrap>
                                        ��Ӫ��Χ��</td>
                                    <td nowrap colspan="3">
                                        <asp:TextBox ID="txtWorkScope" runat="server" Width="370px" CssClass="input"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap style="height: 33px">
                                        ������ʽ��</td>
                                    <td nowrap style="height: 33px">
                                        <select id="selSaleType" name="selSaleType" runat="server">
                                            <option value="" selected>--------��ѡ��--------</option>
                                        </select>
                                    </td>
                                    <td class="form-item" nowrap style="height: 33px">
                                        Ʒ�����</td>
                                    <td nowrap colspan="3" style="height: 33px">
                                        <select id="selCharacterType" name="selCharacterType" runat="server">
                                            <option value="" selected>--------��ѡ��--------</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap>
                                        CCC��֤��</td>
                                    <td nowrap>
                                        <select id="selCCC" name="selCCC" runat="server">
                                            <option value="" selected>--------��ѡ��--------</option>
                                            <option value="1">��</option>
                                            <option value="0">��</option>
                                        </select>
                                    </td>
                                    <td class="form-item" nowrap>
                                        ISO��֤��</td>
                                    <td nowrap colspan="3">
                                        <select id="selISO" name="selISO" runat="server">
                                            <option value="" selected>--------��ѡ��--------</option>
                                            <option value="1">��</option>
                                            <option value="0">��</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" nowrap>
                                        ��ע��</td>
                                    <td nowrap colspan="5">
                                        <asp:TextBox ID="txtRemark" runat="server" Width="650px" CssClass="input" Height="60px"
                                            Rows="2" TextMode="MultiLine"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                    <td class="form-item" nowrap>
                                        �������У�</td>
                                    <td nowrap>
                                    <asp:TextBox runat="server" ID="tbxOpenBank" CssClass="input"></asp:TextBox>
                                    </td>
                                    <td class="form-item" nowrap>
                                        �����ʺţ�</td>
                                    <td  nowrap>
                                    <asp:TextBox runat="server" ID="tbxAccount" CssClass="input"></asp:TextBox>
                                    </td>
                                    <td class="form-item" nowrap>
                                        �ܿ��ˣ�</td>
                                    <td  nowrap>
                                    <asp:TextBox runat="server" ID="tbxReciver" CssClass="input"></asp:TextBox>
                                    </td>
                                </tr>
                                            
                                    <tr>
                                        <td class="form-item" nowrap>
                                            �� ϵ �ˣ�</td>
                                        <td nowrap>
                                            <asp:TextBox ID="txtContractPerson" runat="server" CssClass="input"></asp:TextBox><span
                                                runat="server" id="SpanPerson"><font color="red">*</font></span></td>
                                        <td class="form-item" nowrap>
                                            ��ϵ�绰��</td>
                                        <td>
                                            <asp:TextBox ID="txtOfficePhone" runat="server" CssClass="input"></asp:TextBox><span
                                                runat="server" id="SpanPhone"><font color="red">*</font></span></td>
                                        <td class="form-item" nowrap>
                                            �������룺</td>
                                        <td nowrap>
                                            <asp:TextBox ID="txtPostCode" runat="server" CssClass="input"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item" nowrap>
                                            �ֻ���</td>
                                        <td nowrap>
                                            <asp:TextBox ID="txtMobile" runat="server" CssClass="input"></asp:TextBox></td>
                                        <td class="form-item" nowrap>
                                            ���棺</td>
                                        <td>
                                            <asp:TextBox ID="txtFax" runat="server" CssClass="input"></asp:TextBox></td>
                                        <td class="form-item" nowrap>
                                            EMail��</td>
                                        <td nowrap>
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="input"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item" nowrap>
                                            ��ַ��</td>
                                        <td nowrap>
                                            <asp:TextBox ID="txtWebAddress" runat="server" Width="300px" CssClass="input"></asp:TextBox></td>
                                        <td class="form-item" nowrap>
                                            ���õȼ���</td>
                                        <td nowrap>
                                            <uc1:InputDictItem ID="txtCreditLevel" runat="server" DictName="���õȼ�"></uc1:InputDictItem>
                                        </td>
                                        <td class="form-item" nowrap>
                                            ���ʵȼ���</td>
                                        <td nowrap runat="server" id="tdQualityGrade">
                                            <select id="selQualityGrade" name="selQualityGrade" runat="server">
                                                <option value="" selected>--------��ѡ��--------</option>
                                            </select>
                                            &nbsp;��
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            ���ʣ�</td>
                                        <td nowrap colspan="5">
                                            <asp:TextBox ID="txtQuality" runat="server" Width="650px" CssClass="input" Height="60px"
                                                Rows="2" TextMode="MultiLine"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            ҵ����</td>
                                        <td nowrap colspan="5">
                                            <asp:TextBox ID="txtAchievement" runat="server" Width="650px" CssClass="input" Height="60px"
                                                Rows="2" TextMode="MultiLine"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            ��Ʒ����</td>
                                        <td colspan="5">
                                            <asp:TextBox ID="txtProduct" runat="server" Width="650px" CssClass="input" Height="60px"
                                                Rows="2" TextMode="MultiLine"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="form-item">
                                            ���������</td>
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
                                <input id="btnSave" name="btnSave" type="button" class="submit" value="ȷ ��" runat="server"
                                    onserverclick="btnSave_ServerClick">&nbsp;
                                <input id="btnCancel" name="btnCancel" type="button" class="submit" value="ȡ ��" onclick="javascript:self.close()">
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
		OpenMiddleWindow('../Supplier/SelSupplierType.aspx?SupplierTypeCode='+ code  ,'ѡ��������');
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
