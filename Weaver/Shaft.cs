using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weaver
{
    public class Shaft
    {
        private bool[] _up;

        /// <summary>
        /// Constructor defining size.
        /// The threads are set to be alternating up and down.
        /// </summary>
        /// <param name="size">Number of threads in shaft pattern, forced to be at least 1</param>
        public Shaft(int size)
        {
            size = size < 1 ? 1 : size;

            _up = new bool[size];

            _up[0] = true; // First warp thread is up
            for (int i = 1; i < _up.Length; i++)
            {
                _up[i] = !_up[i - 1];  // Alternate to previous
            }

            System.Console.WriteLine("Shaft(int) called");
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="old">Another Shaft object</param>
        public Shaft(Shaft old)
        {
            if (old != null)
            {
                _up = new bool[old._up.Length];

                for (var i = 0; i < _up.Length; i++)
                {
                    _up[i] = old._up[i];
                }
            }
            else
            {
                _up = new bool[1];
                _up[0] = true;
                // Alternatively throw exception   
            }

            System.Console.WriteLine("Shaft(old) called");
        }

        /// <summary>
        /// Read-only Property indicating the length of the shaft pattern
        /// </summary>
        public int Size
        {
            get
            {
                return _up.Length;
            }
        }

        /// <summary>
        /// Indexer, the value indicating if the corresponding warp thread is lifted, and therefore visible
        /// </summary>
        /// <param name="index">The index of the warp thread in the repeated shaft pattern</param>
        /// <returns>true when the warp thread is lifted up, and thus visible, false otherwise</returns>
        public bool this[int index]
        {
            get
            {
                return _up[index];
            }

            set 
            {
                 _up[index] = value;
            }
        }

        /// <summary>
        /// Resize the shaft pattern, keeping any old settings, adding an alternating setting if grown
        /// </summary>
        /// <param name="newSize">New size of shaft pattern</param>
        public void Resize(int newSize)
        {
            newSize = newSize < 1 ? 1 : newSize;

            bool[] temp = new bool[newSize];

            int i;
            for (i = 0; i < temp.Length && i < _up.Length; i++)
            {
                temp[i] = _up[i];
            }

            for (; i < temp.Length; i++)
            {
                temp[i] = !temp[i - 1];
            }

            _up = temp;
        }

        public override string ToString()
        {
            string result = "";

            for (int i = 0; i < _up.Length; i++)
            {
                result += (_up[i] ? "1" : "0");
            }

            return result;
        }

        /// <summary>
        /// Return a Shaft object, where the threads are reversed from the right hand side
        /// </summary>
        /// <param name="shaft">The right hand side of the unary expression !shaftObject</param>
        /// <returns></returns>
        public static Shaft operator!(Shaft shaft)
        {
            Shaft result = new Shaft(shaft);

            for (int i = 0; i < result._up.Length; i++)
            {
                result[i] = !result[i];
            }

            return result;
        }


    }
}
