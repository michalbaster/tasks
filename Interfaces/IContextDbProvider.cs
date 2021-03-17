using System;

namespace Tasks
{
    public interface IContextDbProvider
    {
        void SetDb(DbDetails dbDetails);
        DbDetails GetDb();
    }
}