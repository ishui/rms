namespace RmsPM.Web.BiddingManage
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Collections;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using BLL;
	using RmsPM.Web.WorkFlowControl;
    using Rms.ORMap;

	/// <summary>
	///		UCBiddingSupplierList ��ժҪ˵����
	/// </summary>
	public partial class UCBiddingSupplierList : BiddingControlBase
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
		}

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		�����֧������ķ��� - ��Ҫʹ�ô���༭��
		///		�޸Ĵ˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.dgList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgList_DeleteCommand);

		}
		#endregion


		#region --- ˽������ -----------------------------------------------------------------
        
		#endregion -----------------------------------------------------------------

		#region --- ˽�з��� -----------------------------------------------------------------

		/// <summary>
		/// DataGrid �¼�
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		public void dgList_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if ( "Delete"==e.CommandName )
				{
					string strCode = e.Item.Cells[0].Text.Trim();

					if ( ""==strCode )
					{
						return;
					}

					BLL.BiddingSupplier cbs = new RmsPM.BLL.BiddingSupplier();
					cbs.BiddingSupplierCode = strCode;
					cbs.BiddingSupplierDelete();

					this.LoadData();
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}
        

		#endregion -----------------------------------------------------------------

		#region --- �������� -----------------------------------------------------------------
        /// <summary>
        /// ��ȡ��Ӧ������
        /// </summary>
        public int SupplierCode
        {
            get
            {  
               return this.dgList.Items.Count;
            }       
        }

		/// <summary>
		/// 
		/// </summary>
		public string BiddingPrejudicationCode
		{
			get{
				if ( null!=this.ViewState["BiddingPrejudicationCode"] )
				{
					return this.ViewState["BiddingPrejudicationCode"].ToString();
				}
				return "";
			}
			set{this.ViewState["BiddingPrejudicationCode"]=value;}
		}
        /// <summary>
        /// �Ƿ�ȡ��һ�ڵ���ͨ��Ԥ��ĵ�λ
        /// </summary>
        public string Flag
        {
            get 
            {
                if (null != this.ViewState["flag"])
                {
                    return this.ViewState["flag"].ToString();
                }
                return "";
            }
            set
            {
                this.ViewState["flag"] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string BiddingForwardPrejudicationCode
        {
            get
            {
                if (null != this.ViewState["BiddingForwardPrejudicationCode"])
                {
                    return this.ViewState["BiddingForwardPrejudicationCode"].ToString();
                }
                return "";
            }
            set { this.ViewState["BiddingForwardPrejudicationCode"] = value; }
        }

		/// <summary>
		/// �Ƿ���ʾѡ����
		/// </summary>
		public bool CanSelect
		{
			get{
				if ( null!=this.ViewState["CanSelect"] )
				{
					return (bool)this.ViewState["CanSelect"];
				}
				return false;
			}
			set{this.ViewState["CanSelect"]=value;}
		}

		/// <summary>
		/// �Ƿ���Ա༭
		/// </summary>
		public bool CanModify
		{
			get{
				if ( null!=this.ViewState["CanModify"] )
				{
					return (bool)this.ViewState["CanModify"];
				}
				return false;
			}
			set{this.ViewState["CanModify"]=value;}
		}
		/// <summary>
		/// ��ȡ�����õ�ǰ������Ϣ
		/// </summary>
		public string DepartMent
		{
			get
			{
				return ViewState["ThisDepartment"].ToString();
			}
			set
			{
				ViewState["ThisDepartment"]=value;
			}
		}
		public BLL.Bidding_SupplierDepartmentIdea SupplierDepartment
		{
			get
			{
				BLL.Bidding_SupplierDepartmentIdea bidding = new Bidding_SupplierDepartmentIdea();
				bidding.BiddingPrejudicationCode = this.BiddingPrejudicationCode;
				return bidding;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		private ModuleState _State = ModuleState.Unbeknown;
		/// <summary>
		/// �Ƿ���ʾ���е�ѡ����Ϣ
		/// </summary>
		public ModuleState State2
		{
			get
			{
				if ( _State == ModuleState.Unbeknown )
				{
					if(this.ViewState["_DepartState"] != null)
						return (ModuleState)this.ViewState["_DepartState"];
					return ModuleState.Unbeknown;
				}
				return _State;
			}
			set
			{
				_State = value;
				this.ViewState["_DepartState"] = value;
			}
		}
	

		#endregion -----------------------------------------------------------------

		#region --- �������� -----------------------------------------------------------------

		/// <summary>
		/// �ؼ���ʼ��
		/// </summary>
		public void IniControl()
		{
			try
			{
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


		/// <summary>
		/// װ�ؿؼ�����
		/// </summary>
		public void LoadData()
		{
			try
			{
				//BLL.V_BiddingSupplier vbs = new RmsPM.BLL.V_BiddingSupplier();

				//if( null!=this.ViewState["BiddingPrejudicationCode"] )
				//	vbs.BiddingPrejudicationCode = this.ViewState["BiddingPrejudicationCode"].ToString();
				dgListState();
                DataTable dt ;
                if (Flag != "")
                {
                    this.MovePassByPrejudication();
                }
               
                dt = BLL.BiddingSystem.Get_AllMessage(this.BiddingPrejudicationCode);
                
				this.dgList.DataSource = dt;
				this.dgList.DataBind();
				//dt.Dispose();
				//Response.Write(Rms.Web.JavaScript.Alert(true,this.BiddingPrejudicationCode));
			}
			catch(Exception ex)
			{
				Response.Write(Rms.Web.JavaScript.Alert(true,ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


        /// <summary>
        /// ����һ�ε�Ͷ��Ԥ��ͨ���ĵ�λ�Ƶ���ǰ��λԤ����
        /// </summary>
        /// <returns></returns>
        public void MovePassByPrejudication()
        {
            try
            {
                BiddingSupplier cbiddingSupplier = new BiddingSupplier();
                cbiddingSupplier.BiddingPrejudicationCode = this.BiddingForwardPrejudicationCode;
                cbiddingSupplier.Flag = "1";
                EntityData entitydata = cbiddingSupplier._GetBiddingSuppliers();
               
                foreach (DataRow dr in entitydata.CurrentTable.Select())
                {
                  
                    DataRow newdr = entitydata.GetNewRecord();
                  
                    BLL.ConvertRule.DataRowCopy(dr, newdr, entitydata.CurrentTable, entitydata.CurrentTable);
                  
                    newdr["BiddingSupplierCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("BiddingSupplier");
                    newdr["BiddingPrejudicationCode"] = this.BiddingPrejudicationCode;
                    entitydata.CurrentTable.Rows.Add(newdr);
                    BLL.BiddingSystem.InsertDepartMent(newdr["BiddingPrejudicationCode"].ToString(), newdr["BiddingSupplierCode"].ToString());
                }

                if (dao != null)
                {
                    dao.SubmitEntity(entitydata);
                }
                else
                {
                    using (dao = new StandardEntityDAO("BiddingSupplier"))
                    {
                        dao.SubmitEntity(entitydata);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



		public void LoadEditData()
		{
			dgListState();
			//ShowDgListColumn(BiddingSystem.DepartMentName name);
			Bind_dgList(BiddingSystem.Get_All_BiddingSupplier(this.ViewState["BiddingPrejudicationCode"].ToString()));
		}
		public void dgListState()
		{
			this.dgList.Columns[2].Visible = this.CanSelect;
			this.dgList.Columns[3].Visible = !this.CanSelect;
			this.dgList.Columns[11].Visible = !this.CanModify;
			this.dgList.Columns[12].Visible = this.CanModify;
			this.dgList.Columns[34].Visible = this.CanModify;
		}
		/// <summary>
		/// �dgList
		/// </summary>
		/// <param name="dt"></param>
		private void Bind_dgList(DataTable dt)
		{
			this.dgList.DataSource = dt;
			this.dgList.DataBind();
			//dt.Dispose();
		}	
		public bool SelectedSupplierFlag
		{
			get
			{
                string SelectSupplierString="";
                for (int i = 0; i < dgList.Items.Count; i++)
                {
                    if (((HtmlInputCheckBox)dgList.Items[i].FindControl("chkSelect")).Checked)
                    {
                        SelectSupplierString += ((HtmlInputCheckBox)dgList.Items[i].FindControl("chkSelect")).Checked;
                    }
                }
				if(SelectSupplierString.Length>0)
					return true;
				else
					return false;
			}
		}

		/// <summary>
		/// ��������(״̬�޸�)
		/// </summary>
		public void SaveData()
		{
			try
			{
				string[] chkCodes = {};
				ArrayList chkAL = new ArrayList();
                //string strchkSelect = "";
                for (int i = 0; i < dgList.Items.Count; i++)
                {
                    if (((HtmlInputCheckBox)dgList.Items[i].FindControl("chkSelect")).Checked)
                    {
                        chkAL.Add(dgList.Items[i].Cells[0].Text.Trim());
                    }
                }

				BLL.BiddingSupplier cbs = new RmsPM.BLL.BiddingSupplier();
				cbs.BiddingPrejudicationCode = this.BiddingPrejudicationCode;
				cbs.dao = dao;
				DataTable myDT = cbs.GetBiddingSuppliers();
				int dtCount = myDT.Rows.Count;
				for(int r=0;r<dtCount;r++)
				{
					string strCode = myDT.Rows[r]["BiddingSupplierCode"].ToString();

					cbs.BiddingSupplierCode = strCode;
					if ( chkAL.Contains(strCode) )
					{
						cbs.Flag = "1";
					}
					else
					{
						cbs.Flag = "0";
					}

					cbs.BiddingSupplierUpdate();
				}
				myDT.Dispose();
				UpdateDepartMentSelect();

			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


		/// <summary>
		/// �༭����
		/// </summary>
		public void ModifyData()
		{
            try
            {

                BiddingSupplier cbiddingSupplier = new BiddingSupplier();
                cbiddingSupplier.dao = this.dao;
                cbiddingSupplier.BiddingPrejudicationCode = this.BiddingForwardPrejudicationCode;
                cbiddingSupplier.Flag = "1";
                EntityData entitydata = cbiddingSupplier._GetBiddingSuppliers();

                foreach (DataRow dr in entitydata.CurrentTable.Select())
                {

                    dr["flag"] = "0";
                }

                if (dao != null)
                {
                    dao.SubmitEntity(entitydata);
                }
                else
                {
                    using (dao = new StandardEntityDAO("BiddingSupplier"))
                    {
                        dao.SubmitEntity(entitydata);
                    }
                }


                int iCount = this.dgList.Items.Count;
                for (int i = 0; i < iCount; i++)
                {
                    string strCode = this.dgList.Items[i].Cells[0].Text.Trim();
                    string strValue = ((HtmlInputText)this.dgList.Items[i].Cells[11].FindControl("txtNominateUser")).Value.Trim();

                    //if( ""==strValue )
                    //{
                    //    Response.Write( Rms.Web.JavaScript.Alert(true,"����д�����ˣ�") );
                    //    return;
                    //}

                    BLL.BiddingSupplier cbs = new RmsPM.BLL.BiddingSupplier();
                    cbs.dao = dao;
                    cbs.BiddingSupplierCode = strCode;
                    cbs.NominateUser = strValue;
                    cbs.BiddingSupplierUpdate();
                }

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
		}
        /// <summary>
        ///ɾ��Ͷ�굥λ
        /// </summary>
        public void DeleteAll(StandardEntityDAO dao) 
        {
            try
            {
                
                    ArrayList saveIdChecked=new ArrayList();
                    BLL.BiddingSupplier cbs = new RmsPM.BLL.BiddingSupplier();
                    cbs.dao = dao;
                   //����ÿһ��
                   foreach(DataGridItem dataItem in this.dgList.Items)
                   {                            
                       string idChecked=dataItem.Cells[0].Text;
                       cbs.BiddingSupplierCode = idChecked;
                       cbs.BiddingSupplierDelete();
                             
                   }
                  
               
            }
            catch (Exception ex) 
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
        }

		#endregion -----------------------------------------------------------------


		#region ������ʾ����
		//public void ShowData()
		//{
		//	object[] ob = new object[20];
		//	foreach(object o in ob)
		//	{
				//Switch(o)
				//{
				//	case : ""
				//}
		//	}
		//}
		#endregion


		#region ѡ������ʾ�Ƿ�ѡ��ĳ��		
		public void ShowDgListColumn(BiddingSystem.DepartMentName name)
		{
			switch(name)
			{
				case BiddingSystem.DepartMentName.������:
					this.dgList.Columns[13].Visible=true;
					ViewState["ThisDepartment"]="Depart_Build";
					break;
				case BiddingSystem.DepartMentName.���̲�:
					this.dgList.Columns[14].Visible=true;
					for(int i=24;i<=24;i++)
					{
						this.dgList.Columns[i].Visible=true;
					}
					ViewState["ThisDepartment"]="Depart_Project";
					break;
				case BiddingSystem.DepartMentName.��Լ��:
					this.dgList.Columns[15].Visible=true;
					for(int i=24;i<=25;i++)
					{
						this.dgList.Columns[i].Visible=true;
					}
					ViewState["ThisDepartment"]="Depart_Agreement";
					break;
				case BiddingSystem.DepartMentName.��Ŀ�ܼ�:
					this.dgList.Columns[16].Visible=false;
					for(int i=24;i<=26;i++)
					{
						this.dgList.Columns[i].Visible=true;
					}
					ViewState["ThisDepartment"]="";
					break;
				case BiddingSystem.DepartMentName.�����ܼ�:
					this.dgList.Columns[17].Visible=true;
					ViewState["ThisDepartment"]="Md_Project";
					break;
				case BiddingSystem.DepartMentName.��Լ�ܼ�:
					this.dgList.Columns[18].Visible=true;
					ViewState["ThisDepartment"]="Md_Agreement";
					break;
				case BiddingSystem.DepartMentName.�����ܼ�:
					this.dgList.Columns[19].Visible=true;
					ViewState["ThisDepartment"]="DepartmentRemark1";
					break;
				case BiddingSystem.DepartMentName.����ִ��:
					this.dgList.Columns[20].Visible=true;
					ViewState["ThisDepartment"]="Director_Project";
					break;
				case BiddingSystem.DepartMentName.��Լִ��:
					this.dgList.Columns[21].Visible=true;
					ViewState["ThisDepartment"]="Director_Agreement";
					break;
				case BiddingSystem.DepartMentName.����ִ��:
					this.dgList.Columns[22].Visible=true;
					ViewState["ThisDepartment"]="Director_Finaace";
					break;
				case BiddingSystem.DepartMentName.����ִ��:
					this.dgList.Columns[23].Visible=true;
					ViewState["ThisDepartment"]="DepartmentRemark";
					break;				
				default:
					break;
			}
			if(State2==ModuleState.Sightless)
			{
				for(int i=24;i<=26;i++)
				{
					this.dgList.Columns[i].Visible=false;
				}
			}
			if(State2==ModuleState.Eyeable)
			{
				for(int i=24;i<=26;i++)
				{
					this.dgList.Columns[i].Visible=true;
				}
			}
		}
		public void InsertDepartMent()
		{
			BLL.BiddingSystem.InsertDepartMent(ViewState["BiddingPrejudicationCode"].ToString());
		}
		public void UpdateDepartMentSelect()
		{
			string sta="0";
			if(ViewState["ThisDepartment"]!=null)
			{
				for(int i=0;i<dgList.Items.Count;i++)
				{
					CheckBox cb = (CheckBox)this.dgList.Items[i].FindControl(ViewState["ThisDepartment"].ToString());
					if(cb.Checked==true)
					{
						sta="1";
						string key = dgList.DataKeys[i].ToString();
						BLL.BiddingSystem.UpDatePrejudicationSelect(ViewState["ThisDepartment"].ToString(),sta,key);
						//UpdateToDB(sta,key);
					}
				}
			}		
		}
		public void UpdateToDB(string sta,string key)
		{
			BLL.Bidding_SupplierDepartmentIdea bidd = new Bidding_SupplierDepartmentIdea();
			bidd.BiddingSupplierCode=key;
			//string depart = ViewState["ThisDepartment"].ToString(); 
			switch(DepartMent)
			{
				case "Depart_Build":
					bidd.Depart_Build=sta;
					break;
				case "Depart_Project":
					bidd.Depart_Project=sta;
					break;
				case "Depart_Agreement":
					bidd.Depart_Agreement=sta;
					break;
				case "Md_Item":
					bidd.Md_Item=sta;
					break;
				case "Md_Project":
					bidd.Md_Project=sta;
					break;
				case "Md_Agreement":
					bidd.Md_Agreement=sta;
					break;
				case "DepartmentRemark1":
					bidd.DepartmentRemark1=sta;
					break;
				case "Director_Project":
					bidd.Director_Project=sta;
					break;
				case "Director_Agreement":
					bidd.Director_Agreement=sta;
					break;
				case "Director_Finaace":
					bidd.Director_Finance=sta;
					break;
				case "DepartmentRemark":
					bidd.DepartmentRemark=sta;
					break;
				default:
					break;
			}
			bidd.Bidding_SupplierDepartmentIdeaUpdate();
			
		}
		#endregion

		private void dgList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{

            //Response.Write(Rms.Web.JavaScript.ScriptStart);

            //Response.Write(Rms.Web.JavaScript.Reload(false));
          
            //Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

        protected void dgList_DeleteCommand1(object source, DataGridCommandEventArgs e)
        {
            
        }
        protected void dgList_ItemCommand1(object source, DataGridCommandEventArgs e)
        {

        }
}
}