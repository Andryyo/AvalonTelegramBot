using Avalon.Core.Interfaces;
using Avalon.Core.Models;
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

            if (votes.Any(x => x == Enums.QuestCard.MissionFailed))
            {
                Context.Results.Add(Enums.QuestCard.MissionFailed);
            }
            else
            {
                Context.Results.Add(Enums.QuestCard.MissionSuccess);
            }

            await Context.SendMessage(string.Format("Quest tokens are {0}", string.Join(", ", votes)));
            await Context.SendMessage(string.Format("Quests results are {0}", string.Join(", ", Context.Results)));

            if (Context.Results.Count(x => x == Enums.QuestCard.MissionSuccess) == SuccessesRequired)
            {
                return new AssassinationPhase(Context);
            }

            if (Context.Results.Count(x => x == Enums.QuestCard.MissionFailed) == FailsRequired)
            {
                return new AssassinationPhase(Context);
            }

            return new TeamBuildingPhase(Context, Round, 0);
        }
    }
}
