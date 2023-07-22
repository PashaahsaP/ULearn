using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace hashes
{
    public class ReadonlyBytes : IEnumerable <byte>   
    {
        private int Hash { get; set; }
        private byte[] Data { get;}
        public int Length { get => Data.Length; }
        public ReadonlyBytes(params byte[] income)
        {
            if (income == null) throw new ArgumentNullException();
            Data = income;
        }

        public IEnumerator<byte> GetEnumerator()
        {
            for (int i = 0; i < Data.Length; i++)
            {
                yield return Data[i];
            }
        }

        public override bool Equals(object obj)
        {
            if(obj != null && obj.GetType() != this.GetType()) return false;
            if (obj is ReadonlyBytes arr)
            {
                if (arr.Data.Length != this.Data.Length) return false;
                if (arr.Hash != 0 && this.Hash != 0)
                    return CompareHashes(arr, this);
                return this.GetHashCode() == arr.GetHashCode();
            }
            return false;
        }

        private bool CompareHashes(ReadonlyBytes firstArr, ReadonlyBytes secondArr)
        {
            int prime = 1677761999;
            int firstHash = unchecked((int)2166136261);
            int secondHash = unchecked((int)2166136261);

            for (int i = 0; i < firstArr.Data.Length; i++)
            {
                unchecked
                {
                    firstHash ^= firstArr[i];
                    firstHash *= prime;
                    secondHash ^= secondArr[i];
                    secondHash *= prime;
                }
                if (firstHash != secondHash) return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            if(Hash != 0) return Hash;
            int prime = 1677761999;
            int hash = unchecked((int)2166136261);

            foreach (byte b in Data)
            {
                unchecked
                {
                    hash ^= b;
                    hash *= prime;
                }
            }

            Hash = hash;
            return hash;
        }

        public override string ToString() => $"[{string.Join(", ", Data)}]";

        public byte this[int key]
        {
            get
            {
                if(key >=Data.Length) throw new IndexOutOfRangeException(); 
                return Data[key];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}