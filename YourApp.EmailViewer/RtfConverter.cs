using System.Windows.Documents;
using System.IO;
using System.Windows;

public class RtfConverter
{
    public string ConvertTextToRtf(string text)
    {
        TextRange textRange = new TextRange(new FlowDocument(new Paragraph(new Run(text))).ContentStart, new FlowDocument(new Paragraph(new Run(text))).ContentEnd);
        using (MemoryStream memoryStream = new MemoryStream())
        {
            textRange.Save(memoryStream, DataFormats.Rtf);
            return System.Text.Encoding.Default.GetString(memoryStream.ToArray());
        }
    }

    public string ConvertRtfToText(string rtf)
    {
        TextRange textRange = new TextRange(new FlowDocument().ContentStart, new FlowDocument().ContentEnd);
        using (MemoryStream memoryStream = new MemoryStream(System.Text.Encoding.Default.GetBytes(rtf)))
        {
            textRange.Load(memoryStream, DataFormats.Rtf);
            return textRange.Text;
        }
    }
}