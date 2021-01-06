using System;
using System.Collections.Generic;
using System.Collections;

namespace Stos
{
    public class StosWTablicy<T> : IStos<T>
    {
        private T[] tab;
        private int szczyt = -1;

        public StosWTablicy(int size = 10)
        {
            tab = new T[size];
            szczyt = -1;
        }

        public T Peek => IsEmpty ? throw new StosEmptyException() : tab[szczyt];

        public int Count => szczyt + 1;

        public bool IsEmpty => szczyt == -1;

        public void Clear() => szczyt = -1;

        public T this[int index] => tab[index];

        public T Pop()
        {
            if (IsEmpty)
                throw new StosEmptyException();

            szczyt--;
            return tab[szczyt + 1];
        }

        public void Push(T value)
        {
            if (szczyt == tab.Length - 1)
            {
                Array.Resize(ref tab, tab.Length * 2);
            }

            szczyt++;
            tab[szczyt] = value;
        }

        public T[] ToArray()
        {
            //return tab;  //bardzo źle - reguły hermetyzacji

            //poprawnie:
            T[] temp = new T[szczyt + 1];
            for (int i = 0; i < temp.Length; i++)
                temp[i] = tab[i];
            return temp;
        }

        public void TrimExcess()
        {
            if (IsEmpty)
            {
                throw new StosEmptyException();
            }
            int size = Count;
            int newSize = size + Convert.ToInt32((1d / 10d) * size);
            if (newSize <= 5)
                newSize++;
            Array.Resize(ref tab, newSize);
        }

        public int Length()
        {
            return tab.Length;
        }

        public System.Collections.ObjectModel.ReadOnlyCollection<T> ToArrayReadOnly()
        {
            return Array.AsReadOnly(tab);
        }


        public IEnumerable<T> Revert
        {
            get
            {
                int tmp = Count - 1;
                while (tmp >= 0)
                {
                    yield return this[tmp];
                    tmp--;
                }

            }
        }

        public IEnumerator<T> ReturnEnum()
        {
            return new StackEnum(this);
        }

        private class StackEnum : IEnumerator<T>
        {
            private readonly StosWTablicy<T> stack;
            private int i = -1;
            internal StackEnum(StosWTablicy<T> stack)
            {
                this.stack = stack;
            }
            public T Current
            {
                get
                {
                    return stack.tab[i];
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public void Dispose() { }

            public override bool Equals(object obj)
            {
                return obj is StackEnum stacks &&
                       EqualityComparer<T>.Default.Equals(Current, stacks.Current);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Current);
            }

            public bool MoveNext()
            {
                if (i > stack.Count - 1)
                    return false;
                else
                {
                    i++;
                    return true;
                }

            }
            public void Reset() => i = -1;

            
        }

    }
}
