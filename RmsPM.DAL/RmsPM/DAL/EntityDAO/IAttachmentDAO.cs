namespace RmsPM.DAL.EntityDAO
{
    using System;
    using Rms.ORMap;

    public interface IAttachmentDAO
    {
        void DeleteAttachMent(EntityData entity);
        EntityData GetAllAttachMent();
        EntityData GetAttachMentByCode(string code);
        EntityData GetAttachMentByMasterCode(string type, string code);
        EntityData GetAttachMentByTypeAndMasterCode(string AttachMentType, string MasterCode);
        void SubmitAllAttachMent(EntityData entity);
        void UpdateAttachMent(EntityData entity);
    }
}

