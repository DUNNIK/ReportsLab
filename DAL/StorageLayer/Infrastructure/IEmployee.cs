﻿
using System.Collections.Generic;

namespace DAL.StorageLayer.Infrastructure
{
    public interface IEmployee
    {
        string Id { get; }
        string Name { get; }
    }
}