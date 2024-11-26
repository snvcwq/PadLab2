﻿namespace Common
{
    public class SyncEntity
    {
        public Guid Id { get; set; }
        public DateTime LastChangeAt { get; set; }
        public string JsonData { get; set; }
        public string SyncType { get; set; }
        public string ObjectType { get; set; }
        public string Origin { get; set; }
    }
}