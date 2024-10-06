﻿namespace DcaCalculator.Domain.Common.Interfaces
{
    public interface IAuditableEntity
    {
        DateTime? CreatedDate { get; set; }
        DateTime? UpdatedDate { get; set; }
    }
}
