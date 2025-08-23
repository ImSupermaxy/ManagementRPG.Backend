namespace ManagementRPG.Domain.Shared.ApiConfig
{
    public static class RunMode
    {
        private static ERunMode _mode = ERunMode.None;

        public static void SetMode(bool isDev, bool isQA, bool isProd)
        {
            if (isDev)
                _mode = ERunMode.Dev;
            if (isQA)
                _mode = ERunMode.QA;
            if (isProd)
                _mode = ERunMode.Prod;
        }

        public static bool IsDev()
        {
            return _mode == ERunMode.Dev;
        }

        public static bool IsQA()
        {
            return _mode == ERunMode.QA;
        }

        public static bool IsProd()
        {
            return _mode == ERunMode.Prod;
        }
    }
}
