using System.Collections.Generic;
using System.Linq;

namespace Clones;

public class CloneVersionSystem : ICloneVersionSystem
{
    public Dictionary<string,Caminuan> CaminuanDic;
    int CumCount { get; set; }

    #region ctor
    public CloneVersionSystem()
    {
        CaminuanDic = new Dictionary<string, Caminuan>(){{"1", new Caminuan("1")}};
        CumCount = 1;
    }

    #endregion

	public string Execute(string query)
    {
        var command = query.Split();
        switch (command[0])
        {
            case "learn": return LearnCommand(command[1], command[2]);
            case "rollback": return RollBackCommand(command[1]);
            case "relearn": return RelearnCommand(command[1]); 
            case "clone": return CloneCommand(command[1]); 
            case "check": return CheckCommand(command[1]); 
                default: return null;
        }
        
    }
    public string LearnCommand( string caminuanNumber, string skill)
    {
         
        if (CaminuanDic.ContainsKey(caminuanNumber))
            CaminuanDic[caminuanNumber].Skills.AddFirst(skill); 
        else
        {
            CumCount++;
            CaminuanDic.Add(caminuanNumber, new Caminuan(caminuanNumber, skill));
        }

        return null;
    }
    public string RollBackCommand( string caminuanNumber )
    {
        if (CaminuanDic.ContainsKey(caminuanNumber))
        { 
           var temp = CaminuanDic[caminuanNumber].Skills.RemoveFirst();
           CaminuanDic[caminuanNumber].RollbackSkills.AddFirst(temp);
        }
        else
            return null;

        return null;
    }
    public string RelearnCommand( string caminuanNumber)
    {
        if (CaminuanDic.ContainsKey(caminuanNumber))
        {
            var temp = CaminuanDic[caminuanNumber].RollbackSkills.RemoveFirst();
            CaminuanDic[caminuanNumber].Skills.AddFirst(temp);
        }
        else
            return null;

        return null;
    }
    public string CloneCommand( string caminuanNumber)
    {
        if (CaminuanDic.ContainsKey(caminuanNumber))
        {
            ++CumCount;
            var cum = CaminuanDic[caminuanNumber];
            CaminuanDic.Add(CumCount.ToString(), new Caminuan(cum.Skills.Head,cum.RollbackSkills.Head));
        }
        else
            return null;

        return null;
    }
    public string CheckCommand( string caminuanNumber)
    {
        if (CaminuanDic.ContainsKey(caminuanNumber))
        {
            var cum = CaminuanDic[caminuanNumber];
            if (cum.Skills.Head == null) return "basic";
            else return cum.Skills.Head.Value;
        }
        else
            return null;
  
        return null;
    }

}

public class Caminuan
{
    public string SerialNumber { get; set; }
    public NodeList<string> Skills { get; set; }
    public NodeList<string> RollbackSkills { get; set; }

    public Caminuan(Node<string> skills, Node<string> rollbackSkills)
    {
        Skills = new();
        RollbackSkills = new(); 
        Skills.Head =skills;
        RollbackSkills.Head =rollbackSkills;
    }
    public Caminuan(string number, string skill):this(number)
    {
        Skills.AddFirst(skill);
        
    }
    public Caminuan(string number)
    {
        SerialNumber = number;
        RollbackSkills = new NodeList<string>();
        Skills = new NodeList<string>();
    }

}

public class Node<T>
{
    public Node<T> Next { get; set; }
    public T Value { get; set; }

    public Node(T value)
    {
        Value=value;
    }
}

public class NodeList<T>
{
    public Node<T> Head { get; set; }

    public NodeList()
    {

    }

    public void AddFirst(T value)
    {
        if (Head != null)
        {
            var temp = Head;
            Head = new Node<T>(value);
            Head.Next = temp;
        }
        else
        {
            Head = new Node<T>(value);
        }
    }

    public T RemoveFirst()
    {
        if (Head == null) return default(T);
        if (Head.Next == null)
        {
            var temp = Head.Value;
            Head = null;
            return temp;
        }
        else
        {
            var temp = Head.Value;
            Head = Head.Next;
            return temp;
        }

        return default(T);
    }

}