using UnityEngine;

namespace ClusterVR.CreatorKit.World
{
    public interface ITextView
    {
        void SetFontAndShader(Font font, Shader shader);

        void SetText(string text);
        void SetSize(float size);
        void SetTextAnchor(TextAnchor textAnchor);
        void SetTextAlignment(TextAlignment textAlignment);
        void SetColor(Color color);

        string Text { get; }
        float Size { get; }
        TextAnchor TextAnchor { get; }
        TextAlignment TextAlignment { get; }
        Color Color { get; }
    }
}
