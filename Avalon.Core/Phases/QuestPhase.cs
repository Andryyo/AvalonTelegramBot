using Avalon.Core.Interfaces;
using Avalon.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalon.Core.Phases
{
    public class QuestPhase : IPhase
    {
        private readonly IEnumerable<IUser> participants;

        public QuestPhase(
            IAvalonContext context,
            IEnumerable<IUser> participants,
            int round)
        {
            Context = context;
            this.participants = participants;
            Round = round;
        }

        public IAvalonContext Context { get; }

        public int Round { get; }

        public async Task<IPhase> Execute()
        {
            var votes = await Task.WhenAll(Context.Users.Select(x => x.SelectQuestCard()));

            if (votes.Any(x => x == Enums.QuestCard.MissionFailed))
            {
                Context.Results.Add(Enums.QuestCard.MissionFailed);
            }
            else
            {
                Context.Results.Add(Enums.QuestCard.MissionSuccess);
            }

            if (Context.Results.Count(x => x == Enums.QuestCard.MissionSuccess) == 3)
            {
                return new AssassinationPhase(Context);
            }

            if (Context.Results.Count(x => x == Enums.QuestCard.MissionFailed) == 3)
            {
                return new AssassinationPhase(Context);
            }

            return new TeamBuildingPhase(Context, Round, 0);
        }
    }
}
