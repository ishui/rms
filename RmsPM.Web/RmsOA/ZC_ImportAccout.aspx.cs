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
    /// 页面标题显示内容！
    /// 背景为蓝色，字体颜色为白色
    /// </summary>
    public string Title
    {
        get { return this.lblTitle.Text; }
        set { this.lblTitle.Text = value; }
    }
    /// <summary>
    /// 页面头部显示内容
    /// 字体颜色为蓝色
    /// </summary>
    public string Head
    {
        get { return this.lblHead.Text; }
        set { this.lblHead.Text = value; }
    }
    /// <summary>
    /// 插入的csv文件的格式
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
    /// 导入数据的总条数
    /// </summary>
    public string TotalDataCount
    {
        get { return lblTotalMessage.Text; }
        set { lblTotalMessage.Text = value; }
    }
    /// <summary>
    /// 有效数据的条数
    /// </summary>
    public string UserfulDataCount
    {
        get { return lblrightMessage.Text; }
        set { lblrightMessage.Text = value; }
    }
    /// <summary>
    /// 错误数据的条数
    /// </summary>
    public string WrongDataCount
    {
        get { return lblWrongMessage.Text; }
        set { lblWrongMessage.Text = value; }
    }

    /// <summary>
    /// 检测上传控件是否有文件
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
    /// 检测上传控件的文件后缀名是否与自己想要的文件相同
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
    /// 文件的后缀名格式
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
    /// 获取上传文件的内容
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
    /// 显示探出信息
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

        this.Title = "固定资产台帐";
        this.Head = "［导入固定资产台帐］";
        this.FileFormat = "名称,类别,编号,购置日期,数量,单位,金额,使用部门,使用人,存放地点,变更情况,备注";
        this.OtherMessage = "4.“使用部门”使用->分割，如：集团公司->建设集团->开发部,且必须和系统中的部门结构对应。";
    }

    /// <summary>
    /// 导入按钮单击事件
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
                ShowMessage(string.Format("要导入的文件格式与要求的文件格式{0}不符", ExtentionName));
            }
        }
        else
        {
            ShowMessage(string.Format("对不起,你必须选择一个要导入的文件！"));
        }

    }

    /// <summary>
    /// 保存按钮单击事件
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
