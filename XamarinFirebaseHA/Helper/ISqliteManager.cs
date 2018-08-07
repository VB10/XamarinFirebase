using System;
using System.Collections.Generic;

namespace XamarinFirebaseHA.Helper
{
    public interface ISqliteManager
    {
        void Insert<T>(T data);
        List<T> GetAll<T>() where T : new();
        int Count<T>() where T : new();
        void Delete<T>(T data);
        void Update<T>(T data);
        void CreateTable<T>();
    }
}
