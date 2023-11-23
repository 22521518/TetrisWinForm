using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameBase.Object;

namespace GameBase
{
    public class QueueBlock
    {
        private readonly Object.Block[] queue = new Object.Block[]
        {
            new IBlock(),
            new JBlock(),
            new LBlock(),
            new OBlock(),
            new SBlock(),
            new TBlock(),
            new ZBlock()
        };
        readonly Random random = new Random();
        public Object.Block nextBlock { get; private set; }
        public QueueBlock()
        {
            nextBlock = RandomBlock();
        }
        Object.Block RandomBlock()
        {
            return queue[random.Next() % queue.Length];
        }
        public Object.Block GetBlock()   
        {
            Object.Block temp = nextBlock;
            do
            {
                nextBlock = RandomBlock();
            } while (nextBlock.Id == temp.Id);
            nextBlock.Reset();
            return temp;
        }

    }
}
