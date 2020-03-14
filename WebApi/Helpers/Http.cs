namespace WebApi.Helpers
{
    public static class Http
    {
        private static readonly string _okMessage = "ok";
        private static readonly string _errorMessage = "error";
        public static object GenerateOkAnswer(string description = "Done")
        {
            return new
            {
                status = _okMessage,
                description
            };
        }
        
        public static object GenerateOkAnswer(object description)
        {
            return new
            {
                status = _okMessage,
                description
            };
        }

        public static object GenerateErrorAnswer(string description = "There was some error")
        {
            return new
            {
                status = _errorMessage,
                description
            };;
        }
    }

}