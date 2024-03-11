using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SharpBoxes.Dlls
{
    /// <summary>
    /// 提供Dll模型集合的类
    /// </summary>
    public class DllModelCollection : ICollection<DllModel>
    {
        private readonly List<DllModel> _list;
        public int Count => _list.Count;
        public bool IsReadOnly => false;

        public DllModel this[string index]
        {
            get { return _list.FirstOrDefault(s => s.Name == index); }
        }

        public void Add(DllModel item)
        {
            _list.Add(item);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(DllModel item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(DllModel[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<DllModel> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public bool Remove(DllModel item)
        {
            return _list.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public DllModelCollection(string folder, Type baseTypeFilter)
        {
            this._list = LibLoadHelper.GetDllModelsFromFolder(folder, baseTypeFilter);
        }
    }
}
