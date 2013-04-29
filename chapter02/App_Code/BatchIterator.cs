using System;
using System.Collections;
using System.Collections.Generic;

namespace chapter_02_core_libraries
{
    public class BatchIterator<T> : IEnumerable<ICollection<T>>
    {
        BatchSource<T> batchDataSource;
        private int batchIndex = 0;

        /// <summary>
        /// Creates a new batch iterator.
        /// </summary>
        /// <param name="batchSource">
        /// The delegate that returns collections to iterate over. 
        /// Each collection represents a "batch" of data.
        /// </param>
        public BatchIterator(BatchSource<T> batchSource)
        {
            this.batchDataSource = batchSource;
        }

        ///<summary>
        ///Returns an enumerator that iterates through the collection.
        ///</summary>
        ///<returns>
        ///A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection.
        ///</returns>
        ///<filterpriority>1</filterpriority>
        public IEnumerator<ICollection<T>> GetEnumerator()
        {
            //First batch.
            ICollection<T> nextBatch = this.batchDataSource(0);
            while (nextBatch != null)
            {
                yield return nextBatch;
                nextBatch = this.batchDataSource(++batchIndex);
            }
        }

        ///<summary>
        ///Returns an enumerator that iterates through a collection.
        ///</summary>
        ///<returns>
        ///An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public delegate ICollection<T> BatchSource<T>(int batchIndex);
}
