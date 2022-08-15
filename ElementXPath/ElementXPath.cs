using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace Knazs.Xml.XPath;

public sealed class XElementNames : IEnumerable<string>
{
    private XElementNames(ArraySegment<string> elementNames)
    {
        _elementNames = elementNames;
    }

    private readonly ArraySegment<string> _elementNames;

    public static XElementNames Parse(string xpath)
    {
        if (!TryParse(xpath, out var xelementNames))
        {
            throw new FormatException(nameof(xpath));
        }

        return xelementNames;
    }

    public static bool TryParse(string xabsolutePath, [NotNullWhen(true)] out XElementNames? xelementNames)
    {
        if (string.IsNullOrEmpty(xabsolutePath))
        {
            goto FalseCase;
        }

        var xsubPaths = xabsolutePath.Split('/');

        string xsubPath;
        var xsubPathsCount = xsubPaths.Length;

        var index = Convert.ToInt32(string.IsNullOrEmpty(xsubPaths[0]));

        for (; index < xsubPathsCount; ++index)
        {
            xsubPath = xsubPaths[index];

            if (!XmlReader.IsName(xsubPath))
            {
                goto FalseCase;
            }
        }

        ArraySegment<string> elementNames = new(xsubPaths, 1, xsubPathsCount - 1);

        xelementNames = new(elementNames);

        return true;

    FalseCase:

        xelementNames = default;

        return false;
    }

    public IEnumerator<string> GetEnumerator()
    {
        return _elementNames.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _elementNames.GetEnumerator();
    }
}
