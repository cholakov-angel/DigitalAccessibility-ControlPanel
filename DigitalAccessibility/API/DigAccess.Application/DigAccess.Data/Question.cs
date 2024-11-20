using System;
using System.Collections.Generic;

namespace DigAccess.Application;

public partial class Question
{
    public Guid Id { get; set; }

    public string UserId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime Date { get; set; }

    public bool IsAnswered { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual AspNetUser User { get; set; } = null!;
}
