namespace Basket.Core.DTO
{
    public class ApiResultDTO<T> where T : class
    {
        public ApiResultDTO()
        {
            ResultType = ResultType.Success;
        }
        public ResultType ResultType { get; set; }

        public string Message { get; set; }

        public bool IsSucceed { get { return ResultType == ResultType.Success; } }

        public T Result { get; set; }
    }

    public enum ResultType
    {
        Success = 1,
        Failed = 2,
        NotFound = 3
    }
}

