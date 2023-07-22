

using System.Collections;

var q = new Queue<int>();

q.Enqueue(1);
q.Enqueue(7);



public class Queue<T> : IEnumerable<T>
{
    
	public QueueItem<T> Head { get; private set; }
	QueueItem<T> tail;

	public bool IsEmpty { get { return Head == null; } }

	public void Enqueue(T value)
	{
		if (IsEmpty)
			tail = Head = new QueueItem<T> { Value = value, Next = null };
		else
		{
			var item = new QueueItem<T> { Value = value, Next = null };
			tail.Next = item;
			tail = item;
		}
	}

	public T Dequeue()
	{
		if (Head == null) throw new InvalidOperationException();
		var result = Head.Value;
		Head = Head.Next;
		if (Head == null)
			tail = null;
		return result;
	}
    #region Interface
        #region Enumerable
        public virtual IEnumerator<T> GetEnumerator()
        {
            return new QueueIEnumerator<T>(this);
        }

        //Не будем останавливаться на этом. Просто всегда пишите так.
        //Это связано с архитектурными особенностями IEnumerable<T>,
        //И требует следующего уровня понимания архитектур
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
        #region Enumerator
    #endregion
    public class QueueIEnumerator<T> : IEnumerator<T>
    {
        private Queue<T> queue;
        private QueueItem<T> item1;
        public T Current => item1.Value;
        public QueueIEnumerator(Queue<T> queue)
        {
            this.queue = queue;
            this.item1 = null;
        }
        public bool MoveNext()
        {
            if (item1 == null) item1 = queue.Head;
            else               item1 = item1.Next;
            return             item1 != null; 
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
    #endregion  
}
public class QueueItem<T>
{
	public T Value { get; set; }
	public QueueItem<T> Next { get; set; }
}