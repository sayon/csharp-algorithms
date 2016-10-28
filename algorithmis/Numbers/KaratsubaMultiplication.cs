//sing System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Algorithmis.Numbers
//{
//    //todo: NOT DONE
//    /* Going from O(n^2) to O(n^log2(3))
//     * 
//     * 
//     * z2 = x1y1
//     * z1 = x1y0+ x0y1
//     * z0 = x0y0.
//     * 
//     * z1 = (x1 + x0)(y1 + y0) - z2 - z0
//     * which holds since

//     * z1 = x1y0+ x0y1
//     * z1 = (x1 + x0)(y1 + y0) - x1y1 - x0y0.
//     */

//    class KaratsubaMultiplication
//    {
//        private static int log2(long arg)
//        {
//            int log = 0;
//            while (arg > 0)
//            {
//                log++;
//                arg >>= 1;
//            }
//            return log;
//        }
//        private static long generateMask(int positionsToMask)
//        {
//            long mask = 1;
//            for (int i = 0; i < positionsToMask; i++)
//                mask <<= 1;
//            return mask - 1;
//        }
//        private static KeyValuePair<long, long> makePair(long arg)
//        {
//            int length = log2(arg);
//            long mask = generateMask(length / 2);

//            long snd = mask ^ arg;
//            long fst = mask ^ (~arg);
//            return new KeyValuePair<long, long>(fst, snd);
//        }

       
//    }
//}
