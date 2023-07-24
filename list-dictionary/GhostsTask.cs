using System;
using System.Linq;
using System.Text;

namespace hashes;

public class GhostsTask : 
	IFactory<Document>, IFactory<Vector>, IFactory<Segment>, IFactory<Cat>, IFactory<Robot>, 
	IMagic
{
    byte[] test = new byte[] { 1,2,3};
    public Vector vector = new Vector(1, 1);
    public Vector vec { get; set; } = new Vector(0, 0);
    public Segment seg { get; set; } 
    public Document doc { get; set; }
    public Robot rob{ get; set; }=  new Robot("12", 100);
    public Cat cat { get; set; } = new Cat("Elephant", "Seamsky", new DateTime(2010, 5, 11));

    public GhostsTask()
    {
        doc = new Document("a",Encoding.Unicode, test);
        seg = new Segment(vector, new Vector(2, 2));
    }
    public void DoMagic()
    {
        vec.Add(new Vector(3, 4));
        cat.Rename(cat.Name + " ");
        Robot.BatteryCapacity = ++Robot.BatteryCapacity;
        test[0] = 2;
        vector.Add(new Vector(2, 3));
    }

    // Чтобы класс одновременно реализовывал интерфейсы IFactory<A> и IFactory<B> 
    // придется воспользоваться так называемой явной реализацией интерфейса.
    // Чтобы отличать методы создания A и B у каждого метода Create нужно явно указать, к какому интерфейсу он относится.
    // На самом деле такое вы уже видели, когда реализовывали IEnumerable<T>.

    Vector IFactory<Vector>.Create()
    {
       return vec;
    }

	Segment IFactory<Segment>.Create()
    {
        return seg;
    }

    Document IFactory<Document>.Create()
    {
        return doc;
    }

    Cat IFactory<Cat>.Create()
    {
        return cat;
    }

    Robot IFactory<Robot>.Create()
    {
        return rob;
    }
}