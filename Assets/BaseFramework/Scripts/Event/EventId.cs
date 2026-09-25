namespace BaseFramework.Runtime
{
    /// <summary>事件Id</summary>
    public readonly struct EventId
    {
        public readonly int Id;
        private EventId(int id) => this.Id = id;
        public static EventId Create(int id) => new EventId(id);

        public bool Equals(EventId other) => Id == other.Id;

        public override bool Equals(object obj) => obj is EventId other && Id == other.Id;
        public override int GetHashCode() => Id;
        public static bool operator ==(EventId left, EventId right) => left.Equals(right);
        public static bool operator !=(EventId left, EventId right) => !left.Equals(right);

        public override string ToString()
        {
            return $"EventId({Id})";
        }
    }
}
