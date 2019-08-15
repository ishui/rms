namespace Codefresh.PhotoBrowserLibrary.Collections
{
    using Codefresh.PhotoBrowserLibrary;
    using System;
    using System.Collections;
    using System.IO;
    using System.Reflection;
    using System.Runtime.Serialization.Formatters.Binary;

    [Serializable]
    public abstract class PhotoCollectionBase
    {
        private SortedList innerList;
        protected Hashtable keyMap;

        public PhotoCollectionBase()
        {
            this.innerList = new SortedList(this.CreateComparer());
            this.keyMap = new Hashtable();
        }

        protected void Add(KeyBase key, PhotoObjectBase obj)
        {
            this.innerList.Add(key, obj);
            this.keyMap.Add(obj.Id, key);
        }

        public PhotoCollectionBase Clone()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream serializationStream = new MemoryStream();
            formatter.Serialize(serializationStream, this);
            serializationStream.Seek((long) 0, SeekOrigin.Begin);
            return (PhotoCollectionBase) formatter.Deserialize(serializationStream);
        }

        public void CopyTo(Array array, int index)
        {
            new ArrayList(this.innerList.Values).CopyTo(array, index);
        }

        protected abstract IComparer CreateComparer();
        protected abstract KeyBase CreateKey(PhotoObjectBase obj);
        public PhotoObjectBase GetByIndex(int index)
        {
            return (PhotoObjectBase) this.innerList.GetByIndex(index);
        }

        public IEnumerator GetEnumerator()
        {
            return this.innerList.Values.GetEnumerator();
        }

        public int IndexOf(PhotoObjectBase obj)
        {
            KeyBase key = this.CreateKey(obj);
            return this.innerList.IndexOfKey(key);
        }

        protected void Remove(KeyBase key)
        {
            this.innerList.Remove(key);
            this.keyMap.Remove(key.Id);
        }

        public int Count
        {
            get
            {
                return this.innerList.Count;
            }
        }

        public PhotoObjectBase this[int id]
        {
            get
            {
                KeyBase base2 = (KeyBase) this.keyMap[id];
                return (PhotoObjectBase) this.innerList[base2];
            }
        }

        protected ICollection Keys
        {
            get
            {
                return this.innerList.Keys;
            }
        }
    }
}

