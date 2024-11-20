using System;
using System.Collections.Generic;

namespace DigAccess.Application;

public partial class Answer
{
    public Guid Id { get; set; }

    public Guid QuestionId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime Date { get; set; }

    public bool IsReviewed { get; set; }

    public virtual Question Question { get; set; } = null!;
}
