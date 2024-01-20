namespace Tailoring.Entities;

public class PostParams
{

    public int PageNumber { get; set; }
    public int pageSize { get; set; }

    public PostParams()
    {
        this.PageNumber = 1;
        this.pageSize = 30;
    }

    public PostParams(int pageNumber, int pageSize)
    {
        this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
        this.pageSize = pageSize > 10 ? 10 : pageSize;

    }

    public static bool TryParse(string? value, IFormatProvider? provider,
        out PostParams? param)
    {
        // Format is "(12.3,10.1)"
        var trimmedValue = value?.TrimStart('(').TrimEnd(')');
        var segments = trimmedValue?.Split(',',
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (segments?.Length == 2
            && int.TryParse(segments[0], out var pageSize)
            && int.TryParse(segments[1], out var pageNumber))
        { 
            param = new PostParams { pageSize = pageSize, PageNumber = pageNumber };
            return true;
        }

        param = null;
        return false;
        
    }
}

