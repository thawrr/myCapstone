using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CapstoneBlackstone.C_SharpClasses
{
    public class Param
    {
        public string Name;
        public object Val;
        public SqlDbType Type;

        public Param(string n, object v, SqlDbType t)
        {
            this.Name = n;
            this.Val = v;
            this.Type = t;
        }
    }
}