using Avalon.Core.Interfaces;
using Avalon.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avalon.Core.Phases
{
    public class QuestPhase : IPhase
    {
        private const int SuccessesRequired = 3;
        private const int FailsRequired = 3;
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
            await Context.SendMessage(string.Format("{0} are to decide quest outcome", string.Join(", ", participants.Select(x => x.Name))));

            var votes = await Task.WhenAll(participants.Select(x => x.SelectQuestCard()));

            if (Context.Results.Count != 0)
            {
                await Context.SendMessage(string.Format("Old quests results are {0}", string.Join(", ", Context.Results)));
            }

            var random = new Random();
            var shuffledVotes = votes.Select(x => new { Vote = x, Value = random.Next() }).OrderBy(x => x.Value).Select(x => x.Vote);

            if (votes.Any(x => x == Enums.QuestCard.MissionFailed))
            {
                await Context.SendMessage(string.Format("Quest failed, tokens are {0}", string.Join(", ", shuffledVotes)));

                Context.Results.Add(Enums.QuestCard.MissionFailed);
            }
            else
            {
                await Context.SendMessage(string.Format("Quest succeeded, tokens are {0}", string.Join(", ", shuffledVotes)));

                Context.Results.Add(Enums.QuestCard.MissionSuccess);
            }

            if (Context.Results.Count(x => x == Enums.QuestCard.MissionSuccess) == SuccessesRequired)
            {
                return new AssassinationPhase(Context);
            }

            if (Context.Results.Count(x => x == Enums.QuestCard.MissionFailed) == FailsRequired)
            {
                return new AssassinationPhase(Context);
            }

            return new TeamBuildingPhase(Context, Round + 1, 0);
        }
    }
}
