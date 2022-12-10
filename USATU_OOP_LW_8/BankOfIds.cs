using System.Collections.Generic;

namespace USATU_OOP_LW_8;

public class BankOfIds
{
    public const int NotExistingId = -1;
    private static BankOfIds _bankOfIds;
    private static SortedSet<int> _usedIds;
    private const int FirstId = 0;

    private BankOfIds()
    {
        _usedIds = new SortedSet<int>();
    }

    public static BankOfIds GetInstance()
    {
        if (_bankOfIds == null)
        {
            _bankOfIds = new BankOfIds();
        }

        return _bankOfIds;
    }

    public int GetId()
    {
        int freeId = FirstId;
        while (_usedIds.Contains(freeId) || _usedIds.Contains(NotExistingId))
        {
            freeId++;
        }

        _usedIds.Add(freeId);
        return freeId;
    }

    public void ReturnId(int returnedId)
    {
        _usedIds.Remove(returnedId);
    }

    public void Clear()
    {
        _usedIds.Clear();
    }
}