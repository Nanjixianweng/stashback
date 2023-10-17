using System;
using System.Collections.Generic;
using System.Text;

namespace Stash.Project.IBusinessRealizeAppService
{
    public class ResultApi<T>
    {
        public int Code { get; set; }
        public T Data { get; set; }
        public int Count { get; set; }
    }
}
