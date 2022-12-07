using System.Windows.Forms;
using CustomDoublyLinkedListLibrary;

namespace USATU_OOP_LW_8;

public class GraphicObjectsListObserverTreeViewUpdater
{
    public delegate void OnTreeNeedUpdate(TreeNode treeNode);

    public event OnTreeNeedUpdate TreeNeedUpdate;

    private readonly GraphicObjectsList _graphicObjectsList;

    public GraphicObjectsListObserverTreeViewUpdater(GraphicObjectsList graphicObjectsList)
    {
        _graphicObjectsList = graphicObjectsList;
    }

    public void UpdateChanges()
    {
        var nodes = ConvertCustomListToTreeNodes(_graphicObjectsList.GetPointerOnBeginning());
        TreeNeedUpdate?.Invoke(nodes);
    }

    public void ProcessTreeSelectionToObjects(TreeNode selectedNode, bool isChecked)
    {
        int lastIndex = 0;
        var currentNode = selectedNode;
        while (currentNode != null)
        {
            lastIndex = currentNode.Index;
            currentNode = currentNode.Parent;
        }

        var pointer = _graphicObjectsList.GetPointerOnBeginning();
        for (int i = 0; i < lastIndex; i++)
        {
            pointer.MoveNext();
        }

        if (isChecked)
        {
            pointer.Current.Select();
        }
        else
        {
            pointer.Current.Unselect();
        }

        UpdateChanges();
    }

    private TreeNode ConvertCustomListToTreeNodes(PointerCustomDoublyLinkedList<GraphicObject> pointer)
    {
        var newTreeNode = new TreeNode();
        for (; !pointer.IsBorderReached(); pointer.MoveNext())
        {
            var tmpTreeNode = new TreeNode();
            if (pointer.Current.IsGroup)
            {
                var currentGroupList = ((GraphicObjectGroup) pointer.Current).GetAllGraphicObjects();
                tmpTreeNode = ConvertCustomListToTreeNodes(currentGroupList.GetPointerOnBeginning());
            }

            tmpTreeNode.Text = pointer.Current.GetName();
            tmpTreeNode.Checked = pointer.Current.IsObjectSelected();
            newTreeNode.Nodes.Add(tmpTreeNode);
        }

        return newTreeNode;
    }
}