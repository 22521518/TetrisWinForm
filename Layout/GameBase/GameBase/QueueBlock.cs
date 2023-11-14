using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameBase.Block;

namespace GameBase
{
    public class QueueBlock
    {
        private readonly Block.Block[] queue = new Block.Block[]
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
        public Block.Block nextBlock { get; private set; }
        public QueueBlock()
        {
            nextBlock = RandomBlock();
        }
        Block.Block RandomBlock()
        {
            return queue[random.Next() % queue.Length];
        }
        public Block.Block GetBlock()   
        {
            Block.Block temp = nextBlock;
            do
            {
                nextBlock = RandomBlock();
            } while (nextBlock.Id == temp.Id);
            nextBlock.Reset();
            return temp;
        }

    }
}
