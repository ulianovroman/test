namespace ConsoleApplication1
{
    using System.Collections.Generic;
    using System.Collections;
    using System.Threading;

    public class SyncQueue<T>
    {
        private Queue<T> internalQueue = new Queue<T>();
        private EventWaitHandle haveItemsEvent = new ManualResetEvent(false);
        private object popLock = new object();

        public void Push(T item)
        {
            lock (((ICollection)internalQueue).SyncRoot)
            {
                internalQueue.Enqueue(item);

                // Устанавливаем сигнал о том, что добавлен элемент
                haveItemsEvent.Set();
            }
        }

        public T Pop()
        {
            T poped;

            // Нам необходимо, чтобы лишь один поток единовременно ожидал сигнала о том, что элемент добавлен
            lock (popLock)
            {
                // Ждём сигнал
                haveItemsEvent.WaitOne();

                // Здесь заберем блокировку SyncRoot для синхронизации чтения\записи
                lock (((ICollection)internalQueue).SyncRoot)
                {
                    poped = internalQueue.Dequeue();
                    if (internalQueue.Count == 0)
                    {
                        haveItemsEvent.Reset();
                    }
                }
            }
            
            return poped;           
        }
    }
}
