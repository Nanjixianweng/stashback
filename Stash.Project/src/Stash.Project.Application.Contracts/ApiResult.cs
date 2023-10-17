namespace Stash.Project
{
    public class ApiResult
    {
        public int code { get; set; }

        public string msg { get; set; } = null!;

        public int count { get; set; }

        public object data { get; set; } = null!;
    }
}
