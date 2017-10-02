using System;

namespace count.less.solutions.Models.Domain
{
    public interface IHaveAuditInformation
    {
        DateTime CreatedAt { get; set; }

        DateTime UpdatedAt { get; set; }
    }
}