using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weaver
{
    public class Pattern
    {
        public WarpThread[] WarpThreads_;
        public WaftThread[] WaftThreads_;
        public Shaft[] Shafts_;

        public int NumWarps
        {
            get
            {
                return WarpThreads_.Length;
            }
        }

        public int NumWafts
        {
            get
            {
                return WaftThreads_.Length;
            }
        }


        public int NumShafts
        {
            get
            {
                return Shafts_.Length;
            }
        }

        public void SetWarpThread(int index, WarpThread warpThread)
        {
            WarpThreads_[index] = new WarpThread(warpThread.ThreadColor);
        }

        public void SetShaft(int index, Shaft shaft)
        {
            Shafts_[index] = shaft;
        }

        public void SetWaftThread(int index, System.Drawing.Color color, int shaftIndex)
        {
            WaftThreads_[index] = new WaftThread(color, Shafts_[shaftIndex]);
        }

        public System.Drawing.Color this[int Warp, int Waft]
        {
            get
            {
                System.Drawing.Color Result;
                if (WaftThreads_[Waft].Shaft[Warp])
                {
                    Result = WarpThreads_[Warp].ThreadColor;
                }
                else
                {
                    Result = WaftThreads_[Waft].ThreadColor;
                }

                return Result;
            }
        }

        public Pattern(int initWarpThreads, int initWaftThreads, int initShafts)
        {
            WarpThreads_ = new WarpThread[initWarpThreads];
            WaftThreads_ = new WaftThread[initWaftThreads];
            Shafts_ = new Shaft[initShafts];
        }
    }
}
