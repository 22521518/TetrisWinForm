using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurvivalPvP.Object;

namespace SurvivalPvP
{
    public class QueueBlock
    {
        private readonly Block[] queue = new Block[]
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
        public Block nextBlock { get; private set; }
        public QueueBlock()
        {
            nextBlock = RandomBlock();
        }
        Block RandomBlock()
        {
            return queue[random.Next() % queue.Length];
        }
        public Block GetBlock()   
        {
            Block temp = nextBlock;
            do
            {
                nextBlock = RandomBlock();
            } while (nextBlock.Id == temp.Id);
            nextBlock.Reset();
            return temp;
        }

    }
}
