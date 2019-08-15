namespace Rms.WorkFlow
{
    using System;
    using System.Collections;
    using System.Data;

    public sealed class WorkCaseManager
    {
        private WorkCaseManager()
        {
        }

        public static void EndWorkCase(WorkCase workCase, string applicationCode, string actCode, string routerCode, string userCodes, string opinionUserCode, string opinionText)
        {
            try
            {
                Act act = workCase.GetAct(actCode);
                Procedure procedureDifinition = DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode, true);
                Task currentTask = procedureDifinition.GetTask(act.ToTaskCode);
                act.FinishDate = DateTime.Now.ToString();
                act.Status = ActStatus.End;
                if (procedureDifinition.GetTask(act.ToTaskCode).IsOrderly == 1)
                {
                    int num = int.Parse(act.ActUnitCode.Substring(2, act.ActUnitCode.Length - 2)) + 1;
                    IDictionaryEnumerator actEnumerator = workCase.GetActEnumerator();
                    while (actEnumerator.MoveNext())
                    {
                        Act act2 = (Act) actEnumerator.Value;
                        if (((act2.ToTaskCode == act.ToTaskCode) && (act2.Status != ActStatus.End)) && ((act2.Copy != 1) && (act2.ActUnitCode == ("no" + num.ToString()))))
                        {
                            act2.ActUnitCode = "ok" + num.ToString();
                        }
                    }
                }
                Router router = procedureDifinition.GetRouter(routerCode);
                SaveOpinion(act, workCase, opinionText, opinionUserCode, currentTask);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
        }

        private static void FillAct(Act act, DataRow dr)
        {
            if (!dr.IsNull("ActCode"))
            {
                act.ActCode = (string) dr["ActCode"];
            }
            if (!dr.IsNull("CaseCode"))
            {
                act.CaseCode = (string) dr["CaseCode"];
            }
            if (!dr.IsNull("ProjectCode"))
            {
                act.ProjectCode = (string) dr["ProjectCode"];
            }
            if (!dr.IsNull("ProcedureCode"))
            {
                act.ProcedureCode = (string) dr["ProcedureCode"];
            }
            if (!dr.IsNull("ProcedureName"))
            {
                act.ProcedureName = (string) dr["ProcedureName"];
            }
            if (!dr.IsNull("ActUnitCode"))
            {
                act.ActUnitCode = (string) dr["ActUnitCode"];
            }
            if (!dr.IsNull("ActUserCode"))
            {
                act.ActUserCode = (string) dr["ActUserCode"];
            }
            if (!dr.IsNull("FromTaskCode"))
            {
                act.FromTaskCode = (string) dr["FromTaskCode"];
            }
            if (!dr.IsNull("FromTaskName"))
            {
                act.FromTaskName = (string) dr["FromTaskName"];
            }
            if (!dr.IsNull("FromDate"))
            {
                act.FromDate = ((DateTime) dr["FromDate"]).ToString();
            }
            if (!dr.IsNull("FromUserCode"))
            {
                act.FromUserCode = (string) dr["FromUserCode"];
            }
            if (!dr.IsNull("FromUnitCode"))
            {
                act.FromUnitCode = (string) dr["FromUnitCode"];
            }
            if (!dr.IsNull("ToTaskCode"))
            {
                act.ToTaskCode = (string) dr["ToTaskCode"];
            }
            if (!dr.IsNull("ToTaskName"))
            {
                act.ToTaskName = (string) dr["ToTaskName"];
            }
            if (!dr.IsNull("Status"))
            {
                act.Status = (ActStatus) Enum.Parse(typeof(ActStatus), (string) dr["Status"]);
            }
            if (!dr.IsNull("SignDate"))
            {
                act.SignDate = ((DateTime) dr["SignDate"]).ToString();
            }
            if (!dr.IsNull("FinishDate"))
            {
                act.FinishDate = ((DateTime) dr["FinishDate"]).ToString();
            }
            if (!dr.IsNull("ActType"))
            {
                act.ActType = (int) dr["ActType"];
            }
            if (!dr.IsNull("IsSleep"))
            {
                act.IsSleep = (int) dr["IsSleep"];
            }
            if (!dr.IsNull("PlanDate"))
            {
                act.PlanDate = ((DateTime) dr["PlanDate"]).ToString();
            }
            if (!dr.IsNull("ApplicationCode"))
            {
                act.ApplicationCode = (string) dr["ApplicationCode"];
            }
            if (!dr.IsNull("ApplicationSubject"))
            {
                act.ApplicationSubject = (string) dr["ApplicationSubject"];
            }
            if (!dr.IsNull("RouterCode"))
            {
                act.RouterCode = (string) dr["RouterCode"];
            }
            if (!dr.IsNull("TaskActorID"))
            {
                act.TaskActorID = (string) dr["TaskActorID"];
            }
            if (!dr.IsNull("TaskActorName"))
            {
                act.TaskActorName = (string) dr["TaskActorName"];
            }
            if (!dr.IsNull("CopyFromActCode"))
            {
                act.CopyFromActCode = (string) dr["CopyFromActCode"];
            }
            if (!dr.IsNull("Copy"))
            {
                act.Copy = (int) dr["Copy"];
            }
        }

        private static void FillActUser(ActUser actUser, DataRow dr)
        {
            if (!dr.IsNull("ActUserCode"))
            {
                actUser.ActUserCode = (string) dr["ActUserCode"];
            }
            if (!dr.IsNull("CaseCode"))
            {
                actUser.CaseCode = (string) dr["CaseCode"];
            }
            if (!dr.IsNull("ActCode"))
            {
                actUser.ActCode = (string) dr["ActCode"];
            }
            if (!dr.IsNull("UserCode"))
            {
                actUser.UserCode = (string) dr["UserCode"];
            }
        }

        private static void FillCaseProperty(CaseProperty CasePropertyCase, DataRow dr)
        {
            if (!dr.IsNull("WorkFlowCasePropertyCode"))
            {
                CasePropertyCase.WorkFlowCasePropertyCode = (string) dr["WorkFlowCasePropertyCode"];
            }
            if (!dr.IsNull("WorkFlowCaseCode"))
            {
                CasePropertyCase.CaseCode = (string) dr["WorkFlowCaseCode"];
            }
            if (!dr.IsNull("WorkFlowProcedurePropertyCode"))
            {
                CasePropertyCase.ProcedurePropertyCode = (string) dr["WorkFlowProcedurePropertyCode"];
            }
            if (!dr.IsNull("WorkFlowProcedurePropertyValue"))
            {
                CasePropertyCase.ProcedurePropertyValue = (string) dr["WorkFlowProcedurePropertyValue"];
            }
            if (!dr.IsNull("Remak"))
            {
                CasePropertyCase.Remak = (string) dr["Remak"];
            }
        }

        private static void FillDataPackage(DataPackage dataPackage, DataRow dr)
        {
            if (!dr.IsNull("DataPackageCode"))
            {
                dataPackage.DataPackageCode = (string) dr["DataPackageCode"];
            }
            if (!dr.IsNull("ProcedureCode"))
            {
                dataPackage.ProcedureCode = (string) dr["ProcedureCode"];
            }
            if (!dr.IsNull("CaseCode"))
            {
                dataPackage.CaseCode = (string) dr["CaseCode"];
            }
            if (!dr.IsNull("ActCode"))
            {
                dataPackage.DataPackageCode = (string) dr["ActCode"];
            }
            if (!dr.IsNull("DataKey"))
            {
                dataPackage.DataKey = (string) dr["DataKey"];
            }
            if (!dr.IsNull("DataValue"))
            {
                dataPackage.DataValue = (string) dr["DataValue"];
            }
        }

        private static ActUser FillNewActUser(string actCode, string CaseCode, string userCode)
        {
            ActUser user2;
            try
            {
                ActUser user = NewActUser();
                user.IsNew = true;
                user.ActCode = actCode;
                user.CaseCode = CaseCode;
                user.UserCode = userCode;
                user2 = user;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return user2;
        }

        private static void FillOpinion(Opinion opinion, DataRow dr)
        {
            if (!dr.IsNull("OpinionCode"))
            {
                opinion.OpinionCode = (string) dr["OpinionCode"];
            }
            if (!dr.IsNull("ApplicationCode"))
            {
                opinion.ApplicationCode = (string) dr["ApplicationCode"];
            }
            if (!dr.IsNull("ProcedureCode"))
            {
                opinion.ProcedureCode = (string) dr["ProcedureCode"];
            }
            if (!dr.IsNull("CaseCode"))
            {
                opinion.CaseCode = (string) dr["CaseCode"];
            }
            if (!dr.IsNull("OpinionType"))
            {
                opinion.OpinionType = (string) dr["OpinionType"];
            }
            if (!dr.IsNull("UserCode"))
            {
                opinion.UserCode = (string) dr["UserCode"];
            }
            if (!dr.IsNull("OpinionText"))
            {
                opinion.OpinionText = (string) dr["OpinionText"];
            }
            if (!dr.IsNull("OpinionDate"))
            {
                opinion.OpinionDate = ((DateTime) dr["OpinionDate"]).ToString();
            }
            if (!dr.IsNull("TaskID"))
            {
                opinion.TaskID = (string) dr["TaskID"];
            }
            if (!dr.IsNull("TaskCode"))
            {
                opinion.TaskCode = (string) dr["TaskCode"];
            }
            if (!dr.IsNull("TaskActorName"))
            {
                opinion.TaskActorName = (string) dr["TaskActorName"];
            }
            if (!dr.IsNull("TaskActorID"))
            {
                opinion.TaskActorID = (string) dr["TaskActorID"];
            }
        }

        private static void FillWorkCase(WorkCase workCase, DataRow dr)
        {
            if (!dr.IsNull("CaseCode"))
            {
                workCase.CaseCode = (string) dr["CaseCode"];
            }
            if (!dr.IsNull("ProjectCode"))
            {
                workCase.ProjectCode = (string) dr["ProjectCode"];
            }
            if (!dr.IsNull("ProcedureCode"))
            {
                workCase.ProcedureCode = (string) dr["ProcedureCode"];
            }
            if (!dr.IsNull("ApplicationCode"))
            {
                workCase.ApplicationCode = (string) dr["ApplicationCode"];
            }
            if (!dr.IsNull("Subject"))
            {
                workCase.Subject = (string) dr["Subject"];
            }
            if (!dr.IsNull("SourceUnitCode"))
            {
                workCase.SourceUnitCode = (string) dr["SourceUnitCode"];
            }
            if (!dr.IsNull("SourceUserCode"))
            {
                workCase.SourceUserCode = (string) dr["SourceUserCode"];
            }
            if (!dr.IsNull("CreateDate"))
            {
                workCase.CreateDate = ((DateTime) dr["CreateDate"]).ToString();
            }
            if (!dr.IsNull("Status"))
            {
                workCase.Status = (WorkCaseStatus) Enum.Parse(typeof(WorkCaseStatus), (string) dr["Status"]);
            }
            if (!dr.IsNull("FinishDate"))
            {
                workCase.FinishDate = ((DateTime) dr["FinishDate"]).ToString();
            }
            if (!dr.IsNull("Transactor"))
            {
                workCase.Transactor = (string) dr["Transactor"];
            }
            if (!dr.IsNull("TransactUnit"))
            {
                workCase.TransactUnit = (string) dr["TransactUnit"];
            }
        }

        public static void FinishWorkCase(WorkCase workCase, Act act)
        {
            try
            {
                if (act.Copy == 1)
                {
                    act.Status = ActStatus.End;
                    act.FinishDate = DateTime.Now.ToString();
                }
                else
                {
                    workCase.Status = WorkCaseStatus.End;
                    workCase.FinishDate = DateTime.Now.ToString();
                    act.Status = ActStatus.End;
                    act.FinishDate = DateTime.Now.ToString();
                    Act act2 = NewAct();
                    act2.IsNew = true;
                    act2.ActUserCode = act.ActUserCode;
                    act2.ApplicationCode = workCase.ApplicationCode;
                    act2.CaseCode = workCase.CaseCode;
                    act2.FromDate = DateTime.Now.ToString();
                    act2.FromTaskCode = act.ToTaskCode;
                    act2.FromTaskName = act.ToTaskName;
                    act2.FromUserCode = act.ActUserCode;
                    act2.FromUnitCode = act.ActCode;
                    act2.ProcedureCode = workCase.ProcedureCode;
                    act2.ProcedureName = DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode, true).Description;
                    act2.Status = ActStatus.End;
                    Task endTask = DefinitionManager.GetEndTask(DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode, true));
                    act2.ToTaskCode = endTask.TaskCode;
                    act2.ToTaskName = endTask.TaskName;
                    act2.FromUnitCode = act.ActCode;
                    workCase.AddNewAct(act2);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
        }

        public static void ForwardCopyWorkCase(WorkCase workCase, string applicationCode, string actCode, string routerCode, string userCodes, string opinionUserCode, string opinionText, string CopyFromActCode, string routerMessage)
        {
            try
            {
                Act act = workCase.GetAct(actCode);
                Procedure procedureDifinition = DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode, true);
                Task currentTask = procedureDifinition.GetTask(act.ToTaskCode);
                if (routerCode != "")
                {
                    Router router = procedureDifinition.GetRouter(routerCode);
                    Task task = procedureDifinition.GetTask(router.ToTaskCode);
                    foreach (string text in userCodes.Split(new char[] { ';' }))
                    {
                        if (text != "")
                        {
                            string[] textArray = text.Split(new char[] { ',' });
                            Act act2 = NewAct();
                            act2.ProcedureCode = procedureDifinition.ProcedureCode;
                            act2.ProcedureName = procedureDifinition.Description;
                            act2.Status = ActStatus.Begin;
                            act2.IsNew = true;
                            act2.ApplicationCode = applicationCode;
                            act2.CaseCode = workCase.CaseCode;
                            act2.FromDate = DateTime.Now.ToString();
                            act2.FromTaskCode = router.FromTaskCode;
                            act2.FromTaskName = procedureDifinition.GetTask(router.FromTaskCode).TaskName;
                            act2.FromUserCode = opinionUserCode;
                            act2.ToTaskCode = router.ToTaskCode;
                            act2.ProjectCode = routerMessage;
                            act2.ToTaskName = currentTask.TaskActorType;
                            act2.TaskActorID = textArray[1];
                            act2.TaskActorName = textArray[3];
                            act2.RouterCode = routerCode;
                            act2.Copy = 1;
                            act2.IsSleep = 0;
                            act2.FromUnitCode = actCode;
                            workCase.AddNewAct(act2);
                            act2.AddNewActUser(FillNewActUser(act2.ActCode, workCase.CaseCode, textArray[0]));
                            act2.CopyFromActCode = CopyFromActCode;
                        }
                    }
                }
                else
                {
                    foreach (string text2 in userCodes.Split(new char[] { ';' }))
                    {
                        if (text2 != "")
                        {
                            string[] textArray2 = text2.Split(new char[] { ',' });
                            Act act3 = NewAct();
                            act3.ProcedureCode = procedureDifinition.ProcedureCode;
                            act3.ProcedureName = procedureDifinition.Description;
                            act3.Status = ActStatus.Begin;
                            act3.IsNew = true;
                            act3.ApplicationCode = applicationCode;
                            act3.CaseCode = workCase.CaseCode;
                            act3.FromDate = DateTime.Now.ToString();
                            act3.FromTaskCode = currentTask.TaskCode;
                            act3.FromTaskName = currentTask.TaskName;
                            act3.FromUserCode = opinionUserCode;
                            act3.ToTaskCode = currentTask.TaskCode;
                            act3.ToTaskName = currentTask.TaskActorType;
                            act3.TaskActorID = textArray2[1];
                            act3.TaskActorName = textArray2[3];
                            act3.RouterCode = "";
                            act3.Copy = 1;
                            act3.ProjectCode = routerMessage;
                            act3.IsSleep = 0;
                            act3.FromUnitCode = actCode;
                            workCase.AddNewAct(act3);
                            act3.AddNewActUser(FillNewActUser(act3.ActCode, workCase.CaseCode, textArray2[0]));
                            act3.CopyFromActCode = CopyFromActCode;
                        }
                    }
                }
                SaveOpinion(act, workCase, opinionText, opinionUserCode, currentTask);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
        }

        public static void ForwardWorkCase(WorkCase workCase, string applicationCode, string actCode, string routerCode, string userCodes, string opinionUserCode, string opinionText, string routerMessage)
        {
            try
            {
                Act act = workCase.GetAct(actCode);
                Procedure procedureDifinition = DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode, true);
                Task currentTask = procedureDifinition.GetTask(act.ToTaskCode);
                act.FinishDate = DateTime.Now.ToString();
                act.Status = ActStatus.End;
                if (currentTask.TaskType == 5)
                {
                    IDictionaryEnumerator actEnumerator = workCase.GetActEnumerator();
                    while (actEnumerator.MoveNext())
                    {
                        Act act2 = (Act) actEnumerator.Value;
                        if (((act2.ToTaskCode == act.ToTaskCode) && (act2.Status != ActStatus.End)) && (act2.Copy != 1))
                        {
                            act2.Status = ActStatus.End;
                            act2.FinishDate = DateTime.Now.ToString();
                        }
                    }
                }
                Router router = procedureDifinition.GetRouter(routerCode);
                Task task = procedureDifinition.GetTask(router.ToTaskCode);
                if (task.TaskType == 5)
                {
                    int num = 1;
                    foreach (string text in userCodes.Split(new char[] { ';' }))
                    {
                        if (text != "")
                        {
                            string[] textArray = text.Split(new char[] { ',' });
                            if (textArray[0] != "")
                            {
                                Act act3 = NewAct();
                                act3.ProcedureCode = procedureDifinition.ProcedureCode;
                                act3.ProcedureName = procedureDifinition.Description;
                                act3.Status = ActStatus.Begin;
                                act3.IsNew = true;
                                act3.ApplicationCode = applicationCode;
                                act3.CaseCode = workCase.CaseCode;
                                act3.FromDate = DateTime.Now.ToString();
                                act3.FromTaskCode = router.FromTaskCode;
                                act3.FromTaskName = procedureDifinition.GetTask(router.FromTaskCode).TaskName;
                                act3.FromUserCode = opinionUserCode;
                                act3.ToTaskCode = router.ToTaskCode;
                                act3.ToTaskName = procedureDifinition.GetTask(router.ToTaskCode).TaskName;
                                act3.TaskActorID = textArray[1];
                                act3.TaskActorName = textArray[3];
                                act3.RouterCode = routerCode;
                                act3.ProjectCode = routerMessage;
                                act3.FromUnitCode = actCode;
                                if (task.IsOrderly == 1)
                                {
                                    if (num == 1)
                                    {
                                        act3.ActUnitCode = "ok" + num.ToString();
                                    }
                                    else
                                    {
                                        act3.ActUnitCode = "no" + num.ToString();
                                    }
                                }
                                else
                                {
                                    act3.ActUnitCode = "ok0";
                                }
                                workCase.AddNewAct(act3);
                                act3.AddNewActUser(FillNewActUser(act3.ActCode, workCase.CaseCode, textArray[0]));
                                num++;
                            }
                        }
                    }
                }
                else
                {
                    Act act4 = NewAct();
                    act4.ProcedureCode = procedureDifinition.ProcedureCode;
                    act4.ProcedureName = procedureDifinition.Description;
                    act4.Status = ActStatus.Begin;
                    act4.IsNew = true;
                    act4.ApplicationCode = applicationCode;
                    act4.CaseCode = workCase.CaseCode;
                    act4.FromDate = DateTime.Now.ToString();
                    act4.FromTaskCode = router.FromTaskCode;
                    act4.FromTaskName = procedureDifinition.GetTask(router.FromTaskCode).TaskName;
                    act4.FromUserCode = opinionUserCode;
                    act4.ToTaskCode = router.ToTaskCode;
                    act4.ToTaskName = procedureDifinition.GetTask(router.ToTaskCode).TaskName;
                    act4.ProjectCode = routerMessage;
                    act4.RouterCode = routerCode;
                    act4.FromUnitCode = actCode;
                    workCase.AddNewAct(act4);
                    foreach (string text2 in userCodes.Split(new char[] { ';' }))
                    {
                        if (text2 != "")
                        {
                            string[] textArray2 = text2.Split(new char[] { ',' });
                            if (textArray2[0] != "")
                            {
                                act4.AddNewActUser(FillNewActUser(act4.ActCode, workCase.CaseCode, textArray2[0]));
                            }
                        }
                    }
                }
                SaveOpinion(act, workCase, opinionText, opinionUserCode, currentTask);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
        }

        public static ArrayList GetActivityAct(WorkCase workCase)
        {
            ArrayList list2;
            try
            {
                ArrayList list = new ArrayList();
                IDictionaryEnumerator actEnumerator = workCase.GetActEnumerator();
                while (actEnumerator.MoveNext())
                {
                    Act act = (Act) actEnumerator.Value;
                    if ((act.Status == ActStatus.Begin) || (act.Status == ActStatus.DealWith))
                    {
                        list.Add(act);
                    }
                }
                list2 = list;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return list2;
        }

        public static ArrayList GetOpinionByTaskCode(WorkCase workCase, string taskCode)
        {
            ArrayList list2;
            try
            {
                ArrayList list = new ArrayList();
                IDictionaryEnumerator opinionEnumerator = workCase.GetOpinionEnumerator();
                while (opinionEnumerator.MoveNext())
                {
                    Opinion opinion = (Opinion) opinionEnumerator.Value;
                    if (opinion.TaskCode == taskCode)
                    {
                        list.Add(opinion);
                    }
                }
                list2 = list;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return list2;
        }

        public static Task GetTaskByAct(Act act)
        {
            Task task;
            try
            {
                Procedure procedureDifinition = DefinitionManager.GetProcedureDifinition(act.ProcedureCode, true);
                if (procedureDifinition == null)
                {
                    return null;
                }
                task = procedureDifinition.GetTask(act.ToTaskCode);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return task;
        }

        public static WorkCase GetWorkCase(string CaseCode)
        {
            return LoadWorkCaseData(CaseCode);
        }

        private static WorkCase LoadWorkCaseData(string CaseCode)
        {
            WorkCase case2;
            try
            {
                DataSet set = InterfaceManager.iWorkCase.InputWorkCase(CaseCode);
                DataTable table = set.Tables["WorkFlowCase"];
                if (table.Rows.Count == 0)
                {
                    throw new ApplicationException("没有这个流程 ！");
                }
                DataRow dr = table.Rows[0];
                WorkCase workCase = new WorkCase();
                FillWorkCase(workCase, dr);
                foreach (DataRow row2 in set.Tables["WorkFlowAct"].Rows)
                {
                    Act act = new Act();
                    FillAct(act, row2);
                    foreach (DataRow row3 in set.Tables["WorkFlowActUser"].Select(string.Format("ActCode='{0}'", act.ActCode)))
                    {
                        ActUser actUser = new ActUser();
                        FillActUser(actUser, row3);
                        act.AddNewActUser(actUser);
                    }
                    workCase.AddNewAct(act);
                }
                foreach (DataRow row4 in set.Tables["WorkFlowDataPackage"].Rows)
                {
                    DataPackage dataPackage = new DataPackage();
                    FillDataPackage(dataPackage, row4);
                    workCase.AddNewDataPackage(dataPackage);
                }
                foreach (DataRow row5 in set.Tables["WorkFlowOpinion"].Rows)
                {
                    Opinion opinion = new Opinion();
                    FillOpinion(opinion, row5);
                    workCase.AddNewOpinion(opinion);
                }
                foreach (DataRow row6 in set.Tables["WorkFlowCaseProperty"].Rows)
                {
                    CaseProperty casePropertyCase = new CaseProperty();
                    FillCaseProperty(casePropertyCase, row6);
                    workCase.AddNewCaseProperty(casePropertyCase);
                }
                case2 = workCase;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return case2;
        }

        public static Act NewAct()
        {
            Act act2;
            try
            {
                string newSysetmCode = InterfaceManager.iSystemCode.GetNewSysetmCode("ActCode");
                Act act = new Act();
                act.ActCode = newSysetmCode;
                act2 = act;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return act2;
        }

        public static ActUser NewActUser()
        {
            ActUser user2;
            try
            {
                string newSysetmCode = InterfaceManager.iSystemCode.GetNewSysetmCode("ActUserCode");
                ActUser user = new ActUser();
                user.ActUserCode = newSysetmCode;
                user2 = user;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return user2;
        }

        public static DataPackage NewDataPackage()
        {
            DataPackage package2;
            try
            {
                string newSysetmCode = InterfaceManager.iSystemCode.GetNewSysetmCode("DataPackageCode");
                DataPackage package = new DataPackage();
                package.DataPackageCode = newSysetmCode;
                package2 = package;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return package2;
        }

        public static Opinion NewOpinion()
        {
            Opinion opinion2;
            try
            {
                string newSysetmCode = InterfaceManager.iSystemCode.GetNewSysetmCode("OpinionCode");
                Opinion opinion = new Opinion();
                opinion.OpinionCode = newSysetmCode;
                opinion2 = opinion;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return opinion2;
        }

        public static WorkCase NewWorkCase()
        {
            WorkCase case2;
            try
            {
                string newSysetmCode = InterfaceManager.iSystemCode.GetNewSysetmCode("CaseCode");
                WorkCase @case = new WorkCase();
                @case.CaseCode = newSysetmCode;
                case2 = @case;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return case2;
        }

        public static void RetrogradeWorkCase(WorkCase workCase, string applicationCode, string actCode, string FromTaskCode, string userCodes, string opinionUserCode, string opinionText, string routerMessage)
        {
            try
            {
                Act act = workCase.GetAct(actCode);
                Procedure procedureDifinition = DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode, true);
                Task currentTask = procedureDifinition.GetTask(act.ToTaskCode);
                act.FinishDate = DateTime.Now.ToString();
                act.Status = ActStatus.End;
                if (currentTask.TaskType == 5)
                {
                    IDictionaryEnumerator actEnumerator = workCase.GetActEnumerator();
                    while (actEnumerator.MoveNext())
                    {
                        Act act2 = (Act) actEnumerator.Value;
                        if (((act2.ToTaskCode == act.ToTaskCode) && (act2.Status != ActStatus.End)) && (act2.Copy != 1))
                        {
                            act2.Status = ActStatus.End;
                            act2.FinishDate = DateTime.Now.ToString();
                        }
                    }
                }
                Task task = procedureDifinition.GetTask(FromTaskCode);
                if (task.TaskType == 5)
                {
                    foreach (string text in userCodes.Split(new char[] { ';' }))
                    {
                        if (text != "")
                        {
                            string[] textArray = text.Split(new char[] { ',' });
                            if (textArray[0] != "")
                            {
                                Act act3 = NewAct();
                                act3.ProcedureCode = procedureDifinition.ProcedureCode;
                                act3.ProcedureName = procedureDifinition.Description;
                                act3.Status = ActStatus.Begin;
                                act3.IsNew = true;
                                act3.ApplicationCode = applicationCode;
                                act3.CaseCode = workCase.CaseCode;
                                act3.FromDate = DateTime.Now.ToString();
                                act3.FromTaskCode = act.ToTaskCode;
                                act3.FromTaskName = procedureDifinition.GetTask(act.ToTaskCode).TaskName;
                                act3.FromUserCode = opinionUserCode;
                                act3.ToTaskCode = task.TaskCode;
                                act3.ToTaskName = procedureDifinition.GetTask(task.TaskCode).TaskName + "( 退回 )";
                                act3.TaskActorID = textArray[1];
                                act3.TaskActorName = textArray[3];
                                act3.ActType = 1;
                                act3.ProjectCode = routerMessage;
                                act3.ActUnitCode = "ok0";
                                act3.FromUnitCode = actCode;
                                workCase.AddNewAct(act3);
                                act3.AddNewActUser(FillNewActUser(act3.ActCode, workCase.CaseCode, textArray[0]));
                            }
                        }
                    }
                }
                else
                {
                    Act act4 = NewAct();
                    act4.ProcedureCode = procedureDifinition.ProcedureCode;
                    act4.ProcedureName = procedureDifinition.Description;
                    act4.Status = ActStatus.Begin;
                    act4.IsNew = true;
                    act4.ApplicationCode = applicationCode;
                    act4.CaseCode = workCase.CaseCode;
                    act4.FromDate = DateTime.Now.ToString();
                    act4.FromTaskCode = act.ToTaskCode;
                    act4.FromTaskName = procedureDifinition.GetTask(act.ToTaskCode).TaskName;
                    act4.FromUserCode = opinionUserCode;
                    act4.ToTaskCode = task.TaskCode;
                    act4.ToTaskName = procedureDifinition.GetTask(task.TaskCode).TaskName + "( 退回 )";
                    act4.ProjectCode = routerMessage;
                    act4.ActType = 1;
                    act4.FromUnitCode = actCode;
                    workCase.AddNewAct(act4);
                    foreach (string text2 in userCodes.Split(new char[] { ';' }))
                    {
                        if (text2 != "")
                        {
                            string[] textArray2 = text2.Split(new char[] { ',' });
                            if (textArray2[0] != "")
                            {
                                act4.AddNewActUser(FillNewActUser(act4.ActCode, workCase.CaseCode, textArray2[0]));
                            }
                        }
                    }
                }
                SaveOpinion(act, workCase, opinionText, opinionUserCode, currentTask);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
        }

        public static void ReturnActWorkCase(WorkCase workCase, string applicationCode, string actCode, string FromTaskCode, string userCodes, string SystemUserCode, string opinionText, string routerMessage)
        {
        }

        public static void SaveOpinion(Act act, WorkCase workCase, string opinionText, string opinionUserCode, Task currentTask)
        {
            bool flag = true;
            Opinion opinion = null;
            IDictionaryEnumerator opinionEnumerator = workCase.GetOpinionEnumerator();
            while (opinionEnumerator.MoveNext())
            {
                Opinion opinion2 = (Opinion) opinionEnumerator.Value;
                if (opinion2.ApplicationCode == act.ActCode)
                {
                    flag = false;
                    opinion = opinion2;
                }
            }
            if (flag)
            {
                opinion = NewOpinion();
                opinion.IsNew = true;
            }
            opinion.ApplicationCode = act.ActCode;
            opinion.CaseCode = workCase.CaseCode;
            opinion.OpinionDate = DateTime.Now.ToString();
            opinion.OpinionText = opinionText;
            opinion.ProcedureCode = workCase.ProcedureCode;
            opinion.UserCode = opinionUserCode;
            opinion.TaskID = currentTask.TaskID;
            opinion.TaskCode = currentTask.TaskCode;
            opinion.TaskActorID = act.TaskActorID;
            opinion.TaskActorName = act.TaskActorName;
            if (flag)
            {
                workCase.AddNewOpinion(opinion);
            }
        }

        public static void SaveSignOpinionText(WorkCase workCase, Act act, Task task, string userCode, string opinionText)
        {
            try
            {
                IDictionaryEnumerator opinionEnumerator = workCase.GetOpinionEnumerator();
                Opinion opinion = null;
                while (opinionEnumerator.MoveNext())
                {
                    Opinion opinion2 = (Opinion) opinionEnumerator.Value;
                    if (opinion2.TaskActorID == act.TaskActorID)
                    {
                        opinion = opinion2;
                        break;
                    }
                }
                if (opinion == null)
                {
                    opinion = NewOpinion();
                    opinion.IsNew = true;
                    workCase.AddNewOpinion(opinion);
                }
                opinion.ApplicationCode = workCase.ApplicationCode;
                opinion.CaseCode = workCase.CaseCode;
                opinion.OpinionDate = DateTime.Now.ToString();
                opinion.OpinionText = opinionText;
                opinion.ProcedureCode = workCase.ProcedureCode;
                opinion.TaskActorID = act.TaskActorID;
                opinion.TaskActorName = act.TaskActorName;
                opinion.TaskCode = act.ToTaskCode;
                opinion.TaskID = task.TaskID;
                opinion.UserCode = userCode;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
        }

        public static void SaveWorkCase(WorkCase workCase)
        {
            InterfaceManager.iWorkCase.OutputWorkCase(SaveWorkCaseData(workCase), workCase.CaseCode);
        }

        public static DataSet SaveWorkCaseData(WorkCase workCase)
        {
            DataSet set2;
            try
            {
                DataSet set = InterfaceManager.iWorkCase.InputWorkCase(workCase.CaseCode);
                DataRow dr = null;
                if (workCase.IsNew)
                {
                    dr = set.Tables["WorkFlowCase"].NewRow();
                }
                else
                {
                    dr = set.Tables["WorkFlowCase"].Rows[0];
                }
                WriteWorkCase(workCase, dr);
                if (workCase.IsNew)
                {
                    set.Tables["WorkFlowCase"].Rows.Add(dr);
                }
                IDictionaryEnumerator actEnumerator = workCase.GetActEnumerator();
                while (actEnumerator.MoveNext())
                {
                    Act act = (Act) actEnumerator.Value;
                    DataRow row2 = null;
                    if (act.IsNew)
                    {
                        row2 = set.Tables["WorkFlowAct"].NewRow();
                    }
                    else
                    {
                        row2 = set.Tables["WorkFlowAct"].Select(string.Format("ActCode='{0}'", act.ActCode))[0];
                    }
                    WriteAct(act, row2);
                    if (act.IsNew)
                    {
                        set.Tables["WorkFlowAct"].Rows.Add(row2);
                    }
                    IDictionaryEnumerator actUserEnumerator = act.GetActUserEnumerator();
                    while (actUserEnumerator.MoveNext())
                    {
                        ActUser actUser = (ActUser) actUserEnumerator.Value;
                        DataRow row3 = null;
                        if (actUser.IsNew)
                        {
                            row3 = set.Tables["WorkFlowActUser"].NewRow();
                        }
                        else
                        {
                            row3 = set.Tables["WorkFlowActUser"].Select(string.Format("ActUserCode='{0}'", actUser.ActUserCode))[0];
                        }
                        WriteActUser(actUser, row3);
                        if (actUser.IsNew)
                        {
                            set.Tables["WorkFlowActUser"].Rows.Add(row3);
                        }
                    }
                }
                IDictionaryEnumerator dataPackageEnumerator = workCase.GetDataPackageEnumerator();
                while (dataPackageEnumerator.MoveNext())
                {
                    DataPackage dataPackage = (DataPackage) dataPackageEnumerator.Value;
                    DataRow row4 = null;
                    if (dataPackage.IsNew)
                    {
                        row4 = set.Tables["WorkFlowDataPackage"].NewRow();
                    }
                    else
                    {
                        row4 = set.Tables["WorkFlowDataPackage"].Select(string.Format("DataPackageCode='{0}'", dataPackage.DataPackageCode))[0];
                    }
                    WriteDataPackage(dataPackage, row4);
                    if (dataPackage.IsNew)
                    {
                        set.Tables["WorkFlowDataPackage"].Rows.Add(row4);
                    }
                }
                IDictionaryEnumerator opinionEnumerator = workCase.GetOpinionEnumerator();
                while (opinionEnumerator.MoveNext())
                {
                    Opinion opinion = (Opinion) opinionEnumerator.Value;
                    DataRow row5 = null;
                    if (opinion.IsNew)
                    {
                        row5 = set.Tables["WorkFlowOpinion"].NewRow();
                    }
                    else
                    {
                        row5 = set.Tables["WorkFlowOpinion"].Select(string.Format("OpinionCode='{0}'", opinion.OpinionCode))[0];
                    }
                    WriteOpinion(opinion, row5);
                    if (opinion.IsNew)
                    {
                        set.Tables["WorkFlowOpinion"].Rows.Add(row5);
                    }
                }
                IDictionaryEnumerator casePropertyEnumerator = workCase.GetCasePropertyEnumerator();
                while (casePropertyEnumerator.MoveNext())
                {
                    CaseProperty casePropertyCase = (CaseProperty) casePropertyEnumerator.Value;
                    DataRow row6 = null;
                    row6 = set.Tables["WorkFlowCaseProperty"].Select(string.Format("WorkFlowCasePropertyCode='{0}'", casePropertyCase.WorkFlowCasePropertyCode))[0];
                    WriteCaseProperty(casePropertyCase, row6);
                }
                set2 = set;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return set2;
        }

        public static void SignWorkAct(Act act, string userCode, string unitCode)
        {
            try
            {
                act.Status = ActStatus.DealWith;
                act.SignDate = DateTime.Now.ToString();
                act.ActUserCode = userCode;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
        }

        public static WorkCase StartNewWorkCase(string applicationCode, string procedureCode, string userCode, string unitCode, ref string currentActCode, string Transactor, string TransactUnit)
        {
            WorkCase case2;
            try
            {
                Procedure procedureDifinition = DefinitionManager.GetProcedureDifinition(procedureCode, true);
                WorkCase @case = NewWorkCase();
                @case.ApplicationCode = applicationCode;
                @case.CreateDate = DateTime.Now.ToString();
                @case.IsNew = true;
                @case.Status = WorkCaseStatus.Begin;
                @case.ProcedureCode = procedureCode;
                @case.SourceUserCode = userCode;
                @case.SourceUnitCode = unitCode;
                @case.Transactor = Transactor;
                @case.TransactUnit = TransactUnit;
                Act act = NewAct();
                act.IsNew = true;
                act.ActUserCode = userCode;
                act.ApplicationCode = applicationCode;
                act.CaseCode = @case.CaseCode;
                Task beginTask = DefinitionManager.GetBeginTask(procedureDifinition);
                act.FromDate = DateTime.Now.ToString();
                act.FromTaskCode = beginTask.TaskCode;
                act.FromTaskName = beginTask.TaskName;
                act.FromUserCode = userCode;
                act.FromUnitCode = unitCode;
                act.SignDate = DateTime.Now.ToString();
                act.ProcedureCode = procedureCode;
                act.ProcedureName = procedureDifinition.Description;
                act.Status = ActStatus.DealWith;
                Task firstTask = DefinitionManager.GetFirstTask(procedureDifinition);
                act.ToTaskCode = firstTask.TaskCode;
                act.ToTaskName = firstTask.TaskName;
                @case.AddNewAct(act);
                ActUser actUser = NewActUser();
                actUser.IsNew = true;
                actUser.ActCode = act.ActCode;
                actUser.CaseCode = @case.CaseCode;
                actUser.UserCode = userCode;
                act.AddNewActUser(actUser);
                currentActCode = act.ActCode;
                case2 = @case;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return case2;
        }

        public static DataSet WorkCaseDelete(string CaseCode)
        {
            DataSet set2;
            try
            {
                DataSet set = InterfaceManager.iWorkCase.InputWorkCase(CaseCode);
                DataTable table = set.Tables["WorkFlowCase"];
                if (table.Rows.Count == 0)
                {
                    throw new ApplicationException("没有这个流程 ！");
                }
                DataRow row = table.Rows[0];
                foreach (DataRow row2 in set.Tables["WorkFlowAct"].Rows)
                {
                    Act act = new Act();
                    FillAct(act, row2);
                    foreach (DataRow row3 in set.Tables["WorkFlowActUser"].Select(string.Format("ActCode='{0}'", act.ActCode)))
                    {
                        row3.Delete();
                    }
                    row2.Delete();
                }
                foreach (DataRow row4 in set.Tables["WorkFlowDataPackage"].Rows)
                {
                    row4.Delete();
                }
                foreach (DataRow row5 in set.Tables["WorkFlowOpinion"].Rows)
                {
                    row5.Delete();
                }
                foreach (DataRow row6 in set.Tables["WorkFlowCaseProperty"].Rows)
                {
                    row6.Delete();
                }
                row.Delete();
                set2 = set;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return set2;
        }

        private static void WriteAct(Act act, DataRow dr)
        {
            dr["ActCode"] = act.ActCode;
            dr["CaseCode"] = act.CaseCode;
            dr["ProjectCode"] = act.ProjectCode;
            dr["ProcedureCode"] = act.ProcedureCode;
            dr["ProcedureName"] = act.ProcedureName;
            dr["ActUnitCode"] = act.ActUnitCode;
            dr["ActUserCode"] = act.ActUserCode;
            dr["FromTaskCode"] = act.FromTaskCode;
            dr["FromTaskName"] = act.FromTaskName;
            if (act.FromDate == "")
            {
                dr["FromDate"] = DBNull.Value;
            }
            else
            {
                dr["FromDate"] = act.FromDate;
            }
            dr["FromUserCode"] = act.FromUserCode;
            dr["FromUnitCode"] = act.FromUnitCode;
            dr["ToTaskCode"] = act.ToTaskCode;
            dr["ToTaskName"] = act.ToTaskName;
            dr["Status"] = act.Status.ToString();
            if (act.SignDate == "")
            {
                dr["SignDate"] = DBNull.Value;
            }
            else
            {
                dr["SignDate"] = act.SignDate;
            }
            if (act.FinishDate == "")
            {
                dr["FinishDate"] = DBNull.Value;
            }
            else
            {
                dr["FinishDate"] = act.FinishDate;
            }
            dr["ActType"] = act.ActType;
            dr["IsSleep"] = act.IsSleep;
            if (act.PlanDate == "")
            {
                dr["PlanDate"] = DBNull.Value;
            }
            else
            {
                dr["PlanDate"] = act.PlanDate;
            }
            dr["ApplicationCode"] = act.ApplicationCode;
            dr["ApplicationSubject"] = act.ApplicationSubject;
            dr["RouterCode"] = act.RouterCode;
            dr["TaskActorID"] = act.TaskActorID;
            dr["TaskActorName"] = act.TaskActorName;
            dr["Copy"] = act.Copy;
            dr["CopyFromActCode"] = act.CopyFromActCode;
        }

        private static void WriteActUser(ActUser actUser, DataRow dr)
        {
            dr["ActUserCode"] = actUser.ActUserCode;
            dr["CaseCode"] = actUser.CaseCode;
            dr["ActCode"] = actUser.ActCode;
            dr["UserCode"] = actUser.UserCode;
        }

        private static void WriteCaseProperty(CaseProperty CasePropertyCase, DataRow dr)
        {
            dr["WorkFlowCasePropertyCode"] = CasePropertyCase.WorkFlowCasePropertyCode;
            dr["WorkFlowCaseCode"] = CasePropertyCase.CaseCode;
            dr["WorkFlowProcedurePropertyCode"] = CasePropertyCase.ProcedurePropertyCode;
            dr["WorkFlowProcedurePropertyValue"] = CasePropertyCase.ProcedurePropertyValue;
            dr["Remak"] = CasePropertyCase.Remak;
        }

        private static void WriteDataPackage(DataPackage dataPackage, DataRow dr)
        {
            dr["DataPackageCode"] = dataPackage.DataPackageCode;
            dr["ProcedureCode"] = dataPackage.ProcedureCode;
            dr["CaseCode"] = dataPackage.CaseCode;
            dr["ActCode"] = dataPackage.ActCode;
            dr["DataKey"] = dataPackage.DataKey;
            dr["DataValue"] = dataPackage.DataValue;
        }

        private static void WriteOpinion(Opinion opinion, DataRow dr)
        {
            dr["OpinionCode"] = opinion.OpinionCode;
            dr["ApplicationCode"] = opinion.ApplicationCode;
            dr["ProcedureCode"] = opinion.ProcedureCode;
            dr["CaseCode"] = opinion.CaseCode;
            dr["OpinionType"] = opinion.OpinionType;
            dr["UserCode"] = opinion.UserCode;
            dr["OpinionText"] = opinion.OpinionText;
            if (opinion.OpinionDate == "")
            {
                dr["OpinionDate"] = DBNull.Value;
            }
            else
            {
                dr["OpinionDate"] = opinion.OpinionDate;
            }
            dr["TaskID"] = opinion.TaskID;
            dr["TaskCode"] = opinion.TaskCode;
            dr["TaskActorName"] = opinion.TaskActorName;
            dr["TaskActorID"] = opinion.TaskActorID;
        }

        private static void WriteWorkCase(WorkCase workCase, DataRow dr)
        {
            dr["CaseCode"] = workCase.CaseCode;
            dr["ProjectCode"] = workCase.ProjectCode;
            dr["ProcedureCode"] = workCase.ProcedureCode;
            dr["ApplicationCode"] = workCase.ApplicationCode;
            dr["Subject"] = workCase.Subject;
            dr["SourceUnitCode"] = workCase.SourceUnitCode;
            dr["SourceUserCode"] = workCase.SourceUserCode;
            dr["CreateDate"] = workCase.CreateDate;
            dr["Status"] = workCase.Status.ToString();
            dr["Transactor"] = workCase.Transactor;
            dr["TransactUnit"] = workCase.TransactUnit;
            if (workCase.FinishDate == "")
            {
                dr["FinishDate"] = DBNull.Value;
            }
            else
            {
                dr["FinishDate"] = workCase.FinishDate;
            }
        }
    }
}

