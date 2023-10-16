using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stash.Project
{
    public class ResultCode
    {
        public const int Success = 200;

        public const int Error = 500;
    }

    public class ResultMsg
    {
        public const string AddSuccess = "添加成功";
        public const string AddError = "添加失败";

        public const string UpdateSuccess = "编辑成功";
        public const string UpdateError = "编辑失败";

        public const string DeleteSuccess = "删除成功";
        public const string DeleteError = "删除失败";

        public const string RequestSuccess = "请求成功";
        public const string RequestError = "请求失败";
    }
}
