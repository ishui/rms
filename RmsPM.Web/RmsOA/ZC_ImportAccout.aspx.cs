using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.IO;
using System.Text;
using RmsOA.BFL;

public partial class RmsOA_ZC_ImportAccout : System.Web.UI.Page
{
    #region Field
    private const string fileContent = "fileContent";
    private string extentionName = ".csv";
    private static List<AssetViewModel> listAsset;
    #endregion

    #region "Property"

    private FileUpload FileUpControl
    {
        get { return this.importFile; }
    }
    /// <summary>
    /// ҳ�������ʾ���ݣ�
    /// ����Ϊ��ɫ��������ɫΪ��ɫ
    /// </summary>
    public string Title
    {
        get { return this.lblTitle.Text; }
        set { this.lblTitle.Text = value; }
    }
    /// <summary>
    /// ҳ��ͷ����ʾ����
    /// ������ɫΪ��ɫ
    /// </summary>
    public string Head
    {
        get { return this.lblHead.Text; }
        set { this.lblHead.Text = value; }
    }
    /// <summary>
    /// �����csv�ļ��ĸ�ʽ
    /// </summary>
    public string FileFormat
    {
        get { return this.lblFieldDesc.Text; }
        set { this.lblFieldDesc.Text = value; }
    }
    public GridView SuccessGridView
    {
        get { return this.successGridView; }
        set { this.successGridView = value; }
    }

    public GridView FailureGridView
    {
        get { return this.failureGridView; }
        set { this.failureGridView = value; }
    }

    /// <summary>
    /// �������ݵ�������
    /// </summary>
    public string TotalDataCount
    {
        get { return lblTotalMessage.Text; }
        set { lblTotalMessage.Text = value; }
    }
    /// <summary>
    /// ��Ч���ݵ�����
    /// </summary>
    public string UserfulDataCount
    {
        get { return lblrightMessage.Text; }
        set { lblrightMessage.Text = value; }
    }
    /// <summary>
    /// �������ݵ�����
    /// </summary>
    public string WrongDataCount
    {
        get { return lblWrongMessage.Text; }
        set { lblWrongMessage.Text = value; }
    }

    /// <summary>
    /// ����ϴ��ؼ��Ƿ����ļ�
    /// </summary>
    public bool HasFile
    {
        get
        {
            bool temp = false;
            if (this.FileUpControl.HasFile)
            {
                temp = true;
            }
            return temp;
        }
    }

    /// <summary>
    /// ����ϴ��ؼ����ļ���׺���Ƿ����Լ���Ҫ���ļ���ͬ
    /// </summary>
    public bool IsWantedFile
    {
        get
        {
            bool temp = false;
            if (Path.GetExtension(UpLoadFileName).Equals(ExtentionName))
            {
                temp = true;
            }
            return temp;
        }
    }

    public string UpLoadFileName
    {
        get { return this.FileUpControl.FileName; }
    }

    /// <summary>
    /// �ļ��ĺ�׺����ʽ
    /// </summary>
    public string ExtentionName
    {
        get { return this.extentionName; }
        set { this.extentionName = value; }
    }

    public string OtherMessage
    {
        get { return this.lblOtherMessage.Text; }
        set { this.lblOtherMessage.Text = value; }
    }

    /// <summary>
    /// ��ȡ�ϴ��ļ�������
    /// </summary>
    public List<string> GetUpFileContent
    {
        get
        {
            List<string> listContent = new List<string>();
            HttpPostedFile postedFile = FileUpControl.PostedFile;
            StreamReader reader = new StreamReader(postedFile.InputStream, Encoding.Default);
            while (reader.Peek() > 0)
            {
                listContent.Add(reader.ReadLine());
            }
            return listContent;
        }
    }
    #endregion

    #region "Method"
    /// <summary>
    /// ��ʾ̽����Ϣ
    /// </summary>
    /// <param name="message"></param>
    public void ShowMessage(string message)
    {
        Response.Write(string.Format("<script>window.alert('{0}');</script>", message));
    }

    public void BindDataToGridView()
    {
        ZC_AssertExtend assert = new ZC_AssertExtend();
        try
        {
            assert.SortData(this.GetUpFileContent);
            listAsset = assert.ListUserfulModel;
            this.successGridView.DataSource = assert.ListUserfulModel;
            this.successGridView.DataBind();
            this.failureGridView.DataSource = assert.ListWrongModel;
            this.failureGridView.DataBind();
            this.TotalDataCount = assert.TotalCount.ToString();
            this.UserfulDataCount = assert.UserfulCount.ToString();
            this.WrongDataCount = assert.WrongCount.ToString();
        }
        catch (Exception e)
        {
            this.ShowMessage(e.Message);
            this.multiView.ActiveViewIndex = 0;
        }
    }
    #endregion

    #region "Event"

    protected void Page_Load(object sender, EventArgs e)
    {

        this.Title = "�̶��ʲ�̨��";
        this.Head = "�۵���̶��ʲ�̨�ʣ�";
        this.FileFormat = "����,���,���,��������,����,��λ,���,ʹ�ò���,ʹ����,��ŵص�,������,��ע";
        this.OtherMessage = "4.��ʹ�ò��š�ʹ��->�ָ�磺���Ź�˾->���輯��->������,�ұ����ϵͳ�еĲ��Žṹ��Ӧ��";
    }

    /// <summary>
    /// ���밴ť�����¼�
    /// </summary>
    protected void Import_Click(object sender, EventArgs e)
    {
        if (HasFile)
        {
            if (IsWantedFile)
            {
                this.multiView.ActiveViewIndex = 1;
                BindDataToGridView();
            }
            else
            {
                ShowMessage(string.Format("Ҫ������ļ���ʽ��Ҫ����ļ���ʽ{0}����", ExtentionName));
            }
        }
        else
        {
            ShowMessage(string.Format("�Բ���,�����ѡ��һ��Ҫ������ļ���"));
        }

    }

    /// <summary>
    /// ���水ť�����¼�
    /// </summary>
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        GK_OA_CapitalAssertAcountBFL bfl = new GK_OA_CapitalAssertAcountBFL();
        foreach (AssetViewModel model in listAsset)
        {
            bfl = new GK_OA_CapitalAssertAcountBFL();
            bfl.Insert(model);
        }
        Response.Write("<script> window.opener.location.reload();window.close();</script>");
    }
    #endregion
}
