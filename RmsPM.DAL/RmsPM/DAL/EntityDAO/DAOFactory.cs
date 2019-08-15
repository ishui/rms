namespace RmsPM.DAL.EntityDAO
{
    using System;

    public class DAOFactory
    {
        public static IAttachmentDAO GetAttachmentDAO()
        {
            return AttachmentDAO.getAttachmentDAO();
        }
    }
}

