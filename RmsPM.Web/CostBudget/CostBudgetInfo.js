var headCount = 2;

var LastSelectedRow;
var LastSelectedRowClass;

//����
function GoBack()
{
	if (document.all.txtFromUrl.value == "")
	{
		window.history.go(-1);
//		window.location.href = "PaymentList.aspx?ProjectCode=" + document.all.txtProjectCode.value;
	}
	else
	{
		window.history.go(-1);
//		window.location.href = document.all.txtFromUrl.value;
	}
}

//ˢ��
function Refresh()
{
	if (!BeforePost()) return false;
	document.all.btnRefresh.click();
}

//ˢ��Ŀ�����
function RefreshTarget()
{
	if (!BeforePost()) return false;
	document.all.btnRefreshTarget.click();
}

//ˢ��Ԥ�����
function RefreshBalance()
{
	if (!BeforePost()) return false;
	document.all.btnRefreshBalance.click();
}

//ˢ�º�ͬ�ƻ�
function RefreshPurchase()
{
	if (!BeforePost()) return false;
	document.all.btnRefreshPurchase.click();
}

//ˢ�º�ͬԤ��
function RefreshCostBudgetContract()
{
	if (!BeforePost()) return false;
	document.all.btnRefreshCostBudgetContract.click();
}

//��ȡ��������
function PickupCostCode(key)
{
	var CostCode = key;
	
	if (key == "R_0") CostCode = "";
	
	return CostCode;
}

//��ȡ��Ͷ����
function PickupBiddingCode(key)
{
    //key��Bidding_100001#100002
	var BiddingCode = key.substr("Bidding_".length);
	
	var arr = BiddingCode.split("#");
	if (arr.length > 0)
	{
    	BiddingCode = arr[0];
    }
    
    return BiddingCode;
}

//���е�idȡ���еķ�������
function GetCostCodeById(id)
{
    var CostCode = "";
    
	if ((id.indexOf("TreeNode_C_") >= 0) || (id.indexOf("TreeNode_B_") >= 0)) //��ͬ��ϸ�л�Ԥ�������
	{
        var AllCode = id.substr("TreeNode_C_".length, id.length - "TreeNode_C_".length);
        
        if (AllCode.indexOf(":") >= 0)
            CostCode = AllCode.substr(0, AllCode.indexOf(":"));
        else
            CostCode = AllCode;
    }
    else if (id.indexOf("TreeNode_") >= 0) //��������
    {
        CostCode = id.substr("TreeNode_".length);
    }
    
    return CostCode;
}

//�޸Ķ�̬����
function ModifyDynamic()
{
	var CostBudgetCode = document.all("txtCostBudgetCode").value;
	var CostBudgetSetCode = document.all("txtCostBudgetSetCode").value;
	window.location.href = "CostBudgetModify.aspx?CostBudgetCode=" + CostBudgetCode + "&CostBudgetSetCode=" + CostBudgetSetCode;
//	OpenFullWindow("CostTargetModify.aspx?CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value, "Ԥ���̬Ԥ���޸�");
}
	
//����
function ModifyEx()
{
	window.location.href = "CostBudgetModify.aspx?OldCostBudgetCode=" + document.all.txtCostBudgetCode.value + "&CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value;
}
	
//���
function DoCheck()
{
	if (!confirm("ȷʵ���ͨ����")) return false;
	
	document.all.divHintSave.style.display = '';
	return true;
	
//	OpenCustomWindow("CostTargetCheck.aspx?PaymentCode=' + paymentCode,"Ԥ���̬Ԥ�����", 600, 400);
}

//�浵
function Backup()
{
	OpenCustomWindow("CostBudgetBackup.aspx?ProjectCode=" + document.all.txtProjectCode.value, "��Ŀ���ñ���", 420, 250);
}

//�鿴�浵
function LoadBackup()
{
	OpenCustomWindow("CostBudgetBackupList.aspx?CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value + "&ProjectCode=" + document.all.txtProjectCode.value, "��Ŀ���ñ����б�", 780, 560);
}

//�鿴�浵(Ԥ�����)
function LoadBackupGroup()
{
	OpenCustomWindow("CostBudgetBackupList.aspx?GroupCode=" + document.all.txtGroupCode.value + "&ProjectCode=" + document.all.txtProjectCode.value, "��Ŀ���ñ����б�", 780, 560);
}

//�л����浵
function sltBackupChange(sender)
{
	var CostBudgetBackupCode = sender.value;
	
	if (CostBudgetBackupCode == "more") //�����浵�б�
	{
		OpenCustomWindow("CostBudgetBackupList.aspx?CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value + "&ProjectCode=" + document.all.txtProjectCode.value, "��Ŀ���ñ����б�", 780, 560);
	}
	else //�鿴�浵��ǰ
	{
		window.location.href = "CostBudgetInfo.aspx?CostBudgetBackupCode=" + CostBudgetBackupCode + "&CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value;
	}
}

//�л����浵(Ԥ�����)
function sltBackupGroupChange(sender)
{
	var CostBudgetBackupCode = sender.value;
	
	if (CostBudgetBackupCode == "more") //�����浵�б�
	{
		OpenCustomWindow("CostBudgetBackupList.aspx?GroupCode=" + document.all.txtGroupCode.value + "&ProjectCode=" + document.all.txtProjectCode.value, "��Ŀ���ñ����б�", 780, 560);
	}
	else //�鿴�浵��ǰ
	{
		window.location.href = "CostBudgetGroupInfo.aspx?ProjectCode=" + document.all.txtProjectCode.value + "&CostBudgetBackupCode=" + CostBudgetBackupCode + "&GroupCode=" + document.all.txtGroupCode.value;
	}
}

//������Excel
function Excel()
{
	OpenCustomWindow("CostBudgetInfo.aspx?CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value + "&CostBudgetBackupCode=" + document.all.txtCostBudgetBackupCode.value +"&act=Excel", "CostBudgetInfoExcel", 400, 250);
}

//�鿴��ʷ
function ViewHistory()
{
	OpenCustomWindow("CostBudgetHistoryList.aspx?TargetFlag=0&CostBudgetCode=" + document.all.txtCostBudgetCode.value, "��̬Ԥ����ʷ", 780, 560);
}

//Ԥ�������
function ModifySet()
{
	OpenCustomWindow("CostBudgetSetModify.aspx?CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value, "Ԥ�������", 500, 350);
}

//��������Ϣ
function ViewCostCode(code)
{
	OpenFullWindow('../Cost/DynamicCostInfo.aspx?ProjectCode=' + document.all.txtProjectCode.value + '&CostCode=' + code ,'��̬��������Ϣ');
}

//��ʾ��ʷĿ�����
function ShowTargetMoneyHis(VerID)
{
	document.all.txtShowTargetMoneyHisVerID.value = VerID;
	document.all.btnShowTargetMoneyHis.click();
}

//������ʷĿ�����
function HideTargetMoneyHis()
{
	document.all.btnHideTargetMoneyHis.click();
}

//��ʾ��ʷԤ����
function ShowBudgetMoneyHis(VerID)
{
	document.all.btnShowBudgetMoneyHis.click();
}

//������ʷԤ����
function HideBudgetMoneyHis()
{
	document.all.btnHideBudgetMoneyHis.click();
}

//��ͬ��Ϣ	
function ViewContractInfo(ContractCode)
{
	if (ContractCode.substr(0, "Bidding_".length) == "Bidding_") //��Ͷ��
	{
		var BiddingCode = PickupBiddingCode(ContractCode);
		OpenFullWindow("../BiddingManage/BiddingModify.aspx?state=edit&ProjectCode=" + document.all.txtProjectCode.value + "&ApplicationCode=" + BiddingCode + "&FunctionName=RefreshPurchase", "��ͬ�ƻ�");
	}
	else if (ContractCode.substr(0, "Payment_".length) == "Payment_")
	{
		var PaymentCode = ContractCode.substr("Payment_".length);
		OpenFullWindow("../Finance/PaymentInfo.aspx?PaymentCode=" + PaymentCode, "����Ϣ");
	}
	else
	{
		OpenFullWindow("../Contract/ContractInfo.aspx?ProjectCode=" + document.all.txtProjectCode.value + "&ContractCode=" + ContractCode,'��ͬ��Ϣ');
	}
}

//������Ϣ
function ViewSupplierInfo(code)
{
	OpenFullWindow("../Supplier/SupplierInfo.aspx?SupplierCode=" + code, "������Ϣ");
}

//�鿴������ϸ
function ViewContractPay(CostCode, ContractCode, IsContract)
{
	CostCode = PickupCostCode(CostCode);
	
	if (ContractCode.substr(0, "Bidding_".length) == "Bidding_")
	{
	}
	else if (ContractCode.substr(0, "Bidding_".length) == "Payment_")  //�Ǻ�ͬ���
	{
		var PaymentCode = ContractCode.substr("Payment_".length);
		OpenFullWindow("../Finance/ShowPaymentItemList.aspx?ProjectCode=" + document.all.txtProjectCode.value + "&CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value + "&CostCode=" + CostCode + "&PaymentCode=" + PaymentCode + "&IsContract=" + IsContract, 'ShowPaymentItemList');
	}
	else
	{
		OpenFullWindow("../Finance/ShowPaymentItemList.aspx?ProjectCode=" + document.all.txtProjectCode.value + "&CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value + "&CostCode=" + CostCode + "&ContractCode=" + ContractCode + "&IsContract=" + IsContract, 'ShowPaymentItemList');
	}
}

//�鿴������ϸ
function ViewContractPBSPay(CostCode, ContractCode, IsContract, PBSType, PBSCode)
{
	CostCode = PickupCostCode(CostCode);
	
	if (ContractCode.substr(0, "Bidding_".length) == "Bidding_")
	{
	}
	else if (ContractCode.substr(0, "Bidding_".length) == "Payment_")  //�Ǻ�ͬ���
	{
		var PaymentCode = ContractCode.substr("Payment_".length);
		OpenFullWindow("../Finance/ShowPaymentItemList.aspx?ProjectCode=" + document.all.txtProjectCode.value + "&CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value + "&CostCode=" + CostCode + "&PaymentCode=" + PaymentCode + "&IsContract=" + IsContract + "&PBSType=" + PBSType + "&PBSCode=" + PBSCode, 'ShowPaymentItemList');
	}
	else
	{
		OpenFullWindow("../Finance/ShowPaymentItemList.aspx?ProjectCode=" + document.all.txtProjectCode.value + "&CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value + "&CostCode=" + CostCode + "&ContractCode=" + ContractCode + "&IsContract=" + IsContract + "&PBSType=" + PBSType + "&PBSCode=" + PBSCode, 'ShowPaymentItemList');
	}
}

//�鿴����δ����ϸ
function ViewContractPayRealBalance(CostCode, ContractCode, IsContract)
{
	CostCode = PickupCostCode(CostCode);
	
	if (ContractCode.substr(0, "Bidding_".length) == "Bidding_")
	{
	}
	else if (ContractCode.substr(0, "Bidding_".length) == "Payment_")  //�Ǻ�ͬ���
	{
		var PaymentCode = ContractCode.substr("Payment_".length);
		OpenFullWindow("../Finance/ShowPaymentItemList.aspx?IsPayout=0,1&ProjectCode=" + document.all.txtProjectCode.value + "&CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value + "&CostCode=" + CostCode + "&PaymentCode=" + PaymentCode + "&IsContract=" + IsContract, 'ShowPaymentItemList');
	}
	else
	{
		OpenFullWindow("../Finance/ShowPaymentItemList.aspx?IsPayout=0,1&ProjectCode=" + document.all.txtProjectCode.value + "&CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value + "&CostCode=" + CostCode + "&ContractCode=" + ContractCode + "&IsContract=" + IsContract, 'ShowPaymentItemList');
	}
}

//�鿴����δ����ϸ
function ViewContractPBSPayRealBalance(CostCode, ContractCode, IsContract, PBSType, PBSCode)
{
	CostCode = PickupCostCode(CostCode);
	
	if (ContractCode.substr(0, "Bidding_".length) == "Bidding_")
	{
	}
	else if (ContractCode.substr(0, "Bidding_".length) == "Payment_")  //�Ǻ�ͬ���
	{
		var PaymentCode = ContractCode.substr("Payment_".length);
		OpenFullWindow("../Finance/ShowPaymentItemList.aspx?IsPayout=0,1&ProjectCode=" + document.all.txtProjectCode.value + "&CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value + "&CostCode=" + CostCode + "&PaymentCode=" + PaymentCode + "&IsContract=" + IsContract + "&PBSType=" + PBSType + "&PBSCode=" + PBSCode, 'ShowPaymentItemList');
	}
	else
	{
		OpenFullWindow("../Finance/ShowPaymentItemList.aspx?IsPayout=0,1&ProjectCode=" + document.all.txtProjectCode.value + "&CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value + "&CostCode=" + CostCode + "&ContractCode=" + ContractCode + "&IsContract=" + IsContract + "&PBSType=" + PBSType + "&PBSCode=" + PBSCode, 'ShowPaymentItemList');
	}
}

//�鿴�Ѹ���ϸ
function ViewContractPayReal(CostCode, ContractCode, IsContract, PayoutDateBegin, PayoutDateEnd)
{
	CostCode = PickupCostCode(CostCode);
	
	if (ContractCode.substr(0, "Bidding_".length) == "Bidding_")
	{
	}
	else if (ContractCode.substr(0, "Bidding_".length) == "Payment_")  //�Ǻ�ͬ���
	{
		var PaymentCode = ContractCode.substr("Payment_".length);
		OpenFullWindow("../Finance/ShowPayoutItemList.aspx?ProjectCode=" + document.all.txtProjectCode.value + "&CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value + "&CostCode=" + CostCode + "&PaymentCode=" + PaymentCode + "&IsContract=" + IsContract + "&PayoutDateBegin=" + PayoutDateBegin + "&PayoutDateEnd=" + PayoutDateEnd, 'ShowPayoutItemList');
	}
	else  
	{
		OpenFullWindow("../Finance/ShowPayoutItemList.aspx?ProjectCode=" + document.all.txtProjectCode.value + "&CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value + "&CostCode=" + CostCode + "&ContractCode=" + ContractCode + "&IsContract=" + IsContract + "&PayoutDateBegin=" + PayoutDateBegin + "&PayoutDateEnd=" + PayoutDateEnd, 'ShowPayoutItemList');
	}
}

//�鿴�Ѹ���ϸ
function ViewContractPBSPayReal(CostCode, ContractCode, IsContract, PayoutDateBegin, PayoutDateEnd, PBSType, PBSCode)
{
	CostCode = PickupCostCode(CostCode);
	
	if (ContractCode.substr(0, "Bidding_".length) == "Bidding_")
	{
	}
	else if (ContractCode.substr(0, "Bidding_".length) == "Payment_")  //�Ǻ�ͬ���
	{
		var PaymentCode = ContractCode.substr("Payment_".length);
		OpenFullWindow("../Finance/ShowPayoutItemList.aspx?ProjectCode=" + document.all.txtProjectCode.value + "&CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value + "&CostCode=" + CostCode + "&PaymentCode=" + PaymentCode + "&IsContract=" + IsContract + "&PayoutDateBegin=" + PayoutDateBegin + "&PayoutDateEnd=" + PayoutDateEnd + "&PBSType=" + PBSType + "&PBSCode=" + PBSCode, 'ShowPayoutItemList');
	}
	else  
	{
		OpenFullWindow("../Finance/ShowPayoutItemList.aspx?ProjectCode=" + document.all.txtProjectCode.value + "&CostBudgetSetCode=" + document.all.txtCostBudgetSetCode.value + "&CostCode=" + CostCode + "&ContractCode=" + ContractCode + "&IsContract=" + IsContract + "&PayoutDateBegin=" + PayoutDateBegin + "&PayoutDateEnd=" + PayoutDateEnd + "&PBSType=" + PBSType + "&PBSCode=" + PBSCode, 'ShowPayoutItemList');
	}
}

//�鿴¥��
function ViewBuilding(BuildingCode)
{
	OpenCustomWindow("../PBS/PBSBuildInfo.aspx?BuildingCode=" + BuildingCode + "&OpenModal=open&action=view", "BuildingInfo", 700, 540);
}

//��ӡ
function Print()
{
	OpenPrintWindow("../Report/PrintList.aspx?FromControlID=tdMaster$tdList1$tbl-container&css=" + escape("../CostBudget/CostBudget.css"), "��ӡ");
}

//�޸ĵ���Ŀ�����
function ModifyTargetMoney(CostCode)
{
	var ProjectCode = document.all("txtProjectCode").value;
	var CostBudgetSetCode = document.all("txtCostBudgetSetCode").value;
	OpenCustomWindow("CostTargetModifyItem.aspx?ProjectCode=" + ProjectCode + "&CostBudgetSetCode=" + CostBudgetSetCode + "&CostCode=" + CostCode, "�޸ĵ���Ŀ�����", 400, 200);
}

//�޸Ķ�̬���õ�Ԥ�����
function ModifyBalance(CostCode, ContractMoney)
{
	var ProjectCode = document.all("txtProjectCode").value;
	var CostBudgetSetCode = document.all("txtCostBudgetSetCode").value;
	OpenCustomWindow("DynamicBalanceModify.aspx?ProjectCode=" + ProjectCode + "&CostBudgetSetCode=" + CostBudgetSetCode + "&CostCode=" + CostCode + "&ContractMoney=" + ContractMoney, "�޸Ķ�̬���õ�Ԥ�����", 400, 200);
}

//������ͬ�ƻ�
function AddPurchase(CostCode)
{ 
	var ProjectCode = document.all("txtProjectCode").value;
	var CostBudgetSetCode = document.all("txtCostBudgetSetCode").value;
	var PBSType = document.all("txtPBSType").value;
	var PBSCode = document.all("txtPBSCode").value;
//	alert("���ڿ���");
//	return;

	OpenFullWindow("../BiddingManage/BiddingModify.aspx?state=edit&ProjectCode=" + ProjectCode + "&CostBudgetSetCode=" + CostBudgetSetCode + "&PBSType=" + PBSType + "&PBSCode=" + PBSCode + "&CostCode=" + CostCode + "&FunctionName=RefreshPurchase", "��ͬ�ƻ�");
	
//	OpenCustomWindow("CostBudgetPurchaseModify.aspx?ProjectCode=" + ProjectCode + "&CostBudgetSetCode=" + CostBudgetSetCode + "&CostCode=" + CostCode, "������ͬ�ƻ�", 760, 580);
}

//�޸ĺ�ͬ���б�ƻ�Ԥ��
function ModifyContractBudget(CostCode, ContractCode)
{
	var ProjectCode = document.all("txtProjectCode").value;
	var CostBudgetSetCode = document.all("txtCostBudgetSetCode").value;

	if (ContractCode.substr(0, "Bidding_".length) == "Bidding_") //�б�ƻ�
	{
		var BiddingCode = PickupBiddingCode(ContractCode);
		OpenCustomWindow("CostBudgetContractModify.aspx?ProjectCode=" + ProjectCode + "&CostBudgetSetCode=" + CostBudgetSetCode + "&CostCode=" + CostCode + "&RelationType=Bidding&ContractCode=" + BiddingCode, "�޸ĺ�ͬԤ��", 400, 200);
	}
	else //��ͬ
	{
		OpenCustomWindow("CostBudgetContractModify.aspx?ProjectCode=" + ProjectCode + "&CostBudgetSetCode=" + CostBudgetSetCode + "&CostCode=" + CostCode + "&RelationType=Contract&ContractCode=" + ContractCode, "�޸ĺ�ͬԤ��", 400, 200);
	}
}

function RowMouseClick(sender)
{
	if (event.button==2)
	{
		ShowEditMenu(sender);
		CBTree_SetRowSelected(sender);
	}
}

//�����˵�
function ShowEditMenu(sender)
{
	var cssFile="../Images/ContentMenu.css";

	var rowCBS, key;
	
	//�鿴����ʱ�޵����˵�
	if ((document.all("txtCostBudgetBackupCode")) && (document.all("txtCostBudgetBackupCode").value != ""))
	{
		alert("ֻ���ڼ�ʱ״̬�²��ܲ�����Ŀ���ã������л�����ʱ״̬");
		return;
	}
	
	if ((sender.id.indexOf("TreeNode_C_") >= 0) || (sender.id.indexOf("TreeNode_B_") >= 0)) //��ͬ��ϸ�л�Ԥ�������
	{
		rowCBS = document.all("TreeNode_" + GetCostCodeById(sender.id));
		key = CBTree_GetTreeNodeKey(rowCBS);
	}
	else //��������
	{
		rowCBS = sender;
		key = CBTree_GetTreeNodeKey(rowCBS);
		
		//�ϼ���
		if (key.substr(0, 2) == "R_") return;
	}

	var Items = new Array();

	var i = -1;
	
	//�ӽڵ���
	var ChildCount = 0;
	if (rowCBS.ChildCount)
	{
		ChildCount = parseInt(rowCBS.ChildCount);
	}
	
	i++;
	Items[i] = new Array(3);
	Items[i][0] = "�޸�Ŀ�����";
	Items[i][1] = "";
	Items[i][2] = "ModifyTargetMoney('" + key + "');";

	if (ChildCount == 0) //Ҷ�ڵ�ķ�����
	{
	    var BalanceContractMoney = 0;
	    if (rowCBS.BalanceContractMoney) BalanceContractMoney = rowCBS.BalanceContractMoney;
	    
		i++;
		Items[i] = new Array(3);
		Items[i][0] = "�޸�Ԥ�����";
		Items[i][1] = "";
		Items[i][2] = "ModifyBalance('" + key + "', " + BalanceContractMoney + ");";

		i++;
		Items[i] = new Array(3);
		Items[i][0] = "�����б�ƻ�";
		Items[i][1] = "";
		Items[i][2] = "AddPurchase('" + key + "');";
	}

	if (sender.id.indexOf("TreeNode_C_") >= 0) //��ͬ���б�ƻ�
	{
//		var pos = sender.id.indexOf("TreeNode_C_") + "TreeNode_C_".length;
		var pos = sender.id.indexOf(":") + ":".length;
		var ContractCode = sender.id.substr(pos, sender.id.length - pos);
		
		i++;
		Items[i] = new Array(3);
		Items[i][0] = "�޸ĺ�ͬԤ��";
		Items[i][1] = "";
		Items[i][2] = "ModifyContractBudget('" + key + "','" + ContractCode + "');";
	}
	
	CreateContentMenu(Items,cssFile,event.x-1,event.y-1);
}

/*
//����Ŀ����ñ����Ƿ��е����˵�
function InitTargetHeadMenuLink()
{
	var objHref = document.all("hrefTargetVerID");
	var objSpan = document.all("spanTargetVerID");
	if ((objHref) && (objSpan))
	{
		if (document.all.txtHasTargetHis.value == "1")  //����ʷĿ�����
		{
			objHref.innerText = objSpan.innerText;
			objSpan.innerText = "";
		}
	}
}
*/

//Ŀ����ñ��ⵯ���˵�
function ShowTargetHeadMenu(sender)
{
	var cssFile="../Images/ContentMenu.css";

	var Items = new Array();

	var i = -1;
	
	if (document.all.txtShowTargetHis.value == "1")
	{
		i++;
		Items[i] = new Array(3);
		Items[i][0] = "������ʷԤ��";
		Items[i][1] = "";
		Items[i][2] = "HideTargetMoneyHis();";
	}
	else
	{
		if (document.all.txtHasTargetHis.value == "1")
		{
			i++;
			Items[i] = new Array(3);
			Items[i][0] = "��ʾ������ʷԤ��";
			Items[i][1] = "";
			Items[i][2] = "ShowTargetMoneyHis('');";
		}
	}

	if (i >= 0)
	{
		var offsetTop = 0;
		var offsetLeft = 0;
		
//		if (document.all(sender.id)[0])
//		{
			offsetTop = document.all("tbl-container").offsetTop + 2;
			offsetLeft = document.all("tbl-container").offsetLeft + 2 + document.all("tdTargetHead").offsetLeft;
//		}
		CreateContentMenu(Items,cssFile, event.x + offsetLeft, event.y + offsetTop);
	}
}

//�б�����¼�
function ListKeyDown(sender)
{
//alert(event.keyCode);

	if (event.keyCode == "38") //�ϼ�ͷ
	{
		var ActiveRow = GetActiveRow();
		var NextRow = ActiveRow.previousSibling;
		while ((NextRow) && (NextRow.style.display == "none"))
		{
			NextRow = NextRow.previousSibling;
		}

		if (NextRow)
		{			
			NextRow.click();
		}

		return false;
	}
	else if (event.keyCode == "40") //�¼�ͷ
	{
		var ActiveRow = GetActiveRow();
		var NextRow = ActiveRow.nextSibling;
		while ((NextRow) && (NextRow.style.display == "none"))
		{
			NextRow = NextRow.nextSibling;
		}

		if (NextRow)
		{			
			NextRow.click();
		}

		return false;
	}
	else if ((event.keyCode == "187") //+
		|| (event.keyCode == "13")) //Enter
	{
		//չ�����
		var ActiveRow = GetActiveRow();
		
		if ((ActiveRow) && (ActiveRow.expand == "0"))
		{
			CBTree_ImgExpandClick(ActiveRow);
		}

		return false;
	}
	else if ((event.keyCode == "189") //-
		|| (event.keyCode == "8")) //�˸�
	{
		//�۵����
		var ActiveRow = GetActiveRow();
		
		if ((ActiveRow) && (ActiveRow.expand == "1"))
		{
			CBTree_ImgExpandClick(ActiveRow);
		}

		return false;
	}

	return true;
}

/*
//�м����¼�
function RowKeyDown(sender)
{
	var ActiveRow = GetActiveRow();
	var NextRow;
	
	if (event.keyCode == "40") //�¼�ͷ
	{
		NextRow = ActiveRow.nextSibling;
		if (NextRow)
		{
			NextRow.click();
		}
	}
	
//alert(event.keyCode);
}
*/

//�е���
function RowClick(sender)
{
	SetRowSelected(sender);
}

//XmlTree���е���
function XmlTreeRowClick()
{
	if (LastSelectedRow)
	{
        for (var i=0;i<LastSelectedRow.childNodes.length;i++)
        {
        	LastSelectedRow.childNodes[i].className = LastSelectedRowClass;
        }
		
		LastSelectedRowClass = "";
		LastSelectedRow = "";
	}

	LastSelectedRow = this;
	LastSelectedRowClass = LastSelectedRow.childNodes[0].className;

    for (var i=0;i<this.childNodes.length;i++)
    {
    	this.childNodes[i].className = "tree-tr-highlight";
    }
}

//������ѡ��
function SetRowSelected(sender)
{
	ClearRowSelected();
	
	LastSelectedRow = sender;
	LastSelectedRowClass = LastSelectedRow.className;
	LastSelectedRow.className = "list-highlight";
}

//ȡ��ǰѡ�е�һ��
function GetActiveRow()
{
	return LastSelectedRow;
}

//�����ѡ��
function ClearRowSelected()
{
	if (LastSelectedRow)
	{
		LastSelectedRow.className = LastSelectedRowClass;
		
		LastSelectedRowClass = "";
		LastSelectedRow = "";
	}
}

//��ʼ����ѡ��
function InitRowSelected()
{
	var LastSelectedRowID = document.all("txtLastSelectedRowID").value;
	if (LastSelectedRowID != "")
	{
		var obj = document.all(LastSelectedRowID);
		SetRowSelected(obj);
	}
}

//��ʼ���б�״̬
function InitListStatus()
{
	//�б����λ��
	if (document.all.txtListScrollTop.value != "")
	{
		var scrollTop = parseInt(document.all.txtListScrollTop.value);
		var scrollLeft = parseInt(document.all.txtListScrollLeft.value);
		
		if (!isNaN(scrollTop)) document.all("tbl-container").scrollTop = scrollTop;
		if (!isNaN(scrollLeft)) document.all("tbl-container").scrollLeft = scrollLeft;
	}
	
	//��ѡ��
	InitRowSelected();
}

//ҳ���ύǰ
function BeforePost()
{
	document.all.txtListScrollTop.value = document.all("tbl-container").scrollTop;
	document.all.txtListScrollLeft.value = document.all("tbl-container").scrollLeft;
	
	document.all.txtLastSelectedRowID.value = "";
	document.all.txtLastSelectedRowClass.value = "";
	if (LastSelectedRow)
	{
		document.all.txtLastSelectedRowID.value = LastSelectedRow.id;
		document.all.txtLastSelectedRowClass.value = LastSelectedRowClass;
	}

	//��¼��չ��״̬
	if (document.all.txtExpandNode) document.all.txtExpandNode.value = m_arrExpandNode.join(",");
		
	return true;

//alert(document.all("tbl-container").scrollTop);
//return;
}

function winload()
{
	if (document.all("tbl-container"))	document.all("tbl-container").oncontextmenu=Function("return false;");

//var d1 = new Date();

	CBTree_InitTree("Tree", "../images/plus.gif", "../images/minus.gif", headCount);

	CBTree_ExpandTreeByNodeDefaultExpand();
	ClearRowSelected();

	InitListStatus();
	
//	LockColumn("Tree", 1);
	
//	InitTargetHeadMenuLink();
	
//	MyLockTable();
	
//var d2 = new Date();
//alert(d1.getMinutes() + ":" + d1.getSeconds() + "." + d1.getMilliseconds() + " - " + d2.getMinutes() + ":" + d2.getSeconds() + "." + d2.getMilliseconds());	
}

/*
function MyLockTable()
{
	LockTable(Tree,0,2,0);
	if (document.all("DivHeadTar"))	document.all("DivHeadTar").oncontextmenu=Function("return false;");
}
*/

