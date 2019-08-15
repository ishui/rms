namespace Rms.ORMap
{
    using System;
    using System.Collections;

    public sealed class EntityDataManager
    {
        private static Hashtable m_entitys = new Hashtable();  //保存entitydata的实例,并保持单个实例

        private EntityDataManager()
        {
        }

        public static void CloneEntityStruct(EntityData entity, string className)
        {
            try
            {
                EntityData data = null;
                if (m_entitys.Contains(className))  //从e_entitys对象列表中查找
                {
                    data = (EntityData) m_entitys[className];
                }
                else
                {
                    data = ClassBuilderFactory.GetClassBuilder(ClassBuilderManager.GetClassBuilderName(className)).BuildClass(className);
                    m_entitys.Add(className, data);
                }
                data.CloneDataStucture(entity);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static EntityData GetEmptyEntity(string className)
        {
            EntityData data2;
            if (m_entitys.Contains(className))
            {
                return ((EntityData) m_entitys[className]).CloneData();
            }
            try
            {
                EntityData data = ClassBuilderFactory.GetClassBuilder(ClassBuilderManager.GetClassBuilderName(className)).BuildClass(className);
                m_entitys.Add(className, data);
                data2 = data.CloneData();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }
    }
}

