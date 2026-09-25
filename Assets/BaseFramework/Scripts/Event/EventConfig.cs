namespace BaseFramework.Runtime
{
    /// <summary>
    /// 时间配置
    /// </summary>
    public static class EventConfig
    {
        public static readonly EventId GameStart = EventId.Create(10001);
        public static readonly EventId GameEnd = EventId.Create(10002);
        public static readonly EventId LoadSceneProgress = EventId.Create(10003);
        public static readonly EventId KeyboardDown = EventId.Create(10004);
        public static readonly EventId KeyboardUp = EventId.Create(10004);
        public static readonly EventId Keyboard = EventId.Create(10004);
        public static readonly EventId MouseDown = EventId.Create(10005);
        public static readonly EventId MouseUp = EventId.Create(10006);
        public static readonly EventId Mouse = EventId.Create(10007);
    }
}