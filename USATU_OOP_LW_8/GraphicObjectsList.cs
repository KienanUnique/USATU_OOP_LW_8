using System.IO;
using System.Text;
using CustomDoublyLinkedListLibrary;

namespace USATU_OOP_LW_8;

public class GraphicObjectsList : CustomDoublyLinkedList<GraphicObject>
{
    private const string CountPrefix = "Count: ";

    public void ParseGraphicObjects(StringReader dataStringReader,
        GraphicObjectsAbstractFactory graphicObjectsAbstractFactory)
    {
        int.TryParse(dataStringReader.ReadLine(), out int countOfElements);
        for (int i = 0; i < countOfElements; i++)
        {
            var readObject = graphicObjectsAbstractFactory.ParseGraphicObject(dataStringReader.ReadLine());
            readObject.loadData(dataStringReader);
            Add(readObject);
        }
    }

    public string PrepareDataToStore()
    {
        var allDataBuilder = new StringBuilder();
        allDataBuilder.AppendLine(CountPrefix + Count);
        allDataBuilder.AppendLine();

        var objectsDataBuilder = new StringBuilder();
        for (var i = GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            objectsDataBuilder.AppendLine(i.Current.PrepareDataToStore());
        }

        objectsDataBuilder.Replace("\n", "\n\t");
        allDataBuilder.Append(objectsDataBuilder);
        return allDataBuilder.ToString();
    }
}